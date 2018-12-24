using DryDump;
using HSMSMessage;
using SecsDriverWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CIM
{
    class MessageReceiveHandler : IGemMessageHandlerExtension
    {

        public bool S1F1(XElement xmlMessage)
        {
            XElement primary = xmlMessage.Element("Primary");
            S1F2 s1f2 = new S1F2();
            s1f2.MDLN = Program.mdln;
            s1f2.SOFTREV = Program.softrev;
            xmlMessage.Element("Secondary").ReplaceWith(Program.Sender.Send(s1f2));
            return true;
        }
        public bool S1F2(XElement xmlMessage)
        {
            S6F11_113 s6f11_113 = new S6F11_113();
            s6f11_113.DATAID = Program.mdln;
            s6f11_113.CEID = "113";
            s6f11_113.RPTID = Program.mdln;
            s6f11_113.CRST = Program.mdln;
            s6f11_113.EQST = Program.mdln;
            xmlMessage.Element("Secondary").ReplaceWith(Program.Sender.Send(s6f11_113));
            return true;
        }
        public bool S1F5(XElement xml)
        {
            XElement primary = xml.Element("Primary");
            var a = primary.Element("Item").Element("Value").Value;
            //pri
            return true;
        }

        public bool S1F6(XElement xmlMessage)
        {
            //throw new NotImplementedException();
            return true;
        }
        public bool S2F17(XElement xmlMessage)
        {
            return true;
        }

        public bool S6F11(XElement xmlMessage)
        {
            XElement primary = xmlMessage.Element("Primary");
            XElement list = primary.Element("Item");
            string a = list.Elements("Item").ElementAt(0).Element("Value").Value;
            string b = list.Elements("Item").ElementAt(1).Element("Value").Value;
            XElement list2 = list.Elements("Item").ElementAt(2).Element("Item");

            return true;
        }

        
        public bool S6F12(XElement xmlMessage)
        {
            S2F17 s2f17 = new S2F17();
            xmlMessage.Element("Primary").ReplaceWith(Program.Sender.Send(s2f17));
            return true;
        }
        public bool S10F5(XElement xmlMessage)
        {
            string text = xmlMessage.Element("Primary").Element("Item").Elements("Item").ElementAt(1).Elements("Item").ElementAt(0).Element("Value").Value.PadLeft(64,'0');
            //Console.WriteLine(xmlMessage.Element("Primary").Element("Item").Elements("Item").ElementAt(1).Elements("Item").ElementAt(0).Element("Value").Value);
            string MCmsgWord = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "4C00", MCRelated.MainCmd.BinWrite, MCRelated.SubCmd.BinWord, "W", "000000", "2000", text);
            string s1=Program.mcr.SendMsg(Program.clientsocket, MCmsgWord, MCRelated.CommunicationType.BIN);
            string MCmsgBit = Program.mcr.CreateMsg(MCRelated.CommunicationType.BIN, "0D00", MCRelated.MainCmd.BinWrite, MCRelated.SubCmd.BinBit, "B", "000000", "0100", "01");
            Program.mcr.SendMsg(Program.clientsocket, MCmsgBit, MCRelated.CommunicationType.BIN);
            S10F6 s10f6 = new S10F6();
            Console.WriteLine("test2");
            xmlMessage.Element("Secondary").ReplaceWith(Program.Sender.Send(s10f6));
            Console.WriteLine("test3");
            return true;
        }
    }
}
