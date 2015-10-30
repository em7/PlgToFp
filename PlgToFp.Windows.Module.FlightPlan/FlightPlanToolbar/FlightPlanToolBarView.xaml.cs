﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlanToolbar
{
    /// <summary>
    /// Interaction logic for FlightPlanToolBar.xaml
    /// </summary>
    public partial class FlightPlanToolbarView : UserControl
    {
        public FlightPlanToolbarView(FlightPlanToolBarViewModel viewmodel)
        {
            
            InitializeComponent();
            this.DataContext = viewmodel;
        }
    }
}
