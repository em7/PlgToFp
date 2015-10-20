using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlgToFp.Core;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model
{
    /// <summary>
    /// A bindable representation of the FlightPlan data
    /// </summary>
    public class FlightPlanModel : BindableBase
    {
        #region Properties
        private Core.FlightPlan _flightPlan;


        private string _origin;

        public string Origin
        {
            get { return _origin; }
            set
            {
                SetProperty(ref _origin, value);
                OnPropertyChanged(() => Origin);
            }
        }

        private string _destination;

        public string Destination
        {
            get { return _destination; }
            set
            {
                SetProperty(ref _destination, value);
                OnPropertyChanged(() => Destination);
            }
        }
        #endregion



        #region ctor
        /// <summary>
        /// Creates an empty flight plan
        /// </summary>
        public FlightPlanModel()
        {

        }
        #endregion


    }
}
