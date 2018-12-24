using HSMSMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecsDriverWrapper;
using HSMSDriver;
using System.Xml.Linq;

namespace CIM
{
    class MessageSender
    {
        private ISecsDriverWrapper Wrapper;

        public MessageSender(ISecsDriverWrapper wrapper)
        {
            this.Wrapper = wrapper;
        }
        public void Send(S1F0 msg)
        {
            XElement primary = new XElement("Primary");
            Wrapper.SendAsync(Program.unitid, "S1F0", primary);
        }
        public XElement Send(S1F1 msg)
        {
            XElement primary = new XElement("Primary");
            Wrapper.SendAsync(Program.unitid, "S1F1", primary);
            return primary;
        }
        public XElement Send(S1F2 msg)
        {
            XElement secondary = new XElement("Secondary");
            var list = NewItem("L", "", "LIST");
            list.Add(NewItem("MDLN", "", "ASCII", msg.MDLN));
            list.Add(NewItem("SOFTREV", "", "ASCII", msg.SOFTREV));
            secondary.Add(list);
            Wrapper.SendAsync(Program.unitid, "S1F2", secondary);
            return secondary;
        }
        public XElement Send(S2F17 msg)
        {
            XElement primary = new XElement("Primary");
            Wrapper.SendAsync(Program.unitid, "S2F17", primary);
            return primary;
        }
        public void Send(S2F31 msg)
        {
            XElement primary = new XElement("Primary");
            var time = NewItem("Time", "", "ASCII");
            time.Add(new XElement("Value", msg.time));
            primary.Add(time);
            Wrapper.SendAsync(Program.unitid, "S2F31", primary);
        }
        public void Send(S2F37 msg)
        {
            XElement primary = new XElement("Primary");
            var list = NewItem("L", "", "LIST");
            list.Add(NewItem("CEED", "", "ASCII", msg.CEED));
            var list2 = NewItem("L", "", "LIST");
            list2.Add(NewItem("CEID", "", "ASCII", msg.CEID));
            list.Add(list2);
            primary.Add(list);
            Wrapper.SendAsync(Program.unitid, "S2F37", primary);
        }
        public XElement Send(S6F11_111 msg)
        {
            XElement primary = new XElement("Primary");
            var list = NewItem("L", "", "LIST");
            list.Add(NewItem("DATAID", "", "ASCII", msg.DATAID));
            list.Add(NewItem("CEID", "", "ASCII", msg.CEID));
            var list2 = NewItem("L", "", "LIST");
            var list3 = NewItem("L", "", "LIST");
            list3.Add(NewItem("RPTID", "", "ASCII", msg.RPTID));
            var list4 = NewItem("L", "", "LIST");
            list4.Add(NewItem("CRST", "", "ASCII", msg.CRST));
            list4.Add(NewItem("EQST", "", "ASCII", msg.EQST));
            list3.Add(list4);
            list2.Add(list3);
            list.Add(list2);
            primary.Add(list);
            Wrapper.SendAsync(Program.unitid, "S6F11", primary);
            return primary;
        }
        public XElement Send(S6F11_113 msg)
        {
            XElement primary = new XElement("Primary");
            var list = NewItem("L", "", "LIST");
            list.Add(NewItem("DATAID", "", "ASCII", msg.DATAID));
            list.Add(NewItem("CEID", "", "ASCII", msg.CEID));
            var list2 = NewItem("L", "", "LIST");
            var list3 = NewItem("L", "", "LIST");
            list3.Add(NewItem("RPTID", "", "ASCII", msg.RPTID));
            var list4 = NewItem("L", "", "LIST");
            list4.Add(NewItem("CRST", "", "ASCII", msg.CRST));
            list4.Add(NewItem("EQST", "", "ASCII", msg.EQST));
            list3.Add(list4);
            list2.Add(list3);
            list.Add(list2);
            primary.Add(list);
            Wrapper.SendAsync(Program.unitid, "S6F11", primary);
            return primary;
        }
        public XElement Send(S6F11_341 msg)
        {
            XElement primary = new XElement("Primary");
            var list = NewItem("L", "", "LIST");
            list.Add(NewItem("DATAID", "", "ASCII", msg.DATAID));
            list.Add(NewItem("CEID", "", "ASCII", "341"));
            var list2 = NewItem("L", "", "LIST");
            var list3 = NewItem("L", "", "LIST");
            list3.Add(NewItem("RPTID", "", "ASCII", msg.RPTID));
            var list4 = NewItem("L", "", "LIST");
            list4.Add(NewItem("CRST", "", "ASCII", msg.CRST));
            list4.Add(NewItem("EQST", "", "ASCII", msg.EQST));
            var list5 = NewItem("L", "", "LIST");
            list5.Add(NewItem("RPTID", "", "ASCII", msg.RPTID));
            var list6 = NewItem("L", "", "LIST");
            list6.Add(NewItem("UNITID", "", "ASCII", msg.UNITID));
            list6.Add(NewItem("LOTID", "", "ASCII", msg.LOTID));
            list6.Add(NewItem("IPID", "", "ASCII", msg.IPID));
            list6.Add(NewItem("OPID", "", "ASCII", msg.OPID));
            list6.Add(NewItem("ICID", "", "ASCII", msg.ICID));
            list6.Add(NewItem("OCID", "", "ASCII", msg.OCID));
            list6.Add(NewItem("PPID", "", "ASCII", msg.PPID));
            list6.Add(NewItem("FSLOTID", "", "ASCII", msg.FSLOTID));
            list6.Add(NewItem("TSLOTID", "", "ASCII", msg.TSLOTID));
            list6.Add(NewItem("RPNLID", "", "ASCII", msg.RPNLID));
            list6.Add(NewItem("HPNLID", "", "ASCII", msg.HPNLID));
            list6.Add(NewItem("PNLJUDGE", "", "ASCII", msg.PNLJUDGE));
            list6.Add(NewItem("PNLGRADE", "", "ASCII", msg.PNLGRADE));
            list6.Add(NewItem("OPERMODE", "", "ASCII", msg.OPERMODE));
            list5.Add(list6);
            list3.Add(list4);
            list2.Add(list3);
            list2.Add(list5);
            list.Add(list2);
            primary.Add(list);
            Wrapper.SendAsync(Program.unitid, "S6F11", primary);
            return primary;
        }
        public XElement Send(S10F6 msg)
        {
            XElement secondary = new XElement("Secondary");
            secondary.Add(NewItem("ACKC10", "", "ASCII", "ACKC10"));
            Wrapper.SendAsync(Program.unitid, "S10F6", secondary);
            return secondary;
        }
        private XElement NewItem(string name, string description, string format)
        {
            return 
                new XElement("Item",new XElement("Name", name),
                new XElement("Description", description),
                new XElement("Format", format));
        }
        private XElement NewItem(string name, string description, string format, string value)
        {
            return 
                new XElement("Item",new XElement("Name", name),
                new XElement("Description", description),
                new XElement("Format", format),
                new XElement("Value", value));
        }
    }
}
