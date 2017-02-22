using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown_Hexo
{
    class ConfigHelper
    {
        public static void setData(string key,string value)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings[key].Value = value;
            cfa.Save();
        }
        public static string getData(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
