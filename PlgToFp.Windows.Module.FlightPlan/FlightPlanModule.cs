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

using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Service;

namespace PlgToFp.Windows.Module.FlightPlan
{
    public class FlightPlanModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        //private IFlightPlanIoService _flightPlanIoService;

        public FlightPlanModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterInstance(typeof(IFlightPlanIoService), _container.Resolve<FlightPlanIoService>());
            _container.RegisterInstance(typeof(INavigationService), _container.Resolve<NavigationService>());

            _container.RegisterType<object, FlightPlanView>("FlightPlanView");
            _container.RegisterType<object, EditWaypointView>("EditWaypointView");

            _regionManager.RegisterViewWithRegion(Regions.Toolbar, typeof(FlightPlanToolbar.FlightPlanToolbarView));
            //_regionManager.RegisterViewWithRegion(Regions.Main, typeof(FlightPlan.FlightPlanView));

            _regionManager.RequestNavigate(Regions.Main, "FlightPlanView");
        }
    }
}
