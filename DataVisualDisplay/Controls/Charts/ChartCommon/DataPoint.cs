using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DataVisualDisplay.Controls
{
    public class DataPoint : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Fields

        private double _beginValue = 0;
        private double _endValue = 0;
        private double _minValue = 0;
        private double _maxValue = 0;
        private double _value = 0;
        private object _label = null;
        private Brush _dataPointForeground = null;
        private bool _isSelected = false;

        #endregion

        #region Properties

        public double BeginValue
        {
            get
            {
                return _beginValue;
            }

            set
            {
                _beginValue = value; RaisePropertyChanged("BeginValue");
            }
        }

        public double EndValue
        {
            get
            {
                return _endValue;
            }

            set
            {
                _endValue = value; RaisePropertyChanged("EndValue");
            }
        }

        public double MinValue
        {
            get
            {
                return _minValue;
            }

            set
            {
                _minValue = value; RaisePropertyChanged("MinValue");
            }
        }

        public double MaxValue
        {
            get
            {
                return _maxValue;
            }

            set
            {
                _maxValue = value; RaisePropertyChanged("MaxValue");
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value; RaisePropertyChanged("Value");
            }
        }

        public object Label
        {
            get
            {
                return _label;
            }

            set
            {
                _label = value; RaisePropertyChanged("Label");
            }
        }

        public Brush DataPointForeground
        {
            get
            {
                return _dataPointForeground;
            }

            set
            {
                _dataPointForeground = value; RaisePropertyChanged("DataPointForeground");
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value; RaisePropertyChanged("IsSelected");
            }
        }

        #endregion
    }
}
