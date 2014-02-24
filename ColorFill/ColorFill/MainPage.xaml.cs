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
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        //get value from start page
        private bool isCharacter = false;
        private int imageNum = 0;

        //private string colorName = "blank";
        //相关数组
        private WriteableBitmap processImageBitMap;
        private Int32Rect processImageBitmapRect;
        private Int32 processImageStride;
        //图片属性
        private int bytePerPixel = 4;
        private int ImageHeight = 900;
        private int ImageWidth = 1200;
        //存储当前图片数据 数组
        private byte[] backupPixelData;
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

        private Color Color0;
        private Color Color1;
        private Color Color2;
        private Color Color3;
        private Color Color4;
        private Color Color5;
        private Color Color6;
        private Color Color7;
        private Color Color8;
        private Color Color9;
        private Color Color10;
        private Color Color11;

        private Color lastColor;
        private Color lastClickColor;

        private int colorNum = 12;
        double colorButtonHeight = 176;
        double colorButtonWidth = 187;
        double colorButtonHeight_Clicked = 191;
        double colorButtonWidth_Clicked = 205;

        int functionButtonHeight=116;
        int functionButtonWidth=258;
        int functionButtonHeight_Clicked=122;
        int functionButtonWidth_Clicked=270;

        int finishButtonHeight = 197;
        int finishButtonWidth = 348;
        int finishButtonHeight_Clicked = 215;
        int finishButtonWidth_Clicked = 382;

        byte[] fileHeadPart;

        public MainPage()
        {
            InitializeComponent();
        }


        //get image type and num
        public void SetFloodFillImage(bool _isCharacter, int i)
        {
            isCharacter = _isCharacter;
            imageNum = i;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Uri uri = new Uri(@"pack://siteoforigin:,,,/Image/MainPageBG.png", UriKind.RelativeOrAbsolute);
            BG.Source = new BitmapImage(uri);
            uri = new Uri(@"pack://siteoforigin:,,,/Image/border.png", UriKind.RelativeOrAbsolute);
            Border.Source = new BitmapImage(uri);
            


            initColor();
            initColorButton();
            initFunctionButton();

            //初始化图案
            initMainImage();
            enhPixelData = new byte[ImageWidth * ImageHeight * bytePerPixel];
            backupPixelData = new byte[ImageWidth * ImageHeight * bytePerPixel];
            enhPixelData = GetImageData();
            enhPixelData.CopyTo(backupPixelData, 0);

            RefreshImage();
        }
        private void initColor()
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

            Color0 = new Color();
            Color0.R = 255;
            Color0.G = 134;
            Color0.B = 131;

            Color1 = new Color();
            Color1.R = 254;
            Color1.G = 0;
            Color1.B = 0;

            Color2 = new Color();
            Color2.R = 189;
            Color2.G = 1;
            Color2.B = 186;

            Color3 = new Color();
            Color3.R = 255;
            Color3.G = 255;
            Color3.B = 67;

            Color4 = new Color();
            Color4.R = 238;
            Color4.G = 185;
            Color4.B = 17;

            Color5 = new Color();
            Color5.R = 255;
            Color5.G = 87;
            Color5.B = 6;

            Color6 = new Color();
            Color6.R = 85;
            Color6.G = 206;
            Color6.B = 235;

            Color7 = new Color();
            Color7.R = 46;
            Color7.G = 99;
            Color7.B = 253;

            Color8 = new Color();
            Color8.R = 255;
            Color8.G = 196;
            Color8.B = 126;

            Color9 = new Color();
            Color9.R = 99;
            Color9.G = 214;
            Color9.B = 35;

            Color10 = new Color();
            Color10.R = 22;
            Color10.G = 112;
            Color10.B = 77;

            Color11 = new Color();
            Color11.R = 76;
            Color11.G = 50;
            Color11.B = 51;


            //当前颜色默认为红色
            //lastColor = redColor;
        }

        private void initColorButton()
        {
            //create button 
            int interval = 2;
            int initLeftStart = 1340;
            int initTopStart = 74;
            for (int i = 0; i < colorNum; i++)
            {
                string name = "color" + i;

                Image imageTemp = new Image();

                imageTemp.Name = "color" + "s" + i;
                imageTemp.Height = colorButtonHeight;
                imageTemp.Width = colorButtonWidth;
                imageTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/ColorButton/MainPage/" + name + ".png", UriKind.Relative));
                imageTemp.Stretch = Stretch.Fill;

                Canvas.SetLeft(imageTemp, initLeftStart + (int)(i % 3) * colorButtonWidth + interval);
                Canvas.SetTop(imageTemp, initTopStart + (int)(i / 3) * colorButtonWidth + interval);

                imageTemp.MouseLeftButtonUp += new MouseButtonEventHandler(ColorButton_Click);
                imageTemp.MouseLeftButtonDown += new MouseButtonEventHandler(ColorButtonClickDown);
                imageTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
                imageTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
                imageTemp.MouseLeave += new MouseEventHandler(ColorButtonReset);
                ButtonLayer.Children.Add(imageTemp);
            }
        }

        private void initFunctionButton()
        {
            //create button 
            int ButtonTop = 940;
            int resetButtonLeft = 1000;
            int backButtonLeft = 82;

            int finishButtonTop = 835;
            int finishButtonLeft = 1460;

            //Reset button
            Image buttonTemp = new Image();

            buttonTemp.Name = "reset";
            buttonTemp.Height = functionButtonHeight;
            buttonTemp.Width = functionButtonWidth;
            buttonTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/MainPage/" + buttonTemp.Name + ".png", UriKind.Relative));
            buttonTemp.Stretch = Stretch.Fill;

            Canvas.SetLeft(buttonTemp, resetButtonLeft);
            Canvas.SetTop(buttonTemp, ButtonTop);

            buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);
            buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(ResetButtonFunction);
            
            buttonTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
            buttonTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
            buttonTemp.MouseLeave += new MouseEventHandler(FunctionButtonReset);
            ButtonLayer.Children.Add(buttonTemp);

            //Back Button
            buttonTemp = new Image();

            buttonTemp.Name = "back";
            buttonTemp.Height = functionButtonHeight;
            buttonTemp.Width = functionButtonWidth;
            buttonTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/MainPage/" + buttonTemp.Name + ".png", UriKind.Relative));
            buttonTemp.Stretch = Stretch.Fill;

            Canvas.SetLeft(buttonTemp, backButtonLeft);
            Canvas.SetTop(buttonTemp, ButtonTop);

            buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);
            buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(BackButtonFunction);

            buttonTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
            buttonTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
            buttonTemp.MouseLeave += new MouseEventHandler(FunctionButtonReset);
            ButtonLayer.Children.Add(buttonTemp);

            //Yes Button
            buttonTemp = new Image();

            buttonTemp.Name = "finish";
            buttonTemp.Height = finishButtonHeight;
            buttonTemp.Width = finishButtonWidth;
            buttonTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/MainPage/" + buttonTemp.Name + ".png", UriKind.Relative));
            buttonTemp.Stretch = Stretch.Fill;

            Canvas.SetLeft(buttonTemp, finishButtonLeft);
            Canvas.SetTop(buttonTemp, finishButtonTop);

            buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);
            buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(FinishButtonFunction);

            buttonTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
            buttonTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
            buttonTemp.MouseLeave += new MouseEventHandler(FunctionButtonReset);
            ButtonLayer.Children.Add(buttonTemp);
        }


        private void initMainImage()
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
            fileHeadPart = new byte[72];
            string filePath;
            if (isCharacter)
            {
                filePath = "Image/FloodFillImage/character/character" + imageNum + ".bmp";
            }
            else
            {
                filePath = "Image/FloodFillImage/scenery/scenery" + imageNum + ".bmp";
            }
            
            byte[] temp = System.IO.File.ReadAllBytes(filePath);
            byte[] result = new byte[temp.Length - 72];
            //手动排除文件格式title 只读取rgbx数据 //
            Array.Copy(temp, 71, result, 0, result.Length);
            //保留头部格式数据
            Array.Copy(temp, 0, fileHeadPart, 0, fileHeadPart.Length);
            return result;
        }


        //执行填充
        private void FillGray(Point point, Color color)
        {
            lastIndexStack.Clear();
            FloodFill1(point, color);
            PlayDonedSound();
            RefreshImage();
        }
        private void FillColor(Point point, Color color)
        {
            FloodFillByIndex(point, color);
            PlayDonedSound();
            RefreshImage();
        }
        private void FloodFillByIndex(Point point, Color color)
        {
            Stack<int> oldIndexStack = new Stack<int>(lastIndexStack);
            while (oldIndexStack.Count > 0)
            {
                int temp = oldIndexStack.Pop();
                SetPixelColor(temp, color);


            }
        }
        Stack<int> lastIndexStack = new Stack<int>();
        Color tempColor = new Color();
        //填充算法
        private void FloodFill1(Point point, Color color)
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
                    break;
                //若该点为可填充的点即执行
                if (CheckDiff(temp, color))//canFillArea(tempColor, color))
                {
                    int index = GetPixelPosition((int)temp.X, (int)temp.Y);
                    //不同则替换
                    SetPixelColor(index, color);
                    lastIndexStack.Push(index);

                    //下
                    Point old = temp;
                    temp.X += 1;
                    if (CheckDiff(temp, color))
                    {
                        pixelStack.Push(temp);
                    }
                    //while (true)
                    //{
                    //    temp.X += 1;
                    //    if (CheckDiff(temp, color))
                    //    {
                    //        index = GetPixelPosition((int)temp.X, (int)temp.Y);
                    //        //不同则替换
                    //        SetPixelColor(index, color);
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}
                    //上
                    temp = old;
                    temp.X -= 1;
                    if (CheckDiff(temp, color))
                    {
                        pixelStack.Push(temp);
                    }
                    //while (true)
                    //{
                    //    temp.X -= 1;
                    //    if (CheckDiff(temp, color))
                    //    {
                    //        index = GetPixelPosition((int)temp.X, (int)temp.Y);
                    //        //不同则替换
                    //        SetPixelColor(index, color);
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}

                    //左
                    temp = old;
                    temp.Y += 1;
                    if (CheckDiff(temp, color))
                    {
                        pixelStack.Push(temp);
                    }
                    //while (true)
                    //{
                    //    temp.Y += 1;
                    //    if (CheckDiff(temp, color))
                    //    {
                    //        index = GetPixelPosition((int)temp.X, (int)temp.Y);
                    //        //不同则替换
                    //        SetPixelColor(index, color);
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}


                    //右
                    temp = old;
                    temp.Y -= 1;
                    if (CheckDiff(temp, color))
                    {
                        pixelStack.Push(temp);
                    }
                    //while (true)
                    //{
                    //    temp.Y -= 1;
                    //    if (CheckDiff(temp, color))
                    //    {
                    //        index = GetPixelPosition((int)temp.X, (int)temp.Y);
                    //        //不同则替换
                    //        SetPixelColor(index, color);
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}





                    //temp = old;
                    //temp.Y -= 1;
                    //temp.X -= 1;
                    //if (CheckDiff(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}

                    //temp = old;
                    //temp.Y += 1;
                    //temp.X += 1;
                    //if (CheckDiff(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}

                    //temp = old;
                    //temp.Y += 1;
                    //temp.X -= 1;
                    //if (CheckDiff(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}

                    //temp = old;
                    //temp.Y -= 1;
                    //temp.X += 1;
                    //if (CheckDiff(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}
                }
                else
                {
                    //nothing
                    continue;
                }
            }
        }

        private void FloodFill2(Point point, Color color)
        {
            //种子
            Point seedPoint = point;
            Stack<Point> pixelStack = new Stack<Point>();
            //Queue<Point> pixelQueue = new Queue<Point>();

            //初始化
            pixelStack.Push(seedPoint);

            //当栈内无数据时停止
            while (pixelStack.Count != 0)
            {
                Point temp = pixelStack.Pop();
                if ((temp.X < 0) || (temp.X >= ImageWidth) || (temp.Y < 0) || (temp.Y >= ImageHeight))
                    continue;

                int index = GetPixelPosition((int)temp.X, (int)temp.Y);

                //Color tempColor = new Color();
                tempColor.B = enhPixelData[index];
                tempColor.G = enhPixelData[index + 1];
                tempColor.R = enhPixelData[index + 2];
                tempColor.A = enhPixelData[index + 3];
                //若该点为可填充的点即执行
                if (canFillArea(tempColor, color))
                {
                    //不同则替换
                    SetPixelColor(index, color);

                    Point old = temp;
                    //left
                    while (true)
                    {
                        temp.X += 1;
                        if (CheckDiff(temp, color))
                        {
                            index = GetPixelPosition((int)temp.X, (int)temp.Y);
                            SetPixelColor(index, color);
                        }
                        else
                        {
                            double oldX = temp.X;

                            temp.Y += 1;
                            pixelStack.Push(temp);

                            temp.Y -= 2;
                            pixelStack.Push(temp);

                            temp.X -= 1;
                            temp.Y += 1;
                            pixelStack.Push(temp);

                            temp.Y -= 2;
                            pixelStack.Push(temp);

                            temp.X = oldX;
                            break;
                        }

                    }
                    double RightEdgePoint = temp.X;


                    temp = old;
                    while (true)
                    {
                        temp.X -= 1;
                        if (CheckDiff(temp, color))
                        {
                            index = GetPixelPosition((int)temp.X, (int)temp.Y);
                            SetPixelColor(index, color);
                        }
                        else
                        {
                            double oldX = temp.X;

                            temp.Y -= 1;
                            pixelStack.Push(temp);

                            temp.Y += 2;
                            pixelStack.Push(temp);

                            temp.X += 1;
                            temp.Y += 1;
                            pixelStack.Push(temp);

                            temp.Y -= 2;
                            pixelStack.Push(temp);

                            temp.X = oldX;
                            break;
                        }
                    }
                    double LeftEdgePoint = temp.X;

                    temp = old;

                    temp.Y -= 1;
                    pixelStack.Push(temp);
                    temp.Y -= 1;
                    pixelStack.Push(temp);

                    temp.Y += 3;
                    pixelStack.Push(temp);
                    temp.Y += 1;
                    pixelStack.Push(temp);

                    double tempMiddle = (RightEdgePoint + LeftEdgePoint) / 2;// +LeftEdgePoint;
                    if (temp.X != tempMiddle)
                    {
                        temp.X = tempMiddle;
                        temp.Y -= 1;
                        pixelStack.Push(temp);

                        temp.Y += 2;
                        pixelStack.Push(temp);
                    }
                    ////下
                    //Point old = temp;
                    //temp.X += 1;
                    //if (Check(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}

                    ////上
                    //temp = old;
                    //temp.X -= 1;
                    //if (Check(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}

                    ////左
                    //temp = old;
                    //temp.Y += 1;
                    //if (Check(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}

                    ////右
                    //temp = old;
                    //temp.Y -= 1;
                    //if (Check(temp, color))
                    //{
                    //    pixelStack.Push(temp);
                    //}

                    ////temp = old;
                    ////temp.X -= 1;
                    ////temp.Y -= 1;
                    ////pixelStack.Push(temp);

                    ////temp = old;
                    ////temp.X -= 1;
                    ////temp.Y += 1;
                    ////pixelStack.Push(temp);

                    ////temp = old;
                    ////temp.X += 1;
                    ////temp.Y -= 1;
                    ////pixelStack.Push(temp);

                    ////temp = old;
                    ////temp.X += 1;
                    ////temp.Y += 1;
                    ////pixelStack.Push(temp);
                }
                else
                {
                    //nothing
                    continue;
                }
            }
        }

        private void FloodFill3(Point point, Color color)
        {
            //种子
            Point seedPoint = point;
            Stack<Point> pixelStack = new Stack<Point>();
            //Queue<Point> pixelQueue = new Queue<Point>();

            //初始化
            pixelStack.Push(seedPoint);

            //当栈内无数据时停止
            while (pixelStack.Count != 0)
            {
                Point temp = pixelStack.Pop();
                if ((temp.X < 0) || (temp.X >= ImageWidth) || (temp.Y < 0) || (temp.Y >= ImageHeight))
                    continue;

                int index = GetPixelPosition((int)temp.X, (int)temp.Y);

                //Color tempColor = new Color();
                tempColor.B = enhPixelData[index];
                tempColor.G = enhPixelData[index + 1];
                tempColor.R = enhPixelData[index + 2];
                tempColor.A = enhPixelData[index + 3];
                //若该点为可填充的点即执行
                if (canFillArea(tempColor, color))
                {
                    //不同则替换
                    SetPixelColor(index, color);

                    Point old = temp;
                    //right
                    while (true)
                    {
                        temp.X += 1;
                        if (!CheckEdge(temp, color))
                        {
                            break;
                        }

                    }
                    double RightEdgePoint = temp.X;

                    temp = old;
                    while (true)
                    {
                        temp.X -= 1;
                        if (!CheckEdge(temp, color))
                        {
                            break;
                        }
                    }
                    double LeftEdgePoint = temp.X;

                    DrawLine(RightEdgePoint, LeftEdgePoint, temp.Y, color);

                    temp = old;
                    temp.X = RightEdgePoint;
                    temp.Y += 1;
                    pixelStack.Push(temp);

                    temp = old;
                    temp.X = RightEdgePoint;
                    temp.Y -= 1;
                    pixelStack.Push(temp);

                    temp = old;
                    temp.X = LeftEdgePoint;
                    temp.Y += 1;
                    pixelStack.Push(temp);

                    temp = old;
                    temp.X = LeftEdgePoint;
                    temp.Y -= 1;
                    pixelStack.Push(temp);
                }
                else
                {
                    //nothing
                    continue;
                }
            }
        }

        private bool DrawLine(double rightPoint, double leftPoint, double pointY, Color color)
        {
            if ((leftPoint < 0) || (rightPoint > ImageWidth) || (pointY < 0) || (pointY > ImageHeight))
                return false;

            else
            {
                for (int i = (int)leftPoint + 1; i < rightPoint; i++)
                {
                    int index = GetPixelPosition((int)i, (int)pointY);
                    SetPixelColor(index, color);
                }

                return true;
            }

        }

        private bool CheckDiff(Point temp, Color color)
        {
            if ((temp.X < 0) || (temp.X >= ImageWidth) || (temp.Y < 0) || (temp.Y >= ImageHeight))
                return false;
            int index = GetPixelPosition((int)temp.X, (int)temp.Y);

            Color tempColor = new Color();
            tempColor.B = enhPixelData[index];
            tempColor.G = enhPixelData[index + 1];
            tempColor.R = enhPixelData[index + 2];
            tempColor.A = enhPixelData[index + 3];

            return canFillArea(tempColor, color);
        }

        private bool CheckEdge(Point temp, Color color)
        {
            if ((temp.X < 0) || (temp.X >= ImageWidth) || (temp.Y < 0) || (temp.Y >= ImageHeight))
                return false;
            int index = GetPixelPosition((int)temp.X, (int)temp.Y);

            Color tempColor = new Color();
            tempColor.B = enhPixelData[index];
            tempColor.G = enhPixelData[index + 1];
            tempColor.R = enhPixelData[index + 2];
            tempColor.A = enhPixelData[index + 3];

            //if (tempColor.Equals(old))
            //{
            //    return false;
            //}
            //else 
            if (!tempColor.Equals(borderColor))
            {
                return true;
            }
            else
                return false;
        }
        //与背景色 以及其余可选色相同 即可填充
        //与边框相同即不可填充 返回false
        private bool canFillArea(Color current, Color old)
        {
            if (current.Equals(old))
            {
                return false;
            }
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
        private void RefreshImage(bool isClear = false)
        {
            if (isClear)
            {
                backupPixelData.CopyTo(enhPixelData, 0);
            }
            processImageBitMap.WritePixels(processImageBitmapRect, enhPixelData, processImageStride, 0);
        }

        //获取像素在数组中位置
        private int GetPixelPosition(int startX, int startY)
        {
            return (startX + (startY * ImageWidth)) * bytePerPixel;
        }

        private void PlayDonedSound()
        {
            MediaPlayer audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(@"pack://siteoforigin:,,,/Image/Audio/Done.wav", UriKind.RelativeOrAbsolute));
            audioPlayer.Play();
        }
    }
}
