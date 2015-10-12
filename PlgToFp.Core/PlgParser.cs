using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FlpToFp.Core
{
    /// <summary>
    /// Parser for the Plan-G flight plans
    /// </summary>
    public class PlgParser
    {
        private readonly NumberStyles COORDS_STYLE = NumberStyles.Integer | NumberStyles.AllowDecimalPoint;

        /// <summary>
        /// Parses the Plan-G flight plan
        /// </summary>
        /// <param name="plg">the plg format loaded in memory</param>
        /// <returns>application representation of the flight plan</returns>
        /// <exception cref="ArgumentNullException">plg argument null</exception>
        /// <exception cref="FlightPlanConvertException">convertion fails, usually due to bad input data</exception>
        public FlightPlan ParsePlg(XDocument plg)
        {
            if (plg == null) throw new ArgumentNullException("plg");

            var flightPlanElem = plg.Element("FlightPlan");
            if (flightPlanElem == null)
                throw new FlightPlanConvertException("FlightPlan element of the PLG flight plan is null.");

            var origin = ParseOrigin(flightPlanElem);
            var dest = ParseDestination(flightPlanElem);
            var waypointsAll = ParseWaypoints(flightPlanElem);
            var waypoints = RemoveOriginDest(waypointsAll, origin, dest).ToList();

            return new FlightPlan(origin, dest, waypoints);
        }

        private string ParseOrigin(XElement flightPlanElem)
        {
            if (flightPlanElem == null)
                throw new ArgumentNullException("flightPlanelem");

            var originElem = flightPlanElem.Element("DepartureID");
            if (originElem == null || originElem.Value == null)
                throw new FlightPlanConvertException("DepartureID element of the PLG flight plan is null or empty.");

            return originElem.Value;
        }

        private string ParseDestination(XElement flightPlanElem)
        {
            if (flightPlanElem == null)
                throw new ArgumentNullException("flightPlanelem");

            var destinationElem = flightPlanElem.Element("DestinationID");
            if (destinationElem == null || destinationElem.Value == null)
                throw new FlightPlanConvertException("DestinationID element of the PLG flight plan is null or empty.");

            return destinationElem.Value;
        }

        private IEnumerable<Waypoint> ParseWaypoints(XElement flightPlanElem)
        {
            if (flightPlanElem == null)
                throw new ArgumentNullException("flightPlanelem");

            var nodesElem = flightPlanElem.Element("Nodes");
            if (nodesElem == null)
                return new List<Waypoint>();

            var waypointElems = nodesElem.Elements("Waypoint");
            if (waypointElems == null || !waypointElems.Any())
                return new List<Waypoint>();

            var waypoints = waypointElems.Select(wpe => ParseWaypoint(wpe));
            return waypoints;
        }

        /// <summary>
        /// Parses the waypoint information. Waypoint identifier is never null. If is null, the value XXXXX is used instead.
        /// </summary>
        private Waypoint ParseWaypoint(XElement waypointElem)
        {
            if (waypointElem == null)
                throw new ArgumentNullException("waypointElem");

            var identElem = waypointElem.Element("Identifier");
            var ident = identElem != null ? identElem.Value.ToUpper() : "XXXXX";

            var latElem = waypointElem.Element("Latitude");
            if (latElem == null || string.IsNullOrEmpty(latElem.Value))
                throw new FlightPlanConvertException("Latitude is missing or unreadable for one of the PLG flight plan waypoint.");
            double latitude = 0.0;
            if (!double.TryParse(latElem.Value, COORDS_STYLE, CultureInfo.InvariantCulture, out latitude))
                throw new FlightPlanConvertException("Latitude is missing or unreadable for one of the PLG flight plan waypoint.");

            var longElem = waypointElem.Element("Longitude");
            if (longElem == null || string.IsNullOrEmpty(longElem.Value))
                throw new FlightPlanConvertException("Longitude is missing or unreadable for one of the PLG flight plan waypoint.");
            double longitude = 0.0;
            if (!double.TryParse(longElem.Value, COORDS_STYLE, CultureInfo.InvariantCulture, out longitude))
                throw new FlightPlanConvertException("Longitude is missing or unreadable for one of the PLG flight plan waypoint.");

            return new Waypoint(latitude, longitude, ident);
        }

        /// <summary>
        /// Removes the waypoints where identifier equals to the origin or destination airport
        /// </summary>
        private IEnumerable<Waypoint> RemoveOriginDest(IEnumerable<Waypoint> waypoints, string origin, string dest)
        {
            if (waypoints == null)
                return null;

            return waypoints.Where(wp => wp.Identifier != origin && wp.Identifier != dest);
        }
    }
}
