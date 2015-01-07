using System;
using System.Net;
using SnmpSharpNet;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SnmpDGet
{
    class Class_snmpGet
    {

		private Dictionary<string, string> SYSTEMLIST = new Dictionary<string, string> {
			{"sysDescr",".1.3.6.1.2.1.1.1.0"},
			{"sysObjectID",".1.3.6.1.2.1.1.2.0"},
			{"sysUpTime",".1.3.6.1.2.1.1.3.0"},
			{"sysContact",".1.3.6.1.2.1.1.4.0"},
			{"sysName",".1.3.6.1.2.1.1.5.0"},
			{"sysLocation",".1.3.6.1.2.1.1.6.0"},
			{"sysServices",".1.3.6.1.2.1.1.7.0"}
		};
		private Dictionary<string, string> _systemhash = new Dictionary<string, string>();
		
		public Dictionary<string, string> systemhash
		{
			get { return _systemhash; }

		}		
		
		//結果
        private Dictionary<string,string> resultList;

		public Dictionary<string, string> resulthash
		{
			get { return resultList; }

		}


		//システム情報の取得
		public void getSystemInfo(Class_InputData input,Class_TextLog log)
		{

			try
			{
				// コミュニティ名
				OctetString comm = new OctetString(input.community);
				// パラメータクラス
				AgentParameters param = new AgentParameters(comm);
				// バージョン取得
				param.Version = input.versionofSNMPsharp;
				IpAddress agent = new IpAddress(input.hostname);

				//System情報の検索
				foreach( KeyValuePair<string, string> systemhs in SYSTEMLIST)
				{

					// Construct target
					UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

					// Pdu class used for all requests
					Pdu pdu = new Pdu(PduType.Get);

					pdu.VbList.Add(systemhs.Value);

					if (input.version == "v1" | input.version == "1")
					{

						SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);

						// If result is null then agent didn't reply or we couldn't parse the reply.
						if (result != null)
						{
							// ErrorStatus other then 0 is an error returned by 
							// the Agent - see SnmpConstants for error definitions

							if (result.Pdu.ErrorStatus != 0)
							{
								// agent reported an error with the request
								log.Write("Error in SNMP reply. Error " + result.Pdu.ErrorStatus + " index " + result.Pdu.ErrorIndex);
							}
							else
							{

								string value = result.Pdu.VbList[0].Value.ToString();

								//TimeTick型の時はミリ秒も出力する
								if (result.Pdu.VbList[0].Value.Type == SnmpConstants.SMI_TIMETICKS)
								{
									TimeTicks timeti = (TimeTicks)result.Pdu.VbList[0].Value;
									value = "(" + timeti.Milliseconds.ToString() + "ms)" + result.Pdu.VbList[0].Value.ToString();

								}

								systemhash[systemhs.Key] = value;
								log.Write("GET項目：" + systemhs.Key + " "+ systemhs.Value  + " 値：" + value);

							}
						}
						else
						{
							//Console.WriteLine("SNMP agentからのレスポンスがありません.");
							log.Write("SNMP agentからのレスポンスがありません.");
						}

					}
					// v2以降の時
					else
					{


						SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);

						// If result is null then agent didn't reply or we couldn't parse the reply.
						if (result != null)
						{
							// ErrorStatus other then 0 is an error returned by 
							// the Agent - see SnmpConstants for error definitions

							if (result.Pdu.ErrorStatus != 0)
							{

								// agent reported an error with the request
								log.Write("Error in SNMP reply. Error " + result.Pdu.ErrorStatus.ToString() + " index " + result.Pdu.ErrorIndex.ToString());
								
								//Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
								//result.Pdu.ErrorStatus,
								//result.Pdu.ErrorIndex);
							}
							else
							{


								string value = result.Pdu.VbList[0].Value.ToString();

								//TimeTick型の時はミリ秒も出力する
								if (result.Pdu.VbList[0].Value.Type == SnmpConstants.SMI_TIMETICKS)
								{
									TimeTicks timeti = (TimeTicks)result.Pdu.VbList[0].Value;
									value = "(" + timeti.Milliseconds.ToString() + "ms)" + result.Pdu.VbList[0].Value.ToString();

								}
								//取得した値を格納
								_systemhash[systemhs.Key] = value;
								log.Write("GET項目：" + systemhs.Key + " " + systemhs.Value + " 値：" + value);


							}
						}
						else
						{
							log.Write("SNMP agentからのレスポンスがありません.");
							//Console.WriteLine("SNMP agentからのレスポンスがありません.");
						}
					}

					target.Close();
				}
				
			}

			catch (Exception)
			{
				throw;
			}



		}

		//GETを取得する
		public void getRequest(Class_InputData input)
		{
        
			try{

                // コミュニティ名
				OctetString comm = new OctetString(input.community);


                // パラメータクラス
                AgentParameters param = new AgentParameters(comm);
                // バージョン取得
				param.Version = input.versionofSNMPsharp;
				IpAddress agent = new IpAddress(input.hostname);


                // Construct target

                UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);


                // Pdu class used for all requests

                Pdu pdu = new Pdu(PduType.Get);

				
				pdu.VbList.Add(input.oid);
				//バージョン1の時
				if (input.version == "v1" | input.version == "1")
				{

					SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);

					// If result is null then agent didn't reply or we couldn't parse the reply.
					if (result != null)
					{
						// ErrorStatus other then 0 is an error returned by 
						// the Agent - see SnmpConstants for error definitions

						if (result.Pdu.ErrorStatus != 0)
						{

							// agent reported an error with the request
							Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
							result.Pdu.ErrorStatus,
							result.Pdu.ErrorIndex);
						}
						else
						{
							resultList = new Dictionary<string,string>();
							resultList["oid"] = result.Pdu.VbList[0].Oid.ToString();
							resultList["type"] = SnmpConstants.GetTypeName(result.Pdu.VbList[0].Value.Type);
							
							//日本語の可能性あり
							//ifdescの時日本語の可能性あり
							//1.3.6.1.2.1.2.2.1.2 ifdesc
							//1.3.6.1.2.1.25.3.2.1.3. hrDeviceDescr
							if (0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.2") | 0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.25.3.2.1.3"))
							{
							
								resultList["value"] = convertJP(result.Pdu.VbList[0].Value.ToString());
							}
							else if (0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.7.") | 0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.8."))
							{
								
								resultList["value"] = Util.convIfStatus(result.Pdu.VbList[0].Oid.ToString());
							}
							else
							{

								string value = result.Pdu.VbList[0].Value.ToString();

								//TimeTick型の時はミリ秒も出力する
								if (resultList["type"] == SnmpConstants.SMI_TIMETICKS_STR)
								{
									TimeTicks timeti = (TimeTicks)result.Pdu.VbList[0].Value;
									value = "(" + timeti.Milliseconds.ToString() + "ms)" + result.Pdu.VbList[0].Value.ToString();

								}

								resultList["value"] = value;

							}
						}
					}
					else
					{
						Console.WriteLine("SNMP agentからのレスポンスがありません.");
					}

	
				}
					// v2以降の時
				else{


					SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);

					// If result is null then agent didn't reply or we couldn't parse the reply.
					if (result != null)
					{
						// ErrorStatus other then 0 is an error returned by 
						// the Agent - see SnmpConstants for error definitions

						if (result.Pdu.ErrorStatus != 0)
						{

							// agent reported an error with the request
							Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
							result.Pdu.ErrorStatus,
							result.Pdu.ErrorIndex);
						}
						else
						{
							resultList = new Dictionary<string,string>();
							resultList["oid"] = result.Pdu.VbList[0].Oid.ToString();
							resultList["type"] = SnmpConstants.GetTypeName(result.Pdu.VbList[0].Value.Type);

							//日本語の可能性あり
							//ifdescの時日本語の可能性あり
							if (0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.2") | 0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.25.3.2.1.3"))
							{
								resultList["value"] = convertJP(result.Pdu.VbList[0].Value.ToString());
							}
							else if (0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.7.") | 0 <= result.Pdu.VbList[0].Oid.ToString().IndexOf("1.3.6.1.2.1.2.2.1.8."))
							{
								resultList["value"] = Util.convIfStatus(result.Pdu.VbList[0].Oid.ToString());
							}
							else
							{
								string value = result.Pdu.VbList[0].Value.ToString();

								//TimeTick型の時はミリ秒も出力する
								if (resultList["type"] == SnmpConstants.SMI_TIMETICKS_STR) 
								{ 
									TimeTicks timeti = (TimeTicks)result.Pdu.VbList[0].Value;
									value = "(" + timeti.Milliseconds.ToString() + "ms)" + result.Pdu.VbList[0].Value.ToString();

								}
								resultList["value"] = value;
							}
						}
					}
					else
					{
						Console.WriteLine("SNMP agentからのレスポンスがありません.");
					}



				}
                target.Close();
            }

            catch (Exception)
            {
                throw;
            }
        }
		//日本語に変換
		private string convertJP(string data)
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
				retstr = data;

			}
			return retstr;
		}
    }

}
