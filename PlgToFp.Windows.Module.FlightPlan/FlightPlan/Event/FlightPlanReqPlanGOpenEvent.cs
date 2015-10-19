using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Event
{
    /// <summary>
    /// Event for request for opening the Plan-G flight plan
    /// </summary>
    public class FlightPlanReqPlanGOpenEvent : PubSubEvent<FlightPlanReqPlanGOpenEventPayload>
    {
    }
}
