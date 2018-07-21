using DataVisualDisplayDemo.Helper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DataVisualDisplayDemo.ViewModel
{
    public class LedDigitalViewModel : ViewModelBase
    {
        #region Fields

        private string _singleDigitalValue = "";
        private string _multiDigitalValue = "";
        private Brush _multiDigitalBrush = null;

        #endregion

        #region Properties

        public string SingleDigitalValue
        {
            get { return _singleDigitalValue; }
            set { _singleDigitalValue = value; RaisePropertyChanged("SingleDigitalValue"); }
        }

        public string MultiDigitalValue
        {
            get
            {
                return _multiDigitalValue;
            }

            set
            {
                _multiDigitalValue = value; RaisePropertyChanged("MultiDigitalValue");
            }
        }

        public Brush MultiDigitalBrush
        {
            get
            {
                return _multiDigitalBrush;
            }

            set
            {
                _multiDigitalBrush = value; RaisePropertyChanged("MultiDigitalBrush");
            }
        }

        #endregion

        #region Constructors

        public LedDigitalViewModel()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        #endregion

        #region Private Methods

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DispatcherHelper.UIDispatcher.Invoke(() =>
            {
                Random r = new Random();
                SingleDigitalValue = r.Next(0, 9).ToString();
                MultiDigitalValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                MultiDigitalBrush = new SolidColorBrush(ColorHelper.GetRandomColor());
            });
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
