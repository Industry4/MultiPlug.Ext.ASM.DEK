using System.Collections.Generic;
using MultiPlug.Base.Http;
using MultiPlug.Ext.ASM.DEK.Properties;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps
{
    class SettingsHomeController : Controller
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Apps.Settings.Home(),
                ModelType = typeof(Models.Apps.ProductFiles.ProductFiles),
                Template = new KeyValuePair<string, string>("GetSettingsHome", Resources.SettingsHome)
            };
        }
    }
}
