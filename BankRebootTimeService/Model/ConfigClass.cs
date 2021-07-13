using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankRebootTimeService.Model
{
    [Serializable]
    public class ConfigClass
    {
        [NonSerialized]
        private static ConfigClass instance;
        public bool IsTimerGo { get; set; }
        private ConfigClass()
        { }

        public static ConfigClass getInstance()
        {
            if (instance == null)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ConfigClass));
                try
                {
                    using (FileStream fs = new FileStream(@"Config\AppConfig.xml", FileMode.OpenOrCreate))
                    {
                        instance = (ConfigClass)formatter.Deserialize(fs);
                    }
                }
                catch
                {
                    using (FileStream fs = new FileStream(@"Config\AppConfig.xml", FileMode.Create))
                    {
                        instance = new ConfigClass() { IsTimerGo = false };
                        formatter.Serialize(fs, instance);
                    }
                }
            }
            return instance;
        }
        public static void SetTimerStatus(bool status)
        {
            if (instance != null)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ConfigClass));
                using (FileStream fs = new FileStream(@"Config\AppConfig.xml", FileMode.Create))
                {
                    instance = new ConfigClass() { IsTimerGo = status };
                    formatter.Serialize(fs, instance);
                }
            }
        }
        public static ConfigClass ReturnTimeConfig()
        {
            return instance;
        }
    }
}

