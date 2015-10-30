using Microsoft.Practices.Unity;
using PlgToFp.Windows.Infrastructure.Interaction.Event;
using Prism.Commands;
using Prism.Events;
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
        public PlanPointsPartViewModel(IEventAggregator evtAggregator, IUnityContainer container)
        {
            _evtAggregator = evtAggregator;
            _container = container;
            _editCommand = new DelegateCommand<object>(HandleEditCmd);
            _deleteCommand = new DelegateCommand<string>(HandleDeleteCmd);
            _closeDialogCommand = new DelegateCommand<object>(HandleCloseCmd);
        } 
        #endregion




        #region Command handlers
        private void HandleEditCmd(object dialogContent)
        {
            //_evtAggregator.GetEvent<ShowDialogEvent>()
            //    .Publish(new ShowDialogEventPayload() { DialogContent = dialogContent });
        }
        private void HandleCloseCmd(object dialogContent)
        {
            _evtAggregator.GetEvent<CloseDialogEvent>()
                .Publish(new CloseDialogEventPayload() { DialogContent = dialogContent });
        } 
        private void HandleDeleteCmd(string identifier)
        {
            var dlgCnt = _container.Resolve<DeleteWaypointDlgContent>();
            var dlgVm = dlgCnt.DataContext as DeleteWaypointDlgViewModel;
            if (dlgVm != null)
            {
                dlgVm.Identifier = identifier;
            }

            _evtAggregator.GetEvent<ShowDialogEvent>()
                .Publish(new ShowDialogEventPayload() {
                    DialogContent = dlgCnt,
                    Header = string.Format("Delete waypoint '{0}'?", identifier),
                });
        }
        #endregion





        



    }
}
