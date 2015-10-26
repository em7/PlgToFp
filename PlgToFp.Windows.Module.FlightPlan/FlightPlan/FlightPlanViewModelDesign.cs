using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class FlightPlanViewModelDesign : FlightPlanViewModel
    {
        public FlightPlanViewModelDesign(PlanPointsPartViewModel planPointsVm)
        {
            PlanPointsViewModel = planPointsVm;
        }

        public FlightPlanViewModelDesign()
        {
            PlanPointsViewModel = new PlanPointsPartViewModelDesign(false);
            PlanPointsViewModel.ParentFlightPlan = this;
            FlightPlan = new Model.FlightPlanModel();
            HasError = false;
        }
    }
}
