using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using DryDump;
using HSMSMessage;


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
            skinWaterTextBox1.Text = Program.unitid;
            S1F1 s1f1 = new S1F1();
            Program.Sender.Send(s1f1);
            S6F11_111 s6f11_111 = new S6F11_111();
            s6f11_111.CEID = "111";
            s6f11_111.CRST = "11";

            Program.Sender.Send(s6f11_111);
            InitTimer1();
            InitTimer2();

        }
        #region THREADING TIMER

        #region Thread timer1 S10F5
        private void InitTimer1()
        {
            Program.threadTimer1 = new System.Threading.Timer(new TimerCallback(TimerUp1), null, Timeout.Infinite, 1000);
        }
        //public int currentCount = 0;
        private void TimerUp1(object state)
        {
            //currentCount += 1;
            this.Invoke(new EventHandler(delegate
            {
                ScanPLC1();
            }));
        }
        private void ScanPLC1()
        {
            try
            {
                string ScanBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0C00", MCRelated.MainCmd.BinRead, MCRelated.SubCmd.BinBit, "B", "000000", "0100", "");
                string s1 = Program.mcr.SendMsg(Program.clientsocket, ScanBit, MCRelated.CommunicationType.BIN);
                if (s1.Substring(22, 1).Equals("1"))
                {
                    string MCmsgBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0D00", MCRelated.MainCmd.BinWrite, MCRelated.SubCmd.BinBit, "B", "000000", "0100", "00");
                    Program.mcr.SendMsg(Program.clientsocket, MCmsgBit, MCRelated.CommunicationType.BIN);
                }
            }
            catch (Exception ex)
            {

            }
            
        }
        #endregion

        #region Thread timer2 S6F11_341
        private void InitTimer2()
        {
            Program.threadTimer2 = new System.Threading.Timer(new TimerCallback(TimerUp2), null, Timeout.Infinite, 1000);
        }
        //public int currentCount = 0;
        private void TimerUp2(object state)
        {
            //currentCount += 1;
            this.Invoke(new EventHandler(delegate
            {
                ScanPLC2();
            }));
        }
        private void ScanPLC2()
        {
            string ScanBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0C00", MCRelated.MainCmd.BinRead, MCRelated.SubCmd.BinBit, "B", "300200", "0100", "");
            string s1 = Program.mcr.SendMsg(Program.clientsocket, ScanBit, MCRelated.CommunicationType.BIN);
            if (s1.Substring(22, 1).Equals("1"))
            {
                S6F11_341 s6F11_341 = new S6F11_341();
                Program.Sender.Send(s6F11_341);
                string MCmsgBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0D00", MCRelated.MainCmd.BinWrite, MCRelated.SubCmd.BinBit, "B", "B00000", "0100", "10");
                Program.mcr.SendMsg(Program.clientsocket, MCmsgBit, MCRelated.CommunicationType.BIN);
            }
        }
        #endregion
        #region Thread timer3 heart bit
        public static bool HBMode = false;
        private void InitTimer3()
        {
            Program.threadTimer3 = new System.Threading.Timer(new TimerCallback(TimerUp3), null, Timeout.Infinite, 1000);
            string ScanBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0C00", MCRelated.MainCmd.BinRead, MCRelated.SubCmd.BinBit, "B", "040000", "0100", "");
            string s1 = Program.mcr.SendMsg(Program.clientsocket, ScanBit, MCRelated.CommunicationType.BIN);
            if (s1.Substring(22, 1).Equals("1"))
            {
                HBMode = true;
            }
         }
        public static int HBCount = 0;
        //public int currentCount = 0;
        private void TimerUp3(object state)
        {
            //currentCount += 1;
            this.Invoke(new EventHandler(delegate
            {
                ScanPLC3();
            }));
        }
        private void ScanPLC3()
        {
            if (HBMode)
            {
                string ScanBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0C00", MCRelated.MainCmd.BinRead, MCRelated.SubCmd.BinBit, "B", "050000", "0100", "");
                string s1 = Program.mcr.SendMsg(Program.clientsocket, ScanBit, MCRelated.CommunicationType.BIN);
                if (s1.Substring(22, 1).Equals("1"))
                {
                    HBCount = 0;
                }
                else
                {
                    HBCount++;
                    if(HBCount==3)
                    {
                        MessageBox.Show("Alarm ")
                    }
                }
                S6F11_341 s6F11_341 = new S6F11_341();
                Program.Sender.Send(s6F11_341);
                string MCmsgBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0D00", MCRelated.MainCmd.BinWrite, MCRelated.SubCmd.BinBit, "B", "B00000", "0100", "10");
                Program.mcr.SendMsg(Program.clientsocket, MCmsgBit, MCRelated.CommunicationType.BIN);
            }
        }
        #endregion

        #endregion THREADING TIMER
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

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.threadTimer1.Change(0, 1000);
            Program.threadTimer2.Change(0, 1000);
            S6F11_341 s6f11_341 = new S6F11_341();
            Program.Sender.Send(s6f11_341);
        }
    }
}
