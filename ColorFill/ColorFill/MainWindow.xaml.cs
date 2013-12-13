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
        private string colorName = "blank";
        //相关数组
        private WriteableBitmap processImageBitMap;
        private Int32Rect processImageBitmapRect;
        private Int32 processImageStride;
        //图片属性
        private int bytePerPixel = 4;
        private int ImageHeight = 800;
        private int ImageWidth = 800;
        //存储当前图片数据 数组
        private byte[] enhPixelData;

        private Point lastMousePoint;

        //color
        private Color backgroundColor;
        private Color borderColor;
        private Color highLightColor;
        private Color redColor;
        private Color blueColor;
        private Color yellowColor;
        private Color greenColor;
        private Color lastColor;
        private Color lastClickColor;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化所有可选颜色 以及边框 底色
            backgroundColor = new Color();
            backgroundColor.R = 255;
            backgroundColor.G = 255;
            backgroundColor.B = 255;

            borderColor = new Color();
            borderColor.R = 0;
            borderColor.G = 0;
            borderColor.B = 0;

            lastClickColor = new Color();
            lastClickColor.R = 0;
            lastClickColor.G = 0;
            lastClickColor.B = 0;

            highLightColor = new Color();
            highLightColor.R = 220;
            highLightColor.G = 220;
            highLightColor.B = 220;

            redColor = new Color();
            redColor.R = 255;
            redColor.G = 0;
            redColor.B = 0;

            blueColor = new Color();
            blueColor.R = 0;
            blueColor.G = 160;
            blueColor.B = 233;

            yellowColor = new Color();
            yellowColor.R = 255;
            yellowColor.G = 244;
            yellowColor.B = 92;

            greenColor = new Color();
            greenColor.R = 46;
            greenColor.G = 204;
            greenColor.B = 113;

            //当前颜色默认为红色
            //lastColor = redColor;

            //初始化图案
            initImage();
            enhPixelData = new byte[ImageWidth * ImageHeight * bytePerPixel];
            enhPixelData = GetImageData();
            RefreshImage();
        }


        private void initImage()
        {
            //初始化UI image 载体数据
            processImageBitMap = new WriteableBitmap(ImageWidth, ImageHeight, 96, 96,
                                                                        PixelFormats.Bgr32, null);
            processImageBitmapRect = new Int32Rect(0, 0, ImageWidth, ImageHeight);
            processImageStride = ImageWidth * bytePerPixel;
            FloodFillImage.Source = processImageBitMap;
        }

        private byte[] GetImageData()
        {
            //bmp格式要求:   
            //在将psd转化为bmp时,选择"高级模式"中的  32位  X8 R8 G8 B8 格式 并翻转行序
            //翻转行序原因: bmp保存是从图片左下角为起点开始保存数据
            string filePath = "Image/FloodFillImg4.bmp";
            byte[] temp = System.IO.File.ReadAllBytes(filePath);
            byte[] result = new byte[temp.Length-72];
            //手动排除文件格式title 只读取rgbx数据 //
            Array.Copy(temp, 71, result, 0, result.Length);
            return result;
        }


        //执行填充
        private void Fill(Point point,Color color)
        {
            FloodFill(point, color);
            RefreshImage();
        }


        //填充算法
        private void FloodFill(Point point, Color color)
        {
            //种子
            Point seedPoint = point;
            Stack<Point> pixelStack = new Stack<Point>();
            //初始化
            pixelStack.Push(seedPoint);
            //当栈内无数据时停止
            while (pixelStack.Count != 0)
            {
                Point temp = pixelStack.Pop();
                if ((temp.X < 0) || (temp.X >= ImageWidth) || (temp.Y < 0) || (temp.Y >= ImageHeight))
                    continue;

                int index = GetPixelPosition((int)temp.X, (int)temp.Y);
                
                Color tempColor = new Color();
                tempColor.B = enhPixelData[index];
                tempColor.G = enhPixelData[index + 1];
                tempColor.R = enhPixelData[index + 2];
                tempColor.A = enhPixelData[index + 3];
                //若该点为可填充的点即执行
                if (canFillArea(tempColor, color))
                {
                    //不同则替换
                    SetPixelColor(index, color);

                    //下
                    Point old = temp;
                    temp.X += 1;
                    pixelStack.Push(temp);

                    //上
                    temp = old;
                    temp.X -= 1;
                    pixelStack.Push(temp);

                    //左
                    temp = old;
                    temp.Y += 1;
                    pixelStack.Push(temp);

                    //右
                    temp = old;
                    temp.Y -= 1;
                    pixelStack.Push(temp);
                }
                else
                {
                    //nothing
                }
            }
        }

        //与背景色 以及其余可选色相同 即可填充
        //与边框相同即不可填充 返回false
        private bool canFillArea(Color current, Color old)
        {
            if (current.Equals(old))
            {
                return false;
            }

            //else if (current.Equals(backgroundColor)
            //    || current.Equals(redColor)
            //    || current.Equals(blueColor)
            //    || current.Equals(yellowColor)
            //    || current.Equals(greenColor)
            //    || current.A != 0
            //    )
            //{
            //    return true;
            //}
            else if (!current.Equals(borderColor))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //设置像素颜色
        private void SetPixelColor(int pixelIndex, Color fillColor)
        {
            ////b g r
            enhPixelData[pixelIndex] = fillColor.B;
            enhPixelData[pixelIndex + 1] = fillColor.G;
            enhPixelData[pixelIndex + 2] = fillColor.R;
        }

        //刷新图片控件
        private void RefreshImage()
        {
            processImageBitMap.WritePixels(processImageBitmapRect, enhPixelData, processImageStride, 0);
        }

        //获取像素在数组中位置
        private int GetPixelPosition(int startX, int startY)
        {
            return (startX + (startY * ImageWidth)) * bytePerPixel;
        }
    }
}
