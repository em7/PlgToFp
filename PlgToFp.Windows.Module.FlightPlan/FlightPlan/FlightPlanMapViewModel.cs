using Prism.Mvvm;
using System.Collections.ObjectModel;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class FlightPlanMapViewModel : BindableBase
    {
        #region Bindable properties

        private FlightPlanViewModel _parentFlightPlan;
        public FlightPlanViewModel ParentFlightPlan
        {
            get { return _parentFlightPlan; }
            set
            {
                SetProperty(ref _parentFlightPlan, value);
                OnPropertyChanged(() => ParentFlightPlan);
            }
        }

        

        public FlightPlanMapViewModel()
        {
           
        }

       

        #endregion
    }
}
