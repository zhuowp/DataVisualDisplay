using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataVisualDisplayDemo.ViewModel
{
    public class CircularGaugeViewModel : ViewModelBase
    {
        #region Fields

        private double _currentValue = 0;
        private double _hour = 0;
        private double _minute = 0;
        private double _second = 0;

        #endregion

        #region Properties

        public double CurrentValue
        {
            get
            {
                return _currentValue;
            }

            set
            {
                _currentValue = value; RaisePropertyChanged("CurrentValue");
            }
        }

        public double Hour
        {
            get
            {
                return _hour;
            }

            set
            {
                _hour = value; RaisePropertyChanged("Hour");
            }
        }

        public double Minute
        {
            get
            {
                return _minute;
            }

            set
            {
                _minute = value; RaisePropertyChanged("Minute");
            }
        }

        public double Second
        {
            get
            {
                return _second;
            }

            set
            {
                _second = value; RaisePropertyChanged("Second");
            }
        }

        #endregion

        #region Constructors

        public CircularGaugeViewModel()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        #endregion

        #region Event Methods

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DispatcherHelper.UIDispatcher.Invoke(() =>
            {
                Random r = new Random();
                CurrentValue = r.NextDouble();

                DateTime currentDateTime = DateTime.Now;
                Second = currentDateTime.Second;
                Minute = currentDateTime.Minute + Second / 60.0;
                Hour = (currentDateTime.Hour + Minute / 60.0) % 12;
            });
        }

        #endregion
    }
}
