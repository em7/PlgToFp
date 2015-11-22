using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class EditWaypointViewModelDesign : EditWaypointViewModel
    {
        public EditWaypointViewModelDesign() : base(null)
        {
            WaypointModel = new WaypointModel() { Identifier = "IDENT", Latitude = 6.66, Longitude = 12.89 };
            ErrorsContainer.SetErrors(() => Longitude, new []{new ValidationResult(false, "Longitude error")});
            ErrorsContainer.SetErrors(() => Latitude, new[] { new ValidationResult(false, "Latitude error") });
        }

        protected override bool CanGoBackCmd()
        {
            return true;
        }
    }
}
