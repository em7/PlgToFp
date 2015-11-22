using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using PlgToFp.Windows.Infrastructure.ViewModel;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Converter;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    public class EditWaypointViewModel : ViewModel
    {
        #region Constants
        public static string NAV_PARAM_WAYPOINT = "NAV_PARAM_WAYPOINT";
        #endregion

        #region Private fields
        private ILoggerFacade _logger;
        private IRegionNavigationService _regionNavSvc;

        private Converter.CoordsLatNumToFmcConverter _latConverter = new CoordsLatNumToFmcConverter();
        private Converter.CoordsLonNumToFmcConverter _lonConverter = new CoordsLonNumToFmcConverter(); 
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
                ValidateIdentifier(value);
                OnPropertyChanged(() => Identifier);
            }
        }

        private string _latitude;
        public string Latitude
        {
            get { return _latitude; }
            set
            {
                SetProperty(ref _latitude, value);
                ValidateLatitude(value);
                OnPropertyChanged(() => Latitude);
            }
        }

        private string _longitude;
        public string Longitude
        {
            get { return _longitude; }
            set
            {
                SetProperty(ref _longitude, value);
                ValidateLongitude(value);
                OnPropertyChanged(() => Longitude);
            }
        }

        public string IdentifierErrors
        {
            get { return GetErrorsFor(() => Identifier); }
        }

        public string LatitudeErrors
        {
            get { return GetErrorsFor(() => Latitude); }
        }

        public string LongitudeErrors
        {
            get { return GetErrorsFor(() => Longitude); }
        }
        #endregion

        #region Commands
        private ICommand _goBackCommand;
        public ICommand GoBackCommand { get { return _goBackCommand; } }

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand { get { return _saveCommand; } }
        #endregion

        #region ctor
        public EditWaypointViewModel(ILoggerFacade logger)
        {
            _logger = logger;

            _goBackCommand = new DelegateCommand(HandleGoBackCmd, CanGoBackCmd);
            _saveCommand = new DelegateCommand(HandleSaveCmd, CanSaveCmd);
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
            if (!CanSaveCmd())
                return;

            UpdateWaypointVmProperties();

            if (!CanGoBackCmd())
                return;

            _regionNavSvc.Journal.GoBack();
        }

        private bool CanSaveCmd()
        {
            return !(HasErrors);
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
                Longitude = _lonConverter.Convert(WaypointModel.Longitude, typeof (string), null,
                                CultureInfo.InvariantCulture) as string;
                Latitude = _latConverter.Convert(WaypointModel.Latitude, typeof (string), null,
                                CultureInfo.InvariantCulture) as string;
            }
            else
            {
                Identifier = "";
                Longitude = "E000°00.0";
                Latitude = "N00°00.0";
            }
            
        }

        /// <summary>
        /// Updates the properties of viewmodel => commits the edit changes
        /// </summary>
        private void UpdateWaypointVmProperties()
        {
            if (WaypointModel != null)
            {
                if (ValidateIdentifier(Identifier))
                {
                    WaypointModel.Identifier = Identifier;
                }

                if (ValidateLatitude(Latitude))
                {
                    object converted = _latConverter.ConvertBack(Latitude, typeof (double), null,
                        CultureInfo.InvariantCulture);
                    if (converted is double)
                    {
                        WaypointModel.Latitude = (double)converted;
                    }
                }

                if (ValidateLongitude(Longitude))
                {
                    object converted = _lonConverter.ConvertBack(Longitude, typeof(double), null,
                        CultureInfo.InvariantCulture);
                    if (converted is double)
                    {
                        WaypointModel.Longitude = (double)converted;
                    }
                }
            }
        }

        private bool ValidateIdentifier(string val)
        {
            var valid = (!string.IsNullOrWhiteSpace(val))
                        && (val.Length > 1)
                        && (val.Length < 6);

            if (valid)
            {
                ErrorsContainer.ClearErrors(() => Identifier);
            }
            else
            {
                ErrorsContainer.ClearErrors(() => Identifier);
                ErrorsContainer.SetErrors(() => Identifier, new [] {new ValidationResult(false, 
                    "Identifier is mandatory. Should contain 2-5 letters and numbers.") });
            }

            RaiseErrorsChanged(() => Identifier);
            _saveCommand?.RaiseCanExecuteChanged();
            OnPropertyChanged(() => IdentifierErrors);
            return valid;
        }

        private bool ValidateLongitude(string val)
        {
            var valid = _lonConverter.ValidateString(val);
            if (valid)
            {
                ErrorsContainer.ClearErrors(() => Longitude);
            }
            else
            {
                ErrorsContainer.ClearErrors(() => Longitude);
                ErrorsContainer.SetErrors(() => Longitude, new [] { new ValidationResult(false, "Please use the E123°45.6 or W12345.6 format."), });
            }

            RaiseErrorsChanged(() => Longitude);
            _saveCommand?.RaiseCanExecuteChanged();
            OnPropertyChanged(() => LongitudeErrors);
            return valid;
        }

        private bool ValidateLatitude(string val)
        {
            var valid = _latConverter.ValidateString(val);
            if (valid)
            {
                ErrorsContainer.ClearErrors(() => Latitude);
            }
            else
            {
                ErrorsContainer.ClearErrors(() => Latitude);
                ErrorsContainer.SetErrors(() => Latitude, new[] { new ValidationResult(false, "Please use the N23°45.6 or S2345.6 format."), });
            }

            RaiseErrorsChanged(() => Latitude);
            _saveCommand?.RaiseCanExecuteChanged();
            OnPropertyChanged(() => LatitudeErrors);
            return valid;
        }

        private string GetErrorsFor<T>(Expression<Func<T>> propertyExpression)
        {
            var errors = ErrorsContainer.GetErrors(PropertySupport.ExtractPropertyName(propertyExpression));
            var errStrs = errors.Where(r => !r.IsValid).Select(r => r.ErrorContent.ToString());
            var errStr = string.Join(Environment.NewLine, errStrs);
            return errStr;
        }
        #endregion

        #region INavigationAware

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

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
