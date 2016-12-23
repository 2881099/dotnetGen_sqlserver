using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;

namespace Server {

	internal partial class CodeBuild {

		protected class CONST {
			public static readonly string corePath = @"src\";
			public static readonly string adminPath = @"src\Admin\";
			public static readonly string xproj =
			#region 内容太长已被收起
 @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""14.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <PropertyGroup>
    <VisualStudioVersion Condition=""'$(VisualStudioVersion)' == ''"">14.0</VisualStudioVersion>
    <VSToolsPath Condition=""'$(VSToolsPath)' == ''"">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
  </PropertyGroup>

  <Import Project=""$(VSToolsPath)\DotNet\Microsoft.DotNet.Props"" Condition=""'$(VSToolsPath)' != ''"" />
  <PropertyGroup Label=""Globals"">
    <ProjectGuid>{0}</ProjectGuid>
    <RootNamespace>{1}</RootNamespace>
    <BaseIntermediateOutputPath Condition=""'$(BaseIntermediateOutputPath)'=='' "">.\obj</BaseIntermediateOutputPath>
    <OutputPath Condition=""'$(OutputPath)'=='' "">.\bin\</OutputPath>
    <TargetFrameworkVersion>v4.5.2</TargetFrameworkVersion>
  </PropertyGroup>

  <PropertyGroup>
    <SchemaVersion>2.0</SchemaVersion>
  </PropertyGroup>
  <Import Project=""$(VSToolsPath)\DotNet\Microsoft.DotNet.targets"" Condition=""'$(VSToolsPath)' != ''"" />
</Project>
";
			#endregion
			public static readonly string sln =
			#region 内容太长已被收起
 @"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 14
VisualStudioVersion = 14.0.25420.1
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{{2150E333-8FDC-42A3-9474-1A3956D46DE8}}"") = ""src"", ""src"", ""{{{0}}}""
EndProject
Project(""{{2150E333-8FDC-42A3-9474-1A3956D46DE8}}"") = ""Solution Items"", ""Solution Items"", ""{{{1}}}""
	ProjectSection(SolutionItems) = preProject
		global.json = global.json
	EndProjectSection
EndProject
Project(""{{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}}"") = ""Common"", ""src\Common\Common.xproj"", ""{{{2}}}""
EndProject
Project(""{{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}}"") = ""{5}.db"", ""src\{5}.db\{5}.db.xproj"", ""{{{3}}}""
EndProject
Project(""{{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}}"") = ""Admin"", ""src\Admin\Admin.xproj"", ""{{{4}}}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{2}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{2}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{2}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{2}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{3}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{3}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{3}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{3}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{4}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{4}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{4}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{4}}}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{{{2}}} = {{{0}}}
		{{{3}}} = {{{0}}}
		{{{4}}} = {{{0}}}
	EndGlobalSection
EndGlobal
";
			#endregion
			public static readonly string global_json =
			#region 内容太长已被收起
@"{{
  ""projects"": [ ""src"", ""test"" ],
  ""sdk"": {{
    ""version"": ""1.0.0-preview2-003121""
  }}
}}
";
			#endregion

			public static readonly string DAL_ConnectionManager_cs =
			#region 内容太长已被收起
 @"using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;

namespace {0}.DAL {{
	/// <summary>
	/// 数据库链接管理器
	/// </summary>
	public partial class ConnectionManager {{

		public static string ConnectionString = null;
		public static Dictionary<int, List<SqlConnection2>> ConnectionPool = new Dictionary<int, List<SqlConnection2>>();
		public static List<SqlConnection2> ConnectionPool2 = new List<SqlConnection2>();
		private static bool _dels_flag = false;
		private static DateTime _min_last_active = DateTime.MaxValue;
		private static object _lock = new object();

		/// <summary>
		/// 获取当前线程的 SqlConnection 连接
		/// </summary>
		/// <returns>返回一个 SqlConnection，注意：使用完毕后 Close 即可，请不要 Dispose 或相关方法消毁此 SqlConnection 的引用，否则将出现不可预料的错误</returns>
		public static SqlConnection GetConnection() {{
			if (string.IsNullOrEmpty(ConnectionString)) {{
				string key = ""{1}"";
				var ini = IniHelper.LoadIni(@""../web.config"");
				if (ini.ContainsKey(""connectionStrings"")) ConnectionString = ini[""connectionStrings""][key];
				if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException(key, string.Format(""未定义 ../web.config 里的 ConnectionStrings 键 '{{0}}' 或值不正确！"", key));
			}}

			SqlConnection2 conn = null;
			int tid = Thread.CurrentThread.ManagedThreadId;

			lock (_lock) {{
				if (!ConnectionPool.ContainsKey(tid)) ConnectionPool.Add(tid, new List<SqlConnection2>());
				conn = ConnectionPool[tid].Find(delegate(SqlConnection2 conn2) {{
					return conn2.SqlConnection != null && conn2.SqlConnection.State == ConnectionState.Closed;
				}});
				if (conn == null) {{
					conn = new SqlConnection2();
					conn.ThreadId = tid;
					conn.SqlConnection = new SqlConnection(ConnectionString);
					ConnectionPool[tid].Add(conn);
					ConnectionPool2.Add(conn);
				}}
				conn.LastActive = DateTime.Now;
			}}
			if (conn.LastActive < _min_last_active) {{
				_min_last_active = conn.LastActive;
			}}

			List<SqlConnection2> dels = null;
			TimeSpan ts = DateTime.Now - _min_last_active;
			if ((ConnectionPool.Count > 60 && ts.TotalSeconds > 90 || ts.TotalSeconds > 180) && !_dels_flag) {{
				lock (_lock) {{
					_dels_flag = true;
					_min_last_active = DateTime.MaxValue;
					dels = ConnectionPool2.FindAll(delegate(SqlConnection2 conn2) {{
						TimeSpan ts2 = DateTime.Now - conn2.LastActive;
						if (ts2.TotalMilliseconds <= 90 && conn2.LastActive < _min_last_active) {{
							_min_last_active = conn2.LastActive;
						}}
						return ts2.TotalSeconds > 90;
					}});
					foreach (SqlConnection2 del in dels) {{
						ConnectionPool[del.ThreadId].Remove(del);
						ConnectionPool2.Remove(del);
						if (ConnectionPool[del.ThreadId].Count == 0) {{
							ConnectionPool.Remove(del.ThreadId);
						}}
					}}
					_dels_flag = false;
				}}
			}}
			if (dels != null) {{
				for (int a = 0; a < dels.Count; a++) {{
					if (dels[a] != null) {{
						dels[a].SqlConnection.Close();
						dels[a].SqlConnection.Dispose();
					}}
				}}
			}}

			return conn.SqlConnection;
		}}
	}}

	public class SqlConnection2 {{
		public SqlConnection SqlConnection;
		public DateTime LastActive;
		internal int ThreadId;
	}}
}}";
			#endregion
			public static readonly string DAL_SqlHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace {0}.DAL {{
	public partial class SqlHelper {{

		public static ILogger Log = new LoggerFactory().CreateLogger(""{0}_DAL_sqlhelper"");

		static void LoggerException(SqlCommand cmd, Exception e) {{
			if (e == null) return;
			string log = ""数据库出错（执行SQL）〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓"" + cmd.CommandText + ""\r\n"";
			foreach (SqlParameter parm in cmd.Parameters) {{
				log += Lib.PadRight(parm.ParameterName, 20) + "" = "" + Lib.PadRight(parm.Value == null ? ""NULL"" : parm.Value, 20) + ""\r\n"";
			}}
			Log.LogError(log);

			RollbackTransaction();
			cmd.Parameters.Clear();
			cmd.Connection.Close();
			throw e;
		}}

		public static string Addslashes(string filter, params object[] parms) {{
			if (filter == null || parms == null) return string.Empty;
			if (parms.Length == 0) return filter;
			object[] nparms = new object[parms.Length];
			for (int a = 0; a < parms.Length; a++) {{
				if (parms[a] == null) nparms[a] = ""NULL"";
				else {{
					if (parms[a] is System.UInt16 ||
						parms[a] is System.UInt32 ||
						parms[a] is System.UInt64 ||
						parms[a] is System.Double ||
						parms[a] is System.Single ||
						parms[a] is System.Decimal ||
						parms[a] is System.Byte) {{
						nparms[a] = parms[a];
					}} else if (parms[a] is DateTime) {{
						DateTime dt = (DateTime)parms[a];
						nparms[a] = string.Concat(""'"", dt.ToString(""yyyy-MM-dd HH:mm:ss""), ""'"");
					}} else if (parms[a] is DateTime?) {{
						DateTime? dt = parms[a] as DateTime?;
						nparms[a] = string.Concat(""'"", dt.Value.ToString(""yyyy-MM-dd HH:mm:ss""), ""'"");
					}} else {{
						nparms[a] = string.Concat(""'"", parms[a].ToString().Replace(""'"", ""''""), ""'"");
						if (parms[a] is string) nparms[a] = string.Concat('N', nparms[a]);
					}}
				}}
			}}
			try {{ string ret = string.Format(filter, nparms); return ret; }} catch {{ return filter; }}
		}}
		public static void ExecuteReader(Action<IDataReader> readerHander, string cmdText, params SqlParameter[] cmdParms) {{
			ExecuteReader(readerHander, CommandType.Text, cmdText, cmdParms);
		}}
		public static void ExecuteReader(Action<IDataReader> readerHander, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
			Exception ex = Lib.Trys(delegate() {{
				if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.OpenAsync().Wait();
				try {{
					using (SqlDataReader dr = cmd.ExecuteReader()) {{
						while (dr.ReadAsync().Result) {{
							if (readerHander != null) readerHander(dr);
						}}
					}}
				}} catch {{
					throw;
				}}
			}}, 1);
			
			if (CurrentThreadTransaction == null) cmd.Connection.Close();
			LoggerException(cmd, ex);
		}}

		public static object[][] ExeucteArray(string cmdText, params SqlParameter[] cmdParms) {{
			return ExeucteArray(CommandType.Text, cmdText, cmdParms);
		}}
		public static object[][] ExeucteArray(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			List<object[]> ret = new List<object[]>();
			ExecuteReader(dr => {{
				object[] item = new object[dr.FieldCount];
				dr.GetValues(item);
				ret.Add(item);
			}}, cmdText, cmdParms);
			return ret.ToArray();
		}}

		public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms) {{
			return ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
		}}
		public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
			int val = 0;
			Exception ex = Lib.Trys(delegate() {{
				if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.OpenAsync().Wait();
				try {{
					val = cmd.ExecuteNonQueryAsync().Result;
				}} catch {{
					throw;
				}}
			}}, 1);

			if (CurrentThreadTransaction == null) cmd.Connection.Close();
			LoggerException(cmd, ex);
			cmd.Parameters.Clear();
			return val;
		}}

		public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms) {{
			return ExecuteScalar(CommandType.Text, cmdText, cmdParms);
		}}
		public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			SqlCommand cmd = new SqlCommand();
			PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
			object val = null;
			Exception ex = Lib.Trys(delegate () {{
				if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.OpenAsync().Wait();
				try {{
					val = cmd.ExecuteScalarAsync().Result;
				}} catch {{
					throw;
				}}
			}}, 1);

			if (CurrentThreadTransaction == null) cmd.Connection.Close();
			LoggerException(cmd, ex);
			cmd.Parameters.Clear();
			return val;
		}}

		private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms) {{
			cmd.CommandType = cmdType;
			cmd.CommandText = cmdText;

			if (conn == null) {{
				SqlTransaction tran = CurrentThreadTransaction;
				cmd.Connection = tran == null ? ConnectionManager.GetConnection() : tran.Connection;
				cmd.Transaction = tran == null ? null : tran;
			}} else {{
				cmd.Connection = conn;
			}}

			if (cmdParms != null) {{
				foreach (SqlParameter parm in cmdParms) {{
					if (parm == null) continue;
					if (parm.Value == null) parm.Value = DBNull.Value;
					cmd.Parameters.Add(parm);
				}}
			}}

			AutoCommitTransaction();
		}}

		#region 事务处理

		class SqlTransaction2 {{
			internal SqlTransaction Transaction;
			internal DateTime RunTime;
			internal TimeSpan Timeout;

			public SqlTransaction2(SqlTransaction tran, TimeSpan timeout) {{
				Transaction = tran;
				RunTime = DateTime.Now;
				Timeout = timeout;
			}}
		}}

		private static Dictionary<int, SqlTransaction2> _trans = new Dictionary<int, SqlTransaction2>();
		private static List<SqlTransaction2> _trans_tmp = new List<SqlTransaction2>();
		private static object _trans_lock = new object();

		private static SqlTransaction CurrentThreadTransaction {{
			get	{{
				int tid = Thread.CurrentThread.ManagedThreadId;

				if (_trans.ContainsKey(tid)) {{
					if (_trans[tid].Transaction.Connection != null) {{
						return _trans[tid].Transaction;
					}}
				}}
				return null;
			}}
		}}

		/// <summary>
		/// 启动事务
		/// </summary>
		public static void BeginTransaction() {{
			BeginTransaction(TimeSpan.FromSeconds(10));
		}}
		public static void BeginTransaction(TimeSpan timeout) {{
			int tid = Thread.CurrentThread.ManagedThreadId;
			SqlConnection conn = ConnectionManager.GetConnection();
			SqlTransaction2 tran = null;

			Exception ex = Lib.Trys(delegate() {{
				if (conn.State != ConnectionState.Open) conn.OpenAsync().Wait();
				tran = new SqlTransaction2(conn.BeginTransaction(), timeout);
			}}, 1);

			if (ex != null) {{
				Log.LogError(new EventId(9999, ""数据库出错（开启事务）""), ex, ""数据库出错（开启事务）"");
				throw ex;
			}}

			if (_trans.ContainsKey(tid)) {{
				CommitTransaction();
				_trans[tid] = tran;
			}} else {{
				_trans.Add(tid, tran);
			}}

			lock (_trans_lock) {{
				_trans_tmp.Add(tran);
			}}
		}}

		/// <summary>
		/// 自动提交事务
		/// </summary>
		private static void AutoCommitTransaction() {{
			if (_trans_tmp.Count > 0) {{
				List<SqlTransaction2> trans = null;

				lock (_trans_lock) {{
					trans = _trans_tmp.FindAll(delegate(SqlTransaction2 st2) {{
						TimeSpan ts = DateTime.Now - st2.RunTime;
						return ts > st2.Timeout;
					}});
				}}

				foreach (SqlTransaction2 tran in trans) {{
					CommitTransaction(true, tran);
				}}
			}}
		}}
		private static void CommitTransaction(bool isCommit, SqlTransaction2 tran) {{
			if (tran == null || tran.Transaction == null || tran.Transaction.Connection == null) return;

			try {{
				SqlConnection conn = tran.Transaction.Connection;
				if (isCommit) {{
					tran.Transaction.Commit();
				}} else {{
					tran.Transaction.Rollback();
				}}
				conn.Close();
			}} catch {{ }}

			lock (_trans_lock) {{
				_trans_tmp.Remove(tran);
			}}
		}}
		private static void CommitTransaction(bool isCommit) {{
			int tid = Thread.CurrentThread.ManagedThreadId;

			if (_trans.ContainsKey(tid)) {{
				CommitTransaction(isCommit, _trans[tid]);
			}}
		}}
		/// <summary>
		/// 提交事务
		/// </summary>
		public static void CommitTransaction() {{
			CommitTransaction(true);
		}}
		/// <summary>
		/// 回滚事务
		/// </summary>
		public static void RollbackTransaction() {{
			CommitTransaction(false);
		}}
		#endregion
	}}
}}";
			#endregion
			public static readonly string DAL_SqlHelper_SelectBuild_cs =
			#region 内容太长已被收起
			@"using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Reflection;

namespace {0}.DAL {{

	public partial class SqlHelper {{
		public partial interface IDAL {{
			string Table {{ get; }}
			string Field {{ get; }}
			string Sort {{ get; }}
			object GetItem(IDataReader dr, ref int index);
		}}
		public class SelectBuild<TReturnInfo, TLinket> : SelectBuild<TReturnInfo> where TLinket : SelectBuild<TReturnInfo> {{
			protected SelectBuild<TReturnInfo> Where1Or(string filterFormat, Array values) {{
				if (values == null) values = new object[] {{ null }};
				if (values.Length == 0) return this;
				if (values.Length == 1) return base.Where(filterFormat, values.GetValue(0));
				string filter = string.Empty;
				for (int a = 0; a < values.Length; a++) filter = string.Concat(filter, "" OR "", string.Format(filterFormat, ""{{"" + a + ""}}""));
				object[] parms = new object[values.Length];
				values.CopyTo(parms, 0);
				return base.Where(filter.Substring(4), parms);
			}}
			public new TLinket Count(out int count) {{
				return base.Count(out count) as TLinket;
			}}
			public new TLinket Where(string filter, params object[] parms) {{
				return base.Where(true, filter, parms) as TLinket;
			}}
			public new TLinket Where(bool isadd, string filter, params object[] parms) {{
				return base.Where(isadd, filter, parms) as TLinket;
			}}
			public new TLinket GroupBy(string groupby) {{
				return base.GroupBy(groupby) as TLinket;
			}}
			public new TLinket Having(string filter, params object[] parms) {{
				return base.Having(true, filter, parms) as TLinket;
			}}
			public new TLinket Having(bool isadd, string filter, params object[] parms) {{
				return base.Having(isadd, filter, parms) as TLinket;
			}}
			public new TLinket Sort(string sort) {{
				return base.Sort(sort) as TLinket;
			}}
			public new TLinket From<TBLL>() {{
				return base.From<TBLL>() as TLinket;
			}}
			public new TLinket From<TBLL>(string alias) {{
				return base.From<TBLL>(alias) as TLinket;
			}}
			public new TLinket InnerJoin<TBLL>(string alias, string on) {{
				return base.InnerJoin<TBLL>(alias, on) as TLinket;
			}}
			public new TLinket LeftJoin<TBLL>(string alias, string on) {{
				return base.LeftJoin<TBLL>(alias, on) as TLinket;
			}}
			public new TLinket RightJoin<TBLL>(string alias, string on) {{
				return base.RightJoin<TBLL>(alias, on) as TLinket;
			}}
			public new TLinket Skip(int skip) {{
				return base.Skip(skip) as TLinket;
			}}
			public new TLinket Limit(int limit) {{
				return base.Limit(limit) as TLinket;
			}}
			public SelectBuild(IDAL dal) : base(dal) {{ }}
		}}
		public class SelectBuild<TReturnInfo> {{
			protected int _limit, _skip;
			protected string _sort, _field, _table, _join, _where, _groupby, _having;
			protected List<IDAL> _dals = new List<IDAL>();
			public List<TReturnInfo> ToList() {{
				List<TReturnInfo> ret = new List<TReturnInfo>();
				SqlHelper.ExecuteReader(dr => {{
					int index = -1;
					TReturnInfo info = (TReturnInfo)_dals[0].GetItem(dr, ref index);
					ret.Add(info);
					for (int a = 1; a < _dals.Count; a++) {{
						object item = _dals[a].GetItem(dr, ref index);
						string name = _dals[a].GetType().Name;
						name = string.Concat(""Obj_"", name[0].ToString().ToLower(), name.Substring(1));
						Type type = info.GetType();
						PropertyInfo pro = type.GetProperty(name);
						if (pro == null) throw new Exception(string.Concat(type.FullName, "" 没有定义属性 "", name));
						pro.SetValue(info, item, null);
					}}
				}}, this.ToString());
				return ret;
			}}
			public TReturnInfo ToOne() {{
				List<TReturnInfo> ret = this.Limit(1).ToList();
				return ret.Count > 0 ? ret[0] : default(TReturnInfo);
			}}
			public override string ToString() {{
				if (string.IsNullOrEmpty(_sort) && _skip > 0) this.Sort(_dals[0].Sort);
				string top = _skip == 0 && _limit > 0 ? string.Concat(""TOP "", _limit, "" "") : string.Empty;
				string row_number = _skip > 0 ? string.Concat("", row_number() over("", _sort, "") AS rownum"") : string.Empty;
				string where = string.IsNullOrEmpty(_where) ? string.Empty : string.Concat("" \r\nWHERE "", _where.Substring(5));
				string orderby = _skip > 0 ? string.Empty : _sort;
				string sql = string.Concat(""SELECT "", top, _field, row_number, _table, _join, where, orderby);
				if (_skip > 0) sql = string.Concat(""WITH t AS ("", sql, "") \r\nSELECT t.* FROM t WHERE t.rownum "", _limit > 0 ? string.Concat(""between "", _skip + 1, "" and "", _skip + _limit) : string.Concat(""> "", _skip));
				return sql;
			}}
			public object[][] Aggregate(string fields) {{
				string top = _skip == 0 && _limit > 0 ? string.Concat(""TOP "", _limit, "" "") : string.Empty;
				string where = string.IsNullOrEmpty(_where) ? string.Empty : string.Concat("" \r\nWHERE "", _where.Substring(5));
				string having = string.IsNullOrEmpty(_groupby) ||
								string.IsNullOrEmpty(_having) ? string.Empty : string.Concat("" \r\nHAVING "", _having.Substring(5));
				string orderby = _skip > 0 ? string.Empty : _sort;
				string sql = string.Concat(""SELECT "", top, fields, _table, _join, where, _groupby, having, orderby);
				return SqlHelper.ExeucteArray(sql);
			}}
			public T Aggregate<T>(string fields) {{
				return Lib.ConvertTo<T>(this.Aggregate(fields)[0][0]);
			}}
			public int Count() {{
				return this.Aggregate<int>(""count(1)"");
			}}
			public SelectBuild<TReturnInfo> Count(out int count) {{
				count = this.Count();
				return this;
			}}
			public static SelectBuild<TReturnInfo> From(IDAL dal) {{
				return new SelectBuild<TReturnInfo>(dal);
			}}
			int _fields_count = 0;
			protected SelectBuild(IDAL dal) {{
				_dals.Add(dal);
				_field = dal.Field;
				_table = string.Concat("" \r\nFROM "", dal.Table, "" a"");
			}}
			public SelectBuild<TReturnInfo> From<TBLL>() {{
				return this.From<TBLL>(string.Empty);
			}}
			public SelectBuild<TReturnInfo> From<TBLL>(string alias) {{
				IDAL dal = this.ConvertTBLL<TBLL>();
				_table = string.Concat(_table, "", "", dal.Table, "" "", alias);
				return this;
			}}
			protected IDAL ConvertTBLL<TBLL>() {{
				string dalTypeName = typeof(TBLL).FullName.Replace("".BLL."", "".DAL."");
				IDAL dal = this.GetType().GetTypeInfo().Assembly.CreateInstance(dalTypeName) as IDAL;
				if (dal == null) throw new Exception(string.Concat(""找不到类型 "", dalTypeName));
				return dal;
			}}
			protected SelectBuild<TReturnInfo> Join<TBLL>(string alias, string on, string joinType) {{
				IDAL dal = this.ConvertTBLL<TBLL>();
				_dals.Add(dal);
				string fields2 = dal.Field.Replace(""a."", string.Concat(alias, "".""));
				string[] names = fields2.Split(new string[] {{ "", "" }}, StringSplitOptions.None);
				for (int a = 0; a < names.Length; a++) {{
					string ast = string.Concat("" as"", ++_fields_count);
					names[a] = string.Concat(names[a], ast);
				}}
				_field = string.Concat(_field, "", \r\n"", string.Join("", "", names));
				_join = string.Concat(_join, "" \r\n"", joinType, "" "", dal.Table, "" "", alias, "" ON "", on);
				return this;
			}}
			public SelectBuild<TReturnInfo> Where(string filter, params object[] parms) {{
				return this.Where(true, filter, parms);
			}}
			public SelectBuild<TReturnInfo> Where(bool isadd, string filter, params object[] parms) {{
				if (isadd) {{
					//将参数 = null 转换成 IS NULL
					if (parms != null && parms.Length > 0) {{
						for (int a = 0; a < parms.Length; a++)
							if (parms[a] == null)
								filter = Regex.Replace(filter, @""\s+=\s+\{{"" + a + @""\}}"", "" IS {{"" + a + ""}}"");
					}}
					_where = string.Concat(_where, "" AND ("", Addslashes(filter, parms), "")"");
				}}
				return this;
			}}
			public SelectBuild<TReturnInfo> GroupBy(string groupby) {{
				_groupby = groupby;
				if (string.IsNullOrEmpty(_groupby)) return this;
				_groupby = string.Concat("" \r\nGROUP BY "", _groupby);
				return this;
			}}
			public SelectBuild<TReturnInfo> Having(string filter, params object[] parms) {{
				return this.Having(true, filter, parms);
			}}
			public SelectBuild<TReturnInfo> Having(bool isadd, string filter, params object[] parms) {{
				if (string.IsNullOrEmpty(_groupby)) return this;
				if (isadd) _having = string.Concat(_having, "" AND ("", Addslashes(filter, parms), "")"");
				return this;
			}}
			public SelectBuild<TReturnInfo> Sort(string sort) {{
				if (!string.IsNullOrEmpty(sort)) _sort = string.Concat("" \r\nORDER BY "", sort);
				return this;
			}}
			public SelectBuild<TReturnInfo> InnerJoin<TBLL>(string alias, string on) {{
				return this.Join<TBLL>(alias, on, ""INNER JOIN"");
			}}
			public SelectBuild<TReturnInfo> LeftJoin<TBLL>(string alias, string on) {{
				return this.Join<TBLL>(alias, on, ""LEFT JOIN"");
			}}
			public SelectBuild<TReturnInfo> RightJoin<TBLL>(string alias, string on) {{
				return this.Join<TBLL>(alias, on, ""RIGHT JOIN"");
			}}
			public SelectBuild<TReturnInfo> Skip(int skip) {{
				_skip = skip;
				return this;
			}}
			public SelectBuild<TReturnInfo> Limit(int limit) {{
				_limit = limit;
				return this;
			}}
		}}
	}}
}}";
			#endregion

			public static readonly string BLL_Build_SqlHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace {0}.BLL {{

	/// <summary>
	/// {0}.DAL.SqlHelper 代理类，全部支持走事务
	/// </summary>
	public abstract class SqlHelper {{
		public static object[][] ExeucteArray(string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExeucteArray(CommandType.Text, cmdText, cmdParms);
		}}
		public static object[][] ExeucteArray(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExeucteArray(cmdType, cmdText, cmdParms);
		}}

		public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
		}}
		public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteNonQuery(cmdType, cmdText, cmdParms);
		}}

		public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteScalar(CommandType.Text, cmdText, cmdParms);
		}}
		public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {{
			return {0}.DAL.SqlHelper.ExecuteScalar(cmdType, cmdText, cmdParms);
		}}

		/// <summary>
		/// 开启事务（不支持异步），10秒未执行完将超时
		/// </summary>
		/// <param name=""handler"">事务体 () => {{}}</param>
		public static void Transaction(AnonymousHandler handler) {{
			Transaction(handler, TimeSpan.FromSeconds(10));
		}}
		/// <summary>
		/// 开启事务（不支持异步）
		/// </summary>
		/// <param name=""handler"">事务体 () => {{}}</param>
		/// <param name=""timeout"">超时</param>
		public static void Transaction(AnonymousHandler handler, TimeSpan timeout) {{
			try {{
				{0}.DAL.SqlHelper.BeginTransaction(timeout);
				handler();
				{0}.DAL.SqlHelper.CommitTransaction();
			}} catch (Exception ex) {{
				{0}.DAL.SqlHelper.RollbackTransaction();
				throw ex;
			}}
		}}
	}}
}}";
			#endregion
			public static readonly string BLL_Build_ItemCache_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;

namespace {0}.BLL {{
	public partial class ItemCache {{

		private static Dictionary<string, long> _dic1 = new Dictionary<string, long>();
		private static Dictionary<long, Dictionary<string, string>> _dic2 = new Dictionary<long, Dictionary<string, string>>();
		private static LinkedList<long> _linked = new LinkedList<long>();
		private static object _dic1_lock = new object();
		private static object _dic2_lock = new object();
		private static object _linked_lock = new object();

		public static void Clear() {{
			lock(_dic1_lock) {{
				_dic1.Clear();
			}}
			lock(_dic2_lock) {{
				_dic2.Clear();
			}}
			lock(_linked_lock) {{
				_linked.Clear();
			}}
		}}
		public static void Remove(string key) {{
			if (string.IsNullOrEmpty(key)) return;
			long time;
			if (_dic1.TryGetValue(key, out time) == false) return;

			lock (_dic1_lock) {{
				_dic1.Remove(key);
			}}
			if (_dic2.ContainsKey(time)) {{
				lock (_dic2_lock) {{
					_dic2.Remove(time);
				}}
			}}
			lock (_linked_lock) {{
				_linked.Remove(time);
			}}
		}}
		public static string Get(string key) {{
			if (string.IsNullOrEmpty(key)) return null;
			long time;
			if (_dic1.TryGetValue(key, out time) == false) return null;
			Dictionary<string, string> dic;
			if (_dic2.TryGetValue(time, out dic) == false) {{
				if (_dic1.ContainsKey(key)) {{
					lock (_dic1_lock) {{
						_dic1.Remove(key);
					}}
				}}
				return null;
			}}
			if (DateTime.Now.Subtract(new DateTime(2016, 5, 1)).TotalSeconds > time) {{
				if (_dic1.ContainsKey(key)) {{
					lock (_dic1_lock) {{
						_dic1.Remove(key);
					}}
				}}
				if (_dic2.ContainsKey(time)) {{
					lock (_dic2_lock) {{
						_dic2.Remove(time);
					}}
				}}
				lock (_linked_lock) {{
					_linked.Remove(time);
				}}
				return null;
			}}
			string ret;
			if (dic.TryGetValue(key, out ret) == false) return null;
			return ret;
		}}
		public static void Set(string key, string value, int expire) {{
			if (string.IsNullOrEmpty(key) || expire <= 0) return;
			long time_cur = (long)DateTime.Now.Subtract(new DateTime(2016, 5, 1)).TotalSeconds;
			long time = time_cur + expire;
			long time2;
			if (_dic1.TryGetValue(key, out time2) == false) {{
				lock (_dic1_lock) {{
					if (_dic1.TryGetValue(key, out time2) == false) {{
						_dic1.Add(key, time2 = time);
					}}
				}}
			}}
			if (time2 != time) {{
				lock (_dic1_lock) {{
					_dic1[key] = time;
				}}
				lock (_dic2_lock) {{
					_dic2.Remove(time2);
				}}
			}}
			Dictionary<string, string> dic;
			bool isNew = false;
			if (_dic2.TryGetValue(time, out dic) == false) {{
				lock (_dic2_lock) {{
					if (_dic2.TryGetValue(time, out dic) == false) {{
						_dic2.Add(time, dic = new Dictionary<string, string>());
						isNew = true;
					}}
					if (dic.ContainsKey(key) == false) dic.Add(key, value);
					else dic[key] = value;
				}}
			}} else {{
				lock (_dic2_lock) {{
					if (dic.ContainsKey(key) == false) dic.Add(key, value);
					else dic[key] = value;
				}}
			}}
			if (isNew == true) {{
				lock (_linked_lock) {{
					if (_linked.Count == 0) {{
						_linked.AddFirst(time);
					}} else {{
						LinkedListNode<long> node = _linked.First;
						while (node != null) {{
							if (node.Value < time_cur) {{
								_linked.Remove(node);
								Dictionary<string, string> dic_del;
								if (_dic2.TryGetValue(node.Value, out dic_del)) {{
									lock (_dic2_lock) {{
										_dic2.Remove(node.Value);
										foreach (KeyValuePair<string, string> dic_del_in in dic_del) {{
											if (_dic1.ContainsKey(dic_del_in.Key)) {{
												lock (_dic1_lock) {{
													_dic1.Remove(dic_del_in.Key);
												}}
											}}
										}}
									}}
								}}
								node = _linked.First;
							}} else break;
						}}
						if (node == null)
							_linked.AddFirst(time);
						else if (node != null && _linked.Last.Value < time)
							_linked.AddLast(time);
						else {{
							while (node != null && node.Value < time) node = node.Next;
							if (node != null && node.Value != time) {{
								_linked.AddBefore(node, time);
							}}
						}}
					}}
				}}
			}}
		}}
	}}
}}";
			#endregion
			public static readonly string BLL_Build_RedisHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace {0}.BLL {{
	public partial class RedisHelper {{
		public static string ConnectionString = """";

		private static ConnectionMultiplexer GetConnection() {{
			if (string.IsNullOrEmpty(ConnectionString)) {{
				string key = ""{0}RedisConnectionString"";
				var ini = IniHelper.LoadIni(@""../web.config"");
				if (ini.ContainsKey(""connectionStrings"")) ConnectionString = ini[""connectionStrings""][key];
				if (string.IsNullOrEmpty(ConnectionString)) throw new ArgumentNullException(key, string.Format(""未定义 ../web.config 里的 ConnectionStrings 键 '{{0}}' 或值不正确！"", key));
			}}
			return ConnectionMultiplexer.ConnectAsync(ConnectionString).Result;
		}}
		public static void Set(string name, string value) {{
			Set(name, value, 0);
		}}
		public static void Set(string name, string value, int expireSeconds) {{
			using (var cm = GetConnection()) {{
				var db = cm.GetDatabase();
				if (expireSeconds > 0) {{
					db.StringSetAsync(name, value, TimeSpan.FromSeconds(expireSeconds)).Wait();
					//db.KeyExpire(name, DateTime.Now.AddSeconds(expireSeconds));
				}} else db.StringSetAsync(name, value).Wait();
			}}
		}}
		public static string Get(string name) {{
			using (var cm = GetConnection()) {{
				var db = cm.GetDatabase();
				//db.Publish(""1"", """");
				return db.StringGetAsync(name).Result;
			}}
		}}
		public static void Remove(string name) {{
			using (var cm = GetConnection()) {{
				var db = cm.GetDatabase();
				db.KeyDeleteAsync(name).Wait();
			}}
		}}
	}}
}}
";
			#endregion
			public static readonly string Model_Build_ExtensionMethods_cs =
			#region 内容太长已被收起
 @"using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace {0}.Model {{
	public static partial class ExtensionMethods {{{1}
		public static string GetJson(IEnumerable items) {{
			StringBuilder sb = new StringBuilder();
			sb.Append(""["");
			IEnumerator ie = items.GetEnumerator();
			if (ie.MoveNext()) {{
				while (true) {{
					sb.Append(string.Concat(ie.Current));
					if (ie.MoveNext()) sb.Append("","");
					else break;
				}}
			}}
			sb.Append(""]"");
			return sb.ToString();
		}}
		public static IDictionary[] GetBson(IEnumerable items) {{
			List<IDictionary> ret = new List<IDictionary>();
			IEnumerator ie = items.GetEnumerator();
			while (ie.MoveNext())
				ret.Add(ie.Current.GetType().GetMethod(""ToBson"").Invoke(ie.Current, null) as IDictionary);
			return ret.ToArray();
		}}
	}}
}}";
			#endregion

			public static readonly string Db_project_json =
			#region 内容太长已被收起
 @"{{
  ""version"": ""1.0.0-*"",

    ""dependencies"": {{
        ""Common"": ""1.0.0-*"",
        ""Microsoft.Extensions.Configuration"": ""1.0.0"",
        ""Microsoft.Extensions.Logging"": ""1.0.0"",
        ""Microsoft.Extensions.Logging.Abstractions"": ""1.0.0"",
        ""NETStandard.Library"": ""1.6.0"",
        ""StackExchange.Redis"": ""1.1.604-alpha"",
        ""System.Data.SqlClient"": ""4.1.0""
    }},

  ""frameworks"": {{
    ""netstandard1.6"": {{
      ""imports"": ""dnxcore50""
    }}
  }}
}}
";
			#endregion

			public static readonly string Common_BmwNet_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

public sealed class BmwNet : IDisposable {{
	public interface IBmwNetOutput {{
		/// <summary>
		/// 
		/// </summary>
		/// <param name=""tOuTpUt"">返回内容</param>
		/// <param name=""oPtIoNs"">渲染对象</param>
		/// <param name=""rEfErErFiLeNaMe"">当前文件路径</param>
		/// <param name=""bMwSeNdEr""></param>
		/// <returns></returns>
		BmwNetReturnInfo OuTpUt(StringBuilder tOuTpUt, IDictionary oPtIoNs, string rEfErErFiLeNaMe, BmwNet bMwSeNdEr);
	}}
	public class BmwNetReturnInfo {{
		public Dictionary<string, int[]> Blocks;
		public StringBuilder Sb;
	}}
	public delegate bool BmwNetIf(object exp);
	public delegate void BmwNetPrint(params object[] parms);

	private static int _view = 0;
	private static Regex _reg = new Regex(@""\{{(\$BMW__CODE|\/\$BMW__CODE|import\s+|module\s+|extends\s+|block\s+|include\s+|for\s+|if\s+|#|\/for|elseif|else|\/if|\/block|\/module)([^\}}]*)\}}"", RegexOptions.Compiled);
	private static Regex _reg_forin = new Regex(@""^([\w_]+)\s*,?\s*([\w_]+)?\s+in\s+(.+)"", RegexOptions.Compiled);
	private static Regex _reg_foron = new Regex(@""^([\w_]+)\s*,?\s*([\w_]+)?,?\s*([\w_]+)?\s+on\s+(.+)"", RegexOptions.Compiled);
	private static Regex _reg_forab = new Regex(@""^([\w_]+)\s+([^,]+)\s*,\s*(.+)"", RegexOptions.Compiled);
	private static Regex _reg_miss = new Regex(@""\{{\/?miss\}}"", RegexOptions.Compiled);
	private static Regex _reg_code = new Regex(@""(\{{%|%\}})"", RegexOptions.Compiled);
	private static Regex _reg_syntax = new Regex(@""<(\w+)\s+@(if|for|else)\s*=""""([^""""]*)"""""", RegexOptions.Compiled);
	private static Regex _reg_htmltag = new Regex(@""<\/?\w+[^>]*>"", RegexOptions.Compiled);
	private static Regex _reg_blank = new Regex(@""\s+"", RegexOptions.Compiled);
	private static Regex _reg_complie_undefined = new Regex(@""(当前上下文中不存在名称)?“(\w+)”"", RegexOptions.Compiled);

	private Dictionary<string, IBmwNetOutput> _cache = new Dictionary<string, IBmwNetOutput>();
	private object _cache_lock = new object();
	private string _viewDir;
	private FileSystemWatcher _fsw = new FileSystemWatcher();

	public BmwNet(string viewDir) {{
		_viewDir = Utils.TranslateUrl(viewDir);
		_fsw = new FileSystemWatcher(_viewDir);
		_fsw.IncludeSubdirectories = true;
		_fsw.Changed += ViewDirChange;
		_fsw.Renamed += ViewDirChange;
		_fsw.EnableRaisingEvents = true;
	}}
	public void Dispose() {{
		_fsw.Dispose();
	}}
	void ViewDirChange(object sender, FileSystemEventArgs e) {{
		string filename = e.FullPath.ToLower();
		lock (_cache_lock) {{
			_cache.Remove(filename);
		}}
	}}
	public BmwNetReturnInfo RenderFile2(StringBuilder sb, IDictionary options, string filename, string refererFilename) {{
		if (filename[0] == '/' || string.IsNullOrEmpty(refererFilename)) refererFilename = _viewDir;
		//else refererFilename = Path.GetDirectoryName(refererFilename);
		string filename2 = Utils.TranslateUrl(filename, refererFilename);
		IBmwNetOutput bmw;
		if (_cache.TryGetValue(filename2, out bmw) == false) {{
			string tplcode = File.Exists(filename2) == false ? string.Concat(""文件不存在 "", filename) : Utils.ReadTextFile(filename2);
			bmw = Parser(tplcode, options);
			lock (_cache_lock) {{
				if (_cache.ContainsKey(filename2) == false) {{
					_cache.Add(filename2, bmw);
				}}
			}}
		}}
		try {{
			return bmw.OuTpUt(sb, options, filename2, this);
		}} catch (Exception ex) {{
			BmwNetReturnInfo ret = sb == null ?
				new BmwNetReturnInfo {{ Sb = new StringBuilder(), Blocks = new Dictionary<string, int[]>() }} :
				new BmwNetReturnInfo {{ Sb = sb, Blocks = new Dictionary<string, int[]>() }};
			ret.Sb.Append(refererFilename);
			ret.Sb.Append("" -> "");
			ret.Sb.Append(filename);
			ret.Sb.Append(""\r\n"");
			ret.Sb.Append(ex.Message);
			ret.Sb.Append(""\r\n"");
			ret.Sb.Append(ex.StackTrace);
			return ret;
		}}
	}}
	public string RenderFile(string filename, IDictionary options) {{
		BmwNetReturnInfo ret = this.RenderFile2(null, options, filename, null);
		return ret.Sb.ToString();
	}}
	private static IBmwNetOutput Parser(string tplcode, IDictionary options) {{
		int view = Interlocked.Increment(ref _view);
		StringBuilder sb = new StringBuilder();
		IDictionary options_copy = new Hashtable();
		foreach (DictionaryEntry options_de in options) options_copy[options_de.Key] = options_de.Value;
		sb.AppendFormat(@""
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using {0}.BLL;
using {0}.Model;

namespace BmwDynamicCodeGenerate {{{{
	public class view{{0}} : BmwNet.IBmwNetOutput {{{{
		public BmwNet.BmwNetReturnInfo OuTpUt(StringBuilder tOuTpUt, IDictionary oPtIoNs, string rEfErErFiLeNaMe, BmwNet bMwSeNdEr) {{{{
			BmwNet.BmwNetReturnInfo rTn = tOuTpUt == null ? 
				new BmwNet.BmwNetReturnInfo {{{{ Sb = (tOuTpUt = new StringBuilder()), Blocks = new Dictionary<string, int[]>() }}}} :
				new BmwNet.BmwNetReturnInfo {{{{ Sb = tOuTpUt, Blocks = new Dictionary<string, int[]>() }}}};
			Dictionary<string, int[]> BMW__blocks = rTn.Blocks;
			Stack<int[]> BMW__blocks_stack = new Stack<int[]>();
			int[] BMW__blocks_stack_peek;
			List<IDictionary> BMW__forc = new List<IDictionary>();

			Func<IDictionary> pRoCeSsOpTiOnS = new Func<IDictionary>(delegate () {{{{
				IDictionary nEwoPtIoNs = new Hashtable();
				foreach (DictionaryEntry oPtIoNs_dE in oPtIoNs)
					nEwoPtIoNs[oPtIoNs_dE.Key] = oPtIoNs_dE.Value;
				foreach (IDictionary BMW__forc_dIc in BMW__forc)
					foreach (DictionaryEntry BMW__forc_dIc_dE in BMW__forc_dIc)
						nEwoPtIoNs[BMW__forc_dIc_dE.Key] = BMW__forc_dIc_dE.Value;
				return nEwoPtIoNs;
			}}}});
			BmwNet.BmwNetIf bMwIf = delegate(object exp) {{{{
				if (exp is bool) return (bool)exp;
				if (exp == null) return false;
				if (exp is int && (int)exp == 0) return false;
				if (exp is string && (string)exp == string.Empty) return false;

				if (exp is long && (long)exp == 0) return false;
				if (exp is short && (short)exp == 0) return false;
				if (exp is byte && (byte)exp == 0) return false;

				if (exp is double && (double)exp == 0) return false;
				if (exp is float && (float)exp == 0) return false;
				if (exp is decimal && (decimal)exp == 0) return false;
				return true;
			}}}};
			BmwNet.BmwNetPrint print = delegate(object[] pArMs) {{{{
				if (pArMs == null || pArMs.Length == 0) return;
				foreach (object pArMs_A in pArMs) if (pArMs_A != null) tOuTpUt.Append(pArMs_A);
			}}}};
			BmwNet.BmwNetPrint Print = print;"", view);

		#region {{miss}}...{{/miss}}块内容将不被解析
		string[] tmp_content_arr = _reg_miss.Split(tplcode);
		if (tmp_content_arr.Length > 1) {{
			sb.AppendFormat(@""
			string[] BMW__MISS = new string[{{0}}];"", Math.Ceiling(1.0 * (tmp_content_arr.Length - 1) / 2));
			int miss_len = -1;
			for (int a = 1; a < tmp_content_arr.Length; a += 2) {{
				sb.Append(string.Concat(@""
			BMW__MISS["", ++miss_len, @""] = """""", Utils.GetConstString(tmp_content_arr[a]), @"""""";""));
				tmp_content_arr[a] = string.Concat(""{{#BMW__MISS["", miss_len, ""]}}"");
			}}
			tplcode = string.Join("""", tmp_content_arr);
		}}
		#endregion
		#region 扩展语法如 <div @if=""表达式""></div>
		tplcode = htmlSyntax(tplcode, 3); //<div @if=""c#表达式"" @for=""index 1,100""></div>
										  //处理 {{% %}} 块 c#代码
		tmp_content_arr = _reg_code.Split(tplcode);
		if (tmp_content_arr.Length == 1) {{
			tplcode = Utils.GetConstString(tplcode)
				.Replace(""{{%"", ""{{$BMW__CODE}}"")
				.Replace(""%}}"", ""{{/$BMW__CODE}}"");
		}} else {{
			tmp_content_arr[0] = Utils.GetConstString(tmp_content_arr[0]);
			for (int a = 1; a < tmp_content_arr.Length; a += 4) {{
				tmp_content_arr[a] = ""{{$BMW__CODE}}"";
				tmp_content_arr[a + 2] = ""{{/$BMW__CODE}}"";
				tmp_content_arr[a + 3] = Utils.GetConstString(tmp_content_arr[a + 3]);
			}}
			tplcode = string.Join("""", tmp_content_arr);
		}}
		#endregion
		sb.Append(@""
			tOuTpUt.Append("""""");

		string error = null;
		int bmw_tmpid = 0;
		int forc_i = 0;
		string extends = null;
		Stack<string> codeTree = new Stack<string>();
		Stack<string> forEndRepl = new Stack<string>();
		sb.Append(_reg.Replace(tplcode, delegate (Match m) {{
			string _0 = m.Groups[0].Value;
			if (!string.IsNullOrEmpty(error)) return _0;

			string _1 = m.Groups[1].Value.Trim(' ', '\t');
			string _2 = m.Groups[2].Value
				.Replace(""\\\\"", ""\\"")
				.Replace(""\\\"""", ""\"""");
			_2 = Utils.ReplaceSingleQuote(_2);

			switch (_1) {{
				#region $BMW__CODE--------------------------------------------------
				case ""$BMW__CODE"":
					codeTree.Push(_1);
					return @"""""");
"";
				case ""/$BMW__CODE"":
					string pop = codeTree.Pop();
					if (pop != ""$BMW__CODE"") {{
						codeTree.Push(pop);
						error = ""编译出错，{{% 与 %}} 并没有配对"";
						return _0;
					}}
					return @""
			tOuTpUt.Append("""""";
				#endregion
				case ""include"":
					return string.Format(@"""""");
bMwSeNdEr.RenderFile2(tOuTpUt, pRoCeSsOpTiOnS(), """"{{0}}"""", rEfErErFiLeNaMe);
			tOuTpUt.Append("""""", _2);
				case ""import"":
					return _0;
				case ""module"":
					return _0;
				case ""/module"":
					return _0;
				case ""extends"":
					//{{extends ../inc/layout.html}}
					if (string.IsNullOrEmpty(extends) == false) return _0;
					extends = _2;
					return string.Empty;
				case ""block"":
					codeTree.Push(""block"");
					return string.Format(@"""""");
BMW__blocks_stack_peek = new int[] {{{{ tOuTpUt.Length, 0 }}}};
BMW__blocks_stack.Push(BMW__blocks_stack_peek);
BMW__blocks.Add(""""{{0}}"""", BMW__blocks_stack_peek);
tOuTpUt.Append("""""", _2.Trim(' ', '\t'));
				case ""/block"":
					codeTreeEnd(codeTree, ""block"");
					return @"""""");
BMW__blocks_stack_peek = BMW__blocks_stack.Pop();
BMW__blocks_stack_peek[1] = tOuTpUt.Length - BMW__blocks_stack_peek[0];
tOuTpUt.Append("""""";

				#region ##---------------------------------------------------------
				case ""#"":
					if (_2[0] == '#')
						return string.Format(@"""""");
			try {{{{ Print({{0}}); }}}} catch {{{{ }}}}
			tOuTpUt.Append("""""", _2.Substring(1));
					return string.Format(@"""""");
			Print({{0}});
			tOuTpUt.Append("""""", _2);
				#endregion
				#region for--------------------------------------------------------
				case ""for"":
					forc_i++;
					int cur_bmw_tmpid = bmw_tmpid;
					string sb_endRepl = string.Empty;
					StringBuilder sbfor = new StringBuilder();
					sbfor.Append(@"""""");"");
					Match mfor = _reg_forin.Match(_2);
					if (mfor.Success) {{
						string mfor1 = mfor.Groups[1].Value.Trim(' ', '\t');
						string mfor2 = mfor.Groups[2].Value.Trim(' ', '\t');
						sbfor.AppendFormat(@""
//new Action(delegate () {{{{
	IDictionary BMW__tmp{{0}} = new Hashtable();
	BMW__forc.Add(BMW__tmp{{0}});
	var BMW__tmp{{1}} = {{3}};
	var BMW__tmp{{2}} = {{4}};"", ++bmw_tmpid, ++bmw_tmpid, ++bmw_tmpid, mfor.Groups[3].Value, mfor1);
						sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor1, cur_bmw_tmpid + 3));
						if (options_copy.Contains(mfor1) == false) options_copy[mfor1] = null;
						if (!string.IsNullOrEmpty(mfor2)) {{
							sbfor.AppendFormat(@""
	var BMW__tmp{{1}} = {{0}};
	{{0}} = 0;"", mfor2, ++bmw_tmpid);
							sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor2, bmw_tmpid));
							if (options_copy.Contains(mfor2) == false) options_copy[mfor2] = null;
						}}
						sbfor.AppendFormat(@""
	if (BMW__tmp{{1}} != null)
	foreach (var BMW__tmp{{0}} in BMW__tmp{{1}}) {{{{"", ++bmw_tmpid, cur_bmw_tmpid + 2);
						if (!string.IsNullOrEmpty(mfor2))
							sbfor.AppendFormat(@""
		BMW__tmp{{1}}[""""{{0}}""""] = ++ {{0}};"", mfor2, cur_bmw_tmpid + 1);
						sbfor.AppendFormat(@""
		BMW__tmp{{1}}[""""{{0}}""""] = BMW__tmp{{2}};
		{{0}} = BMW__tmp{{2}};
		tOuTpUt.Append("""""", mfor1, cur_bmw_tmpid + 1, bmw_tmpid);
						codeTree.Push(""for"");
						forEndRepl.Push(sb_endRepl);
						return sbfor.ToString();
					}}
					mfor = _reg_foron.Match(_2);
					if (mfor.Success) {{
						string mfor1 = mfor.Groups[1].Value.Trim(' ', '\t');
						string mfor2 = mfor.Groups[2].Value.Trim(' ', '\t');
						string mfor3 = mfor.Groups[3].Value.Trim(' ', '\t');
						sbfor.AppendFormat(@""
//new Action(delegate () {{{{
	IDictionary BMW__tmp{{0}} = new Hashtable();
	BMW__forc.Add(BMW__tmp{{0}});
	var BMW__tmp{{1}} = {{3}};
	var BMW__tmp{{2}} = {{4}};"", ++bmw_tmpid, ++bmw_tmpid, ++bmw_tmpid, mfor.Groups[4].Value, mfor1);
						sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor1, cur_bmw_tmpid + 3));
						if (options_copy.Contains(mfor1) == false) options_copy[mfor1] = null;
						if (!string.IsNullOrEmpty(mfor2)) {{
							sbfor.AppendFormat(@""
	var BMW__tmp{{1}} = {{0}};"", mfor2, ++bmw_tmpid);
							sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor2, bmw_tmpid));
							if (options_copy.Contains(mfor2) == false) options_copy[mfor2] = null;
						}}
						if (!string.IsNullOrEmpty(mfor3)) {{
							sbfor.AppendFormat(@""
	var BMW__tmp{{1}} = {{0}};
	{{0}} = 0;"", mfor3, ++bmw_tmpid);
							sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor3, bmw_tmpid));
							if (options_copy.Contains(mfor3) == false) options_copy[mfor3] = null;
						}}
						sbfor.AppendFormat(@""
	if (BMW__tmp{{2}} != null)
	foreach (DictionaryEntry BMW__tmp{{1}} in BMW__tmp{{2}}) {{{{
		{{0}} = BMW__tmp{{1}}.Key;
		BMW__tmp{{3}}[""""{{0}}""""] = {{0}};"", mfor1, ++bmw_tmpid, cur_bmw_tmpid + 2, cur_bmw_tmpid + 1);
						if (!string.IsNullOrEmpty(mfor2))
							sbfor.AppendFormat(@""
		{{0}} = BMW__tmp{{1}}.Value;
		BMW__tmp{{2}}[""""{{0}}""""] = {{0}};"", mfor2, bmw_tmpid, cur_bmw_tmpid + 1);
						if (!string.IsNullOrEmpty(mfor3))
							sbfor.AppendFormat(@""
		BMW__tmp{{1}}[""""{{0}}""""] = ++ {{0}};"", mfor3, cur_bmw_tmpid + 1);
						sbfor.AppendFormat(@""
		tOuTpUt.Append("""""");
						codeTree.Push(""for"");
						forEndRepl.Push(sb_endRepl);
						return sbfor.ToString();
					}}
					mfor = _reg_forab.Match(_2);
					if (mfor.Success) {{
						string mfor1 = mfor.Groups[1].Value.Trim(' ', '\t');
						sbfor.AppendFormat(@""
//new Action(delegate () {{{{
	IDictionary BMW__tmp{{0}} = new Hashtable();
	BMW__forc.Add(BMW__tmp{{0}});
	var BMW__tmp{{1}} = {{5}};
	{{5}} = {{3}} - 1;
	if ({{5}} == null) {{5}} = 0;
	var BMW__tmp{{2}} = {{4}} + 1;
	while (++{{5}} < BMW__tmp{{2}}) {{{{
		BMW__tmp{{0}}[""""{{5}}""""] = {{5}};
		tOuTpUt.Append("""""", ++bmw_tmpid, ++bmw_tmpid, ++bmw_tmpid, mfor.Groups[2].Value, mfor.Groups[3].Value, mfor1);
						sb_endRepl = string.Concat(sb_endRepl, string.Format(@""
	{{0}} = BMW__tmp{{1}};"", mfor1, cur_bmw_tmpid + 1));
						if (options_copy.Contains(mfor1) == false) options_copy[mfor1] = null;
						codeTree.Push(""for"");
						forEndRepl.Push(sb_endRepl);
						return sbfor.ToString();
					}}
					return _0;
				case ""/for"":
					if (--forc_i < 0) return _0;
					codeTreeEnd(codeTree, ""for"");
					return string.Format(@"""""");
	}}}}{{0}}
	BMW__forc.RemoveAt(BMW__forc.Count - 1);
//}}}})();
			tOuTpUt.Append("""""", forEndRepl.Pop());
				#endregion
				#region if---------------------------------------------------------
				case ""if"":
					codeTree.Push(""if"");
					return string.Format(@"""""");
			if ({{1}}bMwIf({{0}})) {{{{
				tOuTpUt.Append("""""", _2[0] == '!' ? _2.Substring(1) : _2, _2[0] == '!' ? '!' : ' ');
				case ""elseif"":
					codeTreeEnd(codeTree, ""if"");
					codeTree.Push(""if"");
					return string.Format(@"""""");
			}}}} else if ({{1}}bMwIf({{0}})) {{{{
				tOuTpUt.Append("""""", _2[0] == '!' ? _2.Substring(1) : _2, _2[0] == '!' ? '!' : ' ');
				case ""else"":
					codeTreeEnd(codeTree, ""if"");
					codeTree.Push(""if"");
					return @"""""");
			}} else {{
			tOuTpUt.Append("""""";
				case ""/if"":
					codeTreeEnd(codeTree, ""if"");
					return @"""""");
			}}
			tOuTpUt.Append("""""";
					#endregion
			}}
			return _0;
		}}));

		sb.Append(@"""""");"");
		if (string.IsNullOrEmpty(extends) == false) {{
			sb.AppendFormat(@""
BmwNet.BmwNetReturnInfo eXtEnDs_ReT = bMwSeNdEr.RenderFile2(null, pRoCeSsOpTiOnS(), """"{{0}}"""", rEfErErFiLeNaMe);
string rTn_Sb_string = rTn.Sb.ToString();
foreach(string eXtEnDs_ReT_blocks_key in eXtEnDs_ReT.Blocks.Keys) {{{{
	if (rTn.Blocks.ContainsKey(eXtEnDs_ReT_blocks_key)) {{{{
		int[] eXtEnDs_ReT_blocks_value = eXtEnDs_ReT.Blocks[eXtEnDs_ReT_blocks_key];
		eXtEnDs_ReT.Sb.Remove(eXtEnDs_ReT_blocks_value[0], eXtEnDs_ReT_blocks_value[1]);
		int[] rTn_blocks_value = rTn.Blocks[eXtEnDs_ReT_blocks_key];
		eXtEnDs_ReT.Sb.Insert(eXtEnDs_ReT_blocks_value[0], rTn_Sb_string.Substring(rTn_blocks_value[0], rTn_blocks_value[1]));
		foreach(string eXtEnDs_ReT_blocks_keyb in eXtEnDs_ReT.Blocks.Keys) {{{{
			if (eXtEnDs_ReT_blocks_keyb == eXtEnDs_ReT_blocks_key) continue;
			int[] eXtEnDs_ReT_blocks_valueb = eXtEnDs_ReT.Blocks[eXtEnDs_ReT_blocks_keyb];
			if (eXtEnDs_ReT_blocks_valueb[0] >= eXtEnDs_ReT_blocks_value[0])
				eXtEnDs_ReT_blocks_valueb[0] = eXtEnDs_ReT_blocks_valueb[0] - eXtEnDs_ReT_blocks_value[1] + rTn_blocks_value[1];
		}}}}
		eXtEnDs_ReT_blocks_value[1] = rTn_blocks_value[1];
	}}}}
}}}}
return eXtEnDs_ReT;
"", extends);
		}} else {{
			sb.Append(@""
return rTn;"");
		}}
		sb.Append(@""
		}}
	}}
}}
"");
		int dim_idx = sb.ToString().IndexOf(""BmwNet.BmwNetPrint Print = print;"") + 33;
		foreach (string dic_name in options_copy.Keys) {{
			sb.Insert(dim_idx, string.Format(@""
			dynamic {{0}} = oPtIoNs[""""{{0}}""""];"", dic_name));
		}}
		//Console.WriteLine(sb.ToString());
		return Complie(sb.ToString(), @""BmwDynamicCodeGenerate.view"" + view);
	}}
	private static string codeTreeEnd(Stack<string> codeTree, string tag) {{
		string ret = string.Empty;
		Stack<int> pop = new Stack<int>();
		foreach (string ct in codeTree) {{
			if (ct == ""import"" ||
				ct == ""include"") {{
				pop.Push(1);
			}} else if (ct == tag) {{
				pop.Push(2);
				break;
			}} else {{
				if (string.IsNullOrEmpty(tag) == false) pop.Clear();
				break;
			}}
		}}
		if (pop.Count == 0 && string.IsNullOrEmpty(tag) == false)
			return string.Concat(""语法错误，{{"", tag, ""}} {{/"", tag, ""}} 并没配对"");
		while (pop.Count > 0 && pop.Pop() > 0) codeTree.Pop();
		return ret;
	}}
	#region htmlSyntax
	private static string htmlSyntax(string tplcode, int num) {{

		while (num-- > 0) {{
			string[] arr = _reg_syntax.Split(tplcode);

			if (arr.Length == 1) break;
			for (int a = 1; a < arr.Length; a += 4) {{
				string tag = string.Concat('<', arr[a]);
				string end = string.Concat(""</"", arr[a], '>');
				int fc = 1;
				for (int b = a; fc > 0 && b < arr.Length; b += 4) {{
					if (b > a && arr[a].ToLower() == arr[b].ToLower()) fc++;
					int bpos = 0;
					while (true) {{
						int fa = arr[b + 3].IndexOf(tag, bpos);
						int fb = arr[b + 3].IndexOf(end, bpos);
						if (b == a) {{
							var z = arr[b + 3].IndexOf(""/>"");
							if ((fb == -1 || z < fb) && z != -1) {{
								var y = arr[b + 3].Substring(0, z + 2);
								if (_reg_htmltag.IsMatch(y) == false)
									fb = z - end.Length + 2;
							}}
						}}
						if (fa == -1 && fb == -1) break;
						if (fa != -1 && (fa < fb || fb == -1)) {{
							fc++;
							bpos = fa + tag.Length;
							continue;
						}}
						if (fb != -1) fc--;
						if (fc <= 0) {{
							var a1 = arr[a + 1];
							var end3 = string.Concat(""{{/"", a1, ""}}"");
							if (a1.ToLower() == ""else"") {{
								if (_reg_blank.Replace(arr[a - 4 + 3], """").EndsWith(""{{/if}}"", StringComparison.CurrentCultureIgnoreCase) == true) {{
									var idx = arr[a - 4 + 3].IndexOf(""{{/if}}"");
									arr[a - 4 + 3] = string.Concat(arr[a - 4 + 3].Substring(0, idx), arr[a - 4 + 3].Substring(idx + 5));
									//如果 @else=""有条件内容""，则变换成 elseif 条件内容
									if (_reg_blank.Replace(arr[a + 2], """").Length > 0) a1 = ""elseif"";
									end3 = ""{{/if}}"";
								}} else {{
									arr[a] = string.Concat(""指令 @"", arr[a + 1], ""='"", arr[a + 2], ""' 没紧接着 if/else 指令之后，无效. <"", arr[a]);
									arr[a + 1] = arr[a + 2] = string.Empty;
								}}
							}}
							if (arr[a + 1].Length > 0) {{
								if (_reg_blank.Replace(arr[a + 2], """").Length > 0 || a1.ToLower() == ""else"") {{
									arr[b + 3] = string.Concat(arr[b + 3].Substring(0, fb + end.Length), end3, arr[b + 3].Substring(fb + end.Length));
									arr[a] = string.Concat(""{{"", a1, "" "", arr[a + 2], ""}}<"", arr[a]);
									arr[a + 1] = arr[a + 2] = string.Empty;
								}} else {{
									arr[a] = string.Concat('<', arr[a]);
									arr[a + 1] = arr[a + 2] = string.Empty;
								}}
							}}
							break;
						}}
						bpos = fb + end.Length;
					}}
				}}
				if (fc > 0) {{
					arr[a] = string.Concat(""不严谨的html格式，请检查 "", arr[a], "" 的结束标签, @"", arr[a + 1], ""='"", arr[a + 2], ""' 指令无效. <"", arr[a]);
					arr[a + 1] = arr[a + 2] = string.Empty;
				}}
			}}
			if (arr.Length > 0) tplcode = string.Join(string.Empty, arr);
		}}
		return tplcode;
	}}
	#endregion
	#region Complie
	//private static string _db_dll_location;
	private static IBmwNetOutput Complie(string cscode, string typename) {{
		//// 1.CSharpCodePrivoder
		//CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
		//// 3.CompilerParameters
		//CompilerParameters objCompilerParameters = new CompilerParameters();
		//objCompilerParameters.ReferencedAssemblies.Add(""System.dll"");
		//objCompilerParameters.GenerateExecutable = false;
		//objCompilerParameters.GenerateInMemory = true;

		//if (string.IsNullOrEmpty(_db_dll_location)) _db_dll_location = Type.GetType(""{0}.DAL.SqlHelper, {0}.db"").Assembly.Location;
		//objCompilerParameters.ReferencedAssemblies.Add(Assembly.GetEntryAssembly().Location);
		//objCompilerParameters.ReferencedAssemblies.Add(_db_dll_location);
		//objCompilerParameters.ReferencedAssemblies.Add(""System.Core.dll"");
		//objCompilerParameters.ReferencedAssemblies.Add(""Microsoft.CSharp.dll"");
		//// 4.CompilerResults
		//CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, cscode);

		//if (cr.Errors.HasErrors) {{
		//	StringBuilder sb = new StringBuilder();
		//	sb.Append(""编译错误："");
		//	int undefined_idx = 0;
		//	int undefined_cout = 0;
		//	Dictionary<string, bool> undefined_exists = new Dictionary<string, bool>();
		//	foreach (CompilerError err in cr.Errors) {{
		//		sb.Append(err.ErrorText + "" 在第"" + err.Line + ""行\r\n"");
		//		if (err.ErrorNumber == ""CS0103"") {{
		//			//如果未定义变量，则自定义变量后重新编译
		//			Match m = _reg_complie_undefined.Match(err.ErrorText);
		//			if (m.Success) {{
		//				string undefined_name = m.Groups[2].Value;
		//				if (undefined_exists.ContainsKey(undefined_name) == false) {{
		//					if (undefined_idx <= 0) undefined_idx = cscode.IndexOf(""BmwNet.BmwNetPrint Print = print;"") + 33;
		//					cscode = cscode.Insert(undefined_idx, string.Format(""\r\n\t\t\tdynamic {{0}} = oPtIoNs[\""{{0}}\""];"", undefined_name));
		//					undefined_exists.Add(undefined_name, true);
		//				}}
		//				undefined_cout++;
		//			}} else {{
		//				sb.AppendFormat(""错误编号：CS0103，但是 _reg_undefined({{0}}) 匹配不到 ErrorText({{1}})\r\n"", _reg_complie_undefined, err.ErrorText);
		//			}}
		//		}}
		//	}}
		//	if (cr.Errors.Count == undefined_cout) {{
		//		return Complie(cscode, typename);
		//	}} else {{
		//		sb.Append(cscode);
		//		throw new Exception(sb.ToString());
		//	}}
		//}} else {{
		//	object ret = cr.CompiledAssembly.CreateInstance(typename);
		//	return ret as IBmwNetOutput;
		//}}
		return null;
	}}
	#endregion

	#region Utils
	public class Utils {{
		public static string ReplaceSingleQuote(object exp) {{
			//将 ' 转换成 ""
			string exp2 = string.Concat(exp);
			int quote_pos = -1;
			while (true) {{
				int first_pos = quote_pos = exp2.IndexOf('\'', quote_pos + 1);
				if (quote_pos == -1) break;
				while (true) {{
					quote_pos = exp2.IndexOf('\'', quote_pos + 1);
					if (quote_pos == -1) break;
					int r_cout = 0;
					for (int p = 1; true; p++) {{
						if (exp2[quote_pos - p] == '\\') r_cout++;
						else break;
					}}
					if (r_cout % 2 == 0/* && quote_pos - first_pos > 2*/) {{
						string str1 = exp2.Substring(0, first_pos);
						string str2 = exp2.Substring(first_pos + 1, quote_pos - first_pos - 1);
						string str3 = exp2.Substring(quote_pos + 1);
						string str4 = str2.Replace(""\"""", ""\\\"""");
						quote_pos += str4.Length - str2.Length;
						exp2 = string.Concat(str1, ""\"""", str4, ""\"""", str3);
						break;
					}}
				}}
				if (quote_pos == -1) break;
			}}
			return exp2;
		}}
		public static string GetConstString(object obj) {{
			return string.Concat(obj)
				.Replace(""\\"", ""\\\\"")
				.Replace(""\"""", ""\\\"""")
				.Replace(""\r"", ""\\r"")
				.Replace(""\n"", ""\\n"");
		}}

		public static string TranslateUrl(string url) {{
			return TranslateUrl(url, null);
		}}
		private static object _ecd_lock = new object();
		public static string TranslateUrl(string url, string baseDir) {{
			if (string.IsNullOrEmpty(baseDir)) baseDir = AppContext.BaseDirectory + ""/"";
			if (string.IsNullOrEmpty(url)) return Path.GetDirectoryName(baseDir);
			if (url.StartsWith(""~/"")) url = url.Substring(1);
			if (url.StartsWith(""/"")) return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(baseDir), url.TrimStart('/')));
			if (url.StartsWith(""\\"")) return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(baseDir), url.TrimStart('\\')));
			if (url.IndexOf("":\\"") != -1) return url;
			return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(baseDir), url));
		}}

		public static string ReadTextFile(string path) {{
			byte[] bytes = ReadFile(path);
			return Encoding.UTF8.GetString(bytes).TrimStart((char)65279);
		}}

		public static byte[] ReadFile(string path) {{
			path = TranslateUrl(path);
			//if (File.Exists(path)) {{
			//string destFileName = Path.GetTempFileName();
			//File.Copy(path, destFileName, true);
			string destFileName = path;
			int read = 0;
			byte[] data = new byte[1024 * 8];
			using (MemoryStream ms = new MemoryStream()) {{
				using (FileStream fs = new FileStream(destFileName, FileMode.OpenOrCreate, FileAccess.Read)) {{
					do {{
						read = fs.Read(data, 0, data.Length);
						if (read <= 0) break;
						ms.Write(data, 0, read);
					}} while (true);
				}}
				//File.Delete(destFileName);
				data = ms.ToArray();
			}}
			return data;
			//}}
			//return new byte[] {{ }};
		}}
	}}
	#endregion
}}";
			#endregion
			public static readonly string Common_IniHelper_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

public class IniHelper {{
	private static Dictionary<string, object> _cache = new Dictionary<string, object>();
	private static Dictionary<string, FileSystemWatcher> _watcher = new Dictionary<string, FileSystemWatcher>();
	private static object _lock = new object();

	private static object loadAndCache(string path) {{
		path = BmwNet.Utils.TranslateUrl(path);
		object ret = null;
		if (!_cache.TryGetValue(path, out ret)) {{
			object value2 = LoadIniNotCache(path);
			string dir = Path.GetDirectoryName(path);
			string name = Path.GetFileName(path);
			FileSystemWatcher fsw = new FileSystemWatcher(dir, name);
			fsw.IncludeSubdirectories = false;
			fsw.Changed += watcher_handler;
			fsw.Renamed += watcher_handler;
			fsw.EnableRaisingEvents = false;
			lock (_lock) {{
				if (!_cache.TryGetValue(path, out ret)) {{
					_cache.Add(path, ret = value2);
					_watcher.Add(path, fsw);
					fsw.EnableRaisingEvents = true;
				}} else {{
					fsw.Dispose();
				}}
			}}
		}}
		return ret;
	}}
	private static void watcher_handler(object sender, FileSystemEventArgs e) {{
		lock (_lock) {{
			_cache.Remove(e.FullPath);
			FileSystemWatcher fsw = null;
			if (_watcher.TryGetValue(e.FullPath, out fsw)) {{
				fsw.EnableRaisingEvents = false;
				fsw.Dispose();
			}}
		}}
	}}

	public static Dictionary<string, NameValueCollection> LoadIni(string path) {{
		return loadAndCache(path) as Dictionary<string, NameValueCollection>;
	}}
	public static Dictionary<string, NameValueCollection> LoadIniNotCache(string path) {{
		Dictionary<string, NameValueCollection> ret = new Dictionary<string, NameValueCollection>();
		string[] lines = ReadTextFile(path).Split(new string[] {{ ""\n"" }}, StringSplitOptions.None);
		string key = """";
		foreach (string line2 in lines) {{
			string line = line2.Trim();
			int idx = line.IndexOf('#');
			if (idx != -1) line = line.Remove(idx);
			if (string.IsNullOrEmpty(line)) continue;

			Match m = Regex.Match(line, @""^\[([^\]]+)\]$"");
			if (m.Success) {{
				key = m.Groups[1].Value;
				continue;
			}}
			if (!ret.ContainsKey(key)) ret.Add(key, new NameValueCollection());
			string[] kv = line.Split(new char[] {{ '=' }}, 2);
			if (!string.IsNullOrEmpty(kv[0])) {{
				ret[key][kv[0]] = kv.Length > 1 ? kv[1] : null;
			}}
		}}
		return ret;
	}}

	public static string ReadTextFile(string path) {{
		byte[] bytes = ReadFile(path);
		return Encoding.UTF8.GetString(bytes).TrimStart((char)65279);
	}}
	public static byte[] ReadFile(string path) {{
		if (File.Exists(path)) {{
			string destFileName = Path.GetTempFileName();
			File.Copy(path, destFileName, true);
			int read = 0;
			byte[] data = new byte[1024];
			using (MemoryStream ms = new MemoryStream()) {{
				using (FileStream fs = new FileStream(destFileName, FileMode.OpenOrCreate, FileAccess.Read)) {{
					do {{
						read = fs.Read(data, 0, data.Length);
						if (read <= 0) break;
						ms.Write(data, 0, read);
					}} while (true);
				}}
				File.Delete(destFileName);
				data = ms.ToArray();
			}}
			return data;
		}}
		return new byte[] {{ }};
	}}
}}";
			#endregion
			public static readonly string Common_JSDecoder_cs =
			#region 内容太长已被收起
 @"using System;
using System.Text;
using System.Text.RegularExpressions;

public class JSDecoder {{
	private const byte STATE_COPY_INPUT = 100;
	private const byte STATE_READLEN = 101;
	private const byte STATE_DECODE = 102;
	private const byte STATE_UNESCAPE = 103;

	private static byte[] _pickEncoding;
	private static byte[] _rawData;
	private static byte[] _digits = new byte[123];
	private static byte[][] _transformed = new byte[3][];

	static JSDecoder() {{
		InitArrayData();
	}}

	private static void InitArrayData() {{
		_pickEncoding = new byte[] {{
			1, 2, 0, 1, 2, 0, 2, 0, 0, 2, 0, 2, 1, 0, 2, 0, 
			1, 0, 2, 0, 1, 1, 2, 0, 0, 2, 1, 0, 2, 0, 0, 2, 
			1, 1, 0, 2, 0, 2, 0, 1, 0, 1, 1, 2, 0, 1, 0, 2, 
			1, 0, 2, 0, 1, 1, 2, 0, 0, 1, 1, 2, 0, 1, 0, 2
		}};

		_rawData = new byte[] {{
			0x64,0x37,0x69, 0x50,0x7E,0x2C, 0x22,0x5A,0x65, 0x4A,0x45,0x72,
			0x61,0x3A,0x5B, 0x5E,0x79,0x66, 0x5D,0x59,0x75, 0x5B,0x27,0x4C,
			0x42,0x76,0x45, 0x60,0x63,0x76, 0x23,0x62,0x2A, 0x65,0x4D,0x43,
			0x5F,0x51,0x33, 0x7E,0x53,0x42, 0x4F,0x52,0x20, 0x52,0x20,0x63,
			0x7A,0x26,0x4A, 0x21,0x54,0x5A, 0x46,0x71,0x38, 0x20,0x2B,0x79,
			0x26,0x66,0x32, 0x63,0x2A,0x57, 0x2A,0x58,0x6C, 0x76,0x7F,0x2B,
			0x47,0x7B,0x46, 0x25,0x30,0x52, 0x2C,0x31,0x4F, 0x29,0x6C,0x3D,
			0x69,0x49,0x70, 0x3F,0x3F,0x3F, 0x27,0x78,0x7B, 0x3F,0x3F,0x3F,
			0x67,0x5F,0x51, 0x3F,0x3F,0x3F, 0x62,0x29,0x7A, 0x41,0x24,0x7E,
			0x5A,0x2F,0x3B, 0x66,0x39,0x47, 0x32,0x33,0x41, 0x73,0x6F,0x77,
			0x4D,0x21,0x56, 0x43,0x75,0x5F, 0x71,0x28,0x26, 0x39,0x42,0x78,
			0x7C,0x46,0x6E, 0x53,0x4A,0x64, 0x48,0x5C,0x74, 0x31,0x48,0x67,
			0x72,0x36,0x7D, 0x6E,0x4B,0x68, 0x70,0x7D,0x35, 0x49,0x5D,0x22,
			0x3F,0x6A,0x55, 0x4B,0x50,0x3A, 0x6A,0x69,0x60, 0x2E,0x23,0x6A,
			0x7F,0x09,0x71, 0x28,0x70,0x6F, 0x35,0x65,0x49, 0x7D,0x74,0x5C,
			0x24,0x2C,0x5D, 0x2D,0x77,0x27, 0x54,0x44,0x59, 0x37,0x3F,0x25,
			0x7B,0x6D,0x7C, 0x3D,0x7C,0x23, 0x6C,0x43,0x6D, 0x34,0x38,0x28,
			0x6D,0x5E,0x31, 0x4E,0x5B,0x39, 0x2B,0x6E,0x7F, 0x30,0x57,0x36,
			0x6F,0x4C,0x54, 0x74,0x34,0x34, 0x6B,0x72,0x62, 0x4C,0x25,0x4E,
			0x33,0x56,0x30, 0x56,0x73,0x5E, 0x3A,0x68,0x73, 0x78,0x55,0x09,
			0x57,0x47,0x4B, 0x77,0x32,0x61, 0x3B,0x35,0x24, 0x44,0x2E,0x4D,
			0x2F,0x64,0x6B, 0x59,0x4F,0x44, 0x45,0x3B,0x21, 0x5C,0x2D,0x37,
			0x68,0x41,0x53, 0x36,0x61,0x58, 0x58,0x7A,0x48, 0x79,0x22,0x2E,
			0x09,0x60,0x50, 0x75,0x6B,0x2D, 0x38,0x4E,0x29, 0x55,0x3D,0x3F
		}};

		for (byte i = 0; i < 3; i++) _transformed[i] = new byte[288];
		for (byte i = 31; i < 127; i++) for (byte j = 0; j < 3; j++) _transformed[j][_rawData[(i - 31) * 3 + j]] = i == 31 ? (byte)9 : i;

		for (byte i = 0; i < 26; i++) {{
			_digits[65 + i] = i;
			_digits[97 + i] = (byte)(i + 26);
		}}

		for (byte i = 0; i < 10; i++)
			_digits[48 + i] = (byte)(i + 52);

		_digits[43] = 62;
		_digits[47] = 63;
	}}

	private static string UnEscape(string s) {{
		string escapes = ""#&!*$"";
		string escaped = ""\r\n<>@"";

		if ((int)s.ToCharArray()[0] > 126) return s;
		if (escapes.IndexOf(s) != -1) return escaped.Substring(escapes.IndexOf(s), 1);
		return ""?"";
	}}

	private static int DecodeBase64(string s) {{
		int val = 0;
		byte[] bs = Encoding.UTF8.GetBytes(s);

		val += ((int)_digits[bs[0]] << 2);
		val += (_digits[bs[1]] >> 4);
		val += (_digits[bs[1]] & 0xf) << 12;
		val += ((_digits[bs[2]] >> 2) << 8);
		val += ((_digits[bs[2]] & 0x3) << 22);
		val += (_digits[bs[3]] << 16);
		return val;
	}}

	public static string Decode(string encodingString) {{
		string marker = ""#@~^"";
		int stringIndex = 0;
		int scriptIndex = -1;
		int unEncodingIndex = 0;
		string strChar = """";
		string getCodeString = """";
		int unEncodinglength = 0;
		int state = STATE_COPY_INPUT;
		string unEncodingString = """";

		try {{
			while (state != 0) {{
				switch (state) {{
					case STATE_COPY_INPUT:

						scriptIndex = encodingString.IndexOf(marker, stringIndex);
						if (scriptIndex != -1) {{
							unEncodingString += encodingString.Substring(stringIndex, scriptIndex);
							scriptIndex += marker.Length;
							state = STATE_READLEN;
						}} else {{
							stringIndex = stringIndex == 0 ? 0 : stringIndex;
							unEncodingString += encodingString.Substring(stringIndex);
							state = 0;
						}}
						break;
					case STATE_READLEN:

						getCodeString = encodingString.Substring(scriptIndex, 6);
						unEncodinglength = DecodeBase64(getCodeString);
						scriptIndex += 8;
						state = STATE_DECODE;
						break;
					case STATE_DECODE:

						if (unEncodinglength == 0) {{
							stringIndex = scriptIndex + ""DQgAAA==^#~@"".Length;
							unEncodingIndex = 0;
							state = STATE_COPY_INPUT;
						}} else {{
							strChar = encodingString.Substring(scriptIndex, 1);
							if (strChar == ""@"") state = STATE_UNESCAPE;
							else {{
								int b = (int)strChar.ToCharArray()[0];
								if (b < 0xFF) {{
									unEncodingString += (char)_transformed[_pickEncoding[unEncodingIndex % 64]][b];
									unEncodingIndex++;
								}} else {{
									unEncodingString += strChar;
								}}
								scriptIndex++;
								unEncodinglength--;
							}}
						}}
						break;
					case STATE_UNESCAPE:

						unEncodingString += UnEscape(encodingString.Substring(++scriptIndex, 1));
						scriptIndex++;
						unEncodinglength -= 2;
						unEncodingIndex++;
						state = STATE_DECODE;
						break;
				}}
			}}
		}} catch {{ }}
		string Pattern;
		Pattern = ""(JScript|VBscript).encode"";
		unEncodingString = Regex.Replace(unEncodingString, Pattern, """", RegexOptions.IgnoreCase);
		return unEncodingString;
	}}
}}";
			#endregion
			public static readonly string Common_Lib_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;
using System.Threading;
using Microsoft.VisualBasic;

public delegate void AnonymousHandler();

/// <summary>
/// 常用函数库
/// </summary>
public class Lib {{

	/// <summary>
	/// 当前程序类型是否为 Web Application
	/// </summary>

	public static string HtmlEncode(object input) {{ return WebUtility.HtmlEncode(string.Concat(input)); }}
	public static string HtmlDecode(object input) {{ return WebUtility.HtmlDecode(string.Concat(input)); }}
	public static string UrlEncode(object input) {{ return WebUtility.UrlEncode(string.Concat(input)); }}
	public static string UrlDecode(object input) {{ return WebUtility.UrlDecode(string.Concat(input)); }}

	public static string JSDecode(string input) {{ return JSDecoder.Decode(input); }}

	#region 弥补 String.PadRight 和 String.PadLeft 对中文的 Bug
	public static string PadRight(object text, int length) {{ return PadRightLeft(text, length, ' ', true); }}
	public static string PadRight(object text, char paddingChar, int length) {{ return PadRightLeft(text, length, paddingChar, true); }}
	public static string PadLeft(object text, int length) {{ return PadRightLeft(text, length, ' ', false); }}
	public static string PadLeft(object text, char paddingChar, int length) {{ return PadRightLeft(text, length, paddingChar, false); }}
	static string PadRightLeft(object text, int length, char paddingChar, bool isRight) {{
		string str = string.Concat(text);
		int len2 = Encoding.UTF8.GetBytes(str).Length;
		for (int a = 0; a < length - len2; a++) if (isRight) str += paddingChar; else str = paddingChar + str;
		return str;
	}}
	#endregion

	#region 序列化/反序列化(二进制)
	public static byte[] Serialize(object obj) {{
		using (MemoryStream ms = new MemoryStream()) {{
			DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(object));
			js.WriteObject(ms, obj);
			return ms.ToArray();
		}}
	}}
	public static object Deserialize(byte[] stream) {{
		using (MemoryStream ms = new MemoryStream(stream)) {{
			DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(object));
			return js.ReadObject(ms);
		}}
	}}
	#endregion

	/// <summary>
	/// 重试某过程 maxError 次，直到成功或失败
	/// </summary>
	/// <param name=""handler"">托管函数</param>
	/// <param name=""maxError"">允许失败的次数</param>
	/// <returns>如果执行成功，则返回 null, 否则返回该错误对象</returns>
	public static Exception Trys(AnonymousHandler handler, int maxError) {{
		if (handler != null) {{
			Exception ex = null;
			for (int a = 0; a < maxError; a++) {{
				try {{
					handler();
					return null;
				}} catch (Exception e) {{
					ex = e;
				}}
			}}
			return ex;
		}}
		return null;
	}}

	/// <summary>
	/// 延迟 milliSecond 毫秒后执行 handler，与 javascript 里的 setTimeout 相似
	/// </summary>
	/// <param name=""handler"">托管函数</param>
	/// <param name=""milliSecond"">毫秒</param>
	public static void SetTimeout(AnonymousHandler handler, int milliSecond) {{
		new Timer((a) => {{
			try {{
				handler();
			}} catch {{ }}
		}}, null, milliSecond, milliSecond);
	}}

	/// <summary>
	/// 将服务器端数据转换成安全的JS字符串
	/// </summary>
	/// <param name=""input"">一个服务器端变量或字符串</param>
	/// <returns>安全的JS字符串</returns>
	public static string GetJsString(object input) {{
		if (input == null) return string.Empty;
		return string.Concat(input).Replace(""\\"", ""\\\\"").Replace(""\r"", ""\\r"").Replace(""\n"", ""\\n"").Replace(""'"", ""\\'"");
	}}

	static Dictionary<string, Type> _InvokeMethod_cache_type = new Dictionary<string, Type>();
	static object _InvokeMethod_cache_type_lock = new object();
	public static object InvokeMethod(string typeName, string method, params object[] parms) {{
		Type type;
		if (!_InvokeMethod_cache_type.TryGetValue(typeName, out type)) {{
			type = System.Type.GetType(typeName);
			lock (_InvokeMethod_cache_type_lock) {{
				if (!_InvokeMethod_cache_type.TryGetValue(typeName, out type))
					_InvokeMethod_cache_type.Add(typeName, type);
			}}
		}}
		return type.GetMethod(method).Invoke(null, parms);
	}}

	/// <summary>
	/// 获取对象属性
	/// </summary>
	/// <param name=""obj"">对象</param>
	/// <param name=""property"">属性，此属性可为多级属性，如：newsInfo.newsClassInfo...</param>
	/// <returns>对象的（子）属性</returns>
	public static object EvaluateValue(object obj, string property) {{
		if (obj == null) return null;
		string prop = property;
		object ret = string.Empty;
		if (property.Contains(""."")) {{
			prop = property.Substring(0, property.IndexOf("".""));
			PropertyInfo propa = EvaluateValue_GetProperty(obj, prop);
			if (propa != null) {{
				object obja = propa.GetValue(obj, null);
				ret = EvaluateValue(obja, property.Substring(property.IndexOf(""."") + 1));
			}}
		}} else {{
			PropertyInfo propa = EvaluateValue_GetProperty(obj, prop);
			if (propa != null) {{
				ret = propa.GetValue(obj, null);
			}}
		}}
		return ret;
	}}
	private static PropertyInfo EvaluateValue_GetProperty(object obj, string property) {{
		if (obj == null) return null;
		Type type = obj.GetType();
		PropertyInfo ret = type.GetProperty(property);
		if (ret == null) {{
			PropertyInfo[] pis = type.GetProperties();
			foreach (PropertyInfo pi in pis) {{
				if (string.Compare(pi.Name, property, true) == 0) {{
					ret = pi;
					break;
				}}
			}}
		}}
		return ret;
	}}

	/// <summary>
	/// (安全转换)对象/值转换类型
	/// </summary>
	/// <typeparam name=""T"">转换后的类型</typeparam>
	/// <param name=""input"">转换的对象</param>
	/// <returns>转换后的对象/值</returns>
	public static T ConvertTo<T>(object input) {{
		return ConvertTo<T>(input, default(T));
	}}
	public static T ConvertTo<T>(object input, T defaultValue) {{
		if (input == null) return defaultValue;
		object obj = null;

		if (defaultValue is System.Byte ||
			defaultValue is System.Decimal ||

			defaultValue is System.Int16 ||
			defaultValue is System.Int32 ||
			defaultValue is System.Int64 ||
			defaultValue is System.SByte ||
			defaultValue is System.Single ||

			defaultValue is System.UInt16 ||
			defaultValue is System.UInt32 ||
			defaultValue is System.UInt64) {{
			decimal trydec = 0;
			if (decimal.TryParse(string.Concat(input), out trydec)) obj = trydec;
		}} else {{
			if (defaultValue is System.DateTime) {{
				DateTime trydt = DateTime.Now;
				if (DateTime.TryParse(string.Concat(input), out trydt)) obj = trydt;
			}} else {{
				if (defaultValue is System.Boolean) {{
					bool trybool = false;
					if (bool.TryParse(string.Concat(input), out trybool)) obj = trybool;
				}} else {{
					if (defaultValue is System.Double) {{
						double trydb = 0;
						if (double.TryParse(string.Concat(input), out trydb)) obj = trydb;
					}} else {{
						obj = input;
					}}
				}}
			}}
		}}

		try {{
			if (obj != null) return (T)Convert.ChangeType(obj, typeof(T));
		}} catch {{ }}

		return defaultValue;
	}}
}}";
			#endregion

			public static readonly string Common_WinFormClass_Socket_BaseSocket_cs =
			#region 内容太长已被收起
 @"using System;
using System.IO;
using System.IO.Compression;
using System.Globalization;
using System.Text;
using System.Threading;

public class BaseSocket {{

	public static byte[] Read(Stream stream, byte[] end) {{
		using (MemoryStream ms = new MemoryStream()) {{
			byte[] data = new byte[1];
			int bytes = data.Length;
			while (bytes > 0 && BaseSocket.findBytes(ms.ToArray(), end, 0) == -1) {{
				bytes = stream.Read(data, 0, data.Length);
				ms.Write(data, 0, data.Length);
			}}
			return ms.ToArray();
		}}
	}}
	protected void Write(Stream stream, SocketMessager messager) {{
		using (MemoryStream ms = new MemoryStream()) {{
			byte[] buff = Encoding.UTF8.GetBytes(messager.GetCanParseString());
			ms.Write(buff, 0, buff.Length);
			if (messager.Arg != null) {{
				using(MemoryStream msArg = new MemoryStream(Lib.Serialize(messager.Arg))) {{
					using (DeflateStream ds = new DeflateStream(msArg, CompressionMode.Compress)) {{
						using (MemoryStream msBuf = new MemoryStream()) {{
							ds.CopyTo(msBuf);
							buff = msBuf.ToArray();
							ms.Write(buff, 0, buff.Length);
						}}
					}}
				}}
			}}
			this.Write(stream, ms.ToArray());
		}}
		
	}}
	private void Write(Stream stream, byte[] data) {{
		using (MemoryStream ms = new MemoryStream()) {{
			byte[] buff = Encoding.UTF8.GetBytes(Convert.ToString(data.Length + 8, 16).PadRight(8));
			ms.Write(buff, 0, buff.Length);
			ms.Write(data, 0, data.Length);
			buff = ms.ToArray();
			stream.Write(buff, 0, buff.Length);
		}}
	}}

	protected SocketMessager Read(Stream stream) {{
		byte[] data = new byte[8];
		int bytes = 0;
		int overs = data.Length;
		string size = string.Empty;
		while (overs > 0) {{
			bytes = stream.Read(data, 0, overs);
			overs -= bytes;
			size += Encoding.UTF8.GetString(data, 0, bytes);
		}}
		
		if (int.TryParse(size, NumberStyles.HexNumber, null, out overs) == false) {{
			return null;
		}}
		overs -= data.Length;
		using (MemoryStream ms = new MemoryStream()) {{
			data = new Byte[1024];
			while (overs > 0) {{
				bytes = stream.Read(data, 0, overs < data.Length ? overs : data.Length);
				overs -= bytes;
				ms.Write(data, 0, bytes);
			}}
			data = ms.ToArray();
		}}
		return SocketMessager.Parse(data);
	}}

	public static int findBytes(byte[] source, byte[] find, int startIndex) {{
		if (find == null) return -1;
		if (find.Length == 0) return -1;
		if (source == null) return -1;
		if (source.Length == 0) return -1;
		if (startIndex < 0) startIndex = 0;
		int idx = -1, idx2 = startIndex - 1;
		do {{
			idx2 = idx = Array.FindIndex<byte>(source, Math.Min(idx2 + 1, source.Length), delegate(byte b) {{
				return b == find[0];
			}});
			if (idx2 != -1) {{
				for (int a = 1; a < find.Length; a++) {{
					if (++idx2 >= source.Length || source[idx2] != find[a]) {{
						idx = -1;
						break;
					}}
				}}
				if (idx != -1) break;
			}}
		}} while (idx2 != -1);
		return idx;
	}}

	public static string formatKBit(int kbit) {{
		double mb = kbit;
		string unt = ""bit"";
		if (mb >= 8) {{
			unt = ""Byte"";
			mb = mb / 8;
			if (mb >= 1024) {{
				unt = ""KB"";
				mb = kbit / 1024;
				if (mb >= 1024) {{
					unt = ""MB"";
					mb = mb / 1024;
					if (mb >= 1024) {{
						unt = ""G"";
						mb = mb / 1024;
					}}
				}}
			}}
		}}
		return Math.Round(mb, 1) + unt;
	}}
}}

public class SocketMessager {{
	private static int _identity;
	internal static readonly SocketMessager SYS_TEST_LINK = new SocketMessager(""\0"");
	internal static readonly SocketMessager SYS_HELLO_WELCOME = new SocketMessager(""Hello, Welcome!"");
	internal static readonly SocketMessager SYS_ACCESS_DENIED = new SocketMessager(""Access Denied."");

	private int _id;
	private string _action;
	private string _permission;
	private DateTime _remoteTime;
	private object _arg;
	private Exception _exception;

	public SocketMessager(string action)
		: this(action, null, null) {{
	}}
	public SocketMessager(string action, object arg)
		: this(action, null, arg) {{
	}}
	public SocketMessager(string action, string permission, object arg) {{
		this._id = Interlocked.Increment(ref _identity);
		this._action = action == null ? string.Empty : action;
		this._permission = permission == null ? string.Empty : permission;
		this._arg = arg;
		this._remoteTime = DateTime.Now;
	}}

	public override string ToString() {{
		return
			this._remoteTime.ToString(""yyyy-MM-dd HH:mm:ss"") + ""\t"" +
			this._id + ""\t"" +
			this._action.Replace(""\t"", ""\\t"") + ""\t"" +
			this._permission.Replace(""\t"", ""\\t"") + ""\t"" +
			this._arg;
	}}

	public string GetCanParseString() {{
		if (string.Compare(this._action, SocketMessager.SYS_TEST_LINK.Action) == 0) {{
			return this.Action;
		}} else if (
			string.Compare(this._action, SocketMessager.SYS_HELLO_WELCOME.Action) == 0 ||
			string.Compare(this._action, SocketMessager.SYS_ACCESS_DENIED.Action) == 0) {{
			return
				this._id + ""\t"" +
				this.Action + ""\r\n"";
		}} else {{
			return
				this._id + ""\t"" +
				this._action.Replace(""\\"", ""\\\\"").Replace(""\t"", ""\\t"").Replace(""\r\n"", ""\\n"") + ""\t"" +
				this._permission.Replace(""\\"", ""\\\\"").Replace(""\t"", ""\\t"").Replace(""\r\n"", ""\\n"") + ""\t"" +
				this._remoteTime.ToString(""yyyy-MM-dd HH:mm:ss"") + ""\r\n"";
		}}
	}}

	public static SocketMessager Parse(byte[] data) {{
		if (data == null) return new SocketMessager(""NULL"");
		if (data.Length == 1 && data[0] == 0) return SocketMessager.SYS_TEST_LINK;
		int idx = BaseSocket.findBytes(data, new byte[] {{ 13, 10 }}, 0);
		string text = Encoding.UTF8.GetString(data, 0, idx);
		string[] loc1 = text.Split(new string[] {{ ""\t"" }}, 4, StringSplitOptions.None);
		string loc2 = loc1[0];
		string loc3 = loc1.Length > 1 ? loc1[1].Replace(""\\\\"", ""\\"").Replace(""\\t"", ""\t"").Replace(""\\n"", ""\r\n"") : null;
		string loc4 = loc1.Length > 2 ? loc1[2].Replace(""\\\\"", ""\\"").Replace(""\\t"", ""\t"").Replace(""\\n"", ""\r\n"") : null;
		string loc5 = loc1.Length > 3 ? loc1[3] : null;
		SocketMessager messager;
		using (MemoryStream ms = new MemoryStream()) {{
			ms.Write(data, idx + 2, data.Length - idx - 2);
			using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Decompress)) {{
				using (MemoryStream msOut = new MemoryStream()) {{
					ds.CopyTo(msOut);
					messager = new SocketMessager(loc3, loc4, ms.Length > 0 ? Lib.Deserialize(msOut.ToArray()) : null);
				}}
			}}
		}}
		if (int.TryParse(loc2, out idx)) messager._id = idx;
		if (!string.IsNullOrEmpty(loc5)) DateTime.TryParse(loc5, out messager._remoteTime);
		if (messager._arg is Exception) messager._exception = messager._arg as Exception;
		return messager;
	}}

	/// <summary>
	/// 消息ID，每个一消息ID都是惟一的，同步发送时用
	/// </summary>
	public int Id {{
		get {{ return _id; }}
		set {{ _id = value; }}
	}}
	public string Action {{
		get {{ return _action; }}
	}}
	public string Permission {{
		get {{ return _permission; }}
	}}
	public DateTime RemoteTime {{
		get {{ return _remoteTime; }}
	}}
	public object Arg {{
		get {{ return _arg; }}
	}}
	public Exception Exception {{
		get {{ return _exception; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_Socket_ClientSocket_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

public class ClientSocket : BaseSocket, IDisposable {{

	private bool _isDisposed;
	private TcpClient _tcpClient;
	private Thread _thread;
	private bool _running;
	private int _receives;
	private int _errors;
	private object _errors_lock = new object();
	private object _write_lock = new object();
	private Dictionary<int, SyncReceive> _receiveHandlers = new Dictionary<int, SyncReceive>();
	private object _receiveHandlers_lock = new object();
	private DateTime _lastActive;
	public event ClientSocketClosedEventHandler Closed;
	public event ClientSocketReceiveEventHandler Receive;
	public event ClientSocketErrorEventHandler Error;

	private WorkQueue _receiveWQ;
	private WorkQueue _receiveSyncWQ;

	public void Connect(string hostname, int port) {{
		if (this._isDisposed == false && this._running == false) {{
			this._running = true;
			try {{
				this._tcpClient = new TcpClient();
				this._tcpClient.ConnectAsync(hostname, port);
				this._receiveWQ = new WorkQueue();
				this._receiveSyncWQ = new WorkQueue();
			}} catch (Exception ex) {{
				this._running = false;
				this.OnError(ex);
				this.OnClosed();
				return;
			}}
			this._receives = 0;
			this._errors = 0;
			this._lastActive = DateTime.Now;
			this._thread = new Thread(delegate() {{
				while (this._running) {{
					try {{
						NetworkStream ns = this._tcpClient.GetStream();
						ns.ReadTimeout = 1000 * 20;
						if (ns.DataAvailable) {{
							SocketMessager messager = base.Read(ns);
							if (string.Compare(messager.Action, SocketMessager.SYS_TEST_LINK.Action) == 0) {{
							}} else if (this._receives == 0 &&
								string.Compare(messager.Action, SocketMessager.SYS_HELLO_WELCOME.Action) == 0) {{
								this._receives++;
								this.Write(messager);
							}} else if (string.Compare(messager.Action, SocketMessager.SYS_ACCESS_DENIED.Action) == 0) {{
								throw new Exception(SocketMessager.SYS_ACCESS_DENIED.Action);
							}} else {{
								ClientSocketReceiveEventArgs e = new ClientSocketReceiveEventArgs(this._receives++, messager);
								SyncReceive receive = null;

								if (this._receiveHandlers.TryGetValue(messager.Id, out receive)) {{
									this._receiveSyncWQ.Enqueue(delegate() {{
										try {{
											receive.ReceiveHandler(this, e);
										}} catch (Exception ex) {{
											this.OnError(ex);
										}} finally {{
											receive.Wait.Set();
										}}
									}});
								}} else if (this.Receive != null) {{
									this._receiveWQ.Enqueue(delegate() {{
										this.OnReceive(e);
									}});
								}}
							}}
							this._lastActive = DateTime.Now;
						}} else {{
							TimeSpan ts = DateTime.Now - _lastActive;
							if (ts.TotalSeconds > 3) {{
								this.Write(SocketMessager.SYS_TEST_LINK);
							}}
						}}
						if (!ns.DataAvailable) Thread.CurrentThread.Join(1);
					}} catch (Exception ex) {{
						this._running = false;
						this.OnError(ex);
					}}
				}}
				this.Close();
				this.OnClosed();
			}});
			this._thread.Start();
		}}
	}}

	public void Close() {{
		if (this._tcpClient != null) {{
			this._tcpClient.Dispose();
			this._tcpClient = null;
		}}
		int[] keys = new int[this._receiveHandlers.Count];
		try {{
			this._receiveHandlers.Keys.CopyTo(keys, 0);
		}} catch {{
			lock (this._receiveHandlers_lock) {{
				keys = new int[this._receiveHandlers.Count];
				this._receiveHandlers.Keys.CopyTo(keys, 0);
			}}
		}}
		foreach (int key in keys) {{
			SyncReceive receiveHandler = null;
			if (this._receiveHandlers.TryGetValue(key, out receiveHandler)) {{
				receiveHandler.Wait.Set();
			}}
		}}
		lock (this._receiveHandlers_lock) {{
			this._receiveHandlers.Clear();
		}}
	}}

	public void Write(SocketMessager messager) {{
		this.Write(messager, null, TimeSpan.Zero);
	}}
	public void Write(SocketMessager messager, ClientSocketReceiveEventHandler receiveHandler) {{
		this.Write(messager, receiveHandler, TimeSpan.FromSeconds(20));
	}}
	public void Write(SocketMessager messager, ClientSocketReceiveEventHandler receiveHandler, TimeSpan timeout) {{
		SyncReceive syncReceive = null;
		try {{
			if (receiveHandler != null) {{
				syncReceive = new SyncReceive(receiveHandler);
				lock (this._receiveHandlers_lock) {{
					if (!this._receiveHandlers.ContainsKey(messager.Id)) {{
						this._receiveHandlers.Add(messager.Id, syncReceive);
					}} else {{
						this._receiveHandlers[messager.Id] = syncReceive;
					}}
				}}
			}}
			lock (_write_lock) {{
				NetworkStream ns = this._tcpClient.GetStream();
				base.Write(ns, messager);
			}}
			this._lastActive = DateTime.Now;
			if (syncReceive != null) {{
				syncReceive.Wait.Reset();
				syncReceive.Wait.WaitOne(timeout);
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}} catch (Exception ex) {{
			this._running = false;
			this.OnError(ex);
			if (syncReceive != null) {{
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}}
	}}

	protected virtual void OnClosed(EventArgs e) {{
		if (this.Closed != null) {{
			new Thread(delegate() {{
				try {{
					this.Closed(this, e);
				}} catch (Exception ex) {{
					this.OnError(ex);
				}}
			}}).Start();
		}}
	}}
	protected void OnClosed() {{
		this.OnClosed(new EventArgs());
	}}

	protected virtual void OnReceive(ClientSocketReceiveEventArgs e) {{
		if (this.Receive != null) {{
			try {{
				this.Receive(this, e);
			}} catch (Exception ex) {{
				this.OnError(ex);
			}}
		}}
	}}

	protected virtual void OnError(ClientSocketErrorEventArgs e) {{
		if (this.Error != null) {{
			this.Error(this, e);
		}}
	}}
	protected void OnError(Exception ex) {{
		int errors = 0;
		lock (this._errors_lock) {{
			errors = ++this._errors;
		}}
		ClientSocketErrorEventArgs e = new ClientSocketErrorEventArgs(ex, errors);
		this.OnError(e);
	}}

	public bool Running {{
		get {{ return this._running; }}
	}}

	class SyncReceive : IDisposable {{
		private ClientSocketReceiveEventHandler _receiveHandler;
		private ManualResetEvent _wait;

		public SyncReceive(ClientSocketReceiveEventHandler receiveHandler) {{
			this._receiveHandler = receiveHandler;
			this._wait = new ManualResetEvent(false);
		}}

		public ClientSocketReceiveEventHandler ReceiveHandler {{
			get {{ return _receiveHandler; }}
		}}
		public ManualResetEvent Wait {{
			get {{ return _wait; }}
		}}

		#region IDisposable 成员

		public void Dispose() {{
			this._wait.Set();
		}}

		#endregion
	}}

	#region IDisposable 成员

	public void Dispose() {{
		this._isDisposed = true;
		this.Close();
	}}

	#endregion
}}

public delegate void ClientSocketClosedEventHandler(object sender, EventArgs e);
public delegate void ClientSocketErrorEventHandler(object sender, ClientSocketErrorEventArgs e);
public delegate void ClientSocketReceiveEventHandler(object sender, ClientSocketReceiveEventArgs e);

public class ClientSocketErrorEventArgs : EventArgs {{

	private int _errors;
	private Exception _exception;

	public ClientSocketErrorEventArgs(Exception exception, int errors) {{
		this._exception = exception;
		this._errors = errors;
	}}

	public int Errors {{
		get {{ return _errors; }}
	}}
	public Exception Exception {{
		get {{ return _exception; }}
	}}
}}

public class ClientSocketReceiveEventArgs : EventArgs {{

	private int _receives;
	private SocketMessager _messager;

	public ClientSocketReceiveEventArgs(int receives, SocketMessager messager) {{
		this._receives = receives;
		this._messager = messager;
	}}

	public int Receives {{
		get {{ return _receives; }}
	}}
	public SocketMessager Messager {{
		get {{ return _messager; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_Socket_ServerSocket_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ServerSocket : IDisposable {{

	private TcpListener _tcpListener;
	private Thread _tcpListenerThread;
	private Dictionary<int, AcceptSocket> _clients = new Dictionary<int, AcceptSocket>();
	private object _clients_lock = new object();
	private int _id = 1;
	private int _port;
	private bool _running;
	private ManualResetEvent _stopWait;
	public event ServerSocketAcceptedEventHandler Accepted;
	public event ServerSocketClosedEventHandler Closed;
	public event ServerSocketReceiveEventHandler Receive;
	public event ServerSocketErrorEventHandler Error;

	private WorkQueue _acceptWQ;
	internal WorkQueue _receiveWQ;
	internal WorkQueue _receiveSyncWQ;
	private WorkQueue _writeWQ;

	public ServerSocket(int port) {{
		this._port = port;
	}}

	public void Start() {{
		if (this._running == false) {{
			this._running = true;
			try {{
				this._tcpListener = new TcpListener(IPAddress.Any, this._port);
				this._tcpListener.Start();
				this._acceptWQ = new WorkQueue();
				this._receiveWQ = new WorkQueue();
				this._receiveSyncWQ = new WorkQueue();
				this._writeWQ = new WorkQueue();
			}} catch (Exception ex) {{
				this._running = false;
				this.OnError(ex);
				return;
			}}
			this._tcpListenerThread = new Thread(delegate() {{
				while (this._running) {{
					try {{
						TcpClient tcpClient = this._tcpListener.AcceptTcpClientAsync().Result;
						this._acceptWQ.Enqueue(delegate() {{
							try {{
								AcceptSocket acceptSocket = new AcceptSocket(this, tcpClient, this._id);
								this.OnAccepted(acceptSocket);
							}} catch (Exception ex) {{
								this.OnError(ex);
							}}
						}});
					}} catch (Exception ex) {{
						this.OnError(ex);
					}}
				}}

				int[] keys = new int[this._clients.Count];
				try {{
					this._clients.Keys.CopyTo(keys, 0);
				}} catch {{
					lock (this._clients_lock) {{
						keys = new int[this._clients.Count];
						this._clients.Keys.CopyTo(keys, 0);
					}}
				}}
				foreach (int key in keys) {{
					AcceptSocket client = null;
					if (this._clients.TryGetValue(key, out client)) {{
						client.Close();
					}}
				}}
				if (this._acceptWQ != null) {{
					this._acceptWQ.Dispose();
				}}
				if (this._receiveWQ != null) {{
					this._receiveWQ.Dispose();
				}}
				if (this._receiveSyncWQ != null) {{
					this._receiveSyncWQ.Dispose();
				}}
				if (this._writeWQ != null) {{
					this._writeWQ.Dispose();
				}}
				this._clients.Clear();
				this._stopWait.Set();
			}});
			this._tcpListenerThread.Start();
		}}
	}}

	public void Stop() {{
		if (this._tcpListener != null) {{
			this._tcpListener.Stop();
		}}
		if (this._running == true) {{
			this._stopWait = new ManualResetEvent(false);
			this._stopWait.Reset();
			this._running = false;
			this._stopWait.WaitOne();
		}}
	}}

	internal void AccessDenied(AcceptSocket client) {{
		client.Write(SocketMessager.SYS_ACCESS_DENIED, delegate(object sender2, ServerSocketReceiveEventArgs e2) {{
		}}, TimeSpan.FromSeconds(1));
		client.Close();
	}}

	public void Write(SocketMessager messager) {{
		int[] keys = new int[this._clients.Count];
		try {{
			this._clients.Keys.CopyTo(keys, 0);
		}} catch {{
			lock (this._clients_lock) {{
				keys = new int[this._clients.Count];
				this._clients.Keys.CopyTo(keys, 0);
			}}
		}}
		foreach (int key in keys) {{
			AcceptSocket client = null;
			if (this._clients.TryGetValue(key, out client)) {{
				this._writeWQ.Enqueue(delegate() {{
					client.Write(messager);
				}});
			}}
		}}
	}}

	public AcceptSocket GetAcceptSocket(int id) {{
		AcceptSocket socket = null;
		this._clients.TryGetValue(id, out socket);
		return socket;
	}}

	internal void CloseClient(AcceptSocket client) {{
		this._clients.Remove(client.Id);
	}}

	protected virtual void OnAccepted(ServerSocketAcceptedEventArgs e) {{
		SocketMessager helloMessager = new SocketMessager(SocketMessager.SYS_HELLO_WELCOME.Action);
		e.AcceptSocket.Write(helloMessager, delegate(object sender2, ServerSocketReceiveEventArgs e2) {{
			if (e2.Messager.Id == helloMessager.Id &&
				string.Compare(e2.Messager.Action, helloMessager.Action) == 0) {{
				e.AcceptSocket._accepted = true;
			}}
		}}, TimeSpan.FromSeconds(2));
		if (e.AcceptSocket._accepted) {{
			if (this.Accepted != null) {{
				try {{
					this.Accepted(this, e);
				}} catch (Exception ex) {{
					this.OnError(ex);
				}}
			}}
		}} else {{
			e.AcceptSocket.AccessDenied();
		}}
	}}
	private void OnAccepted(AcceptSocket client) {{
		lock (_clients_lock) {{
			_clients.Add(this._id++, client);
		}}
		ServerSocketAcceptedEventArgs e = new ServerSocketAcceptedEventArgs(this._clients.Count, client);
		this.OnAccepted(e);
	}}

	protected virtual void OnClosed(ServerSocketClosedEventArgs e) {{
		if (this.Closed != null) {{
			this.Closed(this, e);
		}}
	}}
	internal void OnClosed(AcceptSocket client) {{
		ServerSocketClosedEventArgs e = new ServerSocketClosedEventArgs(this._clients.Count, client.Id);
		this.OnClosed(e);
	}}

	protected virtual void OnReceive(ServerSocketReceiveEventArgs e) {{
		if (this.Receive != null) {{
			this.Receive(this, e);
		}}
	}}
	internal void OnReceive2(ServerSocketReceiveEventArgs e) {{
		this.OnReceive(e);
	}}

	protected virtual void OnError(ServerSocketErrorEventArgs e) {{
		if (this.Error != null) {{
			this.Error(this, e);
		}}
	}}
	protected void OnError(Exception ex) {{
		ServerSocketErrorEventArgs e = new ServerSocketErrorEventArgs(-1, ex, null);
		this.OnError(e);
	}}
	internal void OnError2(ServerSocketErrorEventArgs e) {{
		this.OnError(e);
	}}

	#region IDisposable 成员

	public void Dispose() {{
		this.Stop();
	}}

	#endregion
}}

public class AcceptSocket : BaseSocket, IDisposable {{

	private ServerSocket _server;
	private TcpClient _tcpClient;
	private Thread _thread;
	private bool _running;
	private int _id;
	private int _receives;
	private int _errors;
	private object _errors_lock = new object();
	private object _write_lock = new object();
	private Dictionary<int, SyncReceive> _receiveHandlers = new Dictionary<int, SyncReceive>();
	private object _receiveHandlers_lock = new object();
	private DateTime _lastActive;
	internal bool _accepted;

	public AcceptSocket(ServerSocket server, TcpClient tcpClient, int id) {{
		this._running = true;
		this._id = id;
		this._server = server;
		this._tcpClient = tcpClient;
		this._lastActive = DateTime.Now;
		this._thread = new Thread(delegate() {{
			while (this._running) {{
				try {{
					NetworkStream ns = this._tcpClient.GetStream();
					ns.ReadTimeout = 1000 * 20;
					if (ns.DataAvailable) {{
						SocketMessager messager = base.Read(ns);
						if (string.Compare(messager.Action, SocketMessager.SYS_TEST_LINK.Action) != 0) {{
							ServerSocketReceiveEventArgs e = new ServerSocketReceiveEventArgs(this._receives++, messager, this);
							SyncReceive receive = null;

							if (this._receiveHandlers.TryGetValue(messager.Id, out receive)) {{
								this._server._receiveSyncWQ.Enqueue(delegate() {{
									try {{
										receive.ReceiveHandler(this, e);
									}} catch (Exception ex) {{
										this.OnError(ex);
									}} finally {{
										receive.Wait.Set();
									}}
								}});
							}} else {{
								this._server._receiveWQ.Enqueue(delegate() {{
									this.OnReceive(e);
								}});
							}}
						}}
						this._lastActive = DateTime.Now;
					}} else if (_accepted) {{
						TimeSpan ts = DateTime.Now - _lastActive;
						if (ts.TotalSeconds > 5) {{
							this.Write(SocketMessager.SYS_TEST_LINK);
						}}
					}}
					if (!ns.DataAvailable) Thread.CurrentThread.Join(1);
				}} catch (Exception ex) {{
					this._running = false;
					this.OnError(ex);
				}}
			}}
			this.Close();
			this.OnClosed();
		}});
		this._thread.Start();
	}}

	public void Close() {{
		this._running = false;
		if (this._tcpClient != null) {{
			this._tcpClient.Dispose();
			this._tcpClient = null;
		}}
		this._server.CloseClient(this);
		int[] keys = new int[this._receiveHandlers.Count];
		try {{
			this._receiveHandlers.Keys.CopyTo(keys, 0);
		}} catch {{
			lock (this._receiveHandlers_lock) {{
				keys = new int[this._receiveHandlers.Count];
				this._receiveHandlers.Keys.CopyTo(keys, 0);
			}}
		}}
		foreach (int key in keys) {{
			SyncReceive receiveHandler = null;
			if (this._receiveHandlers.TryGetValue(key, out receiveHandler)) {{
				receiveHandler.Wait.Set();
			}}
		}}
		lock (this._receiveHandlers_lock) {{
			this._receiveHandlers.Clear();
		}}
	}}

	public void Write(SocketMessager messager) {{
		this.Write(messager, null, TimeSpan.Zero);
	}}
	public void Write(SocketMessager messager, ServerSocketReceiveEventHandler receiveHandler) {{
		this.Write(messager, receiveHandler, TimeSpan.FromSeconds(20));
	}}
	public void Write(SocketMessager messager, ServerSocketReceiveEventHandler receiveHandler, TimeSpan timeout) {{
		SyncReceive syncReceive = null;
		try {{
			if (receiveHandler != null) {{
				syncReceive = new SyncReceive(receiveHandler);
				lock (this._receiveHandlers_lock) {{
					if (!this._receiveHandlers.ContainsKey(messager.Id)) {{
						this._receiveHandlers.Add(messager.Id, syncReceive);
					}} else {{
						this._receiveHandlers[messager.Id] = syncReceive;
					}}
				}}
			}}
			lock (_write_lock) {{
				NetworkStream ns = this._tcpClient.GetStream();
				base.Write(ns, messager);
			}}
			this._lastActive = DateTime.Now;
			if (syncReceive != null) {{
				syncReceive.Wait.Reset();
				syncReceive.Wait.WaitOne(timeout);
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}} catch (Exception ex) {{
			this._running = false;
			this.OnError(ex);
			if (syncReceive != null) {{
				syncReceive.Wait.Set();
				lock (this._receiveHandlers_lock) {{
					this._receiveHandlers.Remove(messager.Id);
				}}
			}}
		}}
	}}

	/// <summary>
	/// 拒绝访问，并关闭连接
	/// </summary>
	public void AccessDenied() {{
		this._server.AccessDenied(this);
	}}

	protected virtual void OnClosed() {{
		try {{
			this._server.OnClosed(this);
		}} catch (Exception ex) {{
			this.OnError(ex);
		}}
	}}

	protected virtual void OnReceive(ServerSocketReceiveEventArgs e) {{
		try {{
			this._server.OnReceive2(e);
		}} catch (Exception ex) {{
			this.OnError(ex);
		}}
	}}

	protected virtual void OnError(Exception ex) {{
		int errors = 0;
		lock (this._errors_lock) {{
			errors = ++this._errors;
		}}
		ServerSocketErrorEventArgs e = new ServerSocketErrorEventArgs(errors, ex, this);
		this._server.OnError2(e);
	}}

	public int Id {{
		get {{ return _id; }}
	}}

	class SyncReceive : IDisposable {{
		private ServerSocketReceiveEventHandler _receiveHandler;
		private ManualResetEvent _wait;

		public SyncReceive(ServerSocketReceiveEventHandler onReceive) {{
			this._receiveHandler = onReceive;
			this._wait = new ManualResetEvent(false);
		}}

		public ManualResetEvent Wait {{
			get {{ return _wait; }}
		}}
		public ServerSocketReceiveEventHandler ReceiveHandler {{
			get {{ return _receiveHandler; }}
		}}

		#region IDisposable 成员

		public void Dispose() {{
			this._wait.Set();
		}}

		#endregion
	}}

	#region IDisposable 成员

	void IDisposable.Dispose() {{
		this.Close();
	}}

	#endregion
}}

public delegate void ServerSocketClosedEventHandler(object sender, ServerSocketClosedEventArgs e);
public delegate void ServerSocketAcceptedEventHandler(object sender, ServerSocketAcceptedEventArgs e);
public delegate void ServerSocketErrorEventHandler(object sender, ServerSocketErrorEventArgs e);
public delegate void ServerSocketReceiveEventHandler(object sender, ServerSocketReceiveEventArgs e);

public class ServerSocketClosedEventArgs : EventArgs {{

	private int _accepts;
	private int _acceptSocketId;

	public ServerSocketClosedEventArgs(int accepts, int acceptSocketId) {{
		this._accepts = accepts;
		this._acceptSocketId = acceptSocketId;
	}}

	public int Accepts {{
		get {{ return _accepts; }}
	}}
	public int AcceptSocketId {{
		get {{ return _acceptSocketId; }}
	}}
}}

public class ServerSocketAcceptedEventArgs : EventArgs {{

	private int _accepts;
	private AcceptSocket _acceptSocket;

	public ServerSocketAcceptedEventArgs(int accepts, AcceptSocket acceptSocket) {{
		this._accepts = accepts;
		this._acceptSocket = acceptSocket;
	}}

	public int Accepts {{
		get {{ return _accepts; }}
	}}
	public AcceptSocket AcceptSocket {{
		get {{ return _acceptSocket; }}
	}}
}}

public class ServerSocketErrorEventArgs : EventArgs {{

	private int _errors;
	private Exception _exception;
	private AcceptSocket _acceptSocket;

	public ServerSocketErrorEventArgs(int errors, Exception exception, AcceptSocket acceptSocket) {{
		this._errors = errors;
		this._exception = exception;
		this._acceptSocket = acceptSocket;
	}}

	public int Errors {{
		get {{ return _errors; }}
	}}
	public Exception Exception {{
		get {{ return _exception; }}
	}}
	public AcceptSocket AcceptSocket {{
		get {{ return _acceptSocket; }}
	}}
}}

public class ServerSocketReceiveEventArgs : EventArgs {{

	private int _receives;
	private SocketMessager _messager;
	private AcceptSocket _acceptSocket;

	public ServerSocketReceiveEventArgs(int receives, SocketMessager messager, AcceptSocket acceptSocket) {{
		this._receives = receives;
		this._messager = messager;
		this._acceptSocket = acceptSocket;
	}}

	public int Receives {{
		get {{ return _receives; }}
	}}
	public SocketMessager Messager {{
		get {{ return _messager; }}
	}}
	public AcceptSocket AcceptSocket {{
		get {{ return _acceptSocket; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_Robot_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

/// <summary>
/// 作业调度器，一般运行在控制台
/// </summary>
public class Robot : IDisposable {{

	private string _def_path;
	private List<RobotDef> _robots;
	private Dictionary<string, RobotDef> _dic_robots = new Dictionary<string, RobotDef>();
	private object _robots_lock = new object();
	private FileSystemWatcher _defWatcher;
	public event RobotErrorHandler Error;
	public event RobotRunHandler Run;

	public Robot()
		: this(Path.Combine(AppContext.BaseDirectory, @""robot.txt"")) {{
	}}
	public Robot(string path) {{
		_def_path = path;
	}}

	public void Start() {{
		lock (_robots_lock) {{
			_dic_robots.Clear();
			if (_robots != null) {{
				for (int a = 0; a < _robots.Count; a++)
					_dic_robots.Add(_robots[a].Name, _robots[a]);
				_robots.Clear();
			}}
		}}
		if (!File.Exists(_def_path)) return;
		lock (_robots_lock) {{
			_robots = LoadDef();
			foreach (RobotDef bot in _robots)
				if (bot._timer == null) bot.RunNow();
		}}
		if (_defWatcher == null) {{
			_defWatcher = new FileSystemWatcher(Path.GetDirectoryName(_def_path), Path.GetFileName(_def_path));
			_defWatcher.Changed += delegate(object sender, FileSystemEventArgs e) {{
				_defWatcher.EnableRaisingEvents = false;
				if (_robots.Count > 0) {{
					Start();
				}}
				_defWatcher.EnableRaisingEvents = true;
			}};
			_defWatcher.EnableRaisingEvents = true;
		}}
	}}
	public void Stop() {{
		lock (_robots_lock) {{
			if (_robots != null) {{
				for (int a = 0; a < _robots.Count; a++)
					_robots[a].Dispose();
				_robots.Clear();
			}}
		}}
	}}

	#region IDisposable 成员

	public void Dispose() {{
		if (_defWatcher != null)
			_defWatcher.Dispose();
		Stop();
	}}

	#endregion

	public List<RobotDef> LoadDef() {{
		string defDoc = Encoding.UTF8.GetString(readFile(_def_path));
		return LoadDef(defDoc);
	}}
	public List<RobotDef> LoadDef(string defDoc) {{
		Dictionary<string, RobotDef> dic = new Dictionary<string, RobotDef>();
		string[] defs = defDoc.Split(new string[] {{ ""\r\n"" }}, StringSplitOptions.None);
		int row = 1;
		foreach (string def in defs) {{
			string loc1 = def.Trim();
			if (string.IsNullOrEmpty(loc1) || loc1[0] == 65279 || loc1[0] == ';' || loc1[0] == '#') continue;
			string pattern = @""([^\s]+)\s+(NONE|SEC|MIN|HOUR|DAY|RunOnDay|RunOnWeek|RunOnMonth)\s+([^\s]+)\s+([^\s]+)"";
			Match m = Regex.Match(loc1, pattern, RegexOptions.IgnoreCase);
			if (!m.Success) {{
				onError(new Exception(""Robot配置错误“"" + loc1 + ""”, 第"" + row + ""行""));
				continue;
			}}
			string name = m.Groups[1].Value.Trim('\t', ' ');
			RobotRunMode mode = getMode(m.Groups[2].Value.Trim('\t', ' '));
			string param = m.Groups[3].Value.Trim('\t', ' ');
			string runParam = m.Groups[4].Value.Trim('\t', ' ');
			if (dic.ContainsKey(name)) {{
				onError(new Exception(""Robot配置存在重复的名字“"" + name + ""”, 第"" + row + ""行""));
				continue;
			}}
			if (mode == RobotRunMode.NONE) continue;

			RobotDef rd = null;
			if (_dic_robots.ContainsKey(name)) {{
				rd = _dic_robots[name];
				rd.Update(mode, param, runParam);
				_dic_robots.Remove(name);
			}} else rd = new RobotDef(this, name, mode, param, runParam);
			if (rd.Interval < 0) {{
				onError(new Exception(""Robot配置参数错误“"" + def + ""”, 第"" + row + ""行""));
				continue;
			}}
			dic.Add(rd.Name, rd);
			row++;
		}}
		List<RobotDef> rds = new List<RobotDef>();
		foreach (RobotDef rd in dic.Values)
			rds.Add(rd);
		foreach (RobotDef stopBot in _dic_robots.Values)
			stopBot.Dispose();

		return rds;
	}}

	private void onError(Exception ex) {{
		onError(ex, null);
	}}
	internal void onError(Exception ex, RobotDef def) {{
		if (Error != null)
			Error(this, new RobotErrorEventArgs(ex, def));
	}}
	internal void onRun(RobotDef def) {{
		if (Run != null)
			Run(this, def);
	}}
	private byte[] readFile(string path) {{
		if (File.Exists(path)) {{
			string destFileName = Path.GetTempFileName();
			File.Copy(path, destFileName, true);
			int read = 0;
			byte[] data = new byte[1024];
			using (MemoryStream ms = new MemoryStream()) {{
				using (FileStream fs = new FileStream(destFileName, FileMode.OpenOrCreate, FileAccess.Read)) {{
					do {{
						read = fs.Read(data, 0, data.Length);
						if (read <= 0) break;
						ms.Write(data, 0, read);
					}} while (true);
				}}
				File.Delete(destFileName);
				data = ms.ToArray();
			}}
			return data;
		}}
		return new byte[] {{ }};
	}}
	private RobotRunMode getMode(string mode) {{
		mode = string.Concat(mode).ToUpper();
		switch (mode) {{
			case ""SEC"": return RobotRunMode.SEC;
			case ""MIN"": return RobotRunMode.MIN;
			case ""HOUR"": return RobotRunMode.HOUR;
			case ""DAY"": return RobotRunMode.DAY;
			case ""RUNONDAY"": return RobotRunMode.RunOnDay;
			case ""RUNONWEEK"": return RobotRunMode.RunOnWeek;
			case ""RUNONMONTH"": return RobotRunMode.RunOnMonth;
			default: return RobotRunMode.NONE;
		}}
	}}
}}

public class RobotDef : IDisposable {{
	private string _name;
	private RobotRunMode _mode = RobotRunMode.NONE;
	private string _param;
	private string _runParam;
	private int _runTimes = 0;
	private int _errTimes = 0;

	private Robot _onwer;
	internal Timer _timer;
	private bool _timerIntervalOverflow = false;

	public RobotDef(Robot onwer, string name, RobotRunMode mode, string param, string runParam) {{
		_onwer = onwer;
		_name = name;
		_mode = mode;
		_param = param;
		_runParam = runParam;
	}}
	public void Update(RobotRunMode mode, string param, string runParam) {{
		if (_mode != mode || _param != param || _runParam != runParam) {{
			_mode = mode;
			_param = param;
			_runParam = runParam;
			if (_timer != null) {{
				_timer.Dispose();
				_timer = null;
			}}
			RunNow();
		}}
	}}

	public void RunNow() {{
		double tmp = this.Interval;
		_timerIntervalOverflow = tmp > int.MaxValue;
		int interval = _timerIntervalOverflow ? int.MaxValue : (int)tmp;
		if (interval <= 0) {{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(""		{{0}} Interval <= 0"", _name);
			Console.ResetColor();
			return;
		}}
		Console.WriteLine(interval);
		if (_timer == null) {{
			_timer = new Timer(a => {{
				if (_timerIntervalOverflow) {{
					RunNow();
					return;
				}}
				_runTimes++;
				string logObj = this.ToString();
				try {{
					_onwer.onRun(this);
				}} catch (Exception ex) {{
					_errTimes++;
					_onwer.onError(ex, this);
				}}
				RunNow();
			}}, null, interval, -1);
		}} else {{
			_timer.Change(interval, -1);
		}}
		if (tmp > 1000 * 9) {{
			DateTime nextTime = DateTime.Now.AddMilliseconds(tmp);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(""		{{0}} 下次触发时间：{{1:yyyy-MM-dd HH:mm:ss}}"", _name, nextTime);
			Console.ResetColor();
		}}
	}}

	public override string ToString() {{
		return Name + "", "" + Mode + "", "" + Param + "", "" + RunParam;
	}}

	#region IDisposable 成员

	public void Dispose() {{
		if (_timer != null) {{
			_timer.Dispose();
			_timer = null;
		}}
	}}

	#endregion

	public string Name {{ get {{ return _name; }} }}
	public RobotRunMode Mode {{ get {{ return _mode; }} }}
	public string Param {{ get {{ return _param; }} }}
	public string RunParam {{ get {{ return _runParam; }} }}
	public int RunTimes {{ get {{ return _runTimes; }} }}
	public int ErrTimes {{ get {{ return _errTimes; }} }}

	public double Interval {{
		get {{
			DateTime now = DateTime.Now;
			DateTime curt = DateTime.MinValue;
			TimeSpan ts = TimeSpan.Zero;
			uint ww = 0, dd = 0, hh = 0, mm = 0, ss = 0;
			double interval = -1;
			switch (_mode) {{
				case RobotRunMode.SEC:
					double.TryParse(_param, out interval);
					interval *= 1000;
					break;
				case RobotRunMode.MIN:
					double.TryParse(_param, out interval);
					interval *= 60 * 1000;
					break;
				case RobotRunMode.HOUR:
					double.TryParse(_param, out interval);
					interval *= 60 * 60 * 1000;
					break;
				case RobotRunMode.DAY:
					double.TryParse(_param, out interval);
					interval *= 24 * 60 * 60 * 1000;
					break;
				case RobotRunMode.RunOnDay:
					List<string> hhmmss = new List<string>(string.Concat(_param).Split(':'));
					if (hhmmss.Count == 3)
						if (uint.TryParse(hhmmss[0], out hh) && hh < 24 &&
							uint.TryParse(hhmmss[1], out mm) && mm < 60 &&
							uint.TryParse(hhmmss[2], out ss) && ss < 60) {{
							curt = now.Date.AddHours(hh).AddMinutes(mm).AddSeconds(ss);
							ts = curt.Subtract(now);
							while (!(ts.TotalMilliseconds > 0)) {{
								curt = curt.AddDays(1);
								ts = curt.Subtract(now);
							}}
							interval = ts.TotalMilliseconds;
						}}
					break;
				case RobotRunMode.RunOnWeek:
					string[] wwhhmmss = string.Concat(_param).Split(':');
					if (wwhhmmss.Length == 4)
						if (uint.TryParse(wwhhmmss[0], out ww) && ww < 7 &&
							uint.TryParse(wwhhmmss[1], out hh) && hh < 24 &&
							uint.TryParse(wwhhmmss[2], out mm) && mm < 60 &&
							uint.TryParse(wwhhmmss[3], out ss) && ss < 60) {{
							curt = now.Date.AddHours(hh).AddMinutes(mm).AddSeconds(ss);
							ts = curt.Subtract(now);
							while(!(ts.TotalMilliseconds > 0 && (int)curt.DayOfWeek == ww)) {{
								curt = curt.AddDays(1);
								ts = curt.Subtract(now);
							}}
							interval = ts.TotalMilliseconds;
						}}
					break;
				case RobotRunMode.RunOnMonth:
					string[] ddhhmmss = string.Concat(_param).Split(':');
					if (ddhhmmss.Length == 4)
						if (uint.TryParse(ddhhmmss[0], out dd) && dd > 0 && dd < 32 &&
							uint.TryParse(ddhhmmss[1], out hh) && hh < 24 &&
							uint.TryParse(ddhhmmss[2], out mm) && mm < 60 &&
							uint.TryParse(ddhhmmss[3], out ss) && ss < 60) {{
							curt = new DateTime(now.Year, now.Month, (int)dd).AddHours(hh).AddMinutes(mm).AddSeconds(ss);
							ts = curt.Subtract(now);
							while (!(ts.TotalMilliseconds > 0)) {{
								curt = curt.AddMonths(1);
								ts = curt.Subtract(now);
							}}
							interval = ts.TotalMilliseconds;
						}}
					break;
			}}
			if (interval == 0) interval = 1;
			return interval;
		}}
	}}
}}
/*
; 和 # 匀为行注释
;SEC：					按秒触发
;MIN：					按分触发
;HOUR：					按时触发
;DAY：					按天触发
;RunOnDay：				每天 什么时间 触发
;RunOnWeek：			星期几 什么时间 触发
;RunOnMonth：			每月 第几天 什么时间 触发

;Name1		SEC			2				/schedule/test002.aspx
;Name2		MIN			2				/schedule/test002.aspx
;Name3		HOUR		1				/schedule/test002.aspx
;Name4		DAY			2				/schedule/test002.aspx
;Name5		RunOnDay	15:55:59		/schedule/test002.aspx
;每天15点55分59秒
;Name6		RunOnWeek	1:15:55:59		/schedule/test002.aspx
;每星期一15点55分59秒
;Name7		RunOnMonth	1:15:55:59		/schedule/test002.aspx
;每月1号15点55分59秒
*/
public enum RobotRunMode {{
	NONE = 0,
	SEC = 1,
	MIN = 2,
	HOUR = 3,
	DAY = 4,
	RunOnDay = 11,
	RunOnWeek = 12,
	RunOnMonth = 13
}}

public delegate void RobotErrorHandler(object sender, RobotErrorEventArgs e);
public delegate void RobotRunHandler(object sender, RobotDef e);
public class RobotErrorEventArgs : EventArgs {{

	private Exception _exception;
	private RobotDef _def;

	public RobotErrorEventArgs(Exception exception, RobotDef def) {{
		_exception = exception;
		_def = def;
	}}

	public Exception Exception {{
		get {{ return _exception; }}
	}}
	public RobotDef Def {{
		get {{ return _def; }}
	}}
}}";
			#endregion
			public static readonly string Common_WinFormClass_WorkQueue_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class WorkQueue : WorkQueue<AnonymousHandler> {{
	public WorkQueue() : this(16, -1) {{ }}
	public WorkQueue(int thread)
		: this(thread, -1) {{
	}}
	public WorkQueue(int thread, int capacity) {{
		base.Thread = thread;
		base.Capacity = capacity;
		base.Process += delegate(AnonymousHandler ah) {{
			ah();
		}};
	}}
}}

public class WorkQueue<T> : IDisposable {{
	public delegate void WorkQueueProcessHandler(T item);
	public event WorkQueueProcessHandler Process;

	private int _thread = 16;
	private int _capacity = -1;
	private int _work_index = 0;
	private Dictionary<int, WorkInfo> _works = new Dictionary<int, WorkInfo>();
	private object _works_lock = new object();
	private Queue<T> _queue = new Queue<T>();
	private object _queue_lock = new object();

	public WorkQueue() : this(16, -1) {{ }}
	public WorkQueue(int thread)
		: this(thread, -1) {{
	}}
	public WorkQueue(int thread, int capacity) {{
		_thread = thread;
		_capacity = capacity;
	}}

	public void Enqueue(T item) {{
		lock (_queue_lock) {{
			if (_capacity > 0 && _queue.Count >= _capacity) return;
			_queue.Enqueue(item);
		}}
		lock (_works_lock) {{
			foreach (WorkInfo w in _works.Values) {{
				if (w.IsWaiting) {{
					w.Set();
					return;
				}}
			}}
		}}
		if (_works.Count < _thread) {{
			if (_queue.Count > 0) {{
				int index = 0;
				lock (_works_lock) {{
					index = _work_index++;
					_works.Add(index, new WorkInfo());
				}}
				new Thread(delegate() {{
					WorkInfo work = _works[index];
					while (true) {{
						List<T> de = new List<T>();
						if (_queue.Count > 0) {{
							lock (_queue_lock) {{
								if (_queue.Count > 0) {{
									de.Add(_queue.Dequeue());
								}}
							}}
						}}

						if (de.Count > 0) {{
							try {{
								this.OnProcess(de[0]);
							}} catch {{
							}}
						}}

						if (_queue.Count == 0) {{
							work.WaitOne(TimeSpan.FromSeconds(20));

							if (_queue.Count == 0) {{
								break;
							}}
						}}
					}}
					lock (_works_lock) {{
						_works.Remove(index);
					}}
					work.Dispose();
				}}).Start();
			}}
		}}
	}}

	protected virtual void OnProcess(T item) {{
		if (Process != null) {{
			Process(item);
		}}
	}}

	#region IDisposable 成员

	public void Dispose() {{
		lock (_queue_lock) {{
			_queue.Clear();
		}}
		lock (_works_lock) {{
			foreach (WorkInfo w in _works.Values) {{
				w.Dispose();
			}}
		}}
	}}

	#endregion

	public int Thread {{
		get {{ return _thread; }}
		set {{
			if (_thread != value) {{
				_thread = value;
			}}
		}}
	}}
	public int Capacity {{
		get {{ return _capacity; }}
		set {{
			if (_capacity != value) {{
				_capacity = value;
			}}
		}}
	}}

	public int UsedThread {{
		get {{ return _works.Count; }}
	}}
	public int Queue {{
		get {{ return _queue.Count; }}
	}}

	public string Statistics {{
		get {{
			string value = string.Format(@""线程：{{0}}/{{1}}
队列：{{2}}

"", _works.Count, _thread, _queue.Count);
			int[] keys = new int[_works.Count];
			try {{
				_works.Keys.CopyTo(keys, 0);
			}} catch {{
				lock (_works_lock) {{
					keys = new int[_works.Count];
					_works.Keys.CopyTo(keys, 0);
				}}
			}}
			foreach (int k in keys) {{
				WorkInfo w = null;
				if (_works.TryGetValue(k, out w)) {{
					value += string.Format(@""线程{{0}}：{{1}}
"", k, w.IsWaiting);
				}}
			}}
			return value;
		}}
	}}

	class WorkInfo : IDisposable {{
		private ManualResetEvent _reset = new ManualResetEvent(false);
		private bool _isWaiting = false;

		public void WaitOne(TimeSpan timeout) {{
			try {{
				_reset.Reset();
				_isWaiting = true;
				_reset.WaitOne(timeout);
			}} catch {{ }}
		}}
		public void Set() {{
			try {{
				_isWaiting = false;
				_reset.Set();
			}} catch {{ }}
		}}

		public bool IsWaiting {{
			get {{ return _isWaiting; }}
		}}

		#region IDisposable 成员

		public void Dispose() {{
			this.Set();
		}}

		#endregion
	}}
}}";
			#endregion

			public static readonly string Common_project_json =
			#region 内容太长已被收起
 @"{{
  ""version"": ""1.0.0-*"",

    ""dependencies"": {{
        ""Microsoft.AspNetCore.Http.Abstractions"": ""1.0.0"",
        ""Microsoft.VisualBasic"": ""10.0.1"",
        ""NETStandard.Library"": ""1.6.0"",
        ""System.Collections.NonGeneric"": ""4.0.1"",
        ""System.Collections.Specialized"": ""4.0.1"",
        ""System.IO.FileSystem.Watcher"": ""4.0.0"",
        ""System.Net.NameResolution"": ""4.0.0"",
        ""System.Runtime.Serialization.Json"": ""4.0.1"",
        ""System.Threading.Thread"": ""4.0.0""
    }},

  ""frameworks"": {{
    ""netstandard1.6"": {{
      ""imports"": ""dnxcore50""
    }}
  }}
}}
";
			#endregion

			public static readonly string Admin_bin_Debug_web_config =
			#region 内容太长已被收起
 @"[connectionStrings]
{1}={{connectionString}}
{0}RedisConnectionString=10.17.65.49:6379,password=duoyi@2016,defaultdatabase=13,ConnectTimeout=10000

[appSettings]
{0}_ITEM_CACHE_TIMEOUT=180

";
			#endregion
			public static readonly string Admin_web_config =
			#region 内容太长已被收起
 @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>

  <!--
    Configure your application settings in appsettings.json. Learn more at http://go.microsoft.com/fwlink/?LinkId=786380
  -->

  <system.webServer>
    <handlers>
      <add name=""aspNetCore"" path=""*"" verb=""*"" modules=""AspNetCoreModule"" resourceType=""Unspecified""/>
    </handlers>
    <aspNetCore processPath=""%LAUNCHER_PATH%"" arguments=""%LAUNCHER_ARGS%"" stdoutLogEnabled=""false"" stdoutLogFile="".\logs\stdout"" forwardWindowsAuthToken=""false""/>
  </system.webServer>
</configuration>
";
			#endregion
			public static readonly string Admin_appsettings_json =
			#region 内容太长已被收起
 @"{{
	""Logging"": {{
		""IncludeScopes"": false,
		""LogLevel"": {{
			""Default"": ""Debug"",
			""System"": ""Information"",
			""Microsoft"": ""Information""
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_Program_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace {0}.Admin {{
	public class Program {{
		public static void Main(string[] args) {{
			var host = new WebHostBuilder()
				.UseUrls($""http://*:{{new Random().Next(20000, 20000)}}"")
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_Startup_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Swashbuckle.Swagger.Model;

namespace {0}.Admin {{
	public class Startup {{
		public Startup(IHostingEnvironment env) {{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile(""appsettings.json"", optional: true, reloadOnChange: true)
				.AddJsonFile($""appsettings.{{env.EnvironmentName}}.json"", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}}

		public IConfigurationRoot Configuration {{ get; }}

		public void ConfigureServices(IServiceCollection services) {{
			services.AddSingleton<IDistributedCache>(
				serviceProvider =>
					new RedisCache(new RedisCacheOptions {{
						Configuration = IniHelper.LoadIni(""../web.config"")[""connectionStrings""][""{0}RedisConnectionString""],
						InstanceName = ""Session""
					}})).AddSession();
			services.AddMvc();

			services.ConfigureSwaggerGen(options => {{
				options.SingleApiVersion(new Info {{
					Version = ""v1"",
					Title = ""{0} API"",
					Description = ""{0} 项目webapi接口说明"",
					TermsOfService = ""None"",
					Contact = new Contact {{ Name = ""duoyi"", Email = """", Url = ""http://duoyi.com"" }},
					License = new License {{ Name = ""duoyi"", Url = ""http://duoyi.com"" }}
				}});
				options.IncludeXmlComments(AppContext.BaseDirectory + @""/Admin.xml"");
			}});
			services.AddSwaggerGen();
		}}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Console.OutputEncoding = Encoding.GetEncoding(""GB2312"");
			Console.InputEncoding = Encoding.GetEncoding(""GB2312"");

			loggerFactory.AddConsole(Configuration.GetSection(""Logging""));
			loggerFactory.AddDebug();

			if (env.IsDevelopment()) 
				app.UseDeveloperExceptionPage();

			{0}.DAL.SqlHelper.Log = loggerFactory.CreateLogger(""{0}_DAL_sqlhelper"");
			{0}.DAL.ConnectionManager.ConnectionString = Configuration[""ConnectionStrings:{0}ConnectionString""];

			app.UseSession(new SessionOptions() {{ IdleTimeout = TimeSpan.FromMinutes(30) }});
			app.UseMvc();
			app.UseDefaultFiles().UseStaticFiles(); //UseDefaultFiles 必须在 UseStaticFiles 之前调用

			app.UseSwagger().UseSwaggerUi();
		}}
	}}
}}
";
			#endregion

			public static readonly string Admin_App_Code_BaseController_cs =
			#region 内容太长已被收起
 @"using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using {0}.BLL;
using {0}.Model;

public class BaseController : Controller {{
	//public SysuserInfo LoginUser {{ get; private set; }}
	public override void OnActionExecuting(ActionExecutingContext context) {{
		//byte[] tryvalue;
		//if (context.HttpContext.Session.TryGetValue(""login.username"", out tryvalue)) {{
		//	string username = Encoding.UTF8.GetString(tryvalue);
		//	this.LoginUser = Sysuser.GetItemByUsername(username);
		//}}
		//if (this.LoginUser == null) {{
		//	context.Result = new JsonResult(new APIReturn(-12, ""未登陆或者没有权限""));
		//}}
		base.OnActionExecuting(context);
	}}
	public override void OnActionExecuted(ActionExecutedContext context) {{
		if (context.Exception != null) {{
			// 错误拦截，在这里记录日志
			//this.Json(new APIReturn(-1, context.Exception.Message)).ExecuteResultAsync(context).Wait();
			//context.Exception = null;
		}}
		base.OnActionExecuted(context);
	}}

	//public bool sysrole_check(string url) {{{{
	//	url = url.ToLower();
	//	//Response.Write(url + """"<br>"""");
	//	if (url == """"/"""" || url.IndexOf(""""/default.aspx"""") == 0) return true;
	//	foreach(var role in this.LoginUser.Obj_sysroles) {{{{
	//		//Response.Write(role.ToString());
	//		foreach(var dir in role.Obj_sysdirs) {{{{
	//			//Response.Write(""""-----------------"""" + dir.ToString() + """"<br>"""");
	//			string tmp = dir.Url;
	//			if (tmp.EndsWith(""""/"""")) tmp += """"default.aspx"""";
	//			if (url.IndexOf(tmp) == 0) return true;
	//		}}}}
	//	}}}}
	//	return false;
	//}}}}
}}

public class APIReturn {{
	public int Code {{ get; protected set; }}
	public string Message {{ get; protected set; }}
	public Hashtable Data {{ get; protected set; }}
	public bool Success {{ get {{ return this.Code == 0; }} }}

	public APIReturn() {{
		this.Data = new Hashtable();
	}}
	public APIReturn(int code) : this() {{ this.Code = code; }}
	public APIReturn(string message) : this() {{ this.Message = message; }}
	public APIReturn(int code, string message, params object[] data) : this() {{
		this.Code = code;
		this.Message = message;
		if (data != null) {{
			for (int a = 0; a < data.Length; a += 2) {{
				if (data[a] == null) continue;
				this.Data[data[a]] = a + 1 < data.Length ? data[a + 1] : null;
			}}
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_Microsoft_Extensions_Caching_Redis_RedisCache_cs =
			#region 内容太长已被收起
 @"using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Microsoft.Extensions.Caching.Redis {{
	public class RedisCache : IDistributedCache, IDisposable {{
		// KEYS[1] = = key
		// ARGV[1] = absolute-expiration - ticks as long (-1 for none)
		// ARGV[2] = sliding-expiration - ticks as long (-1 for none)
		// ARGV[3] = relative-expiration (long, in seconds, -1 for none) - Min(absolute-expiration - Now, sliding-expiration)
		// ARGV[4] = data - byte[]
		// this order should not change LUA script depends on it
		private const string SetScript = (@""
                redis.call('HMSET', KEYS[1], 'absexp', ARGV[1], 'sldexp', ARGV[2], 'data', ARGV[4])
                if ARGV[3] ~= '-1' then
                  redis.call('EXPIRE', KEYS[1], ARGV[3])
                end
                return 1"");
		private const string AbsoluteExpirationKey = ""absexp"";
		private const string SlidingExpirationKey = ""sldexp"";
		private const string DataKey = ""data"";
		private const long NotPresent = -1;

		private ConnectionMultiplexer _connection;
		private IDatabase _cache;

		private readonly RedisCacheOptions _options;
		private readonly string _instance;

		public RedisCache(IOptions<RedisCacheOptions> optionsAccessor) {{
			if (optionsAccessor == null) {{
				throw new ArgumentNullException(nameof(optionsAccessor));
			}}

			_options = optionsAccessor.Value;

			// This allows partitioning a single backend cache for use with multiple apps/services.
			_instance = _options.InstanceName ?? string.Empty;
		}}

		public byte[] Get(string key) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			return GetAndRefresh(key, getData: true);
		}}

		public async Task<byte[]> GetAsync(string key) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			return await GetAndRefreshAsync(key, getData: true);
		}}

		public void Set(string key, byte[] value, DistributedCacheEntryOptions options) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			if (value == null) {{
				throw new ArgumentNullException(nameof(value));
			}}

			if (options == null) {{
				throw new ArgumentNullException(nameof(options));
			}}

			Connect();

			var creationTime = DateTimeOffset.UtcNow;

			var absoluteExpiration = GetAbsoluteExpiration(creationTime, options);

			var result = _cache.ScriptEvaluate(SetScript, new RedisKey[] {{ _instance + key }},
				new RedisValue[]
				{{
						absoluteExpiration?.Ticks ?? NotPresent,
						options.SlidingExpiration?.Ticks ?? NotPresent,
						GetExpirationInSeconds(creationTime, absoluteExpiration, options) ?? NotPresent,
						value
				}});
		}}

		public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			if (value == null) {{
				throw new ArgumentNullException(nameof(value));
			}}

			if (options == null) {{
				throw new ArgumentNullException(nameof(options));
			}}

			await ConnectAsync();

			var creationTime = DateTimeOffset.UtcNow;

			var absoluteExpiration = GetAbsoluteExpiration(creationTime, options);

			await _cache.ScriptEvaluateAsync(SetScript, new RedisKey[] {{ _instance + key }},
				new RedisValue[]
				{{
						absoluteExpiration?.Ticks ?? NotPresent,
						options.SlidingExpiration?.Ticks ?? NotPresent,
						GetExpirationInSeconds(creationTime, absoluteExpiration, options) ?? NotPresent,
						value
				}});
		}}

		public void Refresh(string key) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			GetAndRefresh(key, getData: false);
		}}

		public async Task RefreshAsync(string key) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			await GetAndRefreshAsync(key, getData: false);
		}}

		private void Connect() {{
			if (_connection == null) {{
				_connection = ConnectionMultiplexer.Connect(_options.Configuration);
				_cache = _connection.GetDatabase();
			}}
		}}

		private async Task ConnectAsync() {{
			if (_connection == null) {{
				_connection = await ConnectionMultiplexer.ConnectAsync(_options.Configuration);
				_cache = _connection.GetDatabase();
			}}
		}}

		private byte[] GetAndRefresh(string key, bool getData) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			Connect();

			// This also resets the LRU status as desired.
			// TODO: Can this be done in one operation on the server side? Probably, the trick would just be the DateTimeOffset math.
			RedisValue[] results;
			if (getData) {{
				results = _cache.HashMemberGet(_instance + key, AbsoluteExpirationKey, SlidingExpirationKey, DataKey);
			}} else {{
				results = _cache.HashMemberGet(_instance + key, AbsoluteExpirationKey, SlidingExpirationKey);
			}}

			// TODO: Error handling
			if (results.Length >= 2) {{
				// Note we always get back two results, even if they are all null.
				// These operations will no-op in the null scenario.
				DateTimeOffset? absExpr;
				TimeSpan? sldExpr;
				MapMetadata(results, out absExpr, out sldExpr);
				Refresh(key, absExpr, sldExpr);
			}}

			if (results.Length >= 3 && results[2].HasValue) {{
				return results[2];
			}}

			return null;
		}}

		private async Task<byte[]> GetAndRefreshAsync(string key, bool getData) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			await ConnectAsync();

			// This also resets the LRU status as desired.
			// TODO: Can this be done in one operation on the server side? Probably, the trick would just be the DateTimeOffset math.
			RedisValue[] results;
			if (getData) {{
				results = await _cache.HashMemberGetAsync(_instance + key, AbsoluteExpirationKey, SlidingExpirationKey, DataKey);
			}} else {{
				results = await _cache.HashMemberGetAsync(_instance + key, AbsoluteExpirationKey, SlidingExpirationKey);
			}}

			// TODO: Error handling
			if (results.Length >= 2) {{
				// Note we always get back two results, even if they are all null.
				// These operations will no-op in the null scenario.
				DateTimeOffset? absExpr;
				TimeSpan? sldExpr;
				MapMetadata(results, out absExpr, out sldExpr);
				await RefreshAsync(key, absExpr, sldExpr);
			}}

			if (results.Length >= 3 && results[2].HasValue) {{
				return results[2];
			}}

			return null;
		}}

		public void Remove(string key) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			Connect();

			_cache.KeyDelete(_instance + key);
			// TODO: Error handling
		}}

		public async Task RemoveAsync(string key) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			await ConnectAsync();

			await _cache.KeyDeleteAsync(_instance + key);
			// TODO: Error handling
		}}

		private void MapMetadata(RedisValue[] results, out DateTimeOffset? absoluteExpiration, out TimeSpan? slidingExpiration) {{
			absoluteExpiration = null;
			slidingExpiration = null;
			var absoluteExpirationTicks = (long?)results[0];
			if (absoluteExpirationTicks.HasValue && absoluteExpirationTicks.Value != NotPresent) {{
				absoluteExpiration = new DateTimeOffset(absoluteExpirationTicks.Value, TimeSpan.Zero);
			}}
			var slidingExpirationTicks = (long?)results[1];
			if (slidingExpirationTicks.HasValue && slidingExpirationTicks.Value != NotPresent) {{
				slidingExpiration = new TimeSpan(slidingExpirationTicks.Value);
			}}
		}}

		private void Refresh(string key, DateTimeOffset? absExpr, TimeSpan? sldExpr) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			// Note Refresh has no effect if there is just an absolute expiration (or neither).
			TimeSpan? expr = null;
			if (sldExpr.HasValue) {{
				if (absExpr.HasValue) {{
					var relExpr = absExpr.Value - DateTimeOffset.Now;
					expr = relExpr <= sldExpr.Value ? relExpr : sldExpr;
				}} else {{
					expr = sldExpr;
				}}
				_cache.KeyExpire(_instance + key, expr);
				// TODO: Error handling
			}}
		}}

		private async Task RefreshAsync(string key, DateTimeOffset? absExpr, TimeSpan? sldExpr) {{
			if (key == null) {{
				throw new ArgumentNullException(nameof(key));
			}}

			// Note Refresh has no effect if there is just an absolute expiration (or neither).
			TimeSpan? expr = null;
			if (sldExpr.HasValue) {{
				if (absExpr.HasValue) {{
					var relExpr = absExpr.Value - DateTimeOffset.Now;
					expr = relExpr <= sldExpr.Value ? relExpr : sldExpr;
				}} else {{
					expr = sldExpr;
				}}
				await _cache.KeyExpireAsync(_instance + key, expr);
				// TODO: Error handling
			}}
		}}

		private static long? GetExpirationInSeconds(DateTimeOffset creationTime, DateTimeOffset? absoluteExpiration, DistributedCacheEntryOptions options) {{
			if (absoluteExpiration.HasValue && options.SlidingExpiration.HasValue) {{
				return (long)Math.Min(
					(absoluteExpiration.Value - creationTime).TotalSeconds,
					options.SlidingExpiration.Value.TotalSeconds);
			}} else if (absoluteExpiration.HasValue) {{
				return (long)(absoluteExpiration.Value - creationTime).TotalSeconds;
			}} else if (options.SlidingExpiration.HasValue) {{
				return (long)options.SlidingExpiration.Value.TotalSeconds;
			}}
			return null;
		}}

		private static DateTimeOffset? GetAbsoluteExpiration(DateTimeOffset creationTime, DistributedCacheEntryOptions options) {{
			if (options.AbsoluteExpiration.HasValue && options.AbsoluteExpiration <= creationTime) {{
				throw new ArgumentOutOfRangeException(
					nameof(DistributedCacheEntryOptions.AbsoluteExpiration),
					options.AbsoluteExpiration.Value,
					""The absolute expiration value must be in the future."");
			}}
			var absoluteExpiration = options.AbsoluteExpiration;
			if (options.AbsoluteExpirationRelativeToNow.HasValue) {{
				absoluteExpiration = creationTime + options.AbsoluteExpirationRelativeToNow;
			}}

			return absoluteExpiration;
		}}

		public void Dispose() {{
			if (_connection != null) {{
				_connection.Close();
			}}
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_Microsoft_Extensions_Caching_Redis_RedisCacheOptions_cs =
			#region 内容太长已被收起
 @"using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.Caching.Redis {{
	/// <summary>
	/// Configuration options for <see cref=""RedisCache""/>.
	/// </summary>
	public class RedisCacheOptions : IOptions<RedisCacheOptions> {{
		/// <summary>
		/// The configuration used to connect to Redis.
		/// </summary>
		public string Configuration {{ get; set; }}

		/// <summary>
		/// The Redis instance name.
		/// </summary>
		public string InstanceName {{ get; set; }}

		RedisCacheOptions IOptions<RedisCacheOptions>.Value {{
			get {{ return this; }}
		}}
	}}
}}
";
			#endregion
			public static readonly string Admin_App_Code_Microsoft_Extensions_Caching_Redis_RedisExtensions_cs =
			#region 内容太长已被收起
 @"using System.Threading.Tasks;
using StackExchange.Redis;

namespace Microsoft.Extensions.Caching.Redis {{
	internal static class RedisExtensions {{
		private const string HmGetScript = (@""return redis.call('HMGET', KEYS[1], unpack(ARGV))"");

		internal static RedisValue[] HashMemberGet(this IDatabase cache, string key, params string[] members) {{
			var result = cache.ScriptEvaluate(
				HmGetScript,
				new RedisKey[] {{ key }},
				GetRedisMembers(members));

			// TODO: Error checking?
			return (RedisValue[])result;
		}}

		internal static async Task<RedisValue[]> HashMemberGetAsync(
			this IDatabase cache,
			string key,
			params string[] members) {{
			var result = await cache.ScriptEvaluateAsync(
				HmGetScript,
				new RedisKey[] {{ key }},
				GetRedisMembers(members));

			// TODO: Error checking?
			return (RedisValue[])result;
		}}

		private static RedisValue[] GetRedisMembers(params string[] members) {{
			var redisMembers = new RedisValue[members.Length];
			for (int i = 0; i < members.Length; i++) {{
				redisMembers[i] = (RedisValue)members[i];
			}}

			return redisMembers;
		}}
	}}
}}
";
			#endregion

			public static readonly string Admin_Controllers_SysController =
			#region 内容太长已被收起
 @"using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using {0}.BLL;
using {0}.Model;

namespace {0}.Admin.Controllers {{
	[Route(""api/[controller]"")]
	public class SysController : Controller {{
		[HttpGet(@""connection"")]
		public object Get_connection() {{
			List<Hashtable> ret = new List<Hashtable>();
			foreach (int tid in {0}.DAL.ConnectionManager.ConnectionPool.Keys) {{
				List<Hashtable> value = new List<Hashtable>();
				foreach (var conn in {0}.DAL.ConnectionManager.ConnectionPool[tid]) {{
					//conn.SqlConnection.Close();
					value.Add(new Hashtable() {{
						{{ ""数据库"", conn.SqlConnection.Database }},
						{{ ""状态"", conn.SqlConnection.State }},
						{{ ""最后活动"", conn.LastActive }}
					}});
				}}
				ret.Add(new Hashtable() {{
					{{ ""线程"" + tid, value }}
				}});
			}}
			return ret;
		}}

		[HttpGet(@""init_sysdir"")]
		public APIReturn Get_init_sysdir() {{
			/*
			if (Sysdir.SelectByParent_id(null).Count() > 0)
				return new APIReturn(-33, ""本系统已经初始化过，页面没经过任何操作退出。"");

			SysdirInfo dir1, dir2, dir3;
			dir1 = Sysdir.Insert(null, DateTime.Now, ""运营管理"", 1, null);{1}
			*/
			return new APIReturn(0, ""管理目录已初始化完成。"");
		}}
	}}
}}
";
			#endregion

			public static readonly string Admin_Controllers =
			#region 内容太长已被收起
			@"using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using {0}.BLL;
using {0}.Model;

namespace {0}.Admin.Controllers {{
	[Route(""api/[controller]"")]
	public class {1}Controller : BaseController {{
		private readonly ILogger<{1}Controller> _logger;
		public {1}Controller(ILogger<{1}Controller> logger) {{ _logger = logger; }}

		[HttpGet]
		public APIReturn Get_list({12}[FromQuery] int limit = 20, [FromQuery] int skip = 0) {{
			var select = {1}.Select{8};{9}
			var items = select.Skip(skip).Limit(limit).ToList();
			return new APIReturn(0, ""成功"", ""items"", items.ToBson());
		}}

		[HttpGet(@""{3}"")]
		public APIReturn Get_item({4}) {{
			{1}Info item = {1}.GetItem({5});
			if (item == null) return new APIReturn(98, ""记录不存在，或者没有权限"");
			return new APIReturn(0, ""成功"", ""item"", item.ToBson());
		}}

		[HttpPost]
		public APIReturn Post_insert({10}) {{
			{1}Info item = new {1}Info();{13}{7}
			item = {1}.Insert(item);
			return new APIReturn(0, ""成功"", ""item"", item.ToBson());
		}}

		[HttpPut(""{3}"")]
		public APIReturn Put_update({4}{11}) {{
			{1}Info item = new {1}Info();{6}{7}
			int affrows = {1}.Update(item);
			if (affrows > 0) return new APIReturn(0, ""成功"");
			return new APIReturn(99, ""失败"");
		}}

		[HttpDelete(""{3}"")]
		public APIReturn Delete_delete({4}) {{
			int affrows = {1}.Delete({5});
			if (affrows > 0) return new APIReturn(0, string.Format(""删除成功，影响行数：{{0}}"", affrows));
			return new APIReturn(99, ""失败"");
		}}
	}}
}}
";
			#endregion

			public static readonly string Admin_project_json =
			#region 内容太长已被收起
 @"{{
  ""dependencies"": {{
    ""Microsoft.NETCore.App"": {{
      ""version"": ""1.0.0"",
      ""type"": ""platform""
    }},
    ""Microsoft.AspNetCore.Mvc"": ""1.0.0"",
    ""Microsoft.AspNetCore.Server.IISIntegration"": ""1.0.0"",
    ""Microsoft.AspNetCore.Server.Kestrel"": ""1.0.0"",
    ""Microsoft.Extensions.Configuration.EnvironmentVariables"": ""1.0.0"",
    ""Microsoft.Extensions.Configuration.FileExtensions"": ""1.0.0"",
    ""Microsoft.Extensions.Configuration.Json"": ""1.0.0"",
    ""Microsoft.Extensions.Logging"": ""1.0.0"",
    ""Microsoft.Extensions.Logging.Console"": ""1.0.0"",
    ""Microsoft.Extensions.Logging.Debug"": ""1.0.0"",
			""Microsoft.Extensions.Options.ConfigurationExtensions"": ""1.0.0"",
    ""{0}.db"": ""1.0.0-*"",
    ""Microsoft.AspNetCore.Session"": ""1.0.0"",
    ""StackExchange.Redis"": ""1.1.604-alpha"",
    ""System.Text.Encoding.CodePages"": ""4.0.1"",
    ""Swashbuckle"": ""6.0.0-beta902"",
    ""Microsoft.AspNetCore.Diagnostics"": ""1.0.0""
  }},

  ""tools"": {{
    //""Microsoft.AspNetCore.Server.IISIntegration.Tools"": ""1.0.0-preview2-final""
	""Microsoft.DotNet.Watcher.Tools"": ""1.0.0-preview2-final""
  }},

  ""frameworks"": {{
    ""netcoreapp1.0"": {{
      ""imports"": [
        ""dotnet5.6"",
        ""portable-net45+win8""
      ]
    }}
  }},

  ""buildOptions"": {{
    ""emitEntryPoint"": true,
    ""preserveCompilationContext"": true,
    ""xmlDoc"": true
  }},

  ""runtimeOptions"": {{
    ""configProperties"": {{
      ""System.GC.Server"": true
    }}
  }},

  ""publishOptions"": {{
    ""include"": [
      ""wwwroot"",
      ""Views"",
      ""Areas/**/Views"",
      ""appsettings.json"",
      ""web.config""
    ]
  }},

  ""scripts"": {{
    ""postpublish"": [ ""dotnet publish-iis --publish-folder %publish:OutputPath% --framework %publish:FullTargetFramework%"" ]
  }}
}}
";
			#endregion
		}
	}
}
