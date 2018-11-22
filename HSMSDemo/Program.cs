
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

namespace CIM
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
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
