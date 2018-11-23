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
            SECSSetting frm = SECSSetting.CreateFrom();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.WindowState = FormWindowState.Maximized;
            this.tabPage2.Controls.Add(frm);
            //frm.Parent = this.tabPage1;
            frm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        #region 窗体单例
        private static MainForm formInstance;
        public static MainForm CreateFrom()
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个字窗体
            if (formInstance == null || formInstance.IsDisposed)
            {
                formInstance = new MainForm();
            }
            return formInstance;
        }
        #endregion 窗体单例
    }
}
