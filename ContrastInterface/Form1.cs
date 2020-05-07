using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContrastInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = Path.GetFullPath("..\\..\\..\\MiniBlink_VIPDLL\\mb.h");
            textBox4.Text = Path.GetFullPath("..\\..\\..\\MiniBlink_VIPDLL\\MBVIP_API.cs");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

            List<string> MBFunNameList = new List<string>();
            string[] strArrMB = File.ReadAllLines(textBox3.Text);
            foreach (string str in strArrMB)
            {
                if (str.Length >= 8 && str.Substring(0, 8) == "ITERATOR")
                {
                    string strFunName = str.Split(',')[1].Replace(" ", "");
                    MBFunNameList.Add(strFunName);
                }

                if (str.Length >= 7 && str.Substring(0, 7) == "inline ")
                {
                    string strFunName = str.Split(' ')[2];
                    strFunName = strFunName.Substring(0, strFunName.LastIndexOf('('));
                    MBFunNameList.Add(strFunName);
                }
            }

            List<string> CSFunNameList = new List<string>();
            string[] strArrCS = File.ReadAllLines(textBox4.Text);
            foreach (string str in strArrCS)
            {
                if (str.Length >= 19 && str.Substring(0, 19) == "        [DllImport(")
                {
                    string strFunName = str.Split(',')[1].Replace(" EntryPoint = \"", "").Replace("\"", "");
                    CSFunNameList.Add(strFunName);
                }
            }

            foreach (string str in MBFunNameList)
            {
                if (!CSFunNameList.Contains(str))
                {
                    textBox1.AppendText(str + "\r\n\r\n");
                }
            }

            foreach (string str in CSFunNameList)
            {
                if (!MBFunNameList.Contains(str))
                {
                    textBox2.AppendText(str + "\r\n\r\n");
                }
            }

            if (textBox1.Text == string.Empty && textBox2.Text == string.Empty)
            {
                MessageBox.Show("没有找到新增或删除的接口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
