using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


namespace SnmpDGet
{
	


	[Serializable]
	public static class Util
	{
		/// <summary>
		/// ディープコピーを作成する。
		/// クローンするクラスには SerializableAttribute 属性、
		/// 不要なフィールドは NonSerializedAttribute 属性をつける。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static T CloneDeep<T>(this T target)
		{
			object clone = null;
			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, target);
				stream.Position = 0;
				clone = formatter.Deserialize(stream);
			}
			return (T)clone;
		}

		/// <summary>
		/// バイト配列から16進数の文字列を生成します。
		/// </summary>
		/// <param name="bytes">バイト配列</param>
		/// <returns>16進数文字列</returns>
		public static string ToHexString(List<byte> bytes)
		{
			StringBuilder sb = new StringBuilder(bytes.Count * 2);
			foreach (byte b in bytes)
			{
				if (b < 16) sb.Append('0'); // 二桁になるよう0を追加
				
				sb.Append(Convert.ToString(b, 16));
			}
			string dectxt = Encoding.GetEncoding("Shift_JIS").GetString(bytes.ToArray());

			System.Diagnostics.Debug.WriteLine(dectxt);
			return dectxt;
		}

		//テーブル表示時のセルのインデックスを返す。
		public static int getTableYokoIndex(string oidstr ) 
		{
			int selindex = 0;
			if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.1."))
			{
				selindex = 1 -1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.2."))
			{
				selindex = 2 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.3."))
			{
				selindex = 3 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.4."))
			{
				selindex = 4 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.5."))
			{
				selindex = 5 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.6."))
			{
				selindex = 6 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.7."))
			{
				selindex = 7 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.8."))
			{
				selindex = 8 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.9."))
			{
				selindex = 9 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.10."))
			{
				selindex = 10 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.11."))
			{
				selindex = 11 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.12."))
			{
				selindex = 11 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.13."))
			{
				selindex = 13 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.14."))
			{
				selindex = 14 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.15."))
			{
				selindex = 15 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.16."))
			{
				selindex = 16 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.17."))
			{
				selindex = 17 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.18."))
			{
				selindex = 18 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.19."))
			{
				selindex = 19 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.20."))
			{
				selindex = 20 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.21."))
			{
				selindex = 20 - 1;
			}
			else if (0 <= oidstr.IndexOf("1.3.6.1.2.1.2.2.1.22."))
			{
				selindex = 22 - 1;
			}
			else
			{
				selindex = 255;
			}

			return selindex;
		}

		//ifAdminStatusとifOperStatusのステータスを返す
		public static string convIfStatus(string valdata)
		{
			string retvalue = "";

			//up
			if (valdata == "1")
				retvalue = "up(1)";
			//down
			else if (valdata == "2")
				retvalue = "down(2)";
			//testing
			else if (valdata == "3")
				retvalue = "testing(3)";
			//testing
			else if (valdata == "4")
				retvalue = "unknown(4)";
			//dormant
			else if (valdata == "5")
				retvalue = "dormant(5)";
			//notPresent
			else if (valdata == "6")
				retvalue = "notPresent(6)";
			//lowerLayerDown
			else if (valdata == "7")
				retvalue = "lowerLayerDown(7)";
			else
				retvalue = valdata;

			return retvalue;

		}



		//ifTypeを変換
		public static string ifTypeConv(string str) 
		{
			string retstr = "";

			if (str != "")
			{

				//1:other
				switch (str){
					case "1":
						retstr = "1:other";
						break;
					case "2":
						retstr = "2:regular1822";
						break;
					case "3":
						retstr = "3:hdh1822";
						break;
					case "4":
						retstr = "4:ddnX25";
						break;
					case "5":
						retstr = "5:rfc877x25";
						break;
					case "6":
						retstr = "6:ethernetCsmacd";
						break;
					case "7":
						retstr = "7:iso88023Csmacd";
						break;
					case "8":
						retstr = "8:iso88024TokenBus";
						break;
					case "9":
						retstr = "9:iso88025TokenRing";
						break;
					case "10":
						retstr = "10:iso88026Man";
						break;
					case "11":
						retstr = "11:starLan";
						break;
					case "12":
						retstr = "12:proteon10Mbit";
						break;
					case "13":
						retstr = "13:proteon80Mbit";
						break;
					case "14":
						retstr = "14:hyperchannel";
						break;
					case "15":
						retstr = "15:fddi";
						break;
					case "16":
						retstr = "16:lapb";
						break;
					case "17":
						retstr = "17:sdlc";
						break;
					case "18":
						retstr = "18:ds1";
						break;
					case "19":
						retstr = "19:e1";
						break;
					case "20":
						retstr = "20:basicISDN";
						break;
					case "21":
						retstr = "21:primaryISDN";
						break;
					case "22":
						retstr = "22:propPointToPointSerial";
						break;
					case "23":
						retstr = "23:ppp";
						break;
					case "24":
						retstr = "24:softwareLoopback";
						break;
					case "25":
						retstr = "25:eon";
						break;
					case "26":
						retstr = "26:ethernet3Mbit";
						break;
					case "27":
						retstr = "27:nsip";
						break;
					case "28":
						retstr = "28:slip";
						break;
					case "29":
						retstr = "29:ultra";
						break;
					case "30":
						retstr = "30:ds3";
						break;
					case "31":
						retstr = "31:sip";
						break;
					case "32":
						retstr = "32:frameRelay";
						break;
					case "33":
						retstr = "33:rs232";
						break;
					case "34":
						retstr = "34:para";
						break;
					case "35":
						retstr = "35:arcnet";
						break;
					case "36":
						retstr = "36:arcnetPlus";
						break;
					case "37":
						retstr = "37:atm";
						break;
					case "38":
						retstr = "38:miox25";
						break;
					case "39":
						retstr = "39:sonet";
						break;
					case "40":
						retstr = "40:x25ple";
						break;
					case "41":
						retstr = "41:iso88022llc";
						break;
					case "42":
						retstr = "42:localTalk";
						break;
					case "43":
						retstr = "43:smdsDxi";
						break;
					case "44":
						retstr = "44:frameRelayService";
						break;
					case "45":
						retstr = "45:v35";
						break;
					case "46":
						retstr = "46:hssi";
						break;
					case "47":
						retstr = "47:hippi";
						break;
					case "48":
						retstr = "48:modem";
						break;
					case "49":
						retstr = "49:aal5";
						break;
					case "50":
						retstr = "50:sonetPath";
						break;
					case "51":
						retstr = "51:sonetVT";
						break;
					case "52":
						retstr = "52:smdsIcip";
						break;
					case "53":
						retstr = "53:propVirtual";
						break;
					case "54":
						retstr = "54:propMultiplexor";
						break;
					case "55":
						retstr = "55:ieee80212";
						break;
					case "56":
						retstr = "56:fibreChannel";
						break;
					case "57":
						retstr = "57:hippiInterface";
						break;
					case "58":
						retstr = "58:frameRelayInterconnect";
						break;
					case "59":
						retstr = "59:aflane8023";
						break;
					case "60":
						retstr = "60:aflane8025";
						break;
					case "61":
						retstr = "61:cctEmul";
						break;
					case "62":
						retstr = "62:fastEther";
						break;
					case "63":
						retstr = "63:isdn";
						break;
					case "64":
						retstr = "64:v11";
						break;
					case "65":
						retstr = "65:v36";
						break;
					case "66":
						retstr = "66:g703at64k";
						break;
					case "67":
						retstr = "67:g703at2mb";
						break;
					case "68":
						retstr = "68:qllc";
						break;
					case "69":
						retstr = "69:fastEtherFX";
						break;
					case "70":
						retstr = "70:channel";
						break;
					case "71":
						retstr = "71:ieee80211";
						break;
					case "72":
						retstr = "72:ibm370parChan";
						break;
					case "73":
						retstr = "73:escon";
						break;
					case "74":
						retstr = "74:dlsw";
						break;
					case "75":
						retstr = "75:isdns";
						break;
					case "76":
						retstr = "76:isdnu";
						break;
					case "77":
						retstr = "77:lapd";
						break;
					case "78":
						retstr = "78:ipSwitch";
						break;
					case "79":
						retstr = "79:rsrb";
						break;
					case "80":
						retstr = "80:atmLogical";
						break;
					case "81":
						retstr = "81:ds0";
						break;
					case "82":
						retstr = "82:ds0Bundle";
						break;
					case "83":
						retstr = "83:bsc";
						break;
					case "84":
						retstr = "84:async";
						break;
					case "85":
						retstr = "85:cnr";
						break;
					case "86":
						retstr = "86:iso88025Dtr";
						break;
					case "87":
						retstr = "87:eplrs";
						break;
					case "88":
						retstr = "88:arap";
						break;
					case "89":
						retstr = "89:propCnls";
						break;
					case "90":
						retstr = "90:hostPad";
						break;
					case "91":
						retstr = "91:termPad";
						break;
					case "92":
						retstr = "92:frameRelayMPI";
						break;
					case "93":
						retstr = "93:x213";
						break;
					case "94":
						retstr = "94:adsl";
						break;
					case "95":
						retstr = "95:radsl";
						break;
					case "96":
						retstr = "96:sdsl";
						break;
					case "97":
						retstr = "97:vdsl";
						break;
					case "98":
						retstr = "98:iso88025CRFPInt";
						break;
					case "99":
						retstr = "99:myrinet";
						break;
					case "100":
						retstr = "100:voiceEM";
						break;
					case "101":
						retstr = "101:voiceFXO";
						break;
					case "102":
						retstr = "102:voiceFXS";
						break;
					case "103":
						retstr = "103:voiceEncap";
						break;
					case "104":
						retstr = "104:voiceOverIp";
						break;
					case "105":
						retstr = "105:atmDxi";
						break;
					case "106":
						retstr = "106:atmFuni";
						break;
					case "107":
						retstr = "107:atmIma";
						break;
					case "108":
						retstr = "108:pppMultilinkBundle";
						break;
					case "109":
						retstr = "109:ipOverCdlc";
						break;
					case "110":
						retstr = "110:ipOverClaw";
						break;
					case "111":
						retstr = "111:stackToStack";
						break;
					case "112":
						retstr = "112:virtualIpAddress";
						break;
					case "113":
						retstr = "113:mpc";
						break;
					case "114":
						retstr = "114:ipOverAtm";
						break;
					case "115":
						retstr = "115:iso88025Fiber";
						break;
					case "116":
						retstr = "116:tdlc";
						break;
					case "117":
						retstr = "117:gigabitEthernet";
						break;
					case "118":
						retstr = "118:hdlc";
						break;
					case "119":
						retstr = "119:lapf";
						break;
					case "120":
						retstr = "120:v37";
						break;
					case "121":
						retstr = "121:x25mlp";
						break;
					case "122":
						retstr = "122:x25huntGroup";
						break;
					case "123":
						retstr = "123:trasnpHdlc";
						break;
					case "124":
						retstr = "124:interleave";
						break;
					case "125":
						retstr = "125:fast";
						break;
					case "126":
						retstr = "126:ip";
						break;
					case "127":
						retstr = "127:docsCableMaclayer";
						break;
					case "128":
						retstr = "128:docsCableDownstream";
						break;
					case "129":
						retstr = "129:docsCableUpstream";
						break;
					case "130":
						retstr = "130:a12MppSwitch";
						break;
					case "131":
						retstr = "131:tunnel";
						break;
					case "132":
						retstr = "132:coffee";
						break;
					case "133":
						retstr = "133:ces";
						break;
					case "134":
						retstr = "134:atmSubInterface";
						break;
					case "135":
						retstr = "135:l2vlan";
						break;
					case "136":
						retstr = "136:l3ipvlan";
						break;
					case "137":
						retstr = "137:l3ipxvlan";
						break;
					case "138":
						retstr = "138:digitalPowerline";
						break;
					case "139":
						retstr = "139:mediaMailOverIp";
						break;
					case "140":
						retstr = "140:dtm";
						break;
					case "141":
						retstr = "141:dcn";
						break;
					case "142":
						retstr = "142:ipForward";
						break;
					case "143":
						retstr = "143:msdsl";
						break;
					case "144":
						retstr = "144:ieee1394";
						break;
					case "145":
						retstr = "145:if-gsn";
						break;
					case "146":
						retstr = "146:dvbRccMacLayer";
						break;
					case "147":
						retstr = "147:dvbRccDownstream";
						break;
					case "148":
						retstr = "148:dvbRccUpstream";
						break;
					case "149":
						retstr = "149:atmVirtual";
						break;
					case "150":
						retstr = "150:mplsTunnel";
						break;
					case "151":
						retstr = "151:srp";
						break;
					case "152":
						retstr = "152:voiceOverAtm";
						break;
					case "153":
						retstr = "153:voiceOverFrameRelay";
						break;
					case "154":
						retstr = "154:idsl";
						break;
					case "155":
						retstr = "155:compositeLink";
						break;
					case "156":
						retstr = "156:ss7SigLink";
						break;
					case "157":
						retstr = "157:propWirelessP2P";
						break;
					case "158":
						retstr = "158:frForward";
						break;
					case "159":
						retstr = "159:rfc1483";
						break;
					case "160":
						retstr = "160:usb";
						break;
					case "161":
						retstr = "161:ieee8023adLag";
						break;
					case "162":
						retstr = "162:bgppolicyaccounting";
						break;
					case "163":
						retstr = "163:frf16MfrBundle";
						break;
					case "164":
						retstr = "164:h323Gatekeeper";
						break;
					case "165":
						retstr = "165:h323Proxy";
						break;
					case "166":
						retstr = "166:mpls";
						break;
					case "167":
						retstr = "167:mfSigLink";
						break;
					case "168":
						retstr = "168:hdsl2";
						break;
					case "169":
						retstr = "169:shdsl";
						break;
					case "170":
						retstr = "170:ds1FDL";
						break;
					case "171":
						retstr = "171:pos";
						break;
					case "172":
						retstr = "172:dvbAsiIn";
						break;
					case "173":
						retstr = "173:dvbAsiOut";
						break;
					case "174":
						retstr = "174:plc";
						break;
					case "175":
						retstr = "175:nfas";
						break;
					case "176":
						retstr = "176:tr008";
						break;
					case "177":
						retstr = "177:gr303RDT";
						break;
					case "178":
						retstr = "178:gr303IDT";
						break;
					case "179":
						retstr = "179:isup";
						break;
					case "180":
						retstr = "180:propDocsWirelessMaclayer";
						break;
					case "181":
						retstr = "181:propDocsWirelessDownstream";
						break;
					case "182":
						retstr = "182:propDocsWirelessUpstream";
						break;
					case "183":
						retstr = "183:hiperlan2";
						break;
					case "184":
						retstr = "184:propBWAp2Mp";
						break;
					case "185":
						retstr = "185:sonetOverheadChannel";
						break;
					case "186":
						retstr = "186:digitalWrapperOverheadChannel";
						break;
					case "187":
						retstr = "187:aal2";
						break;
					case "188":
						retstr = "188:radioMAC";
						break;
					case "189":
						retstr = "189:atmRadio";
						break;
					case "190":
						retstr = "190:imt";
						break;
					case "191":
						retstr = "191:mvl";
						break;
					case "192":
						retstr = "192:reachDSL";
						break;
					case "193":
						retstr = "193:frDlciEndPt";
						break;
					case "194":
						retstr = "194:atmVciEndPt";
						break;
					case "195":
						retstr = "195:opticalChannel";
						break;
					case "196":
						retstr = "196:opticalTransport";
						break;
					case "197":
						retstr = "197:propAtm";
						break;
					case "198":
						retstr = "198:voiceOverCable";
						break;
					case "199":
						retstr = "199:infiniband";
						break;
					case "200":
						retstr = "200:teLink";
						break;
					case "201":
						retstr = "201:q2931";
						break;
					case "202":
						retstr = "202:virtualTg";
						break;
					case "203":
						retstr = "203:sipTg";
						break;
					case "204":
						retstr = "204:sipSig";
						break;
					case "205":
						retstr = "205:docsCableUpstreamChannel";
						break;
					case "206":
						retstr = "206:econet";
						break;
					case "207":
						retstr = "207:pon155";
						break;
					case "208":
						retstr = "208:pon622";
						break;
					case "209":
						retstr = "209:bridge";
						break;
					case "210":
						retstr = "210:linegroup";
						break;
					case "211":
						retstr = "211:voiceEMFGD";
						break;
					case "212":
						retstr = "212:voiceFGDEANA";
						break;
					case "213":
						retstr = "213:voiceDID";
						break;
					case "214":
						retstr = "214:mpegTransport";
						break;
					case "215":
						retstr = "215:sixToFour";
						break;
					case "216":
						retstr = "216:gtp";
						break;
					case "217":
						retstr = "217:pdnEtherLoop1";
						break;
					case "218":
						retstr = "218:pdnEtherLoop2";
						break;
					case "219":
						retstr = "219:opticalChannelGroup";
						break;
					case "220":
						retstr = "220:homepna";
						break;
					case "221":
						retstr = "221:gfp";
						break;
					case "222":
						retstr = "222:ciscoISLvlan";
						break;
					case "223":
						retstr = "223:actelisMetaLOOP";
						break;
					case "224":
						retstr = "224:fcipLink";
						break;
					case "225":
						retstr = "225:rpr";
						break;
					case "226":
						retstr = "226:qam";
						break;
					case "227":
						retstr = "227:lmp";
						break;
					case "228":
						retstr = "228:cblVectaStar";
						break;
					case "229":
						retstr = "229:docsCableMCmtsDownstream";
						break;
					case "230":
						retstr = "230:adsl2";
						break;
					case "231":
						retstr = "231:macSecControlledIF";
						break;
					case "232":
						retstr = "232:macSecUncontrolledIF";
						break;
					case "233":
						retstr = "233:aviciOpticalEther";
						break;
					case "234":
						retstr = "234:atmbond";
						break;
					default:
						retstr = str;
						break;
				}


			}
			else{
				retstr = "";
			}

			return retstr;

		} 
	}

}
