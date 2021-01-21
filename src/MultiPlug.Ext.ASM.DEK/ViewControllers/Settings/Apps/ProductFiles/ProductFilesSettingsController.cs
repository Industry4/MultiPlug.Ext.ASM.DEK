using System;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps.ProductFiles
{
    [Route("productfiles")]
    public class ProductFilesSettingsController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Apps.Settings.ProductFiles
                {
                    ProductFolderPath = Core.Instance.ProductFilesApp.ProductFilesPath,
                    ResourcesFolderPath = Core.Instance.ProductFilesApp.DEKResourcesFolderPath,
                    AppEnabled = Core.Instance.ProductFilesApp.Enabled? "checked": "" // HTML value of bool
                },
                Template = "GetProductFilesSettings"
            };
        }

        public Response Post(Models.Apps.Settings.ProductFilesPost theModel)
        {
            Core.Instance.ProductFilesApp.ProductFilesPath = theModel.ProductFolderPath;

            Core.Instance.SetDEKResourcesFolderPath(theModel.ResourcesFolderPath);

            Core.Instance.EnableApp(!string.IsNullOrEmpty(theModel.AppEnabled));

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = new Uri(Context.Referrer.ToString())
            };
        }
    }
}
