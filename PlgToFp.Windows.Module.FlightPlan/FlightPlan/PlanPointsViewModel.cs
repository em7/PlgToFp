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
    public class PlanPointsViewModel : BindableBase
    {
        private IEventAggregator _evtAggregator;



        private ICommand _editCommand;
        public ICommand EditCommand { get { return _editCommand; } }

        private ICommand _closeDialogCommand;
        public ICommand CloseDialogCommand { get { return _closeDialogCommand; } }



        public PlanPointsViewModel(IEventAggregator evtAggregator)
        {
            _evtAggregator = evtAggregator;
            _editCommand = new DelegateCommand<object>(HandleEditCmd);
            _closeDialogCommand = new DelegateCommand<object>(HandleCloseCmd);
        }

        


        private void HandleEditCmd(object dialogContent)
        {
            _evtAggregator.GetEvent<ShowDialogEvent>()
                .Publish(new ShowDialogEventPayload() { DialogContent = dialogContent });
        }
        private void HandleCloseCmd(object dialogContent)
        {
            _evtAggregator.GetEvent<CloseDialogEvent>()
                .Publish(new CloseDialogEventPayload() { DialogContent = dialogContent });
        }





        private FlightPlanViewModel _parentFlightPlan;

        public FlightPlanViewModel ParentFlightPlan
        {
            get { return _parentFlightPlan; }
            set { _parentFlightPlan = value; }
        }



    }
}
