using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public static class CodeSessionService
    {
        public static string ProjectName
        {
            get
            {
                string value = Assembly.GetCallingAssembly().GetName().Name;
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeProjectName");
                if (string.IsNullOrEmpty(value)) value = Assembly.GetCallingAssembly().GetName().Name;
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeProjectName", value); }
        }
        public static string DataContext
        {
            get
            {
                string value = "dbEntities";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeDataContext");
                if (string.IsNullOrEmpty(value)) value = "dbEntities";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeDataContext", value); }
        }
        public static string LabelScaleSizeName
        {
            get
            {
                string value = "2 : 10";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeLabelScaleSizeName");
                if (string.IsNullOrEmpty(value)) value = "2 : 10";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeLabelScaleSizeName", value); }
        }
        public static int LabelScaleSize
        {
            get
            {
                int value = 2;
                if (SessionService._context != null)
                {
                    string str_value = SessionService._context.Session.Get<string>("CodeLabelScaleSize");
                    if (!int.TryParse(str_value, out value)) value = 2;
                }
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeLabelScaleSize", value.ToString()); }
        }
        public static int LayoutColCount
        {
            get
            {
                int value = 1;
                if (SessionService._context != null)
                {
                    string str_value = SessionService._context.Session.Get<string>("CodeLayoutColCount");
                    if (!int.TryParse(str_value, out value)) value = 1;
                }
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeLayoutColCount", value.ToString()); }
        }
        public static string LayoutName
        {
            get
            {
                string value = "_Layout";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeLayoutName");
                if (string.IsNullOrEmpty(value)) value = "_Layout";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeLayoutName", value); }
        }
        public static bool Force
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeForce");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeForce", str_value);
            }
        }
        public static bool UseAsyncActions
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeUseAsyncActions");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeUseAsyncActions", str_value);
            }
        }
        public static bool PartialView
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodePartialView");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodePartialView", str_value);
            }
        }
        public static bool UseSQLSelect
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeUseSQLSelect");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeUseSQLSelect", str_value);
            }
        }
        public static string OutDir
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeOutDir");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeOutDir", value); }
        }
        public static string MessageText
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeMessageText");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeMessageText", value); }
        }
        public static string TableViewsList
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeTableViewsList");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeTableViewsList", value); }
        }

        public static string GeneratorType
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeGeneratorType");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeGeneratorType", value); }
        }
        public static string TemplateName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeTemplateName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeTemplateName", value); }
        }
        public static string AreaName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeAreaName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeAreaName", value); }
        }
        public static string ControllerName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeControllerName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeControllerName", value); }
        }
        public static string ViewName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeViewName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeViewName", value); }
        }
        public static string ModelName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeModelName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeModelName", value); }
        }
        public static string ModelNameSpace
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeModelNameSpace");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeModelNameSpace", value); }
        }
        public static string ModelClassName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeModelClassName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeModelClassName", value); }
        }
        public static string TableViewName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeTableViewName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeTableViewName", value); }
        }
        public static string TableViews
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeTableViews");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeTableViews", value); }
        }
        public static string WebAPIName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeWebAPIName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeWebAPIName", value); }
        }
        public static string DeleteNo
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeDeleteNo");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeDeleteNo", value); }
        }
        public static string DeleteName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeDeleteName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeDeleteName", value); }
        }
        public static string KeyName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeKeyName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeKeyName", value); }
        }
        public static string CardSize
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeCardSize");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeCardSize", value); }
        }
        public static bool ForceOverride
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeForceOverride");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeForceOverride", str_value);
            }
        }
        public static bool UseModel
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeUseModel");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeUseModel", str_value);
            }
        }
        public static bool SelectAll
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeSelectAll");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeSelectAll", str_value);
            }
        }
        public static bool UseCard
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeUseCard");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeUseCard", str_value);
            }
        }
        public static bool BaseController
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeBaseController");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeBaseController", str_value);
            }
        }
        public static string ConnStringName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeConnStringName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeConnStringName", value); }
        }
        public static string DbContextName
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeDbContextName");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeDbContextName", value); }
        }
        public static string DBContextType
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeDBContextType");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeDBContextType", value); }
        }
        public static string TableSelectType
        {
            get
            {
                string value = "1";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeTableSelectType");
                if (string.IsNullOrEmpty(value)) value = "1";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeTableSelectType", value); }
        }
        public static string SQLSelectSyntax
        {
            get
            {
                string value = "";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeSQLSelectSyntax");
                if (string.IsNullOrEmpty(value)) value = "";
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeSQLSelectSyntax", value); }
        }
        public static bool CreateView
        {
            get
            {
                string value = "no";
                if (SessionService._context != null) value = SessionService._context.Session.Get<string>("CodeCreateView");
                if (string.IsNullOrEmpty(value)) value = "no";
                return (value == "yes");
            }
            set
            {
                string str_value = (value) ? "yes" : "no";
                SessionService._context?.Session.Set<string>("CodeCreateView", str_value);
            }
        }
        public static int Affecteds
        {
            get
            {
                int value = 0;
                if (SessionService._context != null)
                {
                    string str_value = SessionService._context.Session.Get<string>("CodeAffecteds");
                    if (!int.TryParse(str_value, out value)) value = 0;
                }
                return value;
            }
            set
            { SessionService._context?.Session.Set<string>("CodeAffecteds", value.ToString()); }
        }
    }
}