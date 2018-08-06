using DataVisualDisplay.Controls;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualDisplayDemo.ViewModel
{
    public class PieChartViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<DataPoint> _pieChartDataPoints = null;
        private System.Timers.Timer _timer = null;

        #endregion

        #region Properties

        public ObservableCollection<DataPoint> PieChartDataPoints
        {
            get
            {
                return _pieChartDataPoints;
            }

            set
            {
                _pieChartDataPoints = value; RaisePropertyChanged("PieChartDataPoints");
            }
        }

        #endregion

        #region Constructors

        public PieChartViewModel()
        {
            _pieChartDataPoints = new ObservableCollection<DataPoint>()
            {
                new DataPoint() { Value = 5, Label = "Test Data 1" },
                new DataPoint() { Value = 2, Label = "Test Data 2"},
                new DataPoint() { Value=3, Label= "Test Data 3"},
                new DataPoint() { Value=4, Label= "Test Data 4"},
                new DataPoint() { Value=3, Label= "Test Data 5"},
                new DataPoint() { Value=2, Label= "Test Data 6"},
            };

            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed;
            _timer.Interval = 1000;
            _timer.Start();
        }

        #endregion

        #region Private Methods

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();
            int randomIndex = random.Next(_pieChartDataPoints.Count);
            int randomValue = random.Next(1, 10);

            if (_pieChartDataPoints[randomIndex].Value == randomValue)
            {
                _pieChartDataPoints[randomIndex].Value++;
            }
            else
            {
                _pieChartDataPoints[randomIndex].Value = randomValue;
            }
        }

        #endregion
    }
}
