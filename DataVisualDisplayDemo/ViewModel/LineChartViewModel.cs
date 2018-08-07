using DataVisualDisplay.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualDisplayDemo.ViewModel
{
    public class LineChartViewModel : ViewModelBase
    {
        #region Fields

        private System.Timers.Timer _timer = null;
        private ObservableCollection<DataPoint> _lineDataPointCollection = null;
        private int _index = 0;

        #endregion

        #region Properties

        public ObservableCollection<DataPoint> LineDataPointCollection
        {
            get { return _lineDataPointCollection; }
            set { _lineDataPointCollection = value; RaisePropertyChanged("LineDataPointCollection"); }
        }

        #endregion

        #region Constructors

        public LineChartViewModel()
        {
            _lineDataPointCollection = new ObservableCollection<DataPoint>();

            _timer = new System.Timers.Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        #endregion

        #region Private Methods

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DataPoint dataPoint = new DataPoint();
            dataPoint.Label = _index;
            _index++;

            Random random = new Random();
            dataPoint.Value = random.Next(0, 15);

            DispatcherHelper.UIDispatcher.Invoke(() =>
            {
                if (_lineDataPointCollection.Count > 19)
                {
                    _lineDataPointCollection.RemoveAt(0);
                }
                _lineDataPointCollection.Add(dataPoint);
            });
        }

        #endregion

    }
}
