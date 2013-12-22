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
            if (!isFirstCilck)
            {
                lastClickColor = newColor;
                Fill(lastMousePoint, newColor);
            }

        }
        //鼠标点击图案,获取当前相对于图案的位置
        bool isFirstCilck = true;
        private void FloodFillImage_GetMousePosition(object sender, MouseButtonEventArgs e)
        {
            Point mousePoint = e.GetPosition((Image)sender);


            int index = GetPixelPosition((int)mousePoint.X, (int)mousePoint.Y);

            Color clickPointColor = new Color();
            clickPointColor.B = enhPixelData[index];
            clickPointColor.G = enhPixelData[index + 1];
            clickPointColor.R = enhPixelData[index + 2];
            //tempColor.A = enhPixelData[index + 3];

            if (!clickPointColor.Equals(highLightColor))
            {
                
                if (!isFirstCilck)
                {
                    //恢复前一个点选区域的颜色
                    int newIndex = GetPixelPosition((int)lastMousePoint.X, (int)lastMousePoint.Y);
                    Color newColorOfLastPoint = new Color();
                    newColorOfLastPoint.B = enhPixelData[newIndex];
                    newColorOfLastPoint.G = enhPixelData[newIndex + 1];
                    newColorOfLastPoint.R = enhPixelData[newIndex + 2];

                    //
                    if (newColorOfLastPoint.Equals(highLightColor) || !newColorOfLastPoint.Equals(lastClickColor))
                    {
                        //恢复前一个点选区域的颜色
                        Fill(lastMousePoint, lastColor);
                    }
                    
                }
                //高亮目前区域颜色
                Fill(mousePoint, highLightColor);

                lastMousePoint = mousePoint;
                lastColor = clickPointColor;
                isFirstCilck = false;
            }
            else
            {
                //nothing
            }
            
        }
    }

}
