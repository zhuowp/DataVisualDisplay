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
    /// LineChartView.xaml 的交互逻辑
    /// </summary>
    public partial class LineChartView : UserControl
    {
        #region Fields

        private LineChartViewModel _viewModel = null;

        #endregion

        #region Properties 

        #endregion

        #region Constructors

        public LineChartView()
        {
            InitializeComponent();

            _viewModel = new LineChartViewModel();
            DataContext = _viewModel;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
