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
    public partial class SECSSetting : Form
    {
        public SECSSetting()
        {
            InitializeComponent();
        }
        #region 窗体单例
        private static SECSSetting formInstance;
        public static SECSSetting CreateFrom()
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个字窗体
            if (formInstance == null || formInstance.IsDisposed)
            {
                formInstance = new SECSSetting();
            }
            return formInstance;
        }
        #endregion 窗体单例
    }
}
