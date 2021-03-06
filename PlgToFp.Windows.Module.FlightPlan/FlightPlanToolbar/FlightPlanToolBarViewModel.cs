﻿using PlgToFp.Windows.Infrastructure;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Event;
using PlgToFp.Windows.Infrastructure.Interaction;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlanToolbar
{
    public class FlightPlanToolBarViewModel : BindableBase
    {
        #region Private Fields
        private IInteractionService _interactionService;
        private IEventAggregator _evtAggregator;
        #endregion

        #region Commands
        public DelegateCommand OpenPlanGCommand { get; private set; }
        public DelegateCommand ExportIfmsCommand { get; private set; }
        #endregion

        #region ctor
        public FlightPlanToolBarViewModel(IInteractionService interactionService, IEventAggregator evtAggregator)
        {
            InitializeCommands();
            _interactionService = interactionService;
            _evtAggregator = evtAggregator;

#if DEBUG
            var evtPayload = new FlightPlanReqPlanGOpenEventPayload() { Path = @"DebugData\PANCPAFA.plg" };
            _evtAggregator.GetEvent<FlightPlanReqPlanGOpenEvent>().Publish(evtPayload);
#endif
        }

        private void InitializeCommands()
        {
            OpenPlanGCommand = new DelegateCommand(OpenPlanG);
            ExportIfmsCommand = new DelegateCommand(ExportIfms);
        }
        #endregion

        #region functions
        private void ExportIfms()
        {

        }

        private void OpenPlanG()
        {
            var task = _interactionService.ShowOpenFileDialogAsync();
            task.ContinueWith(t =>
            {
                var path = t.Result;
                if (string.IsNullOrWhiteSpace(path))
                    return;

                var evtPayload = new FlightPlanReqPlanGOpenEventPayload() { Path = path };
                _evtAggregator.GetEvent<FlightPlanReqPlanGOpenEvent>().Publish(evtPayload);
            },
            TaskScheduler.FromCurrentSynchronizationContext());
            
        } 
        #endregion
    }
}
