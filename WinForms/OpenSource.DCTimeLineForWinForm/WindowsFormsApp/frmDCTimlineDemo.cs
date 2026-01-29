using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DCSoft.TemperatureChart;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public partial class frmDCTimlineDemo : Form
    {
        public frmDCTimlineDemo()
        {
            InitializeComponent();
            //temperatureControl1.Document.EventDrawValuePointSymbol += Document_EventDrawValuePointSymbol;

            //temperatureControl1.PrintCurrentPage(new System.Drawing.Printing.PrinterSettings());

            //temperatureControl1.EventDocumentClick += TemperatureControl1_EventDocumentClick;
            //temperatureControl1.EventValuePointClick += TemperatureControl1_EventValuePointClick;
            //temperatureControl1.EventSelectPageIndexChanged += TemperatureControl1_EventSelectPageIndexChanged;
            //temperatureControl1.EventAfterRefreshView += TemperatureControl1_EventAfterRefreshView;
#if MWGA
            toolStripDropDownButton1.DropDownItemClickedAsync += ToolStripDropDownButton1_DropDownItemClicked;
#else
            toolStripDropDownButton1.DropDownItemClicked += ToolStripDropDownButton1_DropDownItemClicked;
#endif
            temperatureControl1.EventLinkClick += TemperatureControl1_EventLinkClick;
        }

        /// <summary>
        /// 将基础URL和相对路径合并为完整的绝对URL（兼容反斜杠\）
        /// </summary>
        /// <param name="baseUrl">基础URL（如 https://www.example.com/api/）</param>
        /// <param name="relativePath">相对路径（支持\和/混合，如 user\123、..\data）</param>
        /// <returns>合并后的绝对URL</returns>
        /// <exception cref="ArgumentNullException">基础URL或相对路径为null/空时抛出</exception>
        /// <exception cref="UriFormatException">URL格式不合法时抛出</exception>
        public static string MergeUrl(string baseUrl, string relativePath)
        {
            // 空值校验
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl), "基础URL不能为空");
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath), "相对路径不能为空");

            // 关键优化：将所有反斜杠\替换为URL标准的正斜杠/
            string normalizedRelativePath = relativePath.Replace('\\', '/');

            // 解析基础URL（确保是合法的绝对URL）
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out Uri baseUri))
                throw new UriFormatException($"基础URL格式不合法：{baseUrl}，请确保包含协议（http/https）和域名");

            // 合并URL（此时相对路径已统一为/分隔符）
            Uri absoluteUri = new Uri(baseUri, normalizedRelativePath);

            return absoluteUri.ToString();
        }


        private void TemperatureControl1_EventLinkClick(object eventSender, DocumentLinkClickEventArgs args)
        {
#if MWGA
            string filename = MergeUrl(Application.StartupPath, args.Link);
            Application.OpenUrl(filename, "_blank");
           
#else
            string filename = Path.Combine(Application.StartupPath, args.Link);
            if (File.Exists (filename) == true)
            {
                frmPicture dlgpic = new frmPicture(filename);
                dlgpic.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("No File Found");
            }
#endif
        }
#if MWGA
private async Task ToolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
#else
private void ToolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
#endif
{
    if (e.ClickedItem.Text == "打开本地时间轴文档")
    {
        using (OpenFileDialog ofd = new OpenFileDialog())
        {
            if (
#if MWGA
                await
#endif
                ofd.ShowDialog() == DialogResult.OK)
            {
                var stream =
#if MWGA
                    await
#endif
                    ofd.OpenFile();
                var reader = new StreamReader(stream, Encoding.UTF8, true);
                var strXml = reader.ReadToEnd();
                temperatureControl1.LoadDocumentFormString(strXml);
                reader.Close();
                //temperatureControl1.LoadDocumentFromFile(ofd.FileName);
            }
        }
    }
            else
            {
                var name = e.ClickedItem.Name;
                var stream = this.GetType().Assembly.GetManifestResourceStream(name);
                if( stream != null )
                {
                    var reader = new StreamReader(stream, Encoding.UTF8, true);
                    var strXml = reader.ReadToEnd();
                    reader.Close();
                    this.temperatureControl1.LoadDocumentFormString(strXml);
                }
            }
        }

        private void TemperatureControl1_EventAfterRefreshView(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        //private void TemperatureControl1_EventSelectPageIndexChanged(object sender, SelectPageIndexChangeArgs e)
        //{
        //    foreach(DocumentData dd in temperatureControl1.Document.Datas)
        //    {
        //        for (int i = dd.Values.Count -1; i >=0; i--)
        //        {
        //            ValuePoint vp = dd.Values[i];
        //            if (vp.ID == "XXX")
        //            {
        //                dd.Values.Remove(vp);
        //            }
        //        }
        //    }
        //    //throw new NotImplementedException();
        //}

        private void TemperatureControl1_EventValuePointClick(object eventSender, ValuePointClickEventArgs args)
        {
            //throw new NotImplementedException();
        }

        private void TemperatureControl1_EventDocumentClick(object eventSender, DocumentClickEventArgs args)
        {
            //throw new NotImplementedException();
        }


        private void Document_EventDrawValuePointSymbol(object eventSender, DrawValuePointSymbolEventArgs args)
        {
            args.DrawString(
                "不升", 
                new Font("微软雅黑", 9, FontStyle.Regular), 
                new SolidBrush(Color.Black), 
                args.ReferRect.X, 
                args.ReferRect.Y, 
                new StringFormat(StringFormatFlags.DirectionVertical));
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            temperatureControl1.PrintDocument();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog sfd = new SaveFileDialog())
            {
                if(sfd.ShowDialog()== DialogResult.OK)
                {
                    temperatureControl1.SaveDocumentToFile(sfd.FileName);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
#if MWGA
            MessageBox.Show("Not supported in MWGA version.");
#else
            temperatureControl1.RunDesigner();

            ValuePoint vp = new ValuePoint();
            vp.SpecifySymbolStyle = ValuePointSymbolStyle.Character;
            vp.CharacterForCharSymbolStyle = '⊙';
#endif
        }
#if MWGA
        private async Task toolStripButton5_Click(object sender, EventArgs e)
#else
        private void toolStripButton5_Click(object sender, EventArgs e)
#endif
        {
            using (frmAddPoint fap = new frmAddPoint())
            {
                if(
#if MWGA
                    await
#endif
                    fap.ShowDialog() == DialogResult.OK)
                {

                    temperatureControl1.AddPoint(fap.ValuePointName, fap.Vp);
                    temperatureControl1.RefreshView();
                }
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void AddStartDayIndex(string titlelineName, DateTime startDate, int startIndex = 0)
        {
            //DCSoft.TemperatureChart.TitleLineInfo line = new TitleLineInfo();
            //line.UpAndDownTextType = UpAndDownTextType.ShowByTick;

            DCSoft.TemperatureChart.DocumentData dd = temperatureControl1.Document.Datas.GetDataByName(titlelineName, false);
            if(dd == null)
            {
                return;
            }
            dd.Values.Clear();
            DateTime startDT = DateTime.MinValue;
            DateTime endDT = DateTime.MinValue;
            temperatureControl1.Document.UpdateNumOfPage(out startDT, out endDT);
            startDT = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            endDT = new DateTime(endDT.Year, endDT.Month, endDT.Day, 0, 0, 0);

            while(startDT <= endDT)
            {
                temperatureControl1.AddPointByTimeText(
                    titlelineName,
                    startDT,
                    startIndex.ToString());
                startIndex++;
                startDT.AddDays(1);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            toolStripDropDownButton1.DropDownItems.Add("打开本地时间轴文档");
            toolStripDropDownButton1.DropDownItems.Add("-");
            string[] resourceNames = this.GetType().Assembly.GetManifestResourceNames();
            string resName = null;
            foreach (var name in resourceNames)
            {
                var index = name.IndexOf("timeline_");
                if(index > 0 )
                {
                    if (name.IndexOf("timeline_HealthRecord") > 0)
                    {
                        resName = name;
                    }
                    var strShortName = name.Substring(index +9);
                    var item = this.toolStripDropDownButton1.DropDownItems.Add(strShortName);
                    item.Name = name;
                }

                
            }

            if (resourceNames.Length > 0 && resName != null)
            {
                using (var stream = this.GetType().Assembly.GetManifestResourceStream(resName))
                {
                    temperatureControl1.LoadDocument(stream);
                    temperatureControl1.ViewMode = DocumentViewMode.Timeline;
                    temperatureControl1.RefreshView();
                    stream.Close();
                }
            }
        }
    }
    
}
