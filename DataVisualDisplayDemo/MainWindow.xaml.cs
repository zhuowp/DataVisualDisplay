using System;
using System.Collections.Generic;
using System.IO;
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

namespace DataVisualDisplayDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Methods

        private void btnSavePic_Click(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)tabContent.ActualWidth, (int)tabContent.ActualHeight, 96d, 96d, PixelFormats.Default);
            targetBitmap.Render(tabContent);

            PngBitmapEncoder saveEncoder = new PngBitmapEncoder();
            saveEncoder.Frames.Add(BitmapFrame.Create(targetBitmap));

            FileStream fs = File.Open("gauge.png", FileMode.OpenOrCreate);
            saveEncoder.Save(fs);
        }

        #endregion
    }
}
