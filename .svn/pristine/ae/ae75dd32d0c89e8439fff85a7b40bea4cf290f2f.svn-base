using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.TemperatureChart;

namespace WindowsFormsApp
{
    public partial class frmAddPoint : Form
    {
        private ValuePoint _vp = null;

        private string _name = null;

        public ValuePoint Vp => _vp;

        public string ValuePointName => _name;

        public frmAddPoint()
        {
            InitializeComponent();
        }

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_name = txtName.Text;
			_vp = new ValuePoint();
			_vp.Text = txtText.Text;
			_vp.Time = dateTimePicker1.Value;
			_vp.Value = (float)numericUpDown1.Value;
			if (chkLanternValue.Checked)
			{
				_vp.LanternValue = (float)numericUpDown2.Value;
			}
			if (chkSpecifySymbol.Checked)
			{
				_vp.SpecifySymbolStyle = (ValuePointSymbolStyle)Enum.Parse(typeof(ValuePointSymbolStyle), cboboxSymbolType.SelectedItem.ToString());
			}
			if (cboboxAlignment.SelectedItem != null)
			{			
				//_vp.TextAlign = (StringAlignment)Enum.Parse(typeof(StringAlignment), cboboxAlignment.SelectedItem.ToString());
			}
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void frmAddPoint_Load(object sender, EventArgs e)
		{
			Array values = Enum.GetValues(typeof(ValuePointSymbolStyle));
			foreach (object item in values)
			{
				cboboxSymbolType.Items.Add(item);
			}
			Array values2 = Enum.GetValues(typeof(StringAlignment));
			foreach (object item2 in values2)
			{
				cboboxAlignment.Items.Add(item2);
			}
		}
	}
}
