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
    ///     <MyNamespace:GaugePointer/>
    ///
    /// </summary>
    public class GaugePointer : Control
    {
        #region Fields

        #endregion

        #region Properties

        public Action<GaugePointer, double, double> PointedValueChangedAction { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty PointerLengthProperty
            = DependencyProperty.Register("PointerLength", typeof(double), typeof(GaugePointer), new PropertyMetadata(85.0));

        public static readonly DependencyProperty PointerThicknessProperty
            = DependencyProperty.Register("PointerThickness", typeof(double), typeof(GaugePointer), new PropertyMetadata(16.0));

        public static readonly DependencyProperty RotateCenterXProperty
            = DependencyProperty.Register("RotateCenterX", typeof(double), typeof(GaugePointer), new PropertyMetadata(40.0));

        public static readonly DependencyProperty RotateCenterYProperty
            = DependencyProperty.Register("RotateCenterY", typeof(double), typeof(GaugePointer), new PropertyMetadata(40.0));

        public static readonly DependencyProperty IndicatedValueProperty
            = DependencyProperty.Register("IndicatedValue", typeof(double), typeof(GaugePointer), new PropertyMetadata(0.0, OnIndicatedValueChanged));

        public static readonly DependencyProperty MinValueProperty
            = DependencyProperty.Register("MinValue", typeof(double), typeof(GaugePointer), new PropertyMetadata(0.0));

        public static readonly DependencyProperty MaxValueProperty
            = DependencyProperty.Register("MaxValue", typeof(double), typeof(GaugePointer), new PropertyMetadata(1.0));

        public static readonly DependencyProperty PointerStartAngleProperty
            = DependencyProperty.Register("PointerStartAngle", typeof(double), typeof(GaugePointer), new PropertyMetadata(120.0));

        public static readonly DependencyProperty PointerSweepAngleProperty
            = DependencyProperty.Register("PointerSweepAngle", typeof(double), typeof(GaugePointer), new PropertyMetadata(300.0));

        public static readonly DependencyProperty AnimatingSpeedFactorProperty
            = DependencyProperty.Register("AnimatingSpeedFactor", typeof(double), typeof(GaugePointer), new PropertyMetadata(1.0));

        public static readonly DependencyProperty EnableAnimateRotateProperty
            = DependencyProperty.Register("EnableAnimateRotate", typeof(bool), typeof(GaugePointer), new PropertyMetadata(false));

        #endregion

        #region Dependency Property Wrappers

        public double PointerLength
        {
            get
            {
                return (double)GetValue(PointerLengthProperty);
            }
            set
            {
                SetValue(PointerLengthProperty, value);
            }
        }

        public double PointerThickness
        {
            get
            {
                return (double)GetValue(PointerThicknessProperty);
            }
            set
            {
                SetValue(PointerThicknessProperty, value);
            }
        }

        public double RotateCenterX
        {
            get { return (double)GetValue(RotateCenterXProperty); }
            set { SetValue(RotateCenterXProperty, value); }
        }

        public double RotateCenterY
        {
            get { return (double)GetValue(RotateCenterYProperty); }
            set { SetValue(RotateCenterYProperty, value); }
        }

        public double IndicatedValue
        {
            get
            {
                return (double)GetValue(IndicatedValueProperty);
            }
            set
            {
                SetValue(IndicatedValueProperty, value);
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

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public double PointerStartAngle
        {
            get { return (double)GetValue(PointerStartAngleProperty); }
            set { SetValue(PointerStartAngleProperty, value); }
        }

        public double PointerSweepAngle
        {
            get { return (double)GetValue(PointerSweepAngleProperty); }
            set { SetValue(PointerSweepAngleProperty, value); }
        }

        public double AnimatingSpeedFactor
        {
            get { return (double)GetValue(AnimatingSpeedFactorProperty); }
            set { SetValue(AnimatingSpeedFactorProperty, value); }
        }

        public bool EnableAnimateRotate
        {
            get { return (bool)GetValue(EnableAnimateRotateProperty); }
            set { SetValue(EnableAnimateRotateProperty, value); }
        }

        #endregion

        #region Constructors

        static GaugePointer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GaugePointer), new FrameworkPropertyMetadata(typeof(GaugePointer)));
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Event Methods

        private static void OnIndicatedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GaugePointer pointer = d as GaugePointer;
            if (pointer == null)
            {
                return;
            }

            double oldValue = (double)e.OldValue;
            double newValue = (double)e.NewValue;

            oldValue = pointer.GetValidateValue(oldValue);
            newValue = pointer.GetValidateValue(newValue);
            if (oldValue != newValue)
            {
                double oldValueAngle = pointer.GetValueAngle(oldValue);
                double newValueAngle = pointer.GetValueAngle(newValue);

                pointer.PointedValueChangedAction.Invoke(pointer, oldValueAngle, newValueAngle);
            }
        }

        #endregion

        #region Private Methods

        private double GetValidateValue(double value)
        {
            if (value > MaxValue)
            {
                value = MaxValue;
            }
            else if (value < MinValue)
            {
                value = MinValue;
            }

            return value;
        }

        private double GetValueAngle(double value)
        {
            double unit = (PointerSweepAngle / (MaxValue - MinValue));
            double angle = PointerStartAngle + (value - MinValue) * unit;

            return angle;
        }

        #endregion
    }
}
