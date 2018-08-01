using DataVisualDisplay.Models.Charts;
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
                new DataPoint() { Value = 1, Label = "Test Data 1" },
                new DataPoint(){Value = 2, Label = "Test Data 2"},
                new DataPoint(){Value=3, Label= "Test Data 3"}
            };
        }

        #endregion
    }
}
