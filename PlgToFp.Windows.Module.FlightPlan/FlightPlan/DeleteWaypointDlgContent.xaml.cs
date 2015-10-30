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

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
{
    /// <summary>
    /// Interaction logic for DeleteWaypointDlgContent.xaml
    /// </summary>
    public partial class DeleteWaypointDlgContent : UserControl
    {
        public DeleteWaypointDlgContent(DeleteWaypointDlgViewModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
        }
    }
}
