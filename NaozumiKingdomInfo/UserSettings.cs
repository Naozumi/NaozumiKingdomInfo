using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace zNaozumiKingdomInfo
{
    class UserSettings
    {
        public InputKey MenuKey { get; set; } = InputKey.End;
        public int MenuKeyInt = 207;
        public bool Loaded = false;

        public UserSettings()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                DirectoryInfo di = Directory.GetParent(Directory.GetCurrentDirectory().ToString());

                doc.Load(Path.Combine(BasePath.Name, "Modules", "zNaozumiKingdomInfo", "ModuleData", "Settings.xml"));

                XmlNodeList nodes = doc.SelectNodes("/Settings/MenuKey[position() = 1]");
                foreach (XmlNode xn in nodes)
                {
                    MenuKeyInt = Int32.Parse(xn.InnerText);
                    MenuKey = (InputKey)Enum.ToObject(typeof(InputKey), MenuKeyInt);
                    Loaded = true;
                    break;
                }
            }
            catch
            {
                //do nothing
            }
        }
    }
}
