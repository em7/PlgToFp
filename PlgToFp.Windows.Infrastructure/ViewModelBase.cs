using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlgToFp.Windows.Infrastructure
{
    /// <summary>
    /// A base for all viewmodels
    /// </summary>
    public class ViewModelBase : BindableBase 
    {
        /// <summary>
        /// True if the code is executed by VS or Blend designer
        /// </summary>
        public bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor
                    .FromProperty(prop, typeof(FrameworkElement))
                    .Metadata.DefaultValue;
            }
        }
    }
}
