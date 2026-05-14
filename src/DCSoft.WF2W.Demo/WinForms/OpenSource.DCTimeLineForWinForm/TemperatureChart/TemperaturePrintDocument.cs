using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Drawing;

using System.Drawing;
using System.Drawing.Printing;


namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 体温单打印文档
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Runtime.InteropServices.ComVisible(false)]
    [System.ComponentModel.ToolboxItem( false ) ]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    [DCSoft.Common.DCPublishAPI]
    public class TemperaturePrintDocument : PrintDocument 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="document">文档对象</param>
        public TemperaturePrintDocument(
            TemperatureDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            _Document = document;
            if( this._Document.Config.PageSettings != null )
            {
                //PageSettings ps = new PageSettings();
                //this._Document.Config.PageSettings.WriteTo( ps );
                //this.DefaultPageSettings = ps ;
            }
            //this.$1.Name = "DCTimeLine " + document.Title;
        }
       
        /// <summary>
        /// 文档对象
        /// </summary>
        private TemperatureDocument _Document = null;
    
        /// <summary>
        /// 当前页码号
        /// </summary>
        private int _CurrentPageIndex = 0 ;

        private int _SpecifyPageIndex = -1;
        /// <summary>
        /// 打印指定的页码
        /// </summary>
        [System.ComponentModel.DefaultValue( -1 )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public int SpecifyPageIndex
        {
            get
            {
                return _SpecifyPageIndex; 
            }
            set
            {
                _SpecifyPageIndex = value; 
            }
        }

        private List<int> _SpecifyPageIndexes = new List<int>();
        /// <summary>
        /// 打印指定的页码列表
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public List<int> SpecifyPageIndexes
        {
            get
            {
                return _SpecifyPageIndexes;
            }
            set
            {
                _SpecifyPageIndexes = value;
            }
        }

        /// <summary>
        /// 查询文档页面设置
        /// </summary>
        /// <param name="e"></param>
        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            if (this._Document.Config.PageSettings != null)
            {
                this._Document.Config.PageSettings.WriteTo(e.PageSettings);
            }
            base.OnQueryPageSettings(e);
        }

        /// <summary>
        /// 开始打印任务
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeginPrint(PrintEventArgs e)
        {
            DateTime maxDate = TemperatureDocument.NullDate;
            DateTime minDate = TemperatureDocument.NullDate;
            _CurrentPageIndex = 0;
            //_Document.UpdateState();
            _Document.UpdateNumOfPage(out maxDate, out minDate);

            if (_SpecifyPageIndexes.Count == 0)
            {
                //没有指定批量打印的页，则检查是否指定打印单个页
                if (_SpecifyPageIndex >= 0)
                {
                    _SpecifyPageIndexes.Add(_SpecifyPageIndex);
                }
                else
                {
                    //没有指定打印单个页和批量打印的页，则将时间轴所有页都加入
                    for (int i = 0; i < _Document.NumOfPages; i++)
                    {
                        _SpecifyPageIndexes.Add(i);
                    }
                }
            }
            _SpecifyPageIndexes.Sort();
            ///////////////////////////////////////////////////////////

            base.OnBeginPrint(e);
        }
        /// <summary>
        /// 打印结束
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
        }

        /// <summary>
        /// 打印一页
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            // 设置当前页码号 - 原来的代码
            //if (_SpecifyPageIndex >= 0)
            //{
            //    _CurrentPageIndex = _SpecifyPageIndex;
            //}
            if (_SpecifyPageIndexes.Count > 0)
            {
                _CurrentPageIndex = _SpecifyPageIndexes[0];
            }
            ////////////////////////

            int pageIndexBack = _Document.PageIndex;
            _Document.PageIndex = _CurrentPageIndex;
            // 设置计量单位
            e.Graphics.PageUnit = GraphicsUnit.Document;
            // 设置文档位置
            RectangleF boundsBack = _Document.Bounds;
            _Document.Left = e.PageSettings.Margins.Left * 3;
            _Document.Top = e.PageSettings.Margins.Top * 3;
            _Document.Width = e.PageSettings.Bounds.Width * 3 
                - _Document.Left - e.PageSettings.Margins.Right * 3 ;
            _Document.Height = e.PageSettings.Bounds.Height * 3 
                - _Document.Top - e.PageSettings.Margins.Bottom * 3 ;
            _Document.ViewMode = DocumentViewMode.Page;
            //是否需要自适应缩放
            Graphics g = e.Graphics;
            if (_Document.Config.PageSettings.AutoFitPageSize)
            {

                float XZoomRate=1f;
                float YZoomRate=1f;
                //// 计算实际的打印区域大小

                if (this.PrinterSettings.PaperSizes.Count > 0)
                {
                    this.DefaultPageSettings.PaperSize = this.PrinterSettings.PaperSizes[0];
                }
                //////////////////////////////////////
                float width = this.DefaultPageSettings.PaperSize.Width;
                float height = this.DefaultPageSettings.PaperSize.Height;

                // 计算当前文档的打印区域大小
                float width2 = e.PageSettings.Bounds.Width;
                float height2 = e.PageSettings.Bounds.Height;
                if (e.PageSettings.Landscape == true)
                {
                    width2 = e.PageSettings.Bounds.Height;
                    height2 = e.PageSettings.Bounds.Width;
                }
                   
                

                if (Math.Abs((width - width2) / width2) > 0.04
                    || Math.Abs((height - height2) / height2) > 0.04)
                {
                    // 预计的打印区域和实际的打印区域出现较大的差别,则进行自动缩放
                    if (width2 > 0 && height2 > 0)
                    {
                        float rate = Math.Min(width / width2, height / height2);
                        XZoomRate = rate;
                        YZoomRate = rate;
                    }
                }
                //进行自适应缩放
                if (XZoomRate != 1f)
                {
                    g.ScaleTransform(XZoomRate, YZoomRate);
                    
                }
                //横向打印缩放情况下打印位置不是最左边开始需要平移。
                if (e.PageSettings.Landscape == true && XZoomRate < 1f)
                {
                    float fixWidth = ((width2 - width2 * XZoomRate) * 3) - 50;
                    if (fixWidth > 1100)
                    {
                        fixWidth = 1100;
                    }
                    if (fixWidth < 700)
                    {
                        fixWidth = 700;
                    }
                    _Document.Left = _Document.Left + fixWidth;
                }
            }
            try
            {
                // 绘制文档
                _Document.PrintingMode = true;
                _Document.Draw2(
                    new DCGraphicsForTimeLine( g ),
                    new RectangleF(0, 0, 10000, 10000),
                    DocumentViewParty.Both );
            }
            finally
            {
                _Document.PrintingMode = false;
                _Document.Bounds = boundsBack;
                _Document.PageIndex = pageIndexBack;
            }

            _SpecifyPageIndexes.RemoveAt(0);
            e.HasMorePages = _SpecifyPageIndexes.Count > 0 && _SpecifyPageIndexes[0] < _Document.NumOfPages;
            // 原来的代码
            //_CurrentPageIndex++;
            //if ( _SpecifyPageIndex >= 0 || _CurrentPageIndex >= _Document.NumOfPages)
            //{
            //    // 若超过最大页则没有后续页，不再打印下一页
            //    e.HasMorePages = false;
            //}
            //else
            //{
            //    e.HasMorePages = true;
            //}
            //////////////////////////
        }

    }
}
