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

using PlgToFp.Windows.Infrastructure.Interaction;
using PlgToFp.Windows.Module.App.Interaction;

namespace PlgToFp.Windows.Module.App
{
    public class AppModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public AppModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Regions.RightWindowCommands, typeof(AppToolbar.AppRightMenuView));

            _container.RegisterType(typeof(IInteractionService), typeof(InteractionService));
            _container.RegisterInstance<IInteractionService>(_container.Resolve<IInteractionService>());
        }
    }
}
