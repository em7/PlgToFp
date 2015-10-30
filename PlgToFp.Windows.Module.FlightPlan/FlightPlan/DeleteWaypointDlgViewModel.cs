using PlgToFp.Windows.Infrastructure.Interaction.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class DeleteWaypointDlgViewModel : BindableBase 
    {
        #region Private properties
        private IEventAggregator _evtAggregator;
        #endregion

        #region Commands
        private ICommand _closeCommand;
        public ICommand CloseCommand { get { return _closeCommand; } }
        #endregion

        #region Bindable properties
        private string _identifier;

        public string Identifier
        {
            get { return _identifier; }
            set
            {
                SetProperty(ref _identifier, value);
                OnPropertyChanged(() => Identifier);
            }
        }
        #endregion

        #region ctor
        public DeleteWaypointDlgViewModel(IEventAggregator evtAggregator)
        {
            _evtAggregator = evtAggregator;
            _closeCommand = new DelegateCommand<FrameworkElement>(HandleCloseCmd);
        }

        private void HandleCloseCmd(FrameworkElement dlgCnt)
        {
            _evtAggregator.GetEvent<CloseDialogEvent>()
                .Publish(new CloseDialogEventPayload() { DialogContent = dlgCnt });
        }
        #endregion

    }
}
