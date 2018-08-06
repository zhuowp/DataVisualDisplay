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
    public class LineChartViewModel : ViewModelBase
    {
        #region Fields

        private System.Timers.Timer _timer = null;
        private ObservableCollection<DataPoint> _lineDataPointCollection = null;

        #endregion

        #region Properties

        public ObservableCollection<DataPoint> LineDataPointCollection
        {
            get { return _lineDataPointCollection; }
            set { _lineDataPointCollection = value; RaisePropertyChanged("LineDataPointCollection"); }
        }

        #endregion

        #region Constructors

        #endregion

        #region Private Methods

        #endregion

    }
}
