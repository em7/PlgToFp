using PlgToFp.Windows.Infrastructure.Interaction.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class DeleteWaypointDlgViewModelDesign : DeleteWaypointDlgViewModel
    {
        public DeleteWaypointDlgViewModelDesign() : base(null, null)
        {
            Identifier = "IDENT";
        }

    }
}
