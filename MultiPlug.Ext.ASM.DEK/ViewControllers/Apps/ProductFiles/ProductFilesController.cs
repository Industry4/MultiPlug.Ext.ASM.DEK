using System.IO;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Http;
using MultiPlug.Ext.ASM.DEK.Properties;
using MultiPlug.Base.Attribute;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles
{
    [Route("")]
    class ProductFilesController : Controller
    {
        public Response Get()
        {
            string[] ProductFiles = new string[0];

            try
            {
                ProductFiles = Directory.GetFiles(Core.Instance.ProductFilesApp.ProductFilesPath, "*.pr1*");
            }
            catch
            {
                // Bad Path. TODO Error Message
            }

            return new Response
            {
                Model = new Models.Apps.ProductFiles.ProductFiles { Files = new List<string>(ProductFiles.Select(f => Path.GetFileName(f))) },
                ModelType = typeof(Models.Apps.ProductFiles.ProductFiles),
                Template = new KeyValuePair<string, string>("GetProductFiles", Resources.ProductFiles)
            };
        }
    }
}
