using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IBStegano
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Fano.Sort();
            Fano.Create(0, Fano.Alpha.Length-1);
        }

        private string SecretTextToBin(string secrettext)
        {
            string binaryasc = "";
            foreach (char c in secrettext.ToUpper())
            {
                binaryasc += Fano.Res[Array.IndexOf(Fano.Alpha, c)];
            }
            return binaryasc+Fano.Res[Array.IndexOf(Fano.Alpha, 'Ѓ')];
        }

        private string SecretTextFromBin(string bin)
        {
            string Decoded = "";
            for (int i = 0; i < bin.Length; i++)
            {
                for (int j = 0; j<Fano.Res.Length; j++)
                {
                    try
                    {
                        if (bin.Substring(i, Fano.Res[j].Length) == Fano.Res[j])
                        {
                            if (Fano.Alpha[j] == 'Ѓ') return Decoded.ToLower();
                            Decoded += Fano.Alpha[j];
                            i += Fano.Res[j].Length - 1;
                            break;
                        }
                    }
                    catch { }
                }
            }
            return Decoded.ToLower();
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            switch (methodCombo.SelectedIndex)
            {
                case 0:
                    richTextBox2.Text = DoubleSpace(richTextBox1.Text, textBox1.Text, true);
                    break;
                case 1:
                    richTextBox2.Text = DifferentSpace(richTextBox1.Text, textBox1.Text, true);
                    break;
                case 2:
                    richTextBox2.Text = EndSpace(richTextBox1.Text, textBox1.Text, true);
                    break;
                case 3:
                    richTextBox2.Text = LettersHide(richTextBox1.Text, textBox1.Text, true);
                    break;
                case 4:
                    richTextBox2.Text = MyMethod(richTextBox1.Text, textBox1.Text, true);
                    break;
            }

            textBox1.Text = "";
        }

        private string DoubleSpace(string text, string secrettext, bool mode)
        {
            string result = "";
            if (mode)
            {
                
                string binaryasc = SecretTextToBin(secrettext);

                if (binaryasc.Length > text.Count(x => x.Equals(' ')))
                {
                    return "Необходимо, чтобы текст содержал не менее " + binaryasc.Length + " пробелов";
                }


                string[] wosp = text.Split(' ');


                for (int i = 0; i < binaryasc.Length; i++)
                {
                    if (binaryasc[i] == '0') wosp[i] += " ";
                    else if (binaryasc[i] == '1') wosp[i] += "  ";
                }

                for (int i = binaryasc.Length; i < wosp.Length - 1; i++)
                {
                    wosp[i] += ' ';
                }


                result = String.Join("", wosp);
            }
            else
            {
                string binaryasc = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        if (text[i + 1] == ' ') { binaryasc += '1'; i++; }
                        else binaryasc += '0';
                    }
                }
                result = SecretTextFromBin(binaryasc);
            }
            return result;
        }

        private string DifferentSpace(string text, string secrettext, bool mode)
        {
            string result = "";
            if (mode)
            {
                string binaryasc = SecretTextToBin(secrettext);

                if (binaryasc.Length > text.Count(x => x.Equals(' ')))
                {
                    return "Необходимо, чтобы текст содержал не менее " + binaryasc.Length + " пробелов";
                }


                string[] wosp = text.Split(' ');


                for (int i = 0; i < binaryasc.Length; i++)
                {
                    if (binaryasc[i] == '0') wosp[i] += " ";
                    else if (binaryasc[i] == '1') wosp[i] += (char)160;
                }

                for (int i = binaryasc.Length; i < wosp.Length - 1; i++)
                {
                    wosp[i] += ' ';
                }


                result = String.Join("", wosp);
            }
            else
            {
                string binaryasc = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        binaryasc += '0';
                    }
                    else if (text[i] == (char)160) binaryasc += '1';
                }
                result = SecretTextFromBin(binaryasc);
            }
            return result;
        }

        private string EndSpace(string text, string secrettext, bool mode)
        {
            string result = "";
            if (mode)
            {
                string binaryasc = SecretTextToBin(secrettext);

                if (binaryasc.Length > text.Count(x => x.Equals('\n')))
                {
                    return "Необходимо, чтобы текст содержал не менее " + binaryasc.Length + " строк";
                }


                string[] wosp = text.Split('\n');


                for (int i = 0; i < binaryasc.Length; i++)
                {
                    if (binaryasc[i] == '0') wosp[i] += "\n";
                    else if (binaryasc[i] == '1') wosp[i] += " \n";
                }

                for (int i = binaryasc.Length; i < wosp.Length; i++)
                {
                    wosp[i] += "\n";
                }

                result = String.Join("", wosp);
            }
            else
            {
                string binaryasc = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '\n')
                    {
                        if (text[i - 1] == ' ') binaryasc += '1';
                        else binaryasc += '0';
                    }
                }
                result = SecretTextFromBin(binaryasc);
            }
            return result;
        }

        private string LettersHide(string text, string secrettext, bool mode)
        {
            char[] letters = {
                'A', 'А', 'B', 'В', 'C', 'С', 'E', 'Е', 'H', 'Н', 'K', 'К',
                'M', 'М', 'O', 'О', 'P', 'Р', 'T', 'Т', 'X', 'Х',
                'a', 'а', 'c', 'с', 'e', 'е', 'o', 'о', 'p', 'р', 'x', 'х' }; //англ - рус
            string result = "";
            if (mode)
            {
                string binaryasc = SecretTextToBin(secrettext);

                if (binaryasc.Length > text.Count(x => Array.IndexOf(letters, x) != -1))
                {
                    return "Количество букв в тексте, подлежащих замене, должно быть не менее " + binaryasc.Length;
                }

                int c = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    int lindex = Array.IndexOf(letters, text[i]);
                    if (lindex != -1 && c < binaryasc.Length)
                    {
                        if (binaryasc[c] == '1') result += letters[lindex - 1];
                        else if (binaryasc[c] == '0') result += text[i];
                        c++;
                    }
                    else result += text[i];
                }
            }
            else
            {
                string binaryasc = "";
                for (int i = 0; i < text.Length; i++)
                {
                    int lindex = Array.IndexOf(letters, text[i]);
                    if (lindex % 2 == 0)
                    {
                        binaryasc += '1';
                    }
                    else if (lindex % 2 == 1)
                    {
                        binaryasc += '0';
                    }
                }
                result = SecretTextFromBin(binaryasc);
            }
            return result;
        }

        private string MyMethod(string text, string secrettext, bool mode) //если после символа есть место возможного переноса - 1, иначе 0, разделитель, если символ встречается дважды
        {
            return "";
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            switch (methodCombo.SelectedIndex)
            {
                case 0:
                    textBox1.Text = DoubleSpace(richTextBox2.Text, "", false);
                    break;
                case 1:
                    textBox1.Text = DifferentSpace(richTextBox2.Text, "", false);
                    break;
                case 2:
                    textBox1.Text = EndSpace(richTextBox2.Text, "", false);
                    break;
                case 3:
                    textBox1.Text = LettersHide(richTextBox2.Text, "", false);
                    break;
                case 4:
                    textBox1.Text = MyMethod(richTextBox2.Text, "", false);
                    break;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            methodCombo.SelectedIndex = 0;
            richTextBox1.AllowDrop = richTextBox2.AllowDrop = true;
            richTextBox1.DragDrop += RichTextBox1_DragDrop;
            richTextBox2.DragDrop += RichTextBox1_DragDrop;

            //richTextBox1.Text = SecretTextToBin("тест");
            //textBox1.Text = SecretTextFromBin(richTextBox1.Text);
            //for (int i = 0; i < Fano.Res.Length; i++) richTextBox2.Text += Fano.Alpha[i] + " " + Fano.Res[i] + "\n";
        }

        private void RichTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);
                using (StreamReader sr = new StreamReader(docPath[0], Encoding.GetEncoding(1251)))
                {
                    ((RichTextBox)sender).Text = sr.ReadToEnd();
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //hideButton.Location = new Point(hideButton.Location.X, this.Size.Height - 70);
            //showButton.Location = new Point(showButton.Location.X, this.Size.Height - 70);
            //label2.Location = new Point(label2.Location.X, this.Size.Height - 105);
            //textBox1.Location = new Point(textBox1.Location.X, this.Size.Height - 105);
            //richTextBox1.Size = new Size(this.Size.Width - 213, this.Size.Height - 244);
        }
    }
}
