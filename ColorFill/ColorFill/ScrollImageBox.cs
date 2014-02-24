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
using System.Windows.Media.Animation;

namespace ColorFill
{
    enum ImageType
    {
        CHARACTERTYPE,
        SCENERYTYPE,
    }

    class ScrollImageBox
    {
        private ImageType _type;
        private Page _pageUI;
        private Canvas _stackPanel;
        private int _imageNum;
        private int currentImageIndex;
        private int _activeNum = 3;

        private int imageHeight ;
        private int imageWidth ;
        private int interval;

        private float imageHeight_Clicked;
        private float imageWidth_Clicked;

        private float changeRate = 1.02f;
        private float AnimationTime=200f;

        Storyboard storyboard;
        public ScrollImageBox(ImageType type, Page page, Canvas panel, int imageNum)
        {
            _type = type;
            _pageUI = page;
            _stackPanel = panel;
            _imageNum = imageNum;
            currentImageIndex = _activeNum-1;
            CreateImageElement();

        }
        private void CreateImageElement()
        {
            switch (_type)
            {
                case ImageType.CHARACTERTYPE:
                    OpenFileByPath("c");
                    break;
                case ImageType.SCENERYTYPE:
                    OpenFileByPath("s");
                    break;
            }
        }
        private void OpenFileByPath(string name)
        {

            imageHeight = 380;
            imageWidth = 500;
            //if (name == "s")
            //{
            //    imageHeight = 393;
            //    imageWidth = 504;
            //}
            imageHeight_Clicked = imageHeight * changeRate;
            imageWidth_Clicked = imageWidth * changeRate;
            interval = 60;


            for (int i = 0; i < _imageNum; i++)
            {
                Image imageTemp = new Image();
                imageTemp.Name = name + i;
                //imageTemp.Height = imageHeight;
                imageTemp.Width = imageWidth;
                //imageTemp.ActualHeight = 369;
                //.ActualWidth = 505;
                //imageTemp.Source.Height = 369;
                //imageTemp.Source.Width = 505;

                Uri uri = new Uri(
                    @"pack://siteoforigin:,,,/Image/UIResources/FunctionButton/StartPage/" + imageTemp.Name + ".png",
                    UriKind.RelativeOrAbsolute);
                //Uri uri2=new Uri(
                //    "C:\\File\\project\\Unity\\系列项目\\ColorFill v2\\ColorFill\\ColorFill\\bin\\Debug\\Image\\UIResources\\FunctionButton\\StartPage\\c1.png",
                //    UriKind.Absolute);
                imageTemp.Source = new BitmapImage(uri);

                imageTemp.Stretch = Stretch.Fill;

                //imageTemp.Margin = 10;

                Canvas.SetLeft(imageTemp, i * (imageWidth + interval));
                //Canvas.SetTop(imageTemp, ButtonTop);

                imageTemp.MouseLeftButtonUp += new MouseButtonEventHandler(ChooseImageAndLoadMainPage);
                imageTemp.MouseLeftButtonDown += new MouseButtonEventHandler(ImageButtonClickDown);
                imageTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
                imageTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
                //imageTemp.MouseLeave += new MouseEventHandler(ImageButtonReset);

                _stackPanel.Children.Add(imageTemp);
            }
        }

        public bool NextFunction()
        {
            if (currentImageIndex >= _imageNum - 1)
            {
                return false;
            }
            else
            {
                moveTo(1);
                currentImageIndex++;
                return true;
            }
        }

        public bool PreFunction()
        {
            if (currentImageIndex - _activeNum + 1 <= 0)
            {
                return false;
            }
            else
            {
                moveTo(-1);
                currentImageIndex--;
                return true;
            }
            
        }

        private void RunAnimation()
        {
 
        }

        private void RefreshDisplay()
        {
            HiddenAll();
            for (int i = 0; i < _imageNum; i++)
            {
                int index = i + currentImageIndex - 2;
                _stackPanel.Children[index].Visibility = Visibility.Visible;
            }
        }

        private void HiddenAll()
        {
            for (int i = 0; i < _stackPanel.Children.Count; i++)
            {
                _stackPanel.Children[i].Visibility = Visibility.Hidden;
            }
        }
        //-1=>pre
        //1=>next
        private void moveTo(int PreOrNext)
        {
            //Point p = e.GetPosition(body); 
            for (int i = 0; i < _stackPanel.Children.Count; i++)
            {
                Image imageTemp=(Image)_stackPanel.Children[i];

                //Storyboard storyboard = new Storyboard();
                storyboard = new Storyboard();
                //创建X轴方向动画 
                double initPosition = Canvas.GetLeft(imageTemp);
                double destination = initPosition - PreOrNext*(imageWidth + interval * 1f);
                DoubleAnimation doubleAnimation = new DoubleAnimation(
                    initPosition,
                    destination,
                    new Duration(TimeSpan.FromMilliseconds(AnimationTime)),
                    FillBehavior.HoldEnd
                );
                Storyboard.SetTarget(doubleAnimation, imageTemp);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Canvas.Left)"));
                storyboard.Children.Add(doubleAnimation);

                storyboard.Begin();
                //storyboard.GetIsPaused();
                ////指定高度变化的起点,终点与持续时间,并在动画结束时保持大小
                //DoubleAnimation heightAnimation =
                //    new DoubleAnimation(50, 100, new Duration(TimeSpan.FromSeconds(0.8)), FillBehavior.HoldEnd);

                //开始动画
                //变化不是阻塞的,而是异步,所以看上去长度与高度几乎是同时变化
                //imageTemp.BeginAnimation(new DependencyProperty() , doubleAnimation);
                //imageTemp.BeginAnimation( new PropertyPath("(Canvas.Left)"),doubleAnimation);
            }
        }

        private void ChooseImageAndLoadMainPage(object sender, MouseEventArgs e)
        {
            

            ButtonEffect(false, sender);
            MainPage mainPage = new MainPage();
            Image temp=(Image)sender;
            string name = temp.Name;
            if (name[0] == 'c')
            {
                mainPage.SetFloodFillImage(true, Int32.Parse(name[1].ToString()));
                //NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
            }
            else
            {
                mainPage.SetFloodFillImage(false, Int32.Parse(name[1].ToString()));
            }

            _pageUI.NavigationService.Navigate(mainPage, UriKind.Relative);
        }

        private void ImageButtonClickDown(object sender, MouseEventArgs e)
        {
            PlayClickedSound();

            ButtonEffect(true, sender);
        }
        private void ImageButtonReset(object sender, MouseEventArgs e)
        {
            ButtonEffect(false, sender);
        }
        private void ButtonEffect(bool isClicked, object sender)
        {
            if (isClicked)
            {
                Image temp = (Image)sender;
                temp.Width = imageWidth_Clicked;
                temp.Height = imageHeight_Clicked;

                
            }
            else
            {
                Image temp = (Image)sender;
                temp.Width = imageWidth;
                temp.Height = imageHeight;
            }
        }
        //universal event for function button and color button
        //basic
        private void ButtonEnter(object sender, MouseEventArgs e)
        {
            ButtonEffect(true, sender);
            _pageUI.Cursor = Cursors.Hand;
        }
        private void ButtonLeave(object sender, MouseEventArgs e)
        {
            ButtonEffect(false, sender);
            _pageUI.Cursor = Cursors.Arrow;
        }
        private void PlayClickedSound()
        {
            MediaPlayer audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(@"pack://siteoforigin:,,,/Image/Audio/Click.wav", UriKind.RelativeOrAbsolute));
            audioPlayer.Play();
        }
    }
}
