// Copyright (c) 2013, guttally.net
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
//   * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//   
//   * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//   
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SnmpDGet
{
	public class Class_TextLog : IDisposable
	{
		//==================================
		// Constants
		//==================================

		public const string FmtTime = "{%time%}"; // 時刻
		public const string FmtText = "{%text%}"; // テキスト
		public const string FmtPID = "{%pid%}";   // プロセスID
		public const string FmtPName = "{%pname%}"; // プロセス名
		public const string FmtThID = "{%thid%}"; // スレッドID
		public const string FmtThName = "{%thname%}"; // スレッドID


		//==================================
		// Fields
		//==================================

		bool disposed;

		int maxGen;
		int rotSize;

		FileStream fs;
		StreamWriter sw;

		string format;
		string dateTimeFormat;


		//==================================
		// Constructor
		//==================================

		public Class_TextLog()
		{
			dateTimeFormat = "yyMMdd HH:mm:ss";
			format = string.Format("{0} {1}", FmtTime, FmtText);

			MaxGeneration = Properties.Settings.Default.LogRotetionGeneration;   // 過去 2 世代を残す
			RotationSize = Properties.Settings.Default.LogRotetionSize;  // 1 MB でローテーション

		}


		//==================================
		// Properties
		//==================================

		/// <summary>
		/// ログの行フォーマットを取得/設定します。
		/// </summary>
		public string Format
		{
			get
			{
				return format;
			}
			set
			{
				if (value == null) throw new ArgumentNullException();
				format = value;
			}
		}

		/// <summary>
		/// ログに付加する時刻のフォーマットを取得/設定します。
		/// </summary>
		public string TimeFormat
		{
			get
			{
				return dateTimeFormat;
			}
			set
			{
				if (value == null) throw new ArgumentNullException();
				dateTimeFormat = value;
			}
		}

		/// <summary>
		/// ローテーションの最大世代数を取得/設定します。
		/// </summary>
		public int MaxGeneration
		{
			get
			{
				return maxGen;
			}
			set
			{
				if (value < 0) value = 0;
				maxGen = value;
			}
		}

		/// <summary>
		/// ログをローテーションするスレッショルドとなるファイルサイズを
		/// kB 単位で取得、設定します。
		/// </summary>
		public int RotationSize
		{
			get
			{
				return rotSize >> 10;
			}
			set
			{
				if (value < 0) value = 0;
				rotSize = value << 10;
			}
		}

		//==================================
		// Methods
		//==================================

		public void Close()
		{
			if (sw != null) sw.Close();
			if (fs != null) fs.Close();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					Close();
					sw.Dispose();
					fs.Dispose();
				}
			}
			disposed = true;
		}

		public void Open( string path)
		{
			string dirPath = Path.GetDirectoryName(path);
			//if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
			
			fs = new FileStream(path, FileMode.Append,
								FileAccess.Write, FileShare.Read);
			sw = new StreamWriter(fs, Encoding.Unicode);
			sw.Flush();
		}

		public void Rotate()
		{
			// MaxGeneration が 0 または RotationSize が 0 の時はローテーションしない
			if (MaxGeneration <= 0 && RotationSize <= 0) return;

			string path = fs.Name;

			lock (this)
			{
				// 今のファイルをローテーション対象にリネーム
				Close();
				string newPath = addGenerationNum(path, 0);
				File.Move(path, newPath);
				Open(path);
			}

			// ファイルをローテーション
			FileNameComparer cmp = new FileNameComparer(path);
			deleteOldGeneration(path, cmp, MaxGeneration);
			incrementGeneration(path, cmp);
		}

		private string addGenerationNum(string path, int generation)
		{
			string name = Path.GetFileNameWithoutExtension(path);
			string ext = Path.GetExtension(path);
			string added = name + generation.ToString() + ext;
			return Path.Combine(Path.GetDirectoryName(path), added);
		}

		private static void deleteOldGeneration(string path, FileNameComparer cmp, int delGen)
		{
			List<string> files = getLogFiles(path, cmp);

			foreach (string file in files)
			{
				int filenum = cmp.GetFileNum(file);
				if (filenum == int.MaxValue) continue;
				if (delGen <= filenum) File.Delete(file);
			}
		}

		private void incrementGeneration(string path, FileNameComparer cmp)
		{
			List<string> files = getLogFiles(path, cmp);
			files.Sort(cmp);

			foreach (string file in files)
			{
				int newNum = cmp.GetFileNum(file) + 1;
				string newFile = cmp.ReplaceFileNum(file, newNum);
				File.Move(file, newFile);
			}
		}

		private static List<string> getLogFiles(string path, FileNameComparer cmp)
		{
			string[] files = Directory.GetFiles(Path.GetDirectoryName(path));
			List<string> ret = new List<string>();
			for (int i = 0; i < files.Length; i++)
			{
				if (cmp.Match(files[i])) ret.Add(files[i]);
			}
			return ret;
		}

		/// <summary>
		/// ログにテキストを書き込む。
		/// テキストに付加される時刻は Write を呼び出した時刻になる。
		/// </summary>
		/// <param name="text">書き込むテキスト</param>
		public void Write(string text)
		{
			Write(text, DateTime.Now);
		}

		/// <summary>
		/// ログにテキストを書き込む。
		/// </summary>
		/// <param name="text">書き込むテキスト</param>
		/// <param name="time">テキストに付加する時刻</param>
		public void Write(string text, DateTime time)
		{
			lock (this)
			{
				if (sw == null)
				{
					throw new InvalidOperationException(
					  "Open されていない Log オブジェクトの Write メソッドが呼び出されました。");
				}

				sw.WriteLine(createLine(text, time));
				sw.Flush();

				if (fs.Length > rotSize) Rotate();
			}
		}

		public string createLine(string text, DateTime time)
		{
			string line = format;

			line = line.Replace(FmtText, text);
			line = line.Replace(FmtTime, time.ToString(TimeFormat));
			line = line.Replace(FmtPID, Process.GetCurrentProcess().Id.ToString());
			line = line.Replace(FmtPName, Process.GetCurrentProcess().ProcessName);
			line = line.Replace(FmtThID, Thread.CurrentThread.ManagedThreadId.ToString());
			line = line.Replace(FmtThName, Thread.CurrentThread.Name);

			return line;
		}


		//==================================
		// Internal Classes
		//==================================

		class FileNameComparer : IComparer<string>
		{
			//================================
			// Constants
			//================================

			const string filenameGroupe = "name";
			const string filenumGroupe = "num";
			const string extensionGroupe = "ext";

			//================================
			// Fields
			//================================

			Regex reg;


			//================================
			// Constructor
			//================================

			public FileNameComparer(string filename)
			{
				string basename = Path.GetFileNameWithoutExtension(filename);
				string extension = Path.GetExtension(filename);
				string pattern = string.Format(@"(?<{2}>{0})(?<{3}>\d+)(?<{4}>\{1})",
											   basename,
											   extension,
											   filenameGroupe,
											   filenumGroupe,
											   extensionGroupe);
				reg = new Regex(pattern, RegexOptions.IgnoreCase);
			}


			//================================
			// Properties
			//================================

			public Regex FileNameRegex
			{
				get
				{
					return reg;
				}
			}


			//================================
			// Methods
			//================================

			/// <summary>
			/// ファイル番号が大きい順（大きいほうが先頭）に並べる。
			/// ただし、null は最小扱い。
			/// </summary>
			/// <param name="x">比較対象となるファイル名その 1</param>
			/// <param name="y">比較対象となるファイル名その 2</param>
			/// <returns></returns>
			public int Compare(string x, string y)
			{
				if (x == null && y == null) return 0;
				if (x == null) return 1;
				if (y == null) return -1;

				int nx = GetFileNum(x);
				int ny = GetFileNum(y);
				return ny - nx;
			}

			public bool Match(string filename)
			{
				return reg.Match(filename).Success;
			}

			public int GetFileNum(string filename)
			{
				Match m = reg.Match(filename);
				if (!m.Success) return int.MaxValue;

				int ret;
				if (!int.TryParse(m.Groups[filenumGroupe].Value, out ret)) ret = int.MaxValue;
				return ret;
			}

			public string ReplaceFileNum(string orgFileName, int newFileNum)
			{
				return reg.Replace(orgFileName, "${name}" + newFileNum.ToString() + "${ext}");
			}
		}
	}
}