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
            m_webView.onLoadUrlEnd += webView_onLoadUrlEnd;
            m_webView.onDocumentReady += webView_OnDocumentReady;
            m_webView.onDownload += webView_OnDownload;
            m_webView.onNetResponse += webView_OnNetResponse;
            m_webView.onImageBufferToDataURL += webView_OnImageBufferToDataURL;

            m_webView.LoadUrl("https://cn.bing.com/search?q=http+analyzer&PC=U316&FORM=CHROMN");
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
            m_webView.onNetJobDataRecv += webView_onNetJobDataRecv;
            m_webView.onNetJobDataFinish += webView_onNetJobDataFinish;
            m_webView.onPopupDialogSaveName += webView_onPopupDialogSaveName;
            
        }

        private void webView_onNetJobDataRecv(object sender, MBVIP_WebView.NetJobDataRecvEventArgs e)
        {

        }

        private void webView_onPopupDialogSaveName(object sender, MBVIP_WebView.PopupDialogSaveNameEventArgs e)
        {
            string strPath = e.strPath;
        }

        private void webView_onNetJobDataFinish(object sender, MBVIP_WebView.NetJobDataFinishEventArgs e)
        {
            if (e.Result == mbLoadingResult.MB_LOADING_SUCCEEDED)
            {
                Console.WriteLine("下载完成");
            }
            else
            {
                Console.WriteLine("下载失败或取消");
            }
        }

        private void webView_OnNetResponse(object sender, MBVIP_WebView.NetResponseEventArgs e)
        {

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
            //m_webView.HookRequest(e.ptrJob);    // 设置此钩子才会触发mbOnLoadUrlEnd回调

            if (e.strUrl.ToLower().Contains("test.js"))
            {
                m_webView.SetMimeType(e.ptrJob, "application/javascript");
                m_webView.NetSetData(e.ptrJob, "alert(\"js代码测试\")");
                m_webView.ContinueJob(e.ptrJob);    // 其实不用这句也能执行js，但是其他类型的Mime就需要了
            }

            /*if (m_webView.GetRequestMethod(e.ptrJob) == mbRequestType.kMbRequestTypePost)
            {
                var dictData = Common.GetHttpPostData(m_webView, e.ptrJob);    // 只有post数据才能获取到，否则会报错，获取后可以根据需要修改
                m_webView.CancelRequest(e.ptrJob);    // 取消本次请求后，重新发一个请求，请求的数据可以是基于刚刚获取的数据修改的，也可以完全新建

                mbPostBodyElements eles = m_webView.CreatePostBodyElements(5);
                mbPostBodyElement ele = m_webView.CreatePostBodyElement();

                m_webView.SetHttpHeaderField(e.ptrJob, "key", "value");
                m_webView.GetHttpHeaderField(e.ptrJob, "key", 0);

                byte[] byteData = eles.StructToBytes();
                m_webView.PostData(e.strUrl, byteData, byteData.Length);
            }*/
        }

        private void webView_onLoadUrlEnd(object sender, MBVIP_WebView.LoadUrlEndEventArgs e)
        {
           
        }

        private void webView_OnLoadingFinish(object sender, MBVIP_WebView.LoadingFinishEventArgs e)
        {
            string ss = $"{e.LoadingResult} {e.strFailedReason}";

            m_webView.GetSource();    // 获取网页源码，因为是多线程架构，异步的，结果要在回调里取
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
