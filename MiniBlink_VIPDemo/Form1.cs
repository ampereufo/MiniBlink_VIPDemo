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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_webView = new MBVIP_WebView();
            m_webView.Bind(panel1.Handle);

            m_webView.onTitleChanged += webView_OnTitleChange;
            m_webView.onUrlChanged += webView_OnURLChange;
            m_webView.onLoadingFinish += webView_OnLoadingFinish;
            m_webView.onLoadUrlBegin += webView_OnLoadUrlBegin;
            m_webView.onDocumentReady += webView_OnDocumentReady;
            m_webView.onDownload += webView_OnDownload;
            m_webView.onImageBufferToDataURL += webView_OnImageBufferToDataURL;

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
            Random random = new Random();    // 修改画布指纹
            string strRandomFingerprint = null;

            for (int i = 0; i < 50; i++)
            {
                strRandomFingerprint += random.Next(0, 99999).ToString();
            }

            e.byteData = Encoding.UTF8.GetBytes(strRandomFingerprint);
        }

        private void webView_OnDocumentReady(object sender, MBVIP_WebView.DocumentReadyEventArgs e)
        {
            Console.WriteLine("加载完成");
        }

        private void webView_OnLoadUrlBegin(object sender, MBVIP_WebView.LoadUrlBeginEventArgs e)
        {
            if (e.strUrl.ToLower().Contains("test.js"))    // 拦截或替换某些你不喜欢的东西
            {
                m_webView.SetMimeType(e.ptrJob, "application/javascript");
                m_webView.NetSetData(e.ptrJob, "alert(\"js代码测试\")");

                m_webView.CancelRequest(e.ptrJob);    // 取消本次请求
            }
        }

        private void webView_OnLoadingFinish(object sender, MBVIP_WebView.LoadingFinishEventArgs e)
        {
            string ss = $"{e.LoadingResult} {e.strFailedReason}";

            m_webView.GetSource();    // 获取网页源码，因为是多线程架构，结果在回调里取
            m_webView.onGetSource += webView_OnGetHtmlCode;

            m_webView.RunJs("return document.URL;");    // 执行js，需要返回值的话需要加return
            m_webView.onRunJs += webView_OnRunJs;
        }

        private void webView_OnGetHtmlCode(object sender, MBVIP_WebView.GetSourceEventArgs e)
        {
            string strCode = e.strHtmlCode;
        }

        private void webView_OnRunJs(object sender, MBVIP_WebView.RunJsEventArgs e)
        {
            string strJsRet = e.strRet;
        }
    }
}
