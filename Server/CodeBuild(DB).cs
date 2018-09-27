using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;

namespace Server {

	internal partial class CodeBuild : IDisposable {
		private ClientInfo _client;
		private AcceptSocket _socket;
		private List<TableInfo> _tables;
		private Dictionary<string, Dictionary<string, string>> _column_coments = new Dictionary<string, Dictionary<string, string>>();

		public CodeBuild(ClientInfo client, AcceptSocket socket) {
			_client = client;
			_socket = socket;
		}

		private object[][] GetDataSet(string commandText) {
			SocketMessager messager = new SocketMessager("ExecuteDataSet", commandText);
			_socket.Write(messager, delegate (object sender, ServerSocketReceiveEventArgs e) {
				messager = e.Messager;
			});
			object[][] ret = messager.Arg as object[][]; //兼容.netcore传过来的数据
			if (ret == null) {
				DataSet ds = messager.Arg as DataSet; //兼容.net传过来的数据
				if (ds != null) {
					List<object[]> tmp = new List<object[]>();
					foreach (DataRow row in ds.Tables[0].Rows)
						tmp.Add(row.ItemArray);
					ret = tmp.ToArray();
				}
			}
			return ret;
		}
		private int ExecuteNonQuery(string commandText) {
			SocketMessager messager = new SocketMessager("ExecuteNonQuery", commandText);
			_socket.Write(messager, delegate(object sender, ServerSocketReceiveEventArgs e) {
				messager = e.Messager;
			});
			int val;
			int.TryParse(string.Concat(messager.Arg), out val);
			return val;
		}

		public List<DatabaseInfo> GetDatabases() {
			Logger.remotor.Info("GetDatabases: " + _client.Server + "," + _client.Username + "," + _client.Password);

			List<DatabaseInfo> loc1 = null;

			object[][] ds = this.GetDataSet(@"select name from sys.databases where name not in ('master','tempdb','model','msdb')");
			if (ds == null) return loc1;

			loc1 = new List<DatabaseInfo>();
			foreach (object[] row in ds) {
				loc1.Add(new DatabaseInfo(string.Concat(row[0])));
			}
			return loc1;
		}

		public List<TableInfo> GetTablesByDatabase(string database) {
			_client.Database = database;
			Logger.remotor.Info("GetTablesByDatabase: " + _client.Server + "," + _client.Username + "," + _client.Password + "," + _client.Database);

			List<TableInfo> loc1 = _tables = null;
			Dictionary<int, TableInfo> loc2 = new Dictionary<int, TableInfo>();
			Dictionary<int, Dictionary<string, ColumnInfo>> loc3 = new Dictionary<int, Dictionary<string, ColumnInfo>>();

			object[][] ds = this.GetDataSet(@"
select 
 a.Object_id
,b.name 'Owner'
,a.name 'Name'
,'T' type
from sys.tables a
inner join sys.schemas b on b.schema_id = a.schema_id
where not(b.name = 'dbo' and a.name = 'sysdiagrams')
union all
select
 a.Object_id
,b.name 'Owner'
,a.name 'Name'
,'V' type
from sys.views a
inner join sys.schemas b on b.schema_id = a.schema_id
union all
select 
 a.Object_id
,b.name 'Owner'
,a.name 'Name'
,'P' type
from sys.procedures a
inner join sys.schemas b on b.schema_id = a.schema_id
where a.type = 'P' and charindex('$NPSP', a.name) = 0 and charindex('diagram', a.name) = 0
order by type desc, b.name, a.name
");
			if (ds == null) return loc1;

			List<int> loc6 = new List<int>();
			List<int> loc66 = new List<int>();
			foreach (object[] row in ds) {
				int object_id = int.Parse(string.Concat(row[0]));
				string owner = string.Concat(row[1]);
				string table = string.Concat(row[2]);
				string type = string.Concat(row[3]);
				loc2.Add(object_id, new TableInfo(object_id, owner, table, type));
				loc3.Add(object_id, new Dictionary<string, ColumnInfo>());
				switch (type) {
					case "V":
					case "T":
						loc6.Add(object_id);
						break;
					case "P":
						loc66.Add(object_id);
						break;
				}
			}
			if (loc6.Count == 0) return loc1;
			string loc8 = string.Join(",", loc6.ConvertAll<string>(delegate(int item) { return string.Concat(item); }).ToArray());
			string loc88 = string.Join(",", loc66.ConvertAll<string>(delegate(int item) { return string.Concat(item); }).ToArray());

			string tsql_place = @"
select 
isnull(e.name,'') + '.' + isnull(d.name,'')
,a.Object_id
,a.name 'Column'
,b.name 'Type'
,case
 when b.name in ('Text', 'NText', 'Image') then -1
 when b.name in ('NChar', 'NVarchar') then a.max_length / 2
 else a.max_length end 'Length'
,b.name + case 
 when b.name in ('Char', 'VarChar', 'NChar', 'NVarChar', 'Binary', 'VarBinary') then '(' + 
  case when a.max_length = -1 then 'MAX' 
  when b.name in ('NChar', 'NVarchar') then cast(a.max_length / 2 as varchar)
  else cast(a.max_length as varchar) end + ')'
 when b.name in ('Numeric', 'Decimal') then '(' + cast(a.precision as varchar) + ',' + cast(a.scale as varchar) + ')'
 else '' end as 'SqlType'
,c.value
{0} a
inner join sys.types b on b.user_type_id = a.user_type_id
left join sys.extended_properties AS c ON c.major_id = a.object_id AND c.minor_id = a.column_id
left join sys.tables d on d.object_id = a.object_id
left join sys.schemas e on e.schema_id = d.schema_id
where a.object_id in ({1})
";
			string tsql = string.Format(tsql_place, @"
,a.is_nullable 'IsNullable'
,a.is_identity 'IsIdentity'
from sys.columns", loc8);
			if (loc88.Length > 0) {
				tsql += "union all" +
				string.Format(tsql_place.Replace(
					"left join sys.extended_properties AS c ON c.major_id = a.object_id AND c.minor_id = a.column_id", 
					"left join sys.extended_properties AS c ON c.major_id = a.object_id AND c.minor_id = a.parameter_id"), @"
,cast(0 as bit) 'IsNullable'
,a.is_output 'IsIdentity'
from sys.parameters", loc88);
			}
			ds = this.GetDataSet(tsql);
			if (ds == null) return loc1;

			foreach (object[] row in ds) {
				string table_id = string.Concat(row[0]);
				int object_id = int.Parse(string.Concat(row[1]));
				string column = string.Concat(row[2]);
				string type = string.Concat(row[3]);
				int max_length = int.Parse(string.Concat(row[4]));
				string sqlType = string.Concat(row[5]);
				string comment = string.Concat(row[6]);
				if (string.IsNullOrEmpty(comment)) comment = column;
				bool is_nullable = bool.Parse(string.Concat(row[7]));
				bool is_identity = bool.Parse(string.Concat(row[8]));
				if (max_length == 0) max_length = -1;
				loc3[object_id].Add(column, new ColumnInfo(
					column, CodeBuild.GetDBType(type), max_length, sqlType,
					DataSort.NONE, is_nullable, is_identity, false, false));
				if (!_column_coments.ContainsKey(table_id)) _column_coments.Add(table_id, new Dictionary<string, string>());
				if (!_column_coments[table_id].ContainsKey(column)) _column_coments[table_id].Add(column, comment);
				else _column_coments[table_id][column] = comment;
			}

			ds = this.GetDataSet(string.Format(@"
select 
 a.object_id 'Object_id'
,c.name 'Column'
,b.index_id 'Index_id'
,b.is_unique 'IsUnique'
,b.is_primary_key 'IsPrimaryKey'
,cast(case when b.type_desc = 'CLUSTERED' then 1 else 0 end as bit) 'IsClustered'
,case when a.is_descending_key = 1 then 2 when a.is_descending_key = 0 then 1 else 0 end 'IsDesc'
from sys.index_columns a
inner join sys.indexes b on b.object_id = a.object_id and b.index_id = a.index_id
left join sys.columns c on c.object_id = a.object_id and c.column_id = a.column_id
where a.object_id in ({0})
", loc8));
			if (ds == null) return loc1;

			Dictionary<int, Dictionary<int, List<ColumnInfo>>> indexColumns = new Dictionary<int, Dictionary<int, List<ColumnInfo>>>();
			Dictionary<int, Dictionary<int, List<ColumnInfo>>> uniqueColumns = new Dictionary<int, Dictionary<int, List<ColumnInfo>>>();
			foreach (object[] row in ds) {
				int object_id = int.Parse(string.Concat(row[0]));
				string column = string.Concat(row[1]);
				int index_id = int.Parse(string.Concat(row[2]));
				bool is_unique = bool.Parse(string.Concat(row[3]));
				bool is_primary_key = bool.Parse(string.Concat(row[4]));
				bool is_clustered = bool.Parse(string.Concat(row[5]));
				int is_desc = int.Parse(string.Concat(row[6]));

				if (loc3.ContainsKey(object_id) == false || loc3[object_id].ContainsKey(column) == false) continue;
				ColumnInfo loc9 = loc3[object_id][column];
				if (loc9.IsClustered == false && is_clustered) loc9.IsClustered = is_clustered;
				if (loc9.IsPrimaryKey == false && is_primary_key) loc9.IsPrimaryKey = is_primary_key;
				if (loc9.Orderby == DataSort.NONE) loc9.Orderby = (DataSort)is_desc;

				Dictionary<int, List<ColumnInfo>> loc10 = null;
				List<ColumnInfo> loc11 = null;
				if (!indexColumns.TryGetValue(object_id, out loc10)) {
					indexColumns.Add(object_id, loc10 = new Dictionary<int, List<ColumnInfo>>());
				}
				if (!loc10.TryGetValue(index_id, out loc11)) {
					loc10.Add(index_id, loc11 = new List<ColumnInfo>());
				}
				loc11.Add(loc9);
				if (is_unique) {
					if (!uniqueColumns.TryGetValue(object_id, out loc10)) {
						uniqueColumns.Add(object_id, loc10 = new Dictionary<int, List<ColumnInfo>>());
					}
					if (!loc10.TryGetValue(index_id, out loc11)) {
						loc10.Add(index_id, loc11 = new List<ColumnInfo>());
					}
					loc11.Add(loc9);
				}
			}
			foreach (int object_id in indexColumns.Keys) {
				foreach (List<ColumnInfo> columns in indexColumns[object_id].Values) {
					loc2[object_id].Indexes.Add(columns);
				}
			}
			foreach (int object_id in uniqueColumns.Keys) {
				foreach (List<ColumnInfo> columns in uniqueColumns[object_id].Values) {
					columns.Sort(delegate(ColumnInfo c1, ColumnInfo c2) {
						return c1.Name.CompareTo(c2.Name);
					});
					loc2[object_id].Uniques.Add(columns);
				}
			}
			ds = this.GetDataSet(string.Format(@"
select 
 b.object_id 'Object_id'
,c.name 'Column'
,a.constraint_object_id 'FKId'
,referenced_object_id
,cast(1 as bit) 'IsForeignKey'
,d.name 'Referenced_Column'
,null 'Referenced_Sln'
,null 'Referenced_Table'
from sys.foreign_key_columns a
inner join sys.tables b on b.object_id = a.parent_object_id
inner join sys.columns c on c.object_id = a.parent_object_id and c.column_id = a.parent_column_id
inner join sys.columns d on d.object_id = a.referenced_object_id and d.column_id = a.referenced_column_id
where b.object_id in ({0})
", loc8));
			if (ds == null) return loc1;

			Dictionary<int, Dictionary<int, ForeignKeyInfo>> fkColumns = new Dictionary<int, Dictionary<int, ForeignKeyInfo>>();
			foreach (object[] row in ds) {
				int object_id, fk_id, referenced_object_id ;
				int.TryParse(string.Concat(row[0]), out object_id);
				string column = string.Concat(row[1]);
				int.TryParse(string.Concat(row[2]), out fk_id);
				int.TryParse(string.Concat(row[3]), out referenced_object_id);
				bool is_foreign_key = bool.Parse(string.Concat(row[4]));
				string referenced_column = string.Concat(row[5]);
				string referenced_db = string.Concat(row[6]);
				string referenced_table = string.Concat(row[7]);
				ColumnInfo loc9 = loc3[object_id][column];
				TableInfo loc10 = null;
				ColumnInfo loc11 = null;
				bool isThisSln = referenced_object_id != 0;

				if (isThisSln) {
					loc10 = loc2[referenced_object_id];
					loc11 = loc3[referenced_object_id][referenced_column];
				} else {

				}
				Dictionary<int, ForeignKeyInfo> loc12 = null;
				ForeignKeyInfo loc13 = null;
				if (!fkColumns.TryGetValue(object_id, out loc12)) {
					fkColumns.Add(object_id, loc12 = new Dictionary<int, ForeignKeyInfo>());
				}
				if (!loc12.TryGetValue(fk_id, out loc13)) {
					if (isThisSln) {
						loc13 = new ForeignKeyInfo(loc2[object_id], loc10);
					} else {
						loc13 = new ForeignKeyInfo(referenced_db, referenced_table, is_foreign_key);
					}
					loc12.Add(fk_id, loc13);
				}
				loc13.Columns.Add(loc9);

				if (isThisSln) {
					loc13.ReferencedColumns.Add(loc11);
				} else {
					loc13.ReferencedColumnNames.Add(referenced_column);
				}
			}
			foreach (int object_id in fkColumns.Keys) {
				foreach (ForeignKeyInfo fk in fkColumns[object_id].Values) {
					loc2[object_id].ForeignKeys.Add(fk);
				}
			}

			foreach (int loc4 in loc3.Keys) {
				foreach (ColumnInfo loc5 in loc3[loc4].Values) {
					loc2[loc4].Columns.Add(loc5);
					if (loc5.IsIdentity) {
						loc2[loc4].Identitys.Add(loc5);
					}
					if (loc5.IsClustered) {
						loc2[loc4].Clustereds.Add(loc5);
					}
					if (loc5.IsPrimaryKey) {
						loc2[loc4].PrimaryKeys.Add(loc5);
					}
				}
			}
			loc1 = _tables = new List<TableInfo>();
			foreach (TableInfo loc4 in loc2.Values) {
				if (loc4.PrimaryKeys.Count == 0 && loc4.Uniques.Count > 0) {
					foreach (ColumnInfo loc5 in loc4.Uniques[0]) {
						loc5.IsPrimaryKey = true;
						loc4.PrimaryKeys.Add(loc5);
					}
				}
				this.Sort(loc4);
				loc1.Add(loc4);
			}

			loc2.Clear();
			loc3.Clear();
			return loc1;
		}

		protected virtual void Sort(TableInfo table) {
			table.PrimaryKeys.Sort(delegate (ColumnInfo c1, ColumnInfo c2) {
				return c1.Name.CompareTo(c2.Name);
			});
			table.Columns.Sort(delegate(ColumnInfo c1, ColumnInfo c2) {
				int compare = c2.IsPrimaryKey.CompareTo(c1.IsPrimaryKey);
				if (compare == 0) {
					bool b1 = table.ForeignKeys.Find(delegate(ForeignKeyInfo fk) {
						return fk.Columns.Find(delegate(ColumnInfo c3) {
							return c3.Name == c1.Name;
						}) != null;
					}) != null;
					bool b2 = table.ForeignKeys.Find(delegate(ForeignKeyInfo fk) {
						return fk.Columns.Find(delegate(ColumnInfo c3) {
							return c3.Name == c2.Name;
						}) != null;
					}) != null;
					compare = b2.CompareTo(b1);
				}
				if (compare == 0) compare = c1.Name.CompareTo(c2.Name);
				return compare;
			});
		}

		#region IDisposable 成员

		public void Dispose() {
			if (_tables != null) {
				_tables.Clear();
			}
		}

		#endregion
	}
}
