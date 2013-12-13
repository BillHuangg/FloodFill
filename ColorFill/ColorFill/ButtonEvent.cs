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
            Color newColor = new Color();
            switch (colorName)
            {
                case "red":
                    newColor = redColor;
                    break;
                case "blue":
                    newColor = blueColor;
                    break;
                case "green":
                    newColor = greenColor;
                    break;
                case "yellow":
                    newColor = yellowColor;
                    break;

            }
            Fill(lastMousePoint, newColor);

        }
        //鼠标点击图案,获取当前相对于图案的位置
        bool isFirstCilck = true;
        private void FloodFillImage_GetMousePosition(object sender, MouseButtonEventArgs e)
        {
            Point mousePoint = e.GetPosition((Image)sender);


            int index = GetPixelPosition((int)mousePoint.X, (int)mousePoint.Y);

            Color tempColor = new Color();
            tempColor.B = enhPixelData[index];
            tempColor.G = enhPixelData[index + 1];
            tempColor.R = enhPixelData[index + 2];
            //tempColor.A = enhPixelData[index + 3];

            if (!tempColor.Equals(highLightColor))
            {
                //高亮目前区域颜色
                Fill(mousePoint, highLightColor);

                if (!isFirstCilck)
                {
                    //恢复前一个点选区域的颜色
                    //检测该区域颜色有没有变化 若有 即不恢复
                    index = GetPixelPosition((int)lastMousePoint.X, (int)lastMousePoint.Y);
                    Color newColor = new Color();
                    newColor.B = enhPixelData[index];
                    newColor.G = enhPixelData[index + 1];
                    newColor.R = enhPixelData[index + 2];
                    if (tempColor.Equals(newColor))
                    {
                        //恢复前一个点选区域的颜色
                        Fill(lastMousePoint, lastColor);
                    }
                    isFirstCilck = false;
                }

                lastMousePoint = mousePoint;
                lastColor = tempColor;
            }
            else
            {
                //nothing
            }
        }
    }

}
