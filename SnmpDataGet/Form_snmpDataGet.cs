using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace SnmpDGet
{
    public partial class Form_snmpDataGet : Form
    {


		List<Dictionary<string, string>> miblist;

		//ログのインスタンス定義
		Class_TextLog CLog = new Class_TextLog();
		DataTable dt ;
		private int sort_kind = 0;

		//walkの時のOIDは固定
		private static string WALKOID = ".1.3.6.1.2.1";
		//tableの時は
		private static string TABLEOID = ".1.3.6.1.2.1.2.2";
		
		//コマンドラインからの起動の時はTRUE
		public bool CommandLine;

		ArrayList getlist;
		//ArrayList walklist;
		//ArrayList tablelist;

		//中断ボタンが押された時TRUE
		public Boolean cancelflg = false;
		public BackgroundWorker worker;

		public Form_snmpDataGet()
        {
            InitializeComponent();
        }
		//
		// 引数が示すindexのアイテムを返すと描画される
		//
		void listView2_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{

			//	e.Item = _item[e.ItemIndex];
			DataRow row = dt.Rows[e.ItemIndex];
			e.Item = new ListViewItem(
				new String[]
				{ 
					Convert.ToString(row[0]), 
					Convert.ToString(row[1]), 
					Convert.ToString(row[2]), 
					Convert.ToString(row[3])
					});
		}
        //表示前処理
        private void Form_snmpDataGet_Load(object sender, EventArgs e)
        {
			//ログファイルのオープン
			CLog.Open(Properties.Settings.Default.LogFilePath);

			try { 
				listView2.VirtualMode = true;
				// １行全体選択
				listView2.FullRowSelect = true;
				listView2.HideSelection = false;
				//listView2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
				listView2.HeaderStyle = ColumnHeaderStyle.Clickable;

				ImageList imageListSmall = new ImageList();
				imageListSmall.ImageSize = new Size(1, 25);
				listView2.SmallImageList = imageListSmall;

				// Column追加
				listView2.Columns.Insert(0, "No", 30, HorizontalAlignment.Left); 
				listView2.Columns.Insert(1, "OID", 180, HorizontalAlignment.Left);
				listView2.Columns.Insert(2, "タイプ", 80, HorizontalAlignment.Left);
				listView2.Columns.Insert(3, "値", 180, HorizontalAlignment.Left);

				//Hook up handlers for VirtualMode events.
				listView2.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView2_RetrieveVirtualItem);


                //NECE用はドメインのチェックは行わない

				//バックグランドワーカーのイベントハンドらの定義
				//RunWorkerAsyncが呼び出された時
				bgworker.DoWork += new DoWorkEventHandler(bgworker_DoWork);
				bgworker.WorkerSupportsCancellation = true;

				suspendBtn.Enabled = false;


				this.tabControl1.TabPages[0].Text = "System";
				this.tabControl1.TabPages[1].Text = "Interfaces";
				this.tabControl1.TabPages[2].Text = "一覧";

				//OIDをコンボボックスに設定
				//コンボボックスの値はファイルから読み込む
				getComboData();

				this.m_getRadio.Checked = true;
				this.groupBox1.Enabled = false;
			
				this.m_OIDcombo.Enabled = false;
				tabControl1.SelectedIndex = 0;
				this.m_OIDcombo.Text = WALKOID + ",mib-2(固定)";

				//左端のレコードセレクタを非表示
				//dataGridView1.RowHeadersVisible = false;
            
				this.m_versioncombo.SelectedIndex = 1;

				//バージョン情報
				System.Diagnostics.FileVersionInfo ver = 
					System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
				this.m_versioninfo.Text = "ver:" + ver.FileVersion;

				//製造元
				System.Reflection.AssemblyCopyrightAttribute asmcpy =
					(System.Reflection.AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),
					typeof(System.Reflection.AssemblyCopyrightAttribute));
				toolStripStatusLabel1.Text = asmcpy.Copyright;


				//コマンドラインからの起動の時
				if (System.Environment.GetCommandLineArgs().Length > 2)
				{
					//値の表示
					Class_paramerter param = new Class_paramerter();
					int ret = startFromCommand(ref param);
					if (ret == -1)
					{

						//エラーがあった場合は即終了
						this.Close();
					}
					//即実行する
					//m_OK.PerformClick();

					executeSnmpGet(this.tabControl1.SelectedIndex);
				}

			}
			catch(Exception ex)
			{
				CLog.Write(ex.Message);
				MessageBox.Show(ex.Message);

			}
			finally { 
				if(CLog != null) CLog.Close();
			}
		}

		//コマンドラインから実行の時はパラメータを画面に表示する
		private int startFromCommand(ref Class_paramerter param){

			Dictionary<string, Boolean> flgDict = new Dictionary<string, Boolean>(){
					{"ipaddress",false},
					{"type",true},
					{"version",true},
					{"community",true},
					{"oid",false}	 
				 };

			//パラメータ解析
			int i = 0;
			//パラメータを取得
			string[] args = System.Environment.GetCommandLineArgs();

			for (i = 0; i < args.Length; i++)
			{
				switch (args[i])
				{
					//ホスト名IPアドレス
					case "-ip":
					case "-IP":
						i += 1;
						if (i < args.Length)
						{
							param.ipaddress = args[i].Trim('"');
							flgDict["ipaddress"] = true;
						}
						continue;

					//実行タイプ
					//get 
					//walk or bulk 
					//table
					case "-t":
					case "-T": 
						i += 1;
						if (i < args.Length)
						{
							param.type = args[i].Trim('"').ToLower();
							flgDict["type"] = true;
						}
						continue;
					//バージョン
					case "-v":
					case "-V":
						i += 1;

						if (i < args.Length)
						{
							param.version = args[i].Trim('"').ToLower();

							flgDict["version"] = true;
						}
						continue;
					//コミュニティ 
					case "-c":
					case "-C":
						i += 1;

						if (i < args.Length)
						{
							param.community = args[i].Trim('"').ToLower();
							flgDict["community"] = true;
						}
						continue;
					//OID
					case "-o":
					case "-O":

						i += 1;

						if (i < args.Length )
						{
							param.oid = args[i].Trim('"');
							flgDict["oid"] = true;
						}
						continue;
				}
			}
			//パラメータの入力チェック
			//GETでOIDの入力がなかったらエラー
			foreach (KeyValuePair<string, Boolean> vdict in flgDict)
			{

				if (vdict.Value == false)
				{
					if (vdict.Key == "oid" & param.type == "get")
					{
						CLog.Write("OIDを指定してください。");
						MessageBox.Show("OIDを指定してください。", "SnmpDGet", MessageBoxButtons.OK, MessageBoxIcon.Error);

						return -1;
					}
					else if (vdict.Key != "oid" )
					{
						CLog.Write("パラメータが正しく設定されていません。");
						MessageBox.Show("パラメータが正しく設定されていません。", "SnmpDGet", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return -1;

					}
				}
			}

			//チェックがOKの場合のみデータを取得する
			string oidstr = "";
			//タイプ
			//getの時
			if (param.type == "get")
			{
				this.m_getRadio.Checked = true;
				this.tabControl1.SelectedIndex = 2;
				oidstr = param.oid;
			}
			// walkの時
			else if (param.type == "walk" | param.type == "bulk")
			{
				this.m_walkRadio.Checked = true;
				this.tabControl1.SelectedIndex = 2;
				//OID
				oidstr = param.oid;
			}
			//テーブルの時
			else if (param.type == "table" | param.type == "interfaces")
			{
				//this.m_tableRadio.Checked = true;
				this.tabControl1.SelectedIndex = 1;
				//OID
				oidstr = TABLEOID + ",ifTable(固定)";
			}
			else
			{
				//Systemの時
				this.tabControl1.SelectedIndex = 0;
				//OID
				oidstr = WALKOID + ",mib-2(固定)";
			}
			//OID
			this.m_OIDcombo.Text = oidstr;
			//ホスト名/IP
			this.m_host.Text = param.ipaddress;

			//コミュニティ
			this.m_commu.Text = param.community;

			//バージョン
			if (param.version == "1" | param.version == "v1")
			{
				this.m_versioncombo.Text = "v1";
			}
			else if (param.version == "2" | param.version == "v2c" | param.version == "2c")
			{
				this.m_versioncombo.Text ="v2c";

			}
            else if (param.version == "0")
            {
                //0の時
                CLog.Write("指定されたバージョンが不正です。");
                MessageBox.Show("指定されたバージョンが不正です。", "SnmpDGet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;

            }

			return 0;

		}
        //実行ボタンが押された時
        private void m_OK_Click(object sender, EventArgs e)
        {
			
			int index = this.tabControl1.SelectedIndex;
			//ログ書き込みの開始
			CLog.Open(Properties.Settings.Default.LogFilePath);

			executeSnmpGet(index);
		}

		// SNMPGETを行う
		private void executeSnmpGet(int index)
		{

			//入力チェック
			if (this.m_host.Text == "")
			{
				MessageBox.Show("ホスト名が未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				m_host.Focus();
				CLog.Close();
				return;
			}
			if (this.m_commu.Text == "")
			{
				MessageBox.Show("コミュニティが未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				m_commu.Focus();
				CLog.Close();
				return;
			}
			if (this.m_versioncombo.Text == "")
			{
				MessageBox.Show("バージョンが未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				m_versioncombo.Focus();
				CLog.Close();
				return;

			}
			if (this.m_OIDcombo.Text == "" & (this.m_getRadio.Checked | this.m_walkRadio.Checked) )
			{
				MessageBox.Show("OIDが未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				m_OIDcombo.Focus();
				CLog.Close();
				return;
			}

			groupBox1.Enabled = false;
			m_host.Enabled = false;
			m_commu.Enabled = false;
			m_versioncombo.Enabled = false;
			m_OIDcombo.Enabled = false;
			tabControl1.Enabled = false;

			suspendBtn.Enabled = true;
			m_end.Enabled = false;
			m_OK.Enabled = false;

			//情報取得
			toolStripStatusLabel1.Text = "情報取得中....";
			System.Windows.Forms.Application.DoEvents();

			Class_InputData input = new Class_InputData();

			if (this.m_getRadio.Checked)
			{
				input.method = 1;

			}
			else if (this.m_walkRadio.Checked)
			{
				input.method = 2;

			}

			//元のカーソルを保持
			Cursor preCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			Stopwatch sw = new Stopwatch();
			sw.Start();

			input.hostname = this.m_host.Text;
			input.community = this.m_commu.Text;
			input.version = this.m_versioncombo.Text;

			//OID取得
			string str = this.m_OIDcombo.Text;
			string outoid;
			string[] starray = str.Split(',');
			outoid = starray[0];
			input.oid = outoid;

			this.dataGridView1.Rows.Clear();
			listView2.Clear();

			m_sysDescr.Text = "";
			m_sysObjectID.Text= "";
			m_sysUpTime.Text = "";
			m_sysContact.Text = "";
			m_sysName.Text = "";
			m_sysLocation.Text = "";
			m_sysServices.Text = "";


			
			try
			{
				//system情報のみの場合
				if (index == 0)
					//GETリクエストを送信
					systemDisp(input);
				// テーブルが選択された時
				else if(index == 1)
					//.1.3.6.1.2.1.2.2
					getTable(input);
				else 
					if (m_getRadio.Checked == true)
						//GETの時
						Dispget(input);
					else
						//input.oid = WALKOID;
						//walk or tableの時
						Displist(input);
			}
			catch (Exception ex)
			{
				string msg = "";
				msg = ex.Message;
				if (ex.Message.Contains("Request has reached maximum retries."))
				{
					msg = "リトライ回数に達しましたが、対象ホストにアクセスできませんでした。";
				}
				if (ex.Message.Contains("Unable to parse or resolve supplied value to an IP address."))
				{
					msg = "ホスト名/IPアドレスが正しく取得できませんでした。";
				}
				MessageBox.Show(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

				CLog.Write(msg);
				
				return;
			}
			finally{


				tabControl1.Enabled = true;
				m_host.Enabled = true;
				m_commu.Enabled = true;
				m_versioncombo.Enabled = true;
				

				if(this.tabControl1.SelectedIndex == 2){
					m_OIDcombo.Enabled = true;
					groupBox1.Enabled = true;
				}

				suspendBtn.Enabled = false;
				m_end.Enabled = true;
				m_OK.Enabled = true;

				toolStripStatusLabel1.Text = "";
				Cursor.Current =  preCursor ;
				sw.Stop();
				toolStripStatusLabel1.Text = sw.Elapsed.ToString();

				if (CLog != null) CLog.Close();
			}
		}

		//テーブルタブ選択時
		private void getTable(Class_InputData input)
		{
			input.oid = TABLEOID;
			input.method = 3;

			Class_snmpwalk snmpwalk = new Class_snmpwalk(input);
			int ret = snmpwalk.listGet(this);

			toolStripStatusLabel1.Text = "テーブル表示中....";
			System.Windows.Forms.Application.DoEvents();
			
			//headerリストを取得する
			List<string> headerLst = snmpwalk.headerList;

			//ヘッダに表示するカラム表示用リスト
			List<string> DispheaderList = new List<string>();



			//データの挿入
			if (snmpwalk.TableDispArray.Count <= 0) {
				MessageBox.Show("データが取得できませんでした。","SnmpDG",MessageBoxButtons.OK,MessageBoxIcon.Error);	
			}
			
			
			foreach (var key in snmpwalk.TableDispArray.Keys)
			{
				this.dataGridView1.Rows.Add(snmpwalk.TableDispArray[key].ToArray());
			}

			//dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
			//listView1.EndUpdate();		
		}

		//システムタブの項目表示
		private void systemDisp(Class_InputData input)
		{			
			Class_snmpGet snmpget = new Class_snmpGet();
			try
			{
				CLog.Write("System  ホスト : " + input.hostname + " : " + input.version + " : " + input.community + " : " + input.oid);
				snmpget.getSystemInfo(input, CLog);

				//データの挿入
				if (snmpget.systemhash == null)
				{
					MessageBox.Show("値を取得できませんでした。", "SnmpDGet", MessageBoxButtons.OK, MessageBoxIcon.Error);
					CLog.Write("System ERROR 値を取得できませんでした。ホスト名：" + input.hostname);
					return;
				}

				foreach (KeyValuePair<string, string> v in snmpget.systemhash)
				{
					//システム
					//ホスト名
					if (v.Key.IndexOf("sysDescr") > -1)
					{
						this.m_sysDescr.Text = v.Value;
					}
					// オブジェクトID
					else if (v.Key.IndexOf("sysObjectID") > -1)
					{
						this.m_sysObjectID.Text = v.Value;
					}
					else if (v.Key.IndexOf("sysUpTime") > -1)
					{
						this.m_sysUpTime.Text = v.Value;
					}
					else if (v.Key.IndexOf("sysContact") > -1)
					{
						this.m_sysContact.Text = v.Value;
					}
					else if (v.Key.IndexOf("sysName") > -1)
					{
						this.m_sysName.Text = v.Value;
					}
					else if (v.Key.IndexOf("sysLocation") > -1)
					{
						this.m_sysLocation.Text = v.Value;
					}
					else if (v.Key.IndexOf("sysServices") > -1)
					{
						this.m_sysServices.Text = v.Value;
					}
				}
			}
			catch(Exception ex)
			{
				CLog.Write("System ERROR " + ex.Message + "ホスト名：" + input.hostname);
				throw;
			}
			CLog.Write("System 終了  ホスト名：" + input.hostname);
		}
		//GETデータの表示
		private void Dispget(Class_InputData input)
		{
			Class_snmpGet snmpget = new Class_snmpGet();

			//GETリクエストを送信
			snmpget.getRequest(input);

			if (snmpget.resulthash == null)
			{
				MessageBox.Show("値を取得できませんでした。No Such Name OID ! ", "SnmpDGet", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.listView2.Clear();

			//リストビューを初期化する
			dt = new DataTable("table1");

			listView2.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
			listView2.Columns.Insert(1, "OID", 180, HorizontalAlignment.Left);
			listView2.Columns.Insert(2, "タイプ", 80, HorizontalAlignment.Left);
			listView2.Columns.Insert(3, "値", 180, HorizontalAlignment.Left);

			dt.Columns.Add("No", Type.GetType("System.Int32"));
			dt.Columns.Add("OID", Type.GetType("System.String"));
			dt.Columns.Add("タイプ", Type.GetType("System.String"));
			dt.Columns.Add("値", Type.GetType("System.String"));

			//データの挿入

			//データの挿入
			DataRow row = dt.NewRow();
			row["No"] = 1;
			row["OID"] = snmpget.resulthash["oid"].ToString();
			row["タイプ"] = snmpget.resulthash["type"].ToString();
			row["値"] = snmpget.resulthash["value"].ToString();
			dt.Rows.Add(row);

			listView2.VirtualListSize = 1;
			listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


			//カラムの幅を合わせる
			foreach (ColumnHeader ch in listView2.Columns)
			{
				ch.Width = -1;
			}
		}

		//一覧に表示する
		private void Displist(Class_InputData input )
		{
			Class_snmpwalk snmpwalk = new Class_snmpwalk(input);

			//SNMPデータを取得する
			int ret = snmpwalk.listGet(this);
			long cnt = 0;
			miblist = snmpwalk.resultList;
			
			toolStripStatusLabel1.Text = "リスト表示中....";
			System.Windows.Forms.Application.DoEvents();
			
			if (this.m_walkRadio.Checked == true  ){

				//リストビューを初期化する
				dt = new DataTable("table1");

				// Column追加
				listView2.Columns.Insert(0, "No", 180, HorizontalAlignment.Left); 
				listView2.Columns.Insert(1, "OID", 180, HorizontalAlignment.Left);
				listView2.Columns.Insert(2, "タイプ", 80, HorizontalAlignment.Left);
				listView2.Columns.Insert(3, "値", 180, HorizontalAlignment.Left);

				dt.Columns.Add("No", Type.GetType("System.Int32"));
				dt.Columns.Add("OID", Type.GetType("System.String"));
				dt.Columns.Add("タイプ", Type.GetType("System.String"));
				dt.Columns.Add("値", Type.GetType("System.String"));


				cancelflg = false;

				//リストの中にある件数を取得
				//_item = new ListViewItem[miblist.Count];
				//データの挿入
				foreach (Dictionary<string, string> v in miblist)
				{
					//System.Windows.Forms.Application.DoEvents();
					//if (cancelflg)
					//{

					//	//中断された時
					//	break;
					//}
					DataRow row = dt.NewRow();
					row["No"] = cnt + 1;
					row["OID"] = v["oid"].ToString();
					row["タイプ"] =  v["type"].ToString();
					row["値"] = v["value"].ToString();
					dt.Rows.Add(row);

					cnt++ ;
				}
				listView2.VirtualListSize = miblist.Count;
				listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
			
		}

		//ファイルを読み込みデータを取得
        private void getComboData()
		{
            try
            {


                string line = "";
                getlist = new ArrayList();

                string path = System.Windows.Forms.Application.StartupPath;

                
                using (StreamReader sr = new StreamReader(
                        path + "\\" + "getOIDList.txt", Encoding.GetEncoding("Shift_JIS")))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        //#はコメント
                        if (line.StartsWith("#"))
                        {
                            continue;
                        }
                        getlist.Add(line.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                CLog.Write("ファイル読み込み時のエラー" + ex.Message);
                MessageBox.Show("ファイル読み込み時のエラー" + ex.Message , "SnmpDGet", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
		}

		//閉じるボタン
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		//ラジオボタンがクリックされた時
		private void m_getRadio_CheckedChanged(object sender, EventArgs e)
		{

				//GETをチェックしたとき				
				this.m_OIDcombo.DataSource = getlist;
				this.m_OIDcombo.Enabled = true;
				this.tabControl1.SelectedIndex = 2;

		}

		//テーブルのキーダウン
		private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			//Ctrl + c
			if (e.KeyData == (Keys.Control | Keys.C))
			{
				if (dataGridView1.SelectedRows.Count > 0)
				{
					string clip = "";
					string aa = "";
					int i = 0;
					//ヘッダーを取得
					for (int hIndex = 0; hIndex < dataGridView1.Columns.Count; hIndex++)
					{
						clip += dataGridView1.Columns[hIndex].HeaderText.ToString() + ",";
					}
					clip += Environment.NewLine;

					//データを取得
					DataGridViewRowCollection rows = dataGridView1.Rows;
					for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
					{
						DataGridViewRow row = rows[rowIndex];

						//選択のチェック
						if (!row.Selected) continue;

						//カラム数分ループ
						for (i = 0; i < row.Cells.Count - 1; i++)
						{

							if (row.Cells[i].Value == null)
								aa = "";
							else
								aa = row.Cells[i].Value.ToString().TrimEnd('\0');
							clip += aa + ",";
						}
						if (row.Cells[i].Value == null)
							aa = "";
						else
							aa = row.Cells[i].Value.ToString().TrimEnd('\0');

						clip += aa + Environment.NewLine;
					}

					Clipboard.SetText(clip);

					//本来のキーダウンを無効にする
					e.IsInputKey = true;
				}
			}
			//ENTERキーが押された時
			else if (e.KeyData == Keys.Return)
			{

				if (dataGridView1.SelectedRows.Count < 0 ) return;

				Form_DetailList Form_Detail = new Form_DetailList();


				Form_Detail.hashlist = new Dictionary<string, string>();

				for (int j = 0; j < dataGridView1.Columns.Count; j++)
				{
					if (this.dataGridView1.SelectedRows[0].Cells[j].Value != null)
						Form_Detail.hashlist[dataGridView1.Columns[j].HeaderText] = dataGridView1.SelectedRows[0].Cells[j].Value.ToString();
				}

				Form_Detail.Show();
				Form_Detail.Owner = this;
				//本来のキーダウンを無効にする
				e.IsInputKey = true;
				return;
			}

		}



		//リストビューのキーダウンMIBの方
		private void listView2_KeyDown(object sender, KeyEventArgs e)
		{
			
			//Ctrl + c
			if (e.KeyData == ( Keys.Control | Keys.C ))
			{

				if (listView2.SelectedIndices.Count > 0)
				{
					string clip = "";

					ListView.SelectedIndexCollection item = listView2.SelectedIndices;

					foreach (int idx in item)
					{
						clip += string.Format("{0},{1},{2},{3}" + Environment.NewLine,
									listView2.Items[idx].SubItems[0].Text.TrimEnd('\0'),
									listView2.Items[idx].SubItems[1].Text.TrimEnd('\0'),
									listView2.Items[idx].SubItems[2].Text.TrimEnd('\0'),
									listView2.Items[idx].SubItems[3].Text.TrimEnd('\0'));
					}
					Clipboard.SetText(clip);
				}
			}
			//全件選択
			else if (e.KeyData == (Keys.Control | Keys.A))
			{
				listView2.BeginUpdate();

				listView2.SelectedIndices.Clear();
				for (int i = 0; i < this.dt.Rows.Count; ++i)
					listView2.SelectedIndices.Add(i);

				listView2.EndUpdate();
				
			}
			//ENTERキーが押された時
			else if (e.KeyData == Keys.Return)
			{
				ListView.SelectedIndexCollection item = listView2.SelectedIndices;
				
				Form_detail formdetail = new Form_detail();
				formdetail.oid = listView2.Items[item[0]].SubItems[1].Text;
				formdetail.stringdata = listView2.Items[item[0]].SubItems[3].Text;
				formdetail.Show();
				formdetail.Owner = this;

			}
		
		}

		//右クリックからのコピー
		private void コピーToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//現在選択されているタブ
			int index = tabControl1.SelectedIndex;
			int i = 0;
			
			//1つめのタブ
			if (index == 1)
			{
				// 選択されているかどうか
				if (this.dataGridView1.SelectedRows.Count > 0)
				{
					string clip = "";
					string aa = "";

					//ヘッダーを取得
					for (int hIndex = 0; hIndex < dataGridView1.Columns.Count; hIndex++)
					{
						clip += dataGridView1.Columns[hIndex].HeaderText.ToString() + ",";
					}
					clip += Environment.NewLine;
					
					//データを取得
					DataGridViewRowCollection rows = dataGridView1.Rows;
					for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++ )
					{
						DataGridViewRow row = rows[rowIndex];

						//選択のチェック
						if (!row.Selected) continue;

						//カラム数分ループ
						for (i = 0; i < row.Cells.Count - 1; i++)
						{

							if (row.Cells[i].Value == null)
								aa = "";
							else
								aa = row.Cells[i].Value.ToString().TrimEnd('\0');
							clip += aa + ",";
						}
						if (row.Cells[i].Value == null)
							aa = "";
						else
							aa = row.Cells[i].Value.ToString().TrimEnd('\0');

						clip += aa + Environment.NewLine;
					}

					Clipboard.SetText(clip);
				}
			}
			//2つめのタブ
			else if (index == 2)
			{
				if (listView2.SelectedIndices.Count > 0)
				{
					string clip = "";

					ListView.SelectedIndexCollection item = listView2.SelectedIndices;

					foreach (int idx in item)
					{
						clip += string.Format("{0},{1},{2},{3}" + Environment.NewLine,
									listView2.Items[idx].SubItems[0].Text.TrimEnd('\0'),
									listView2.Items[idx].SubItems[1].Text.TrimEnd('\0'),
									listView2.Items[idx].SubItems[2].Text.TrimEnd('\0'),
									listView2.Items[idx].SubItems[3].Text.TrimEnd('\0'));
					}
					Clipboard.SetText(clip);
				}
			}
		}

		//ダブルクリック
		//テーブル
		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{

			if (e.RowIndex < 0) return;

			Form_DetailList Form_Detail = new Form_DetailList();


			Form_Detail.hashlist = new Dictionary<string, string>();

			for (int j = 0; j < dataGridView1.Columns.Count; j++)
			{
				if (this.dataGridView1.Rows[e.RowIndex].Cells[j].Value != null)
					Form_Detail.hashlist[dataGridView1.Columns[j].HeaderText] = dataGridView1.Rows[e.RowIndex].Cells[j].Value.ToString();
			}			
			
			Form_Detail.Show();
			Form_Detail.Owner = this;
		}

		//ダブルクリック
		// walk
		private void listView2_DoubleClick(object sender, EventArgs e)
		{
			ListView.SelectedIndexCollection item = listView2.SelectedIndices;

			Form_detail formdetail = new Form_detail();
			formdetail.oid = listView2.Items[item[0]].SubItems[1].Text;
			formdetail.stringdata = listView2.Items[item[0]].SubItems[3].Text;
			formdetail.Show();
			formdetail.Owner = this;
		}

		//リストビューのソート(walk)
		private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (this.dt.Rows.Count <= 0)
				return;
			//DataViewクラス ソートするためのクラス
			DataView  dv = new DataView(dt);

			//一時クラス
			DataTable  dttmp = new DataTable();

			String strSort = "";

			//0なら昇順にソート
			if( sort_kind == 0 ){
				strSort = " ASC";
				sort_kind = 1;
			}
			else{
				//１の時は昇順にソート
				strSort = " DESC";
				sort_kind = 0;
			}
			
			//コピーを作成
			dttmp = dt.Clone();
			//ソートを実行
			dv.Sort = dt.Columns[e.Column].ColumnName + strSort;

			// ソートされたレコードのコピー
			foreach(DataRowView drv in dv){
				// 一時テーブルに格納
				dttmp.ImportRow(drv.Row);
			}
			//格納したテーブルデータを上書く
			dt = dttmp.Copy();
			
			//行が存在するかチェックを行う。
			if (listView2.TopItem != null){
				//現在一番上の行に表示されている行を取得
				int start = listView2.TopItem.Index;
				// ListView画面の再表示を行う
				listView2.RedrawItems(start, listView2.Items.Count - 1, true);
			}


		}
		
		//タブが選択された時
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{

			// systemが選択された時
			if (tabControl1.SelectedIndex == 0)
			{
				this.groupBox1.Enabled = false;
				this.m_OIDcombo.Enabled = false;
				this.m_OIDcombo.Text = WALKOID + ",mib-2(固定)";
			}

			//テーブルタブが選択されたとき
			else if (tabControl1.SelectedIndex == 1)
			{
				this.m_OIDcombo.Text = TABLEOID + ",ifTable(固定)";
				this.groupBox1.Enabled = false;
				this.m_OIDcombo.Enabled = false;
			}

			// mibタブが選択されたとき
			else if (tabControl1.SelectedIndex == 2)
			{
				this.groupBox1.Enabled = true;
				this.m_OIDcombo.Enabled = true;
				this.m_OIDcombo.DataSource = getlist;

				if (this.m_getRadio.Checked )
				{
					this.m_OIDcombo.Text = "";

				}
				else if (this.m_walkRadio.Checked)
				{
					this.m_OIDcombo.Text = "";
					//this.m_OIDcombo.Text = WALKOID + ",mib-2";
				}
			}
		}
		//時間のかかる処理を行うメソッド
		private void bgworker_DoWork(object sender, DoWorkEventArgs e)
		{
			worker = (BackgroundWorker)sender;
			
			// 実行を行う
			executeSnmpGet(1);
		}
		//中断ボタン
		private void suspendBtn_Click(object sender, EventArgs e)
		{
			workSuspend();
		}

		//処理中断処理
		private void  workSuspend()
		{
			//処理中断
			bgworker.CancelAsync();	
			cancelflg = true;

		}

		//リターンキーが押された時
		private void m_host_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Return)) 
			{
				//ログ書き込みの開始
				CLog.Open(Properties.Settings.Default.LogFilePath);

				//実行する
				executeSnmpGet(this.tabControl1.SelectedIndex);
			}
		}
		//テーブルのソート
		private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			// Try to sort based on the cells in the current column.
			

			// If the cells are equal, sort based on the ID column.
			if (e.Column.Name != "Column2" && 
				e.Column.Name != "Column3" && 
				e.Column.Name != "Column6" && 
				e.Column.Name != "Column7" && 
				e.Column.Name != "Column8" && 
				e.Column.Name != "Column9" && 
				e.Column.Name != "Column22" )
			{
				string a, b;
				if (e.CellValue1 == null)
					a = "0";
				else
					a = e.CellValue1.ToString();
				if (e.CellValue2 == null)
					b = "0";
				else
					b = e.CellValue2.ToString();

				e.SortResult = long.Parse(a).CompareTo(long.Parse(b));
				e.Handled = true;//pass by the default sorting
			
			}
			if(e.Column.Name == "Column22"){
				string a, b;
				if (e.CellValue1 == null)
					a = "0";
				else
					a = e.CellValue1.ToString();
				if (e.CellValue2 == null)
					b = "0";
				else
					b = e.CellValue2.ToString();

				e.SortResult = double.Parse(a).CompareTo(double.Parse(b));
				e.Handled = true;//pass by the default sorting			
			
			}
		}

		private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				//Ctrl + c
				case Keys.Control | Keys.C:
				//Enter
				case Keys.Enter:
					e.Handled = true;
					break;
			}
		}





    }
}
