using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class FlightPlanViewModel : BindableBase
    {

        #region private fields
        private readonly IEventAggregator _evtAggregator; 
        #endregion

        #region ctor

        public FlightPlanViewModel(IEventAggregator evtAggregator)
        {
            _evtAggregator = evtAggregator;

            _evtAggregator.GetEvent<FlightPlanReqPlanShowEvent>().Subscribe(OnReqPlanShow);
        } 
        #endregion

        #region event handlers
        /// <summary>
        /// Event handler for requesting flight plan displaying
        /// </summary>
        /// <param name="obj"></param>
        private void OnReqPlanShow(FlightPlanReqPlanShowEventPayload payload)
        {
            FlightPlan = payload.FlightPlan;
        } 
        #endregion

        #region Properties
        private FlightPlanModel _flightPlan;

        public FlightPlanModel FlightPlan
        {
            get { return _flightPlan; }
            set
            {
                SetProperty(ref _flightPlan, value);
                OnPropertyChanged(() => FlightPlan);
            }
        } 
        #endregion

    }
}
