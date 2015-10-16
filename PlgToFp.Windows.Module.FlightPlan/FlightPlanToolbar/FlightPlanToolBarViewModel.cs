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
        #region Commands
        public DelegateCommand OpenPlanGCommand { get; private set; }
        public DelegateCommand ExportIfmsCommand { get; private set; }
        #endregion

        #region ctor
        public FlightPlanToolBarViewModel()
        {
            InitializeCommands();
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

        } 
        #endregion
    }
}
