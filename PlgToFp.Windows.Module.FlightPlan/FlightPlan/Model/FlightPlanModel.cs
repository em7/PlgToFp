using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlgToFp.Core;
using System.Collections.ObjectModel;
using PlgToFp.Windows.Infrastructure.Helpers;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model
{
    /// <summary>
    /// A bindable representation of the FlightPlan data
    /// </summary>
    public class FlightPlanModel : BindableBase
    {
        #region Private fields
        private Core.FlightPlan _flightPlan;
        #endregion

        #region Bindable properties
        private string _origin;

        public string Origin
        {
            get { return _origin; }
            set
            {
                SetProperty(ref _origin, value);
                OnPropertyChanged(() => Origin);
            }
        }

        private string _destination;

        public string Destination
        {
            get { return _destination; }
            set
            {
                SetProperty(ref _destination, value);
                OnPropertyChanged(() => Destination);
            }
        }

        private ObservableCollection<WaypointModel> _waypoints;

        public ObservableCollection<WaypointModel> Waypoints
        {
            get { return _waypoints; }
            set
            {
                SetProperty(ref _waypoints, value);
                OnPropertyChanged(() => Waypoints);
            }
        }

        #endregion

        #region ctor
        /// <summary>
        /// Creates an empty flight plan
        /// </summary>
        public FlightPlanModel()
        {
            if (DesignHelper.IsInDesignMode())
            {
                Waypoints = new ObservableCollection<WaypointModel>(new List<WaypointModel>
                {
                    new WaypointModel() { Identifier = "ID1", Latitude = 6.66, Longitude = 12.89 },
                    new WaypointModel() { Identifier = "ID2", Latitude = -6.66, Longitude = 12.89 },
                    new WaypointModel() { Identifier = "ID3", Latitude = 6.66, Longitude = -12.89 },
                    new WaypointModel() { Identifier = "ID4", Latitude = -6.66, Longitude = -12.89 },
                });
            }
        }
        #endregion

        #region Public methods
        public static FlightPlanModel FromCoreFlightPlan(Core.FlightPlan flightPlan)
        {
            var model = new FlightPlanModel();
            model._flightPlan = flightPlan;
            model._origin = flightPlan.Origin;
            model._destination = flightPlan.Destination;
            
            if (flightPlan.Waipoints != null)
            {
                var waypoints = flightPlan.Waipoints.Select(wp => WaypointModel.FromCoreWaypoint(wp));
                model._waypoints = new ObservableCollection<WaypointModel>(waypoints);
            }

            return model;
        }
        #endregion
    }
}
