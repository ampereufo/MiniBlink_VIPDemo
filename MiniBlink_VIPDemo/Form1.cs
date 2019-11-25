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
using System.Runtime.InteropServices;

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

            m_webView.onTitleChanged += new EventHandler<MBVIP_WebView.TitleChangedEventArgs>(webView_OnTitleChange);
            m_webView.onUrlChanged += new EventHandler<MBVIP_WebView.UrlChangedEventArgs>(webView_OnUrlChange);

            m_webView.LoadUrl("http://miniblink.net/");
        }

        void webView_OnTitleChange(object sender, MBVIP_WebView.TitleChangedEventArgs e)
        {
            Text = e.strTitle;
        }

        void webView_OnUrlChange(object sender, MBVIP_WebView.UrlChangedEventArgs e)
        {
            Text = e.strUrl;
        }
    }
}
