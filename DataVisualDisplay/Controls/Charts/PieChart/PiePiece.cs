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
    ///     <MyNamespace:PiePiece/>
    ///
    /// </summary>
    public class PiePiece : Shape
    {
        #region Dependency Properties

        public static readonly DependencyProperty CenterXProperty
            = DependencyProperty.Register("CenterX", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty CenterYProperty
            = DependencyProperty.Register("CenterY", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty RadiusProperty
            = DependencyProperty.Register("Radius", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty InnerRadiusProperty
            = DependencyProperty.Register("InnerRadius", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ExtractLengthProperty
            = DependencyProperty.Register("ExtractLength", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty RotateAngleProperty
            = DependencyProperty.Register("RotateAngle", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty PercentageProperty
            = DependencyProperty.Register("Percentage", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0));

        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register("Value", typeof(double), typeof(PiePiece), new FrameworkPropertyMetadata(0.0, OnValuePropertyChanged));

        public static readonly DependencyProperty IsSelectedProperty
            = DependencyProperty.Register("IsSelected", typeof(bool), typeof(PiePiece), new PropertyMetadata(false, OnIsSelectedPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

        public double CenterX
        {
            get { return (double)GetValue(CenterXProperty); }
            set { SetValue(CenterXProperty, value); }
        }

        public double CenterY
        {
            get { return (double)GetValue(CenterYProperty); }
            set { SetValue(CenterYProperty, value); }
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

        public double ExtractLength
        {
            get { return (double)GetValue(ExtractLengthProperty); }
            set { SetValue(ExtractLengthProperty, value); }
        }

        public double RotateAngle
        {
            get { return (double)GetValue(RotateAngleProperty); }
            set { SetValue(RotateAngleProperty, value); }
        }

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        #endregion

        #region Properties

        public Action OnValueChangedAction { get; set; }

        #endregion

        #region Constructors

        static PiePiece()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PiePiece), new FrameworkPropertyMetadata(typeof(PiePiece)));
        }

        #endregion

        #region Override Methods

        protected override Geometry DefiningGeometry
        {
            get
            {
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                using (StreamGeometryContext context = geometry.Open())
                {
                    DrawPiePiece(context);
                }

                geometry.Freeze();
                return geometry;
            }
        }

        #endregion

        #region Dependency Property Changed

        private static void OnIsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PiePiece piece = d as PiePiece;
            if (piece == null)
            {
                return;
            }

            DoubleAnimation a = new DoubleAnimation();
            a.Duration = new Duration(TimeSpan.FromMilliseconds(300));

            bool isSelected = (bool)e.NewValue;
            if (isSelected)
            {
                a.To = 10;
            }
            else
            {
                a.To = 0;
            }

            piece.BeginAnimation(ExtractLengthProperty, a);
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PiePiece piece = d as PiePiece;
            if (piece == null)
            {
                return;
            }

            piece.OnValueChangedAction?.Invoke();
        }

        #endregion

        #region Private Methods

        private void DrawPiePiece(StreamGeometryContext context)
        {
            double wedgeAngle = 360 * Percentage;

            Point startPoint = new Point(CenterX, CenterY);

            Point innerArcStartPoint = ComputeCartesianCoordinate(RotateAngle, InnerRadius);
            innerArcStartPoint.Offset(CenterX, CenterY);

            Point innerArcEndPoint = ComputeCartesianCoordinate(RotateAngle + wedgeAngle, InnerRadius);
            innerArcEndPoint.Offset(CenterX, CenterY);

            Point outerArcStartPoint = ComputeCartesianCoordinate(RotateAngle, Radius);
            outerArcStartPoint.Offset(CenterX, CenterY);

            Point outerArcEndPoint = ComputeCartesianCoordinate(RotateAngle + wedgeAngle, Radius);
            outerArcEndPoint.Offset(CenterX, CenterY);

            bool largeArc = wedgeAngle > 180.0;

            if (ExtractLength > 0)
            {
                Point offset = ComputeCartesianCoordinate(RotateAngle + wedgeAngle / 2, ExtractLength);
                innerArcStartPoint.Offset(offset.X, offset.Y);
                innerArcEndPoint.Offset(offset.X, offset.Y);
                outerArcStartPoint.Offset(offset.X, offset.Y);
                outerArcEndPoint.Offset(offset.X, offset.Y);

            }

            Size outerArcSize = new Size(Radius, Radius);
            Size innerArcSize = new Size(InnerRadius, InnerRadius);

            context.BeginFigure(innerArcStartPoint, true, true);
            context.LineTo(outerArcStartPoint, true, true);
            context.ArcTo(outerArcEndPoint, outerArcSize, 0, largeArc, SweepDirection.Clockwise, true, true);
            context.LineTo(innerArcEndPoint, true, true);
            context.ArcTo(innerArcStartPoint, innerArcSize, 0, largeArc, SweepDirection.Counterclockwise, true, true);
        }

        public static Point ComputeCartesianCoordinate(double angle, double radius)
        {
            double angleRad = (Math.PI / 180.0) * (angle - 90);

            double x = radius * Math.Cos(angleRad);
            double y = radius * Math.Sin(angleRad);

            return new Point(x, y);
        }

        #endregion
    }

}
