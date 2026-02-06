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
            toolStripDropDownButton1.DropDownItemClicked += ToolStripDropDownButton1_DropDownItemClicked;
            temperatureControl1.EventLinkClick += TemperatureControl1_EventLinkClick;
        }




        private void TemperatureControl1_EventLinkClick(object eventSender, DocumentLinkClickEventArgs args)
        {
#if MWGA
            Application.OpenUrl(Application.StartupPath, args.Link, "_blank");

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
        private void ToolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "打开本地时间轴文档")
            {
#if MWGA
                Application.InvokeAsync(async delegate ()
                {
                    // 将同步方法改为异步方法
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        if (await ofd.ShowDialog() == DialogResult.OK)
                        {
                            var stream = await ofd.OpenFile();
                            var reader = new StreamReader(stream, Encoding.UTF8, true);
                            var strXml = reader.ReadToEnd();
                            temperatureControl1.LoadDocumentFormString(strXml);
                            reader.Close();
                            //temperatureControl1.LoadDocumentFromFile(ofd.FileName);
                        }
                    }
                });
#else
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        var stream = ofd.OpenFile();
                        var reader = new StreamReader(stream, Encoding.UTF8, true);
                        var strXml = reader.ReadToEnd();
                        temperatureControl1.LoadDocumentFormString(strXml);
                        reader.Close();
                        //temperatureControl1.LoadDocumentFromFile(ofd.FileName);
                    }
                }
#endif
            }
            else
            {
                var name = e.ClickedItem.Name;
                var stream = this.GetType().Assembly.GetManifestResourceStream(name);
                if (stream != null)
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
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
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
                if (
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
            if (dd == null)
            {
                return;
            }
            dd.Values.Clear();
            DateTime startDT = DateTime.MinValue;
            DateTime endDT = DateTime.MinValue;
            temperatureControl1.Document.UpdateNumOfPage(out startDT, out endDT);
            startDT = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            endDT = new DateTime(endDT.Year, endDT.Month, endDT.Day, 0, 0, 0);

            while (startDT <= endDT)
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
            string CurrentUICultureName = System.Globalization.CultureInfo.CurrentUICulture.Name;
            bool IsEng = CurrentUICultureName.StartsWith("en");
            foreach (var name in resourceNames)
            {
                var index = name.IndexOf("timeline_");
                if (index > 0)
                {
                    if ( (IsEng == false && name.IndexOf("timeline_HealthRecord") > 0) ||
                        (IsEng == true && name.IndexOf("timeline_TimeZoneTest_ENG") > 0))
                    {
                        resName = name;
                    }
                    var strShortName = name.Substring(index + 9);
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
