using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class EditWaypointViewModel : BindableBase, INavigationAware
    {
        #region Constants
        public static string NAV_PARAM_WAYPOINT = "NAV_PARAM_WAYPOINT";
        #endregion

        #region Private fields
        private ILoggerFacade _logger;
        private IRegionNavigationService _regionNavSvc;
        #endregion

        #region Bindable properties
        private WaypointModel _waypointModel;

        public WaypointModel WaypointModel
        {
            get { return _waypointModel; }
            set
            {
                SetProperty(ref _waypointModel, value);
                OnPropertyChanged(() => WaypointModel);
                ReadWaypointVmProperties();
            }
        }

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

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                SetProperty(ref _latitude, value);
                OnPropertyChanged(() => Latitude);
            }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                SetProperty(ref _longitude, value);
                OnPropertyChanged(() => Longitude);
            }
        }
        #endregion

        #region Commands
        private ICommand _goBackCommand;
        public ICommand GoBackCommand { get { return _goBackCommand; } }

        private ICommand _saveCommand;
        public ICommand SaveCommand { get { return _saveCommand; } }
        #endregion

        #region ctor
        public EditWaypointViewModel(ILoggerFacade logger)
        {
            _logger = logger;

            _goBackCommand = new DelegateCommand(HandleGoBackCmd, CanGoBackCmd);
            _saveCommand = new DelegateCommand(HandleSaveCmd);
        }
        #endregion

        #region Command handlers
        private void HandleGoBackCmd()
        {
            if (!CanGoBackCmd())
                return;

            _regionNavSvc.Journal.GoBack();
        }

        protected virtual bool CanGoBackCmd()
        {
            return _regionNavSvc.Journal.CanGoBack;
        }

        private void HandleSaveCmd()
        {
            UpdateWaypointVmProperties();

            if (!CanGoBackCmd())
                return;

            _regionNavSvc.Journal.GoBack();
        }
        #endregion

        #region Private functions
        /// <summary>
        /// Reads the properties of viewmodel and makes copy of them so
        /// they can be edited and the edit commited/discarded later
        /// </summary>
        private void ReadWaypointVmProperties()
        {
            if (WaypointModel != null)
            {
                Identifier = WaypointModel.Identifier;
                Longitude = WaypointModel.Longitude;
                Latitude = WaypointModel.Latitude;
            }
            else
            {
                Identifier = "";
                Longitude = 0d;
                Latitude = 0d;
            }
            
        }

        /// <summary>
        /// Updates the properties of viewmodel => commits the edit changes
        /// </summary>
        private void UpdateWaypointVmProperties()
        {
            if (WaypointModel != null)
            {
                WaypointModel.Identifier = Identifier;
                WaypointModel.Longitude = Longitude;
                WaypointModel.Latitude = Latitude;
            }
        }
        #endregion


        #region INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!navigationContext.Parameters.ContainsKey(NAV_PARAM_WAYPOINT))
            {
                _logger.Log(string.Format("{0}:OnNavigatedTo - The navigationContext doesn't contain NAV_PARAM_WAPYPOINT.", GetType().FullName),
                    Category.Warn, Priority.Medium);
                return;
            }

            if (!(navigationContext.Parameters[NAV_PARAM_WAYPOINT] is WaypointModel))
            {
                _logger.Log(string.Format("{0}:OnNavigatedTo - The navigationContext isn't a WaypointModel type.", GetType().FullName),
                    Category.Warn, Priority.Medium);
                return;
            }

            WaypointModel = (WaypointModel)navigationContext.Parameters[NAV_PARAM_WAYPOINT];
            _regionNavSvc = navigationContext.NavigationService;
        } 
        #endregion
    }
}
