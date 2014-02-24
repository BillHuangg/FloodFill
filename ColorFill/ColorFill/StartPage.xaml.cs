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
    /// <summary>
    /// StartPage.xaml 的交互逻辑
    /// </summary>
    public partial class StartPage : Page
    {

        ScrollImageBox characterBox;
        ScrollImageBox sceneryBox;

        int functionButtonWidth = 140;
        int functionButtonWidth_Clicked = 154;

        bool isLodingOver = false;
        public StartPage()
        {
            InitializeComponent();
            InitElement();
            isLodingOver = true;
        }
        private void InitElement()
        {
            Uri uri = new Uri(
                    @"pack://siteoforigin:,,,/Image/StartPageBG.png",
                    UriKind.RelativeOrAbsolute);
            BG.Source = new BitmapImage(uri);

            characterBox = new ScrollImageBox(ImageType.CHARACTERTYPE, this, CharacterCanvas, 10);
            sceneryBox = new ScrollImageBox(ImageType.SCENERYTYPE, this, SceneryCanvas, 10);

            initButton();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public bool isInitOver()
        {
            return isLodingOver;
        }

        private void initButton()
        {
            for (int i = 0; i < FunctionButtonLayer.Children.Count; i++)
            {
                Image imageTemp = (Image)FunctionButtonLayer.Children[i];
                string[] name = imageTemp.Name.Split('s');
                imageTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);

                if (name[2] == "1")
                {
                    if (name[1] == "Pre")
                    {
                        imageTemp.MouseLeftButtonUp += new MouseButtonEventHandler(Character_pre_Click);
                    }
                    else
                    {
                        imageTemp.MouseLeftButtonUp += new MouseButtonEventHandler(Character_next_Click);
                    }
                }
                else
                {
                    if (name[1] == "Pre")
                    {
                        imageTemp.MouseLeftButtonUp += new MouseButtonEventHandler(Scenery_pre_Click);
                    }
                    else
                    {
                        imageTemp.MouseLeftButtonUp += new MouseButtonEventHandler(Scenery_next_Click);
                    }
                }
                imageTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
                imageTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
                imageTemp.MouseLeave += new MouseEventHandler(FunctionButtonReset);
            }
        }
        private void ButtonEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void ButtonLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void FunctionButtonClickDown(object sender, MouseEventArgs e)
        {
            PlayClickedSound();

            Image temp = (Image)sender;
            temp.Width = functionButtonWidth_Clicked;
        }
        private void FunctionButtonReset(object sender, MouseEventArgs e)
        {
            Image temp = (Image)sender;
            temp.Width = functionButtonWidth;
        }
        private void Character_next_Click(object sender, RoutedEventArgs e)
        {
            characterBox.NextFunction();
            Image temp = (Image)sender;
            temp.Width = functionButtonWidth;
        }
        private void Character_pre_Click(object sender, RoutedEventArgs e)
        {
            characterBox.PreFunction();
            Image temp = (Image)sender;
            temp.Width = functionButtonWidth;
        }
        private void Scenery_next_Click(object sender, RoutedEventArgs e)
        {
            sceneryBox.NextFunction();
            Image temp = (Image)sender;
            temp.Width = functionButtonWidth;
        }
        private void Scenery_pre_Click(object sender, RoutedEventArgs e)
        {
            sceneryBox.PreFunction();
            Image temp = (Image)sender;
            temp.Width = functionButtonWidth;
        }

        private void PlayClickedSound()
        {
            MediaPlayer audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(@"pack://siteoforigin:,,,/Image/Audio/Click.wav", UriKind.RelativeOrAbsolute));
            audioPlayer.Play();
        }

        private void CloseFunction(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }













        //private void testButton_Click(object sender, RoutedEventArgs e)
        //{
        //    //MainWindow temp = new MainWindow();
        //    //temp.SetFloodFillImage(true, 1);

        //    //temp.Show();
        //    //this.Close();
        //    //(Frame)this.Parent.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        //    //frame.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        //    MainPage mainPage = new MainPage();
        //    mainPage.SetFloodFillImage(true, 3);
        //    //NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        //    NavigationService.Navigate(mainPage, UriKind.Relative);
        //}











        //private void moveTo(Point deskPoint)
        //{
        //    //Point p = e.GetPosition(body); 

        //    Point curPoint = new Point();
        //    curPoint.X = Canvas.GetLeft(ell);
        //    curPoint.Y = Canvas.GetTop(ell);

        //    double _s = System.Math.Sqrt(Math.Pow((deskPoint.X - curPoint.X), 2) + Math.Pow((deskPoint.Y - curPoint.Y), 2));

        //    double _secNumber = (_s / 1000) * 500;

        //    Storyboard storyboard = new Storyboard();

        //    //创建X轴方向动画 

        //    DoubleAnimation doubleAnimation = new DoubleAnimation(

        //      Canvas.GetLeft(ell),

        //      deskPoint.X,

        //      new Duration(TimeSpan.FromMilliseconds(_secNumber))

        //    );
        //    Storyboard.SetTarget(doubleAnimation, ell);
        //    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Canvas.Left)"));
        //    storyboard.Children.Add(doubleAnimation);

        //    //创建Y轴方向动画 

        //    doubleAnimation = new DoubleAnimation(
        //      Canvas.GetTop(ell),
        //      deskPoint.Y,
        //      new Duration(TimeSpan.FromMilliseconds(_secNumber))
        //    );
        //    Storyboard.SetTarget(doubleAnimation, ell);
        //    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Canvas.Top)"));
        //    storyboard.Children.Add(doubleAnimation);
        //    storyboard.Begin(); 
        //}
        

 
    }
}
