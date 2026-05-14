using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DCSoft.Drawing;

// 袁永福到此一游

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间轴设计器控件
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    [System.ComponentModel.ToolboxItem( true )]
    [ToolboxBitmap( typeof( TimeLineDesignerControl ))]
    [DCSoft.Common.DCPublishAPI]
    [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false )]

    public partial class TimeLineDesignerControl : UserControl
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public TimeLineDesignerControl()
        {
            InitializeComponent();
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(TimeLineZoneInfo));
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(HeaderLabelInfo));
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(TitleLineInfo));
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(YAxisInfo));
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(DCTimeLineLabel));
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(DocumentPageSettings));
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(TemperatureDocumentConfig));
            DCSoft.Common.DCDescriptionHelper.PrepareType(typeof(ValuePointDataSourceInfo));
        }

        /// <summary>
        /// 主工具条
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public ToolStrip MainToolbar
        {
            get
            {
                return this.toolStrip1;
            }
        }

        private string _ResultConfigXML = null;
        /// <summary>
        /// 设计结果的XML字符串
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string ResultConfigXML
        {
            get
            {
                return _ResultConfigXML; 
            }
            set
            {
                _ResultConfigXML = value; 
            }
        }

        /// <summary>
        /// 获取设计结果的运行时的CONFIGXML字符串
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string RuntimeConfigXML
        {
            get
            {
                if (this.ctlTimeLine != null && this.ctlTimeLine.Document != null)
                {
                    return this.ctlTimeLine.Document.ConfigXml;
                }
                else
                {
                    return _ResultConfigXML;
                }
            }
        }
        private string _DocumentConfigXml = null;
        /// <summary>
        /// 文档配置信息XML字符串
        /// </summary>
        [Browsable(false)]
        [System.Runtime.InteropServices.ComVisible(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public string DocumentConfigXml
        {
            get
            {
                return _DocumentConfigXml;
            }
            set
            {
                _DocumentConfigXml = value;
            }
        }

        private TemperatureDocument _SourceDocument = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureDocument SourceDocument
        {
            get
            {
                return _SourceDocument;
            }
            set
            {
                _SourceDocument = value;
            }
        }

         

        private bool _Modified = false;
        /// <summary>
        /// 内容被修改标记
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public bool Modified
        {
            get { return _Modified; }
            set { _Modified = value; }
        }

        private TemperatureControl _SourceControl = null;
        /// <summary>
        /// 来源控件
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public TemperatureControl SourceControl
        {
            get { return _SourceControl; }
            set { _SourceControl = value; }
        }
        /// <summary>
        /// 控件加载时的处理
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            if (this.DesignMode)
            {
                return;
            }
            TemperatureDocument doc = null;
            if (this.SourceDocument == null)
            {
                doc = new TemperatureDocument();
            }
            else
            {
                doc = this.SourceDocument.Clone();
            }
            ctlTimeLine.BehaviorMode = DocumentBehaviorMode.DesignMode;
            ctlTimeLine.Document = doc;
            ctlTimeLine.ViewMode = DocumentViewMode.Timeline;
            if (DocumentConfigXml != null)
            {
                ctlTimeLine.Document.ConfigXml = DocumentConfigXml;
                DocumentConfigXml = null;
            }
            if (this.SourceControl != null)
            {
                this.ViewMode = this.SourceControl.ViewMode;
            }
            ctlTimeLine.AllowMouseDragScroll = false;
            ctlTimeLine.ViewParty = DocumentViewParty.Both;
            //ctlTimeLine.RefreshViewWithoutRefreshDataSource();
            RefreshItems();
            ctlTimeLine.EventSelectedObjectChanged += new EventHandler(ctlTimeLine_EventSelectedObjectChanged);
            ctlTimeLine.Document.SelectedObject = ctlTimeLine.Document;
            ctlTimeLine_EventSelectedObjectChanged(null, null);
            this.mShowLocalPropertyName.Checked = DCSoft.Common.DCDescriptionPropoertyDescriptor.ShowLocalizationDisplayName;
            if (TemperatureDocument.IsAssemblyObfuscation == false)
            {
                this.Text = this.Text + "[未加密]";
            }
        }

        private bool _HandlingSelectionEvent = false;

        private void RefreshItems()
        {
            _HandlingSelectionEvent = false;
            ctlTimeLine.RefreshViewWithoutRefreshDataSource();
            TemperatureDocumentConfig cfg = ctlTimeLine.Document.Config;
            tvwDOM.Nodes.Clear();
            TreeNode cfgNode = tvwDOM.Nodes.Add(DCTimeLineStrings.DocumentConfig);
            cfgNode.Tag = cfg;
            TreeNode rootNode = cfgNode.Nodes.Add(DCTimeLineStrings.TimeLineZone);
            foreach (TimeLineZoneInfo info in cfg.Zones)
            {
                TreeNode node = rootNode.Nodes.Add(info.Name);
                node.Tag = info;
            }

            rootNode = cfgNode.Nodes.Add(DCTimeLineStrings.HeaderLabel);
            foreach (HeaderLabelInfo info in cfg.HeaderLabels)
            {
                TreeNode node = rootNode.Nodes.Add(info.Title);
                node.Tag = info;
            }

            rootNode = cfgNode.Nodes.Add(DCTimeLineStrings.HeaderTitleLine);
            foreach (TitleLineInfo info in cfg.HeaderLines)
            {
                TreeNode node = rootNode.Nodes.Add(info.Title);
                node.Tag = info;
            }

            rootNode = cfgNode.Nodes.Add(DCTimeLineStrings.YAxis);
            foreach (YAxisInfo info in cfg.YAxisInfos)
            {
                TreeNode node = rootNode.Nodes.Add(info.Title);
                node.Tag = info;
            }

            rootNode = cfgNode.Nodes.Add(DCTimeLineStrings.FooterLine);
            foreach (TitleLineInfo info in cfg.FooterLines)
            {
                TreeNode node = rootNode.Nodes.Add(info.Title);
                node.Tag = info;
            }
            //rootNode = cfgNode.Nodes.Add(ChartResource.DocumentImage);
            //foreach (DCTimeLineImage info in cfg.Images)
            //{
            //    string txt = "NONE";
            //    if (info.Image != null)
            //    {
            //        txt = info.Image.ToString();
            //    }
            //    TreeNode node = rootNode.Nodes.Add(txt);
            //    node.Tag = info;
            //}

            rootNode = cfgNode.Nodes.Add(DCTimeLineStrings.DocumentLabel);
            foreach (DCTimeLineLabel lbl in cfg.Labels) 
            {
                string txt = lbl.Text;
                if (string.IsNullOrEmpty(txt))
                {
                    if (string.IsNullOrEmpty(lbl.ParameterName) == false )
                    {
                        txt = "[" + lbl.ParameterName + "]";
                    }
                    else if (lbl.Image != null && lbl.Image.HasContent )
                    {
                        txt = "[image]";
                    }
                }
                TreeNode node = rootNode.Nodes.Add(txt);
                node.Tag = lbl;
            }

            SelectTreeNode(tvwDOM.Nodes, ctlTimeLine.Document.SelectedObject);
            tvwDOM.ExpandAll();

            _HandlingSelectionEvent = true ;

            //cboItems.Items.Clear();
            //cboItems.Items.Add(new MyListItem(ChartResource.DocumentConfig, cfg));
            //cboItems.Items.Add(new MyListItem(ChartResource.HeaderLabel, null));
            //foreach (HeaderLabelInfo info in cfg.HeaderLabels)
            //{
            //    cboItems.Items.Add( new MyListItem( "   " + info.Title , info ));
            //}
            //cboItems.Items.Add(new MyListItem(ChartResource.HeaderTitleLine, null));
            //foreach (TitleLineInfo info in cfg.HeaderLines)
            //{
            //    cboItems.Items.Add(new MyListItem("   " + info.Title, info));
            //}
            //cboItems.Items.Add( new MyListItem(ChartResource.YAxis, null));
            //foreach (YAxisInfo info in cfg.YAxisInfos)
            //{
            //    cboItems.Items.Add(new MyListItem("   " + info.Title, info));
            //}
            //cboItems.Items.Add(new MyListItem(ChartResource.FooterLine, null));
            //foreach (TitleLineInfo info in cfg.FooterLines)
            //{
            //    cboItems.Items.Add(new MyListItem("   " + info.Title, info));
            //}
            //cboItems.Items.Add(new MyListItem(ChartResource.DocumentImage, null));
            //foreach (DCTimeLineImage info in cfg.Images)
            //{
            //    string txt = "NONE";
            //    if (info.Image != null)
            //    {
            //        txt = info.Image.ToString();
            //    }
            //    cboItems.Items.Add(new MyListItem("   " + txt , info));
            //}
            //cboItems.SelectedValue = ctlTimeLine.Document.SelectedObject;
            //_HandlingSelectionEvent = true;
        }

        //
        //private class MyListItem
        //{
        //    public MyListItem(string txt, object v)
        //    {
        //        this._Text = txt;
        //        this._Value = v;
        //    }
        //    private string _Text = null;

        //    public string Text
        //    {
        //        get { return _Text; }
        //        set { _Text = value; }
        //    }

        //    private object _Value = null;

        //    public object Value
        //    {
        //        get { return _Value; }
        //        set { _Value = value; }
        //    }
        //    public override string ToString()
        //    {
        //        return this.Text;
        //    }
        //}

        //private void cboItems_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (_HandlingSelectionEvent == true)
        //    {
        //        _HandlingSelectionEvent = false;
        //        object sv = cboItems.SelectedItem == null ? null : ((MyListItem)cboItems.SelectedItem).Value;
        //        if (ctlTimeLine.Document.SelectedObject != sv)
        //        {
        //            ctlTimeLine.Document.SelectedObject = sv;
        //            ctlTimeLine.Invalidate();
        //            ctlProerty.SelectedObject = sv;
        //        }
        //        _HandlingSelectionEvent = true;
        //    }
        //}

        void ctlTimeLine_EventSelectedObjectChanged(object sender, EventArgs e)
        {
            object obj = ctlTimeLine.Document.SelectedObject;
            if (obj == null || obj == ctlTimeLine.Document)
            {
                obj = ctlTimeLine.Document.Config;
            }
            if (_HandlingSelectionEvent == true)
            {
                _HandlingSelectionEvent = false;
                SelectTreeNode(tvwDOM.Nodes, obj);
                //cboItems.SelectedValue = obj;
                _HandlingSelectionEvent = true;
            }
            ctlProerty.SelectedObject = obj;
        }

        private void SelectTreeNode(TreeNodeCollection nodes, object obj)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag == obj)
                {
                    tvwDOM.SelectedNode = node;
                    this.ctlProerty.SelectedObject = node.Tag;
                    node.EnsureVisible();
                }
                else
                {
                    if (node.Nodes.Count > 0)
                    {
                        SelectTreeNode(node.Nodes, obj);
                    }
                }
            }
        }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler EventOKButtonClick = null;
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = true)]
        public event EventHandler EventCancelButtonClick = null;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Modified)
            {
                if (this.SourceDocument != null)
                {
                    this.SourceDocument.Config = ctlTimeLine.Document.Config;
                }
                this.ResultConfigXML = ctlTimeLine.Document.ConfigXml;
                if (this.EventOKButtonClick != null)
                {
                    this.EventOKButtonClick(this, null);
                }
            }
            else
            {
                if (this.EventCancelButtonClick != null)
                {
                    this.EventCancelButtonClick(this, null);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.EventCancelButtonClick != null)
            {
                this.EventCancelButtonClick(this, null);
            }
        }

        private void btnViewMode_DropDownOpening(object sender, EventArgs e)
        {
            btnPageViewMode.Checked = ctlTimeLine.ViewMode == DocumentViewMode.Page;
            btnNormalViewMode.Checked = ctlTimeLine.ViewMode == DocumentViewMode.Normal;
            btnWidelyViewMode.Checked = ctlTimeLine.ViewMode == DocumentViewMode.Timeline;
        }

        /// <summary>
        /// 文档视图模式
        /// </summary>
        private DocumentViewMode ViewMode
        {
            get
            {
                return ctlTimeLine.ViewMode;
            }
            set
            {
                if (ctlTimeLine.ViewMode != value)
                {
                    ctlTimeLine.ViewMode = value;
                    if (value == DocumentViewMode.Normal || value == DocumentViewMode.Timeline)
                    {
                        ctlTimeLine.BackColor = ctlTimeLine.Document.Config.PageBackColor;
                    }
                    else
                    {
                        if (this.SourceControl == null)
                        {
                            ctlTimeLine.BackColor = this.BackColor;
                        }
                        else
                        {
                            ctlTimeLine.BackColor = this.SourceControl.BackColor;
                        }
                    }
                    ctlTimeLine.UpdateViewSize();
                    ctlTimeLine.Invalidate();
                }
            }
        }

        private void btnNormalViewMode_Click(object sender, EventArgs e)
        {
            this.ViewMode = DocumentViewMode.Normal;
            ctlTimeLine.AllowMouseDragScroll = true;
        }

        private void btnPageViewMode_Click(object sender, EventArgs e)
        {
            this.ViewMode = DocumentViewMode.Page;
            ctlTimeLine.AllowMouseDragScroll = true;
        }

        private void btnWidelyViewMode_Click(object sender, EventArgs e)
        {
            this.ViewMode = DocumentViewMode.Timeline;
            ctlTimeLine.AllowMouseDragScroll = false;
        }

        private void ctlProerty_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.Modified = true;
            this.ctlTimeLine.RefreshViewWithoutRefreshDataSource();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            object obj = this.ctlTimeLine.Document.SelectedObject;
            if (obj is TemperatureDocumentConfig)
            {
                return;
            }
            bool flag = false;
            TemperatureDocumentConfig cfg = ctlTimeLine.Document.Config;
            if (obj is HeaderLabelInfo)
            {
                cfg.HeaderLabels.Remove((HeaderLabelInfo)obj);
                flag = true;
            }
            if (obj is TimeLineZoneInfo)
            {
                cfg.Zones.Remove((TimeLineZoneInfo)obj);
                flag = true;
            }
            if (obj is YAxisInfo)
            {
                cfg.YAxisInfos.Remove((YAxisInfo)obj);
                flag = true;
            }
            if (obj is TitleLineInfo)
            {
                TitleLineInfo info = (TitleLineInfo)obj;
                if (cfg.HeaderLines.Contains(info))
                {
                    cfg.HeaderLines.Remove(info);
                    flag = true;
                }
                else if (cfg.FooterLines.Contains(info))
                {
                    cfg.FooterLines.Remove(info);
                    flag = true;
                }
            }
            if (obj is DCTimeLineImage)
            {
                DCTimeLineImage img = (DCTimeLineImage)obj;
                if (cfg.Images.Contains(img))
                {
                    cfg.Images.Remove(img);
                    flag = true;
                }
            }
            if (obj is DCTimeLineLabel)
            {
                DCTimeLineLabel lbl = (DCTimeLineLabel)obj;
                if (cfg.Labels.Contains(lbl))
                {
                    cfg.Labels.Remove(lbl);
                    flag = true;
                }
            }
            if (flag)
            {
                this.Modified = true;
                ctlTimeLine.Document.SelectedObject = null;
                this.ctlTimeLine.Invalidate();
                ctlTimeLine_EventSelectedObjectChanged(null, null);
                RefreshItems();
            }
        }
         
        private void mInsertHeaderLabel_Click(object sender, EventArgs e)
        {
            HeaderLabelInfo info = new HeaderLabelInfo();
            info.Title = mInsertHeaderLabel.Text;
            ctlTimeLine.Document.Config.HeaderLabels.Add(info);
            ctlTimeLine.Document.SelectedObject = info;
            ctlTimeLine.Invalidate();
            ctlTimeLine_EventSelectedObjectChanged(null, null);
            RefreshItems();
            this.Modified = true;
        }

        private void mInsertHeaderLine_Click(object sender, EventArgs e)
        {
            TitleLineInfo info = new TitleLineInfo();
            info.Title = mInsertHeaderLine.Text;
            info.Name = "NewLine";
            ctlTimeLine.Document.Config.HeaderLines.Add(info);
            ctlTimeLine.Document.SelectedObject = info;
            ctlTimeLine.Invalidate();
            ctlTimeLine_EventSelectedObjectChanged(null, null);
            RefreshItems();
            this.Modified = true;
        }

        private void mInsertYAxis_Click(object sender, EventArgs e)
        {
            YAxisInfo info = new YAxisInfo();
            info.Title = mInsertYAxis.Text;
            info.Name = "NewValue";
            ctlTimeLine.Document.Config.YAxisInfos.Add(info);
            ctlTimeLine.Document.SelectedObject = info;
            ctlTimeLine.Invalidate();
            ctlTimeLine_EventSelectedObjectChanged(null, null);
            RefreshItems();
            this.Modified = true;
        }

        private void mInsertFooterLine_Click(object sender, EventArgs e)
        {
            TitleLineInfo info = new TitleLineInfo();
            info.Title = mInsertFooterLine.Text;
            info.Name = "NewLine";
            ctlTimeLine.Document.Config.FooterLines.Add(info);
            ctlTimeLine.Document.SelectedObject = info;
            ctlTimeLine.Invalidate();
            ctlTimeLine_EventSelectedObjectChanged(null, null);
            RefreshItems();
            this.Modified = true;
        }

        private void btnMovePre_Click(object sender, EventArgs e)
        {
            MoveElement(true);
        }

        private void MoveElement( bool forward )
        {
            object obj = ctlTimeLine.Document.SelectedObject;
            TemperatureDocumentConfig cfg = ctlTimeLine.Document.Config;
            if (obj is HeaderLabelInfo)
            {
                HeaderLabelInfo info = (HeaderLabelInfo)obj;
                if (MoveIndex(cfg.HeaderLabels, info, forward , null ))
                {
                    ctlTimeLine.Invalidate();
                }
            }
            else if (obj is TitleLineInfo)
            {
                TitleLineInfo info = (TitleLineInfo)obj;
                if (cfg.HeaderLines.Contains(info))
                {
                    if (MoveIndex(cfg.HeaderLines, info, forward , forward ? null : cfg.FooterLines))
                    {
                        ctlTimeLine.Invalidate();
                    }
                }
                else if (cfg.FooterLines.Contains(info))
                {
                    if (MoveIndex(cfg.FooterLines, info, forward , forward ? cfg.HeaderLines : null ))
                    {
                        ctlTimeLine.Invalidate();
                    }
                }
            }
            else if (obj is YAxisInfo)
            {
                YAxisInfo info = (YAxisInfo)obj;
                if (MoveIndex(cfg.YAxisInfos, info, forward , null ))
                {
                    ctlTimeLine.Invalidate();
                }
            }
            else if (obj is DCTimeLineImage)
            {
                DCTimeLineImage img = (DCTimeLineImage)obj;
                if (MoveIndex(cfg.Images, img, forward , null ))
                {
                    ctlTimeLine.Invalidate();
                }
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            MoveElement(false);
        }

        private bool MoveIndex(
            IList list, 
            object obj, 
            bool moveForward, 
            IList nextList )
        {
            int index = list.IndexOf(obj);
            if (index < 0)
            {
                return false;
            }
            if (moveForward)
            {
                if (index == 0)
                {
                    if (nextList != null)
                    {
                        list.RemoveAt(index);
                        nextList.Add( obj );
                        RefreshItems();
                        this.ctlTimeLine.RefreshViewWithoutRefreshDataSource();
                        this.Modified = true;
                        return true;
                    } 
                    MessageBox.Show(
                        this,
                        DCTimeLineStrings.CannotMoveForward,
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return false;
                }
                list.RemoveAt(index);
                list.Insert(index - 1, obj);
                RefreshItems();
                this.ctlTimeLine.RefreshViewWithoutRefreshDataSource();
                this.Modified = true;
                return true;
            }
            else
            {
                if (index == list.Count - 1)
                {
                    if (nextList != null)
                    {
                        list.RemoveAt(index);
                        nextList.Insert( 0 , obj );
                        RefreshItems();
                        this.ctlTimeLine.RefreshViewWithoutRefreshDataSource();
                        this.Modified = true;
                        return true;
                    }
                    MessageBox.Show(
                        this,
                        DCTimeLineStrings.CannotMoveNext,
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return false;
                }
                list.RemoveAt(index);
                list.Insert(index + 1, obj);
                RefreshItems();
                this.ctlTimeLine.RefreshViewWithoutRefreshDataSource();
                this.Modified = true;
                return true;
            }
        }
        private
#if MWGA
            async
#endif
            void btnViewXMLSource_Click(object sender, EventArgs e)
        {
            string xml = ctlTimeLine.Document.ConfigXml;
            using (frmConfigXML dlg = new frmConfigXML())
            {
                dlg.XMLText = xml;
                if (
#if MWGA
                    await
#endif
                    dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    ctlTimeLine.Document.ConfigXml = dlg.XMLText;
                    ctlTimeLine.Invalidate();
                    ctlTimeLine_EventSelectedObjectChanged(null, null);
                    this.Modified = true;
                    RefreshItems();
                }
            }
        }

        private void btnPageSettings_Click(object sender, EventArgs e)
        {
#if ! MWGA
            using (PageSetupDialog dlg = new PageSetupDialog())
            {
                dlg.PageSettings = new System.Drawing.Printing.PageSettings();
                TemperatureDocumentConfig cfg = ctlTimeLine.Document.Config;
                if (cfg.PageSettings != null)
                {
                    cfg.PageSettings.WriteTo(dlg.PageSettings);
                }
                dlg.EnableMetric = true;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    if (cfg.PageSettings == null)
                    {
                        cfg.PageSettings = new DocumentPageSettings();
                    }
                    cfg.PageSettings.ReadFrom(dlg.PageSettings);
                    ctlTimeLine.RefreshViewWithoutRefreshDataSource();
                    this.Modified = true;
                }
            }
#endif
        }

        private void mShowLocalPropertyName_Click(object sender, EventArgs e)
        {
            mShowLocalPropertyName.Checked = !mShowLocalPropertyName.Checked;
            DCSoft.Common.DCDescriptionPropoertyDescriptor.ShowLocalizationDisplayName = mShowLocalPropertyName.Checked;
            ctlProerty.Refresh();
        }

      

        private void mCloneInsert_Click(object sender, EventArgs e)
        {
            object obj = ctlTimeLine.Document.SelectedObject;
            if (obj != null)
            {
                object newObj = null;
                if (obj is TitleLineInfo)
                {
                    TitleLineInfo info = (TitleLineInfo)obj;
                    if (ctlTimeLine.Document.HeaderLines.Contains(info))
                    {
                        TitleLineInfo newInfo = info.Clone();
                        ctlTimeLine.Document.HeaderLines.Add(newInfo);
                        newObj = newInfo;
                    }
                    else if (ctlTimeLine.Document.FooterLines.Contains(info))
                    {
                        TitleLineInfo newInfo = info.Clone();
                        ctlTimeLine.Document.FooterLines.Add(newInfo);
                        newObj = newInfo;
                    }
                }
                else if (obj is HeaderLabelInfo)
                {
                    HeaderLabelInfo info = (HeaderLabelInfo)obj;
                    HeaderLabelInfo newInfo = info.Clone();
                    ctlTimeLine.Document.HeaderLabels.Add(newInfo);
                    newObj = newInfo;
                }
                else if (obj is YAxisInfo)
                {
                    YAxisInfo ya = (YAxisInfo)obj;
                    YAxisInfo newInfo = ya.Clone();
                    ctlTimeLine.Document.YAxisInfos.Add(newInfo);
                    newObj = newInfo;
                }
                else if (obj is TimeLineZoneInfo)
                {
                    TimeLineZoneInfo zone = (TimeLineZoneInfo)obj;
                    TimeLineZoneInfo newZone = zone.Clone();
                    newZone.StartTime = zone.EndTime.AddDays(1);
                    newZone.EndTime = newZone.StartTime.AddDays(1);
                    ctlTimeLine.Document.Config.Zones.Add(newZone);
                    newObj = newZone;
                }
                if (newObj != null)
                {
                    ctlTimeLine.Document.SelectedObject = newObj;
                    ctlTimeLine.Invalidate();
                    tvwDOM_AfterSelect(null, null);
                    RefreshItems();
                    this.Modified = true;
                }
            }
        }

        private void btnTreeView_Click(object sender, EventArgs e)
        {
            tvwDOM.Visible = btnTreeView.Checked;
            spTreeView.Visible = btnTreeView.Checked;
        }

        private void tvwDOM_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_HandlingSelectionEvent == true)
            {
                _HandlingSelectionEvent = false;
                if (tvwDOM.SelectedNode != null)
                {
                    object sv = tvwDOM.SelectedNode.Tag;
                    if (ctlTimeLine.Document.SelectedObject != sv)
                    {
                        ctlTimeLine.Document.SelectedObject = sv;
                        ctlTimeLine.Invalidate();
                        ctlProerty.SelectedObject = sv;
                    }
                }
                _HandlingSelectionEvent = true;
            }
        }

        private void mNewTimeZone_Click(object sender, EventArgs e)
        {
            TimeLineZoneInfo newInfo = new TimeLineZoneInfo();
            newInfo.StartTime = DateTime.Now.Date;
            newInfo.EndTime = newInfo.StartTime.AddDays(1);
            newInfo.Name = "zone" + ctlTimeLine.Document.Config.Zones.Count;
            ctlTimeLine.Document.Config.Zones.Add(newInfo);
            ctlTimeLine.Document.SelectedObject = newInfo;
            RefreshItems();
            SelectTreeNode(tvwDOM.Nodes, newInfo);
        }

        private void menuInsertLabel_Click(object sender, EventArgs e)
        {
            DCTimeLineLabel lbl = new DCTimeLineLabel();
            lbl.Text = DCTimeLineStrings.DocumentLabel;
            ctlTimeLine.Document.Config.Labels.Add(lbl);
            ctlTimeLine.Document.SelectedObject = lbl;
            RefreshItems();
            SelectTreeNode(tvwDOM.Nodes, lbl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK && this.SourceControl != null)
                {
                    this.SourceControl.SaveDocumentToFile(sfd.FileName);
                }
            }
        }
    }
}


