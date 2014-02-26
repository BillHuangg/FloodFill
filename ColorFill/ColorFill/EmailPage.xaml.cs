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
using System.IO;
namespace ColorFill
{


    /// <summary>
    /// ResultPage.xaml 的交互逻辑
    /// </summary>
    public partial class EmailPage : Page
    {
        int functionButtonWidth = 301;
        int functionButtonWidth_Clicked = 329;
        private string condition = "";
        private bool isSent = false;
        PromptDialogBox promptDialogBox;

        string[] MailAddress;
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


            MailAddress = new string[5];


            LoadFile();
            //textAddress.Text = "请输入您的邮件地址";
            //initKeyboard();
        }

        private void LoadFile()
        {
            string pathFile = "MainMailAddress.txt";
            StreamReader sr = new StreamReader(pathFile);

            String tempLine;
            for (int i = 0; i < MailAddress.Count(); i++)
            {
                if ((tempLine = sr.ReadLine()) != null)
                {
                    MailAddress[i] = tempLine;
                }
            }
            sr.Close();
        }
        private void SaveFile(string message)
        {
            string pathFile = "MailDebugLog.txt";
            StreamWriter sw = new StreamWriter(pathFile, false);
            sw.WriteLine(message);
            sw.Close();
        }
        private void SendEmail()
        {
            //通过邮箱发送
            MailMessage myMail = new MailMessage();
            //sender
            myMail.From = new MailAddress(MailAddress[0]);//"testmailttt@163.com");//"cd@sstm.org.cn");//"testmailttt@163.com");
            //receiver
            try
            {
                myMail.To.Add(new MailAddress(textAddress.Text));
            }
            catch (FormatException e)
            {
                isSent = false;
                condition = "您的邮件发送失败 " + "\n" + "\n" + e.Message;
                SaveFile(condition);
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
                condition = "请重试,您的邮件发送失败";// + "\n" + e.Message;
            }
            SmtpClient smtp = new SmtpClient(MailAddress[1]);//"smtp.sstm.org.cn");//smtp.sstm-adex-01.sstm.org.cn");//"smtp.163.com");
            smtp.EnableSsl = true;
            // Do not send the DefaultCredentials with requests
            smtp.UseDefaultCredentials = false;

            //用户名,密码
            smtp.Credentials = new NetworkCredential(MailAddress[2], MailAddress[3]) as ICredentialsByHost;//("cd","654321") as ICredentialsByHost;//

            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtp.Timeout = Int32.Parse(MailAddress[4]);
            try
            {
                smtp.Send(myMail);//, userState); ;
                condition = "您的邮件已发送成功";
                isSent = true;
            }
            catch (SmtpException e)
            {
                isSent = false;
                condition = "请重试,您的邮件发送失败 "+ e.Message+"  : "+e.InnerException;
                SaveFile(condition);

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

        private void BackButtonFunction(object sender, RoutedEventArgs e)
        {
            StartPage startPage = new StartPage();

            ////跳转
            this.NavigationService.Navigate(startPage, UriKind.Relative);
        }

        private void initButton()
        {

            SendButton.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);

            SendButton.MouseLeftButtonUp += new MouseButtonEventHandler(SendEmailButtonFuntion);

            SendButton.MouseEnter += new MouseEventHandler(ButtonEnter);
            SendButton.MouseLeave += new MouseEventHandler(ButtonLeave);
            SendButton.MouseLeave += new MouseEventHandler(FunctionButtonReset);


            ///
            BackButton.MouseLeftButtonDown += new MouseButtonEventHandler(FunctionButtonClickDown);

            BackButton.MouseLeftButtonUp += new MouseButtonEventHandler(BackButtonFunction);

            BackButton.MouseEnter += new MouseEventHandler(ButtonEnter);
            BackButton.MouseLeave += new MouseEventHandler(ButtonLeave);
            BackButton.MouseLeave += new MouseEventHandler(FunctionButtonReset);
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
