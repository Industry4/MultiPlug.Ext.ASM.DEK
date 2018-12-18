using System;
using System.Collections.Generic;
using MultiPlug.Extension.Core;
using MultiPlug.Base.Exchange;
using MultiPlug.Extension.Core.Views;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Apps.ProductFiles;
using MultiPlug.Ext.ASM.DEK.ViewControllers.Settings;
using MultiPlug.Ext.ASM.DEK.Models.Load;

namespace MultiPlug.Ext.ASM.DEK
{
    public class DEK : ExtensionBase
    {
        public DEK()
        {
            Core.Instance.AppsUpdated += Instance_AppsUpdated;
        }

        private void Instance_AppsUpdated(object sender, EventArgs e)
        {
            AppsUpdated(this, Apps);
        }

        public override MultiPlugType<ViewBase[]> Apps
        {
            get
            {
                return new MultiPlugType<ViewBase[]> { Value = Core.Instance.Apps.ToArray() };
            }
        }

        public override List<Event> Events
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override List<Subscription> Subscriptions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override event EventHandler<MultiPlugType<ViewBase[]>> AppsUpdated;
        public override event EventHandler<List<Event>> EventsUpdated;
        public override event EventHandler<List<Subscription>> SubscriptionsUpdated;

        public override void Initialise()
        {
            throw new NotImplementedException();
        }

        public override void Load(KeyValuesJson[] config)
        {
            throw new NotImplementedException();
        }

        public void Load(Root theLoadModel)
        {
            if(theLoadModel.ProductFilesApp != null)
            {
                Core.Instance.EnableApp(theLoadModel.ProductFilesApp.Enabled);

                Core.Instance.ProductFilesApp = theLoadModel.ProductFilesApp;
            }
        }

        public override void OnUnhandledException(UnhandledExceptionEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override object Save()
        {
            return Core.Instance;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
