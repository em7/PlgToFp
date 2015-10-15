using PlgToFp.Windows.Infrastructure;
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

        public FlightPlanModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Regions.Toolbar, typeof(FlightPlanToolbar.FlightPlanToolbarView));
        }
    }
}
