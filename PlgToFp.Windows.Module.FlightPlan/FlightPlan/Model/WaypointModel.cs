using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model
{
    /// <summary>
    /// A bindable representation of the Waypoint data
    /// </summary>
    public class WaypointModel : BindableBase 
    {
        #region Private fields
        private Core.Waypoint _waypoint;
        #endregion

        #region Bindable properties
        private double _latitude;

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                SetProperty(ref _latitude, value);
                OnPropertyChanged(() => Latitude);
            }
        }

        private double _longitude;

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                SetProperty(ref _longitude, value);
                OnPropertyChanged(() => Longitude);
            }
        }

        private string _identifier;

        public string Identifier
        {
            get { return _identifier; }
            set
            {
                SetProperty(ref _identifier, value);
                OnPropertyChanged(() => Identifier);
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates a new waypoint model from the waypoint data object
        /// </summary>
        public static WaypointModel FromCoreWaypoint(Core.Waypoint waypoint)
        {
            var model = new WaypointModel();
            model._waypoint = waypoint;
            model._latitude = waypoint.Latitude;
            model._longitude = waypoint.Longitude;
            model._identifier = waypoint.Identifier;

            return model;
        } 
        #endregion
    }
}
