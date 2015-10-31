using PlgToFp.Windows.Infrastructure.Helpers;
using PlgToFp.Windows.Infrastructure.Interaction.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    /// <summary>
    /// A design-time viewmodel class for PlanPointsPartView
    /// </summary>
    public class PlanPointsPartViewModelDesign : PlanPointsPartViewModel
    {
        public PlanPointsPartViewModelDesign() : this(true) { }
    
        public PlanPointsPartViewModelDesign(bool createParent) : base(null, null, null, null)
        {
            if (createParent && DesignHelper.IsInDesignMode())
            {
                var fpvm = new FlightPlanViewModelDesign(this)
                {
                    FlightPlan = new Model.FlightPlanModel()
                };
                ParentFlightPlan = fpvm;
            }
        }
    }
}
