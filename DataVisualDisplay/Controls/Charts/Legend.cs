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
    ///     <MyNamespace:Legend/>
    ///
    /// </summary>
    public class Legend : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty LegendTitleProperty
            = DependencyProperty.Register("LegendTitle", typeof(string), typeof(Legend), new PropertyMetadata(""));

        public static readonly DependencyProperty DataPointsProperty
            = DependencyProperty.Register("DataPoints", typeof(ObservableCollection<DataPoint>), typeof(Legend), new PropertyMetadata(null));

        public static readonly DependencyProperty BrushSetProperty
            = DependencyProperty.Register("BrushSet", typeof(DataPointBrushs), typeof(Legend), new PropertyMetadata(null, OnColorSetPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

        public string LegendTitle
        {
            get { return (string)GetValue(LegendTitleProperty); }
            set { SetValue(LegendTitleProperty, value); }
        }

        public ObservableCollection<DataPoint> DataPoints
        {
            get { return (ObservableCollection<DataPoint>)GetValue(DataPointsProperty); }
            set { SetValue(DataPointsProperty, value); }
        }

        public DataPointBrushs BrushSet
        {
            get { return (DataPointBrushs)GetValue(BrushSetProperty); }
            set { SetValue(BrushSetProperty, value); }
        }

        #endregion

        #region Constructors

        static Legend()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Legend), new FrameworkPropertyMetadata(typeof(Legend)));
        }

        #endregion

        #region Dependency Property Changed Callbacks

        private static void OnColorSetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion
    }
}
