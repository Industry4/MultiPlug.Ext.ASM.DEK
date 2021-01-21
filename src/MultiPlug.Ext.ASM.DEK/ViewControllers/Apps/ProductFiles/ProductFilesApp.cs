using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles
{
    [Name("DEK Product Files")]
    [ViewAs(ViewAs.Partial)]
    [HttpEndpointType(HttpEndpointType.App)]
    public class ProductFilesApp : Controller
    {
    }
}
