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
    /// VideoPage.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPage : Page
    {
        //get value from last page
        private bool isCharacter = false;
        private int imageNum = 0;

        int functionButtonHeight = 142;
        int functionButtonWidth = 363;
        int functionButtonHeight_Clicked = 156;
        int functionButtonWidth_Clicked = 402;

        private string condition = "wait";

        public VideoPage()
        {
            InitializeComponent();
        }

        //get image type and num
        public void SetImageNumber(bool _isCharacter, int i)
        {
            isCharacter = _isCharacter;
            imageNum = i;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Uri uri = new Uri(@"pack://siteoforigin:,,,/Image/MainPageBG.png", UriKind.RelativeOrAbsolute);
            BG.Source = new BitmapImage(uri);
            //uri = new Uri(@"pack://siteoforigin:,,,/Image/border.png", UriKind.RelativeOrAbsolute);
            //Border.Source = new BitmapImage(uri);
            try
            {
                uri = new Uri(@"pack://siteoforigin:,,,/Image/Video/video.mp4", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ie)
            {
                condition += ie.Message;
            }
            try
            {
                mediaElement1.Source = uri;
                mediaElement1.Play();
            }
            catch (Exception ie)
            {
                condition += ie.Message;
            }
            //RefreshDisplay();
            initFunctionButton();


        }
        private void RefreshDisplay()
        {
            textNote.Text = condition;
        }

        private void initFunctionButton()
        {
            //create button 
            int ButtonTop = 930;
            int backButtonLeft = 478;
            Image buttonTemp = new Image();

            ////Back Button
            

            //buttonTemp.Name = "back";
            //buttonTemp.Height = functionButtonHeight;
            //buttonTemp.Width = functionButtonWidth;
            //buttonTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + buttonTemp.Name + ".png", UriKind.Relative));
            //buttonTemp.Stretch = Stretch.Fill;

            //Canvas.SetLeft(buttonTemp, backButtonLeft);
            //Canvas.SetTop(buttonTemp, ButtonTop);

            //buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);
            //buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(BackButtonFunction);

            //buttonTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
            //buttonTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
            //buttonTemp.MouseLeave += new MouseEventHandler(FunctionButtonReset);
            //ButtonLayer.Children.Add(buttonTemp);


            //email Button
            ButtonTop = 930;
            backButtonLeft = 778;

            buttonTemp = new Image();

            buttonTemp.Name = "email";
            buttonTemp.Height = functionButtonHeight;
            buttonTemp.Width = functionButtonWidth;
            buttonTemp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + buttonTemp.Name + ".png", UriKind.Relative));
            buttonTemp.Stretch = Stretch.Fill;

            Canvas.SetLeft(buttonTemp, backButtonLeft);
            Canvas.SetTop(buttonTemp, ButtonTop);

            buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);
            buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(GoEmailButtonFunction);

            buttonTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
            buttonTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
            buttonTemp.MouseLeave += new MouseEventHandler(FunctionButtonReset);
            ButtonLayer.Children.Add(buttonTemp);



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
        private void GoEmailButtonFunction(object sender, RoutedEventArgs e)
        {
            //button 
            ButtonEffect(false, false, sender);

            GoEmail();
        }
        private void GoEmail()
        {
            //go back to startPage
            EmailPage emailPage = new EmailPage();
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
