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
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web; 
namespace ColorFill
{


    /// <summary>
    /// ResultPage.xaml 的交互逻辑
    /// </summary>
    public partial class EmailPage : Page
    {
        int functionButtonWidth = 301;
        int functionButtonWidth_Clicked = 329;
        private string condition = "wait";
        private bool isSent = false;
        PromptDialogBox promptDialogBox;
        public EmailPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard keyboard = new Keyboard(this);
            initButton();
            Uri uri = new Uri(@"pack://siteoforigin:,,,/Image/EmailPageBG.png", UriKind.RelativeOrAbsolute);
            BG.Source = new BitmapImage(uri);

            //textAddress.Text = "请输入您的邮件地址";
            //initKeyboard();
        }

        private void SendEmail()
        {
            //通过邮箱发送
            MailMessage myMail = new MailMessage();
            //sender
            myMail.From = new MailAddress("testmailttt@163.com");
            //receiver
            try
            {
                myMail.To.Add(new MailAddress(textAddress.Text));
            }
            catch (FormatException e)
            {
                isSent = false;
                condition = "Incorrect Email" + "\n" + e.Message;
                return;
            }
            
            myMail.Subject = "科技馆:xxx";
            myMail.SubjectEncoding = Encoding.UTF8;
            myMail.Body = "您填色的作品:见附件";
            myMail.BodyEncoding = Encoding.UTF8 ;
            myMail.IsBodyHtml = true;
            
            //OLD
            ////设置邮件的附件，然后加入到mail中
            ////此操作需要有服务器支持:将在客户端选择的附件先上传到服务器保存一个，
            ////string fileName = txtUpFile.PostedFile.FileName.Trim();
            ////实时保存然后再从获取保存好的文件作为附件发送
            //txtUpFile.PostedFile.SaveAs(fileName); // 将文件保存至服务器

            //NEW: 直接使用文件路径
            string fileName = "Photo.jpg";
            try
            {
                //System.IO.Stream ms = new System.IO.MemoryStream(fileName);
                Attachment tempA = new Attachment(fileName);
                myMail.Attachments.Add(tempA);
            }
            catch (Exception e)
            {
                isSent = false;
                condition = "Send Unsuccessfully" + "\n" + e.Message;
            }
            SmtpClient smtp = new SmtpClient("smtp.163.com");
            smtp.EnableSsl = true;
            // Do not send the DefaultCredentials with requests
            smtp.UseDefaultCredentials = false;

            //用户名,密码
            smtp.Credentials = new NetworkCredential("testmailttt", "testmail") as ICredentialsByHost;

            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtp.Timeout = 900000000;
            try
            {
                smtp.Send(myMail);//, userState); ;
                condition = "Send Successfully";
                isSent = true;
            }
            catch (SmtpException e)
            {
                isSent = false;
                condition = "Send Unsuccessfully"+"\n"+e.Message;
            }
        }
        private void RefreshDisplay()
        {
            //textNote.Text = condition;
            promptDialogBox = new PromptDialogBox(condition, isSent, this);
            //promptDialogBox = new PromptDialogBox("successful", true, this);
        }


        //event
        private void SendEmailButtonFuntion(object sender, RoutedEventArgs e)
        {
            Image temp = (Image)sender;
            temp.Width = functionButtonWidth;
            SendEmail();
            RefreshDisplay();
        }
        private void initButton()
        {

            SendButton.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);

            SendButton.MouseLeftButtonUp += new MouseButtonEventHandler(SendEmailButtonFuntion);

            SendButton.MouseEnter += new MouseEventHandler(ButtonEnter);
            SendButton.MouseLeave += new MouseEventHandler(ButtonLeave);
            SendButton.MouseLeave += new MouseEventHandler(FunctionButtonReset);

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
                temp.Width = functionButtonWidth_Clicked;
                //temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + name + "_Clicked.png", UriKind.Relative));
            }
            else
            {
                //function button need to be changed source
                Image temp = (Image)sender;
                string name = temp.Name;
                temp.Width = functionButtonWidth;
                //temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/ResultPage/" + name + ".png", UriKind.Relative));
            }
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
