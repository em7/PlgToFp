using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;

namespace PlgToFp.Windows.Infrastructure.ViewModel
{
    /// <summary>
    /// A base class for viewmodels
    /// </summary>
    public abstract class ViewModel : BindableBase, INavigationAware, INotifyDataErrorInfo
    {
        protected ErrorsContainer<ValidationResult> ErrorsContainer;

        protected ViewModel()
        {
            ErrorsContainer = new ErrorsContainer<ValidationResult>(RaiseErrorsChanged);
        }

        #region INavigationAware
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        #endregion

        #region INotifyDataErrorInfo
        public virtual IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        public bool HasErrors => ErrorsContainer.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            OnPropertyChanged(() => HasErrors);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void RaiseErrorsChanged<T>(Expression<Func<T>> propertyExpression)
        {
            RaiseErrorsChanged(PropertySupport.ExtractPropertyName(propertyExpression));
        }
        #endregion
    }
}
