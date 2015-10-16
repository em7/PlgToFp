﻿using PlgToFp.Windows.Infrastructure;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlanToolbar
{
    public class FlightPlanToolBarViewModel : BindableBase
    {
        #region Private Fields
        private IInteractionService _interactionService;
        private IFlightPlanIoService _flightPlanIoService;
        #endregion

        #region Commands
        public DelegateCommand OpenPlanGCommand { get; private set; }
        public DelegateCommand ExportIfmsCommand { get; private set; }
        #endregion

        #region ctor
        public FlightPlanToolBarViewModel(IInteractionService interactionService, IFlightPlanIoService flightPlanIoService)
        {
            InitializeCommands();
            _interactionService = interactionService;
            _flightPlanIoService = flightPlanIoService;
        }

        private void InitializeCommands()
        {
            OpenPlanGCommand = new DelegateCommand(async () => OpenPlanG());
            ExportIfmsCommand = new DelegateCommand(ExportIfms);
        }
        #endregion

        #region functions
        private void ExportIfms()
        {

        }

        private async Task OpenPlanG()
        {
            var path = await _interactionService.ShowOpenFileDialogAsync();
            if (string.IsNullOrWhiteSpace(path))
                return;

            //TODO publish event instead
            var plan = await _flightPlanIoService.LoadPlanGFlightPlanAsync(path);
        } 
        #endregion
    }
}