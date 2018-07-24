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
    ///     <MyNamespace:CircularGauge/>
    ///
    /// </summary>
    public class CircularGauge : Control
    {
        #region Fields

        private Grid _rootGrid = null;
        private Grid _pointersPanel = null;
        private Path _rangeIndicator;

        private Ellipse lightIndicator;
        private Double arcradius1;
        private Double arcradius2;

        #endregion

        #region Properties

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty MinValueProperty
            = DependencyProperty.Register("MinValue", typeof(double), typeof(CircularGauge), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaxValueProperty
            = DependencyProperty.Register("MaxValue", typeof(double), typeof(CircularGauge), new PropertyMetadata(1.0));

        public static readonly DependencyProperty GaugeRadiusProperty
            = DependencyProperty.Register("GaugeRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(150.0));

        public static readonly DependencyProperty GaugeBackgroundColorProperty
            = DependencyProperty.Register("GaugeBackgroundColor", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.Black));

        public static readonly DependencyProperty PointerCapRadiusProperty
            = DependencyProperty.Register("PointerCapRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(35.0));

        public static readonly DependencyProperty ScaleRadiusProperty
            = DependencyProperty.Register("ScaleRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(110.0));

        public static readonly DependencyProperty ScaleStartAngleProperty
            = DependencyProperty.Register("ScaleStartAngle", typeof(double), typeof(CircularGauge), new PropertyMetadata(120.0));

        public static readonly DependencyProperty ScaleSweepAngleProperty
            = DependencyProperty.Register("ScaleSweepAngle", typeof(double), typeof(CircularGauge), new PropertyMetadata(300.0));

        public static readonly DependencyProperty OptimalRangeStartValueProperty
            = DependencyProperty.Register("OptimalRangeStartValue", typeof(double), typeof(CircularGauge), new PropertyMetadata(new PropertyChangedCallback(OnOptimalRangeStartValuePropertyChanged)));

        public static readonly DependencyProperty OptimalRangeEndValueProperty
            = DependencyProperty.Register("OptimalRangeEndValue", typeof(double), typeof(CircularGauge), new PropertyMetadata(new PropertyChangedCallback(OnOptimalRangeEndValuePropertyChanged)));

        public static readonly DependencyProperty IconProperty
            = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(CircularGauge), new PropertyMetadata(null));

        public static readonly DependencyProperty IconHeightProperty
            = DependencyProperty.Register("IconHeight", typeof(double), typeof(CircularGauge), new PropertyMetadata(16.0));

        public static readonly DependencyProperty IconWidthProperty
            = DependencyProperty.Register("IconWidth", typeof(double), typeof(CircularGauge), new PropertyMetadata(16.0));

        public static readonly DependencyProperty IconOffsetProperty
            = DependencyProperty.Register("IconOffset", typeof(double), typeof(CircularGauge), new PropertyMetadata(-50.0));

        public static readonly DependencyProperty RangeIndicatorLightRadiusProperty
            = DependencyProperty.Register("RangeIndicatorLightRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(10.0));

        public static readonly DependencyProperty RangeIndicatorLightOffsetProperty
            = DependencyProperty.Register("RangeIndicatorLightOffset", typeof(double), typeof(CircularGauge), new PropertyMetadata(80.0));

        public static readonly DependencyProperty RangeIndicatorRadiusProperty
            = DependencyProperty.Register("RangeIndicatorRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(120.0));

        public static readonly DependencyProperty RangeIndicatorThicknessProperty
            = DependencyProperty.Register("RangeIndicatorThickness", typeof(double), typeof(CircularGauge), new PropertyMetadata(8.0));

        public static readonly DependencyProperty ScaleLabelRadiusProperty
            = DependencyProperty.Register("ScaleLabelRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(90.0));

        public static readonly DependencyProperty ScaleLabelSizeProperty
            = DependencyProperty.Register("ScaleLabelSize", typeof(Size), typeof(CircularGauge), new PropertyMetadata(new Size(40, 20)));

        public static readonly DependencyProperty ScaleLabelFontSizeProperty
            = DependencyProperty.Register("ScaleLabelFontSize", typeof(double), typeof(CircularGauge), new PropertyMetadata(10.0));

        public static readonly DependencyProperty ScaleLabelForegroundProperty
            = DependencyProperty.Register("ScaleLabelForeground", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.LightGray));

        public static readonly DependencyProperty ScaleValuePrecisionProperty
            = DependencyProperty.Register("ScaleValuePrecision", typeof(int), typeof(CircularGauge), new PropertyMetadata(5));

        public static readonly DependencyProperty MajorDivisionsCountProperty
            = DependencyProperty.Register("MajorDivisionsCount", typeof(int), typeof(CircularGauge), new PropertyMetadata(10));

        public static readonly DependencyProperty MajorTickSizeProperty
            = DependencyProperty.Register("MajorTickRectSize", typeof(Size), typeof(CircularGauge), new PropertyMetadata(new Size(10, 3)));

        public static readonly DependencyProperty MajorTickColorProperty
            = DependencyProperty.Register("MajorTickColor", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.LightGray));

        public static readonly DependencyProperty MinorDivisionsCountProperty
            = DependencyProperty.Register("MinorDivisionsCount", typeof(int), typeof(CircularGauge), new PropertyMetadata(5));

        public static readonly DependencyProperty MinorTickSizeProperty
            = DependencyProperty.Register("MinorTickSize", typeof(Size), typeof(CircularGauge), new PropertyMetadata(new Size(3, 1)));

        public static readonly DependencyProperty MinorTickColorProperty
            = DependencyProperty.Register("MinorTickColor", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.LightGray));

        public static readonly DependencyProperty BelowOptimalRangeColorProperty
            = DependencyProperty.Register("BelowOptimalRangeColor", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.Yellow));

        public static readonly DependencyProperty OptimalRangeColorProperty
            = DependencyProperty.Register("OptimalRangeColor", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.Green));

        public static readonly DependencyProperty AboveOptimalRangeColorProperty
            = DependencyProperty.Register("AboveOptimalRangeColor", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.Red));

        public static readonly DependencyProperty DialTextProperty
            = DependencyProperty.Register("DialText", typeof(string), typeof(CircularGauge), new PropertyMetadata("Dial Text"));

        public static readonly DependencyProperty DialTextColorProperty
            = DependencyProperty.Register("DialTextColor", typeof(Color), typeof(CircularGauge), new PropertyMetadata(Colors.Black));

        public static readonly DependencyProperty DialTextFontSizeProperty
            = DependencyProperty.Register("DialTextFontSize", typeof(double), typeof(CircularGauge), new PropertyMetadata(8.0));

        public static readonly DependencyProperty DialTextOffsetProperty
            = DependencyProperty.Register("DialTextOffset", typeof(double), typeof(CircularGauge), new PropertyMetadata(40.0));

        public static readonly DependencyProperty PointersProperty
            = DependencyProperty.Register("Pointers", typeof(GaugePointerCollection), typeof(CircularGauge), new PropertyMetadata(null, OnPointersPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

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

        public double GaugeRadius
        {
            get
            {
                return (double)GetValue(GaugeRadiusProperty);
            }
            set
            {
                SetValue(GaugeRadiusProperty, value);
            }
        }

        public double PointerCapRadius
        {
            get
            {
                return (double)GetValue(PointerCapRadiusProperty);
            }
            set
            {
                SetValue(PointerCapRadiusProperty, value);
            }
        }

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

        public double OptimalRangeEndValue
        {
            get
            {
                return (double)GetValue(OptimalRangeEndValueProperty);
            }
            set
            {
                SetValue(OptimalRangeEndValueProperty, value);
            }
        }

        public double OptimalRangeStartValue
        {
            get
            {
                return (double)GetValue(OptimalRangeStartValueProperty);
            }
            set
            {
                SetValue(OptimalRangeStartValueProperty, value);
            }
        }

        public ImageSource Icon
        {
            get
            {
                return (ImageSource)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public double IconWidth
        {
            get
            {
                return (double)GetValue(IconWidthProperty);
            }
            set
            {
                SetValue(IconWidthProperty, value);
            }
        }

        public double IconHeight
        {
            get
            {
                return (double)GetValue(IconHeightProperty);
            }
            set
            {
                SetValue(IconHeightProperty, value);
            }
        }

        public double IconOffset
        {
            get
            {
                return (double)GetValue(IconOffsetProperty);
            }
            set
            {
                SetValue(IconOffsetProperty, value);
            }
        }

        public double RangeIndicatorLightRadius
        {
            get
            {
                return (double)GetValue(RangeIndicatorLightRadiusProperty);
            }
            set
            {
                SetValue(RangeIndicatorLightRadiusProperty, value);
            }
        }

        public double RangeIndicatorLightOffset
        {
            get
            {
                return (double)GetValue(RangeIndicatorLightOffsetProperty);
            }
            set
            {
                SetValue(RangeIndicatorLightOffsetProperty, value);
            }
        }

        public double RangeIndicatorRadius
        {
            get
            {
                return (double)GetValue(RangeIndicatorRadiusProperty);
            }
            set
            {
                SetValue(RangeIndicatorRadiusProperty, value);
            }
        }

        public double RangeIndicatorThickness
        {
            get
            {
                return (double)GetValue(RangeIndicatorThicknessProperty);
            }
            set
            {
                SetValue(RangeIndicatorThicknessProperty, value);
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

        public Color ScaleLabelForeground
        {
            get
            {
                return (Color)GetValue(ScaleLabelForegroundProperty);
            }
            set
            {
                SetValue(ScaleLabelForegroundProperty, value);
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

        public Color MajorTickColor
        {
            get
            {
                return (Color)GetValue(MajorTickColorProperty);
            }
            set
            {
                SetValue(MajorTickColorProperty, value);
            }
        }

        public Color MinorTickColor
        {
            get
            {
                return (Color)GetValue(MinorTickColorProperty);
            }
            set
            {
                SetValue(MinorTickColorProperty, value);
            }
        }

        public Color GaugeBackgroundColor
        {
            get
            {
                return (Color)GetValue(GaugeBackgroundColorProperty);
            }
            set
            {
                SetValue(GaugeBackgroundColorProperty, value);
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

        public Color BelowOptimalRangeColor
        {
            get
            {
                return (Color)GetValue(BelowOptimalRangeColorProperty);
            }
            set
            {
                SetValue(BelowOptimalRangeColorProperty, value);
            }
        }

        public Color OptimalRangeColor
        {
            get
            {
                return (Color)GetValue(OptimalRangeColorProperty);
            }
            set
            {
                SetValue(OptimalRangeColorProperty, value);
            }
        }

        public Color AboveOptimalRangeColor
        {
            get
            {
                return (Color)GetValue(AboveOptimalRangeColorProperty);
            }
            set
            {
                SetValue(AboveOptimalRangeColorProperty, value);
            }
        }

        public string DialText
        {
            get
            {
                return (string)GetValue(DialTextProperty);
            }
            set
            {
                SetValue(DialTextProperty, value);
            }
        }

        public Color DialTextColor
        {
            get
            {
                return (Color)GetValue(DialTextColorProperty);
            }
            set
            {
                SetValue(DialTextColorProperty, value);
            }
        }

        public double DialTextFontSize
        {
            get
            {
                return (double)GetValue(DialTextFontSizeProperty);
            }
            set
            {
                SetValue(DialTextFontSizeProperty, value);
            }
        }

        public double DialTextOffset
        {
            get
            {
                return (double)GetValue(DialTextOffsetProperty);
            }
            set
            {
                SetValue(DialTextOffsetProperty, value);
            }
        }

        public GaugePointerCollection Pointers
        {
            get
            {
                return (GaugePointerCollection)GetValue(PointersProperty);
            }
            set
            {
                SetValue(PointersProperty, value);
            }
        }

        #endregion

        #region Routed Events

        #endregion

        #region Routed Event Wrappers

        #endregion

        #region Constructors

        static CircularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularGauge), new FrameworkPropertyMetadata(typeof(CircularGauge)));
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _pointersPanel = GetTemplateChild("PART_PointersPanel") as Grid;
            if (_pointersPanel != null && Pointers.Count > 0)
            {
                foreach (GaugePointer pointer in Pointers)
                {
                    if (pointer.PointedValueChangedAction == null)
                    {
                        pointer.PointedValueChangedAction = new Action<GaugePointer, double, double>(OnPointerValueChanged);
                    }

                    if (_pointersPanel != null && !_pointersPanel.Children.Contains(pointer))
                    {
                        _pointersPanel.Children.Add(pointer);

                        OnPointerValueChanged(pointer, 0, 0);
                        Panel.SetZIndex(pointer, 100000);
                    }
                }
            }

            _rootGrid = GetTemplateChild("LayoutRoot") as Grid;
            //pointerCap = GetTemplateChild("PointerCap") as Ellipse;
            lightIndicator = GetTemplateChild("RangeIndicatorLight") as Ellipse;

            DrawScale();
            DrawRangeIndicator();

            //Panel.SetZIndex(pointerCap, 100001);
        }

        #endregion

        #region Dependency Property Changed Callback

        private static void OnOptimalRangeEndValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGauge gauge = d as CircularGauge;
            if ((double)e.NewValue > gauge.MaxValue)
            {
                gauge.OptimalRangeEndValue = gauge.MaxValue;
            }
        }

        private static void OnOptimalRangeStartValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGauge gauge = d as CircularGauge;
            if ((double)e.NewValue < gauge.MinValue)
            {
                gauge.OptimalRangeStartValue = gauge.MinValue;
            }
        }

        private static void OnPointersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGauge gauge = d as CircularGauge;
            GaugePointerCollection pointerCollection = e.NewValue as GaugePointerCollection;
            if (pointerCollection != null)
            {
                for (int i = 0; i < pointerCollection.Count; i++)
                {
                    GaugePointer pointer = pointerCollection[i];
                    if (pointer.PointedValueChangedAction == null)
                    {
                        pointer.PointedValueChangedAction = new Action<GaugePointer, double, double>(gauge.OnPointerValueChanged);
                    }

                    if (gauge._pointersPanel != null)
                    {
                        gauge._pointersPanel.Children.Add(pointer);

                        gauge.OnPointerValueChanged(pointer, 0, 0);
                        Panel.SetZIndex(pointer, 100000);
                    }
                }

                pointerCollection.CollectionChanged += gauge.PointerCollection_CollectionChanged;
            }
        }

        private void PointerCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<GaugePointer> pointerCollection = sender as ObservableCollection<GaugePointer>;
            if (pointerCollection == null)
            {
                return;
            }

            for (int i = 0; i < pointerCollection.Count; i++)
            {
                GaugePointer pointer = pointerCollection[i];
                if (pointer.PointedValueChangedAction == null)
                {
                    pointer.PointedValueChangedAction = new Action<GaugePointer, double, double>(OnPointerValueChanged);
                    if (_pointersPanel != null)
                    {
                        _pointersPanel.Children.Add(pointer);

                        OnPointerValueChanged(pointer, 0, 0);
                        Panel.SetZIndex(pointer, 100000);
                    }
                }
            }
        }

        #endregion

        #region Event Methods

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion

        #region Methods
        
        private void OnPointerValueChanged(GaugePointer pointer, double oldAngle, double newAngle)
        {
            if (pointer == null)
            {
                return;
            }

            if(pointer.EnableAnimateRotate)
            {
                AnimateRotatePointer(pointer, oldAngle, newAngle);
            }
            else
            {
                RotatePointer(pointer, newAngle);
            }
        }

        /// <summary>
        /// 设置指针转角
        /// </summary>
        /// <param name="pointer"></param>
        /// <param name="angleValue"></param>
        private void RotatePointer(GaugePointer pointer, double angleValue)
        {
            RotateTransform rt = new RotateTransform();
            pointer.RenderTransform = rt;
            pointer.RenderTransformOrigin = new Point(0.5, 0.5);

            rt.Angle = angleValue;
        }
        
        /// <summary>
        /// 动画的形式设置指针转角
        /// </summary>
        /// <param name="pointer"></param>
        /// <param name="oldAngle"></param>
        /// <param name="newAngle"></param>
        private void AnimateRotatePointer(GaugePointer pointer, double oldAngle, double newAngle)
        {
            RotateTransform rtf = new RotateTransform();
            pointer.RenderTransform = rtf;
            pointer.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation da = new DoubleAnimation();
            da.From = oldAngle;
            da.To = newAngle;

            double animDuration = Math.Abs(oldAngle - newAngle) * pointer.AnimatingSpeedFactor;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(animDuration));

            Storyboard sb = new Storyboard();
            sb.Children.Add(da);
            Storyboard.SetTarget(da, pointer);
            Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.Angle"));

            sb.Begin();
        }

        /// <summary>
        /// Switch on the Range indicator light after the pointer completes animating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void sb_Completed(object sender, EventArgs e)
        //{
        //    if (this.Value > OptimalRangeEndValue)
        //    {
        //        lightIndicator.Fill = GetRangeIndicatorGradEffect(AboveOptimalRangeColor);

        //    }
        //    else if (this.Value <= OptimalRangeEndValue && this.Value >= OptimalRangeStartValue)
        //    {
        //        lightIndicator.Fill = GetRangeIndicatorGradEffect(OptimalRangeColor);

        //    }
        //    else if (this.Value < OptimalRangeStartValue)
        //    {
        //        lightIndicator.Fill = GetRangeIndicatorGradEffect(BelowOptimalRangeColor);
        //    }
        //}

        /// <summary>
        /// Get gradient brush effect for the range indicator light
        /// </summary>
        /// <param name="gradientColor"></param>
        /// <returns></returns>
        private GradientBrush GetRangeIndicatorGradEffect(Color gradientColor)
        {

            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0, 0);
            gradient.EndPoint = new Point(1, 1);
            GradientStop color1 = new GradientStop();
            if (gradientColor == Colors.Transparent)
            {
                color1.Color = gradientColor;
            }
            else
                color1.Color = Colors.LightGray;

            color1.Offset = 0.2;
            gradient.GradientStops.Add(color1);
            GradientStop color2 = new GradientStop();
            color2.Color = gradientColor; color2.Offset = 0.5;
            gradient.GradientStops.Add(color2);
            GradientStop color3 = new GradientStop();
            color3.Color = gradientColor; color3.Offset = 0.8;
            gradient.GradientStops.Add(color3);
            return gradient;
        }




        //Drawing the scale with the Scale Radius
        private void DrawScale()
        {
            //Calculate one major tick angle 
            Double majorTickUnitAngle = ScaleSweepAngle / MajorDivisionsCount;

            //Obtaining One minor tick angle 
            Double minorTickUnitAngle = ScaleSweepAngle / MinorDivisionsCount;

            //Obtaining One major ticks value
            Double majorTicksUnitValue = (MaxValue - MinValue) / MajorDivisionsCount;
            majorTicksUnitValue = Math.Round(majorTicksUnitValue, ScaleValuePrecision);

            Double minvalue = MinValue; ;

            // Drawing Major scale ticks
            for (Double i = ScaleStartAngle; i <= (ScaleStartAngle + ScaleSweepAngle); i = i + majorTickUnitAngle)
            {

                //Majortick is drawn as a rectangle 
                Rectangle majortickrect = new Rectangle();
                majortickrect.Height = MajorTickSize.Height;
                majortickrect.Width = MajorTickSize.Width;
                majortickrect.Fill = new SolidColorBrush(MajorTickColor);
                Point p = new Point(0.5, 0.5);
                majortickrect.RenderTransformOrigin = p;
                majortickrect.HorizontalAlignment = HorizontalAlignment.Center;
                majortickrect.VerticalAlignment = VerticalAlignment.Center;

                TransformGroup majortickgp = new TransformGroup();
                RotateTransform majortickrt = new RotateTransform();

                //Obtaining the angle in radians for calulating the points
                Double i_radian = (i * Math.PI) / 180;
                majortickrt.Angle = i;
                majortickgp.Children.Add(majortickrt);
                TranslateTransform majorticktt = new TranslateTransform();

                //Finding the point on the Scale where the major ticks are drawn
                //here drawing the points with center as (0,0)
                majorticktt.X = (int)((ScaleRadius) * Math.Cos(i_radian));
                majorticktt.Y = (int)((ScaleRadius) * Math.Sin(i_radian));

                //Points for the textblock which hold the scale value
                TranslateTransform majorscalevaluett = new TranslateTransform();
                //here drawing the points with center as (0,0)
                majorscalevaluett.X = (int)((ScaleLabelRadius) * Math.Cos(i_radian));
                majorscalevaluett.Y = (int)((ScaleLabelRadius) * Math.Sin(i_radian));

                //Defining the properties of the scale value textbox
                TextBlock tb = new TextBlock();

                tb.Height = ScaleLabelSize.Height;
                tb.Width = ScaleLabelSize.Width;
                tb.FontSize = ScaleLabelFontSize;
                tb.Foreground = new SolidColorBrush(ScaleLabelForeground);
                tb.TextAlignment = TextAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;

                //Writing and appending the scale value

                //checking minvalue < maxvalue w.r.t scale precion value
                if (Math.Round(minvalue, ScaleValuePrecision) <= Math.Round(MaxValue, ScaleValuePrecision))
                {
                    minvalue = Math.Round(minvalue, ScaleValuePrecision);
                    tb.Text = minvalue.ToString();
                    minvalue = minvalue + majorTicksUnitValue;

                }
                else
                {
                    break;
                }
                majortickgp.Children.Add(majorticktt);
                majortickrect.RenderTransform = majortickgp;
                tb.RenderTransform = majorscalevaluett;
                _rootGrid.Children.Add(majortickrect);
                _rootGrid.Children.Add(tb);


                //Drawing the minor axis ticks
                Double onedegree = ((i + majorTickUnitAngle) - i) / (MinorDivisionsCount);

                if ((i < (ScaleStartAngle + ScaleSweepAngle)) && (Math.Round(minvalue, ScaleValuePrecision) <= Math.Round(MaxValue, ScaleValuePrecision)))
                {
                    //Drawing the minor scale
                    for (Double mi = i + onedegree; mi < (i + majorTickUnitAngle); mi = mi + onedegree)
                    {
                        //here the minortick is drawn as a rectangle 
                        Rectangle mr = new Rectangle();
                        mr.Height = MinorTickSize.Height;
                        mr.Width = MinorTickSize.Width;
                        mr.Fill = new SolidColorBrush(MinorTickColor);
                        mr.HorizontalAlignment = HorizontalAlignment.Center;
                        mr.VerticalAlignment = VerticalAlignment.Center;
                        Point p1 = new Point(0.5, 0.5);
                        mr.RenderTransformOrigin = p1;

                        TransformGroup minortickgp = new TransformGroup();
                        RotateTransform minortickrt = new RotateTransform();
                        minortickrt.Angle = mi;
                        minortickgp.Children.Add(minortickrt);
                        TranslateTransform minorticktt = new TranslateTransform();

                        //Obtaining the angle in radians for calulating the points
                        Double mi_radian = (mi * Math.PI) / 180;
                        //Finding the point on the Scale where the minor ticks are drawn
                        minorticktt.X = (int)((ScaleRadius) * Math.Cos(mi_radian));
                        minorticktt.Y = (int)((ScaleRadius) * Math.Sin(mi_radian));

                        minortickgp.Children.Add(minorticktt);
                        mr.RenderTransform = minortickgp;
                        _rootGrid.Children.Add(mr);


                    }

                }

            }
        }

        /// <summary>
        /// Obtaining the Point (x,y) in the circumference 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private Point GetCircumferencePoint(Double angle, Double radius)
        {
            Double angle_radian = (angle * Math.PI) / 180;
            //Radius-- is the Radius of the gauge
            Double X = (Double)((GaugeRadius) + (radius) * Math.Cos(angle_radian));
            Double Y = (Double)((GaugeRadius) + (radius) * Math.Sin(angle_radian));
            Point p = new Point(X, Y);
            return p;
        }

        /// <summary>
        /// Draw the range indicator
        /// </summary>
        private void DrawRangeIndicator()
        {
            Double realworldunit = (ScaleSweepAngle / (MaxValue - MinValue));
            Double optimalStartAngle;
            Double optimalEndAngle;
            double db;

            //Checking whether the  OptimalRangeStartvalue is -ve 
            if (OptimalRangeStartValue < 0)
            {
                db = MinValue + Math.Abs(OptimalRangeStartValue);
                optimalStartAngle = ((double)(Math.Abs(db * realworldunit)));
            }
            else
            {
                db = Math.Abs(MinValue) + OptimalRangeStartValue;
                optimalStartAngle = ((double)(db * realworldunit));
            }

            //Checking whether the  OptimalRangeEndvalue is -ve
            if (OptimalRangeEndValue < 0)
            {
                db = MinValue + Math.Abs(OptimalRangeEndValue);
                optimalEndAngle = ((double)(Math.Abs(db * realworldunit)));
            }
            else
            {
                db = Math.Abs(MinValue) + OptimalRangeEndValue;
                optimalEndAngle = ((double)(db * realworldunit));
            }
            // calculating the angle for optimal Start value

            Double optimalStartAngleFromStart = (ScaleStartAngle + optimalStartAngle);

            // calculating the angle for optimal Start value

            Double optimalEndAngleFromStart = (ScaleStartAngle + optimalEndAngle);

            //Calculating the Radius of the two arc for segment 
            arcradius1 = (RangeIndicatorRadius + RangeIndicatorThickness);
            arcradius2 = RangeIndicatorRadius;

            double endAngle = ScaleStartAngle + ScaleSweepAngle;

            // Calculating the Points for the below Optimal Range segment from the center of the gauge

            Point A = GetCircumferencePoint(ScaleStartAngle, arcradius1);
            Point B = GetCircumferencePoint(ScaleStartAngle, arcradius2);
            Point C = GetCircumferencePoint(optimalStartAngleFromStart, arcradius2);
            Point D = GetCircumferencePoint(optimalStartAngleFromStart, arcradius1);

            bool isReflexAngle = Math.Abs(optimalStartAngleFromStart - ScaleStartAngle) > 180.0;
            DrawSegment(A, B, C, D, isReflexAngle, BelowOptimalRangeColor);

            // Calculating the Points for the Optimal Range segment from the center of the gauge

            Point A1 = GetCircumferencePoint(optimalStartAngleFromStart, arcradius1);
            Point B1 = GetCircumferencePoint(optimalStartAngleFromStart, arcradius2);
            Point C1 = GetCircumferencePoint(optimalEndAngleFromStart, arcradius2);
            Point D1 = GetCircumferencePoint(optimalEndAngleFromStart, arcradius1);
            bool isReflexAngle1 = Math.Abs(optimalEndAngleFromStart - optimalStartAngleFromStart) > 180.0;
            DrawSegment(A1, B1, C1, D1, isReflexAngle1, OptimalRangeColor);

            // Calculating the Points for the Above Optimal Range segment from the center of the gauge

            Point A2 = GetCircumferencePoint(optimalEndAngleFromStart, arcradius1);
            Point B2 = GetCircumferencePoint(optimalEndAngleFromStart, arcradius2);
            Point C2 = GetCircumferencePoint(endAngle, arcradius2);
            Point D2 = GetCircumferencePoint(endAngle, arcradius1);
            bool isReflexAngle2 = Math.Abs(endAngle - optimalEndAngleFromStart) > 180.0;
            DrawSegment(A2, B2, C2, D2, isReflexAngle2, AboveOptimalRangeColor);
        }

        //Drawing the segment with two arc and two line

        private void DrawSegment(Point p1, Point p2, Point p3, Point p4, bool reflexangle, Color clr)
        {

            // Segment Geometry
            PathSegmentCollection segments = new PathSegmentCollection();

            // First line segment from pt p1 - pt p2
            segments.Add(new LineSegment() { Point = p2 });

            //Arc drawn from pt p2 - pt p3 with the RangeIndicatorRadius 
            segments.Add(new ArcSegment()
            {
                Size = new Size(arcradius2, arcradius2),
                Point = p3,
                SweepDirection = SweepDirection.Clockwise,
                IsLargeArc = reflexangle
            });

            // Second line segment from pt p3 - pt p4
            segments.Add(new LineSegment() { Point = p4 });

            //Arc drawn from pt p4 - pt p1 with the Radius of arcradius1 
            segments.Add(new ArcSegment()
            {
                Size = new Size(arcradius1, arcradius1),
                Point = p1,
                SweepDirection = SweepDirection.Counterclockwise,
                IsLargeArc = reflexangle

            });

            // Defining the segment path properties
            Color rangestrokecolor;
            if (clr == Colors.Transparent)
            {
                rangestrokecolor = clr;
            }
            else
                rangestrokecolor = Colors.White;



            _rangeIndicator = new Path()
            {
                StrokeLineJoin = PenLineJoin.Round,
                Stroke = new SolidColorBrush(rangestrokecolor),
                //Color.FromArgb(0xFF, 0xF5, 0x9A, 0x86)
                Fill = new SolidColorBrush(clr),
                Opacity = 0.65,
                StrokeThickness = 0.25,
                Data = new PathGeometry()
                {
                    Figures = new PathFigureCollection()
                     {
                        new PathFigure()
                        {
                            IsClosed = true,
                            StartPoint = p1,
                            Segments = segments
                        }
                    }
                }
            };

            //Set Z index of range indicator
            _rangeIndicator.SetValue(Canvas.ZIndexProperty, 150);
            // Adding the segment to the root grid 
            _rootGrid.Children.Add(_rangeIndicator);

        }

        #endregion
    }
}
