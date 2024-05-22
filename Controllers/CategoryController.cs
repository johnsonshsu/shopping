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
        [Route("Category/Index/{category?}/{page?}/{pageSize?}")]
        public IActionResult Index(string category = "All", int page = 1, int pageSize = 10)
        {
            using var product = new z_sqlProducts();
            var model = product.GetCategoryDataList(category).ToPagedList(page, pageSize);
            SessionService.SetProgramInfo("", "商品分類");
            ActionService.SetActionName(enAction.List);
            SessionService.SetPageInfo(page, model.PageCount);
            SessionService.SearchText = "";
            SessionService.StringValue1 = category;
            using var db = new dbEntities();
            var products = db.Products
                .OrderBy(m => m.SalePrice)
                .ThenByDescending(m => m.InventoryQty)
                .ToList();

            return View(model);
        }
    }
}