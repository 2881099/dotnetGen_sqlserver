using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ServerWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Server.Protocol _prol;
        private void Form1_Load(object sender, EventArgs e)
        {
            Deflate.cs_head = Settings.Default.cs_head;
            _prol = Server.Protocol.Create(Settings.Default.socket_port);
			Server.Protocol.debugAppendLog = log => safeAppendText(this.textBox1, log);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _prol.Dispose();
        }

		delegate void SafeSetText(Component c, string text);
		protected void safeSetText(Component c, string text) {
			if (this.InvokeRequired)
				this.Invoke(new SafeSetText(safeSetText), new object[] { c, text });
			else {
				if (c is ToolStripItem) {
					ToolStripItem o = c as ToolStripItem;
					if (o == null) return;
					o.Text = text;
				} else if (c is Control) {
					Control o = c as Control;
					if (o == null) return;
					o.Text = text;
				}
			}
		}
		protected void safeAppendText(Component c, string text) {
			if (this.InvokeRequired)
				this.Invoke(new SafeSetText(safeAppendText), new object[] { c, text });
			else {
				TextBox o = c as TextBox;
				if (o == null) return;
				o.AppendText(DateTime.Now.ToString("MM:ss ") + text + "\r\n");
				o.ScrollToCaret();
			}
		}
	}
}