using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notepad.Properties;

namespace Notepad
{
    public partial class Form2 : Form
    {
        public bool confirm, backup;

        public Form2()
        {
            InitializeComponent();
            confirm = Settings.Default.Data01;
            backup = Settings.Default.Data02;
            if (confirm)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            if (backup)
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }
        }

        private void Save (object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                confirm = false;
            }

            else
            {
                confirm = true;
            }

            if (checkBox2.Checked == false)
            {
                backup = false;
            }

            else
            {
                backup = true;
            }

            Settings.Default.Data01 = confirm;
            Settings.Default.Data02 = backup;
            Settings.Default.Save();
            Close();
        }

        private void Cancel (object sender, EventArgs e)
        {
            if (confirm && checkBox1.Checked == false)
            {
                checkBox1.Checked = true;
            }

            if (backup && checkBox2.Checked == false)
            {
                checkBox2.Checked = true;
            }

            Close();
        }
    }
}
