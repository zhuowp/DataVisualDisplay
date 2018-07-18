using DataVisualDisplayDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// LedDigitalView.xaml 的交互逻辑
    /// </summary>
    public partial class LedDigitalView : UserControl
    {
        #region Fields

        private LedDigitalViewModel _viewModel = null;

        #endregion

        #region Constructors

        public LedDigitalView()
        {
            InitializeComponent();

            _viewModel = new LedDigitalViewModel();
            DataContext = _viewModel;
        }

        #endregion
    }
}
