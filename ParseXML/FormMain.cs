using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ParseXML
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public bool loaded = false;
        public bool start = false;
        public bool test = false; // Test Mode if "true" it will work on Test Environment.
        public string key = "auto"; // Search Key to find "second button"

        public FormMain()
        {
            InitializeComponent();
            
            btnGenerate.Click += BtnGenerate_Click;

            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            listFiles.Click += ListFiles_Click;
            LoadFiles();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            start = true;
            loaded = false;
            CleanLogs();
            LogStatus("Loading first page...");
            if (!test)
                webBrowser.Navigate(textURL.Text); // Start Parsing
            else
                ParseXML(); // For Test
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
            if (!start) return;
            if (!loaded)
            {
                if (webBrowser.ReadyState == WebBrowserReadyState.Complete)
                {
                    loaded = true;
                    LogStatus("Successfully loaded. waiting...");
                    LogStatus("Searching button to second URL...");
                    bool f = false;

                    foreach (HtmlElement el in webBrowser.Document.GetElementsByTagName("a"))
                    {
                        LogStatus(el.GetAttribute("href"), "URL");
                        if (el.GetAttribute("href").IndexOf(key) != -1)
                        {
                            LogStatus("Found URL. Navigating...");
                            webBrowser.Navigate(el.GetAttribute("href"));
                            f = true;
                            break;
                        }
                    }
                    if (!f)
                        LogStatus("Cannot find URL. Try again!", "Error");
                }
            }
            else
            {
                if (webBrowser.ReadyState == WebBrowserReadyState.Complete)
                {
                    LogStatus("Start Parsing XML...");
                    ParseXML();
                }
            }
        }

        private void ParseXML()
        {
            if (test)
            {
                string siteContent = string.Empty;

                siteContent = textXML.Text;
                string lineSeparator = ((char)0x2028).ToString();
                string paragraphSeparator = ((char)0x2029).ToString();
                siteContent = siteContent.Replace("\r\n", string.Empty).Replace("\r\n", string.Empty).Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);

                labelStatus.ForeColor = System.Drawing.Color.DodgerBlue;
                LogStatus("Successfully Parsed! Exporting...");
                textXML.Lines = GenerateXMLfromString(siteContent);
                ExportMXL();
            }
            else
            {
                string siteContent = string.Empty;

                siteContent = webBrowser.Document.Body.InnerHtml;
                string lineSeparator = ((char)0x2028).ToString();
                string paragraphSeparator = ((char)0x2029).ToString();
                siteContent = siteContent.Replace("\r\n", string.Empty).Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);

                labelStatus.ForeColor = System.Drawing.Color.DodgerBlue;
                LogStatus("Successfully Parsed! Exporting...");
                textXML.Lines = GenerateXMLfromString(siteContent);
                ExportMXL();
            }
        }

        private void ListFiles_Click(object sender, EventArgs e)
        {
            string fileName = listFiles.GetItemText(listFiles.SelectedItem);
            if (fileName == "")
                LoadFiles();
            else
                NavigateExportedFile(fileName);
        }

        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }

        private void LoadFiles ()
        {
            listFiles.Items.Clear();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                while(file.IndexOf('\\') != -1)
                {
                    file = file.Substring(file.IndexOf('\\') + 1);
                }
                files[i] = file;
            }
            listFiles.Items.AddRange(files);
        }

        private void LogStatus(string log, string prefix = "Common")
        {
            if (prefix == "Error")
            {
                labelStatus.ForeColor = System.Drawing.Color.Red;
            }
            labelStatus.Text = log;
            List<string> lines = textStatus.Lines.ToList();
            DateTime logtime = DateTime.Now;
            string timeLog = "[" + logtime.ToString("hh:mm:ss.ff tt") + "]";
            lines.Insert(0, timeLog + " " + prefix + " : " + log);
            textStatus.Lines = lines.ToArray();
        }

        private void CleanLogs()
        {
            labelStatus.Text = "No Status";
            textStatus.Text = "";
            labelStatus.ForeColor = System.Drawing.Color.Black;
        }

        private void NavigateExportedFile(string exportFileName)
        {
            string currentDir = Directory.GetCurrentDirectory();
            currentDir = currentDir.Replace("\\", "/");
            string url = "file:///" + currentDir + "/" + exportFileName;
            webBrowser.Navigate(url);
        }

        private void ExportMXL ()
        {
            start = false;
            string exportFileName = "export_" + DateTime.Now.ToString("yyyyhhmmsstt") + ".xml";
            File.WriteAllText(exportFileName, textXML.Text);

            if (!test)
            {
                NavigateExportedFile(exportFileName);

                LogStatus("Successfully Exported at [" + exportFileName + "]");
                LogStatus("Click 'View Exported Files' to check your files.", "Info");

                LoadFiles();
                //tabControl.SelectedIndex = 1;
            } else
            {
                NavigateExportedFile(exportFileName);

                LogStatus("Successfully Exported at [" + exportFileName + "]");
                LogStatus("Click 'View Exported Files' to check your files.", "Info");

                LoadFiles();
                //tabControl.SelectedIndex = 1;
            }
        }

        bool isElement (string strElement, string data)
        {
            return data.IndexOf(strElement) == 0;
        }

        string getElementString(string data, bool flag = true)
        {
            var ret = data;
            while (ret.IndexOf("<") != -1)
            {
                var s = ret.IndexOf("<");
                var e = ret.IndexOf(">") + 1;
                ret = ret.Substring(0, s) + ret.Substring(e);
            }
            if (flag)
            {
                ret = ret.Replace(" / ", "_");
                ret = ret.Replace("&nbsp;", string.Empty);
                ret = ret.Replace('/', '_');
                ret = ret.Replace('(', '_');
                ret = ret.Replace(')', '_');
                ret = ret.Replace(':', '_');
                ret = ret.Replace('.', '_');
                ret = ret.Replace('$', '_');
                ret = ret.Replace('=', '_');
                ret = ret.Replace(' ', '_');
                if (ret.Length > 0)
                    if (ret[0] >= '0' && ret[0] <= '9')
                        ret = 'n' + ret;
            } else
            {
                ret = ret.Replace("&nbsp;", string.Empty);
                ret = ret.Replace("&", string.Empty);
                ret = ret.Replace(";", string.Empty);
            }
            if (ret == "")
                return "NONE";
            return ret.ToUpper();
        }

        string[] getInfoStrings(string data)
        {
            string[] ret = new string[2];
            if (data.IndexOf("</span>") != -1)
            {
                ret[0] = data.Substring(0, data.IndexOf("</span>") + 7);
                ret[1] = data.Substring(ret[0].Length);
                ret[0] = getElementString(ret[0]);
                ret[1] = getElementString(ret[1], false);
            }
            else if (data.IndexOf("</label>") != -1)
            {
                ret[0] = data.Substring(0, data.IndexOf("</label>") + 8);
                ret[1] = data.Substring(ret[0].Length);
                ret[0] = getElementString(ret[0]);
                ret[1] = getElementString(ret[1], false);
            }
            else
            {
                ret[0] = "UNKNOWN";
                ret[1] = "UNKNOWN";
            }
            return ret;
        }

        class EncapsulateNode
        {
            public string hd;
            public int idx;
        }

        EncapsulateNode EncapsulateString(string meta, List<string> data, string htmlData, bool header = true, int index = 0)
        {
            string hd = htmlData;
            int idx = index;
            string prefix = "<";
            data.Insert(idx, prefix + meta + ">");
            while (hd.IndexOf("<") != -1) {
                while (hd.IndexOf("</") == hd.IndexOf("<") && hd.IndexOf("<") != -1)
                {
                    hd = hd.Substring(hd.IndexOf(">") + 1);
                }
                hd = hd.Substring(hd.IndexOf("<") + 1);
                if (isElement("table", hd))
                {
                    if (header)
                    {
                        hd = hd.Substring(hd.IndexOf("<td"));
                        hd = hd.Substring(hd.IndexOf(">") + 1);
                        var dd = hd.Substring(0, hd.IndexOf("</td>"));
                        hd = hd.Substring(dd.Length);
                        dd = getElementString(dd);
                        EncapsulateNode result = EncapsulateString(dd, data, hd, false, idx + 1);
                        hd = result.hd;
                        idx = result.idx;
                    } else
                    {
                        string td = hd.Substring(0, hd.IndexOf("</table"));
                        hd = hd.Substring(hd.IndexOf("</table>") + 7);
                        while (td.IndexOf("<td") != -1)
                        {
                            td = td.Substring(td.IndexOf("<td"));
                            var rr = td.Substring(0, td.IndexOf("</td>") + 5);
                            td = td.Substring(rr.Length);
                            rr = rr.Substring(rr.IndexOf(">") + 1);
                            rr = rr.Substring(0, rr.IndexOf("</td>"));
                            string[] dd = getInfoStrings(rr);
                            data.Insert(++idx, "<" + dd[0] + ">");
                            data.Insert(++idx, dd[1]);
                            data.Insert(++idx, "</" + dd[0] + ">");
                        }
                        break;
                    }
                }
            }
            data.Insert(++idx, prefix + "/" + meta + ">");
            EncapsulateNode ret = new EncapsulateNode();
            ret.hd = hd;
            ret.idx = idx;
            return ret;
        }

        private string[] GenerateXMLfromString(string data)
        {
            List<string> ret = new List<string>();
            data = data.ToLower();
            data = data.Substring(data.IndexOf("<table") + 5);

            ret.Insert(0, "<INVOICE>");

            ret.Insert(1, "<HEADER>");
            EncapsulateNode result = EncapsulateString("SERIAL", ret, data, false, ret.Count);
            data = result.hd;
            string header = string.Empty;
            for (int i = 0; i < 8; i ++)
            {
                data = data.Substring(data.IndexOf("<table"));
                string t = data.Substring(0, data.IndexOf("</table") + 8);
                data = data.Substring(t.Length);
                if (i == 1 || i == 2 || i == 4 || i == 5)
                {
                    string r = t.Substring(0, t.IndexOf("</tr>") + 5);
                    t = t.Substring(r.Length);
                    t = r.Replace("</tr>", "</table><table>") + t;
                    header += t;
                }
                else if (i == 3)
                {
                    string r = t;
                    data = data.Substring(data.IndexOf("<table"));
                    t = data.Substring(0, data.IndexOf("</table") + 8);
                    data = data.Substring(t.Length);

                    r = r.Replace("</table>", string.Empty);
                    t = t.Substring(t.IndexOf('>') + 1);
                    t = r + t;

                    r = t.Substring(0, t.IndexOf("</tr>") + 5);
                    t = t.Substring(r.Length);
                    t = r.Replace("</tr>", "</table><table>") + t;
                    header += t;
                }
                else
                {
                    header += t;

                    data = data.Substring(data.IndexOf("<table"));
                    t = data.Substring(0, data.IndexOf("</table") + 8);
                    header += t;
                    data = data.Substring(t.Length);
                }
            }
            data = data.Substring(data.IndexOf("<table"));
            EncapsulateString("INFO", ret, header, true, ret.Count);
            ret.Insert(ret.Count, "</HEADER>");

            data = data.Substring(data.IndexOf("<table"));
            data = data.Substring(0, data.LastIndexOf("</table>") + 8);

            string[] footer = new string[8];
            for (int i = 0; i < 8; i ++)
            {
                string t = data.Substring(data.LastIndexOf("<table"));
                t = t.Substring(0, t.IndexOf("</table>") + 8);
                data = data.Substring(0, data.LastIndexOf("<table"));
                data = data.Substring(0, data.LastIndexOf("</table>") + 8);
                footer[7 - i] = t;
            }
            string footerTitle = getElementString(footer[0]);
            string footerData = string.Empty;
            string temp = footer[1];
            temp = temp.Substring(0, temp.IndexOf("<tr")) + temp.Substring(temp.IndexOf("</tr>") + 5);
            temp = temp.Substring(0, temp.IndexOf("</tr")) + "</table><table>" + temp.Substring(temp.IndexOf("</tr>"));
            footerData += temp;
            temp = footer[2];
            temp = temp.Substring(0, temp.IndexOf("</tr")) + "</table><table>" + temp.Substring(temp.IndexOf("</tr>"));
            footerData += temp;
            temp = footer[4];
            temp = temp.Substring(0, temp.IndexOf("</tr")) + "</table><table>" + temp.Substring(temp.IndexOf("</tr>"));
            footerData += temp;
            temp = footer[5];
            temp = temp.Substring(0, temp.IndexOf("</tr")) + "</table><table>" + temp.Substring(temp.IndexOf("</tr>"));
            footerData += temp;
            temp = footer[6];
            temp = temp.Substring(0, temp.IndexOf("</tr")) + "</table><table>" + temp.Substring(temp.IndexOf("</tr>"));
            footerData += temp;
            temp = getElementString(footer[7]);
            temp = "<table><td>INFO</td></table><table><td><span>DATE</span><span>" + temp + "</span></td></table>";
            footerData += temp;

            string productTitle = data.Substring(0, data.IndexOf("</table>") + 8);
            data = data.Substring(data.IndexOf("</table"));
            data = data.Substring(data.IndexOf("<table"));
            productTitle = getElementString(productTitle);

            string products = "";
            while(data.IndexOf("<div") != -1)
            {
                string product = string.Empty;
                string s = data.Substring(0, data.IndexOf("<div"));
                data = data.Substring(data.IndexOf("<div"));
                s = s.Replace("/table", "/tr");

                string e = string.Empty;
                if (data.IndexOf("</div") != -1)
                    e = data.Substring(0, data.IndexOf("</div>") + 5);
                else
                    e = data;
                data = data.Substring(e.Length);
                if (e.IndexOf("table") != -1)
                {
                    e = e.Substring(e.IndexOf("<table") + 6);
                    e = e.Substring(0, e.LastIndexOf("</table"));

                    while (e.IndexOf("<table") != -1)
                    {
                        string se = e.Substring(0, e.IndexOf("<table"));
                        //string espan = se.Substring(se.IndexOf("<span"));
                        //se = se.Substring(0, se.IndexOf("<span"));
                        se = se.Substring(0, se.LastIndexOf("<td"));
                        string ee = e.Substring(e.IndexOf("<table"));
                        e = se + ee;
                        e = e.Replace("table", "tr");
                    }
                }

                product = s + e + "</table>";
                product = product.Substring(0, product.IndexOf("</td>")) + "</td></table><table>" + product.Substring(product.IndexOf("</td>") + 5);
                products += product;
            }
            
            EncapsulateString(productTitle, ret, products, true, ret.Count);

            EncapsulateString(footerTitle, ret, footerData, true, ret.Count);

            ret.Insert(ret.Count, "</INVOICE>");
            return ret.ToArray();
        }
    }
}
