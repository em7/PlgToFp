using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Event
{
    /// <summary>
    /// Event payload for requesting flight plan displaying
    /// </summary>
    public class FlightPlanReqPlanShowEventPayload
    {
        public FlightPlanModel FlightPlan;
    }
}