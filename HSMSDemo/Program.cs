
using HSMSMessage;
using SecsDriverWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using CCWin;

namespace CIM
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
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
        }
        private static void InitLog4Net()
        {
            //var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.Configure();
        }
    }
}
