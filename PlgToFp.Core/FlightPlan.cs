using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlpToFp.Core
{
    /// <summary>
    /// An internal representation of a flight plan
    /// </summary>
    class FlightPlan
    {

        private string _origin;
        private string _dest;
        private List<Waypoint> _waypoints;

        /// <summary>
        /// Constructs an empty flightplan
        /// </summary>
        public FlightPlan() : this(null, null, new List<Waypoint>())
        {

        }

        /// <summary>
        /// Constructs a complete flight plan
        /// </summary>
        /// <param name="origin">ICAO of origin airport</param>
        /// <param name="destination">ICAO of destination airport</param>
        /// <param name="waypoints">the list of waypoints</param>
        public FlightPlan(string origin, string destination, List<Waypoint> waypoints)
        {
            _origin = origin;
            _dest = destination;
            _waypoints = waypoints;
        }

        /// <summary>
        /// ICAO of the origin airport
        /// </summary>
        public string Origin
        {
            get { return _origin; }
        }

        /// <summary>
        /// ICAO of the destination airport
        /// </summary>
        public string Destination
        {
            get { return _dest; }
        }

        /// <summary>
        /// the readonly list of waypoints
        /// </summary>
        public IList<Waypoint> Waipoints
        {
            get { return _waypoints.AsReadOnly(); }
        }


    }
}
