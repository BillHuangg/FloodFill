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
using System.IO;
using System.Runtime.InteropServices;
namespace ColorFill
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {

        private void PlayClickedSound()
        {
            MediaPlayer audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(@"pack://siteoforigin:,,,/Image/Audio/Click.wav", UriKind.RelativeOrAbsolute));
            audioPlayer.Play();
        }


        //universal event for function button and color button
        //basic
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
                //is clicked, enhanced
                if (isColorButton)
                {
                    Image temp = (Image)sender;
                    temp.Width = colorButtonWidth_Clicked;
                    temp.Height = colorButtonHeight_Clicked;
                }
                else
                {
                    //function button need to be changed source
                    Image temp = (Image)sender;
                    string name = temp.Name;

                    if (name != "finish")
                    {
                        temp.Height = functionButtonHeight_Clicked;
                        temp.Width = functionButtonWidth_Clicked;
                        temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/MainPage/" + name + "_Clicked.png", UriKind.Relative));
                    }
                    else
                    {
                        temp.Height = finishButtonHeight_Clicked;
                        temp.Width = finishButtonWidth_Clicked;
                        temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/MainPage/" + name + "_Clicked.png", UriKind.Relative));
                    }
                }
            }
            else
            {
                //turn into normal
                if (isColorButton)
                {
                    Image temp = (Image)sender;
                    temp.Width = colorButtonWidth;
                    temp.Height = colorButtonHeight;
                }
                else
                {
                    //function button need to be changed source
                    Image temp = (Image)sender;
                    string name = temp.Name;

                    if (name != "finish")
                    {
                        temp.Height = functionButtonHeight;
                        temp.Width = functionButtonWidth;
                        temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/MainPage/" + name + ".png", UriKind.Relative));
                    }
                    else
                    {
                        temp.Height = finishButtonHeight;
                        temp.Width = finishButtonWidth;
                        temp.Source = new BitmapImage(new Uri(@"Image/UIResources/FunctionButton/MainPage/" + name + ".png", UriKind.Relative));
                    }
                }
            }
        }


        

        //color button interaction
        //up
        private void ColorButton_Click(object sender, MouseEventArgs e)
        {
            //button 
            ButtonEffect(false,true, sender);

            //run area fill function
            ClickFill(sender);
        }
        private void ColorButtonReset(object sender, MouseEventArgs e)
        {
            //if clicked and leave the button, manual reset
            ButtonEffect(false, true, sender);
        }
        //down
        private void ColorButtonClickDown(object sender, MouseEventArgs e)
        {
            ButtonEffect(true, true, sender);
        }
        private void ClickFill(object sender)
        {

            //get the name for color 
            Image temp = (Image)sender;
            int colorName = Int32.Parse(temp.Name.ToString().Split('s')[1]);
            Color newColor = new Color();
            switch (colorName)
            {
                case 0:
                    newColor = Color0;
                    break;
                case 1:
                    newColor = Color1;
                    break;
                case 2:
                    newColor = Color2;
                    break;
                case 3:
                    newColor = Color3;
                    break;
                case 4:
                    newColor = Color4;
                    break;
                case 5:
                    newColor = Color5;
                    break;
                case 6:
                    newColor = Color6;
                    break;
                case 7:
                    newColor = Color7;
                    break;
                case 8:
                    newColor = Color8;
                    break;
                case 9:
                    newColor = Color9;
                    break;
                case 10:
                    newColor = Color10;
                    break;
                case 11:
                    newColor = Color11;
                    break;
            }
            if (!isFirstCilck)
            {
                lastClickColor = newColor;
                FillColor(lastMousePoint, newColor);
            }
        }

        //鼠标点击图案,获取当前相对于图案的位置
        bool isFirstCilck = true;
        private void FloodFillImage_GetMousePosition(object sender, MouseButtonEventArgs e)
        {
            
            Point mousePoint = e.GetPosition((Image)sender);

            int index = GetPixelPosition((int)mousePoint.X, (int)mousePoint.Y);

            Color clickPointColor = new Color();
            clickPointColor.B = enhPixelData[index];
            clickPointColor.G = enhPixelData[index + 1];
            clickPointColor.R = enhPixelData[index + 2];
            //tempColor.A = enhPixelData[index + 3];

            if (!clickPointColor.Equals(highLightColor))
            {
                
                if (!isFirstCilck)
                {
                    //恢复前一个点选区域的颜色
                    int newIndex = GetPixelPosition((int)lastMousePoint.X, (int)lastMousePoint.Y);
                    Color newColorOfLastPoint = new Color();
                    newColorOfLastPoint.B = enhPixelData[newIndex];
                    newColorOfLastPoint.G = enhPixelData[newIndex + 1];
                    newColorOfLastPoint.R = enhPixelData[newIndex + 2];

                    //
                    if (newColorOfLastPoint.Equals(highLightColor) || !newColorOfLastPoint.Equals(lastClickColor))
                    {
                        //恢复前一个点选区域的颜色
                        FillGray(lastMousePoint, lastColor);
                    }
                    
                }
                //高亮目前区域颜色
                FillGray(mousePoint, highLightColor);

                lastMousePoint = mousePoint;
                lastColor = clickPointColor;
                isFirstCilck = false;
            }
            else
            {
                //nothing
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
        private void ResetButtonFunction(object sender, RoutedEventArgs e)
        {
            //button 
            ButtonEffect(false, false, sender);
            Reset();
        }
        private void Reset()
        {
            //reset the first click for fill function
            isFirstCilck = true;

            //reset the whole data
            RefreshImage(true);
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
        private void FinishButtonFunction(object sender, RoutedEventArgs e)
        {
            //button 
            ButtonEffect(false, false, sender);

            //////
            SaveFunction();
            //RefreshDisplay();
            Finish();
            
        }
        private void Finish()
        {
            //finish and go to email page
            //SaveFunction();
            //EmailPage resultPage = new EmailPage();
            //this.NavigationService.Navigate(resultPage, UriKind.Relative);

            //finish and go to result page
            ResultPage resultPage = new ResultPage();
            resultPage.SetResultData(enhPixelData);
            resultPage.SetImageNumber(isCharacter, imageNum);
            this.NavigationService.Navigate(resultPage, UriKind.Relative);
            
        }
        private string condition = "wait";
        private void SaveFunction()
        {
            //byte[] result = new byte[enhPixelData.Length + 72];
            //Array.Copy(fileHeadPart, 0, result, 0, fileHeadPart.Length);
            //恢复头部格式数据
           // Array.Copy(enhPixelData, 0, result, 71, enhPixelData.Length);

            //string path = "ResultImage/result.jpg";
            //try
            //{
            //    System.IO.File.WriteAllBytes(path, result);
            //}
            //catch (Exception e)
            //{
            //    condition = "first : " + e.Message;
            //}

            //FileStream pFileStream = null;
            //pFileStream = new FileStream("ResultImage/result.jpg", FileMode.OpenOrCreate);

            //pFileStream.Write(result, 0, result.Length);


            try
            {
                //System.IO.Stream ms = new System.IO.MemoryStream(result);
                //ms.Position = 0;
                //System.Drawing.Image image = System.Drawing.Image.FromStream(ms,false);
                //image.Save("ResultImage/Photo.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //ms.Close();

                System.Drawing.Bitmap b = new System.Drawing.Bitmap(1200, 900);
                var bits = b.LockBits(new System.Drawing.Rectangle(0, 0, 1200, 900), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);// .ImageFormat.Jpeg);
                //Random rand = new Random();
                //var pixels = Enumerable.Range(1, 1200 * 900).Select(n => rand.Next()).ToArray();
                Marshal.Copy(enhPixelData, 0, bits.Scan0, 1200 * 900 * 4);
                b.UnlockBits(bits);
                // use the image ...
                b.Save("Photo.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                condition += "\n" + "third : " + e.Message;
            }

        }
        private void RefreshDisplay()
        {
            //textNote.Text = condition;
        }
        //close button
        private void CloseFunction(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }



        //private void save()
        //{
        //    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(@"Photo.bmp");
        //    //新建第二个bitmap类型的bmp2变量 
        //    System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(400, 300, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
        //    //将第一个bmp拷贝到bmp2中
        //    System.Drawing.Graphics draw = System.Drawing.Graphics.FromImage(bmp2);

        //    draw.DrawImage(bmp, 0, 0,400, 300);
          

        //    bmp.Save(@"test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        //    //filePanel.BackgroundImage = (Image)bmp2;//读取bmp2到picturebox
        //    //draw.Dispose();
        //    // bmp.Dispose();//释放bmp文件资源
        //}
        //private void DeleteDataFile()
        //{
        //    string path = @"Photo.bmp";
        //    System.IO.File.Delete(path);
        //}
    }

}
