using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model
{
    /// <summary>
    /// Builds the flight plan model
    /// </summary>
    public class FlightPlanModelBuilder
    {
        private FlightPlanModel _model = new FlightPlanModel();

        public FlightPlanModelBuilder ForFlightPlan(Core.FlightPlan flightPlan)
        {
            _model.Origin = flightPlan.Origin;
            _model.Destination = flightPlan.Destination;

            return this;
        }

        public FlightPlanModel Build()
        {
            return _model;
        }
    }
}
