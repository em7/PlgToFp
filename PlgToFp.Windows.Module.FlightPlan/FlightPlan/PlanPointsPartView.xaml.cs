using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PlanPointsView.xaml
    /// </summary>
    public partial class PlanPointsPartView : UserControl
    {
        public PlanPointsPartView()
        {
            this.DataContextChanged += PlanPointsPartView_DataContextChanged;
            InitializeComponent();
        }

        private void PlanPointsPartView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // TODO trigger?
            var dialog = (BaseMetroDialog) Resources["CustomCloseDialogTest"];
            dialog.DataContext = DataContext;
        }



        #region Waypoint drag and drop
        private Point _startPoint;
        private DragAdorner _adorner;
        private AdornerLayer _layer;
        private bool _dragIsOutOfScope;
        private static readonly string WAYPOINT_DATA_FORMAT = "WaypointListViewItem";

        private void MoveHandlePreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(WaypointsListView);
        }

        private void MoveHandlePreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = e.GetPosition(null);
                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    BeginDrag(e);
                }
            }
        }

        private void ListViewDragOver(object sender, DragEventArgs e)
        {
            if (_adorner != null)
            {
                _adorner.OffsetLeft = e.GetPosition(WaypointsListView).X;
                _adorner.OffsetTop = e.GetPosition(WaypointsListView).Y - _startPoint.Y;
            }
        }

        private void ListViewDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(WAYPOINT_DATA_FORMAT) ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        void ListViewDragLeave(object sender, DragEventArgs e)
        {
            if (e.OriginalSource == WaypointsListView)
            {
                Point p = e.GetPosition(WaypointsListView);
                Rect r = VisualTreeHelper.GetContentBounds(WaypointsListView);
                if (!r.Contains(p))
                {
                    _dragIsOutOfScope = true;
                    e.Handled = true;
                }
            }
        }

        private void ListViewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(WAYPOINT_DATA_FORMAT))
            {
                var droppedItem = e.Data.GetData(WAYPOINT_DATA_FORMAT);
                var itemDroppedOn = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                var viewmodel = ((PlanPointsPartViewModel)this.DataContext).ParentFlightPlan;

                if (itemDroppedOn != null)
                {
                    var itemToReplace = WaypointsListView.ItemContainerGenerator.ItemFromContainer(itemDroppedOn);

                    viewmodel.RearrangeWaypoints(droppedItem, itemToReplace);
                    

                    //var idx = WaypointsListView.Items.IndexOf(itemToReplace);
                    //var collection = ()WaypointsListView.ItemsSource;
                    //var idx = collection.IndexOf(itemToReplace);

                    //if (idx >= 0)
                    //{
                    //    WaypointsListView.Items.Remove(droppedItem);
                    //    WaypointsListView.Items.Insert(idx, droppedItem);
                    //}
                }
                else
                {
                    viewmodel.RearrangeWaypoints(droppedItem, null);
                    //WaypointsListView.Items.Remove(droppedItem);
                    //WaypointsListView.Items.Add(droppedItem);
                }
            }
        }

        private void BeginDrag(MouseEventArgs e)
        {
            ListView listView = this.WaypointsListView;
            ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject) e.OriginalSource);

            if (listViewItem == null)
                return;

            var item = listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

            InitializeAdorner(listViewItem);

            listView.PreviewDragOver += ListViewDragOver;
            listView.DragLeave += ListViewDragLeave;
            listView.DragEnter += ListViewDragEnter;

            DataObject data = new DataObject(WAYPOINT_DATA_FORMAT, item);
            DragDropEffects de = DragDrop.DoDragDrop(listView, data, DragDropEffects.Move);

            listView.PreviewDragOver -= ListViewDragOver;
            listView.DragLeave -= ListViewDragLeave;
            listView.DragEnter -= ListViewDragEnter;

            if (_adorner != null)
            {
                AdornerLayer.GetAdornerLayer(listView).Remove(_adorner);
                _adorner = null;
            }
        }

        private void InitializeAdorner(ListViewItem listViewItem)
        {
            var brush = new VisualBrush(listViewItem);
            _adorner = new DragAdorner((UIElement)listViewItem, listViewItem.RenderSize, brush);
            _adorner.Opacity = 0.5;
            _layer = AdornerLayer.GetAdornerLayer(WaypointsListView as Visual);
            _layer.Add(_adorner);
        }

        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        #endregion
    }
}
