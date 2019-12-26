using System.Collections.Generic;

using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Http;
using MultiPlug.Extension.Core.Attribute;

using MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles.ProductFile;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles
{
    [Name("DEK Product Files")]
    [ViewAs(ViewAs.Partial)]
    [HttpEndpointType(HttpEndpointType.App)]
    class ProductFilesApp : HttpEndpoint
    {
        readonly Controller[] m_Controllers = new Controller[] { new ProductFilesController(), new ProductFileController() };

        public override IEnumerable<Controller> Controllers
        {
            get
            {
                return m_Controllers;
            }
        }
    }
}
