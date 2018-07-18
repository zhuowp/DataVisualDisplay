using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualDisplayDemo.ViewModel
{
    public class LedDigitalViewModel : ViewModelBase
    {
        #region Fields

        private string _singleDigitalValue = "";

        #endregion

        #region Properties

        public string SingleDigitalValue
        {
            get { return _singleDigitalValue; }
            set { _singleDigitalValue = value; RaisePropertyChanged("SingleDigitalValue"); }
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
            });
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
