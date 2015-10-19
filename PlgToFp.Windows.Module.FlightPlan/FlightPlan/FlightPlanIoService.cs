using PlgToFp.Core;
using Prism.Events;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
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
        /// Async version of LoadPlanGFlightPlan
        /// </summary>
        public Task<FlightPlanModel> LoadPlanGFlightPlanAsync(string path)
        {
            return Task.Run(() => LoadPlanGFlightPlan(path));
        }

        /// <summary>
        /// Handler for event - PlanG flight plan open requested
        /// </summary>
        private async void OnReqPlanGOpen(FlightPlanReqPlanGOpenEventPayload payload)
        {
            if (payload == null || string.IsNullOrWhiteSpace(payload.Path))
                return;

            FlightPlanModel fplModel = null;

            try
            {
                var flightplan = await ParsePlanGFlightPlanAsync(payload.Path);
                var builder = new FlightPlanModelBuilder();
                builder.ForFlightPlan(flightplan);
                fplModel = builder.Build();
            }
            catch (Exception ex)
            {
                var message = string.Format("Exception in OnReqPlanGOpen:\n{0}", ex.ToString());
                _logger.Log(message, Category.Exception, Priority.Medium);

                var evtPayload = new FlightPlanOpenErrorEventPayload()
                {
                    Exception = ex
                };
                _evtAggregator.GetEvent<FlightPlanOpenErrorEvent>().Publish(evtPayload);
            }

            var showEvtPayload = new FlightPlanReqPlanShowEventPayload()
            {
                FlightPlan = fplModel
            };
            _evtAggregator.GetEvent<FlightPlanReqPlanShowEvent>().Publish(showEvtPayload);
        }

        private Core.FlightPlan ParsePlanGFlightPlan(string path)
        {
            try
            {
                var plgDoc = XDocument.Load(path);
                var parser = new Core.PlgParser();
                var flightPlan = parser.ParsePlg(plgDoc);
                return flightPlan;
            }
            catch (Exception ex)
            {
                var message = string.Format("Exception when parsing PlanGFlightPlan:\n{0}", ex.ToString());
                _logger.Log(message, Category.Exception, Priority.Medium);
                throw;
            }
        }

        private Task<Core.FlightPlan> ParsePlanGFlightPlanAsync(string path)
        {
            return Task.Run(() => ParsePlanGFlightPlan(path));
        }
    }
}
