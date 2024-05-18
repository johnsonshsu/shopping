using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateArea(CodeGeneratorModel model)
        {
            string str_message = "";
            string path = Directory.GetCurrentDirectory();
            string pathAreaName = Path.Combine(path, "Areas");
            if (!Directory.Exists(path)) Directory.CreateDirectory(pathAreaName);
            pathAreaName = Path.Combine(pathAreaName, model.AreaName);
            if (Directory.Exists(pathAreaName))
            {
                str_message = $"區域名稱:{model.AreaName} 已建立，無法再重新建立!!";
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(pathAreaName);
                    List<string> folders = new List<string>() { "Controllers", "Data", "Models", "Views" };
                    foreach (var item in folders)
                    {
                        string folderName = Path.Combine(pathAreaName, item);
                        if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
                    }
                    CodeSessionService.Affecteds = 1;
                }
                catch (Exception ex)
                {
                    str_message = ex.Message;
                }
            }
            return str_message;
        }
    }
}