using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace shopping.Controllers
{
    public class CategoryController : Controller
    {
        [Route("Category/Index/{id?}/{page?}/{pageSize?}")]
        public IActionResult Index(string id = "All", int page = 1, int pageSize = 10)
        {
            using var product = new z_sqlProducts();
            using var category = new z_sqlCategorys();
            var cateModel = category.GetData(id);
            var model = product.GetCategoryDataList(id).ToPagedList(page, pageSize);
            SessionService.SetProgramInfo("", "商品分類");
            ActionService.SetActionName(enAction.List);
            SessionService.SetPageInfo(page, model.PageCount);
            SessionService.SearchText = "";
            SessionService.StringValue1 = id;
            if (id == "All" || category == null)
            {
                SessionService.StringValue2 = "all";
                SessionService.StringValue3 = "全站商品";
            }
            else if (string.IsNullOrEmpty(cateModel.ParentNo))
            {
                SessionService.StringValue2 = cateModel.CategoryNo.ToLower();
                SessionService.StringValue3 = cateModel.CategoryName;
            }
            else
            {
                SessionService.StringValue2 = cateModel.ParentNo.ToLower();
                SessionService.StringValue3 = cateModel.CategoryName;
                var cateModel1 = category.GetData(cateModel.ParentNo);
                SessionService.StringValue3 = cateModel1.CategoryName + " > " + SessionService.StringValue3;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Sort(string id = "Product")
        {
            SessionService.SortNo = id;
            return RedirectToAction("Index", new { id = SessionService.StringValue1 });
        }
    }
}