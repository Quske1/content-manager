using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using HtmlDocument = System.Windows.Forms.HtmlDocument;

namespace ManagerFree
{
    public partial class Form1 : Form
    {
        public string zadalovokn = "<h2";
        public string zadalovokk = "</h2>";
        public string zadalovoknnew = "<h3";
        public string zadalovokknnew = "</h3>";
        public string oln = "<ol>";
        public string olnk = "</ol>";
        public string uln = "<ul>";
        public string ulnk = "</ul>";

        public Form1()
        {
            InitializeComponent();

        }
        private static void SetWebBrowserCompatiblityLevel()
        {
            string appName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            int lvl = 1000 * GetBrowserVersion();
            bool fixVShost = File.Exists(Path.ChangeExtension(Application.ExecutablePath, ".vshost.exe"));

            WriteCompatiblityLevel("HKEY_LOCAL_MACHINE", appName + ".exe", lvl);
            if (fixVShost) WriteCompatiblityLevel("HKEY_LOCAL_MACHINE", appName + ".vshost.exe", lvl);

            WriteCompatiblityLevel("HKEY_CURRENT_USER", appName + ".exe", lvl);
            if (fixVShost) WriteCompatiblityLevel("HKEY_CURRENT_USER", appName + ".vshost.exe", lvl);
        }

        private static void WriteCompatiblityLevel(string root, string appName, int lvl)
        {
            try
            {
                Microsoft.Win32.Registry.SetValue(root + @"\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", appName, lvl);
            }
            catch (Exception)
            {
            }
        }

        public static int GetBrowserVersion()
        {
            string strKeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer";
            string[] ls = new string[] { "svcVersion", "svcUpdateVersion", "Version", "W2kVersion" };

            int maxVer = 0;
            for (int i = 0; i < ls.Length; ++i)
            {
                object objVal = Microsoft.Win32.Registry.GetValue(strKeyPath, ls[i], "0");
                string strVal = Convert.ToString(objVal);
                if (strVal != null)
                {
                    int iPos = strVal.IndexOf('.');
                    if (iPos > 0)
                        strVal = strVal.Substring(0, iPos);

                    int res = 0;
                    if (int.TryParse(strVal, out res))
                        maxVer = Math.Max(maxVer, res);
                }
            }

            return maxVer;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = ishodopis.Text.Length.ToString(CultureInfo.InvariantCulture);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label7.Text = resultbox.Text.Length.ToString(CultureInfo.InvariantCulture);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {


            if (checkBox2.Checked == false)
            {
                checkBox1.Enabled = true;
                webBrowser1.Visible = false;
                HtmlDocument doc = webBrowser1.Document;
                doc.Body.InnerHtml = null;
                System.Windows.Forms.Application.DoEvents();

            }
            else
            {
                    checkBox1.Enabled = false;
                    webBrowser1.Visible = true;
                    HtmlDocument doc = webBrowser1.Document;
                    doc.Body.InnerHtml = resultbox.Text;
                
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                checkBox2.Enabled = true;
                webBrowser1.Visible = false;
                HtmlDocument doc = webBrowser1.Document;
                doc.Body.InnerHtml = null;
                System.Windows.Forms.Application.DoEvents();
            }
            else
            {
                checkBox2.Enabled = false;
                        webBrowser1.Visible = true;
                    HtmlDocument doc = webBrowser1.Document;
                    doc.Body.InnerHtml = ishodopis.Text;
                

            }



        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
          if(Marketbox.Checked==true)
            { Sitebox.Enabled = false;
              
                    if (ishodopis.Text == "")
                    {
                        Marketbox.Checked = false;
                        MessageBox.Show(
                "Поле не содержит кода",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                        return;
                    }
                    else
                    {


                        string isxod = ishodopis.Text;
                        isxod = Regex.Replace(isxod, "\\s+", " ");
                        isxod = isxod.Replace(zadalovokn, zadalovoknnew);
                        isxod = isxod.Replace(zadalovokk, zadalovokknnew);
                        isxod = isxod.Replace(oln, uln);
                        isxod = isxod.Replace(olnk, ulnk);
                        isxod = isxod.Replace("<tr>", "<li>");
                        isxod = isxod.Replace("<tbody>", "<ul>");
                        isxod = isxod.Replace("</tbody>", "</ul>");
                        isxod = isxod.Replace("<li><li>", "<li>");
                        isxod = isxod.Replace("<br> <br>", "<br>"); 
                        isxod = isxod.Replace("<li>  ", " ");
                        isxod = Regex.Replace(isxod, "<div[^>]+>", string.Empty);
                        isxod = Regex.Replace(isxod, "</div>", string.Empty);
                      
                        isxod = Regex.Replace(isxod, "</a>", string.Empty);
                        isxod = Regex.Replace(isxod, "<a[^>]+>", string.Empty);
                        isxod = Regex.Replace(isxod, "<blockquote[^>]+>", string.Empty);
                        isxod = Regex.Replace(isxod, "</blockquote>", string.Empty);
                        isxod = Regex.Replace(isxod, "<strong>", string.Empty);
                        isxod = Regex.Replace(isxod, "</strong>", string.Empty);
                        isxod = Regex.Replace(isxod, "align=[^>]+", string.Empty);
                        isxod = Regex.Replace(isxod, "<table class=[^>]+>", string.Empty);
                        isxod = Regex.Replace(isxod, "</table>", string.Empty);
                        isxod = Regex.Replace(isxod, "<td>", string.Empty);
                        isxod = Regex.Replace(isxod, "</td>", string.Empty);
                        isxod = Regex.Replace(isxod, "</tr>", string.Empty);
                        isxod = Regex.Replace(isxod, "<img[^>]+>", string.Empty);
                        isxod = Regex.Replace(isxod, "<thead>(.*?)</thead>", "", RegexOptions.IgnoreCase);
                        isxod = Regex.Replace(isxod, "<p[^>]+></p>", string.Empty);
                        isxod = isxod.Replace("<li>  <ul>", "<ul>");
                        isxod = Regex.Replace(isxod, "<th[^>]+>", string.Empty);
                        isxod = Regex.Replace(isxod, "</th>", string.Empty);
                        string outText = isxod
                                  .Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                  .Where(str => !string.IsNullOrWhiteSpace(str))
                                  .Aggregate("", (rez, str) => rez + str.Trim() + "\r\n");

                        resultbox.Text = outText;

                    }
                
               

            }
            else
            {
                resultbox.Text = null;
                Sitebox.Enabled = true;
            }

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (Sitebox.Checked == true)
            {
                Marketbox.Enabled = false;

                if (ishodopis.Text == "")
                {
                    Sitebox.Checked = false;
                    MessageBox.Show(
            "Поле не содержит кода",
            "Ошибка",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                else
                {
                    string isxod = ishodopis.Text;
                    isxod = Regex.Replace(isxod, "<table[^>]+>", "<table class=\"colored_table\">");
                    isxod = Regex.Replace(isxod, "<div[^>]+>", string.Empty);
                    isxod = Regex.Replace(isxod, "<tr[^>]+>", "<tr>");
                    isxod = Regex.Replace(isxod, "<th[^>]+>", "<th>");
                    isxod = Regex.Replace(isxod, "<td[^>]+>", "<td>");
                    isxod = Regex.Replace(isxod, "<ul[^>]+>", "<ul>");
                    isxod = Regex.Replace(isxod, "<li[^>]+>", "<li>");
                    isxod = Regex.Replace(isxod, "<ol[^>]+>", "<ol>");
                    isxod = Regex.Replace(isxod, "<div[^>]+>", string.Empty);
                    isxod = Regex.Replace(isxod, "</div>", string.Empty);
                    isxod = Regex.Replace(isxod, "</a>", string.Empty);
                    isxod = Regex.Replace(isxod, "<a[^>]+>", string.Empty);
                    isxod = Regex.Replace(isxod, "<blockquote[^>]+>", string.Empty);
                    isxod = Regex.Replace(isxod, "</blockquote>", string.Empty);
                    isxod = Regex.Replace(isxod, "<strong>", string.Empty);
                    isxod = Regex.Replace(isxod, "</strong>", string.Empty);
                    isxod = Regex.Replace(isxod, "align=[^>]+", string.Empty);
                    isxod = Regex.Replace(isxod, "<img[^>]+>", string.Empty);
                    isxod = Regex.Replace(isxod, "<p[^>]+></p>", string.Empty);
                    isxod = isxod.Replace("<li>  <ul>", "<ul>");
                    string outText = isxod
                             .Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                              .Where(str => !string.IsNullOrWhiteSpace(str))
                            .Aggregate("", (rez, str) => rez + str.Trim() + "\r\n");

                    resultbox.Text = outText;

                }
            }
            else
            {
                resultbox.Text = null;
                Marketbox.Enabled = true;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            resultbox.Text = null;
            ishodopis.Text = null;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            Marketbox.Checked = false;
            Sitebox.Checked = false;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            Marketbox.Enabled = true;
            Sitebox.Enabled = true;
            webBrowser1.Visible = false;
            HtmlDocument doc = webBrowser1.Document;
            doc.Body.InnerHtml = null;
            System.Windows.Forms.Application.DoEvents();
        }
    }
    
}
