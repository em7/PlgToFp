using PlgToFp.Windows.Infrastructure;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Service
{
    /// <summary>
    /// A service which handles UI navigation within this module
    /// </summary>
    public class NavigationService : INavigationService
    {
        private IRegionManager _regionMgr;

        public NavigationService(IRegionManager regionMgr)
        {
            _regionMgr = regionMgr;
        }

        public void EditWaypoint(WaypointModel waypoint)
        {
            var pars = new NavigationParameters();
            pars.Add(EditWaypointViewModel.NAV_PARAM_WAYPOINT, waypoint);
            _regionMgr.RequestNavigate(Regions.Main, "EditWaypointView", pars);
        }
    }
}
