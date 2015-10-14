using Microsoft.Practices.Unity;
using PlgToFp.Windows.Infrastructure;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlgToFp.Windows.Module.App
{
    public class AppModule : IModule
    {
        private IRegionManager _regionManager;

        public AppModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Regions.Toolbar, typeof(AppToolbar.AppToolbarView));
        }
    }
}
