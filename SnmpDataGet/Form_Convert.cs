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

	public partial class Form_Convert : Form
	{
		public string m_textdata;
		
		public Form_Convert()
		{
			InitializeComponent();
		}

		private void Form_Convert_Load(object sender, EventArgs e)
		{
			this.m_text.Text = m_textdata;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

	}
}
