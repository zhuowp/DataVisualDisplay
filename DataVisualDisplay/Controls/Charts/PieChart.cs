using DataVisualDisplay.Models.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataVisualDisplay.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:DataVisualDisplay.Controls.Charts"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:DataVisualDisplay.Controls.Charts;assembly=DataVisualDisplay.Controls.Charts"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:PieChart/>
    ///
    /// </summary>
    public class PieChart : Control
    {
        #region Fields

        private Panel _piePiecePanel = null;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty DataPointsProperty
            = DependencyProperty.Register("DataPoints", typeof(ObservableCollection<DataPoint>), typeof(PieChart), new PropertyMetadata(null, OnDataPointsPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

        public ObservableCollection<DataPoint> DataPoints
        {
            get { return (ObservableCollection<DataPoint>)GetValue(DataPointsProperty); }
            set { SetValue(DataPointsProperty, value); }
        }

        #endregion

        #region Constructors

        static PieChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PieChart), new FrameworkPropertyMetadata(typeof(PieChart)));
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _piePiecePanel = GetTemplateChild("PART_PiePiecePanel") as Panel;

            ConstructPiePieces();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "ActualWidth" || e.Property.Name == "ActualHeight")
            {
                ConstructPiePieces();
            }
        }

        #endregion

        #region Dependency Property Changed Callback

        private static void OnDataPointsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PieChart pieChart = d as PieChart;
            if (pieChart == null)
            {
                return;
            }

            ObservableCollection<DataPoint> oldDataPoints = e.OldValue as ObservableCollection<DataPoint>;
            ObservableCollection<DataPoint> newDataPoints = e.NewValue as ObservableCollection<DataPoint>;

            if (oldDataPoints != null)
            {
                oldDataPoints.CollectionChanged -= pieChart.DataPoints_CollectionChanged;
            }

            if (newDataPoints != null)
            {
                newDataPoints.CollectionChanged += pieChart.DataPoints_CollectionChanged;
            }
        }

        #endregion

        #region Event Methods

        private void DataPoints_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ConstructPiePieces();
        }

        #endregion

        #region Private Methods

        private void ConstructPiePieces()
        {
            if (_piePiecePanel == null)
            {
                return;
            }

            double halfWidth = Math.Min(_piePiecePanel.ActualHeight, _piePiecePanel.ActualWidth) / 2;
            double innerRadius = halfWidth * 0.3;

            double total = 0;
            foreach (DataPoint dataPoint in DataPoints)
            {
                total += dataPoint.Value;
            }

            _piePiecePanel.Children.Clear();
            //piePieces.Clear();

            double accumulativeAngle = 0;
            foreach (DataPoint dataPoint in DataPoints)
            {
                double percentage = dataPoint.Value / total;
                double wedgeAngle = percentage * 360;

                PiePiece piece = new PiePiece()
                {
                    Radius = halfWidth,
                    Percentage = percentage,
                    InnerRadius = innerRadius,
                    CenterX = halfWidth,
                    CenterY = halfWidth,
                    ExtractLength = 0,
                    Value = dataPoint.Value,
                    RotateAngle = accumulativeAngle,
                    Fill = Brushes.Black,

                    // record the index of the item which this pie slice represents
                    //Tag = myCollectionView.IndexOf(item),
                    ToolTip = new ToolTip()
                };

                //piece.ToolTipOpening += new ToolTipEventHandler(PiePieceToolTipOpening);
                //piece.MouseUp += new MouseButtonEventHandler(PiePieceMouseUp);

                //piePieces.Add(piece);
                _piePiecePanel.Children.Insert(0, piece);
                accumulativeAngle += wedgeAngle;
            }
        }

        #endregion
    }
}
