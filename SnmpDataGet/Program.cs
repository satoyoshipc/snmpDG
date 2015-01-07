using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Forms;

namespace SnmpDGet
{


    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

			if (args.Length == 0) {

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);				
				Application.Run(new Form_snmpDataGet());
			}
			else
			{


				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run( new Form_snmpDataGet() );
			}

	
		
		
		
		
		
		
		}
    }
}
