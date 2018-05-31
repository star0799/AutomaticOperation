using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using mshtml;
using tessnet2;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;



namespace AutomaticOperation
{
    public partial class Form1 : Form
    {
        public Form1()
        {           
            InitializeComponent();          
        }  
        List<int> listC2B = new List<int>();
        List<int> listC2C = new List<int>();
        List<int> listPincode = new List<int>();
        List<string> err_subnum =new List<string>();
        string err_message;
        int count = 0;
        int rowCount = 1;
        int pageCount = 1;
        int C2BsubCount;
        int C2CsubCount;
        String[] rex = { "'" };
        List<string> listSubC2B = new List<string>();
        List<string> listSubC2C = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            allClear();
         
                string path = ConfigurationManager.AppSettings["path"] + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (File.Exists(path))
                {
                    string[] readText = File.ReadAllLines(path);
                    //取子場和母廠
                    for (int i = 0; i < readText.Length / 2; i++)
                    {
                        if (readText[i] == "C2B")
                        {
                            string input = readText[i + 2];
                            string[] C2Bsubnum = input.Split(rex, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 1; j < C2Bsubnum.Count() - 1; j += 2)
                            {
                                if (C2Bsubnum[j] == "716"||C2Bsubnum[j] == "717"||C2Bsubnum[j] == "906" || C2Bsubnum[j] == "907" || C2Bsubnum[j] == "908" || C2Bsubnum[j] == "909")
                                {
                                    //加入母廠代碼
                                    listC2B.Add(Convert.ToInt16(C2Bsubnum[j]));
                                }
                                else if (C2Bsubnum[j] == "001")
                                {
                                    continue;
                                }
                                else
                                {
                                    //加入子場
                                    listSubC2B.Add(C2Bsubnum[j]);
                                    tbtest.Text += C2Bsubnum[j] + "  ";
                                    C2BsubCount++;
                                }
                            }
                        }
                        else if (readText[i] == "C2C")
                        {
                            string input = readText[i + 2];

                            string[] C2Csubnum = input.Split(rex, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 1; j < C2Csubnum.Count() - 1; j += 2)
                            {
                                if (C2Csubnum[j] == "761" || C2Csubnum[j] == "762" || C2Csubnum[j] == "779" || C2Csubnum[j] == "780" || C2Csubnum[j] == "850" || C2Csubnum[j] == "851")
                                {
                                    //加入母廠代碼
                                    listC2C.Add(Convert.ToInt16(C2Csubnum[j]));

                                }
                                else if (C2Csubnum[j] == "001")
                                {
                                    continue;
                                }
                                else
                                {
                                    listSubC2B.Add(C2Csubnum[j]);
                                    tbtest.Text += C2Csubnum[j] + "  ";
                                    C2CsubCount++;
                                }
                            }
                        }
                    }
                    tbShow.Text = "C2B: " + CheckList(listC2B) + Environment.NewLine + "C2C: " + CheckList(listC2C) + Environment.NewLine + "C2B子場" + C2BsubCount + "筆" + Environment.NewLine + "C2C子場" + C2CsubCount + "筆";
                    int sum;
                    sum = listC2B.Count + listC2C.Count;
                    DialogResult result = MessageBox.Show(string.Format("審核項目:\n\nC2B: {0}\nC2C: {1}\n\n共{2}項目", CheckList(listC2B), CheckList(listC2C), sum), "確認項目", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if (webBrowser1.Url.ToString() == "https://eservice.7-11.com.tw/E-Tracking/Login.aspx")
                        {
                            MessageBox.Show("請先登入");
                            HtmlDocument elm = webBrowser1.Document;
                            HtmlElement username = elm.All["Login1_UserName"];
                            HtmlElement password = elm.All["Login1_Password"];
                            password.SetAttribute("value", "Star0799");
                            username.SetAttribute("value", "star0799");
                            allClear();
                        }
                        else
                        {
                            log("審核開始");
                            Customize();                    
                        }
                    }
                    else
                    {
                        allClear();
                        tbtest.Text = "";
                        tbShow.Text = "";
                        C2BsubCount = 0;
                        C2CsubCount = 0;
                    }
                }
                else
                {
                    MessageBox.Show("確認App.config路徑位置和檔案名稱(Select_" + DateTime.Now.ToString("yyyyMMdd") + ".txt" + ")是否存在");
                }
        }
        string CheckList(List<int> list)
        {
            string tempnumber = "";
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    tempnumber += item + "  ";
                }
                return tempnumber;
            }
            return "";
        }
        void Customize()
        {
            //把list值(母廠商)取出來       
                if (listC2B.Count != 0)
                {
                    webBrowser1.Navigate("https://eservice.7-11.com.tw/E-Tracking/C2B_Maintain_OLPS_son.aspx");
                    loading();
                    foreach (var items in listC2B)
                    {
                        VendorC2B(items);
                        count = 0;
                        rowCount = 1;
                        pageCount = 1;
                    }
                }
                if (listC2C.Count != 0)
                {
                    webBrowser1.Navigate("https://eservice.7-11.com.tw/E-Tracking/C2C_Maintain_OLPS_son.aspx");
                    loading();
                    foreach (var items in listC2C)
                    {
                        VendorC2B(items);
                        count = 0;
                        rowCount = 1;
                        pageCount = 1;
                    }
                }
                //if (listPincode.Count != 0)
                //{
                //    webBrowser1.Navigate("https://eservice.7-11.com.tw/E-Tracking/PINCODE_Maintain_OLPS_son.aspx");
                //    loading();
                //    foreach (var items in listPincode)
                //    {
                //        ChooseVendor(items);
                //    }
                //}
                //清除重複子場
                HashSet<string> hs = new HashSet<string>(err_subnum);
                hs.ToList().ForEach(item => err_message += item + "  ");
                if (err_message == null || err_message.Trim() == "")
                {
                    log("審核完畢，沒有未審核的項目");
                    MessageBox.Show("審核完畢，沒有未審核的項目");
                }
                else
                {
                    log("審核完畢\n未審核到的子場:  " + err_message);
                    MessageBox.Show("審核完畢，未審核到的子場:  " + err_message);               
                }
                allClear();
            
      
        }
        int ligto_FemalePlant;
        //選取C2B母廠查詢
        void VendorC2B(int items)          
        {
            HtmlDocument elm = webBrowser1.Document;
            HtmlElement select = elm.All["ContentPlaceHolder1_drpParent_Q"];
            HtmlElement selectOLPS = elm.All["ContentPlaceHolder1_drpOLPSflag_Q"];
            HtmlElement submit = elm.All["ContentPlaceHolder1_butQuery"];
            ligto_FemalePlant = items;
          
                //  01/12跳出catch內容 修改判斷式重新讀取控制項。原程式if(select==null){loading}。待下周測試
                if (select == null || elm==null||selectOLPS==null||submit==null)
                {
                    log("找不到選擇母廠畫面控制項");
                    loading();
                    VendorC2B(items);
                }
            // 1/19
                try
                {
                    select.SetAttribute("value", items.ToString());
                    selectOLPS.SetAttribute("value", "0");
                    submit.InvokeMember("click");
                    loading();
                    Review();
                }
                catch
                {
                    log("VendorC2B找不到控制項(in catch)");
                    select.SetAttribute("value", items.ToString());
                    selectOLPS.SetAttribute("value", "0");
                    submit.InvokeMember("click");
                    loading();
                    Review();
                }
      
        }
        void allClear()
        {
            listC2B.Clear();
            listC2C.Clear();
            listPincode.Clear();
            err_subnum.Clear();
            listSubC2B.Clear();
            listSubC2C.Clear();
            err_message = "";
            count = 0;
            rowCount = 1;
            pageCount = 1;
            C2BsubCount = 0;
            C2CsubCount = 0;
            tbtest.Text = "";
            //tbShow.Text = "C2B:" + Environment.NewLine + "C2C:" + Environment.NewLine + "Pincode:" + Environment.NewLine;
        }
        private void Review()
        {          
            HtmlDocument elm = webBrowser1.Document;
            HtmlElement edit = elm.All["ContentPlaceHolder1_GridView1_butEdit_" + (rowCount - 1)];
            HtmlElement subNum = null;
            try
            {
                subNum = elm.All["ContentPlaceHolder1_GridView1"].Children[0].Children[rowCount].Children[1];
                string checksub = subNum.InnerText.Trim().Substring(0, 3);
                bool checksub_Exist = listSubC2B.Any(l => l == checksub);

                if (edit != null && count < 3)
                {
                    if (checksub_Exist)
                    {
                        edit.InvokeMember("click");
                        loading();
                        Verify();
                    }
                    else
                    {
                        err_subnum.Add(checksub);
                        if (rowCount < 10)
                        {
                            rowCount++;
                            if (elm.All["ContentPlaceHolder1_GridView1_butEdit_" + (rowCount - 1)] == null || elm.All["ContentPlaceHolder1_GridView1"].Children[0].Children[rowCount].Children[1] == null)
                            {
                            }
                            else
                            {
                                Review();
                            }
                        }
                        else
                        {
                            HtmlElement page = elm.All["ContentPlaceHolder1_GridView1"].Children[0].Children[11].FirstChild.FirstChild.FirstChild.FirstChild.Children[pageCount].FirstChild;
                            if (page != null)
                            {
                                page.InvokeMember("click");
                                pageCount++;
                                rowCount = 1;
                                loading();
                                Review();
                            }
                        }
                    }
                }
                else
                {

                    if (count < 3)
                    {
                        count++;
                        log("找不到edit控制項目前檢查次數為" + count / 3);
                        Review();
                    }
                    else
                    {
                        log("母廠" + ligto_FemalePlant + "檢查完");
                    }
                }
            }
            catch{
                count = 0;
                rowCount = 1;
                pageCount = 1;
            }
          
        }
       
        void Verify()
        {
            try
            {
                HtmlDocument elm = webBrowser1.Document;
                HtmlElement date = elm.All["ContentPlaceHolder1_txtOLPS_Pass_Date_D"];
                HtmlElement save = elm.All["ContentPlaceHolder1_butSave"];
                if (date == null || save == null)
                {
                    count++;
                    //這的loading()防止回review頁籤找不到edit
                    loading();
                }
                else
                {
                    date.SetAttribute("value", DateTime.Today.ToString("yyyy-MM-dd"));
                    save.InvokeMember("click");
                    loading();
                    // HtmlElement back = elm.All["ContentPlaceHolder1_butBack"];必放在這,否則無法正常執行
                    HtmlElement back = elm.All["ContentPlaceHolder1_butBack"];
                    back.InvokeMember("click");
                    //這的loading()防止回review頁籤找不到edit
                    loading();
                    HtmlElement edit = elm.All["ContentPlaceHolder1_GridView1_butEdit_0"];
                    if (edit != null)
                        Review();
                }
            }
            catch (Exception ex)
            {
                log("Verify()錯誤訊息:"+ex.Message);
            }
        }
        public void loading()
        {        
         WebBrowserReadyState loadStatus = default(WebBrowserReadyState);
         int waittime = 100000;
         int counter = 0;
         while (true) {
         try { loadStatus = webBrowser1.ReadyState;}
         catch { }       
        Application.DoEvents();      
        if ((counter > waittime) || (loadStatus == WebBrowserReadyState.Uninitialized) || (loadStatus == WebBrowserReadyState.Loading) || (loadStatus == WebBrowserReadyState.Interactive)) {
            break; // TODO: might not be correct. Was : Exit While           
        }
        counter += 1;
    }
    counter = 0;
    while (true)
    {
        try { loadStatus = webBrowser1.ReadyState;}
        catch { }
        Application.DoEvents();
        if (loadStatus == WebBrowserReadyState.Complete)
        {
            break; // TODO: might not be correct. Was : Exit While
        }
        counter += 1;
    }
        }
        //點選alert視窗
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            IHTMLDocument2 vDocument = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            vDocument.parentWindow.execScript("function confirm(str){return true;} ", "javascript"); //彈出確認
            vDocument.parentWindow.execScript("function alert(str){return true;} ", "javaScript");
        }
        //點選alert視窗
        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            IHTMLDocument2 vDocument = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            vDocument.parentWindow.execScript("function confirm(str){return true;} ", "javascript"); //彈出確認
            vDocument.parentWindow.execScript("function alert(str){return true;} ", "javaScript");
        }
        //載入設定控制項
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://eservice.7-11.com.tw/E-Tracking/Login.aspx");
            
            loading();
            HtmlDocument elm = webBrowser1.Document;
            HtmlElement username = elm.All["Login1_UserName"];
            HtmlElement password = elm.All["Login1_Password"];
            password.SetAttribute("value", "Star0799");
            username.SetAttribute("value", "star0799");    
        }         
        private void button2_Click_1(object sender, EventArgs e)
        {
            allClear();
            log("審核完畢");           
        }

        //  1/12新增log
        void log(string message)
        {           
            if (!Directory.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath,"log")))
            {
                Directory.CreateDirectory(Path.Combine(System.Windows.Forms.Application.StartupPath, "log"));
            }
           
                using (StreamWriter sw = new StreamWriter(Path.Combine(System.Windows.Forms.Application.StartupPath, "log", DateTime.Now.ToString("yyyyMMdd") + ".txt"),true))
                {
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "   " + message);
                }
    
            
        }
                  }
              }

            


    

