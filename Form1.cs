using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using Notepad.Properties;

namespace Notepad
{

    public partial class Form1 : Form
    {
        int indention = 0;

        public Form1()
        {
            InitializeComponent();
            if (Settings.Default.Data02)
            {
                textBox.Text = Settings.Default.SaveData;
                statusBar.Text = "設定、履歴などのすべてのデータの読み込みが完了しました。";
            }
        }

        private void Preview (object sender, EventArgs e)
        {
            while (Opacity > 0)
            {
                Thread.Sleep(1);
                Opacity -= 0.01;
            }

            MessageBox.Show(textBox.Text, "Preview (beta)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Opacity = 1;
        }

        private void Preference (object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void About (object sender, EventArgs e)
        {
            MessageBox.Show("Made by 裕斗 (yuto0214w)\nこれは Visual Studio にて作られました。", "Credit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Reset (object sender, EventArgs e)
        {
            textBox.Text = string.Empty;
        }

        private void Pause (object sender, EventArgs e)
        {
            TopMost = !TopMost;

            if (TopMost)
            {
                pauseToolStripMenuItem.Text = "Pause (Enabled)";
            }
            else
            {
                pauseToolStripMenuItem.Text = "Pause (Disabled)";
            }
        }

        private async void Cut (object sender, EventArgs e)
        {
            if (textBox.SelectionLength > 0) textBox.Cut();
            else
            {
                textBox.SelectAll();
                textBox.Cut();
                statusBar.Text = "すべての内容を切り取りしました。";
                await Task.Delay(1000);
                statusBar.Text = "設定、履歴などのすべてのデータの読み込みが完了しました。";
            }
        }

        private async void Copy (object sender, EventArgs e)
        {
            if (textBox.SelectionLength > 0) textBox.Copy();
            else
            {
                textBox.SelectAll();
                textBox.Copy();
                statusBar.Text = "すべての内容をコピーしました。";
                await Task.Delay(1000);
                statusBar.Text = "設定、履歴などのすべてのデータの読み込みが完了しました。";
            }

            textBox.SelectionLength = 0;
        }

        private void Paste (object sender, EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data != null && data.GetDataPresent(DataFormats.Text) == true) textBox.Paste();
        }

        private void Save (object sender, EventArgs e)
        {
            if (textBox.TextLength > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                sfd.Filter = "テキスト文書|*.txt|すべてのファイル|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "Save as...";
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                DialogResult result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    File.WriteAllText(fileName, textBox.Text, Encoding.Default);
                    MessageBox.Show(fileName + "として保存されました。", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = "Text is saved successfully.";
                }
                else if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("キャンセルされました。", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = "Cancelled.";
                }
            }
            else
            {
                MessageBox.Show("何も入力されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Update (object sender, EventArgs e)
        {
            string str = textBox.Text;
            if (Settings.Default.Data02) {
                Settings.Default.SaveData = str;
                Settings.Default.Save();
            }
            string target = "\n";
            int num = str.IndexOf(target);

            if (num > 0)
            {
                indention = 1;
                while (num > 0)
                {
                    num = str.IndexOf(target, num + 1);
                    if (num > 0)
                    {
                        indention += 1;
                    }
                }
            }

            else
            {
                indention = 0;
            }

            label1.Text = "文字数: " + (textBox.TextLength - indention);
        }

        private void Exit (object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Help (object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("まだ実装してないから待ってくれ。", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes) MessageBox.Show("ありがとう。", "Thank you", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (dr == DialogResult.No) MessageBox.Show("えぇ...", "Please wait...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Close (object sender, FormClosingEventArgs e)
        {
            if (textBox.TextLength > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                sfd.Filter = "テキスト文書|*.txt|すべてのファイル|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "Save as...";
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                DialogResult result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    File.WriteAllText(fileName, textBox.Text, Encoding.Default);
                    MessageBox.Show(fileName + "として保存されました。", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = "Text is saved successfully.";
                }
            }
        }
    }
}
