using Microsoft.Practices.Unity;
using PlgToFp.Windows.Infrastructure;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan
{
    public class FlightPlanModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public FlightPlanModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType(typeof(IFlightPlanIoService), typeof(FlightPlanIoService));

            _regionManager.RegisterViewWithRegion(Regions.Toolbar, typeof(FlightPlanToolbar.FlightPlanToolbarView));
            _regionManager.RegisterViewWithRegion(Regions.Main, typeof(FlightPlan.FlightPlanView));
        }
    }
}
