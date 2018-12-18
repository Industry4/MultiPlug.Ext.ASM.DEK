using System;
using System.Collections.Generic;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.ASM.DEK.Properties;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps.ProductFiles
{
    [Route("productfiles")]
    class ProductFilesSettingsController : Controller
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Apps.Settings.ProductFiles
                {
                    FolderPath = Core.Instance.ProductFilesApp.ProductFilesPath,
                    AppEnabled = Core.Instance.ProductFilesApp.Enabled? "checked": "" // HTML value of bool
                },
                ModelType = typeof(Models.Apps.ProductFiles.ProductFiles),
                Template = new KeyValuePair<string, string>("GetProductFilesSettings", Resources.ProductFilesSettings)
            };
        }

        public Response Post(Models.Apps.Settings.ProductFilesPost theModel)
        {
            Core.Instance.ProductFilesApp.ProductFilesPath = theModel.FolderPath;

            Core.Instance.EnableApp(!string.IsNullOrEmpty(theModel.AppEnabled));

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = new Uri(Context.Referrer.ToString())
            };
        }
    }
}
