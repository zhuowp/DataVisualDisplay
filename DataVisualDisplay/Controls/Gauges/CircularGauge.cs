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

        private Panel _rootGrid = null;
        private Panel _pointersPanel = null;
        private Panel _scalesPanel = null;

        #endregion

        #region Properties

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty GaugeRadiusProperty
            = DependencyProperty.Register("GaugeRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(150.0));

        public static readonly DependencyProperty GaugeBackgroundProperty
            = DependencyProperty.Register("GaugeBackground", typeof(Brush), typeof(CircularGauge), new PropertyMetadata(new SolidColorBrush(Colors.SteelBlue)));

        public static readonly DependencyProperty PointerCapRadiusProperty
            = DependencyProperty.Register("PointerCapRadius", typeof(double), typeof(CircularGauge), new PropertyMetadata(35.0));

        public static readonly DependencyProperty IconProperty
            = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(CircularGauge), new PropertyMetadata(null));

        public static readonly DependencyProperty IconHeightProperty
            = DependencyProperty.Register("IconHeight", typeof(double), typeof(CircularGauge), new PropertyMetadata(16.0));

        public static readonly DependencyProperty IconWidthProperty
            = DependencyProperty.Register("IconWidth", typeof(double), typeof(CircularGauge), new PropertyMetadata(16.0));

        public static readonly DependencyProperty IconOffsetProperty
            = DependencyProperty.Register("IconOffset", typeof(double), typeof(CircularGauge), new PropertyMetadata(-50.0));

        public static readonly DependencyProperty DialTextProperty
            = DependencyProperty.Register("DialText", typeof(string), typeof(CircularGauge), new PropertyMetadata("Dial Text"));

        public static readonly DependencyProperty DialTextForegroundProperty
            = DependencyProperty.Register("DialTextForeground", typeof(Brush), typeof(CircularGauge), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty DialTextFontSizeProperty
            = DependencyProperty.Register("DialTextFontSize", typeof(double), typeof(CircularGauge), new PropertyMetadata(8.0));

        public static readonly DependencyProperty DialTextOffsetProperty
            = DependencyProperty.Register("DialTextOffset", typeof(double), typeof(CircularGauge), new PropertyMetadata(40.0));

        public static readonly DependencyProperty PointersProperty
            = DependencyProperty.Register("Pointers", typeof(GaugePointerCollection), typeof(CircularGauge), new PropertyMetadata(null, OnPointersPropertyChanged));

        public static readonly DependencyProperty ScalesProperty
            = DependencyProperty.Register("Scales", typeof(GaugeScaleCollection), typeof(CircularGauge), new PropertyMetadata(null, OnScalesPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

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

        public Brush GaugeBackground
        {
            get
            {
                return (Brush)GetValue(GaugeBackgroundProperty);
            }
            set
            {
                SetValue(GaugeBackgroundProperty, value);
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

        public Brush DialTextForeground
        {
            get
            {
                return (Brush)GetValue(DialTextForegroundProperty);
            }
            set
            {
                SetValue(DialTextForegroundProperty, value);
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

        public GaugeScaleCollection Scales
        {
            get { return (GaugeScaleCollection)GetValue(ScalesProperty); }
            set { SetValue(ScalesProperty, value); }
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

            _rootGrid = GetTemplateChild("PART_Root") as Panel;
            _pointersPanel = GetTemplateChild("PART_PointersPanel") as Panel;
            _scalesPanel = GetTemplateChild("PART_ScalesPanel") as Panel;

            InitScales();
            InitPointers();
        }

        #endregion

        #region Dependency Property Changed Callback

        private static void OnPointersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGauge gauge = d as CircularGauge;
            if (gauge == null)
            {
                return;
            }

            GaugePointerCollection oldPointerCollection = e.OldValue as GaugePointerCollection;
            if (oldPointerCollection != null)
            {
                oldPointerCollection.CollectionChanged -= gauge.PointerCollection_CollectionChanged;
            }

            GaugePointerCollection newPointerCollection = e.NewValue as GaugePointerCollection;
            if (newPointerCollection != null)
            {
                newPointerCollection.CollectionChanged += gauge.PointerCollection_CollectionChanged;
                gauge.InitPointers();
            }
        }

        private static void OnScalesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularGauge gauge = d as CircularGauge;
            if (gauge == null)
            {
                return;
            }

            GaugeScaleCollection oldScaleCollection = e.OldValue as GaugeScaleCollection;
            if (oldScaleCollection != null)
            {
                if (gauge._rootGrid != null)
                {

                }

                oldScaleCollection.CollectionChanged -= gauge.ScaleCollection_CollectionChanged;
            }

            GaugeScaleCollection newScaleCollection = e.NewValue as GaugeScaleCollection;
            if (newScaleCollection != null)
            {
                newScaleCollection.CollectionChanged += gauge.ScaleCollection_CollectionChanged;
            }
        }

        #endregion

        #region Event Methods

        private void PointerCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InitPointers();
        }

        private void ScaleCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InitScales();
        }

        #endregion

        #region Private Methods

        private void OnPointerValueChanged(GaugePointer pointer, double oldAngle, double newAngle)
        {
            if (pointer == null)
            {
                return;
            }

            if (pointer.EnableAnimateRotate)
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

        private void InitPointers()
        {
            if (Pointers == null || _pointersPanel == null)
            {
                return;
            }

            _pointersPanel.Children.Clear();
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
                }
            }
        }

        private void InitScales()
        {
            if (Scales == null || _scalesPanel == null)
            {
                return;
            }

            _scalesPanel.Children.Clear();
            foreach (GaugeScale scale in Scales)
            {
                scale.ScaleContainer = _scalesPanel;
                scale.DrawScale();
            }
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
