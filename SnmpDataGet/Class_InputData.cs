using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnmpSharpNet;

namespace SnmpDGet
{
	class Class_InputData
	{
		//GET方式
		private int _radioMethod;

		//ホスト名
		private string HOSTNAME;

		//バージョン
		private SnmpSharpNet.SnmpVersion  versionofsharp;
		private string VERSION;

		//コミュニティ
		private string COMMUNITY;

		//oid
		private string OID;

		//アクセサ
		public string hostname
		{
			get { return HOSTNAME; }

			set { HOSTNAME = value; }

		}
		public int method
		{
			get { return _radioMethod; }
			set	{_radioMethod = value; }

		}
		public string version
		{
			get { return VERSION; }
			set
			{
				VERSION = value;
				//v1
				if (value == "v1")
					versionofsharp = SnmpVersion.Ver1;
				//v2c				
				else if (value == "v2c")
					versionofsharp = SnmpVersion.Ver2;

				//何も取れなかったらv2
				else
					versionofsharp = SnmpVersion.Ver2;
			}
		}
		public  SnmpSharpNet.SnmpVersion versionofSNMPsharp
		{
			get { return versionofsharp; }
		}


		public string community
		{
			get { return COMMUNITY; }
			set { COMMUNITY = value; }
		}

		public string oid
		{
			get { return OID; }
			set { OID = value; }
		}
	}
}
