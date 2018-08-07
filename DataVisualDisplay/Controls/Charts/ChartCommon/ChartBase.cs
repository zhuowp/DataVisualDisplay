using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataVisualDisplay.Controls
{
    public class ChartBase : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty DataPointsProperty
            = DependencyProperty.Register("DataPoints", typeof(ObservableCollection<DataPoint>), typeof(ChartBase), new PropertyMetadata(null, OnDataPointsPropertyChanged));

        public static readonly DependencyProperty BrushSetProperty
            = DependencyProperty.Register("BrushSet", typeof(DataPointBrushs), typeof(ChartBase), new PropertyMetadata(null, OnBrushSetPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

        public ObservableCollection<DataPoint> DataPoints
        {
            get { return (ObservableCollection<DataPoint>)GetValue(DataPointsProperty); }
            set { SetValue(DataPointsProperty, value); }
        }

        public DataPointBrushs BrushSet
        {
            get { return (DataPointBrushs)GetValue(BrushSetProperty); }
            set { SetValue(BrushSetProperty, value); }
        }

        #endregion

        #region Dependency Property Changed Callbacks

        private static void OnDataPointsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChartBase chart = d as ChartBase;
            if (chart == null)
            {
                return;
            }

            ObservableCollection<DataPoint> oldDataPoints = e.OldValue as ObservableCollection<DataPoint>;
            ObservableCollection<DataPoint> newDataPoints = e.NewValue as ObservableCollection<DataPoint>;

            if (oldDataPoints != null)
            {
                oldDataPoints.CollectionChanged -= chart.DataPoints_CollectionChanged;
            }

            if (newDataPoints != null)
            {
                newDataPoints.CollectionChanged += chart.DataPoints_CollectionChanged;
            }

            chart.UpdateChartDisplay();
        }

        private static void OnBrushSetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChartBase chart = d as ChartBase;
            if (chart != null)
            {
                chart.UpdateChartDisplay();
            }
        }

        #endregion

        #region Virtual Methods

        protected virtual void UpdateChartDisplay()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private void DataPoints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateChartDisplay();
        }

        #endregion
    }
}
