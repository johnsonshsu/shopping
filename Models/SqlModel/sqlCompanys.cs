using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlCompanys : DapperSql<Companys>
    {
        public z_sqlCompanys()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Companys.CompNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Companys.Id, Companys.IsDefault, Companys.IsEnabled, Companys.CodeNo, vi_CodeCompany.CodeName, 
Companys.CompNo, Companys.CompName, Companys.ShortName, Companys.EngName, Companys.EngShortName,
Companys.RegisterDate, Companys.BossName, Companys.ContactName, Companys.CompTel, 
Companys.ContactTel, Companys.CompFax, Companys.CompID, Companys.ContactEmail, Companys.CompAddress, 
Companys.CompUrl, Companys.TwitterUrl, Companys.FacebookUrl, Companys.InstagramUrl, Companys.SkypeUrl, 
Companys.LinkedinUrl, Companys.Latitude, Companys.Longitude, Companys.AboutusText, Companys.SupportText, 
Companys.ReturnText, Companys.ShippingText, Companys.PaymentText, Companys.Remark
FROM Companys 
LEFT OUTER JOIN vi_CodeCompany ON Companys.CodeNo = vi_CodeCompany.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeCompany.CodeName");
            return searchColumn;
        }

        public Companys GetDefaultCompany()
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Companys.IsDefault = @IsDefault AND Companys.IsEnabled = @IsEnabled";
            var param = new DynamicParameters();
            param.Add("@IsDefault", true, DbType.Boolean);
            param.Add("@IsEnabled", true, DbType.Boolean);
            var model = dpr.ReadSingle<Companys>(sql_query, param);
            return model;
        }
    }
}