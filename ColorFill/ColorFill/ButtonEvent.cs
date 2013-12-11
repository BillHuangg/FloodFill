using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorFill
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Button temp = (Button)sender;
            colorName = temp.Content.ToString();

            switch (colorName)
            {
                case "red":
                    currentColor = redColor;
                    break;
                case "blue":
                    currentColor = blueColor;
                    break;
                case "green":
                    currentColor = greenColor;
                    break;
                case "yellow":
                    currentColor = yellowColor;
                    break;

            }

        }
        //鼠标点击图案,获取当前相对于图案的位置
        private void FloodFillImage_GetMousePosition(object sender, MouseButtonEventArgs e)
        {
            Point mousePoint = e.GetPosition((Image)sender);
            currentMousePoint = mousePoint;

            Fill();
        }
    }

}
