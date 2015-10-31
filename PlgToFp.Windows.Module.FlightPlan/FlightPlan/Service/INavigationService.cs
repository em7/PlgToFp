using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Service
{
    public interface INavigationService
    {
        void EditWaypoint(WaypointModel waypoint);
    }
}