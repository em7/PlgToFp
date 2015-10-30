using PlgToFp.Windows.Infrastructure.Interaction.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
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
        private ILoggerFacade _logger;
        #endregion

        #region Public callbacks
        public Action DeleteWaypointCallback { get; set; }
        #endregion

        #region Commands
        private ICommand _closeCommand;
        public ICommand CloseCommand { get { return _closeCommand; } }

        private ICommand _deleteWpCommand;
        public ICommand DeleteWpCommand { get { return _deleteWpCommand; } }
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
        public DeleteWaypointDlgViewModel(IEventAggregator evtAggregator, ILoggerFacade logger)
        {
            _evtAggregator = evtAggregator;
            _logger = logger;
            _closeCommand = new DelegateCommand<FrameworkElement>(HandleCloseCmd);
            _deleteWpCommand = new DelegateCommand<FrameworkElement>(HandleDeleteCmd);
        }
        #endregion

        #region Command handlers
        private void HandleCloseCmd(FrameworkElement dlgCnt)
        {
            CloseDlg(dlgCnt);
        }

        private void HandleDeleteCmd(FrameworkElement dlgCnt)
        {
            if (DeleteWaypointCallback == null)
            {
                _logger.Log(string.Format("{0}:HandleDeleteCmd - DeleteWaypointCallback is null.", GetType().FullName),
                    Category.Warn, Priority.Medium);
                return;
            }

            CloseDlg(dlgCnt);
            DeleteWaypointCallback();
        }

        private void CloseDlg(FrameworkElement dlgCnt)
        {
            _evtAggregator.GetEvent<CloseDialogEvent>()
                .Publish(new CloseDialogEventPayload() { DialogContent = dlgCnt });
        }
        #endregion

    }
}
