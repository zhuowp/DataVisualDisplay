using DataVisualDisplay.Helpers;
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

namespace DataVisualDisplay.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:DataVisualDisplay.Controls.Gauges"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:DataVisualDisplay.Controls.Gauges;assembly=DataVisualDisplay.Controls.Gauges"
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
    ///     <MyNamespace:GaugeScale/>
    ///
    /// </summary>
    public class GaugeScale : Control
    {
        #region Fields

        private Panel _scaleContainer = null;

        #endregion

        #region Properties

        public Panel ScaleContainer
        {
            get
            {
                return _scaleContainer;
            }

            set
            {
                _scaleContainer = value;
            }
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ScaleRadiusProperty
            = DependencyProperty.Register("ScaleRadius", typeof(double), typeof(GaugeScale), new PropertyMetadata(110.0));

        public static readonly DependencyProperty ScaleStartAngleProperty
            = DependencyProperty.Register("ScaleStartAngle", typeof(double), typeof(GaugeScale), new PropertyMetadata(120.0));

        public static readonly DependencyProperty ScaleSweepAngleProperty
            = DependencyProperty.Register("ScaleSweepAngle", typeof(double), typeof(GaugeScale), new PropertyMetadata(300.0));

        public static readonly DependencyProperty ScaleLabelRadiusProperty
            = DependencyProperty.Register("ScaleLabelRadius", typeof(double), typeof(GaugeScale), new PropertyMetadata(90.0));

        public static readonly DependencyProperty ScaleLabelSizeProperty
            = DependencyProperty.Register("ScaleLabelSize", typeof(Size), typeof(GaugeScale), new PropertyMetadata(new Size(40, 20)));

        public static readonly DependencyProperty ScaleLabelFontSizeProperty
            = DependencyProperty.Register("ScaleLabelFontSize", typeof(double), typeof(GaugeScale), new PropertyMetadata(10.0));

        public static readonly DependencyProperty ScaleLabelForegroundProperty
            = DependencyProperty.Register("ScaleLabelForeground", typeof(Brush), typeof(GaugeScale), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty ScaleValuePrecisionProperty
            = DependencyProperty.Register("ScaleValuePrecision", typeof(int), typeof(GaugeScale), new PropertyMetadata(5));

        public static readonly DependencyProperty MajorDivisionsCountProperty
            = DependencyProperty.Register("MajorDivisionsCount", typeof(int), typeof(GaugeScale), new PropertyMetadata(10));

        public static readonly DependencyProperty MajorTickSizeProperty
            = DependencyProperty.Register("MajorTickSize", typeof(Size), typeof(GaugeScale), new PropertyMetadata(new Size(10, 3)));

        public static readonly DependencyProperty MajorTickForegroundProperty
            = DependencyProperty.Register("MajorTickForeground", typeof(Brush), typeof(GaugeScale), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty MinorDivisionsCountProperty
            = DependencyProperty.Register("MinorDivisionsCount", typeof(int), typeof(GaugeScale), new PropertyMetadata(5));

        public static readonly DependencyProperty MinorTickSizeProperty
            = DependencyProperty.Register("MinorTickSize", typeof(Size), typeof(GaugeScale), new PropertyMetadata(new Size(3, 1)));

        public static readonly DependencyProperty MinorTickForegroundProperty
            = DependencyProperty.Register("MinorTickForeground", typeof(Brush), typeof(GaugeScale), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty MinValueProperty
            = DependencyProperty.Register("MinValue", typeof(double), typeof(GaugeScale), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaxValueProperty
            = DependencyProperty.Register("MaxValue", typeof(double), typeof(GaugeScale), new PropertyMetadata(1.0));

        public static readonly DependencyProperty IsDrawStartTickProperty
            = DependencyProperty.Register("IsDrawStartTick", typeof(bool), typeof(GaugeScale), new PropertyMetadata(true));
        
        #endregion

        #region Dependency Property Wrappers

        public double ScaleRadius
        {
            get
            {
                return (double)GetValue(ScaleRadiusProperty);
            }
            set
            {
                SetValue(ScaleRadiusProperty, value);
            }
        }

        public double ScaleStartAngle
        {
            get
            {
                return (double)GetValue(ScaleStartAngleProperty);
            }
            set
            {
                SetValue(ScaleStartAngleProperty, value);
            }
        }

        public double ScaleSweepAngle
        {
            get
            {
                return (double)GetValue(ScaleSweepAngleProperty);
            }
            set
            {
                SetValue(ScaleSweepAngleProperty, value);
            }
        }

        public double ScaleLabelRadius
        {
            get
            {
                return (double)GetValue(ScaleLabelRadiusProperty);
            }
            set
            {
                SetValue(ScaleLabelRadiusProperty, value);
            }
        }

        public Size ScaleLabelSize
        {
            get
            {
                return (Size)GetValue(ScaleLabelSizeProperty);
            }
            set
            {
                SetValue(ScaleLabelSizeProperty, value);
            }
        }

        public double ScaleLabelFontSize
        {
            get
            {
                return (double)GetValue(ScaleLabelFontSizeProperty);
            }
            set
            {
                SetValue(ScaleLabelFontSizeProperty, value);
            }
        }

        public Brush ScaleLabelForeground
        {
            get
            {
                return (Brush)GetValue(ScaleLabelForegroundProperty);
            }
            set
            {
                SetValue(ScaleLabelForegroundProperty, value);
            }
        }

        public int ScaleValuePrecision
        {
            get
            {
                return (int)GetValue(ScaleValuePrecisionProperty);
            }
            set
            {
                SetValue(ScaleValuePrecisionProperty, value);
            }
        }

        public Size MajorTickSize
        {
            get
            {
                return (Size)GetValue(MajorTickSizeProperty);
            }
            set
            {
                SetValue(MajorTickSizeProperty, value);
            }
        }

        public int MajorDivisionsCount
        {
            get
            {
                return (int)GetValue(MajorDivisionsCountProperty);
            }
            set
            {
                SetValue(MajorDivisionsCountProperty, value);
            }
        }

        public Size MinorTickSize
        {
            get
            {
                return (Size)GetValue(MinorTickSizeProperty);
            }
            set
            {
                SetValue(MinorTickSizeProperty, value);
            }
        }

        public Brush MajorTickForeground
        {
            get
            {
                return (Brush)GetValue(MajorTickForegroundProperty);
            }
            set
            {
                SetValue(MajorTickForegroundProperty, value);
            }
        }

        public Brush MinorTickForeground
        {
            get
            {
                return (Brush)GetValue(MinorTickForegroundProperty);
            }
            set
            {
                SetValue(MinorTickForegroundProperty, value);
            }
        }

        public int MinorDivisionsCount
        {
            get
            {
                return (int)GetValue(MinorDivisionsCountProperty);
            }
            set
            {
                SetValue(MinorDivisionsCountProperty, value);
            }
        }

        public double MinValue
        {
            get
            {
                return (double)GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        public double MaxValue
        {
            get
            {
                return (double)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        public bool IsDrawStartTick
        {
            get { return (bool)GetValue(IsDrawStartTickProperty); }
            set { SetValue(IsDrawStartTickProperty, value); }
        }
        
        #endregion

        #region Constructors

        static GaugeScale()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GaugeScale), new FrameworkPropertyMetadata(typeof(GaugeScale)));
        }

        #endregion

        #region Private Methods

        private void AddMajorTick(double majorTickAngle)
        {
            double majorTickRadian = majorTickAngle * Math.PI / 180;
            string majorTickPathString = string.Format("M 0,0 L{0},0 L{1},{2} L0,{3}Z", MajorTickSize.Width, MajorTickSize.Width, MajorTickSize.Height, MajorTickSize.Height);

            Path majorTickPath = GraphicsPloter.DrawGeometry(majorTickPathString);
            majorTickPath.Fill = MajorTickForeground;

            AddTick(majorTickPath, majorTickAngle);
        }

        private void AddMinorTick(double minorTickAngle)
        {
            string minorTickPathString = string.Format("M 0,0 L{0},0 L{1},{2} L0,{3}Z", MinorTickSize.Width, MinorTickSize.Width, MinorTickSize.Height, MinorTickSize.Height);
            Path minorTickPath = GraphicsPloter.DrawGeometry(minorTickPathString);
            minorTickPath.Fill = MinorTickForeground;

            AddTick(minorTickPath, minorTickAngle);
        }

        private void AddScaleLable(double tickAngle, double tickValue)
        {
            double tickRadian = tickAngle * Math.PI / 180;

            TextBlock tb = new TextBlock();
            tb.Height = ScaleLabelSize.Height;
            tb.Width = ScaleLabelSize.Width;
            tb.FontSize = ScaleLabelFontSize;
            tb.Foreground = ScaleLabelForeground;
            tb.TextAlignment = TextAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.HorizontalAlignment = HorizontalAlignment.Center;

            if (Math.Round(tickValue, ScaleValuePrecision) <= Math.Round(MaxValue, ScaleValuePrecision))
            {
                tickValue = Math.Round(tickValue, ScaleValuePrecision);
                tb.Text = tickValue.ToString();
            }

            TranslateTransform majorscalevaluett = new TranslateTransform();
            majorscalevaluett.X = ScaleLabelRadius * Math.Cos(tickRadian);
            majorscalevaluett.Y = ScaleLabelRadius * Math.Sin(tickRadian);
            tb.RenderTransform = majorscalevaluett;

            _scaleContainer.Children.Add(tb);
        }

        private void AddTick(Path path, double rotateAngle)
        {
            Point renderTransformPoint = new Point(0.5, 0.5);
            path.RenderTransformOrigin = renderTransformPoint;
            path.HorizontalAlignment = HorizontalAlignment.Center;
            path.VerticalAlignment = VerticalAlignment.Center;

            TransformGroup transformGroup = new TransformGroup();

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = rotateAngle;
            transformGroup.Children.Add(rotateTransform);

            double majorTickRadian = rotateAngle * Math.PI / 180;
            TranslateTransform translateTransform = new TranslateTransform();
            translateTransform.X = ScaleRadius * Math.Cos(majorTickRadian);
            translateTransform.Y = ScaleRadius * Math.Sin(majorTickRadian);
            transformGroup.Children.Add(translateTransform);

            path.RenderTransform = transformGroup;

            _scaleContainer.Children.Add(path);
        }

        #endregion

        #region Public Methods

        public void DrawScale()
        {
            if (_scaleContainer == null)
            {
                return;
            }

            //主刻度单元格角度
            double majorTickUnitAngle = ScaleSweepAngle / MajorDivisionsCount;
            //主刻度单元格值
            double majorTickUnitValue = Math.Round((MaxValue - MinValue) / MajorDivisionsCount, ScaleValuePrecision);

            //副刻度单元格角度
            double minorTickUinitAngle = majorTickUnitAngle / MinorDivisionsCount;
            //副刻度单元格值
            double minorTickUnitValue = Math.Round((MaxValue - MinValue) / MajorDivisionsCount / MinorDivisionsCount, ScaleValuePrecision);
            
            if (IsDrawStartTick)
            {
                AddMajorTick(ScaleStartAngle);
                AddScaleLable(ScaleStartAngle, MinValue);
            }
            
            for (int i = 1; i < MajorDivisionsCount + 1; i++)
            {
                double majorTickAngle = ScaleStartAngle + i * majorTickUnitAngle;
                double majorValue = MinValue + i * majorTickUnitValue;

                AddMajorTick(majorTickAngle);
                AddScaleLable(majorTickAngle, majorValue);

                for (int j = 1; j < MinorDivisionsCount; j++)
                {
                    double minorTickAngle = majorTickAngle - majorTickUnitAngle + j * minorTickUinitAngle;
                    AddMinorTick(minorTickAngle);
                }
            }
        }

        #endregion
    }
}
