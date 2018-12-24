using SecsDriverWrapper;
using System;
using System.Windows.Forms;
using System.Threading;
using CCWin;
using DryDump;
using HSMSMessage;
using System.Xml.Linq;
using HslCommunication.ModBus;
using HslCommunication;
using System.Net.Sockets;
using System.Net;

namespace CIM
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        public static ISecsDriverWrapper wrapper = new SecsWellWrapper();
        public static MessageSender Sender = new MessageSender(wrapper);
        public static string XmlPath = AppDomain.CurrentDomain.BaseDirectory+@"/ConfigureFiles/HSMS/HostInfo.xml";
        public static string unitid = null;
        public static string mdln = null;
        public static string softrev = null;
        public static IPAddress ip=null;
        public static int port ;
        public static FileRelated filer = new FileRelated();
        public static MCRelated mcr = new MCRelated();
        //public static string MCmsg = null;
        //public static ModbusTcpNet busTcpClient = new ModbusTcpNet("192.168.1.195",502, 0x01);   // 站号1
        public static System.Threading.Timer threadTimer1;
        public static System.Threading.Timer threadTimer2;
        public static System.Threading.Timer threadTimer3;
        public static Socket clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static bool PLCFLAG = false;
        [STAThread]
        static void Main()
        {
            Initialize();
            //设置正在运行的软件不能重复再次打开  Mutex互斥
            bool createNew;
            Mutex mutex = new Mutex(true, "CIM", out createNew);
            if (createNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBoxEx.Show("程序正在运行中，请勿重复打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Thread.Sleep(1000);
            }
            System.Environment.Exit(1);
            #region annotation
            /*
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "serverlog.config")));
            ISecsDriverWrapper wrapper = new SecsWellWrapper();
            wrapper.Register(new MessageReceiveHandler());
            wrapper.OpenHSMSPort();
            Console.ReadLine();
            MessageSender sender = new MessageSender(wrapper);
            
            S2F37 s2f37 = new S2F37();
            s2f37.CEED = "abc";
            s2f37.CEID = "cba";
            sender.Send(s2f37);
            MessageReceiveHandler messageReceive = new MessageReceiveHandler();
            Console.WriteLine(messageReceive);
            Console.ReadLine();
            wrapper.CloseHSMSPort();
            */
            #endregion annotation
        }
        public static void Initialize()
        {

            unitid = filer.ReadXml(XmlPath, "HostInfo", "UnitID");
            mdln = filer.ReadXml(XmlPath, "HostInfo", "MDLN");
            softrev = filer.ReadXml(XmlPath, "HostInfo", "SOFTREV");
            ip = IPAddress.Parse(filer.ReadXml(XmlPath, "HostInfo", "PLCIPAddress"));
            port = Convert.ToInt16(filer.ReadXml(XmlPath, "HostInfo", "PLCIPPort"));
            if (mcr.SocketLink(clientsocket, ip, port))
            {
                MessageBox.Show("PLC connected");
                PLCFLAG = true;
            }
            else
                MessageBox.Show("PLC disconnected");
            wrapper.Register(new MessageReceiveHandler());
            wrapper.OpenHSMSPort();
            //int int100 = busTcpClient.ReadInt32("100").Content;      // 读取寄存器100-101的int值
            //Console.WriteLine(int100);

        }

    }
}
