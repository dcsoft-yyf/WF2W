using System;
using System.Windows.Forms;

namespace WF2WWinFormDemo.ControlsDemo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private async Task RuntFrmTestButton()
        {
            var dlg2 = new OpenFileDialog();
            try
            {
                var dlg = new frmTestButton();
                using (dlg)
                {
                    var s = 1;
                    await dlg.ShowDialog(this);
                }
            }
            catch( System.Exception ext )
            {
                Console.WriteLine(ext.ToString());
            }
        }
        private void btnTestButton_Click(object sender, EventArgs e)
        {
#if WF2W
            this.BeginInvoke(
                RuntFrmTestButton
                );
#else
                using (var dlg = new frmTestButton())
                {
                    var s = 1;
                    dlg.ShowDialog(this);
                }
#endif
        }

        private void btnTestBindingNavigator_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestBindingNavigator())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestCheckBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestCheckBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestCheckedListBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestCheckedListBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestComboBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestComboBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestContainerControl_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestContainerControl())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestContextMenuStrip_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestContextMenuStrip())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestDataGrid_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestDataGrid())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestDataGridTextBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestDataGridTextBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestDataGridView_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestDataGridView())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestDataGridViewComboBoxEditingControl_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestDataGridViewComboBoxEditingControl())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestDataGridViewTextBoxEditingControl_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestDataGridViewTextBoxEditingControl())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestDateTimePicker_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestDateTimePicker())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestDomainUpDown_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestDomainUpDown())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestFlowLayoutPanel_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestFlowLayoutPanel())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestForm_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestForm())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestGroupBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestGroupBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestHScrollBar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestHScrollBar())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestLabel_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestLabel())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestLinkLabel_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestLinkLabel())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestListBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestListBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestListView_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestListView())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestMaskedTextBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestMaskedTextBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestMdiClient_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestMdiClient())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestMenuStrip_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestMenuStrip())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestMonthCalendar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestMonthCalendar())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestNumericUpDown_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(async delegate ()
            {
                using (var dlg = new frmTestNumericUpDown())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestPanel_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestPanel())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestPictureBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestPictureBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestPrintPreviewControl_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestPrintPreviewControl())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestPrintPreviewDialog_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestPrintPreviewDialog())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestProgressBar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestProgressBar())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestPropertyGrid_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestPropertyGrid())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestRadioButton_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestRadioButton())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestRichTextBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestRichTextBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestScrollableControl_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestScrollableControl())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestSplitContainer_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestSplitContainer())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestSplitter_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestSplitter())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestStatusBar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestStatusBar())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestStatusStrip_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestStatusStrip())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestTabControl_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestTabControl())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestTableLayoutPanel_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestTableLayoutPanel())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestTabPage_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestTabPage())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestTextBox_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestTextBox())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestToolBar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestToolBar())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestToolStrip_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestToolStrip())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestToolStripContainer_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestToolStripContainer())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestToolStripContentPanel_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestToolStripContentPanel())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestToolStripDropDown_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestToolStripDropDown())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestToolStripDropDownMenu_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestToolStripDropDownMenu())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestToolStripPanel_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestToolStripPanel())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestTrackBar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestTrackBar())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestTreeView_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestTreeView())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestUserControl_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestUserControl())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestVScrollBar_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestVScrollBar())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }

        private void btnTestWebBrowser_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(
#if WF2W
                async
#endif
                delegate ()
            {
                using (var dlg = new frmTestWebBrowser())
                {
#if WF2W
                    await
#endif
                    dlg.ShowDialog(this);
                }
            });
        }
    }
}


