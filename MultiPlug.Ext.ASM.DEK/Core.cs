using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Settings;
using MultiPlug.Extension.Core.Views;

namespace MultiPlug.Ext.ASM.DEK
{
    class Core
    {
        public event EventHandler AppsUpdated;

        private static Core m_Instance = null;

        [DataMember]
        public Models.Apps.ProductFiles.Properties ProductFilesApp { get; set; } = new Models.Apps.ProductFiles.Properties();

        private ViewBase m_ProductFilesApp = new ProductFilesApp();

        private Core()
        {
            Apps.Add(new SettingsApp());

            if( ProductFilesApp.Enabled)
            {
                Apps.Add(m_ProductFilesApp);
            }
        }

        public static Core Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Core();
                }
                return m_Instance;
            }
        }

        public void EnableApp(bool isEnabled)
        {
            if( isEnabled && !ProductFilesApp.Enabled)
            {
                Apps.Add(m_ProductFilesApp);
                AppsUpdated(this, EventArgs.Empty);
            }
            else if( !isEnabled && ProductFilesApp.Enabled)
            {
                Apps.Remove(m_ProductFilesApp);
                AppsUpdated(this, EventArgs.Empty);
            }

            ProductFilesApp.Enabled = isEnabled;
        }

        public List<ViewBase> Apps { get; set; } = new List<ViewBase>();
    }
}
