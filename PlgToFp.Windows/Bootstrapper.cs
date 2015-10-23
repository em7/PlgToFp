using Microsoft.Practices.Unity;
using PlgToFp.Windows.Infrastructure;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlgToFp.Windows
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            //return new Shell();
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(Module.App.AppModule));
            moduleCatalog.AddModule(typeof(Module.FlightPlan.FlightPlanModule));
            
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //RegisterTypeIfMissing(typeof(IDialogService), typeof(DialogService), true);
        }
    }
}
