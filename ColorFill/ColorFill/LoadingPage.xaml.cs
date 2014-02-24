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
using System.Windows.Threading;
using System.Threading;
namespace ColorFill
{
    /// <summary>
    /// LoadingPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingPage : Page
    {
        private Timer parentTimer;

        delegate void UpdateTimer();

        StartPage startPage;

        public LoadingPage()
        {
            InitializeComponent();
        }
        public void setElement(StartPage _startPage)
        {
            startPage = _startPage;
            
        }
        private void SetUpTimer()
        {
            parentTimer = new Timer(new TimerCallback(OnTimedEvent));
            //每秒执行一次
            parentTimer.Change(0, 90000);
        }

        private void OnTimedEvent(object state)
        {
            this.Dispatcher.BeginInvoke(new UpdateTimer(Update));
        }

        private void Update()
        {
            if (startPage!=null&&startPage.isInitOver())
            {
                
                this.NavigationService.Navigate(startPage, UriKind.Relative);
                parentTimer.Dispose();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetUpTimer();
            startPage = new StartPage();
            //LoadingPage loadingPage = new LoadingPage();

           // this.NavigationService.Navigate(startPage, UriKind.Relative);
            
        }
    }
}
