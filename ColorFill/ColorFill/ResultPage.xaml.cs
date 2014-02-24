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
    /// ResultPage.xaml 的交互逻辑
    /// </summary>
    public partial class ResultPage : Page
    {
        //get value from last page
        private bool isCharacter = false;
        private int imageNum = 0;

        //get value from mainPage
        private byte[] enhPixelData;

        //相关数组
        private WriteableBitmap processImageBitMap;
        private Int32Rect processImageBitmapRect;
        private Int32 processImageStride;
        //图片属性
        private int bytePerPixel = 4;
        private int ImageHeight = 900;
        private int ImageWidth = 1200;

        int functionButtonHeight = 142;
        int functionButtonWidth = 363;
        int functionButtonHeight_Clicked = 156;
        int functionButtonWidth_Clicked = 402;

        //int finishButtonHeight = 197;
        //int finishButtonWidth = 348;
        //int finishButtonHeight_Clicked = 215;
        //int finishButtonWidth_Clicked = 382;

        byte[] fileHeadPart;

        public ResultPage()
        {
            InitializeComponent();

            //初始化图案
            initMainImage();
            enhPixelData = new byte[ImageWidth * ImageHeight * bytePerPixel];
            RefreshImage();
        }
        //get image type and num
        public void SetResultData(byte[] result)
        {
            enhPixelData = result;
            RefreshImage();
        }
        //get image type and num
        public void SetImageNumber(bool _isCharacter, int i)
        {
            isCharacter = _isCharacter;
            imageNum = i;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Uri uri = new Uri(@"pack://siteoforigin:,,,/Image/ResultPageBG.png", UriKind.RelativeOrAbsolute);
            BG.Source = new BitmapImage(uri);
            //uri = new Uri(@"pack://siteoforigin:,,,/Image/border.png", UriKind.RelativeOrAbsolute);
            //Border.Source = new BitmapImage(uri);

            initFunctionButton();


        }


        private void initFunctionButton()
        {
            //create button 
            int ButtonTop = 930;
            int backButtonLeft = 478;


            //Back Button
            Image buttonTemp = new Image();

            buttonTemp.Name = "back";
            buttonTemp.Height = functionButtonHeight;
            buttonTemp.Width = functionButtonWidth;
            buttonTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + buttonTemp.Name + ".png", UriKind.Relative));
            buttonTemp.Stretch = Stretch.Fill;

            Canvas.SetLeft(buttonTemp, backButtonLeft);
            Canvas.SetTop(buttonTemp, ButtonTop);

            buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);
            buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(BackButtonFunction);

            buttonTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
            buttonTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
            buttonTemp.MouseLeave += new MouseEventHandler(FunctionButtonReset);
            ButtonLayer.Children.Add(buttonTemp);


            //email Button
            ButtonTop = 930;
            backButtonLeft = 1078;

            buttonTemp = new Image();

            buttonTemp.Name = "email";
            buttonTemp.Height = functionButtonHeight;
            buttonTemp.Width = functionButtonWidth;
            buttonTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + buttonTemp.Name + ".png", UriKind.Relative));
            buttonTemp.Stretch = Stretch.Fill;

            Canvas.SetLeft(buttonTemp, backButtonLeft);
            Canvas.SetTop(buttonTemp, ButtonTop);

            buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);
            buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(GoVideoButtonFunction);

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

        //刷新图片控件
        private void RefreshImage(bool isClear = false)
        {
            processImageBitMap.WritePixels(processImageBitmapRect, enhPixelData, processImageStride, 0);
        }





        private void ButtonEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void ButtonLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void ButtonEffect(bool isClicked, bool isColorButton, object sender = null)
        {
            if (isClicked)
            {
                PlayClickedSound();
                //function button need to be changed source
                Image temp = (Image)sender;
                string name = temp.Name;
                temp.Height = functionButtonHeight_Clicked;
                temp.Width = functionButtonWidth_Clicked;
                //temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + name + "_Clicked.png", UriKind.Relative));
            }
            else
            {
                //function button need to be changed source
                Image temp = (Image)sender;
                string name = temp.Name;
                temp.Height = functionButtonHeight;
                temp.Width = functionButtonWidth;
                //temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + name + ".png", UriKind.Relative));
            }
        }
        //function button event

        //down
        private void FunctionButtonClickDown(object sender, MouseEventArgs e)
        {
            ButtonEffect(true, false, sender);
        }
        private void FunctionButtonReset(object sender, MouseEventArgs e)
        {
            //if clicked and leave the button, manual reset
            ButtonEffect(false, false, sender);
        }


        //up
        private void BackButtonFunction(object sender, RoutedEventArgs e)
        {
            //button 
            ButtonEffect(false, false, sender);

            Back();
        }
        private void Back()
        {
            //go back to startPage
            StartPage startPage = new StartPage();
            NavigationService.Navigate(startPage, UriKind.Relative);
        }
        //up
        private void GoVideoButtonFunction(object sender, RoutedEventArgs e)
        {
            //button 
            ButtonEffect(false, false, sender);

            GoVideo();
        }
        private void GoVideo()
        {
            //go back to startPage
            EmailPage emailPage = new EmailPage();
            NavigationService.Navigate(emailPage, UriKind.Relative);
            //VideoPage videoPage = new VideoPage();
            //videoPage.SetImageNumber(isCharacter, imageNum);
            NavigationService.Navigate(emailPage, UriKind.Relative);
        }
        private void PlayClickedSound()
        {
            MediaPlayer audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(@"pack://siteoforigin:,,,/Image/Audio/Click.wav", UriKind.RelativeOrAbsolute));
            audioPlayer.Play();
        }

        //close button
        private void CloseFunction(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
