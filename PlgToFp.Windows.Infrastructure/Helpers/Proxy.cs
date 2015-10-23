using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlgToFp.Windows.Infrastructure.Helpers
{
    /// <summary>
    /// A proxy class for holding an instance of an object in a dependency property
    /// </summary>
    public class Proxy : DependencyObject 
    {

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(Proxy), new PropertyMetadata(null));

    }
}
