using System;
using System.Collections.Generic;
using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Views;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles.ProductFile;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles
{
    class ProductFilesApp : ViewBase
    {
        readonly Guid m_Id;

        readonly Controller[] m_Controllers = new Controller[] { new ProductFilesController(), new ProductFileController() };
        public ProductFilesApp()
        {
            m_Id = Guid.NewGuid();
        }

        public override IEnumerable<Controller> Controllers
        {
            get
            {
                return m_Controllers;
            }
        }

        public override Guid Id
        {
            get
            {
                return m_Id;
            }
        }

        public override bool isPartial
        {
            get
            {
                return true;
            }
        }

        public override string Name
        {
            get
            {
                return "DEK Product Files";
            }
        }

        public override ViewType Type
        {
            get
            {
                return ViewType.App;
            }
        }
    }
}
