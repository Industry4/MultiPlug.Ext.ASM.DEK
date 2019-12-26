using System.Collections.Generic;

using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Http;
using MultiPlug.Extension.Core.Attribute;

using MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps.ProductFiles;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Settings
{
    [ViewAs(ViewAs.Partial)]
    [HttpEndpointType(HttpEndpointType.Settings)]
    class SettingsApp : HttpEndpoint
    {
        readonly Controller[] m_Controllers = new Controller[] { new SettingsHomeController(), new ProductFilesSettingsController() };
        public override IEnumerable<Controller> Controllers
        {
            get
            {
                return m_Controllers;
            }
        }
    }
}
