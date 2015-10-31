using Microsoft.Practices.Unity;
using PlgToFp.Windows.Infrastructure.Interaction.Event;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Service;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class PlanPointsPartViewModel : BindableBase
    {
        #region Private properties
        private IEventAggregator _evtAggregator;
        private IUnityContainer _container;
        private ILoggerFacade _logger;
        private INavigationService _navigationService;
        #endregion

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
        #endregion

        #region Commands
        private ICommand _editCommand;
        public ICommand EditCommand { get { return _editCommand; } }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand { get { return _deleteCommand; } }

        private ICommand _closeDialogCommand;
        
        public ICommand CloseDialogCommand { get { return _closeDialogCommand; } }
        #endregion



        #region ctor
        public PlanPointsPartViewModel(IEventAggregator evtAggregator, IUnityContainer container,
            ILoggerFacade logger, INavigationService navigationService)
        {
            _evtAggregator = evtAggregator;
            _container = container;
            _logger = logger;
            _navigationService = navigationService;
            _editCommand = new DelegateCommand<WaypointModel>(HandleEditCmd);
            _deleteCommand = new DelegateCommand<WaypointModel>(HandleDeleteCmd);
            _closeDialogCommand = new DelegateCommand<object>(HandleCloseCmd);
        } 
        #endregion




        #region Command handlers
        private void HandleEditCmd(WaypointModel waypoint)
        {
            _navigationService.EditWaypoint(waypoint);
        }
        private void HandleCloseCmd(object dialogContent)
        {
            _evtAggregator.GetEvent<CloseDialogEvent>()
                .Publish(new CloseDialogEventPayload() { DialogContent = dialogContent });
        } 
        private void HandleDeleteCmd(WaypointModel waypoint)
        {
            if (waypoint == null)
                return;

            var dlgCnt = _container.Resolve<DeleteWaypointDlgContent>();
            var dlgVm = dlgCnt.DataContext as DeleteWaypointDlgViewModel;
            if (dlgVm != null)
            {
                dlgVm.Identifier = waypoint.Identifier;
                dlgVm.DeleteWaypointCallback = () => DeleteWaypoint(waypoint);
            }

            _evtAggregator.GetEvent<ShowDialogEvent>()
                .Publish(new ShowDialogEventPayload() {
                    DialogContent = dlgCnt,
                    Header = string.Format("Delete waypoint '{0}'?", waypoint.Identifier),
                });
        }
        #endregion


        #region Private functions
        private void DeleteWaypoint(WaypointModel waypoint)
        {
            if (waypoint == null || _parentFlightPlan == null)
            {
                _logger.Log(string.Format("{0}:DeleteWaypoint - waypoint or parent flight plan null. Waypoint: {1}; ParentFlightPlan: {2};",
                                            this.GetType().FullName,
                                            waypoint == null ? "null" : "OK",
                                            _parentFlightPlan == null ? "null" : "OK"),
                            Category.Warn,
                            Priority.Medium
                    );
                return;
            }

            ParentFlightPlan.RemoveWaypoint(waypoint);
        }
        #endregion






    }
}
