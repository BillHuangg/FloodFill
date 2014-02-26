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
    class PromptDialogBox
    {
        string promptMessage;
        bool isSent;
        EmailPage _pageUI;
        private Timer parentTimer;
        delegate void UpdateTimer();
        private static int WAITTIME = 3;
        private int intervalTime = 1000;        //update time 1s
        private int remainingTime = WAITTIME;   // 30s
        private bool isStartingCountDown = true;

        public PromptDialogBox(string m,bool isSuccess,EmailPage page)
        {
            promptMessage = m;
            isSent = isSuccess;
            _pageUI = page;
            DisplayDialogBox();
            SetUpTimer();
        }
        private void DisplayDialogBox()
        {
            _pageUI.DialogBoxLayer.Visibility = Visibility.Visible;

            _pageUI.textAddress.Text = promptMessage;

            ///全部隐藏
            _pageUI.successImage.Visibility = Visibility.Hidden;
            _pageUI.failImage.Visibility = Visibility.Hidden;

            //
            if (isSent)
            {

                _pageUI.successImage.Visibility = Visibility.Visible;
            }
            else
            {
                _pageUI.failImage.Visibility = Visibility.Visible;
            }
            
        }
        private void HideDialogBox()
        {
            _pageUI.DialogBoxLayer.Visibility = Visibility.Hidden;
            _pageUI.textAddress.Text = "请重新输入您的邮件地址";

            ///全部隐藏
            _pageUI.successImage.Visibility = Visibility.Hidden;
            _pageUI.failImage.Visibility = Visibility.Hidden;
            
        }
        private void SetUpTimer()
        {
            intervalTime = 1000;  //update time 1s
            remainingTime = WAITTIME;   // 30s
            isStartingCountDown = true;
            parentTimer = new Timer(new TimerCallback(OnTimedEvent));
            //每秒执行一次
            parentTimer.Change(0, intervalTime);
        }

        private void OnTimedEvent(object state)
        {
            _pageUI.Dispatcher.BeginInvoke(new UpdateTimer(Update));
        }

        private void Update()
        {
            if (isStartingCountDown)
            {
                remainingTime--;
            }
            if (remainingTime <= 0)
            {
                isStartingCountDown = false;
                HideDialogBox();
                parentTimer.Dispose();
                if (isSent)
                {
                    GoBackMainPage();
                }
                else
                {
                    //stay at current page
                    _pageUI.textAddress.Text = "请重新输入您的邮件地址";
                }
            }
        }
        private void GoBackMainPage()
        {
            StartPage startPage = new StartPage();

            ////跳转
            _pageUI.NavigationService.Navigate(startPage, UriKind.Relative);
        }
    }
}
