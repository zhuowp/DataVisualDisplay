using DataVisualDisplay.Helpers;
using DataVisualDisplay.Models.LedDigital;
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
    ///     xmlns:MyNamespace="clr-namespace:DataVisualDisplay.Controls.LedDigital"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:DataVisualDisplay.Controls.LedDigital;assembly=DataVisualDisplay.Controls.LedDigital"
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
    ///     <MyNamespace:LedDigital/>
    ///
    /// </summary>
    public class LedDigital : Control
    {
        #region Fields

        private Panel _rootPanel;

        private Path _topSegment;
        private Path _upLeftSegment;
        private Path _upRightSegment;
        private Path _middleSegment;
        private Path _downLeftSegment;
        private Path _downRightSegment;
        private Path _bottomSegment;

        private Path _dotSegment;
        private Path _colonUpSegment;
        private Path _colonDownSegment;

        private ILedDigitalSegmentCreator _segementCreator = null;
        private bool _isInit = false;

        #endregion

        #region Dependency properties

        /// <summary>
        /// Dependency property to Get/Set the current value 
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(LedDigital), new PropertyMetadata(null, new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        /// 依赖属性-LED显示颜色
        /// </summary>
        public static readonly DependencyProperty LEDColorProperty =
            DependencyProperty.Register("LEDColor", typeof(Color), typeof(LedDigital), new PropertyMetadata(Colors.Red, new PropertyChangedCallback(OnLEDColorPropertyChange)));

        /// <summary>
        /// LED高度
        /// </summary>
        public static readonly DependencyProperty LEDHeightProperty =
            DependencyProperty.Register("LEDHeight", typeof(double), typeof(LedDigital), new PropertyMetadata(40.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// LED的宽度
        /// </summary>
        public static readonly DependencyProperty LEDWidthProperty =
            DependencyProperty.Register("LEDWidth", typeof(double), typeof(LedDigital), new PropertyMetadata(20.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// LED字体的粗细
        /// </summary>
        public static readonly DependencyProperty LEDThicknessProperty =
            DependencyProperty.Register("LEDThickness", typeof(double), typeof(LedDigital), new PropertyMetadata(5.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// Segment间的距离
        /// </summary>
        public static readonly DependencyProperty SegmentIntervalProperty =
            DependencyProperty.Register("SegmentInterval", typeof(double), typeof(LedDigital), new PropertyMetadata(2.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// segment两端的截断长度
        /// </summary>
        public static readonly DependencyProperty BevelWidthProperty =
            DependencyProperty.Register("BevelWidth", typeof(double), typeof(LedDigital), new PropertyMetadata(2.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        #endregion

        #region Wrapper properties

        /// <summary>
        /// Gets/Sets the current value
        /// </summary>
        public string Value
        {
            get
            {
                return (string)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED颜色
        /// </summary>
        public Color LEDColor
        {
            get
            {
                return (Color)GetValue(LEDColorProperty);
            }
            set
            {
                SetValue(LEDColorProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED高度
        /// </summary>
        public double LEDHeight
        {
            get
            {
                return (double)GetValue(LEDHeightProperty);
            }
            set
            {
                SetValue(LEDHeightProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED宽度
        /// </summary>
        public double LEDWidth
        {
            get
            {
                return (double)GetValue(LEDWidthProperty);
            }
            set
            {
                SetValue(LEDWidthProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED字体粗细
        /// </summary>
        public double LEDThickness
        {
            get
            {
                return (double)GetValue(LEDThicknessProperty);
            }
            set
            {
                SetValue(LEDThicknessProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED segment间距
        /// </summary>
        public double SegmentInterval
        {
            get
            {
                return (double)GetValue(SegmentIntervalProperty);
            }
            set
            {
                SetValue(SegmentIntervalProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED截断宽度
        /// </summary>
        public double BevelWidth
        {
            get
            {
                return (double)GetValue(BevelWidthProperty);
            }
            set
            {
                SetValue(BevelWidthProperty, value);
            }
        }

        #endregion

        #region Constructor

        static LedDigital()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LedDigital), new FrameworkPropertyMetadata(typeof(LedDigital)));
        }

        #endregion

        #region Property Changed Callbacks

        /// <summary>
        /// 控件属性值发生变化时调用的方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "Height")
            {
                LEDHeight = (double)e.NewValue;
            }
            else if (e.Property.Name == "Width")
            {
                LEDWidth = (double)e.NewValue - LEDThickness;
            }
        }

        /// <summary>
        /// 当前值发生变化时候调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigital led = d as LedDigital;
            if (led != null)
            {
                led.SetDisplayDigitalValue(led.Value);
            }
        }

        /// <summary>
        /// LED颜色发生变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnLEDColorPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigital led = d as LedDigital;
            //led.SetAllSegmentsColor(led.dd, led.LEDColor);
        }

        /// <summary>
        /// 当led形状参数发生变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //LedDigital led = d as LedDigital;

            ////获取根布局
            //Grid rootGrid = led.GetTemplateChild("gdRoot") as Grid;
            //if (rootGrid == null)
            //{
            //    return;
            //}

            ////清除原图形
            //if (led.rootGrid != null)
            //{
            //    led.rootGrid.Children.Clear();
            //}

            ////画新数字图形
            ////初始化Segments的点集digitalSegmentDict
            //led.SetSegmentsData();
            ////画数字
            //led.dd = led.DrawSegments(led.digitalSegmentDict, led.LEDColor);
            ////将线段添加到容器
            //led.AddSegmentsToPanel(led.dd);

            //led.dd.DisplayDigital(led.Value);
        }

        #endregion

        #region Method

        /// <summary>
        /// 调用模板时的方法
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //获取根容器
            _rootPanel = GetTemplateChild("PART_Root") as Panel;

            //初始化数字片段生成器
            DigitalParameter dp = GetDigitalParameter();
            _segementCreator = new BaseLedDigitalSegmentCreator(dp);

            //初始化数字
            InitAllDigitalSegments();

            //设置初始值
            SetDisplayDigitalValue(Value);

            _isInit = true;
        }

        private DigitalParameter GetDigitalParameter()
        {
            DigitalParameter dp = new DigitalParameter();
            dp.BevelWidth = BevelWidth;
            dp.SegmentInterval = SegmentInterval;
            dp.SegmentThickness = LEDThickness;
            dp.DigitalHeight = LEDHeight;
            dp.DigitalWidth = LEDWidth;

            return dp;
        }

        /// <summary>
        /// 初始化Segments的点集
        /// </summary>
        private void InitAllDigitalSegments()
        {
            if (_segementCreator == null)
            {
                throw new Exception("The segment creator is null.");
            }

            if (_rootPanel == null)
            {
                throw new Exception("The root panel is null.");
            }

            InitDigitalSegment(_segementCreator.GetTopSegment(), ref _topSegment);
            InitDigitalSegment(_segementCreator.GetUpLeftSegment(), ref _upLeftSegment);
            InitDigitalSegment(_segementCreator.GetUpRightSegment(), ref _upRightSegment);
            InitDigitalSegment(_segementCreator.GetMiddleSegment(), ref _middleSegment);
            InitDigitalSegment(_segementCreator.GetDownLeftSegment(), ref _downLeftSegment);
            InitDigitalSegment(_segementCreator.GetDownRightSegment(), ref _downRightSegment);
            InitDigitalSegment(_segementCreator.GetBottomSegment(), ref _bottomSegment);

            InitDigitalSegment(_segementCreator.GetDotSegment(), ref _dotSegment);
            InitDigitalSegment(_segementCreator.GetUpColonSegment(), ref _colonUpSegment);
            InitDigitalSegment(_segementCreator.GetDownColonSegment(), ref _colonDownSegment);
        }

        /// <summary>
        /// 将数字片段添加到显示容器
        /// </summary>
        /// <param name="dd"></param>
        private void InitDigitalSegment(List<Point> points, ref Path path)
        {
            Path segment = null;
            if (points.Count > 1)
            {
                segment = GraphicsPloter.DrawLine(points, LEDColor);
            }
            else if (points.Count == 1)
            {
                segment = GraphicsPloter.DrawEllipse(points[0], LEDThickness / 2, LEDColor);
            }

            path = segment;
            _rootPanel.Children.Add(segment);
        }

        /// <summary>
        /// 设置segment的状态
        /// </summary>
        /// <param name="top"></param>
        /// <param name="upRight"></param>
        /// <param name="downRight"></param>
        /// <param name="bottom"></param>
        /// <param name="downLeft"></param>
        /// <param name="upLeft"></param>
        /// <param name="middle"></param>
        /// <param name="dot"></param>
        /// <param name="colon"></param>
        private void LightDigitalSegments(bool top, bool upRight, bool downRight, bool bottom, bool downLeft, bool upLeft, bool middle, bool dot, bool colon)
        {
            if(!_isInit)
            {
                return;
            }

            double ON = 1;
            double OFF = 0.05;

            _topSegment.Opacity = top ? ON : OFF;
            _upRightSegment.Opacity = upRight ? ON : OFF;
            _downRightSegment.Opacity = downRight ? ON : OFF;
            _bottomSegment.Opacity = bottom ? ON : OFF;
            _downLeftSegment.Opacity = downLeft ? ON : OFF;
            _upLeftSegment.Opacity = upLeft ? ON : OFF;
            _middleSegment.Opacity = middle ? ON : OFF;
            _dotSegment.Opacity = dot ? ON : OFF;
            _colonUpSegment.Opacity = _colonDownSegment.Opacity = colon ? ON : OFF;
        }

        /// <summary>
        /// 根据输入字符显示相应的字符
        /// </summary>
        /// <param name="digital"></param>
        private void SetDisplayDigitalValue(string digital)
        {
            switch (digital)
            {
                case null:
                case " ":
                    LightDigitalSegments(false, false, false, false, false, false, false, false, false);
                    break;
                case "0":
                    LightDigitalSegments(true, true, true, true, true, true, false, false, false);
                    break;
                case "1":
                    LightDigitalSegments(false, true, true, false, false, false, false, false, false);
                    break;
                case "2":
                    LightDigitalSegments(true, true, false, true, true, false, true, false, false);
                    break;
                case "3":
                    LightDigitalSegments(true, true, true, true, false, false, true, false, false);
                    break;
                case "4":
                    LightDigitalSegments(false, true, true, false, false, true, true, false, false);
                    break;
                case "5":
                    LightDigitalSegments(true, false, true, true, false, true, true, false, false);
                    break;
                case "6":
                    LightDigitalSegments(true, false, true, true, true, true, true, false, false);
                    break;
                case "7":
                    LightDigitalSegments(true, true, true, false, false, false, false, false, false);
                    break;
                case "8":
                    LightDigitalSegments(true, true, true, true, true, true, true, false, false);
                    break;
                case "9":
                    LightDigitalSegments(true, true, true, true, false, true, true, false, false);
                    break;
                case "0.":
                    LightDigitalSegments(true, true, true, true, true, true, false, true, false);
                    break;
                case "1.":
                    LightDigitalSegments(false, true, true, false, false, false, false, true, false);
                    break;
                case "2.":
                    LightDigitalSegments(true, true, false, true, true, false, true, true, false);
                    break;
                case "3.":
                    LightDigitalSegments(true, true, true, true, false, false, true, true, false);
                    break;
                case "4.":
                    LightDigitalSegments(false, true, true, false, false, true, true, true, false);
                    break;
                case "5.":
                    LightDigitalSegments(true, false, true, true, false, true, true, true, false);
                    break;
                case "6.":
                    LightDigitalSegments(true, false, true, true, true, true, true, true, false);
                    break;
                case "7.":
                    LightDigitalSegments(true, true, true, false, false, false, false, true, false);
                    break;
                case "8.":
                    LightDigitalSegments(true, true, true, true, true, true, true, true, false);
                    break;
                case "9.":
                    LightDigitalSegments(true, true, true, true, false, true, true, true, false);
                    break;
                case ":":
                case "：":
                    LightDigitalSegments(false, false, false, false, false, false, false, false, true);
                    break;
                case "-":
                    LightDigitalSegments(false, false, false, false, false, false, true, false, false);
                    break;
                default:
                    throw new Exception("输入字符错误！");
            }
        }

        #endregion
    }
}
