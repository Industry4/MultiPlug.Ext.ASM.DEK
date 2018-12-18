﻿using System;
using System.Collections.Generic;
using MultiPlug.Extension.Core.Views;
using MultiPlug.Base.Http;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps.ProductFiles;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Settings.Apps;

namespace MultiPlug.Ext.ASM.DEK.ViewControllers.Settings
{
    class SettingsApp : ViewBase
    {
        readonly Guid m_Id = Guid.NewGuid();

        readonly Controller[] m_Controllers = new Controller[] { new SettingsHomeController(), new ProductFilesSettingsController() };
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
                return "ASM DEK";
            }
        }

        public override ViewType Type
        {
            get
            {
                return ViewType.Settings;
            }
        }
    }
}
