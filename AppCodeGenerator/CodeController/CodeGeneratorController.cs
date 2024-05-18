using CodeGenerator;
using Microsoft.AspNetCore.Mvc;

namespace CodeGenerator.Controllers
{
    public class CodeGeneratorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.GeneratorType = "Controller";
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(CodeGeneratorModel model)
        {
            using var genType = new EnumService<enGeneratorType>();
            CodeSessionService.GeneratorType = model.GeneratorType;
            string str_action_name = $"Create{model.GeneratorType}";
            if (!genType.NameExists(model.GeneratorType)) str_action_name = "Index";
            return RedirectToAction(str_action_name, "CodeGenerator", new { area = "" });
        }
        [HttpGet]
        public ActionResult CreateArea()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.AreaName = "";
            CodeSessionService.Affecteds = 0;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateArea(CodeGeneratorModel model)
        {
            if (string.IsNullOrEmpty(model.AreaName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("AreaName", "區域名稱不可空白!!");
                return View(model);
            }
            using var codeCreate = new CodeCreate();
            string str_message = codeCreate.CreateArea(model);
            if (string.IsNullOrEmpty(str_message))
            {
                using var CodeMessage = new CodeMessage();
                str_message = CodeMessage.CreateAreaMessage(model);
            }
            CodeSessionService.MessageText = str_message;
            return RedirectToAction("Message", "CodeGenerator", new { area = "" });
        }
        [HttpGet]
        public ActionResult CreateController()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.ProjectName = CodeSessionService.ProjectName;
            model.AreaName = "";
            model.ControllerName = "";
            model.UseModel = false;
            model.ModelName = "";
            model.ModelNameSpace = "";
            model.ModelClassName = "";
            model.TemplateName = "Empty";
            model.ForceOverride = false;
            model.BaseController = true;
            CodeSessionService.Affecteds = 0;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateController(CodeGeneratorModel model)
        {
            if (model.AreaName == "空白") model.AreaName = "";
            if (string.IsNullOrEmpty(model.ControllerName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ControllerName", "控制器名稱不可空白!!");
                return View(model);
            }
            model.ModelNameSpace = "";
            model.ModelClassName = "";
            if (model.UseModel)
            {
                model.ModelClassName = model.ModelName;
                if (model.ModelName.Contains("."))
                {
                    var data = model.ModelName.Split(".").ToList();
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (i == (data.Count - 1))
                            model.ModelClassName = data[i];
                        else
                        {
                            if (!string.IsNullOrEmpty(model.ModelNameSpace)) model.ModelNameSpace += ".";
                            model.ModelNameSpace += data[i];
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(model.ModelClassName)) model.ModelClassName = "Empty";
            using var codeCreate = new CodeCreate();
            string str_message = codeCreate.CreateController(model);
            if (string.IsNullOrEmpty(str_message))
            {
                using var CodeMessage = new CodeMessage();
                str_message = CodeMessage.CreateControllerMessage(model, false);
            }
            CodeSessionService.MessageText = str_message;
            return RedirectToAction("Message", "CodeGenerator", new { area = "" });
        }
        [HttpGet]
        public ActionResult CreateView()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.ProjectName = CodeSessionService.ProjectName;
            model.AreaName = "";
            model.ControllerName = "";
            model.ViewName = "";
            model.TemplateName = "";
            model.LayoutName = "";
            model.ModelName = "";
            model.ModelNameSpace = "";
            model.ModelClassName = "";
            model.CardSize = "Medium";
            model.ForceOverride = false;
            model.UseModel = false;
            model.UseCard = false;
            model.LayoutColCount = 1;
            model.LabelScaleSize = 2;
            model.LabelScaleSizeName = "2";
            CodeSessionService.Affecteds = 0;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateView(CodeGeneratorModel model)
        {
            if (string.IsNullOrEmpty(model.ViewName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ViewName", "網頁(View)名稱不可空白!!");
                return View(model);
            }
            if (model.UseModel && string.IsNullOrEmpty(model.ModelName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ModelName", "資料模型名稱不可空白!!");
                return View(model);
            }
            if (model.LayoutName == "空白") model.LayoutName = "_Layout";
            model.ModelNameSpace = "";
            model.ModelClassName = "";
            if (model.UseModel)
            {
                model.ModelClassName = model.ModelName;
                if (model.ModelName.Contains("."))
                {
                    var data = model.ModelName.Split(".").ToList();
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (i == (data.Count - 1))
                            model.ModelClassName = data[i];
                        else
                        {
                            if (!string.IsNullOrEmpty(model.ModelNameSpace)) model.ModelNameSpace += ".";
                            model.ModelNameSpace += data[i];
                        }
                    }
                }
            }
            model.LabelScaleSize = int.Parse(model.LabelScaleSizeName);
            if (string.IsNullOrEmpty(model.ModelClassName)) model.ModelClassName = "Empty";
            var datas = model.ControllerName.Split("/").ToList();
            if (datas.Count == 2)
            {
                model.AreaName = "";
                model.ControllerName = model.ControllerName = datas[1];
            }
            else if (datas.Count == 4)
            {
                model.AreaName = model.ControllerName = datas[1];
                model.ControllerName = model.ControllerName = datas[3];
            }
            if (model.ControllerName.Contains("."))
            {
                datas = model.ControllerName.Split('.').ToList();
                model.ControllerName = datas[0];
                model.ControllerName = model.ControllerName.Substring(0, model.ControllerName.Length - 10);
            }

            CodeSessionService.ProjectName = model.ProjectName;
            CodeSessionService.AreaName = model.AreaName;
            CodeSessionService.ControllerName = model.ControllerName;
            CodeSessionService.ViewName = model.ViewName;
            CodeSessionService.TemplateName = model.TemplateName;
            CodeSessionService.LayoutName = model.LayoutName;
            CodeSessionService.ModelName = model.ModelName;
            CodeSessionService.ModelNameSpace = model.ModelNameSpace;
            CodeSessionService.ModelClassName = model.ModelClassName;
            CodeSessionService.CardSize = model.CardSize;
            CodeSessionService.ForceOverride = model.ForceOverride;
            CodeSessionService.UseModel = model.UseModel;
            CodeSessionService.UseCard = model.UseCard;
            CodeSessionService.LayoutColCount = model.LayoutColCount;
            CodeSessionService.LabelScaleSize = model.LabelScaleSize;
            CodeSessionService.LabelScaleSizeName = model.LabelScaleSizeName;

            return RedirectToAction("CreateViewList", "CodeGenerator", new { area = "" });
        }
        [HttpGet]
        public ActionResult CreateViewList()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.ProjectName = CodeSessionService.ProjectName;
            model.AreaName = CodeSessionService.AreaName;
            model.ControllerName = CodeSessionService.ControllerName;
            model.ViewName = CodeSessionService.ViewName;
            model.TemplateName = CodeSessionService.TemplateName;
            model.LayoutName = CodeSessionService.LayoutName;
            model.ModelName = CodeSessionService.ModelName;
            model.ModelNameSpace = CodeSessionService.ModelNameSpace;
            model.ModelClassName = CodeSessionService.ModelClassName;
            model.CardSize = CodeSessionService.CardSize;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.UseModel = CodeSessionService.UseModel;
            model.UseCard = CodeSessionService.UseCard;
            model.LayoutColCount = CodeSessionService.LayoutColCount;
            model.LabelScaleSize = CodeSessionService.LabelScaleSize;
            model.LabelScaleSizeName = CodeSessionService.LabelScaleSizeName;
            model.SelectAllColumn = true;
            model.DisplayColumns = new List<string>();
            model.DropDownColumns = new List<string>();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateViewList(CodeGeneratorModel model)
        {
            model.ProjectName = CodeSessionService.ProjectName;
            model.AreaName = CodeSessionService.AreaName;
            model.ControllerName = CodeSessionService.ControllerName;
            model.ViewName = CodeSessionService.ViewName;
            model.TemplateName = CodeSessionService.TemplateName;
            model.LayoutName = CodeSessionService.LayoutName;
            model.ModelName = CodeSessionService.ModelName;
            model.ModelNameSpace = CodeSessionService.ModelNameSpace;
            model.ModelClassName = CodeSessionService.ModelClassName;
            model.CardSize = CodeSessionService.CardSize;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.UseModel = CodeSessionService.UseModel;
            model.UseCard = CodeSessionService.UseCard;
            model.LayoutColCount = CodeSessionService.LayoutColCount;
            model.LabelScaleSize = CodeSessionService.LabelScaleSize;
            model.LabelScaleSizeName = CodeSessionService.LabelScaleSizeName;
            model.ModelNameSpace = "";
            model.ModelClassName = "";
            if (model.UseModel)
            {
                model.ModelClassName = model.ModelName;
                if (model.ModelName.Contains("."))
                {
                    var data = model.ModelName.Split(".").ToList();
                    int int_count = data.Count - 1;
                    model.ModelClassName = data[int_count];

                    for (int i = 0; i < int_count; i++)
                    {
                        if (!string.IsNullOrEmpty(model.ModelNameSpace)) model.ModelNameSpace += ".";
                        model.ModelNameSpace += data[i];
                    }
                }
            }
            if (string.IsNullOrEmpty(model.ModelClassName)) model.ModelClassName = "Empty";
            CodeSessionService.Affecteds = 0;
            CodeSessionService.DeleteNo = model.DeleteNo;
            CodeSessionService.DeleteName = model.DeleteName;
            using var codeCreate = new CodeCreate();
            string str_message = codeCreate.CreateView(model);
            if (string.IsNullOrEmpty(str_message))
            {
                using var CodeMessage = new CodeMessage();
                str_message = CodeMessage.CreateViewMessage(model, false);
            }
            CodeSessionService.MessageText = str_message;
            return RedirectToAction("Message", "CodeGenerator", new { area = "" });
        }
        [HttpGet]
        public ActionResult CreateDbContext()
        {
            using var codeService = new CodeGeneratorService();
            CodeGeneratorModel model = new CodeGeneratorModel();
            CodeSessionService.ProjectName = CodeSessionService.ProjectName;
            model.ProjectName = CodeSessionService.ProjectName;
            model.ConnStringName = "dbconn";
            model.DbContextName = "dbEntities";
            model.DBContextType = "DBContextAndTable";
            model.ConnectionString = "";
            model.CreateView = true;
            model.ForceOverride = true;
            model.TableSelectType = "1";
            CodeSessionService.Affecteds = 0;
            var conns = codeService.GetConnNameList().FirstOrDefault();
            if (conns != null) model.ConnectionString = codeService.GetConnectionString(conns);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateDbContext(CodeGeneratorModel model)
        {
            if (string.IsNullOrEmpty(model.DbContextName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("DbContextName", "資料庫模型名稱不可空白!!");
                return View(model);
            }
            if (string.IsNullOrEmpty(model.ConnStringName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ConnStringName", "連線字串名稱不可空白!!");
                return View(model);
            }
            using var codeService = new CodeGeneratorService();
            string str_message = codeService.CheckConnectionString(model.ConnStringName);
            if (!string.IsNullOrEmpty(str_message))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ConnStringName", str_message);
                return View(model);
            }
            CodeSessionService.ConnStringName = model.ConnStringName;
            CodeSessionService.DbContextName = model.DbContextName;
            CodeSessionService.DBContextType = model.DBContextType;
            CodeSessionService.CreateView = model.CreateView;
            CodeSessionService.ForceOverride = model.ForceOverride;
            CodeSessionService.TableSelectType = model.TableSelectType;
            CodeSessionService.SelectAll = false;
            CodeSessionService.TableViewsList = "";
            if (model.DBContextType == "DBContextOnly") return RedirectToAction("CreateDbContextFiles", "CodeGenerator", new { area = "" });
            return RedirectToAction("CreateDbContextTables", "CodeGenerator", new { area = "" });
        }
        [HttpGet]
        public ActionResult CreateDbContextTables()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.ConnStringName = CodeSessionService.ConnStringName;
            model.DbContextName = CodeSessionService.DbContextName;
            model.DBContextType = CodeSessionService.DBContextType;
            model.CreateView = CodeSessionService.CreateView;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.TableSelectType = CodeSessionService.TableSelectType;
            model.TableViewName = "";
            model.SelectAll = false;
            model.TableViews = new List<string>();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateDbContextTables(CodeGeneratorModel model)
        {
            CodeSessionService.TableViewsList = "";
            CodeSessionService.SelectAll = model.SelectAll;
            CodeSessionService.TableViewName = model.TableViewName;
            model.TableSelectType = CodeSessionService.TableSelectType;
            if (model.TableSelectType == "1")
            {
                CodeSessionService.TableViewsList = model.TableViewName;
            }
            else
            {
                if (!model.SelectAll && model.TableViews.Count() > 0)
                {
                    string str_value = "";
                    foreach (var item in model.TableViews)
                    {
                        if (!string.IsNullOrEmpty(str_value)) str_value += ",";
                        str_value += item;
                    }
                    CodeSessionService.TableViewsList = str_value;
                }
            }
            if (model.DBContextType != "DBContextOnly")
            {
                if (!model.SelectAll && model.TableSelectType == "2" && (model.TableViews == null || model.TableViews.Count() <= 0))
                {
                    ModelState.ClearAllErrors();
                    ModelState.AddModelError("SelectAll", "未選擇任何表格!!");
                    return View(model);
                }
            }
            return RedirectToAction("CreateDbContextFiles", "CodeGenerator", new { area = "" });
        }

        [HttpGet]
        public ActionResult CreateDbContextFiles()
        {
            var model = new CodeGeneratorModel();
            model.ProjectName = CodeSessionService.ProjectName;
            model.ConnStringName = CodeSessionService.ConnStringName;
            model.DbContextName = CodeSessionService.DbContextName;
            model.DBContextType = CodeSessionService.DBContextType;
            model.CreateView = CodeSessionService.CreateView;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.TableSelectType = CodeSessionService.TableSelectType;
            model.SelectAll = CodeSessionService.SelectAll;
            model.TableViewName = CodeSessionService.TableViewName;
            model.TableViews = new List<string>();
            if (!string.IsNullOrEmpty(CodeSessionService.TableViewsList))
            {
                model.TableViews = CodeSessionService.TableViewsList.Split(',').ToList();
            }
            using var codeCreate = new CodeCreate();
            string str_message = codeCreate.CreateDbContext(model);
            if (string.IsNullOrEmpty(str_message))
            {
                using var CodeMessage = new CodeMessage();
                str_message = CodeMessage.CreateDbContextMessage(model, false);
            }
            CodeSessionService.MessageText = str_message;
            return RedirectToAction("Message", "CodeGenerator", new { area = "" });
        }

        [HttpGet]
        public ActionResult CreateMetadata()
        {
            using var codeService = new CodeGeneratorService();
            CodeGeneratorModel model = new CodeGeneratorModel();
            CodeSessionService.ProjectName = CodeSessionService.ProjectName;
            model.ProjectName = CodeSessionService.ProjectName;
            model.ConnStringName = "dbconn";
            model.DbContextName = "dbEntities";
            model.ConnectionString = "";
            model.ForceOverride = false;
            model.TableSelectType = "1";
            CodeSessionService.Affecteds = 0;
            var conns = codeService.GetConnNameList().FirstOrDefault();
            if (conns != null) model.ConnectionString = codeService.GetConnectionString(conns);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMetadata(CodeGeneratorModel model)
        {
            if (string.IsNullOrEmpty(model.ConnStringName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ConnStringName", "連線字串名稱不可空白!!");
                return View(model);
            }
            using var codeService = new CodeGeneratorService();
            string str_message = codeService.CheckConnectionString(model.ConnStringName);
            if (!string.IsNullOrEmpty(str_message))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ConnStringName", str_message);
                return View(model);
            }
            CodeSessionService.ConnStringName = model.ConnStringName;
            CodeSessionService.ForceOverride = model.ForceOverride;
            CodeSessionService.TableSelectType = model.TableSelectType;
            return RedirectToAction("CreateMetadataTables", "CodeGenerator", new { area = "" });
        }

        [HttpGet]
        public ActionResult CreateMetadataTables()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.ConnStringName = CodeSessionService.ConnStringName;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.TableSelectType = CodeSessionService.TableSelectType;
            model.SelectAll = false;
            model.TableViews = new List<string>();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMetadataTables(CodeGeneratorModel model)
        {
            if (!model.SelectAll && model.TableSelectType == "2" && (model.TableViews == null || model.TableViews.Count() <= 0))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("SelectAll", "未選擇任何表格!!");
                return View(model);
            }

            model.ProjectName = CodeSessionService.ProjectName;
            model.ConnStringName = CodeSessionService.ConnStringName;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.TableSelectType = CodeSessionService.TableSelectType;

            using var codeCreate = new CodeCreate();
            string str_message = codeCreate.CreateMetadata(model);
            if (string.IsNullOrEmpty(str_message))
            {
                using var CodeMessage = new CodeMessage();
                str_message = CodeMessage.CreateMetaDataMessage(model, false);
            }
            CodeSessionService.MessageText = str_message;
            return RedirectToAction("Message", "CodeGenerator", new { area = "" });
        }

        [HttpGet]
        public ActionResult CreateRepository()
        {
            using var codeService = new CodeGeneratorService();
            CodeGeneratorModel model = new CodeGeneratorModel();
            CodeSessionService.ProjectName = CodeSessionService.ProjectName;
            model.ProjectName = CodeSessionService.ProjectName;
            model.ConnStringName = "dbconn";
            model.ConnectionString = "";
            model.ForceOverride = false;
            CodeSessionService.Affecteds = 0;
            var conns = codeService.GetConnNameList().FirstOrDefault();
            if (conns != null) model.ConnectionString = codeService.GetConnectionString(conns);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRepository(CodeGeneratorModel model)
        {
            if (string.IsNullOrEmpty(model.ConnStringName))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ConnStringName", "連線字串名稱不可空白!!");
                return View(model);
            }
            using var codeService = new CodeGeneratorService();
            string str_message = codeService.CheckConnectionString(model.ConnStringName);
            if (!string.IsNullOrEmpty(str_message))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("ConnStringName", str_message);
                return View(model);
            }
            CodeSessionService.ConnStringName = model.ConnStringName;
            CodeSessionService.ForceOverride = model.ForceOverride;
            CodeSessionService.TableSelectType = model.TableSelectType;
            return RedirectToAction("CreateRepositoryTables", "CodeGenerator", new { area = "" });
        }

        [HttpGet]
        public ActionResult CreateRepositoryTables()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.ConnStringName = CodeSessionService.ConnStringName;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.TableSelectType = CodeSessionService.TableSelectType;
            model.UseSQLSelect = false;
            model.SelectAll = false;
            model.TableViews = new List<string>();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRepositoryTables(CodeGeneratorModel model)
        {
            if (!model.SelectAll && model.TableSelectType == "2" && (model.TableViews == null || model.TableViews.Count() <= 0))
            {
                ModelState.ClearAllErrors();
                ModelState.AddModelError("SelectAll", "未選擇任何表格!!");
                return View(model);
            }
            if (model.UseSQLSelect)
            {
                if (string.IsNullOrEmpty(model.SQLSelectSyntax))
                {
                    ModelState.ClearAllErrors();
                    ModelState.AddModelError("SQLSelectSyntax", "未輸入 SQL 語法!!");
                    return View(model);
                }
            }
            else
            {
                model.SQLSelectSyntax = "";
            }
            using var codeService = new CodeGeneratorService();
            var cols = codeService.GetSortColumnList(model);
            CodeSessionService.TableViewName = model.TableViewName;
            CodeSessionService.UseSQLSelect = model.UseSQLSelect;
            CodeSessionService.SQLSelectSyntax = model.SQLSelectSyntax;
            CodeSessionService.SelectAll = model.SelectAll;
            if (model.SelectAll || model.TableSelectType == "2")
            {
                model.SortColumnName = "";
                model.SortDirection = "ASC";
                model.UseDropDownList = true;
                model.DropDownColumnNo = "DataNo";
                model.DropDownColumnName = "DataName";
                using var codeCreate = new CodeCreate();
                string str_message = codeCreate.CreateRepository(model);
                if (string.IsNullOrEmpty(str_message))
                {
                    using var CodeMessage = new CodeMessage();
                    str_message = CodeMessage.CreateRepositoryMessage(model, false);
                }
                CodeSessionService.MessageText = str_message;
                return RedirectToAction("Message", "CodeGenerator", new { area = "" });
            }
            return RedirectToAction("CreateRepositorySort", "CodeGenerator", new { area = "" });
        }

        [HttpGet]
        public ActionResult CreateRepositorySort()
        {
            CodeGeneratorModel model = new CodeGeneratorModel();
            model.ProjectName = CodeSessionService.ProjectName;
            model.ConnStringName = CodeSessionService.ConnStringName;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.TableSelectType = CodeSessionService.TableSelectType;
            model.TableViewName = CodeSessionService.TableViewName;
            model.UseSQLSelect = CodeSessionService.UseSQLSelect;
            model.SQLSelectSyntax = CodeSessionService.SQLSelectSyntax;
            model.SelectAll = CodeSessionService.SelectAll;
            model.UseDropDownList = true;
            model.DropDownColumnNo = "";
            model.DropDownColumnName = "";
            model.SortColumnName = "";
            model.SortDirection = "ASC";
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRepositorySort(CodeGeneratorModel model)
        {
            using var codeService = new CodeGeneratorService();
            using var codeSQLServer = new CodeSQLServer();
            model.ProjectName = CodeSessionService.ProjectName;
            model.ConnStringName = CodeSessionService.ConnStringName;
            model.ForceOverride = CodeSessionService.ForceOverride;
            model.TableSelectType = CodeSessionService.TableSelectType;
            model.TableViewName = CodeSessionService.TableViewName;
            model.UseSQLSelect = CodeSessionService.UseSQLSelect;
            model.SQLSelectSyntax = CodeSessionService.SQLSelectSyntax;
            model.SelectAll = CodeSessionService.SelectAll;
            if (!model.UseDropDownList)
            {
                model.DropDownColumnNo = "DataNo";
                model.DropDownColumnName = "DataName";
            }

            using var codeCreate = new CodeCreate();
            string str_message = codeCreate.CreateRepository(model);
            if (string.IsNullOrEmpty(str_message))
            {
                using var CodeMessage = new CodeMessage();
                str_message = CodeMessage.CreateRepositoryMessage(model, false);
            }
            CodeSessionService.MessageText = str_message;
            return RedirectToAction("Message", "CodeGenerator", new { area = "" });
        }


        [HttpGet]
        public ActionResult Message()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetConnString(string id)
        {
            using var codeService = new CodeGeneratorService();
            string str_value = codeService.GetConnectionString(id);
            return Json(str_value);
        }
    }
}
