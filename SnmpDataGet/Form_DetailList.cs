using System;

using System.Windows.Forms;
using System.Collections.Generic;

namespace SnmpDGet
{


	public partial class Form_DetailList : Form
	{

		private Dictionary<string, string> _LISTHASH;

		private Class_ListViewColumnSorter _columnSorter;		

		//リストに表示するデータの取得
		public Dictionary<string, string> hashlist
		{
			get { return _LISTHASH; }
			set { _LISTHASH = value ; }

		}
	
		public Form_DetailList()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Form_DetailDisp_Load(object sender, EventArgs e)
		{

			_columnSorter = new Class_ListViewColumnSorter();
			listView1.ListViewItemSorter = _columnSorter;

			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;

			//リストビューを初期化する
			listView1.Columns.Add("オブジェクト");
			listView1.Columns.Add("値");

			//データの挿入
			foreach (KeyValuePair<string, string> v in hashlist)
			{
				string[] item = { v.Key.ToString(), v.Value.ToString()};
				listView1.Items.Add(new ListViewItem(item));
			}
			//カラムの幅を合わせる
			foreach (ColumnHeader ch in listView1.Columns)
			{
				ch.Width = -1;
			}
		}


		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			ListViewItem item = listView1.SelectedItems[0];
			Form_detail formdetail = new Form_detail();

			formdetail.oid = item.SubItems[0].Text;
			formdetail.stringdata = item.SubItems[1].Text;
			formdetail.Show();
			formdetail.Owner = this;

		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column == _columnSorter.SortColumn)
			{
				if (_columnSorter.Order == SortOrder.Ascending)
				{
					_columnSorter.Order = SortOrder.Descending;
				}
				else
				{
					_columnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				_columnSorter.SortColumn = e.Column;
				_columnSorter.Order = SortOrder.Ascending;
			}
			listView1.Sort();
		}

		//変換
		private void 変換ToolStripMenuItem_Click(object sender, EventArgs e)
		{

			ListViewItem item = listView1.SelectedItems[0];
			Form_Convert convform = new Form_Convert();
			convform.m_textdata = Class_snmpwalk.convertJP(item.SubItems[1].Text);
			convform.Show();
			convform.Owner = this;

		}
	}
}
