using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class EditWaypointViewModelDesign : EditWaypointViewModel
    {
        public EditWaypointViewModelDesign() : base(null)
        {
            WaypointModel = new WaypointModel() { Identifier = "IDENT", Latitude = 6.66, Longitude = 12.89 };
        }
    }
}
