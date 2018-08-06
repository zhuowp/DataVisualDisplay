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
using System.Windows.Media.Animation;
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
        private List<PiePiece> _piePieceList = new List<PiePiece>();

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty DataPointsProperty
            = DependencyProperty.Register("DataPoints", typeof(ObservableCollection<DataPoint>), typeof(PieChart), new PropertyMetadata(null, OnDataPointsPropertyChanged));

        public static readonly DependencyProperty IsStepwisePiePieceProperty
            = DependencyProperty.Register("IsStepwisePiePiece", typeof(bool), typeof(PieChart), new PropertyMetadata(false));

        public static readonly DependencyProperty BrushSetProperty
            = DependencyProperty.Register("BrushSet", typeof(DataPointBrushs), typeof(PieChart), new PropertyMetadata(null, OnColorSetPropertyChanged));

        public static readonly DependencyProperty RadiusProperty
            = DependencyProperty.Register("Radius", typeof(double), typeof(PieChart), new PropertyMetadata(100.0, OnRadiusPropertyChanged));

        public static readonly DependencyProperty InnerRadiusProperty
            = DependencyProperty.Register("InnerRadius", typeof(double), typeof(PieChart), new PropertyMetadata(0.0, OnRadiusPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

        public ObservableCollection<DataPoint> DataPoints
        {
            get { return (ObservableCollection<DataPoint>)GetValue(DataPointsProperty); }
            set { SetValue(DataPointsProperty, value); }
        }

        public bool IsStepwisePiePiece
        {
            get { return (bool)GetValue(IsStepwisePiePieceProperty); }
            set { SetValue(IsStepwisePiePieceProperty, value); }
        }

        public DataPointBrushs BrushSet
        {
            get { return (DataPointBrushs)GetValue(BrushSetProperty); }
            set { SetValue(BrushSetProperty, value); }
        }

        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public double InnerRadius
        {
            get { return (double)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
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
            AnimationDisplay();
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

            pieChart.ConstructPiePieces();
        }

        private static void OnColorSetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PieChart pieChart = d as PieChart;
            if (pieChart == null)
            {
                return;
            }

            pieChart.ConstructPiePieces();
        }

        private static void OnRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PieChart pieChart = d as PieChart;
            if (pieChart == null)
            {
                return;
            }

            pieChart.ConstructPiePieces();
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

            double pieRadius = Radius;
            if (pieRadius <= 0)
            {
                return;
            }
            
            double total = 0;
            foreach (DataPoint dataPoint in DataPoints)
            {
                total += dataPoint.Value;
            }

            _piePiecePanel.Children.Clear();
            _piePieceList.Clear();

            double accumulativeAngle = 0;
            for (int i = 0; i < DataPoints.Count; i++)
            {
                double percentage = DataPoints[i].Value / total;
                double wedgeAngle = percentage * 360;

                double radius = pieRadius;
                if (IsStepwisePiePiece)
                {
                    radius = (pieRadius - InnerRadius) / DataPoints.Count / 2 * (i + 1) + (InnerRadius + pieRadius) / 2;
                }

                if (DataPoints[i].DataPointForeground == null && BrushSet != null && BrushSet.Count > 0)
                {
                    DataPoints[i].DataPointForeground = BrushSet[i % BrushSet.Count];
                }

                PiePiece piece = new PiePiece()
                {
                    Radius = radius,
                    Percentage = percentage,
                    InnerRadius = InnerRadius,
                    CenterX = pieRadius + 10,
                    CenterY = pieRadius + 10,
                    ExtractLength = 0,
                    Value = DataPoints[i].Value,
                    RotateAngle = accumulativeAngle,
                    Fill = DataPoints[i].DataPointForeground,
                    Tag = DataPoints[i],
                };

                Binding valueBinding = new Binding()
                {
                    Source = DataPoints[i],
                    Path = new PropertyPath("Value"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                BindingOperations.SetBinding(piece, PiePiece.ValueProperty, valueBinding);

                Binding binding = new Binding()
                {
                    Source = DataPoints[i],
                    Path = new PropertyPath("IsSelected"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged

                };
                BindingOperations.SetBinding(piece, PiePiece.IsSelectedProperty, binding);

                ToolTip toolTip = new ToolTip();
                toolTip.Content = string.Format("{0} - {1} - {2}%", DataPoints[i].Label.ToString(), DataPoints[i].Value, Math.Round(percentage * 100, 2));
                piece.ToolTip = toolTip;

                piece.OnValueChangedAction = new Action(ConstructPiePieces);
                piece.ToolTipOpening += new ToolTipEventHandler(PiePieceToolTipOpening);
                piece.MouseUp += new MouseButtonEventHandler(PiePieceMouseUp);

                _piePieceList.Add(piece);
                _piePiecePanel.Children.Insert(0, piece);
                accumulativeAngle += wedgeAngle;
            }
        }

        private void PiePieceToolTipOpening(object sender, ToolTipEventArgs e)
        {
        }

        private void PiePieceMouseUp(object sender, MouseButtonEventArgs e)
        {
            PiePiece piece = sender as PiePiece;

            bool newValue = !piece.IsSelected;

            foreach (PiePiece piePiece in _piePieceList)
            {
                piePiece.IsSelected = false;
            }

            piece.IsSelected = newValue;
        }

        #endregion

        #region Public Methods

        public void AnimationDisplay()
        {
            if (_piePieceList == null)
            {
                return;
            }

            DoubleAnimation positionAnimation = new DoubleAnimation();
            positionAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            positionAnimation.From = 40;
            positionAnimation.To = 0;

            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            opacityAnimation.From = 0;
            opacityAnimation.To = 1;

            foreach (PiePiece piece in _piePieceList)
            {
                piece.BeginAnimation(PiePiece.ExtractLengthProperty, positionAnimation);
                piece.BeginAnimation(OpacityProperty, opacityAnimation);
            }
        }

        #endregion
    }
}
