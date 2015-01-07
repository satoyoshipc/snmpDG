using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnmpDGet
{
	//コマンドライン実行時のパラメータを格納する
	class Class_paramerter
	{
		// -ip
		private string _ipaddress;
		// 方式
		private string _TYPE = "system";
		// -c
		private string _COMMUNITY = "public";
		// -v
		private string _VERSION = "2c";
		// -oid
		private string _OID;


		public string ipaddress {			
			get { return _ipaddress; }
			set { _ipaddress = value; } 
		}
		
		public string type
		{
			get { return _TYPE; }
			set { _TYPE = value; }
		}
		
		public string community
		{
			get { return _COMMUNITY; }
			set { _COMMUNITY = value; }
		}
		
		public string version
		{
			get { return _VERSION; }
			set { _VERSION = value; }
		}

		public string oid
		{
			get { return _OID; }
			set { _OID = value; }
		}	

	}





}
