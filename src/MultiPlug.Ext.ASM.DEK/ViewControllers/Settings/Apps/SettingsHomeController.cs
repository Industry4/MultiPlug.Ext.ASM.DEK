using MultiPlug.Base.Http;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps
{
    [Base.Attribute.Route("")]
    public class SettingsHomeController : SettingsApp
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
