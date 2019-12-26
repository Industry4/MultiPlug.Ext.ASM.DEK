using MultiPlug.Base.Http;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps
{
    class SettingsHomeController : Controller
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Apps.Settings.Home(),
                Template = "GetSettingsHome"
            };
        }
    }
}
