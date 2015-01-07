using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnmpDGet
{
	public partial class Form_detail : Form
	{

		// 
		private string _oid = "";
		// -oid
		private string _STRING = "";


		public string oid
		{
			get { return _oid; }
			set { _oid = value; }
		}

		public string stringdata
		{
			get { return _STRING; }
			set { _STRING = value; }
		}


		public Form_detail()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Form_detail_Load(object sender, EventArgs e)
		{
			m_oid.Text = _oid;
//			m_objectname.Text = Class_headerConv.headerConvert(_oid.Substring(0,_oid.LastIndexOf('.')+1));
			m_objectname.Text = Class_headerConv.headerConvert(_oid);
			m_string.Text = stringdata;
		}

		//変換
		private void button2_Click(object sender, EventArgs e)
		{
			Form_Convert convform = new Form_Convert();
			convform.m_textdata = Class_snmpwalk.convertJP(this.m_string.Text);
			convform.Show();
			convform.Owner = this;

		}
		//変換
		private void 変換cToolStripMenuItem_Click(object sender, EventArgs e)
		{

			Form_Convert convform = new Form_Convert();
			convform.m_textdata = Class_snmpwalk.convertJP(this.m_string.Text);
			convform.Show();
			convform.Owner = this;
		}
	}
}
