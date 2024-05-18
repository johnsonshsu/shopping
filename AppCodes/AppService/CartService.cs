using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class CartService
{
    /// <summary>
    /// HttpContextAccessor 物件
    /// </summary>
    /// <returns></returns>
    public static IHttpContextAccessor _contextAccessor { get; set; } = new HttpContextAccessor();
    /// <summary>
    /// HttpContext 物件
    /// </summary>
    public static HttpContext? _context { get { return _contextAccessor.HttpContext; } }

    #region 公開屬性
    /// <summary>
    /// 訂單編號
    /// </summary>
    public static string OrderNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("OrderNo");
            if (str_value == null) str_value = "";
            return str_value;
        }
        set
        { _context?.Session.Set<string>("OrderNo", value); }
    }

    /// <summary>
    /// 購物批號 LotNo
    /// </summary>
    public static string LotNo
    {
        get
        {
            string str_value = "";
            if (_context != null) str_value = _context.Session.Get<string>("LotNo");
            if (string.IsNullOrEmpty(str_value)) str_value = NewLotNo();
            return str_value;
        }
        set
        { _context?.Session.Set<string>("LotNo", value); }
    }
    /// <summary>
    /// 購物批號建立時間
    /// </summary>
    public static string LotCreateTime
    {
        get
        {
            string str_value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (_context != null) str_value = _context.Session.Get<string>("LotCreateTime");
            if (str_value == null) str_value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return str_value;
        }
        set
        { _context?.Session.Set<string>("LotCreateTime", value); }
    }
    #endregion
    #region 公用函數
    /// <summary>
    /// 更新購物批號
    /// </summary>
    /// <returns></returns>
    public static string NewLotNo()
    {
        LotNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15).ToUpper();
        LotCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        return LotNo;
    }
    #endregion
    #region 公用事件
    /// <summary>
    /// 登入時將現有遊客的購物車加入客戶的購物車
    /// </summary>
    public static void MergeCart()
    {
        if (!string.IsNullOrEmpty(LotNo))
        {
            using var carts = new z_sqlCarts();
            carts.MergeCart();
        }
    }
    /// <summary>
    /// 加入購物車
    /// </summary>
    /// <param name="productNo">商品編號</param>
    public static void AddCart(string productNo)
    {
        AddCart(productNo, "", 1);
    }

    /// <summary>
    /// 加入購物車
    /// </summary>
    /// <param name="productNo">商品編號</param>
    /// <param name="buyQty">數量</param>
    public static void AddCart(string productNo, int buyQty)
    {
        AddCart(productNo, "", buyQty);
    }

    /// <summary>
    /// 加入購物車
    /// </summary>
    /// <param name="productNo">商品編號</param>
    /// <param name="prod_Spec">商品規格</param>
    /// <param name="buyQty">數量</param>
    public static void AddCart(string productNo, string prod_Spec, int buyQty)
    {
        using var carts = new z_sqlCarts();
        carts.AddCart(productNo, prod_Spec, buyQty);
    }

    /// <summary>
    /// 更新購物車
    /// </summary>
    /// <param name="id">row ID</param>
    /// <param name="qty">數量</param>
    public static void UpdateCart(int id, int qty)
    {
        using var carts = new z_sqlCarts();
        carts.UpdateCart(id, qty);
    }

    /// <summary>
    /// 刪除購物車
    /// </summary>
    /// <param name="id">row ID</param>
    public static void DeleteCart(int id)
    {
        using var carts = new z_sqlCarts();
        carts.DeleteCart(id);
    }

    /// <summary>
    /// 消費者付款
    /// </summary>
    public static string CartPayment(vmOrders model)
    {
        using var order = new z_sqlOrders();
        using var detail = new z_sqlOrderDetails();
        using var cart = new z_sqlCarts();
        //建立訂單表頭
        OrderNo = order.CreateNewOrder(model);
        //建立訂明細
        detail.CreateNewOrderDetail(OrderNo);
        //刪除購物車
        cart.DeleteCart();
        //建新新購物批號
        NewLotNo();
        return OrderNo;
    }
    #endregion
    #region 私有函數
    #endregion
}