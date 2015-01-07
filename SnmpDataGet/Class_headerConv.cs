using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnmpDGet
{
	public static class Class_headerConv
	{

		//メジャーなOIDは変換する
		public static string headerConvert(string strOid)
		{
			string retString = strOid;
			if (strOid == "")
			{
				return strOid;

			}
			else
			{

				//mib-2
				if (0 <= strOid.IndexOf("1.3.6.1.2.1."))
				{
					retString = strOid.Replace("1.3.6.1.2.1", "mib-2");
				}
				//enterprises
				if (0 <= strOid.IndexOf("1.3.6.1.4.1."))
				{
					retString = strOid.Replace("1.3.6.1.4.1", "enterprises");
				}
				//ifIndex
				if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.1."))
				{
					retString = "ifIndex";
				}
				//ifDescr
				else if(0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.2."))
				{
					retString = "ifDescr";
				}
				//ifType
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.3."))
				{
					retString = "ifType";
				}
				//ifMtu
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.4."))
				{
					retString = "ifMtu";
				}
				//ifSpeed
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.5."))
				{
					retString = "ifSpeed";
				}
				//ifPhysAddress
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.6."))
				{
					retString = "ifPhysAddress";
				}
				//ifAdminStatus
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.7."))
				{
					retString = "ifAdminStatus";
				}
				//ifOperStatus
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.8."))
				{
					retString = "ifOperStatus";
				}
				//ifLastChange
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.9."))
				{
					retString = "ifLastChange";
				}
				//ifInOctets
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.10."))
				{
					retString = "ifInOctets";
				}
				//ifInUcastPkts
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.11."))
				{
					retString = "ifInUcastPkts";
				}
				//ifInNUcastPkts
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.12."))
				{
					retString = "ifInNUcastPkts";
				}
				//ifInDiscards
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.13."))
				{
					retString = "ifInDiscards";
				}
				//ifInErrors
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.14."))
				{
					retString = "ifInErrors";
				}
				//ifInUnknownProtos
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.15."))
				{
					retString = "ifInUnknownProtos";
				}
				//ifOutOctets
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.16."))
				{
					retString = "ifOutOctets";
				}
				//ifOutUcastPkts
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.17."))
				{
					retString = "ifOutUcastPkts";
				}
				//ifOutNUcastPkts
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.18."))
				{
					retString = "ifOutNUcastPkts";
				}
				//ifOutDiscards
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.19."))
				{
					retString = "ifOutDiscards";
				}
				//ifOutErrors
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.20."))
				{
					retString = "ifOutErrors";
				}
				//ifOutQlen
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.21."))
				{
					retString = "ifOutQlen";
				}
				//ifSpecific
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.2.2.1.22."))
				{
					retString = "ifSpecific";
				}
				//sysDescr
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.1."))
				{
					retString = "sysDescr";
				}
				//sysObjectID
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.2."))
				{
					retString = "sysObjectID";
				}

				//sysUpTimeInstance
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.3."))
				{
					retString = "sysUpTimeInstance";
				}

				//sysContact
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.4."))
				{
					retString = "sysContact";
				}

				//sysName
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.5."))
				{
					retString = "sysName";
				}
				//sysLocation
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.6."))
				{
					retString = "sysLocation";
				}
				//sysServices
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.7."))
				{
					retString = "sysServices";
				}
				//sysORLastChange
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.8."))
				{
					retString = "sysORLastChange";
				}
				//sysORTable
				else if (0 <= strOid.IndexOf("1.3.6.1.2.1.1.8."))
				{
					retString = "sysORTable";
				}
				//LAN Manager
				else if (0 <= strOid.IndexOf("1.3.6.1.4.1.77."))
				{
					retString = strOid.Replace("1.3.6.1.4.1.77", "LANmanager");
				}	
				//nec
				else if (0 <= strOid.IndexOf("1.3.6.1.4.1.119."))
				{
					retString = strOid.Replace("1.3.6.1.4.1.119", "nec");
				}	


			}

			return retString;
		}
	}
}
