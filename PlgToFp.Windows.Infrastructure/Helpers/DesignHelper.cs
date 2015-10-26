using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlgToFp.Windows.Infrastructure.Helpers
{
    /// <summary>
    /// Helper for design time
    /// </summary>
    public class DesignHelper
    {
        private static MyDependencyObject _myDo;

        /// <summary>
        /// True if the code is run in the designer, false otherwise
        /// </summary>
        public static bool IsInDesignMode()
        {
            if (_myDo == null)
                _myDo = new MyDependencyObject();

            return DesignerProperties.GetIsInDesignMode(_myDo);
        }

        private class MyDependencyObject : DependencyObject { }
    }
}
