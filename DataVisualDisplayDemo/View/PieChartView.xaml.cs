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
    /// ChartView.xaml 的交互逻辑
    /// </summary>
    public partial class PieChartView : UserControl
    {
        #region Fields

        private PieChartViewModel _viewModel = null;

        #endregion

        #region Constructors

        public PieChartView()
        {
            InitializeComponent();

            _viewModel = new PieChartViewModel();
            DataContext = _viewModel;
        }

        #endregion

        private void btnAnimation_Click(object sender, RoutedEventArgs e)
        {
            pieChart.AnimationDisplay();
        }
    }
}
