using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlgToFp.Core
{
    /// <summary>
    /// Exporter to the iFMS format
    /// </summary>
    public class FpExporter
    {
        /// <summary>
        /// Creates the fp XML document in the iFMS format
        /// </summary>
        /// <param name="flightPlan">flight plan to convert</param>
        public XDocument CreateFp(FlightPlan flightPlan)
        {
            var routeWaypoints = flightPlan.Waipoints.Select(CreateRouteWaypoint);
            var route = CreateRoute(routeWaypoints);
            var fp = CreateFlightplan(route, flightPlan);

            var doc = new XDocument(fp);
            return doc;
        }

        private XElement CreateFlightplan(XElement route, FlightPlan flightPlan)
        {
            var fp = new XElement("FLIGHTPLAN",
                new XElement("OriginAirport", flightPlan.Origin),
                new XElement("DestinationAirport", flightPlan.Destination),
                route);
            return fp;
        }

        private XElement CreateRoute(IEnumerable<XElement> routeWaypoints)
        {
            var routeElem = new XElement("ROUTE",
                new XElement("LEGS", routeWaypoints));
            return routeElem;
        }

        private XElement CreateRouteWaypoint(Waypoint waypoint)
        {
            var routeWaypoint = new XElement("Route_Waypoint",
                new XElement("Name", waypoint.Identifier),
                new XElement("Type", "Normal"),
                new XElement("Latitude", FormatCoordinate(waypoint.Latitude)),
                new XElement("Longitude", FormatCoordinate(waypoint.Longitude)),
                new XElement("Speed", 0),
                new XElement("Altitude", 0),
                new XElement("AltitudeCons", 0),
                new XElement("AltitudeRestriction", 0));

            return routeWaypoint;
        }

        private string FormatCoordinate(double coord)
        {
            return coord.ToString("F6", CultureInfo.InvariantCulture);
        }
    }
}
