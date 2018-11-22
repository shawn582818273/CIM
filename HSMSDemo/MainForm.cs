using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
namespace CIM
{
    public partial class MainForm : CCSkinMain
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBoxEx.Show("您确定要退出吗？", "退出确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)        //如果单击“是”按钮
            {
                e.Cancel = false;                 //关闭窗体
            }
            else                                           //如果单击“否”按钮
            {
                e.Cancel = true;                  //不执行操作
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            SECSSetting frm = new SECSSetting();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.WindowState = FormWindowState.Maximized;
            this.tabPage2.Controls.Add(frm);
            //frm.Parent = this.tabPage1;
            frm.Show();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {

        }
    }
}
