using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Event
{
    /// <summary>
    /// Event for requesting flight plan displaying
    /// </summary>
    public class FlightPlanReqPlanShowEvent : PubSubEvent<FlightPlanReqPlanShowEventPayload>
    {
    }
}
