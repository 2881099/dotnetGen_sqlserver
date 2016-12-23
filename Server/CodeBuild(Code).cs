using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;

namespace Server {

	internal partial class CodeBuild {
		public void SetOutput(bool[] outputs) {
			if (this._tables.Count == outputs.Length) {
				for (int a = 0; a < outputs.Length; a++) {
					this._tables[a].IsOutput = outputs[a];
				}
			}
		}

		public List<BuildInfo> Build(string solutionName, bool isSolution, bool isMakeAdmin, bool isDownloadRes) {
			Logger.remotor.Info("Build: " + solutionName + ",isSolution: " + isSolution + ",isMakeAdmin: " + isMakeAdmin + ",isDownloadRes: " + isDownloadRes + "(" + _client.Server + "," + _client.Username + "," + _client.Password + "," + _client.Database + ")");
			List<BuildInfo> loc1 = new List<BuildInfo>();

			//solutionName = CodeBuild.UFString(solutionName);
			string dbName = CodeBuild.UFString(CodeBuild.GetCSName(_client.Database));
			string connectionStringName = _client.Database + "ConnectionString";
			string basicName = "Build";

			string srcGuid = Guid.NewGuid().ToString().ToUpper();
			string slnGuid = Guid.NewGuid().ToString().ToUpper();
			string commonGuid = Guid.NewGuid().ToString().ToUpper();
			string dbGuid = Guid.NewGuid().ToString().ToUpper();
			string adminGuid = Guid.NewGuid().ToString().ToUpper();

			Dictionary<string, bool> isMakedHtmlSelect = new Dictionary<string, bool>();
			StringBuilder Model_Build_ExtensionMethods_cs = new StringBuilder();
			List<string> admin_controllers_syscontroller_init_sysdir = new List<string>();

			StringBuilder sb1 = new StringBuilder();
			StringBuilder sb2 = new StringBuilder();
			StringBuilder sb3 = new StringBuilder();
			StringBuilder sb4 = new StringBuilder();
			StringBuilder sb5 = new StringBuilder();
			StringBuilder sb6 = new StringBuilder();
			StringBuilder sb7 = new StringBuilder();
			StringBuilder sb8 = new StringBuilder();
			StringBuilder sb9 = new StringBuilder();
			StringBuilder sb10 = new StringBuilder();
			StringBuilder sb11 = new StringBuilder();
			StringBuilder sb12 = new StringBuilder();
			StringBuilder sb13 = new StringBuilder();
			StringBuilder sb14 = new StringBuilder();
			StringBuilder sb15 = new StringBuilder();
			StringBuilder sb16 = new StringBuilder();
			StringBuilder sb17 = new StringBuilder();
			StringBuilder sb18 = new StringBuilder();
			StringBuilder sb19 = new StringBuilder();
			StringBuilder sb20 = new StringBuilder();
			StringBuilder sb21 = new StringBuilder();
			StringBuilder sb22 = new StringBuilder();
			StringBuilder sb23 = new StringBuilder();
			StringBuilder sb24 = new StringBuilder();
			StringBuilder sb25 = new StringBuilder();
			StringBuilder sb26 = new StringBuilder();
			StringBuilder sb27 = new StringBuilder();
			StringBuilder sb28 = new StringBuilder();
			StringBuilder sb29 = new StringBuilder();
			AnonymousHandler clearSb = delegate () {
				sb1.Remove(0, sb1.Length);
				sb2.Remove(0, sb2.Length);
				sb3.Remove(0, sb3.Length);
				sb4.Remove(0, sb4.Length);
				sb5.Remove(0, sb5.Length);
				sb6.Remove(0, sb6.Length);
				sb7.Remove(0, sb7.Length);
				sb8.Remove(0, sb8.Length);
				sb9.Remove(0, sb9.Length);
				sb10.Remove(0, sb10.Length);
				sb11.Remove(0, sb11.Length);
				sb12.Remove(0, sb12.Length);
				sb13.Remove(0, sb13.Length);
				sb14.Remove(0, sb14.Length);
				sb15.Remove(0, sb15.Length);
				sb16.Remove(0, sb16.Length);
				sb17.Remove(0, sb17.Length);
				sb18.Remove(0, sb18.Length);
				sb19.Remove(0, sb19.Length);
				sb20.Remove(0, sb20.Length);
				sb21.Remove(0, sb21.Length);
				sb22.Remove(0, sb22.Length);
				sb23.Remove(0, sb23.Length);
				sb24.Remove(0, sb24.Length);
				sb25.Remove(0, sb25.Length);
				sb26.Remove(0, sb26.Length);
				sb27.Remove(0, sb27.Length);
				sb28.Remove(0, sb28.Length);
				sb29.Remove(0, sb29.Length);
			};

			if (isSolution) {
				#region solution.sln
				sb1.AppendFormat(CONST.sln, srcGuid, slnGuid, commonGuid, dbGuid, adminGuid, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"..\", solutionName, ".sln"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region global.json
				sb1.AppendFormat(CONST.global_json);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"..\", "global.json"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region Project Common
				#region BmwNet.cs
				sb1.AppendFormat(CONST.Common_BmwNet_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\BmwNet.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region IniHelper.cs
				sb1.AppendFormat(CONST.Common_IniHelper_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\IniHelper.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region JSDecoder.cs
				sb1.AppendFormat(CONST.Common_JSDecoder_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\JSDecoder.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Lib.cs
				sb1.AppendFormat(CONST.Common_Lib_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Lib.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region WinFormClass/Socket/BaseSocket.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Socket_BaseSocket_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Socket\BaseSocket.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/Socket/ClientSocket.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Socket_ClientSocket_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Socket\ClientSocket.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/Socket/ServerSocket.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Socket_ServerSocket_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Socket\ServerSocket.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/Robot.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_Robot_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\Robot.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region WinFormClass/WorkQueue.cs
				sb1.AppendFormat(CONST.Common_WinFormClass_WorkQueue_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\WinFormClass\WorkQueue.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region Common.xproj
				sb1.AppendFormat(CONST.xproj, commonGuid, "Common");
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\Common.xproj"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region project.json
				sb1.AppendFormat(CONST.Common_project_json);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, @"Common\project.json"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#endregion
			}

			foreach (TableInfo table in _tables) {
				if (table.IsOutput == false) continue;
				if (table.Type == "P") continue;

				if (table.Uniques.Count == 0) {
					throw new Exception("检查到表 “" + table.Owner + "." + table.Name + "” 没有设定惟一键！");
				}

				#region commom variable define
				string uClass_Name = CodeBuild.UFString(table.ClassName);
				string nClass_Name = table.ClassName;
				string nTable_Name = "[" + table.Owner + "].[" + table.Name + "]";
				string Class_Name_BLL_Full = string.Format(@"{0}.BLL.{1}", solutionName, uClass_Name);
				string Class_Name_Model_Full = string.Format(@"{0}.Model.{1}", solutionName, uClass_Name);

				string pkCsParam = "";
				string pkCsParamNoType = "";
				string pkSqlParamFormat = "";
				string pkSqlParam = "";
				string pkSpNotNull = "";
				string pkEvalsQuerystring = "";
				string CsParam1 = "";
				string CsParamNoType1 = "";
				string CsParam2 = "";
				string CsParamNoType2 = "";
				string pkMvcRoute = "";
				string orderBy = table.Clustereds.Count > 0 ?
					string.Join(", ", table.Clustereds.ConvertAll<string>(delegate (ColumnInfo cli) {
						return "a.[" + cli.Name + "]" + (cli.Orderby == DataSort.ASC ? string.Empty : string.Concat(" ", cli.Orderby));
					}).ToArray()) :
					string.Join(", ", table.Uniques[0].ConvertAll<string>(delegate (ColumnInfo cli) {
						return "a.[" + cli.Name + "]" + (cli.Orderby == DataSort.ASC ? string.Empty : string.Concat(" ", cli.Orderby));
					}).ToArray());

				int pkSqlParamFormat_idx = -1;
				foreach (ColumnInfo columnInfo in table.PrimaryKeys) {
					pkCsParam += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
					pkCsParamNoType += CodeBuild.UFString(columnInfo.Name) + ", ";
					pkSqlParamFormat += "[" + columnInfo.Name + "] = {" + ++pkSqlParamFormat_idx + "} AND ";
					pkSqlParam += "[" + columnInfo.Name + "] = @" + columnInfo.Name + " AND ";
					pkSpNotNull += "NOT @" + columnInfo.Name + " IS NULL AND ";
					pkEvalsQuerystring += string.Format("{0}=<%# Eval(\"{0}\") %>&", CodeBuild.UFString(columnInfo.Name));
					pkMvcRoute += "{" + CodeBuild.UFString(columnInfo.Name) + "}/";
				}
				pkCsParam = pkCsParam.Substring(0, pkCsParam.Length - 2);
				pkCsParamNoType = pkCsParamNoType.Substring(0, pkCsParamNoType.Length - 2);
				pkSqlParamFormat = pkSqlParamFormat.Substring(0, pkSqlParamFormat.Length - 5);
				pkSqlParam = pkSqlParam.Substring(0, pkSqlParam.Length - 5);
				pkSpNotNull = pkSpNotNull.Substring(0, pkSpNotNull.Length - 5);
				pkEvalsQuerystring = pkEvalsQuerystring.Substring(0, pkEvalsQuerystring.Length - 1);
				foreach (ColumnInfo columnInfo in table.Columns) {
					CsParam1 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
					CsParamNoType1 += CodeBuild.UFString(columnInfo.Name) + ", ";
					if (columnInfo.IsIdentity) {
						CsParamNoType2 += "null, ";
					} else {
						CsParam2 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						CsParamNoType2 += CodeBuild.UFString(columnInfo.Name) + ", ";
					}
				}
				CsParam1 = CsParam1.Substring(0, CsParam1.Length - 2);
				CsParamNoType1 = CsParamNoType1.Substring(0, CsParamNoType1.Length - 2);
				if (CsParam2.Length > 0) CsParam2 = CsParam2.Substring(0, CsParam2.Length - 2);
				if (CsParamNoType2.Length > 0) CsParamNoType2 = CsParamNoType2.Substring(0, CsParamNoType2.Length - 2);
				#endregion

				#region Model *.cs
				sb1.AppendFormat(
	@"using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace {0}.Model {{

	public partial class {1}Info {{
		#region fields
", solutionName, uClass_Name);
				int column_idx = -1;
				foreach (ColumnInfo column in table.Columns) {
					column_idx++;
					string csType = CodeBuild.GetCSType(column.Type);
					string nColumn_Name = column.Name;
					string uColumn_Name = CodeBuild.UFString(column.Name);

					sb1.AppendFormat(
	@"		private {0} _{1};
", csType, uColumn_Name);

					string tmpinfo = string.Empty;
					List<string> tsvarr = new List<string>();
					List<ForeignKeyInfo> fks = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk) {
						int fkc1idx = 0;
						string fkcsBy = "By";
						string fkcsParms = string.Empty;
						ColumnInfo fkc = fk.Columns.Find(delegate (ColumnInfo c1) {
							fkc1idx++;
							fkcsParms += "_" + CodeBuild.UFString(c1.Name) + ", ";
							return c1.Name == column.Name;
						});
						if (fk.ReferencedTable != null) {
							fk.ReferencedColumns.ForEach(delegate (ColumnInfo c1) {
								fkcsBy += CodeBuild.UFString(c1.Name) + "And";
							});
						} else {
							fk.ReferencedColumnNames.ForEach(delegate (string c1) {
								fkcsBy += CodeBuild.UFString(c1) + "And";
							});
						}
						if (fkc == null) return false;
						string FK_uClass_Name = fk.ReferencedTable != null ? CodeBuild.UFString(fk.ReferencedTable.ClassName) :
							CodeBuild.UFString(TableInfo.GetClassName(fk.ReferencedTableName));
						string FK_uClass_Name_full = fk.ReferencedTable != null ? FK_uClass_Name :
							string.Format(@"{0}.Model.{1}", solutionName, FK_uClass_Name);
						string FK_uEntry_Name = fk.ReferencedTable != null ? CodeBuild.GetCSName(fk.ReferencedTable.Name) :
							CodeBuild.GetCSName(TableInfo.GetEntryName(fk.ReferencedTableName));
						string tableNamefe3 = fk.ReferencedTable != null ? fk.ReferencedTable.Name :
							FK_uEntry_Name;
						string memberName = fk.Columns[0].Name.IndexOf(tableNamefe3) == -1 ? CodeBuild.LFString(tableNamefe3) :
							(CodeBuild.LFString(fk.Columns[0].Name.Substring(0, fk.Columns[0].Name.IndexOf(tableNamefe3)) + tableNamefe3));

						tsvarr.Add(string.Format(@"_obj_{0} = null;", memberName));
						if (fkc1idx == fk.Columns.Count) {
							fkcsBy = fkcsBy.Remove(fkcsBy.Length - 3);
							fkcsParms = fkcsParms.Remove(fkcsParms.Length - 2);
							if (fk.ReferencedColumns.Count > 0 && fk.ReferencedColumns[0].IsPrimaryKey ||
								fk.ReferencedTable == null && fk.ReferencedIsPrimaryKey) {
								fkcsBy = string.Empty;
							}
							sb1.AppendFormat(
		@"		private {0}Info _obj_{1};
", FK_uClass_Name_full, memberName);
							tmpinfo += string.Format(
		@"		public {0}Info Obj_{1} {{
			get {{
				if (_obj_{1} == null) _obj_{1} = {2}.BLL.{5}.GetItem{3}({4});
				return _obj_{1};
			}}
			internal set {{ _obj_{1} = value; }}
		}}
", FK_uClass_Name_full, memberName, solutionName, fkcsBy, fkcsParms, FK_uClass_Name);
						}
						return fkc != null;
					});
					if (fks.Count > 0) {
						string tmpsetvalue = string.Format(
@"		public {0} {1} {{
			get {{ return _{1}; }}
			set {{
				if (_{1} != value) ", csType, uColumn_Name);
						string tsvstr = string.Join(@"
					", tsvarr.ToArray());
						if (fks.Count > 1) {
							tmpsetvalue += string.Format(@"{{
					{0}
				}}", tsvstr);
						} else {
							tmpsetvalue += tsvstr;
						}
						tmpsetvalue += string.Format(@"
				_{0} = value;
			}}
		}}
", uColumn_Name);
						sb2.Append(tmpsetvalue);
						sb2.Append(tmpinfo);
					} else {
						sb2.AppendFormat(
	@"		public {0} {1} {{
			get {{ return _{1}; }}
			set {{ _{1} = value; }}
		}}
", csType, uColumn_Name);
					}
					sb3.AppendFormat("{0} {1}, ", csType, uColumn_Name);
					sb4.AppendFormat(
	@"			_{0} = {0};
", uColumn_Name);
					sb5.AppendFormat(@"
				__jsonIgnore.ContainsKey(""{0}"") ? string.Empty : string.Format("", {0} : {{0}}"", {1}), ", uColumn_Name, CodeBuild.GetToStringFieldConcat(column));
					sb10.AppendFormat(@"
			if (!__jsonIgnore.ContainsKey(""{0}"")) ht[""{0}""] = {0};", uColumn_Name);
					sb7.AppendFormat(@"
				{0}, ""|"",", GetToStringStringify(column));
                    sb8.AppendFormat(@"
			if (string.Compare(""null"", ret[{2}]) != 0) _{0} = {1};", 
                        uColumn_Name, string.Format(CodeBuild.GetStringifyParse(column.Type), "ret[" + column_idx +"]"), column_idx);
                }

				if (sb2.Length != 0) {
					sb2.Remove(sb2.Length - 2, 2);
					sb3.Remove(sb3.Length - 2, 2);
					sb5.Remove(sb5.Length - 2, 2);
					sb7.Remove(sb7.Length - 6, 6);
				}

				Dictionary<string, string> dic_objs = new Dictionary<string, string>();
				// m -> n
				_tables.ForEach(delegate (TableInfo t2) {
					if (t2.ForeignKeys.Count > 2) {
						foreach(TableInfo t3 in _tables) {
							if (t3.FullName == t2.FullName) continue;
							ForeignKeyInfo fk3 = t3.ForeignKeys.Find(delegate (ForeignKeyInfo ffk3) {
								return ffk3.ReferencedTable.FullName == t2.FullName;
							});
							if (fk3 != null) {
								if (fk3.Columns[0].IsPrimaryKey) return;
							}
						}
					}
					ForeignKeyInfo fk_Common = null;
					ForeignKeyInfo fk = t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffk) {
						if (ffk.ReferencedTable.FullName == table.FullName/* && 
							ffk.Table.FullName != table.FullName*/) { //注释这行条件为了增加 parent_id 的 obj 对象
							fk_Common = ffk;
							return true;
						}
						return false;
					});
					if (fk == null) return;
					//if (fk.Table.FullName == table.FullName) return; //注释这行条件为了增加 parent_id 的 obj 对象
					List<ForeignKeyInfo> fk2 = t2.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk2) {
						return ffk2 != fk;
					});
					// 1 -> 1
					ForeignKeyInfo fk1v1 = table.ForeignKeys.Find(delegate (ForeignKeyInfo ffk2) {
						return ffk2.ReferencedTable.FullName == t2.FullName
							&& ffk2.ReferencedColumns[0].IsPrimaryKey && ffk2.Columns[0].IsPrimaryKey; //这行条件为了增加 parent_id 的 obj 对象
					});
					if (fk1v1 != null) return;

					//t2.Columns
					string t2name = t2.Name;
					string tablename = table.Name;
					string addname = t2name;
					if (t2name.StartsWith(tablename + "_")) {
						addname = t2name.Substring(tablename.Length + 1);
					} else if (t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(addname.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(t2name.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + fk2[0].ReferencedTable.Name)) {
						addname = t2name;
					}

					string parms1 = "";
					string parmsNoneType1 = "";
					string parms2 = "";
					string parmsNoneType2 = "";
					string parms3 = "";
					string parmsNoneType3 = "";
					string parms4 = "";
					string parmsNoneType4 = "";
					string pkNamesNoneType = "";
					string updateDiySet = "";
					string add_or_flag = "Add";
					int ms = 0;
					foreach (ColumnInfo columnInfo in t2.Columns) {
						if (columnInfo.Name == fk.Columns[0].Name) {
							parmsNoneType2 += "this." + CodeBuild.UFString(table.PrimaryKeys[0].Name) + ", ";
							parmsNoneType4 += "this." + CodeBuild.UFString(table.PrimaryKeys[0].Name) + ", ";
							if (columnInfo.IsPrimaryKey) pkNamesNoneType += "this." + CodeBuild.UFString(table.PrimaryKeys[0].Name) + ", ";
							continue;
						}
						if (columnInfo.IsPrimaryKey) pkNamesNoneType += CodeBuild.UFString(columnInfo.Name) + ", ";
						else updateDiySet += string.Format("\r\n\t\t\t\t\t.Set{0}({0})", CodeBuild.UFString(columnInfo.Name));

						if (columnInfo.IsIdentity)
							continue;
						parms2 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsNoneType2 += CodeBuild.UFString(columnInfo.Name) + ", ";

						ForeignKeyInfo fkk3 = t2.ForeignKeys.Find(delegate (ForeignKeyInfo fkk33) {
							return fkk33.Columns[0].Name == columnInfo.Name;
						});
						if (fkk3 == null) {
							parms1 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
							parmsNoneType1 += CodeBuild.UFString(columnInfo.Name) + ", ";
						} else {
							string fkk3_ReferencedTable_ObjName = fkk3.ReferencedTable.Name;
							string endStr = "_" + fkk3.ReferencedTable.Name + "_" + fkk3.ReferencedColumns[0].Name;
							if (columnInfo.Name.EndsWith(endStr))
								fkk3_ReferencedTable_ObjName = columnInfo.Name.Remove(columnInfo.Name.Length - fkk3.ReferencedColumns[0].Name.Length - 1);

							fkk3_ReferencedTable_ObjName = CodeBuild.UFString(fkk3_ReferencedTable_ObjName);
							parms1 += CodeBuild.UFString(fkk3.ReferencedTable.ClassName) + "Info " + fkk3_ReferencedTable_ObjName + ", ";
							parmsNoneType1 += fkk3_ReferencedTable_ObjName + "." + CodeBuild.UFString(fkk3.ReferencedColumns[0].Name) + ", ";
							parms3 += CodeBuild.UFString(fkk3.ReferencedTable.ClassName) + "Info " + fkk3_ReferencedTable_ObjName + ", ";
							parmsNoneType3 += fkk3_ReferencedTable_ObjName + "." + CodeBuild.UFString(fkk3.ReferencedColumns[0].Name) + ", ";

							parms4 += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
							parmsNoneType4 += CodeBuild.UFString(columnInfo.Name) + ", ";
							if (add_or_flag != "Flag" && fk.Columns[0].IsPrimaryKey) //中间表关系键，必须为主键
								t2.Uniques.ForEach(delegate (List<ColumnInfo> cs) {
									if (cs.Count < 2) return;
									ms = 0;
									foreach (ColumnInfo c in cs) {
										if (t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffkk2) {
											return ffkk2.Columns[0].Name == c.Name;
										}) != null) ms++;
									}
									if (ms == cs.Count) {
										add_or_flag = "Flag";
									}
								});
						}
					}
					if (parms1.Length > 0) parms1 = parms1.Remove(parms1.Length - 2);
					if (parmsNoneType1.Length > 0) parmsNoneType1 = parmsNoneType1.Remove(parmsNoneType1.Length - 2);
					if (parms2.Length > 0) parms2 = parms2.Remove(parms2.Length - 2);
					if (parmsNoneType2.Length > 0) parmsNoneType2 = parmsNoneType2.Remove(parmsNoneType2.Length - 2);
					if (parms3.Length > 0) parms3 = parms3.Remove(parms3.Length - 2);
					if (parmsNoneType3.Length > 0) parmsNoneType3 = parmsNoneType3.Remove(parmsNoneType3.Length - 2);
					if (parms4.Length > 0) parms4 = parms4.Remove(parms4.Length - 2);
					if (parmsNoneType4.Length > 0) parmsNoneType4 = parmsNoneType4.Remove(parmsNoneType4.Length - 2);
					if (pkNamesNoneType.Length > 0) pkNamesNoneType = pkNamesNoneType.Remove(pkNamesNoneType.Length - 2);

					if (add_or_flag == "Flag") {
						if (parms1 != parms2)
							sb6.AppendFormat(@"
		public {0}Info Flag{1}({2}) {{
			return Flag{1}({3});
		}}", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms1, parmsNoneType1);
						sb6.AppendFormat(@"
		public {0}Info Flag{1}({2}) {{
			{0}Info item = {4}.BLL.{0}.GetItem({5});
			if (item == null) item = {4}.BLL.{0}.Insert({3});{6}
			return item;
		}}
", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms2, parmsNoneType2, solutionName, pkNamesNoneType, updateDiySet.Length > 0 ? "\r\n\t\t\telse item.UpdateDiy" + updateDiySet + ".ExecuteNonQuery();" : string.Empty);
					} else {
						if (parms1 != parms2)
							sb6.AppendFormat(@"
		public {0}Info Add{1}({2}) {{
			return Add{1}({3});
		}}", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms1, parmsNoneType1);
						sb6.AppendFormat(@"
		public {0}Info Add{1}({2}) {{
			return {4}.BLL.{0}.Insert({3});
		}}
", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms2, parmsNoneType2, solutionName);
					}

					if (add_or_flag == "Flag") {
						string deleteByUniqui = string.Empty;
						for (int deleteByUniqui_a = 0; deleteByUniqui_a < fk.Table.Uniques.Count; deleteByUniqui_a++)
							if (fk.Table.Uniques[deleteByUniqui_a].Count > 1 && fk.Table.Uniques[deleteByUniqui_a][0].IsPrimaryKey == false) {
								foreach (ColumnInfo deleteByuniquiCol in fk.Table.Uniques[deleteByUniqui_a])
									deleteByUniqui = deleteByUniqui + "And" + CodeBuild.UFString(deleteByuniquiCol.Name);
								deleteByUniqui = "By" + deleteByUniqui.Substring(3);
								break;
							}
						sb6.AppendFormat(@"
		public int Unflag{1}({2}) {{
			return Unflag{1}({3});
		}}
		public int Unflag{1}({4}) {{
			return {6}.BLL.{0}.Delete{9}({5});
		}}
		public int Unflag{1}ALL() {{
			return {6}.BLL.{0}.DeleteBy{8}(this.{7});
		}}
", CodeBuild.UFString(t2.ClassName), CodeBuild.UFString(addname), parms3, parmsNoneType3, parms4, parmsNoneType4,
	solutionName, CodeBuild.UFString(table.PrimaryKeys[0].Name), CodeBuild.UFString(fk.Columns[0].Name), deleteByUniqui);

						if (ms > 2) {

						} else {
							string civ = string.Format(GetCSTypeValue(table.PrimaryKeys[0].Type), "_" + CodeBuild.UFString(table.PrimaryKeys[0].Name));
							string f5 = tablename;
							if (addname != f5) {
								string fk20_ReferencedTable_Name = fk2[0].ReferencedTable.Name;
								string fk_ReferencedTable_Name = fk.ReferencedTable.Name;
								if (addname.EndsWith("_" + fk20_ReferencedTable_Name))
									f5 = addname.Remove(addname.Length - fk20_ReferencedTable_Name.Length - 1);
								else if (string.Compare(t2name, fk20_ReferencedTable_Name + "_" + fk_ReferencedTable_Name) != 0 &&
									string.Compare(t2name, fk_ReferencedTable_Name + "_" + fk20_ReferencedTable_Name) != 0)
									f5 = t2name;
							}
							string objs_value = string.Format(@"
		private List<{0}Info> _obj_{1}s;
		public List<{0}Info> Obj_{1}s {{
			get {{
				if (_obj_{1}s == null) _obj_{1}s = {2}.BLL.{0}.SelectBy{5}_{4}({3}).ToList();
				return _obj_{1}s;
			}}
		}}", CodeBuild.UFString(fk2[0].ReferencedTable.ClassName), CodeBuild.LFString(addname), solutionName, civ, table.PrimaryKeys[0].Name, CodeBuild.UFString(f5));
							string objs_key = string.Format("Obj_{0}s", CodeBuild.LFString(addname));
							if (dic_objs.ContainsKey(objs_key))
								dic_objs[objs_key] = objs_value;
							else
								dic_objs.Add(objs_key, objs_value);
						}
					} else {
						string f2 = fk.Columns[0].Name.CompareTo("parent_id") == 0 ? t2name : fk.Columns[0].Name.Replace(tablename + "_" + table.PrimaryKeys[0].Name, "") + CodeBuild.LFString(t2name);
						string objs_value = string.Format(@"
		private List<{0}Info> _obj_{1}s;
		public List<{0}Info> Obj_{1}s {{
			get {{
				if (_obj_{1}s == null) _obj_{1}s = {2}.BLL.{0}.SelectBy{3}(_{4}).Limit(500).ToList();
				return _obj_{1}s;
			}}
		}}", CodeBuild.UFString(t2.ClassName), f2, solutionName, CodeBuild.UFString(fk.Columns[0].Name), CodeBuild.UFString(table.PrimaryKeys[0].Name));
						string objs_key = string.Format("Obj_{0}s", f2);
						if (!dic_objs.ContainsKey(objs_key))
							dic_objs.Add(objs_key, objs_value);
					}
				});
				string[] dic_objs_values = new string[dic_objs.Count];
				dic_objs.Values.CopyTo(dic_objs_values, 0);
				sb9.Append(string.Join("", dic_objs_values));

				sb6.Insert(0, string.Format(@"
		public {0}.DAL.{1}.SqlUpdateBuild UpdateDiy {{
			get {{ return {0}.BLL.{1}.UpdateDiy(this, _{2}); }}
		}}", solutionName, uClass_Name, pkCsParamNoType.Replace(", ", ", _")));

				sb1.AppendFormat(
	@"		#endregion

		public {0}Info() {{ }}

		public {0}Info({1}) {{
{2}		}}

		#region 独创的序列化，反序列化
		protected static readonly string StringifySplit = ""@<{0}(Info]?#>"";
		public string Stringify() {{
			return string.Concat({7});
		}}
		public {0}Info(string stringify) {{
			string[] ret = stringify.Split(new char[] {{ '|' }}, {6}, StringSplitOptions.None);
			if (ret.Length != {6}) throw new Exception(""格式不正确，{0}Info："" + stringify);{8}
		}}
		#endregion

		#region override
		private static Dictionary<string, bool> __jsonIgnore;
		private static object __jsonIgnore_lock = new object();
		public override string ToString() {{
			this.Init__jsonIgnore();
			string json = string.Concat({3}, "" }}"");
			return string.Concat(""{{"", json.Substring(1));
		}}
		public IDictionary ToBson() {{
			this.Init__jsonIgnore();
			IDictionary ht = new Hashtable();{10}
			return ht;
		}}
		private void Init__jsonIgnore() {{
			if (__jsonIgnore == null) {{
				lock (__jsonIgnore_lock) {{
					if (__jsonIgnore == null) {{
						FieldInfo field = typeof({0}Info).GetField(""JsonIgnore"");
						__jsonIgnore = new Dictionary<string, bool>();
						if (field != null) {{
							string[] fs = string.Concat(field.GetValue(null)).Split(',');
							foreach (string f in fs) if (!string.IsNullOrEmpty(f)) __jsonIgnore[f] = true;
						}}
					}}
				}}
			}}
		}}
		public override bool Equals(object obj) {{
			{0}Info item = obj as {0}Info;
			if (item == null) return false;
			return this.ToString().Equals(item.ToString());
		}}
		public override int GetHashCode() {{
			return this.ToString().GetHashCode();
		}}
		public static bool operator ==({0}Info op1, {0}Info op2) {{
			if (object.Equals(op1, null)) return object.Equals(op2, null);
			return op1.Equals(op2);
		}}
		public static bool operator !=({0}Info op1, {0}Info op2) {{
			return !(op1 == op2);
		}}
		#endregion

		#region properties
{4}{9}
		#endregion
{5}
	}}
}}

", uClass_Name, sb3.ToString(), sb4.ToString(), sb5.ToString(), sb2.ToString(), sb6.ToString(), table.Columns.Count, sb7.ToString(), sb8.ToString(), sb9.ToString(), sb10.ToString());

				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\Model\", basicName, @"\", uClass_Name, "Info.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();

				Model_Build_ExtensionMethods_cs.AppendFormat(@"
		public static string ToJson(this {0}Info item) {{ return string.Concat(item); }}
		public static string ToJson(this {0}Info[] items) {{ return GetJson(items); }}
		public static string ToJson(this IEnumerable<{0}Info> items) {{ return GetJson(items); }}
		public static IDictionary[] ToBson(this {0}Info[] items) {{ return GetBson(items); }}
		public static IDictionary[] ToBson(this IEnumerable<{0}Info> items) {{ return GetBson(items); }}
", uClass_Name);
				#endregion

				#region DAL *.cs

				#region use t-sql
				string sqlTable = "declare @table table(";
				string sqlFields = "";
				string sqlDelete = string.Format("DELETE FROM {0} ", nTable_Name);
				string sqlUpdate = string.Format("UPDATE {0} SET ", nTable_Name);
				string sqlInsert = string.Format("INSERT INTO {0}(", nTable_Name);
				string sqlSelect = string.Format("SELECT <top> \" + GetFields(null) + \"");
				string temp1 = string.Empty;
				string temp2 = string.Empty;
				string temp3 = string.Empty;
				string temp4 = string.Empty;
				foreach (ColumnInfo columnInfo in table.Columns) {
					if (columnInfo.IsIdentity == false) {
						temp1 += string.Format("[{0}] = @{0}, ", columnInfo.Name);
						temp2 += string.Format("[{0}], ", columnInfo.Name);
						temp3 += string.Format("@{0}, ", columnInfo.Name);
					}
					temp4 += string.Format("a.[{0}], ", columnInfo.Name);
					sqlTable += string.Format("[{0}] {1},", columnInfo.Name, columnInfo.SqlType);
				}
				temp1 = temp1.Substring(0, temp1.Length - 2);
				temp2 = temp2.Substring(0, temp2.Length - 2);
				temp3 = temp3.Substring(0, temp3.Length - 2);
				temp4 = temp4.Substring(0, temp4.Length - 2);
				sqlTable = sqlTable.Substring(0, sqlTable.Length - 1) + ")\\r\\n";
				sqlFields = temp4;
				sqlDelete += "WHERE ";
				sqlUpdate += temp1 + string.Format(" WHERE {0}", pkSqlParam);
				sqlInsert += string.Format("{0}) OUTPUT \" + Field.Replace(\"a.\", \"INSERTED.\") + \" INTO @table VALUES({1})\\r\\nselect * from @table", temp2, temp3);
				sqlSelect += string.Format(", row_number() over(<order by>) AS rownum FROM {0}", nTable_Name);

				temp1 = "";
				temp2 = "";
				temp3 = "";
				temp4 = "";

				sb1.AppendFormat(
	@"using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using {0}.Model;

namespace {0}.DAL {{

	public partial class {1} : SqlHelper.IDAL {{
		#region transact-sql define
		public string Table {{ get {{ return TSQL.Table; }} }}
		public string Field {{ get {{ return TSQL.Field; }} }}
		public string Sort {{ get {{ return TSQL.Sort; }} }}
		internal class TSQL {{
			internal static readonly string Table = ""{3}"";
			internal static readonly string Field = ""{5}"";
			internal static readonly string Sort = ""{6}"";
			public static readonly string Delete = ""DELETE FROM {3} WHERE "";
			public static readonly string Insert = ""{2}{4}"";
		}}
		#endregion

		#region common call
		protected static SqlParameter GetParameter(string name, SqlDbType type, int size, object value) {{
			SqlParameter parm = new SqlParameter(name, type, size);
			parm.Value = value;
			return parm;
		}}
		protected static SqlParameter[] GetParameters({1}Info item) {{
			return new SqlParameter[] {{
{7}}};
		}}", solutionName, uClass_Name, sqlTable, nTable_Name, sqlInsert, sqlFields, orderBy, CodeBuild.AppendParameters(table, "				"));

				sb1.AppendFormat(@"
		public {0}Info GetItem(IDataReader dr) {{
			int index = -1;
			return GetItem(dr, ref index) as {0}Info;
		}}
		public object GetItem(IDataReader dr, ref int index) {{
			return new {0}Info(", uClass_Name);

				foreach (ColumnInfo columnInfo in table.Columns) {
					if (columnInfo.Type == SqlDbType.Image ||
						columnInfo.Type == SqlDbType.Binary ||
						columnInfo.Type == SqlDbType.VarBinary) {
						if (sb4.Length == 0) {
							sb4.AppendFormat(@"
		public byte[] GetBytes(IDataReader dr, int index) {{
			if (dr.IsDBNull(index)) return null;
			using(MemoryStream ms = new MemoryStream()) {{
				byte[] bt = new byte[1048576 * 8];
				int size = 0;
				while ((size = (int)dr.GetBytes(index, ms.Position, bt, 0, bt.Length)) > 0) ms.Write(bt, 0, size);
				return ms.ToArray();
			}}
		}}");
						}
						sb1.AppendFormat(
	@"
				GetBytes(dr, ++index), ");
					} else {
						sb1.AppendFormat(
	@"
				dr.IsDBNull(++index) ? null : {0}dr.{1}(index), ", CodeBuild.GetDbToCsConvert(columnInfo.Type), CodeBuild.GetDataReaderMethod(columnInfo.Type));
					}
				}
				sb1 = sb1.Remove(sb1.Length - 2, 2);
				sb1.AppendFormat(@");
		}}");
				sb1.Append(sb4.ToString());
				sb1.AppendFormat(@"
		public SqlHelper.SelectBuild<{0}Info> Select {{
			get {{ return SqlHelper.SelectBuild<{0}Info>.From(this); }}
		}}
		#endregion", uClass_Name, table.Columns.Count + 1);
				Dictionary<string, bool> del_exists = new Dictionary<string, bool>();
				foreach (List<ColumnInfo> cs in table.Uniques) {
					string parms = string.Empty;
					string parmsBy = "By";
					string sqlParms = string.Empty;
					string sqlParmsA = string.Empty;
					string sqlParmsANoneType = string.Empty;
					int sqlParmsAIndex = 0;
					foreach (ColumnInfo columnInfo in cs) {
						parms += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsBy += CodeBuild.UFString(columnInfo.Name) + "And";
						sqlParms += "[" + columnInfo.Name + "] = @" + columnInfo.Name + " AND ";
						sqlParmsA += "a.[" + columnInfo.Name + "] = {" + sqlParmsAIndex++ + "} AND ";
						sqlParmsANoneType += CodeBuild.UFString(columnInfo.Name) + ", ";
					}
					parms = parms.Substring(0, parms.Length - 2);
					parmsBy = parmsBy.Substring(0, parmsBy.Length - 3);
					sqlParms = sqlParms.Substring(0, sqlParms.Length - 5);
					sqlParmsA = sqlParmsA.Substring(0, sqlParmsA.Length - 5);
					sqlParmsANoneType = sqlParmsANoneType.Substring(0, sqlParmsANoneType.Length - 2);
					del_exists.Add(parms, true);
					sb2.AppendFormat(@"
		public int Delete{2}({0}) {{
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, ""{1}""), 
{3});
		}}", parms, sqlParms, cs[0].IsPrimaryKey ? string.Empty : parmsBy, CodeBuild.AppendParameters(cs, "				"));

					sb3.AppendFormat(@"
		public {0}Info GetItem{3}({1}) {{
			return this.Select.Where(""{2}"", {4}).ToOne();
		}}", uClass_Name, parms, sqlParmsA, cs[0].IsPrimaryKey ? string.Empty : parmsBy, sqlParmsANoneType);
				}
				table.ForeignKeys.ForEach(delegate (ForeignKeyInfo fkk) {
					string parms = string.Empty;
					string parmsBy = "By";
					string sqlParms = string.Empty;
					foreach (ColumnInfo columnInfo in fkk.Columns) {
						parms += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsBy += CodeBuild.UFString(columnInfo.Name) + "And";
						sqlParms += "[" + columnInfo.Name + "] = @" + columnInfo.Name + " AND ";
					}
					parms = parms.Substring(0, parms.Length - 2);
					parmsBy = parmsBy.Substring(0, parmsBy.Length - 3);
					sqlParms = sqlParms.Substring(0, sqlParms.Length - 5);
					if (del_exists.ContainsKey(parms)) return;
					del_exists.Add(parms, true);

					sb2.AppendFormat(@"
		public int Delete{2}({0}) {{
			return SqlHelper.ExecuteNonQuery(string.Concat(TSQL.Delete, ""{1}""), 
{3});
		}}", parms, sqlParms, parmsBy, CodeBuild.AppendParameters(fkk.Columns, "				"));
				});

				foreach (ColumnInfo columnInfo in table.Columns) {
					if (columnInfo.IsIdentity ||
						columnInfo.IsPrimaryKey ||
						table.PrimaryKeys.FindIndex(delegate (ColumnInfo pkf) { return pkf.Name == columnInfo.Name; }) != -1) continue;
					string valueParm = CodeBuild.AppendParameters(columnInfo, "");
					valueParm = valueParm.Remove(valueParm.LastIndexOf(", ") + 2);
					sb5.AppendFormat(@"
			public SqlUpdateBuild Set{0}({2} value) {{
				if (_item != null) _item.{0} = value;
				return this.Set(""[{1}]"", string.Concat(""@{1}_"", _parameters.Count), 
					{3}value));
			}}", CodeBuild.UFString(columnInfo.Name), columnInfo.Name, CodeBuild.GetCSType(columnInfo.Type), valueParm.Replace("\"@" + columnInfo.Name + "\"", "string.Concat(\"@" + columnInfo.Name + "_\", _parameters.Count)"));
					if ((columnInfo.Type == SqlDbType.BigInt ||
						columnInfo.Type == SqlDbType.Decimal ||
						columnInfo.Type == SqlDbType.Float ||
						columnInfo.Type == SqlDbType.Int ||
						columnInfo.Type == SqlDbType.Money ||
						columnInfo.Type == SqlDbType.Real ||
						columnInfo.Type == SqlDbType.SmallInt ||
						columnInfo.Type == SqlDbType.SmallMoney ||
						columnInfo.Type == SqlDbType.TinyInt) && 
						table.ForeignKeys.FindIndex(delegate(ForeignKeyInfo fkf) { return fkf.Columns.FindIndex(delegate (ColumnInfo fkfpkf) { return fkfpkf.Name == columnInfo.Name; }) != -1; }) == -1) {
						
						sb5.AppendFormat(@"
			public SqlUpdateBuild Set{0}Increment({2} value) {{
				if (_item != null) _item.{0} += value;
				return this.Set(""[{1}]"", string.Concat(""[{1}] + @{1}_"", _parameters.Count), 
					{3}value));
			}}", CodeBuild.UFString(columnInfo.Name), columnInfo.Name, CodeBuild.GetCSType(columnInfo.Type), valueParm.Replace("\"@" + columnInfo.Name + "\"", "string.Concat(\"@" + columnInfo.Name + "_\", _parameters.Count)"));
					}
					sb6.AppendFormat(@"
				.Set{0}(item.{0})", CodeBuild.UFString(columnInfo.Name));
				}

				sb1.AppendFormat(@"
{1}

		public int Update({0}Info item) {{
			return new SqlUpdateBuild(null, item.{7}){8}.ExecuteNonQuery();
		}}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {{
			protected {0}Info _item;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			public SqlUpdateBuild({0}Info item, {3}) {{
				_item = item;
				_where = SqlHelper.Addslashes(""{4}"", {5});
			}}
			public SqlUpdateBuild() {{ }}
			public override string ToString() {{
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception(""防止 {9}.DAL.{0}.SqlUpdateBuild 误修改，请必须设置 where 条件。"");
				return string.Concat(""UPDATE "", TSQL.Table, "" SET "", _fields.Substring(1), "" WHERE "", _where);
			}}
			public int ExecuteNonQuery() {{
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				return SqlHelper.ExecuteNonQuery(this.ToString(), _parameters.ToArray());
			}}
			public SqlUpdateBuild Where(string filterFormat, params object[] values) {{
				if (!string.IsNullOrEmpty(_where)) _where = string.Concat(_where, "" AND "");
				_where = string.Concat(_where, ""("", SqlHelper.Addslashes(filterFormat, values), "")"");
				return this;
			}}
			public SqlUpdateBuild Set(string field, string value, params SqlParameter[] parms) {{
				if (value.IndexOf('\'') != -1) throw new Exception(""{9}.DAL.{0}.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。"");
				_fields = string.Concat(_fields, "", "", field, "" = "", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}}{6}
		}}
		#endregion

		public {0}Info Insert({0}Info item) {{
			{0}Info ret = null;
			SqlHelper.ExecuteReader(dr => ret = GetItem(dr), TSQL.Insert, GetParameters(item));
			return ret;
		}}
{2}
	}}
}}", uClass_Name, sb2.ToString(), sb3.ToString(), pkCsParam, pkSqlParamFormat, pkCsParamNoType, sb5.ToString(),
pkCsParamNoType.Replace(", ", ", item."), sb6.ToString(), solutionName);
				#endregion

				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\", basicName, @"\", uClass_Name, ".cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region BLL *.cs
				sb1.AppendFormat(
	@"using System;
using System.Collections.Generic;
using System.Linq;
using {0}.Model;

namespace {0}.BLL {{

	public partial class {1} {{

		protected static readonly {0}.DAL.{1} dal = new {0}.DAL.{1}();
		protected static readonly int itemCacheTimeout;

		static {1}() {{
			var ini = IniHelper.LoadIni(@""../web.config"");
			if (ini.ContainsKey(""appSettings"") && !int.TryParse(ini[""appSettings""][""{0}_ITEM_CACHE_TIMEOUT_{1}""], out itemCacheTimeout))
				int.TryParse(ini[""appSettings""][""{0}_ITEM_CACHE_TIMEOUT""], out itemCacheTimeout);
		}}

		#region delete, update, insert", solutionName, uClass_Name);

				string removeCacheCode = string.Format(@"
			if (itemCacheTimeout > 0) RemoveCache(GetItem({1}));", uClass_Name, pkCsParamNoType);
				Dictionary<string, bool> del_exists2 = new Dictionary<string, bool>();
				foreach (List<ColumnInfo> cs in table.Uniques) {
					string parms = string.Empty;
					string parmsBy = "By";
					string parmsNoneType = string.Empty;
					string parmsNodeTypeUpdateCacheRemove = string.Empty;
					string cacheCond = string.Empty;
					string cacheRemoveCode = string.Empty;
					foreach (ColumnInfo columnInfo in cs) {
						parms += CodeBuild.GetCSType(columnInfo.Type) + " " + CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsBy += CodeBuild.UFString(columnInfo.Name) + "And";
						parmsNoneType += CodeBuild.UFString(columnInfo.Name) + ", ";
						parmsNodeTypeUpdateCacheRemove += "item." + CodeBuild.UFString(columnInfo.Name) + ", \"_,_\", ";
						cacheCond += CodeBuild.UFString(columnInfo.Name) + " == null || ";
					}
					parms = parms.Substring(0, parms.Length - 2);
					parmsBy = parmsBy.Substring(0, parmsBy.Length - 3);
					parmsNoneType = parmsNoneType.Substring(0, parmsNoneType.Length - 2);
					parmsNodeTypeUpdateCacheRemove = parmsNodeTypeUpdateCacheRemove.Substring(0, parmsNodeTypeUpdateCacheRemove.Length - 9);
					cacheCond = cacheCond.Substring(0, cacheCond.Length - 4);

					del_exists2.Add(parms, true);
					sb2.AppendFormat(@"
		public static int Delete{2}({0}) {{{3}
			return dal.Delete{2}({1});
		}}", parms, parmsNoneType, cs[0].IsPrimaryKey ? string.Empty : parmsBy, cs[0].IsPrimaryKey ? removeCacheCode : string.Empty);


					sb3.AppendFormat(@"
		public static {1}Info GetItem{2}({4}) {{
			if ({6}) return null;
			if (itemCacheTimeout <= 0) return dal.GetItem{2}({5});
			string key = string.Concat(""{0}_BLL_{1}{2}_"", {3});
			string value = RedisHelper.Get(key);
			if (!string.IsNullOrEmpty(value))
				try {{ return new {1}Info(value); }} catch {{ }}
			{1}Info item = dal.GetItem{2}({5});
			if (item == null) return null;
			RedisHelper.Set(key, item.Stringify(), itemCacheTimeout);
			return item;
		}}", solutionName, uClass_Name, cs[0].IsPrimaryKey ? string.Empty : parmsBy, parmsNodeTypeUpdateCacheRemove.Replace("item.", ""),
		parms, parmsNoneType, cacheCond);

					sb4.AppendFormat(@"
			RedisHelper.Remove(string.Concat(""{0}_BLL_{1}{2}_"", {3}));", solutionName, uClass_Name, cs[0].IsPrimaryKey ? string.Empty : parmsBy, parmsNodeTypeUpdateCacheRemove);
				}

				sb2.AppendFormat(@"|deleteby_fk|");

				//string UpdateDiyPkParms = string.Empty;
				//string UpdateDiyPkParmsNoneType = string.Empty;
				//table.PrimaryKeys.ForEach(delegate (ColumnInfo UpdateDiyPk) {
				//	UpdateDiyPkParms += CodeBuild.GetCSType(UpdateDiyPk.Type) + " " + CodeBuild.UFString(UpdateDiyPk.Name) + ", ";
				//	UpdateDiyPkParmsNoneType += CodeBuild.UFString(UpdateDiyPk.Name) + ", ";
				//});
				//UpdateDiyPkParms = UpdateDiyPkParms.Substring(0, UpdateDiyPkParms.Length - 2);
				//UpdateDiyPkParmsNoneType = UpdateDiyPkParmsNoneType.Substring(0, UpdateDiyPkParmsNoneType.Length - 2);

				sb1.AppendFormat(@"
{0}
", sb2.ToString());
		//		//if (table.Columns.Count < 6)
		//			sb1.AppendFormat(@"
		//public static int Update({1}) {{
		//	return Update(new {0}Info({2}));
		//}}", uClass_Name, CsParam1, CsParamNoType1);

				sb1.AppendFormat(@"
		public static int Update({1}Info item) {{
			if (itemCacheTimeout > 0) RemoveCache(item);
			return dal.Update(item);
		}}
		public static {0}.DAL.{1}.SqlUpdateBuild UpdateDiy({2}) {{
			return UpdateDiy(null, {3});
		}}
		public static {0}.DAL.{1}.SqlUpdateBuild UpdateDiy({1}Info item, {2}) {{
			if (itemCacheTimeout > 0) RemoveCache(item != null ? item : GetItem({3}));
			return new {0}.DAL.{1}.SqlUpdateBuild(item, {3});
		}}
		/// <summary>
		/// 用于批量更新
		/// </summary>
		public static {0}.DAL.{1}.SqlUpdateBuild UpdateDiyDangerous {{
			get {{ return new {0}.DAL.{1}.SqlUpdateBuild(); }}
		}}
", solutionName, uClass_Name, pkCsParam, pkCsParamNoType);
				//if (table.Columns.Count < 6)
					sb1.AppendFormat(@"
		/// <summary>
		/// [否决] 适用字段较少的表；避规后续改表风险，字段数较大请改用 {0}.Insert({0}Info item)
		/// </summary>
		public static {0}Info Insert({1}) {{
			return Insert(new {0}Info({2}));
		}}", uClass_Name, CsParam2, CsParamNoType2);

				sb1.AppendFormat(@"
		public static {0}Info Insert({0}Info item) {{
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}}
		private static void RemoveCache({0}Info item) {{
			if (item == null) return;{2}
		}}
		#endregion
{1}
", uClass_Name, sb3.ToString(), sb4.ToString());

				sb1.AppendFormat(@"
		public static List<{0}Info> GetItems() {{
			return Select.ToList();
		}}
		public static {0}SelectBuild Select {{
			get {{ return new {0}SelectBuild(dal); }}
		}}", uClass_Name, solutionName);

				Dictionary<string, bool> byItems = new Dictionary<string, bool>();
				foreach (ForeignKeyInfo fk in table.ForeignKeys) {
					string fkcsBy = string.Empty;
					string fkcsParms = string.Empty;
					string fkcsTypeParms = string.Empty;
					string fkcsFilter = string.Empty;
					int fkcsFilterIdx = 0;
					foreach (ColumnInfo c1 in fk.Columns) {
						fkcsBy += CodeBuild.UFString(c1.Name) + "And";
						fkcsParms += CodeBuild.UFString(c1.Name) + ", ";
						fkcsTypeParms += CodeBuild.GetCSType(c1.Type) + " " + CodeBuild.UFString(c1.Name) + ", ";
						fkcsFilter += "a.[" + c1.Name + "] = {" + fkcsFilterIdx++ + "} and ";
					}
					fkcsBy = fkcsBy.Remove(fkcsBy.Length - 3);
					fkcsParms = fkcsParms.Remove(fkcsParms.Length - 2);
					fkcsTypeParms = fkcsTypeParms.Remove(fkcsTypeParms.Length - 2);
					fkcsFilter = fkcsFilter.Remove(fkcsFilter.Length - 4);
					if (byItems.ContainsKey(fkcsBy)) {
						continue;
					}
					byItems.Add(fkcsBy, true);

					if (!del_exists2.ContainsKey(fkcsTypeParms)) {
						sb5.AppendFormat(@"
		public static int DeleteBy{2}({0}) {{
			return dal.DeleteBy{2}({1});
		}}", fkcsTypeParms, fkcsParms, fkcsBy);
						del_exists2.Add(fkcsTypeParms, true);
					}
					if (fk.Columns.Count > 1) {
						sb1.AppendFormat(
		@"
		public static List<{0}Info> GetItemsBy{1}({2}) {{
			return Select.Where{1}({3}).ToList();
		}}
		public static List<{0}Info> GetItemsBy{1}({2}, int limit) {{
			return Select.Where{1}({3}).Limit(limit).ToList();
		}}
		public static {0}SelectBuild SelectBy{1}({2}) {{
			return Select.Where{1}({3});
		}}", uClass_Name, fkcsBy, fkcsTypeParms, fkcsParms);
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}({2}) {{
			return base.Where(""{4}"", {3}) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, fkcsTypeParms, fkcsParms, fkcsFilter, solutionName);
					} else if (fk.Columns.Count == 1/* && fk.Columns[0].IsPrimaryKey == false*/) {
						string csType = CodeBuild.GetCSType(fk.Columns[0].Type);
						sb1.AppendFormat(
		@"
		public static List<{0}Info> GetItemsBy{1}(params {2}[] {1}) {{
			return Select.Where{1}({1}).ToList();
		}}
		public static List<{0}Info> GetItemsBy{1}({2}[] {1}, int limit) {{
			return Select.Where{1}({1}).Limit(limit).ToList();
		}}
		public static {0}SelectBuild SelectBy{1}(params {2}[] {1}) {{
			return Select.Where{1}({1});
		}}", uClass_Name, fkcsBy, csType);
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
					}
				}

				_tables.ForEach(delegate (TableInfo t2) {
					ForeignKeyInfo fk = t2.ForeignKeys.Find(delegate (ForeignKeyInfo ffk) {
						if (ffk.ReferencedTable.FullName == table.FullName) {
							return true;
						}
						return false;
					});
					if (fk == null) return;
					if (fk.Table.FullName == table.FullName) return;
					List<ForeignKeyInfo> fk2 = t2.ForeignKeys.FindAll(delegate (ForeignKeyInfo ffk2) {
						return ffk2 != fk;
					});
					if (fk2.Count != 1) return;
					if (fk.Columns[0].IsPrimaryKey == false) return; //中间表关系键，必须为主键

					//t2.Columns
					string t2name = t2.Name;
					string tablename = table.Name;
					string addname = t2name;
					if (t2name.StartsWith(tablename + "_")) {
						addname = t2name.Substring(tablename.Length + 1);
					} else if (t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(addname.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + tablename)) {
						addname = t2name.Remove(t2name.Length - tablename.Length - 1);
					} else if (fk2.Count == 1 && t2name.EndsWith("_" + fk2[0].ReferencedTable.Name)) {
						addname = t2name;
					}

					string orgInfo = CodeBuild.UFString(fk2[0].ReferencedTable.ClassName);
					string fkcsBy = CodeBuild.UFString(addname);
					if (byItems.ContainsKey(fkcsBy)) {
						return;
					}
					byItems.Add(fkcsBy, true);

					string civ = string.Format(GetCSTypeValue(fk2[0].ReferencedTable.PrimaryKeys[0].Type), CodeBuild.UFString(fk2[0].ReferencedTable.PrimaryKeys[0].Name));
					sb1.AppendFormat(@"
		public static {0}SelectBuild SelectBy{1}(params {2}Info[] items) {{
			return Select.Where{1}(items);
		}}
		public static {0}SelectBuild SelectBy{1}_{4}(params {3}[] ids) {{
			return Select.Where{1}_{4}(ids);
		}}", uClass_Name, fkcsBy, orgInfo,
		GetCSType(fk2[0].ReferencedTable.PrimaryKeys[0].Type).Replace("?", ""), table.PrimaryKeys[0].Name);

					string _f6 = fk.Columns[0].Name;
					string _f7 = fk.ReferencedTable.PrimaryKeys[0].Name;
					string _f8 = fk2[0].Columns[0].Name;
					string _f9 = GetCSType(fk2[0].ReferencedTable.PrimaryKeys[0].Type).Replace("?", "");

					if (fk.ReferencedTable.ClassName == fk2[0].ReferencedTable.ClassName) {
						_f6 = fk2[0].Columns[0].Name;
						_f7 = fk2[0].ReferencedTable.PrimaryKeys[0].Name;
						_f8 = fk.Columns[0].Name;
						_f9 = GetCSType(fk2[0].Table.PrimaryKeys[0].Type).Replace("?", "");
					}
					sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}Info[] items) {{
			if (items == null || items.Length == 0) return this;
			return Where{1}_{7}(items.Select<{2}Info, {9}>(a => a.{3}).ToArray());
		}}
		public {0}SelectBuild Where{1}_{7}(params {9}[] ids) {{
			if (ids == null || ids.Length == 0) return this;
			return base.Where(string.Format(@""EXISTS( SELECT [{6}] FROM [{4}].[{5}] WHERE [{6}] = a.[{7}] AND [{8}] IN ({{0}}) )"", string.Join<{9}>("","", ids))) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, orgInfo, civ, t2.Owner, t2.Name, _f6, _f7, _f8, _f9);
				});

				table.Columns.ForEach(delegate (ColumnInfo col) {
					string csType = CodeBuild.GetCSType(col.Type);
					string lname = col.Name.ToLower();
					//if (col.IsPrimaryKey) return;
					//if (lname == "create_time" ||
					//	lname == "update_time") return;
					string fkcsBy = CodeBuild.UFString(col.Name);
					if (byItems.ContainsKey(fkcsBy)) {
						return;
					}
					byItems.Add(fkcsBy, true);

					if (csType == "bool?") {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if ((col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int) && (lname == "status" || lname.StartsWith("status_") || lname.EndsWith("_status"))) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params int[] _0_16) {{
			if (_0_16 == null || _0_16.Length == 0) return this;
			{2}[] copy = new {2}[_0_16.Length];
			for (int a = 0; a < _0_16.Length; a++) copy[a] = ({2})Math.Pow(_0_16[a], 2);
			return this.Where1Or(""(a.[{1}] & {{0}}) = {{0}}"", copy);
		}}", uClass_Name, fkcsBy, csType.Replace("?", ""));
						return;
					}
					if (col.Type == SqlDbType.BigInt || col.Type == SqlDbType.Int || col.Type == SqlDbType.SmallInt) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if (col.Type == SqlDbType.Decimal || col.Type == SqlDbType.Float || col.Type == SqlDbType.Money || col.Type == SqlDbType.Real) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}Range({2} begin) {{
			return base.Where(""a.[{1}] >= {{0}}"", begin) as {0}SelectBuild;
		}}
		public {0}SelectBuild Where{1}Range({2} begin, {2} end) {{
			if (end == null) return Where{1}Range(begin);
			return base.Where(""a.[{1}] between {{0}} and {{1}}"", begin, end) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if (col.Type == SqlDbType.DateTime || col.Type == SqlDbType.SmallDateTime || col.Type == SqlDbType.Date) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}Range({2} begin) {{
			return base.Where(""a.[{1}] >= {{0}}"", begin) as {0}SelectBuild;
		}}
		public {0}SelectBuild Where{1}Range({2} begin, {2} end) {{
			if (end == null) return Where{1}Range(begin);
			return base.Where(""a.[{1}] between {{0}} and {{1}}"", begin, end) as {0}SelectBuild;
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
					if (col.Length > 0 && col.Length < 101 && (col.Type == SqlDbType.VarChar || col.Type == SqlDbType.NVarChar)) {
						sb6.AppendFormat(@"
		public {0}SelectBuild Where{1}(params {2}[] {1}) {{
			return this.Where1Or(""a.[{1}] = {{0}}"", {1});
		}}", uClass_Name, fkcsBy, csType);
						return;
					}
				});

				sb1.AppendFormat(@"
	}}
	public partial class {0}SelectBuild : {1}.DAL.SqlHelper.SelectBuild<{0}Info, {0}SelectBuild> {{{2}
		protected new {0}SelectBuild Where1Or(string filterFormat, Array values) {{
			return base.Where1Or(filterFormat, values) as {0}SelectBuild;
		}}
		public {0}SelectBuild({1}.DAL.SqlHelper.IDAL dal) : base(dal) {{ }}
	}}
}}", uClass_Name, solutionName, sb6.ToString());

				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\", uClass_Name, ".cs"), Deflate.Compress(sb1.ToString().Replace("|deleteby_fk|", sb5.ToString()))));
				clearSb();
				#endregion

				#region admin
				if (isMakeAdmin) {

					#region common define
					string pkNames = string.Empty;
					string pkUrlQuerys = string.Empty;
					for (int a = 0; a < table.PrimaryKeys.Count; a++) {
						ColumnInfo col88 = table.PrimaryKeys[a];
						pkNames += CodeBuild.UFString(col88.Name) + ",";
						pkUrlQuerys += col88.Name + "={" + a + "}&";
					}
					if (pkNames.Length > 0) pkNames = pkNames.Remove(pkNames.Length - 1);
					if (pkUrlQuerys.Length > 0) pkUrlQuerys = pkUrlQuerys.Remove(pkUrlQuerys.Length - 1);
					#endregion

					#region init_sysdir
					admin_controllers_syscontroller_init_sysdir.Add(string.Format(@"

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, ""{0}"", {1}, ""/{0}/"");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""列表"", 1, ""/{0}/"");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""添加"", 2, ""/{0}/add.aspx"");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""编辑"", 3, ""/{0}/edit.aspx"");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, ""删除"", 4, ""/{0}/del.aspx"");", nClass_Name, admin_controllers_syscontroller_init_sysdir.Count + 1));
					#endregion

					#region Controller.cs

					string keyLikes = string.Empty;
					string getListParamQuery = "";
					foreach (ColumnInfo col in table.Columns) {
						List<ColumnInfo> us = table.Uniques.Find(delegate (List<ColumnInfo> cs) {
							return cs.Find(delegate (ColumnInfo col88) {
								return col88.Name == col.Name;
							}) != null;
						});
						if (us == null) us = new List<ColumnInfo>();
						List<ForeignKeyInfo> fks_comb = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk2) {
							return fk2.Columns.Count == 1 && fk2.Columns[0].Name == col.Name;
						});

						string csType = CodeBuild.GetCSType(col.Type);
						string csUName = CodeBuild.UFString(col.Name);
						if (csType == "string") {
							keyLikes += "a.[" + col.Name + "] like {0} or ";
						}
						List<ForeignKeyInfo> fks = table.ForeignKeys.FindAll(delegate (ForeignKeyInfo fk88) {
							return fk88.Columns.Find(delegate (ColumnInfo col88) {
								return col88.Name == col.Name;
							}) != null;
						});

						ForeignKeyInfo fk = null;
						string FK_uEntry_Name = string.Empty;
						string tableNamefe3 = string.Empty;
						string memberName = string.Empty;
						string strName = string.Empty;
						if (fks.Count > 0) {
							fk = fks[0];
							FK_uEntry_Name = fk.ReferencedTable != null ? CodeBuild.GetCSName(fk.ReferencedTable.Name) :
								CodeBuild.GetCSName(TableInfo.GetEntryName(fk.ReferencedTableName));
							tableNamefe3 = fk.ReferencedTable != null ? fk.ReferencedTable.Name :
								FK_uEntry_Name;
							memberName = fk.Columns[0].Name.IndexOf(tableNamefe3) == -1 ? CodeBuild.LFString(tableNamefe3) :
								(CodeBuild.LFString(fk.Columns[0].Name.Substring(0, fk.Columns[0].Name.IndexOf(tableNamefe3)) + tableNamefe3));

							ColumnInfo strNameCol = null;
							if (fk.ReferencedTable != null) {
								strNameCol = fk.ReferencedTable.Columns.Find(delegate (ColumnInfo col88) {
									return col88.Name.ToLower().IndexOf("name") != -1 || col88.Name.ToLower().IndexOf("title") != -1;
								});
								if (strNameCol == null) strNameCol = fk.ReferencedTable.Columns.Find(delegate (ColumnInfo col88) {
									return GetCSType(col88.Type) == "string" && col88.Length > 0 && col88.Length < 128;
								});
							}
							strName = strNameCol != null ? "." + CodeBuild.UFString(strNameCol.Name) : string.Empty;
						}
						string Obj_name = string.Concat("Obj_", memberName, strName);

						if (!col.IsIdentity && fks.Count == 1) {
							ForeignKeyInfo fkcb = fks[0];
							string FK_uClass_Name = fkcb.ReferencedTable != null ? CodeBuild.UFString(fkcb.ReferencedTable.ClassName) :
								CodeBuild.UFString(TableInfo.GetClassName(fkcb.ReferencedTableName));

							getListParamQuery += string.Format(@"[FromQuery] {0}[] {1}, ", csType, csUName);
							sb3.AppendFormat(@"
			if ({0}.Length > 0) select.Where{0}({0});", csUName);
						} else if (!col.IsIdentity && us.Count == 1 || col.IsPrimaryKey && table.PrimaryKeys.Count == 1) {
							//主键或唯一键，非自动增值
						}
					}
					if (keyLikes.Length > 0) {
						keyLikes = keyLikes.Remove(keyLikes.Length - 4);
						getListParamQuery = "[FromQuery] string key, " + getListParamQuery;
						sb2.AppendFormat(@"
				.Where(!string.IsNullOrEmpty(key), ""{0}"", string.Concat(""%"", key, ""%""))", keyLikes);
					}

					string itemSetValuePK = "";
					string itemSetValuePKInsert = "";
					string itemSetValueNotPK = "";
					string itemCsParamInsertForm = "";
					string itemCsParamUpdateForm = "";
					table.Columns.ForEach(delegate (ColumnInfo col88) {
						string csLName = CodeBuild.LFString(col88.Name);
						string csUName = CodeBuild.UFString(col88.Name);
						string csType = CodeBuild.GetCSType(col88.Type);

						if (col88.IsPrimaryKey) {
							itemSetValuePK += string.Format(@"
			item.{0} = {0};", csUName);
							if (col88.IsIdentity) ;
							else {
								itemSetValuePKInsert += string.Format(@"
			item.{0} = {0};", csUName);
								itemCsParamInsertForm += string.Format(", [FromForm] {0} {1}", CodeBuild.GetCSType(col88.Type), csUName);
							}
						} else if (col88.IsIdentity) {
						} else {
							string colvalue = "";
							if (csType == "DateTime?" && (
	   string.Compare(csLName, "create_time", true) == 0 ||
	   string.Compare(csLName, "update_time", true) == 0
   )) {
								colvalue = "DateTime.Now";
							} else {
								itemCsParamInsertForm += string.Format(", [FromForm] {0} {1}", CodeBuild.GetCSType(col88.Type), csUName);
								itemCsParamUpdateForm += string.Format(", [FromForm] {0} {1}", CodeBuild.GetCSType(col88.Type), csUName);
								colvalue = csUName;
							}
							itemSetValueNotPK += string.Format(@"
			item.{0} = {1};", csUName, colvalue);
						}
					});
					if (itemCsParamInsertForm.Length > 0) itemCsParamInsertForm = itemCsParamInsertForm.Substring(2);

					sb1.AppendFormat(CONST.Admin_Controllers, solutionName, uClass_Name, nClass_Name, pkMvcRoute, pkCsParam, pkCsParamNoType, itemSetValuePK, itemSetValueNotPK, sb2.ToString(), sb3.ToString(), itemCsParamInsertForm, itemCsParamUpdateForm, getListParamQuery, itemSetValuePKInsert);

					loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"\Controllers\", uClass_Name, @"Controller.cs"), Deflate.Compress(sb1.ToString())));
					clearSb();
					#endregion
				}
				#endregion
			}

			#region BLL StoreProcedure.cs
			int spsssss = 0;
			sb1.AppendFormat(@"using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace {0}.BLL {{

	public partial class StoreProcedure {{
", solutionName);

			foreach (TableInfo table in _tables) {
				if (table.IsOutput == false) continue;
				if (table.Type != "P") continue;

				string uClass_Name = CodeBuild.UFString(table.ClassName);
				string nClass_Name = table.ClassName;
				string nTable_Name = "[" + table.Owner + "].[" + table.Name + "]";

				List<string> csParms = new List<string>();
				List<string> csParmsNoType = new List<string>();
				List<string> setOutParmsNull = new List<string>();
				List<string> dimParms = new List<string>();
				List<string> dimOutParms = new List<string>();
				List<string> dimOutParmsInput = new List<string>();
				List<string> dimOutParmsReturn = new List<string>();
				int idx = 0;
				foreach (ColumnInfo column in table.Columns) {
					string name = CodeBuild.GetCSName(column.Name);
					string csType = CodeBuild.GetCSType(column.Type);
					string nameOut = string.Empty;
					string sqlParm = string.Empty;
					if (column.IsIdentity) {
						setOutParmsNull.Add(string.Format(@"{0} = null;", name));
						dimOutParms.Add(string.Format(@"SqlParameter parmO{0} = null;", idx));
						dimOutParmsInput.Add(string.Format(@"parmO{0}.Direction = ParameterDirection.Output;", idx));
						dimOutParmsReturn.Add(string.Format(@"if (parmO{0}.Value != DBNull.Value) {1} = ({2})parmO{0}.Value;", idx, name, csType));
						nameOut = "out ";
						sqlParm = "parmO" + idx++ + " = ";
					}
					csParms.Add(nameOut + csType + " " + name);
					csParmsNoType.Add(nameOut + name);
					dimParms.Add(sqlParm +
						string.Format(@"GetParameter(""{0}"", SqlDbType.{1}, {2}, {3})", column.Name, column.Type, column.Length, name));
				}
				if (table.Columns.Count == 0) {
					sb1.AppendFormat(@"
		#region {0}
		public static void {0}() {{
			{0}(false);
		}}
		public static DataSet {0}_dataset() {{
			return {0}(true);
		}}
		private static DataSet {0}(bool IsReturnDataSet) {{
			DataSet ds = null;
			string sql = @""{1}"";

			if (IsReturnDataSet)
				ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql);
			else
				SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql);

			return ds;
		}}
		#endregion
", uClass_Name, nTable_Name);
				} else {
					if (setOutParmsNull.Count > 0) setOutParmsNull.Add("");
					if (dimOutParms.Count > 0) dimOutParms.Add("");
					if (dimOutParmsInput.Count > 0) dimOutParmsInput.Add("");
					if (dimOutParmsReturn.Count > 0) dimOutParmsReturn.AddRange(new string[] { "", "" });
					sb1.AppendFormat(@"
		#region {0}
		public static void {0}({1}) {{
			{0}({2}, false);
		}}
		public static DataSet {0}_dataset({1}) {{
			return {0}({2}, true);
		}}
		private static DataSet {0}({1}, bool IsReturnDataSet) {{
			{3}{5}SqlParameter[] parms = new SqlParameter[] {{
				{4}
			}};
			{6}DataSet ds = null;
			string sql = @""{7}"";

			if (IsReturnDataSet)
				ds = SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, sql, parms);
			else
				SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, sql, parms);

			{8}return ds;
		}}
		#endregion
", uClass_Name,
					string.Join(", ", csParms.ToArray()),
					string.Join(", ", csParmsNoType.ToArray()),
					string.Join("\r\n			", setOutParmsNull.ToArray()),
					string.Join(",\r\n				", dimParms.ToArray()),
					string.Join("\r\n			", dimOutParms.ToArray()),
					string.Join("\r\n			", dimOutParmsInput.ToArray()),
					nTable_Name,
					string.Join("\r\n			", dimOutParmsReturn.ToArray()));
				}

				spsssss++;
			}

			sb1.AppendFormat(@"
		public static SqlParameter GetParameter(string name, SqlDbType type, int size, object value) {{
			SqlParameter parm = new SqlParameter(name, type, size);
			parm.Value = value;
			return parm;
		}}
	}}
}}");
			string bll_sp_cs = null;
			if (spsssss > 0) {
				bll_sp_cs = string.Format(@"
		<Compile Include=""BLL\{0}\StoreProcedure.cs"" />", basicName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\StoreProcedure.cs"), Deflate.Compress(sb1.ToString())));
			}
			clearSb();
			#endregion

			#region BLL SqlHelper.cs
			sb1.AppendFormat(CONST.BLL_Build_SqlHelper_cs, solutionName);
			loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\SqlHelper.cs"), Deflate.Compress(sb1.ToString())));
			clearSb();
			#endregion
			//#region BLL ItemCache.cs
			//sb1.AppendFormat(CONST.BLL_Build_ItemCache_cs, solutionName);
			//loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\ItemCache.cs"), Deflate.Compress(sb1.ToString())));
			//clearSb();
			//#endregion
			#region BLL RedisHelper.cs
			sb1.AppendFormat(CONST.BLL_Build_RedisHelper_cs, solutionName);
			loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\BLL\", basicName, @"\RedisHelper.cs"), Deflate.Compress(sb1.ToString())));
			clearSb();
			#endregion
			#region Model ExtensionMethods.cs 扩展方法
			sb1.AppendFormat(CONST.Model_Build_ExtensionMethods_cs, solutionName, Model_Build_ExtensionMethods_cs.ToString());
			loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\Model\", basicName, @"\ExtensionMethods.cs"), Deflate.Compress(sb1.ToString())));
			clearSb();
			#endregion

			if (isSolution) {

				#region db.xproj

				#region DBUtility/ConnectionManager.cs
				sb1.AppendFormat(CONST.DAL_ConnectionManager_cs, solutionName, connectionStringName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\DBUtility\ConnectionManager.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region DBUtility/SqlHelper.cs
				sb1.AppendFormat(CONST.DAL_SqlHelper_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\DBUtility\SqlHelper.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region DBUtility/SqlSelectBuild.cs
				sb1.AppendFormat(CONST.DAL_SqlHelper_SelectBuild_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\DAL\DBUtility\SqlSelectBuild.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				sb1.AppendFormat(CONST.xproj, dbGuid, solutionName + ".db");
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\", solutionName, ".db.xproj"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#region project.json
				sb1.AppendFormat(CONST.Db_project_json);
				loc1.Add(new BuildInfo(string.Concat(CONST.corePath, solutionName, @".db\", @"project.json"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#endregion
			}

			if (isMakeAdmin) {
				#region Project Admin
				#region bin/Debug/web.config
				sb1.AppendFormat(CONST.Admin_bin_Debug_web_config, solutionName, connectionStringName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"bin/Debug/web.config"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region web.config
				sb1.AppendFormat(CONST.Admin_web_config, solutionName, connectionStringName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"web.config"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region appsettings.json
				sb1.AppendFormat(CONST.Admin_appsettings_json, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"appsettings.json"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Program.cs
				sb1.AppendFormat(CONST.Admin_Program_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"Program.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Startup.cs
				sb1.AppendFormat(CONST.Admin_Startup_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"Startup.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region App_Code\BaseController.cs
				sb1.AppendFormat(CONST.Admin_App_Code_BaseController_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\BaseController.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region App_Code\Microsoft.Extensions.Caching.Redis\RedisCache.cs
				sb1.AppendFormat(CONST.Admin_App_Code_Microsoft_Extensions_Caching_Redis_RedisCache_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\Microsoft.Extensions.Caching.Redis\RedisCache.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region App_Code\Microsoft.Extensions.Caching.Redis\RedisCacheOptions.cs
				sb1.AppendFormat(CONST.Admin_App_Code_Microsoft_Extensions_Caching_Redis_RedisCacheOptions_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\Microsoft.Extensions.Caching.Redis\RedisCacheOptions.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region App_Code\Microsoft.Extensions.Caching.Redis\RedisExtensions.cs
				sb1.AppendFormat(CONST.Admin_App_Code_Microsoft_Extensions_Caching_Redis_RedisExtensions_cs, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"App_Code\Microsoft.Extensions.Caching.Redis\RedisExtensions.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion

				#region SysController.cs
				sb1.AppendFormat(CONST.Admin_Controllers_SysController, solutionName, string.Join(string.Empty, admin_controllers_syscontroller_init_sysdir.ToArray()));
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"Controllers/SysController.cs"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region Admin.xproj
				sb1.AppendFormat(CONST.xproj, adminGuid, "Admin");
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"Admin.xproj"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region project.json
				sb1.AppendFormat(CONST.Admin_project_json, solutionName);
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"project.json"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#region index.html
				sb1.Append("<h1>这是一个测试首页</h1>\r\n<h2>swagger webapi：<a href='/swagger/ui/' target='_blank'>/swagger/ui/</a><h2>");
				loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"wwwroot\index.html"), Deflate.Compress(sb1.ToString())));
				clearSb();
				#endregion
				#endregion
			}
			if (isDownloadRes) {
				//loc1.Add(new BuildInfo(string.Concat(CONST.adminPath, @"htm.zip"), Server.Properties.Resources.htm));
			}

			GC.Collect();
			return loc1;
		}
	}
}
