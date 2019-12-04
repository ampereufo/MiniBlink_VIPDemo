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
            m_webView.onUrlChanged += new EventHandler<MBVIP_WebView.UrlChangedEventArgs>(webView_OnURLChange);
            m_webView.onLoadingFinish += new EventHandler<MBVIP_WebView.LoadingFinishEventArgs>(webView_OnLoadingFinish);
            m_webView.onLoadUrlBegin += new EventHandler<MBVIP_WebView.LoadUrlBeginEventArgs>(webView_OnLoadUrlBegin);
            m_webView.onDocumentReady += new EventHandler<MBVIP_WebView.DocumentReadyEventArgs>(webView_OnDocumentReady);
            m_webView.onDownload += new EventHandler<MBVIP_WebView.DownloadEventArgs>(webView_OnDownload);
            m_webView.onImageBufferToDataURL += new EventHandler<MBVIP_WebView.ImageBufferToDataUrlEventArgs>(webView_OnImageBufferToDataURL);

            m_webView.LoadUrl("http://miniblink.net/");
        }

        private void webView_OnURLChange(object sender, MBVIP_WebView.UrlChangedEventArgs e)
        {
            string url = e.strUrl;
        }

        private void webView_OnTitleChange(object sender, MBVIP_WebView.TitleChangedEventArgs e)
        {
            string title = e.strTitle;
        }

        private void webView_OnDownload(object sender, MBVIP_WebView.DownloadEventArgs e)
        {
            e.bCancel = true;
        }

        private void webView_OnImageBufferToDataURL(object sender, MBVIP_WebView.ImageBufferToDataUrlEventArgs e)
        {
            byte[] byteData = e.byteData;
        }

        private void webView_OnDocumentReady(object sender, MBVIP_WebView.DocumentReadyEventArgs e)
        {

        }

        private void webView_OnLoadUrlBegin(object sender, MBVIP_WebView.LoadUrlBeginEventArgs e)
        {
            string url = e.strUrl;
        }

        private void webView_OnLoadingFinish(object sender, MBVIP_WebView.LoadingFinishEventArgs e)
        {
            string ss = $"{e.LoadingResult} {e.strFailedReason}";

            m_webView.GetSource();
            m_webView.onGetSource += new EventHandler<MBVIP_WebView.GetSourceEventArgs>(webView_OnGetHtmlCode);

            m_webView.RunJs("return document.URL;");
            m_webView.onRunJs += new EventHandler<MBVIP_WebView.RunJsEventArgs>(webView_OnRunJs);
        }

        private void webView_OnGetHtmlCode(object sender, MBVIP_WebView.GetSourceEventArgs e)
        {
            string strCode = e.strHtmlCode;
        }

        private void webView_OnRunJs(object sender, MBVIP_WebView.RunJsEventArgs e)
        {
            string ss = e.strRet;
        }
    }
}
