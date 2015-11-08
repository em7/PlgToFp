using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Event;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using System.IO;
using System.Xml;
using PlgToFp.Core;
using PlgToFp.Windows.Infrastructure.Helpers;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class FlightPlanViewModel : BindableBase
    {

        #region private fields
        private readonly IEventAggregator _evtAggregator; 
        #endregion

        #region ctor

        public FlightPlanViewModel(IEventAggregator evtAggregator, PlanPointsPartViewModel planPointsVm,
            FlightPlanMapViewModel flightPlanMapViewModel)
        {
            _evtAggregator = evtAggregator;
            _planPointsViewModel = planPointsVm;
            _planPointsViewModel.ParentFlightPlan = this;
            _flightPlanMapViewModel = flightPlanMapViewModel;
            _flightPlanMapViewModel.ParentFlightPlan = this;

            var reqPlanShowEvt = _evtAggregator.GetEvent<FlightPlanReqPlanShowEvent>();
            var openPlanErrEvt = _evtAggregator.GetEvent<FlightPlanOpenErrorEvent>();

            if (reqPlanShowEvt != null)
                reqPlanShowEvt.Subscribe(OnReqPlanShow);

            if (openPlanErrEvt != null)
                openPlanErrEvt.Subscribe(OnOpenPlanError);
        }

        /// <summary>
        /// For Design-Time only
        /// </summary>
        protected FlightPlanViewModel() { }
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

        private PlanPointsPartViewModel _planPointsViewModel;

        public PlanPointsPartViewModel PlanPointsViewModel
        {
            get { return _planPointsViewModel; }
            set
            {
                SetProperty(ref _planPointsViewModel, value);
                OnPropertyChanged(() => PlanPointsViewModel);
            }
        }

        private FlightPlanMapViewModel _flightPlanMapViewModel;
        public FlightPlanMapViewModel FlightPlanMapViewModel
        {
            get { return _flightPlanMapViewModel; }
            set
            {
                SetProperty(ref _flightPlanMapViewModel, value);
                OnPropertyChanged(() => FlightPlanMapViewModel);
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

        #region Public functions
        /// <summary>
        /// Moves a waypoint before another one or to the end of the waypoints list if
        /// before parameter is null
        /// </summary>
        /// <param name="waypointToMove">waypoint to be moved</param>
        /// <param name="before">before which waypoint the new one should be moved or null if the new should be moved to the end</param>
        public void RearrangeWaypoints(object waypointToMove, object before)
        {
            if (FlightPlan == null)
                return;

            var wpToMove = waypointToMove as WaypointModel;
            var wpBefore = before as WaypointModel;

            if (before != null)
            {
                var idx = FlightPlan.Waypoints.IndexOf(wpBefore);
                if (idx >= 0)
                {
                    FlightPlan.Waypoints.Remove(wpToMove);
                    FlightPlan.Waypoints.Insert(idx, wpToMove);
                }
                else
                {
                    FlightPlan.Waypoints.Remove(wpToMove);
                    FlightPlan.Waypoints.Add(wpToMove);
                }
            }
        }

        public void RemoveWaypoint(WaypointModel waypoint)
        {
            if (FlightPlan == null || waypoint == null)
                return;

            FlightPlan.Waypoints.Remove(waypoint);
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
            sb.Append("Opening the flight plan was not successful.");
            if (io)
            {
                identified = true;
                sb.Append(Environment.NewLine + "Please check the file if is accessible and you have permissions to read it.");
            }
            if (xml || convert)
            {
                identified = true;
                sb.Append(Environment.NewLine + "Please check the format of the flight plan.");
            }

            if (! identified)
            {
                sb.Append(Environment.NewLine + "The flight plan could not be read.");
            }


            return sb.ToString();
        }
        #endregion
    }
}
