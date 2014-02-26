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
    class Keyboard
    {
        private EmailPage _pageUI;
        string[] KeyLayout1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "dot", };
        string[] KeyLayout2 = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "delete", };
        string[] KeyLayout3 = { "a", "s", "d", "f", "g", "h", "j", "k", "l", "enter", "999", };
        string[] KeyLayout4 = { "caps", "z", "x", "c", "v", "b", "n", "m", "UnderLine", "MidLine", "at", };

        delegate void deleMethod(object sender, MouseEventArgs e);

        private bool capsButtonIsClicked = false;

        public Keyboard(EmailPage page)
        {
            _pageUI = page;
            _pageUI.textAddress.Text = "请输入您的邮件地址";
            initKeyboard();
        }

        private void initKeyboard()
        {
            List<string[]> layoutList = new List<string[]>();
            layoutList.Add(KeyLayout1);
            layoutList.Add(KeyLayout2);
            layoutList.Add(KeyLayout3);
            layoutList.Add(KeyLayout4);
            for (int i = 0; i < layoutList.Count; i++)
            {
                string[] temp = layoutList[i];
                for (int j = 0; j < temp.Count(); j++)
                {
                    Create(temp[j], i, j);
                }
            }

        }

        private void Create(string name, int row, int col)
        {
            if (name == "caps")
            {
                CreateButton(name, row, col, new deleMethod(CapsButtonFunction));
            }
            else if (name == "delete")
            {
                CreateButton(name, row, col, new deleMethod(DeleteButtonFunction));
            }
            else if (name == "enter")
            {
                CreateButton(name, row, col, new deleMethod(EnterButtonFunction));
            }
            else if (name == "MidLine")
            {
                CreateButton(name, row, col, new deleMethod(MidLineButtonFunction));
            }
            else if (name == "UnderLine")
            {
                CreateButton(name, row, col, new deleMethod(UnderLineButtonFunction));
            }
            else if (name == "at")
            {
                CreateButton(name, row, col, new deleMethod(AtButtonFunction));
            }
            else if (name == "dot")
            {
                CreateButton(name, row, col, new deleMethod(DotssButtonFunction));
            }
            else if (name != "999")
            {
                CreateButton(name, row, col, new deleMethod(NormalButtonFunction));
            }

        }
        private int keyWidth = 68;
        private int keyHeight = 51;
        private int interval = 9;
        private int heightInterval = 7;
        private void CreateButton(string name, int row, int col, deleMethod clickedFunction)
        {
            ////Back Button
            Image buttonTemp = new Image();

            if (KeyLayout1.Contains(name))
            {
                buttonTemp.Name = "_" + name;
            }
            else
            {
                buttonTemp.Name = name;
            }
            buttonTemp.Tag = name;

            buttonTemp.Height = keyHeight;
            buttonTemp.Width = keyWidth;

            buttonTemp.Source = new BitmapImage(new Uri(@"Image/Keyboard/notekey.png", UriKind.Relative));
            buttonTemp.Stretch = Stretch.Fill;
            buttonTemp.Opacity = 0;
            if (row == 2)
            {
                Canvas.SetLeft(buttonTemp, 24 + interval + (keyWidth + interval) * col);
                Canvas.SetTop(buttonTemp, 30 + heightInterval + (keyHeight + heightInterval) * row);
            }
            else
            {
                Canvas.SetLeft(buttonTemp, interval + (keyWidth + interval) * col);
                Canvas.SetTop(buttonTemp, 30 + heightInterval + (keyHeight + heightInterval) * row);
            }

            buttonTemp.MouseLeftButtonUp += new MouseButtonEventHandler(clickedFunction);
            buttonTemp.MouseLeftButtonDown += new MouseButtonEventHandler(ClickedSound);
            buttonTemp.MouseEnter += new MouseEventHandler(ButtonEnter);
            buttonTemp.MouseLeave += new MouseEventHandler(ButtonLeave);
            _pageUI.KeyboardLayer.Children.Add(buttonTemp);
        }

        private void ButtonEnter(object sender, MouseEventArgs e)
        {
            _pageUI.Cursor = Cursors.Hand;
        }
        private void ButtonLeave(object sender, MouseEventArgs e)
        {
            _pageUI.Cursor = Cursors.Arrow;
        }

        ////////keyboard function event
        private void CapsButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();
            capsButtonIsClicked = !capsButtonIsClicked;
            if (capsButtonIsClicked)
            {
                _pageUI.BGKeyboard.Source=new BitmapImage(new Uri(@"Image/Keyboard/TextCapital.png", UriKind.Relative));
            }
            else
            {
                _pageUI.BGKeyboard.Source = new BitmapImage(new Uri(@"Image/Keyboard/TextLower.png", UriKind.Relative));
            }
        }
        private void DotssButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();
            _pageUI.textAddress.Text += ".";
        }
        private void MidLineButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();
            _pageUI.textAddress.Text += "-";
        }
        private void UnderLineButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();
            _pageUI.textAddress.Text += "_";
        }
        private void AtButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();
            _pageUI.textAddress.Text += "@";
        }
        private void EnterButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();
        }
        private void DeleteButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();

            string temp = _pageUI.textAddress.Text;
            if (temp.Count() >= 1)
            {
                string result = temp.Remove(temp.Count() - 1, 1);
                _pageUI.textAddress.Text = result;
            }
        }

        private void NormalButtonFunction(object sender, MouseEventArgs e)
        {
            FocusSetBlank();
            Image temp = (Image)sender;
            //string[] nameS=temp.Name.Split('s');
            string name = temp.Name;
            if (name[0] == '_')
            {
                //number
                _pageUI.textAddress.Text += (string)temp.Tag;
            }
            else
            {
                if (capsButtonIsClicked)
                {
                    _pageUI.textAddress.Text += ((string)temp.Tag).ToUpper();
                }
                else
                {
                    _pageUI.textAddress.Text += ((string)temp.Tag);
                }
            }
        }

        private void FocusSetBlank()
        {
            if (_pageUI.textAddress.Text == "请输入您的邮件地址"
                || _pageUI.textAddress.Text == "请重新输入您的邮件地址")
            {
                _pageUI.textAddress.Text = "";
            }
        }
        private void ClickedSound(object sender, MouseEventArgs e)
        {
            PlayClickedSound();
        }
        private void PlayClickedSound()
        {
            MediaPlayer audioPlayer = new MediaPlayer();
            audioPlayer.Open(new Uri(@"pack://siteoforigin:,,,/Image/Audio/Click.wav", UriKind.RelativeOrAbsolute));
            audioPlayer.Play();
        }
    }
}
