using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

using MultiPlug.Extension.Core.Http;

using MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Settings;
using MultiPlug.Ext.ASM.DEK.Models.Apps.ProductFiles;

namespace MultiPlug.Ext.ASM.DEK
{
    class Core
    {
        public event EventHandler AppsUpdated;

        private static Core m_Instance = null;

        [DataMember]
        public Models.Apps.ProductFiles.Properties ProductFilesApp { get; set; } = new Models.Apps.ProductFiles.Properties();

        private HttpEndpoint m_ProductFilesApp = new ProductFilesApp();

        public string DefaultsFilePath { get; private set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParamDesDefaults.config");

        public Dictionary<short, ParameterDescription> ParameterDescriptions { get; set; } = new Dictionary<short, ParameterDescription>();

        public Models.Apps.ProductFiles.DEKResources.Resources DEKResources { get; private set; }


        private Core()
        {
            Apps.Add(new SettingsApp());

            if( ProductFilesApp.Enabled)
            {
                Apps.Add(m_ProductFilesApp);
            }

            try
            {
                Defaults DefaultsFile;
                XmlSerializer Serialiser = new XmlSerializer(typeof(Defaults));
                using (FileStream Stream = new FileStream(DefaultsFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    DefaultsFile = (Defaults)Serialiser.Deserialize(Stream);
                }

                DefaultsFile.Parameters.ForEach(p => ParameterDescriptions.Add(p.Id, p));
            }
            catch (Exception)
            {
            }

            LoadDEKResources();
        }

        internal void SetDEKResourcesFolderPath(string resourcesFolderPath)
        {
            if( resourcesFolderPath != ProductFilesApp.DEKResourcesFolderPath)
            {
                ProductFilesApp.DEKResourcesFolderPath = resourcesFolderPath;

                LoadDEKResources();
            }
        }

        // Should this only be called within the Settings Page? and not every time on startup...
        public void LoadDEKResources()
        {
            try
            {
                XmlSerializer Serialiser = new XmlSerializer(typeof(Models.Apps.ProductFiles.DEKResources.Resources));
                using (FileStream Stream = new FileStream(Path.Combine(ProductFilesApp.DEKResourcesFolderPath, "en.xml"), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    DEKResources = (Models.Apps.ProductFiles.DEKResources.Resources)Serialiser.Deserialize(Stream);
                }
            }
            catch (Exception)
            {
                DEKResources = new Models.Apps.ProductFiles.DEKResources.Resources();
                DEKResources.TextResources = new Models.Apps.ProductFiles.DEKResources.TextResources();
                DEKResources.TextResources.Text = new List<Models.Apps.ProductFiles.DEKResources.Textb>();
                DEKResources.TextResources.DateTime = string.Empty;
                DEKResources.TextResources.EnglishName = string.Empty;
                DEKResources.TextResources.Language = string.Empty;
                DEKResources.TextResources.NativeName = string.Empty;
                DEKResources.TextResources.Version = string.Empty;
            }

            DEKResources.TextResources.Text = DEKResources.TextResources.Text.Where(t =>  t.ID.StartsWith("Param") && t.ID.EndsWith("LabelTxt") ).ToList();


            var DEKResourcesDictionary = new Dictionary<int, string>();

            DEKResources.TextResources.Text.ForEach(t =>
           {
               string Id = t.ID.Replace("Param_", "");
               Id = Id.Replace("_LabelTxt", "");

                int Parsed = 0;

                if( Int32.TryParse(Id, out Parsed) )
                {
                    DEKResourcesDictionary.Add(Parsed, t.Text);
                }
            });

            bool NameApplied = false;

            foreach (KeyValuePair<short, ParameterDescription> entry in ParameterDescriptions)
            {
                if( string.IsNullOrEmpty( entry.Value.Name))
                {
                    string Name;       

                    if ( DEKResourcesDictionary.TryGetValue(entry.Key, out Name) )
                    {
                        entry.Value.Name = Name;
                        NameApplied = true;
                    }
                }
            }
            if( NameApplied )
            {
                SaveDefaultsFile();
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

        public void SaveDefaultsFile()
        {
            var DefaultsFile = new Defaults();
            DefaultsFile.Parameters = ParameterDescriptions.Values.ToList();

            XmlSerializer Serialiser = new XmlSerializer(typeof(Defaults));

            using (Stream stream = new FileStream(Core.Instance.DefaultsFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Serialiser.Serialize(stream, DefaultsFile);
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

        public List<HttpEndpoint> Apps { get; set; } = new List<HttpEndpoint>();
    }
}
