using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlgToFp.Windows.Module.App.AppToolbar
{
    public class AppRightMenuViewModel: BindableBase 
    {
        public DelegateCommand HelloCommand { get; private set; }

        public AppRightMenuViewModel()
        {
            HelloCommand = new DelegateCommand(() => MessageBox.Show("Hello World"));

        }
    }
}
