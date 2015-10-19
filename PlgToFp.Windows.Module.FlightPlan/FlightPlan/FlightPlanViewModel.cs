using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Event;
using System.IO;
using System.Xml;
using PlgToFp.Core;

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
            _evtAggregator.GetEvent<FlightPlanOpenErrorEvent>().Subscribe(OnOpenPlanError);
        }
        #endregion

        #region event handlers
        /// <summary>
        /// Event handler for requesting flight plan displaying
        /// </summary>
        /// <param name="obj"></param>
        private void OnReqPlanShow(FlightPlanReqPlanShowEventPayload payload)
        {
            HasError = false;
            ErrorMessage = null;
            FlightPlan = payload.FlightPlan;
        }

        /// <summary>
        /// Event handler when error ocurred during opening
        /// </summary>
        private void OnOpenPlanError(FlightPlanOpenErrorEventPayload payload)
        {
            HasError = true;
            ErrorMessage = GetOpenFpErrorMessageFor(payload.Exception);
            FlightPlan = null;
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

        private bool _hasError;

        public bool HasError
        {
            get { return _hasError; }
            set
            {
                SetProperty(ref _hasError, value);
                OnPropertyChanged(() => HasError);
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                SetProperty(ref _errorMessage, value);
                OnPropertyChanged(() => ErrorMessage);
            }
        }


        #endregion

        #region private functions
        /// <summary>
        /// Checks the exceptions which can occur during opening the flight plan and
        /// returns appropriate error messages which can be displayed on the UI
        /// </summary>
        /// <param name="exception">Exception or AggregateException</param>
        /// <returns>the error message to be displayed or null if exception is null</returns>
        private string GetOpenFpErrorMessageFor(Exception exception)
        {
            if (exception == null) return null;

            bool io = false;
            bool xml = false;
            bool convert = false;

            bool identified = false; //at least one of the exceptions was identified

            if (exception is AggregateException)
            {
                 var aggregateExc = (AggregateException)exception;
                aggregateExc.Flatten().Handle(ex =>
                {
                    if (ex is IOException) io = true;
                    if (ex is XmlException) xml = true;
                    if (ex is FlightPlanConvertException) convert = true;

                    return true;
                });
            }
            else
            {
                if (exception is IOException) io = true;
                if (exception is XmlException) xml = true;
                if (exception is FlightPlanConvertException) convert = true;
            }

            var sb = new StringBuilder();
            sb.AppendLine("Opening the flight plan was not successful.");
            if (io)
            {
                identified = true;
                sb.AppendLine("Please check the file if is accessible and you have permissions to read it.");
            }
            if (xml || convert)
            {
                identified = true;
                sb.AppendLine("Please check the format of the flight plan.");
            }

            if (! identified)
            {
                sb.AppendLine("The flight plan could not be read.");
            }


            return sb.ToString();
        }
        #endregion
    }
}
