﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class PlanPointsViewModel : BindableBase
    {
        private FlightPlanViewModel _parentFlightPlan;

        public FlightPlanViewModel ParentFlightPlan
        {
            get { return _parentFlightPlan; }
            set { _parentFlightPlan = value; }
        }

    }
}
