
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;

using MultiPlug.Ext.ASM.DEK.Models.Load;

namespace MultiPlug.Ext.ASM.DEK
{
    public class DEK : MultiPlugExtension
    {
        public DEK()
        {
        }

        public override RazorTemplate[] RazorTemplates
        {
            get
            {
                return new RazorTemplate[]
                {
                    new RazorTemplate("GetSettingsHome", Properties.Resources.SettingsHome),
                    new RazorTemplate("GetProductFile", Properties.Resources.ProductFile),
                    new RazorTemplate("GetProductFiles", Properties.Resources.ProductFiles),
                    new RazorTemplate("GetProductFilesSettings", Properties.Resources.ProductFilesSettings)
                };
            }
        }

        public void Load(Root theLoadModel)
        {
            if (theLoadModel.ProductFilesApp != null)
            {
                Core.Instance.EnableApp(theLoadModel.ProductFilesApp.Enabled);

                if(Core.Instance.ProductFilesApp.DEKResourcesFolderPath != null)
                {
                    Core.Instance.SetDEKResourcesFolderPath(theLoadModel.ProductFilesApp.DEKResourcesFolderPath);
                }

                if( Core.Instance.ProductFilesApp.ProductFilesPath != null)
                {
                    Core.Instance.ProductFilesApp.ProductFilesPath = theLoadModel.ProductFilesApp.ProductFilesPath;
                }
            }
        }

        public override object Save()
        {
            return Core.Instance;
        }
    }
}
