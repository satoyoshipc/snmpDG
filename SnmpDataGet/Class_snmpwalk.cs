using System;
using System.Net;
using System.Collections.Generic;
using SnmpSharpNet;
using System.Collections;
using System.Text.RegularExpressions;


namespace SnmpDGet
{
	class Class_snmpwalk
	{

		private SnmpSharpNet.SnmpVersion VERSION;
		private Class_InputData INPUT;
		private string errMsg;
		private List<string> _headerList;
		private List<Dictionary<string,string>> resultHashL ;
		private Dictionary<string, List<string>> tablearray;

		//結果
		public List<Dictionary<string, string>> resultList
		{
			get { return resultHashL; }
		}


		//ヘッダ情報(テーブルの時のみ)
		public List<string> headerList
		{
			get { return _headerList; }
		}

		//テーブル表示用
		public Dictionary<string,  List<string>> TableDispArray
		{
			get { return tablearray; }
		}

		//コンストラクタ
		public Class_snmpwalk(Class_InputData input)
		{
			VERSION = input.versionofSNMPsharp;
			INPUT = input;

		}

		public string Errormsg
		{
			get { return errMsg; }
			set {errMsg = value;}
		}


		//v1ならwalk v2ならbulk
		public int listGet(System.Windows.Forms.Form form){
			int ret = 0;

			//コミュニティ名OctetString
			OctetString community = new OctetString(INPUT.community);
			AgentParameters param = new AgentParameters(community);

			//ヴァージョン
			param.Version = INPUT.versionofSNMPsharp;

			if (VERSION == SnmpSharpNet.SnmpVersion.Ver1)
			{
				//v1はwalk
				getWalk(param, (Form_snmpDataGet)form);

			}
			else if(VERSION == SnmpSharpNet.SnmpVersion.Ver2){
				//v2はbulk
				getBulk(param,(Form_snmpDataGet) form);
			}

			return ret;

		}
		//v2の時
		private int getBulk(AgentParameters param, Form_snmpDataGet form)
		{
			try { 
				int ret = 0;
				//結果格納
				resultHashL = new List<Dictionary<string, string>>();
				_headerList = new List<string>();

				tablearray = new Dictionary<string, List<string>>();
				


				int flg = 0;
				IpAddress agent = new IpAddress(INPUT.hostname);
 
				UdpTarget target = new UdpTarget((IPAddress)agent, 161, 5000, 1);	// アドレス ポート 待ち時間 リトライ
 
				Oid rootOid = new Oid(INPUT.oid); 
 
				Oid lastOid = (Oid)rootOid.Clone();
 
				// Pdu class used for all requests
				Pdu pdu = new Pdu(PduType.GetBulk);
 
				
				pdu.NonRepeaters = 0;

				pdu.MaxRepetitions = 50;
 
				// Loop through results
				while (lastOid != null)
				{
					
					System.Windows.Forms.Application.DoEvents();

					if (form.cancelflg)
					{
						//中断された時
						form.cancelflg = false;
						break;
					}

					if (pdu.RequestId != 0)
					{
						pdu.RequestId += 1;
					}

					// Clear Oids from the Pdu class.
					pdu.VbList.Clear();

					// Initialize request PDU with the last retrieved Oid
					pdu.VbList.Add(lastOid);

					// Make SNMP request
					System.Threading.Thread.Sleep(15);
					SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);

					//Console.WriteLine("{0}  {1}  ", result.Pdu.ErrorStatus,result.Pdu.ErrorIndex);

					// You should catch exceptions in the Request if using in real application.
					// If result is null then agent didn't reply or we couldn't parse the reply.
					if (result != null)
					{

						// ErrorStatus other then 0 is an error returned by 
						// the Agent - see SnmpConstants for error definitions
						if (result.Pdu.ErrorStatus != 0)
						{
							// agent reported an error with the request
							Errormsg = string.Format("Error in SNMP reply. Error {0} index {1}",
								result.Pdu.ErrorStatus,
								result.Pdu.ErrorIndex);
							lastOid = null;
							ret = -1;
							break;
						}
						else
						{

							int i = 0;
							// Walk through returned variable bindings
							foreach (Vb v in result.Pdu.VbList)
							{

								// Check that retrieved Oid is "child" of the root OID
								if (rootOid.IsRootOf(v.Oid))
								{
									//tableget
									//値の取得
									Dictionary<string, string> hashtbl = new Dictionary<string, string>();

									if (INPUT.method == 3) {
										//テーブル用データの取得
										getForTable(v, ref flg);
									}
									else {

										hashtbl["oid"] = v.Oid.ToString();
										hashtbl["type"] = SnmpConstants.GetTypeName(v.Value.Type);

										//ifdescの時日本語の可能性あり
										//1.3.6.1.2.1.2.2.1.2 ifdesc
										//1.3.6.1.2.1.25.3.2.1.3. hrDeviceDescr
										if (0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.2.") | 0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.25.3.2.1.3."))
										{
											hashtbl["value"] = convertJP(v.Value.ToString());
										}
										else{
											if (0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.3."))
											{
												hashtbl["value"] = Util.ifTypeConv(v.Value.ToString());
											}
											else if (0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.7.") | 0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.8."))
											{
												hashtbl["value"] = Util.convIfStatus(v.Value.ToString());
											}
											else
											{
												string value = v.Value.ToString();

												//TimeTick型の時はミリ秒も出力する
												if (hashtbl["type"] == SnmpConstants.SMI_TIMETICKS_STR)
												{
													TimeTicks timeti = (TimeTicks)v.Value;
													value = "(" + timeti.Milliseconds.ToString() + "ms)" + v.Value.ToString();
												}
												hashtbl["value"] = value;
											}
										}

										//リストに格納
										resultHashL.Add(hashtbl);

									}
									if (v.Value.Type == SnmpConstants.SMI_ENDOFMIBVIEW)
									{
										lastOid = null;
									}
									else {
										lastOid = v.Oid;
									}
								}
								else
								{
									// we have reached the end of the requested
									// MIB tree. Set lastOid to null and exit loop
									lastOid = null;
								}
								i++;
							}
						}
					}
					else
					{
						Console.WriteLine("No response received from SNMP agent.");
					}
				}
				target.Close();
				return ret;
			}
			catch{
				throw;
			}


		}

		private int getWalk(AgentParameters param, Form_snmpDataGet form)
		{
			try {

				int ret = 0;
				int flg = 0;
				//結果格納
				resultHashL = new List<Dictionary<string,string>>();

				_headerList = new List<string>();

				tablearray = new Dictionary<string, List<string>>();
            
				//ホスト名
				IpAddress agent = new IpAddress(INPUT.hostname);
 
				// 
				UdpTarget target = new UdpTarget((IPAddress)agent, 161, 5000, 1);


				Oid rootOid = new Oid(INPUT.oid); // ifDescr
 
				//  the SNMP agent
				Oid lastOid = (Oid)rootOid.Clone();
 
				// pduインスタンスを生成
				Pdu pdu = new Pdu(PduType.GetNext);
 
				// Loop through results
				while (lastOid != null)
				{

					System.Windows.Forms.Application.DoEvents();
					if (form.cancelflg)
					{
						//中断された時
						form.cancelflg = false;
						break;
					}

					// When Pdu class is first constructed, RequestId is set to a random value
					// that needs to be incremented on subsequent requests made using the
					// same instance of the Pdu class.
					if (pdu.RequestId != 0)
					{
						pdu.RequestId += 1;
					}
					// Clear Oids from the Pdu class.
					pdu.VbList.Clear();

					// Initialize request PDU with the last retrieved Oid
					pdu.VbList.Add(lastOid);

					// Make SNMP request
					System.Threading.Thread.Sleep(20);
					SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);

					// You should catch exceptions in the Request if using in real application.
					// If result is null then agent didn't reply or we couldn't parse the reply.
					if (result != null)
					{
						// ErrorStatus other then 0 is an error returned by 
						// the Agent - see SnmpConstants for error definitions
						if (result.Pdu.ErrorStatus != 0)
						{
							// agent reported an error with the request
							Errormsg = string.Format("Error in SNMP reply. Error {0} index {1}", 
								result.Pdu.ErrorStatus,
								result.Pdu.ErrorIndex);

							lastOid = null;
							ret = -1;
							break;
						}
						else
						{
							// Walk through returned variable bindings
							foreach (Vb v in result.Pdu.VbList)
							{
								// Check that retrieved Oid is "child" of the root OID
								if (rootOid.IsRootOf(v.Oid))
								{
									//ifIndexを取得しヘッダ用のリストを生成
									if (INPUT.method == 3)
									{
										//テーブル用データの取得
										getForTable(v, ref flg);
									}
									else
									{
										//値の取得
										Dictionary<string, string> hashtbl = new Dictionary<string, string>();
										hashtbl["oid"] = v.Oid.ToString();
										hashtbl["type"] = SnmpConstants.GetTypeName(v.Value.Type);


										//日本語の可能性あり
										//ifdescの時日本語の可能性あり
										//1.3.6.1.2.1.2.2.1.2 ifdesc
										//1.3.6.1.2.1.25.3.2.1.3. hrDeviceDescr
										if (0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.2.") | 0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.25.3.2.1.3.")  )
										{
											hashtbl["value"] = convertJP(v.Value.ToString());
										}
										else
										{
											if (0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.3."))
											{
												hashtbl["value"] = Util.ifTypeConv(v.Value.ToString());
											}
											else if (0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.7.") | 0 <= v.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.8."))
											{
												hashtbl["value"] = Util.convIfStatus(v.Value.ToString());
											}
											else
											{
												string value = v.Value.ToString();

												//TimeTick型の時はミリ秒も出力する
												if (hashtbl["type"] == SnmpConstants.SMI_TIMETICKS_STR)
												{
													TimeTicks timeti = (TimeTicks)v.Value;
													value = "(" + timeti.Milliseconds.ToString() + "ms)" + v.Value.ToString();

												}
												hashtbl["value"] = value;
											}

										}


										//リストに格納
										resultHashL.Add(hashtbl);
									}																
									lastOid = v.Oid;
								}
								else
								{
									// we have reached the end of the requested
									// MIB tree. Set lastOid to null and exit loop
									lastOid = null;
								}
							}
						}
					}
					else
					{
						// agent reported an error with the request
						Errormsg = "No response received from SNMP agent.";
					}
				}
				target.Close();
				return ret;

			}catch{
				throw;
			}
		}

		//テーブルデータの取得
		private void getForTable(Vb data, ref int flg)
		{
			int index = 0;
			List<string> recordList = new List<string>();
			
			//mib-2
			if (data.Oid.ToString().IndexOf("1.3.6.1.2.1.") >= 0)
			{
				//ifindexか判定
				if (data.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.1.") >= 0)
				{
					string str = "";
					
					str = data.Oid.ToString();
					index = data.Oid.ToString().LastIndexOf(".");

					//ヘッダなので1回目のみ実行
					if (flg == 0)
					{
						//ヘッダのリストに取得
						string ss = "";
						ss = Class_headerConv.headerConvert(str.Substring(0, index + 1));
						_headerList.Add(ss);
						flg = 1;
					}

					//ifindexを多次元リストに格納
					recordList = new List<string>(new string[22]);
					//recordList = new List<string>();
					List<string> tmpAry = recordList.CloneDeep();
					tmpAry[0] = str.Substring(index + 1);

					tablearray.Add(str.Substring(index + 1), tmpAry);
				}
				//ifEntry
				else if (data.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.") >= 0)
				{
					string str = "";
					str = data.Oid.ToString();
					index = data.Oid.ToString().LastIndexOf(".");

					if (str.EndsWith("." + "1"))
					{
						//ヘッダのリストに取得
						string ss = "";
						ss = Class_headerConv.headerConvert(str.Substring(0, index + 1));
						_headerList.Add(ss);

					}

					//値をリストに格納
					string valdata = (data.Value.ToString() == null) ? "" : data.Value.ToString();
					
					//日本語を変換
					if (0 <= data.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.2.") | 0 <= data.Oid.ToString().IndexOf("1.3.6.1.2.1.25.3.2.1.3.") )
					{
						valdata = convertJP(valdata);
					}
					else if (0 <= data.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.3."))
					{
						valdata = Util.ifTypeConv(valdata);
					}
					else if (0 <= data.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.7.") | 0 <= data.Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.8."))
					{
						valdata = Util.convIfStatus(valdata);

					}
					//テーブルの指定された項目にデータを挿入
					//インデックス(横)の取得
					int idx = Util.getTableYokoIndex(data.Oid.ToString());
					((List<string>)tablearray[str.Substring(index + 1)])[idx] = valdata;
				}
			}
		}

		//日本語に変換
		public static string convertJP(string data)
		{
				string retstr = "";
				List<byte> dd = new List<byte>();
				string[] sl = data.Split(' ');

				foreach (string ss in sl)
				{
					if (Regex.IsMatch(ss, @"^[0-9a-fA-F]{2}$"))
						dd.Add(Convert.ToByte(ss, 16));
				}

				string st = Util.ToHexString(dd);
				if (st != "")
				{
					retstr = st;
				}
				else
				{
					retstr=data;

				}
				return retstr;
		}

	}
}
