using PlgToFp.Core;
using Prism.Events;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Event;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Service
{
    /// <summary>
    /// Service for importing/exporting flight plans
    /// </summary>
    public class FlightPlanIoService : IFlightPlanIoService
    {

        private readonly ILoggerFacade _logger;
        private readonly IEventAggregator _evtAggregator;

        public FlightPlanIoService(ILoggerFacade logger, IEventAggregator evtAggregator)
        {
            _logger = logger;
            _evtAggregator = evtAggregator;
            _evtAggregator.GetEvent<FlightPlanReqPlanGOpenEvent>().Subscribe(OnReqPlanGOpen);
        }

        

        /// <summary>
        /// Loads the Plan-G flight plan
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns>loaded flight plan</returns>
        /// <exception cref="ArgumentNullException">path is null</exception>
        public FlightPlanModel LoadPlanGFlightPlan(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var flightPlan = ParsePlanGFlightPlan(path);
            var builder = new FlightPlanModelBuilder();
            builder.ForFlightPlan(flightPlan);

            return builder.Build();
        }

        /// <summary>
        /// Handler for event - PlanG flight plan open requested
        /// </summary>
        private void OnReqPlanGOpen(FlightPlanReqPlanGOpenEventPayload payload)
        {
            if (payload == null || string.IsNullOrWhiteSpace(payload.Path))
                return;

            var task = Task.Run(() => ParsePlanGFlightPlan(payload.Path))
                .ContinueWith(FlightPlanParsed, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void FlightPlanParsed(Task<Core.FlightPlan> task)
        {
            if (task.Exception != null)
            {
                RaiseFlightPlanParseExceptionEvent(task.Exception);
                return;
            }

            try
            {
                var builder = new FlightPlanModelBuilder();
                builder.ForFlightPlan(task.Result);
                var fplModel = builder.Build();
                RaiseFlightPlanParseOkEvent(fplModel);
            }
            catch (Exception ex)
            {
                RaiseFlightPlanParseExceptionEvent(ex);
            }
        }

        private void RaiseFlightPlanParseExceptionEvent(Exception exception)
        {
            var message = string.Format("Exception in OnReqPlanGOpen:\n{0}", exception.ToString());
            _logger.Log(message, Category.Exception, Priority.Medium);

            var evtPayload = new FlightPlanOpenErrorEventPayload()
            {
                Exception = exception
            };
            _evtAggregator.GetEvent<FlightPlanOpenErrorEvent>().Publish(evtPayload);
        }

        private void RaiseFlightPlanParseOkEvent(FlightPlanModel fplModel)
        {
            var showEvtPayload = new FlightPlanReqPlanShowEventPayload()
            {
                FlightPlan = fplModel
            };
            _evtAggregator.GetEvent<FlightPlanReqPlanShowEvent>().Publish(showEvtPayload);
        }

        private Core.FlightPlan ParsePlanGFlightPlan(string path)
        {
            var plgDoc = XDocument.Load(path);
            var parser = new Core.PlgParser();
            var flightPlan = parser.ParsePlg(plgDoc);
            return flightPlan;
        }

        private FlightPlanModel BuildFlightPlanModel(Core.FlightPlan flightPlan)
        {
            var builder = new FlightPlanModelBuilder();
            builder.ForFlightPlan(flightPlan);
            var fplModel = builder.Build();
            return fplModel;
        }

    }
}
