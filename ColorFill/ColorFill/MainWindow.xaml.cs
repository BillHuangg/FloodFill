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
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading;
namespace ColorFill
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Timer parentTimer;

        //delegate void UpdateTimer();

        //StartPage startPage;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ////SetUpTimer();
            ////frame.Navigate(new Uri("StartPage.xaml", UriKind.Relative));
            //startPage = new StartPage();
            ////LoadingPage loadingPage = new LoadingPage();

            //this.frame.NavigationService.Navigate(startPage, UriKind.Relative);
            ////loadingPage.setElement(startPage);
            
           
            ////LoadingPage loadingPage = new LoadingPage();
            
            
            
        }
        //private void SetUpTimer()
        //{
        //    parentTimer = new Timer(new TimerCallback(OnTimedEvent));
        //    //每秒执行一次
        //    parentTimer.Change(0, 1);
        //}

        //private void OnTimedEvent(object state)
        //{
        //    this.Dispatcher.BeginInvoke(new UpdateTimer(Update));
        //}

        //private void Update()
        //{
        //    if (startPage.isInitOver())
        //    {
        //        parentTimer.Dispose();
        //        this.frame.NavigationService.Navigate(startPage, UriKind.Relative);
                
                
        //    }
        //}
    }
}
