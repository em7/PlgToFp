using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Controls
{
    /// <summary>
    /// Shows the flight plan waypoints as a map relative to each other
    /// </summary>
    public class FlightPlanMap : Canvas
    {
        static FlightPlanMap()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlightPlanMap), new FrameworkPropertyMetadata(typeof(FlightPlanMap)));
        }

        public ObservableCollection<WaypointModel> MapPoints
        {
            get { return (ObservableCollection<WaypointModel>)GetValue(MapPointsProperty); }
            set
            {
                SetValue(MapPointsProperty, value);
            }
        }

        public static readonly DependencyProperty MapPointsProperty =
            DependencyProperty.Register("MapPoints", typeof(ObservableCollection<WaypointModel>), typeof(FlightPlanMap),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, MapPointsChangedCallback));

        private static void MapPointsChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var flightPlanMap = obj as FlightPlanMap;
            if (flightPlanMap == null)
                return;

            if (e.NewValue != null)
                flightPlanMap.MapPoints.CollectionChanged += flightPlanMap.MapPoints_CollectionChanged;

            if (e.OldValue != null)
                flightPlanMap.MapPoints.CollectionChanged -= flightPlanMap.MapPoints_CollectionChanged;
        }

        private void MapPoints_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InvalidateVisual();
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            var backgroundRect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
            drawingContext.DrawRectangle(Brushes.Black,
                null,
                backgroundRect);

            if (MapPoints == null)
                return;

            var minX = MapPoints.Min(p => p.Longitude);
            var maxX = MapPoints.Max(p => p.Longitude);
            var minY = MapPoints.Min(p => p.Latitude);
            var maxY = MapPoints.Max(p => p.Latitude);
            var mDistX = maxX - minX; //the maximum X distance between points
            var mDistY = maxY - minY;

            Point? prevPoint = null;

            foreach (var point in MapPoints)
            {
                //calc coordinates for this point
                var x = ((point.Longitude - minX) / mDistX) * ActualWidth;
                var y = ActualHeight - (((point.Latitude - minY) / mDistY) * ActualHeight);

                //draw its marker
                drawingContext.DrawRectangle(Brushes.Magenta,
                    null, new Rect(x - 3, y - 3, 6, 6));

                //draw line to previous if able
                var thisPoint = new Point(x, y);
                if (prevPoint.HasValue)
                {
                    drawingContext.DrawLine(new Pen(Brushes.Magenta, 2d),
                        thisPoint, 
                        prevPoint.Value);
                }

                //set this as previous
                prevPoint = thisPoint;
            }

            //base.OnRender(drawingContext);
            //var formText = new FormattedText(string.Format("Points count: {0}", MapPoints.Count),
            //    CultureInfo.InvariantCulture,
            //    FlowDirection.LeftToRight,
            //    new Typeface("Verdana"),
            //    24,
            //    Brushes.Magenta);
            //drawingContext.DrawText(formText, new Point(0, 0));
        }
    }
}
