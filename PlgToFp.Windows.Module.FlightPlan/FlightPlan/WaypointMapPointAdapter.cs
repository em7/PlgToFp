using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Controls;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    /// <summary>
    /// An adapter for WaypointModel and IFligthPlanMapPoint
    /// </summary>
    public class WaypointMapPointAdapter : IFlightPlanMapPoint
    {
        private WaypointModel _waypoint;

        public double Longitude
        {
            get
            {
                if (_waypoint == null)
                    return 0d;
                return _waypoint.Longitude;
            }
        }

        public double Latitude
        {
            get
            {
                if (_waypoint == null)
                    return 0d;
                return _waypoint.Latitude;
            }
        }

        public WaypointMapPointAdapter(WaypointModel wp)
        {
            this._waypoint = wp;
        }
    }
}
