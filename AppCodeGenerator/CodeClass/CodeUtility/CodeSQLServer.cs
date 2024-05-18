using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeGenerator
{
    public class CodeSQLServer : CodeBaseClass
    {
        public List<string> GetConnNameList()
        {
            List<string> values = new List<string>();
            bool bln_start = false;
            string path = Directory.GetCurrentDirectory();
            string file = Path.Combine(path, "appsettings.json");
            List<string> datas = File.ReadAllText(file).Split("\r\n").ToList();
            foreach (var item in datas)
            {
                string data = item.Replace("\"", "");
                if (data.Contains("ConnectionStrings"))
                {
                    bln_start = true;
                    continue;
                }
                if (bln_start)
                {
                    if (data.Contains("}")) break;
                    if (data.Contains(":"))
                    {
                        string connData = data.Split(":").ToList()[0].Trim();
                        values.Add(connData);
                    }
                }
            }
            return values;
        }
        public string GetConnectionString(string connectionName)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString(connectionName) ?? "";
            return connectionString;
        }
    }
}