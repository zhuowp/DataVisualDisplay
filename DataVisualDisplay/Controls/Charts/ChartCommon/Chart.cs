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
    ///     <MyNamespace:Chart/>
    ///
    /// </summary>
    public class Chart : Control
    {
        #region Fields

        private Panel _chartFramePanel = null;
        private Panel _xAxisesPanel = null;
        private Panel _yAxisesPanel = null;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty TitleProperty
            = DependencyProperty.Register("Title", typeof(string), typeof(Chart), new PropertyMetadata(""));

        public static readonly DependencyProperty TitleFontSizeProperty
            = DependencyProperty.Register("TitleFontSize", typeof(double), typeof(Chart), new PropertyMetadata(16.0));

        public static readonly DependencyProperty TitleForegroundProperty
            = DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(Chart), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty TitleFontFamilyProperty
            = DependencyProperty.Register("TitleFontFamily", typeof(FontFamily), typeof(Chart));

        public static readonly DependencyProperty ChartFramesProperty
            = DependencyProperty.Register("ChartFrames", typeof(ChartFrames), typeof(Chart), new PropertyMetadata(null, OnChartFramesPropertyChanged));

        public static readonly DependencyProperty XAxisesProperty
            = DependencyProperty.Register("XAxises", typeof(Axises), typeof(Chart), new PropertyMetadata(null, OnXAxisesPropertyChanged));

        public static readonly DependencyProperty YAxisesProperty
            = DependencyProperty.Register("YAxises", typeof(Axises), typeof(Chart), new PropertyMetadata(null, OnYAxisesPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        public FontFamily TitleFontFamily
        {
            get { return (FontFamily)GetValue(TitleFontFamilyProperty); }
            set { SetValue(TitleFontFamilyProperty, value); }
        }

        public ChartFrames ChartFrames
        {
            get { return (ChartFrames)GetValue(ChartFramesProperty); }
            set { SetValue(ChartFramesProperty, value); }
        }

        public Axises XAxises
        {
            get { return (Axises)GetValue(XAxisesProperty); }
            set { SetValue(XAxisesProperty, value); }
        }

        public Axises YAxises
        {
            get { return (Axises)GetValue(YAxisesProperty); }
            set { SetValue(YAxisesProperty, value); }
        }

        #endregion

        #region Constructors

        static Chart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Chart), new FrameworkPropertyMetadata(typeof(Chart)));
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _chartFramePanel = GetTemplateChild("PART_ChartFramesPanel") as Panel;
            _xAxisesPanel = GetTemplateChild("PART_XAxisesPanel") as Panel;
            _yAxisesPanel = GetTemplateChild("PART_YAxisesPanel") as Panel;

            InitChartFrames();
            InitXAxises();
            InitYAxises();
        }

        #endregion

        #region Dependency Property Changed Callbacks

        private static void OnChartFramesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Chart chart = d as Chart;
            if (chart == null)
            {
                return;
            }

            ChartFrames oldChartFrames = e.OldValue as ChartFrames;
            if (oldChartFrames != null)
            {
                oldChartFrames.CollectionChanged -= chart.ChartFrames_CollectionChanged;
            }

            ChartFrames newChartFrames = e.NewValue as ChartFrames;
            if (newChartFrames != null)
            {
                newChartFrames.CollectionChanged += chart.ChartFrames_CollectionChanged;
            }

            chart.InitChartFrames();
        }

        private static void OnXAxisesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnYAxisesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion

        #region Private Methods

        private void ChartFrames_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InitChartFrames();
        }

        private void InitChartFrames()
        {
            InitCollectiveComponents(_chartFramePanel, ChartFrames);
        }

        private void InitXAxises()
        {
            InitCollectiveComponents(_xAxisesPanel, XAxises);
        }

        private void InitYAxises()
        {
            InitCollectiveComponents(_yAxisesPanel, YAxises);
        }

        private void InitCollectiveComponents<T>(Panel containerPanel, ObservableCollection<T> controls) where T : Control
        {
            if(containerPanel == null)
            {
                return;
            }
            containerPanel.Children.Clear();

            if(controls == null)
            {
                return;
            }

            foreach(Control control in controls)
            {
                containerPanel.Children.Add(control);
            }
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
