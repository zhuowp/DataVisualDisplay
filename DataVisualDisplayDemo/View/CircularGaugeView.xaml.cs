using DataVisualDisplayDemo.ViewModel;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataVisualDisplayDemo.View
{
    /// <summary>
    /// CircularGaugeView.xaml 的交互逻辑
    /// </summary>
    public partial class CircularGaugeView : UserControl
    {
        #region Fields

        private CircularGaugeViewModel _viewModel = null;

        #endregion

        #region Constructors

        public CircularGaugeView()
        {
            InitializeComponent();

            _viewModel = new CircularGaugeViewModel();
            DataContext = _viewModel;
        }

        #endregion
    }
}
