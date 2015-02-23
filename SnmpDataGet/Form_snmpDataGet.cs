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
        
        //テーブル表示のデータテーブル
		DataTable dt ;

        //一括表示用のデータテーブル
        DataTable dt_list;

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

        void manyList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {

            //	e.Item = _item[e.ItemIndex];
            DataRow row = dt_list.Rows[e.ItemIndex];
            e.Item = new ListViewItem(
                new String[]
				{ 
					Convert.ToString(row[0]), 
					Convert.ToString(row[1]), 
					Convert.ToString(row[2]), 
					Convert.ToString(row[3]),
					Convert.ToString(row[4]),
   					Convert.ToString(row[5])
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



                this.manyList.VirtualMode = true;
                // １行全体選択
                this.manyList.FullRowSelect = true;
                this.manyList.HideSelection = false;
                this.manyList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                //Hook up handlers for VirtualMode events.
                this.manyList.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(manyList_RetrieveVirtualItem);
                this.manyList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


                // Column追加
                this.manyList.Columns.Insert(0, "No", 30, HorizontalAlignment.Left);
                this.manyList.Columns.Insert(1, "IPアドレス", 180, HorizontalAlignment.Left);
                this.manyList.Columns.Insert(2, "バージョン", 30, HorizontalAlignment.Left);
                this.manyList.Columns.Insert(3, "コミュニティ", 30, HorizontalAlignment.Left);
                this.manyList.Columns.Insert(4, "ホスト名", 80, HorizontalAlignment.Left);
                this.manyList.Columns.Insert(5, "機器の説明(sysDescr)", 180, HorizontalAlignment.Left);


				// 指定されたドメインでのみ起動を行う
				System.Net.IPHostEntry host;

				host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

				// actwatch.com以外はエラー
				if (!(host.HostName.EndsWith("actwatch.com")))
				{
					if (System.Environment.GetCommandLineArgs().Length > 1)
					{
						//パラメータを取得
						string[] args = System.Environment.GetCommandLineArgs();
						//noがあった場合は普通に起動する
						if (args[1].ToLower().Trim() != "no")
						{
							MessageBox.Show("実行許可を取得できませんでした。");
							CLog.Write("実行許可を取得できませんでした。");

							//アプリケーションを終了する
							Application.Exit();
						}
					}
					else
					{
						MessageBox.Show("実行許可を取得できませんでした。");
						CLog.Write("実行許可を取得できませんでした。");

						//アプリケーションを終了する
						Application.Exit();
					}
				}


				//バックグランドワーカーのイベントハンドらの定義
				//RunWorkerAsyncが呼び出された時
				bgworker.DoWork += new DoWorkEventHandler(bgworker_DoWork);
				bgworker.WorkerSupportsCancellation = true;

				suspendBtn.Enabled = false;
                this.m_readBtn.Enabled = false;

				this.tabControl1.TabPages[0].Text = "System";
				this.tabControl1.TabPages[1].Text = "Interfaces";
				this.tabControl1.TabPages[2].Text = "一覧";
                this.tabControl1.TabPages[3].Text = "一括実行";

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

                this.m_selectBtn.Enabled = true;


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
            try
            {

                for (i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        //ホスト名IPアドレス
                        case "-f":
                        case "-F":
                            i += 1;
                            this.tabControl1.SelectedIndex = 3;
                            this.m_listFile.Text = args[i].Trim('"', '\'');
                            int ret = 0;
                            try
                            {
                                ret = fileExecute();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("一括実行　ファイル読込時" + ex.Message);
                                ret = -1;
                            }

                            return ret;
                        //ホスト名IPアドレス
                        case "-ip":
                        case "-IP":
                            i += 1;
                            if (i < args.Length)
                            {
                                param.ipaddress = args[i].Trim('"', '\'');
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
                                param.type = args[i].Trim('"', '\'').ToLower();
                                flgDict["type"] = true;
                            }
                            continue;
                        //バージョン
                        case "-v":
                        case "-V":
                            i += 1;

                            if (i < args.Length)
                            {
                                param.version = args[i].Trim('"', '\'').ToLower();

                                flgDict["version"] = true;
                            }
                            continue;
                        //コミュニティ 
                        case "-c":
                        case "-C":
                            i += 1;

                            if (i < args.Length)
                            {
                                param.community = args[i].Trim('"', '\'').ToLower();
                                flgDict["community"] = true;
                            }
                            continue;
                        //OID
                        case "-o":
                        case "-O":

                            i += 1;

                            if (i < args.Length)
                            {
                                param.oid = args[i].Trim('"', '\'');
                                flgDict["oid"] = true;
                            }
                            continue;
                    }

                }
            }
            catch(Exception ex){
                MessageBox.Show("パラメータが取得できませんでした。" +  ex.Message);
                return -1;
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
            if (this.tabControl1.SelectedIndex != 3)
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
                if (this.m_OIDcombo.Text == "" & (this.m_getRadio.Checked | this.m_walkRadio.Checked))
                {
                    MessageBox.Show("OIDが未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_OIDcombo.Focus();
                    CLog.Close();
                    return;
                }
            }
            else
            {
                //一括実行が選択されているとき
                if (this.m_listFile.Text == "")
                {
                    MessageBox.Show("ファイル名が未入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_commu.Focus();
                    CLog.Close();
                    return;
                }
                //ファイルの読み込みを実行してください。
                if (manyList.Items.Count == 0)
                {
                    MessageBox.Show("ファイルの読込を実行してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_commu.Focus();
                    CLog.Close();
                    return;
                }
                //一括実行のときのみ確認
                if (MessageBox.Show("一括実行を実施します。よろしいですか？" + Environment.NewLine +
                    "ファイル名：" + m_listFile.Text, "snmpdget", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    CLog.Close();
                    return;
                }
            }
			groupBox1.Enabled = false;
			m_host.Enabled = false;
			m_commu.Enabled = false;
			m_versioncombo.Enabled = false;
			m_OIDcombo.Enabled = false;

            m_OIDcombo.Enabled = false;
            m_OIDcombo.Enabled = false;
            m_listFile.Enabled = false;
            m_selectBtn.Enabled = false;

            tabControl1.Enabled = false;

			suspendBtn.Enabled = true;
			m_end.Enabled = false;
			m_OK.Enabled = false;

			//情報取得
			toolStripStatusLabel1.Text = "情報取得中....";
			System.Windows.Forms.Application.DoEvents();

            Class_InputData input = new Class_InputData();
            Cursor preCursor = Cursor.Current;

            //一括実行選択時
            if (this.tabControl1.SelectedIndex == 3)
            {

                //元のカーソルを保持
                this.m_readBtn.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                Stopwatch sw1 = new Stopwatch();
                sw1.Start();
                Class_snmpGet snmpget;

                try
                {
                    //リストから読み込む
                    for (int i = 0; i < manyList.Items.Count; i++)
                    {
                        //一覧表示
                        input.hostname = manyList.Items[i].SubItems[1].Text;
                        input.version = manyList.Items[i].SubItems[2].Text;
                        input.community = manyList.Items[i].SubItems[3].Text;
                        snmpget = new Class_snmpGet();
                        try
                        {
                            snmpget.getSystemInfo(input, CLog);
                            //データの挿入
                            if (snmpget.systemhash == null)
                            {
                                MessageBox.Show("値を取得できませんでした。", "SnmpDGet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CLog.Write("一括 ERROR 値を取得できませんでした。ホスト名：" + input.hostname);
                                CLog.Close();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            CLog.Write(ex.Message + " ホスト名：" + input.hostname);
                            //MessageBox.Show(ex.Message);                          
                        }


                        dt_list.Rows[i]["No"] = i + 1;
                        dt_list.Rows[i]["IPアドレス"] = input.hostname;
                        dt_list.Rows[i]["バージョン"] = input.version;
                        dt_list.Rows[i]["コミュニティ"] = input.community;
                        if (snmpget.systemhash.Count <= 0)
                        {
                            dt_list.Rows[i]["ホスト名"] = "■エラー発生! 値を取得できませんでした。";
                        }
                        else {
                            dt_list.Rows[i]["ホスト名"] = snmpget.systemhash["sysName"];
                            dt_list.Rows[i]["機器の説明(sysDescr)"] = snmpget.systemhash["sysDescr"];
                        }


                    }
                    this.manyList.VirtualListSize = dt_list.Rows.Count;
                    this.manyList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    //カラムの幅を合わせる
                    foreach (ColumnHeader ch in this.manyList.Columns)
                    {
                        ch.Width = -1;
                    }
                    return;
                }
                catch (Exception ex)
                {
                    CLog.Write(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {


                    tabControl1.Enabled = true;

                    this.m_listFile.Enabled = true;
                    this.m_selectBtn.Enabled = true;
                    this.m_readBtn.Enabled = true;

                    m_host.Enabled = true;
                    m_commu.Enabled = true;
                    m_versioncombo.Enabled = true;


                    suspendBtn.Enabled = false;
                    m_end.Enabled = true;
                    m_OK.Enabled = true;

                    toolStripStatusLabel1.Text = "";
                    Cursor.Current = preCursor;
                    sw1.Stop();
                    toolStripStatusLabel1.Text = sw1.Elapsed.ToString();

                    if (CLog != null) CLog.Close();


                }
            }
            else
            {
                if (this.m_getRadio.Checked)
                {
                    input.method = 1;

                }
                else if (this.m_walkRadio.Checked)
                {
                    input.method = 2;

                }

                //元のカーソルを保持

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
                m_sysObjectID.Text = "";
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
                    else if (index == 1)
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
                finally
                {


                    tabControl1.Enabled = true;
                    m_host.Enabled = true;
                    m_commu.Enabled = true;
                    m_versioncombo.Enabled = true;

                    this.m_listFile.Enabled = true;
                    this.m_selectBtn.Enabled = true;
                    this.m_readBtn.Enabled = true;

                    if (this.tabControl1.SelectedIndex == 2)
                    {
                        m_OIDcombo.Enabled = true;
                        groupBox1.Enabled = true;
                    }

                    suspendBtn.Enabled = false;
                    m_end.Enabled = true;
                    m_OK.Enabled = true;

                    toolStripStatusLabel1.Text = "";
                    Cursor.Current = preCursor;
                    sw.Stop();
                    toolStripStatusLabel1.Text = sw.Elapsed.ToString();

                    if (CLog != null) CLog.Close();
                }
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
			string line = "";
			getlist = new ArrayList();

			using (StreamReader sr = new StreamReader(
					"getOIDList.txt", Encoding.GetEncoding("Shift_JIS")))
			{
				while ((line = sr.ReadLine()) != null)
				{
					//#はコメント
					if(line.StartsWith("#")){
						continue;
					}
					getlist.Add(line.Trim());
				}
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
            //3つめのタブ
            else if (index == 3)
            {
                if (this.manyList.SelectedIndices.Count > 0)
                {
                    string clip = "";

                    ListView.SelectedIndexCollection item = this.manyList.SelectedIndices;

                    foreach (int idx in item)
                    {
                        clip += string.Format("{0},{1},{2},{3},{4},{5}" + Environment.NewLine,
                                    manyList.Items[idx].SubItems[0].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[1].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[2].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[3].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[4].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[5].Text.TrimEnd('\0'));
                    }
                    Clipboard.SetText(clip);
                }
            }
		}


        //一括実行のキーダウン
        private void manyList_KeyDown(object sender, KeyEventArgs e)
        {
            //Ctrl + c
            if (e.KeyData == (Keys.Control | Keys.C))
            {

                if (manyList.SelectedIndices.Count > 0)
                {
                    string clip = "";

                    ListView.SelectedIndexCollection item = manyList.SelectedIndices;

                    foreach (int idx in item)
                    {
                        clip += string.Format("{0},{1},{2},{3},{4},{5}" + Environment.NewLine,
                                    manyList.Items[idx].SubItems[0].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[1].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[2].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[3].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[4].Text.TrimEnd('\0'),
                                    manyList.Items[idx].SubItems[5].Text.TrimEnd('\0'));
                    }
                    Clipboard.SetText(clip);
                }
            }
            //全件選択
            else if (e.KeyData == (Keys.Control | Keys.A))
            {
                manyList.BeginUpdate();

                manyList.SelectedIndices.Clear();
                for (int i = 0; i < this.dt_list.Rows.Count; ++i)
                    manyList.SelectedIndices.Add(i);

                manyList.EndUpdate();

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

        //一括変更ダブルクリック
        private void manyList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection item = this.manyList.SelectedIndices;

            Form_detail formdetail = new Form_detail();
            formdetail.oid = this.manyList.Items[item[0]].SubItems[1].Text;
            formdetail.stringdata = this.manyList.Items[item[0]].SubItems[5].Text;
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
            m_host.Focus();
            
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

        //参照ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            //ファイル選択ダイアログの表示
            string str = "";
            str = Disp_FileSelectDlg();
            if (str != "")
            {
                m_listFile.Text = str;

            }
        }

        //一括実行読み込み
        private int fileExecute()
        {

            String retStr = string.Empty;
            string filepath = string.Empty;
            System.IO.StreamReader cReader;

            filepath = m_listFile.Text;
            try
            { 
                //ファイルの存在チェック
                if (!File.Exists(filepath))
                {
                    // filePathのファイルは存在しない
                    MessageBox.Show("指定されたファイルが見つかりませんでした。", "SnmpDG", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }


                // StreamReader の新しいインスタンスを生成する
                cReader = (
                    new System.IO.StreamReader(filepath, System.Text.Encoding.Default)
                );
                string fileContext = cReader.ReadToEnd();

                cReader.Close();

                System.IO.StringReader rs = new System.IO.StringReader(fileContext);
                // 読み込んだ結果をすべて格納するための変数を宣言する
                string[] stResult;
                string csvdata = string.Empty;
                int count = 0;
                
                //リストビューを初期化する
                dt_list = new DataTable("table2");
                dt_list.Columns.Add("No", Type.GetType("System.Int32"));
                dt_list.Columns.Add("IPアドレス", Type.GetType("System.String"));
                dt_list.Columns.Add("バージョン", Type.GetType("System.String"));
                dt_list.Columns.Add("コミュニティ", Type.GetType("System.String"));
                dt_list.Columns.Add("ホスト名", Type.GetType("System.String"));
                dt_list.Columns.Add("機器の説明(sysDescr)", Type.GetType("System.String"));

            
                // 読み込みできる文字がなくなるまで繰り返す
                while (rs.Peek() >= 0)
                {
                    count++;
                    // ファイルを 1 行ずつ読み込む
                    csvdata = rs.ReadLine();

                    //配列に格納
                    stResult = csvdata.Split(',');

                    //IPアドレス
                    string ip = stResult[0].Replace("\"", "");
                    ip = ip.Replace("'", "");

                    //バージョン情報
                    string versioninfo = string.Empty;
                    if (stResult.Length > 2)
                    {
                        //2があったらv2c
                        if (stResult[1].IndexOf("2") >= 0 || stResult[1].IndexOf("２") >= 0)
                        {
                            versioninfo = "v2c";
                        }
                        //1だったらv1
                        else if (stResult[1].IndexOf("1") >= 0 || stResult[1].IndexOf("１") >= 0)
                        {
                            versioninfo = "v1";
                        }
                        else
                            versioninfo = "取得できませんでした。";

                        //コミュニティ名
                        string community = stResult[2].ToLower();
                        community = community.Replace("\"", "");
                        community = community.Replace("'", "");

                        //データの挿入
                        DataRow row = dt_list.NewRow();
                        row["No"] = count;
                        row["IPアドレス"] = ip;
                        row["バージョン"] = versioninfo;
                        row["コミュニティ"] = community;
                        dt_list.Rows.Add(row);
                    }
                }

                this.manyList.VirtualListSize = dt_list.Rows.Count;

                //カラムの幅を合わせる
                foreach (ColumnHeader ch in this.manyList.Columns)
                {
                   ch.Width = -1;
                }

                // rs を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                rs.Close();
                return 1;
            }
            catch (Exception)
            {

                throw;

            }

        }

        //ファイル選択ダイアログを表示
        private string Disp_FileSelectDlg()
        {

            string retStr = "";

            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = "";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                retStr = ofd.FileName;
            }

            return retStr;
        }

        //読込みボタン
        private void button1_Click_1(object sender, EventArgs e)
        {

            try { 
                fileExecute();
            }
            catch (Exception ex)
            {

                MessageBox.Show("一括実行　ファイル読込時" + ex.Message);

                return;
            }
            this.tabControl1.SelectedIndex = 3;
        }

        private void m_listFile_TextChanged(object sender, EventArgs e)
        {
            if (m_listFile.Text == "")
                this.m_readBtn.Enabled = false;
            else
                this.m_readBtn.Enabled = true;
            
        }




    }
}
