using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MBVIP;

namespace MiniBlink_VIPDemo
{
    public partial class Form1 : Form
    {
        MBVIP_WebView m_webView;

        public Form1()
        {
            InitializeComponent();

            m_webView = new MBVIP_WebView();
            m_webView.Bind(panel1.Handle);

            m_webView.OnTitleChange += new EventHandler<MBVIP_WebView.TitleChangeEventArgs>(webView_OnTitleChange);
            m_webView.OnUrlChange += new EventHandler<MBVIP_WebView.UrlChangeEventArgs>(webView_OnUrlChange);

            m_webView.LoadUrl("www.baidu.com");
        }

        void webView_OnTitleChange(object sender, MBVIP_WebView.TitleChangeEventArgs e)
        {
            //Text = e.Title;
        }

        void webView_OnUrlChange(object sender, MBVIP_WebView.UrlChangeEventArgs e)
        {
            Text = e.URL;
        }
    }
}
