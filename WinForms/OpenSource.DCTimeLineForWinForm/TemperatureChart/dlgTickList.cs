using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DCSoft.TemperatureChart
{
    /// <summary>
    /// 时间刻度列表编辑器
    /// </summary>
    [System.Runtime.InteropServices.ComVisible( false )]
    //[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
    internal partial class dlgTickList : Form
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public dlgTickList()
        {
            DCSoft.WinForms.WinFormUtils.SetFormDefaultFont(this);
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private TickInfoList _InputTicks = null;
        /// <summary>
        /// 时刻列表
        /// </summary>
        public TickInfoList InputTicks
        {
            get { return _InputTicks; }
            set { _InputTicks = value; }
        }

        private void dlgTickList_Load(object sender, EventArgs e)
        {
            if (_InputTicks != null)
            {
                foreach (TickInfo item in this._InputTicks)
                {
                    dgvTicks.Rows.Add(item.Text, item.Value, ColorTranslator.ToHtml(item.Color));
                }
            }
        }

        private void dgvTicks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dgvTicks.CancelEdit();
                dgvTicks.EndEdit();
                using (ColorDialog dlg = new ColorDialog())
                {
                    string txt = Convert.ToString( dgvTicks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value );
                    if (txt != null)
                    {
                        dlg.Color = ColorTranslator.FromHtml(txt);
                    }
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        dgvTicks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ColorTranslator.ToHtml(dlg.Color);
                    }
                }
            }
        }

        private void btnSetTickBySeconds_Click(object sender, EventArgs e)
        {
            int step = 0;
            if (int.TryParse(txtSeconds.Text, out step))
            {
                int num = 3600 * 24 / step;
                if (num > 100)
                {
                    if (MessageBox.Show(
                        this, 
                        DCTimeLineStrings.WarringSmallTickStep, 
                        this.Text, 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question, 
                        MessageBoxDefaultButton.Button2) 
                            == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                dgvTicks.Rows.Clear();
                for (int iCount = 0; iCount < 3600 * 24; iCount += step)
                {
                    DateTime dtm = new DateTime( 1900 , 1 , 1 );
                    dtm  = dtm.AddSeconds( iCount );
                    string txt = dtm.Hour.ToString();
                    if( step < 60 )
                    {
                        txt = dtm.ToString("HH:mm:ss");
                    }
                    else if( step < 3600 )
                    {
                        txt = dtm.ToString("HH:mm");
                    }
                    dgvTicks.Rows.Add(
                        txt, 
                        iCount / 3600.0, 
                        ColorTranslator.ToHtml(Color.Black));
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this._InputTicks == null)
            {
                this._InputTicks = new TickInfoList();
            }
            else
            {
                this._InputTicks.Clear();
            }

            foreach (DataGridViewRow row in dgvTicks.Rows)
            {
                if (row.IsNewRow == false)
                {
                    try
                    {
                        TickInfo item = new TickInfo();
                        item.Text = Convert.ToString(row.Cells[0].Value);
                        item.Value = Convert.ToSingle(row.Cells[1].Value);
                        string txt = Convert.ToString(row.Cells[2].Value);
                        if (string.IsNullOrEmpty(txt) == false)
                        {
                            item.Color = ColorTranslator.FromHtml(txt);
                        }
                        this._InputTicks.Add(item);
                    }
                    catch (Exception ext)
                    {
                        // 转换数据异常
                        MessageBox.Show(
                            this, 
                            ext.Message, 
                            this.Text, 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
                        row.Selected = true;
                        return;
                    }
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTicks.CurrentRow != null && dgvTicks.CurrentRow.IsNewRow == false)
            {
                dgvTicks.Rows.Remove(dgvTicks.CurrentRow);
            }
        }
    }
}
