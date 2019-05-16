using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Model;

namespace Server {

	internal partial class CodeBuild {

		//protected static string GetEntryName(string name) {

		//}

		protected static SqlDbType GetDBType(string strType) {
			switch (strType.ToLower()) {
				case "bit": return SqlDbType.Bit;
				case "tinyint": return SqlDbType.TinyInt;
				case "smallint": return SqlDbType.SmallInt;
				case "int": return SqlDbType.Int;
				case "bigint": return SqlDbType.BigInt;
				case "numeric":
				case "decimal": return SqlDbType.Decimal;
				case "smallmoney": return SqlDbType.SmallMoney;
				case "money": return SqlDbType.Money;
				case "float": return SqlDbType.Float;
				case "real": return SqlDbType.Real;
				case "date": return SqlDbType.Date;
				case "datetime":
				case "datetime2": return SqlDbType.DateTime;
				case "datetimeoffset": return SqlDbType.DateTimeOffset;
				case "smalldatetime": return SqlDbType.SmallDateTime;
				case "time": return SqlDbType.Time;
				case "char": return SqlDbType.Char;
				case "varchar": return SqlDbType.VarChar;
				case "text": return SqlDbType.Text;
				case "nchar": return SqlDbType.NChar;
				case "nvarchar": return SqlDbType.NVarChar;
				case "ntext": return SqlDbType.NText;
				case "binary": return SqlDbType.Binary;
				case "varbinary": return SqlDbType.VarBinary;
				case "image": return SqlDbType.Image;
				case "timestamp": return SqlDbType.Timestamp;
				case "uniqueidentifier": return SqlDbType.UniqueIdentifier;
				case "xml": return SqlDbType.Xml;
				default: return SqlDbType.Variant;
			}
		}

		protected static string GetDbToCsConvert(SqlDbType type) {
			switch (type) {
				case SqlDbType.Bit: return "(bool?)";
				case SqlDbType.TinyInt: return "(byte?)";
				case SqlDbType.SmallInt: return "(short?)";
				case SqlDbType.Int: return "(int?)";
				case SqlDbType.BigInt: return "(long?)";
				case SqlDbType.Decimal:
				case SqlDbType.SmallMoney:
				case SqlDbType.Money: return "(decimal?)";
				case SqlDbType.Float: return "(double?)";
				case SqlDbType.Real: return "(float?)";
				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				case SqlDbType.SmallDateTime: return "(DateTime?)";
				case SqlDbType.DateTimeOffset: return "(DateTimeOffset?)";
				case SqlDbType.Time: return "(TimeSpan?)";
				case SqlDbType.Char:
				case SqlDbType.VarChar:
				case SqlDbType.Text:
				case SqlDbType.NChar:
				case SqlDbType.NVarChar:
				case SqlDbType.NText: return "(string)";
				case SqlDbType.Binary:
				case SqlDbType.VarBinary:
				case SqlDbType.Image: return "(byte[])";
				case SqlDbType.Timestamp: return "(byte[])";
				case SqlDbType.UniqueIdentifier: return "(Guid?)";
				case SqlDbType.Xml: return "(string)";
				default: return "";
			}
		}

		protected static string GetCSTypeValue(SqlDbType type) {
			switch (type) {
				case SqlDbType.Bit:
				case SqlDbType.TinyInt:
				case SqlDbType.SmallInt:
				case SqlDbType.Int:
				case SqlDbType.BigInt:
				case SqlDbType.Decimal:
				case SqlDbType.SmallMoney:
				case SqlDbType.Money:
				case SqlDbType.Float:
				case SqlDbType.Real:
				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				case SqlDbType.SmallDateTime: return "{0}.Value";
				case SqlDbType.DateTimeOffset: return "{0}.Value";
				case SqlDbType.Time: return "{0}.Value";
				case SqlDbType.Char:
				case SqlDbType.VarChar:
				case SqlDbType.Text:
				case SqlDbType.NChar:
				case SqlDbType.NVarChar:
				case SqlDbType.NText: return "{0}";
				case SqlDbType.Binary:
				case SqlDbType.VarBinary:
				case SqlDbType.Image: return "string.Concat({0})";
				case SqlDbType.Timestamp: return "string.Concat({0})";
				case SqlDbType.UniqueIdentifier: return "{0}.Value";
				case SqlDbType.Xml: return "{0}";
				default: return "string.Concat({0})";
			}
		}
		protected static string GetCSType(SqlDbType type) {
			switch (type) {
				case SqlDbType.Bit: return "bool?";
				case SqlDbType.TinyInt: return "byte?";
				case SqlDbType.SmallInt: return "short?";
				case SqlDbType.Int: return "int?";
				case SqlDbType.BigInt: return "long?";
				case SqlDbType.Decimal:
				case SqlDbType.SmallMoney:
				case SqlDbType.Money: return "decimal?";
				case SqlDbType.Float: return "double?";
				case SqlDbType.Real: return "float?";
				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				case SqlDbType.SmallDateTime: return "DateTime?";
				case SqlDbType.DateTimeOffset: return "DateTimeOffset?";
				case SqlDbType.Time: return "TimeSpan?";
				case SqlDbType.Char:
				case SqlDbType.VarChar:
				case SqlDbType.Text:
				case SqlDbType.NChar:
				case SqlDbType.NVarChar:
				case SqlDbType.NText: return "string";
				case SqlDbType.Binary:
				case SqlDbType.VarBinary:
				case SqlDbType.Image: return "byte[]";
				case SqlDbType.Timestamp: return "byte[]";
				case SqlDbType.UniqueIdentifier: return "Guid?";
				case SqlDbType.Xml: return "string";
				default: return "object";
			}
		}

		protected static string GetDataReaderMethod(SqlDbType type) {
			switch (type) {
				case SqlDbType.Bit: return "GetBoolean";
				case SqlDbType.TinyInt: return "GetByte";
				case SqlDbType.SmallInt: return "GetInt16";
				case SqlDbType.Int: return "GetInt32";
				case SqlDbType.BigInt: return "GetInt64";
				case SqlDbType.Decimal:
				case SqlDbType.SmallMoney:
				case SqlDbType.Money: return "GetDecimal";
				case SqlDbType.Float: return "GetDouble";
				case SqlDbType.Real: return "GetFloat";
				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				case SqlDbType.SmallDateTime: return "GetDateTime";
				case SqlDbType.DateTimeOffset: return "GetDateTimeOffset";
				case SqlDbType.Time: return "GetTimeSpan";
				case SqlDbType.Char:
				case SqlDbType.VarChar:
				case SqlDbType.Text:
				case SqlDbType.NChar:
				case SqlDbType.NVarChar:
				case SqlDbType.NText: return "GetString";
				case SqlDbType.Binary:
				case SqlDbType.VarBinary:
				case SqlDbType.Image: return "GetBytes";
				case SqlDbType.Timestamp: return "GetBytes";
				case SqlDbType.UniqueIdentifier: return "GetGuid";
				case SqlDbType.Xml: return "GetValue";
				default: return "GetValue";
			}
		}

		protected static string GetToStringFieldConcat(ColumnInfo columnInfo) {
			switch (columnInfo.Type) {
				case SqlDbType.Bit: return string.Format("{0} == null ? \"null\" : ({0} == true ? \"true\" : \"false\")", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.TinyInt:
				case SqlDbType.SmallInt:
				case SqlDbType.Int:
				case SqlDbType.BigInt:
				case SqlDbType.Decimal:
				case SqlDbType.SmallMoney:
				case SqlDbType.Money:
				case SqlDbType.Float:
				//string.Format("", {0} : {{0}}"", {0} == null ? ""null"" : {0}.ToString())
				case SqlDbType.Real: return string.Format("{0} == null ? \"null\" : {0}.ToString()", CodeBuild.UFString(columnInfo.Name));
					// "'\" + _" + CodeBuild.UFString(columnInfo.Name) + " +\r\n				\"'";
				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				//string.Format("\", {0} == null ? \"null\" : string.Concat(\"Date(\", {0}.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, \")\"), \r\n				\"", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.SmallDateTime: return string.Format("{0} == null ? \"null\" : string.Concat(\"\", {0}.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, \"\")", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.DateTimeOffset: return string.Format("{0} == null ? \"null\" : string.Concat(\"\", {0}.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, \"\")", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.Time: return string.Format("{0} == null ? \"null\" : string.Concat(\"\", {0}.Value.Ticks, \"\")", CodeBuild.UFString(columnInfo.Name));

				case SqlDbType.Binary:
				case SqlDbType.VarBinary:
				// return "\" + (_" + CodeBuild.UFString(columnInfo.Name) + " == null ? null : Encoding.UTF8.GetString(_" + CodeBuild.UFString(columnInfo.Name) + ")) +\r\n				\"";
				case SqlDbType.Image: return string.Format("{0} == null ? \"null\" : Convert.ToBase64String({0})", CodeBuild.UFString(columnInfo.Name));
				//return "'\" + _" + CodeBuild.UFString(columnInfo.Name) + " +\r\n				\"'";
				case SqlDbType.UniqueIdentifier: return string.Format("{0} == null ? \"null\" : {0}.ToString()", CodeBuild.UFString(columnInfo.Name));

				case SqlDbType.Xml:
				case SqlDbType.Char:
				case SqlDbType.VarChar:
				case SqlDbType.Text:
				case SqlDbType.NChar:
				case SqlDbType.NVarChar:
				// return "'\" + (_" + CodeBuild.UFString(columnInfo.Name) + " == null ? string.Empty : _" + CodeBuild.UFString(columnInfo.Name) + ".Replace(\"\\\\\", \"\\\\\\\\\").Replace(\"\\r\\n\", \"\\\\r\\\\n\").Replace(\"'\", \"\\\\'\")) + \r\n				\"'";
				case SqlDbType.NText: return string.Format("{0} == null ? \"null\" : string.Format(\"'{{0}}'\", {0}.Replace(\"\\\\\", \"\\\\\\\\\").Replace(\"\\r\\n\", \"\\\\r\\\\n\").Replace(\"'\", \"\\\\'\"))", CodeBuild.UFString(columnInfo.Name));

				case SqlDbType.Timestamp: return string.Format("{0} == null ? \"null\" : Convert.ToBase64String({0})", CodeBuild.UFString(columnInfo.Name));
				default: return string.Format("{0} == null ? \"null\" : {0}.ToString()", CodeBuild.UFString(columnInfo.Name)); 
			}
		}
		protected static string GetToHashtableFieldConcat(ColumnInfo columnInfo) {
			switch (columnInfo.Type) {
				case SqlDbType.Bit:
				case SqlDbType.TinyInt:
				case SqlDbType.SmallInt:
				case SqlDbType.Int:
				case SqlDbType.BigInt:
				case SqlDbType.Decimal:
				case SqlDbType.SmallMoney:
				case SqlDbType.Money:
				case SqlDbType.Float:
				case SqlDbType.Real: return string.Format("{0}", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				case SqlDbType.SmallDateTime: return string.Format("{0}.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.DateTimeOffset: return string.Format("{0}.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.Time: return string.Format("{0}.Value.Ticks", CodeBuild.UFString(columnInfo.Name));

				case SqlDbType.Binary:
				case SqlDbType.VarBinary:
				case SqlDbType.Image: return string.Format("Convert.ToBase64String({0})", CodeBuild.UFString(columnInfo.Name));
				case SqlDbType.UniqueIdentifier: return string.Format("{0}", CodeBuild.UFString(columnInfo.Name));

				case SqlDbType.Xml:
				case SqlDbType.Char:
				case SqlDbType.VarChar:
				case SqlDbType.Text:
				case SqlDbType.NChar:
				case SqlDbType.NVarChar:
				case SqlDbType.NText: return string.Format("{0}", CodeBuild.UFString(columnInfo.Name));

				case SqlDbType.Timestamp:
				default: return string.Format("{0}", CodeBuild.UFString(columnInfo.Name));
			}
		}

		protected static string GetToStringStringify(ColumnInfo columnInfo)
        {
            switch (columnInfo.Type)
            {
                case SqlDbType.Bit: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : (_" + CodeBuild.UFString(columnInfo.Name) + " == true ? \"1\" : \"0\")";
				case SqlDbType.TinyInt:
                case SqlDbType.SmallInt:
                case SqlDbType.Int:
                case SqlDbType.BigInt:
                case SqlDbType.Decimal:
                case SqlDbType.SmallMoney:
                case SqlDbType.Money:
                case SqlDbType.Float:
                case SqlDbType.Real: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : _" + CodeBuild.UFString(columnInfo.Name) + ".ToString()";

				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				case SqlDbType.SmallDateTime: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : _" + CodeBuild.UFString(columnInfo.Name) + ".Value.Ticks.ToString()";
				case SqlDbType.DateTimeOffset: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : _" + CodeBuild.UFString(columnInfo.Name) + ".Value.Ticks.ToString()";
				case SqlDbType.Time: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : _" + CodeBuild.UFString(columnInfo.Name) + ".Value.Ticks.ToString()";

				case SqlDbType.Binary:
                case SqlDbType.VarBinary:
                case SqlDbType.Image: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : Convert.ToBase64String(_" + CodeBuild.UFString(columnInfo.Name) + ")";
                case SqlDbType.UniqueIdentifier: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : _" + CodeBuild.UFString(columnInfo.Name) + ".ToString()";

				case SqlDbType.Xml:
				case SqlDbType.Char:
                case SqlDbType.VarChar:
                case SqlDbType.Text:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                case SqlDbType.NText: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : _" + CodeBuild.UFString(columnInfo.Name) + ".Replace(\"|\", StringifySplit)";

				case SqlDbType.Timestamp: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : Convert.ToBase64String(_" + CodeBuild.UFString(columnInfo.Name) + ")";
				default: return "_" + CodeBuild.UFString(columnInfo.Name) + " == null ? \"null\" : _" + CodeBuild.UFString(columnInfo.Name) + ".ToString().Replace(\"|\", StringifySplit)";
            }
        }
        protected static string GetStringifyParse(SqlDbType type)
        {
            switch (type)
            {
                case SqlDbType.Bit: return "{0} == \"1\"";
				case SqlDbType.TinyInt: return "byte.Parse({0})";
                case SqlDbType.SmallInt: return "short.Parse({0})";
                case SqlDbType.Int: return "int.Parse({0})";
                case SqlDbType.BigInt: return "long.Parse({0})";
                case SqlDbType.Decimal:
                case SqlDbType.SmallMoney:
                case SqlDbType.Money: return "decimal.Parse({0})";
                case SqlDbType.Float: return "double.Parse({0})";
                case SqlDbType.Real: return "float.Parse({0})";
				case SqlDbType.Date:
				case SqlDbType.DateTime:
				case SqlDbType.DateTime2:
				case SqlDbType.SmallDateTime: return "new DateTime(long.Parse({0}))";
				case SqlDbType.DateTimeOffset: return "new DateTimeOffset(new DateTime(long.Parse({0})))";
				case SqlDbType.Time: return "TimeSpan.Parse({0})";
				case SqlDbType.Char:
                case SqlDbType.VarChar:
                case SqlDbType.Text:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                case SqlDbType.NText: return "{0}.Replace(StringifySplit, \"|\")";
                case SqlDbType.Binary:
                case SqlDbType.VarBinary:
                case SqlDbType.Image: return "Convert.FromBase64String({0})";
                case SqlDbType.Timestamp: return "Convert.FromBase64String({0})";
                case SqlDbType.UniqueIdentifier: return "Guid.Parse({0})";
				case SqlDbType.Xml: return "{0}.Replace(StringifySplit, \"|\")";
				default: return "{0}";
            }
        }

        protected static string UFString(string text) {
			text = Regex.Replace(text, @"[^\w]", "_");
			if (text.Length <= 1) return text.ToUpper();
			else return text.Substring(0, 1).ToUpper() + text.Substring(1, text.Length - 1);
		}

		protected static string LFString(string text) {
			if (text.Length <= 1) return text.ToLower();
			else return text.Substring(0, 1).ToLower() + text.Substring(1, text.Length - 1);
		}

		protected static string GetCSName(string name) {
			name = Regex.Replace(name.TrimStart('@'), @"[^\w]", "_");
			return char.IsLetter(name, 0) ? name : string.Concat("_", name);
		}

		protected static string AppendParameter(ColumnInfo columnInfo, string value, string place) {
			if (columnInfo == null) return "";
			
			string returnValue = place + string.Format("new SqlParameter {{ ParameterName = \"{0}{1}\", SqlDbType = SqlDbType.{2}, Size = {3}, Value = {4} }}, \r\n",
				columnInfo.Name.StartsWith("@") ? null : "@", columnInfo.Name, columnInfo.Type,
				columnInfo.Length.ToString(),
				//columnInfo.Type == SqlDbType.Image ? string.Format("{0} == null ? 0 : {0}.Length", value + Lib.UFString(columnInfo.Name)) : columnInfo.Length.ToString(),
				value + CodeBuild.UFString(columnInfo.Name));

			return returnValue;
		}
		protected static string AppendParameters(List<ColumnInfo> columnInfos, string value, string place) {
			string returnValue = "";

			foreach (ColumnInfo columnInfo in columnInfos) {
				returnValue += AppendParameter(columnInfo, value, place);
			}

			return returnValue == "" ? "" : returnValue.Substring(0, returnValue.Length - 4);
		}
		protected static string AppendParameters(List<ColumnInfo> columnInfos, string place) {
			return AppendParameters(columnInfos, "", place);
		}
		protected static string AppendParameters(TableInfo table, string place) {
			return AppendParameters(table.Columns, "item.", place);
		}
		protected static string AppendParameters(ColumnInfo columnInfo, string place) {
			string returnValue = AppendParameter(columnInfo, "", place);
			return returnValue == "" ? "" : returnValue.Substring(0, returnValue.Length - 4);
		}

		protected static string AppendAddslashes(ColumnInfo columnInfo, string value, string place) {
			if (columnInfo == null) return "";

			string returnValue = place + value + CodeBuild.UFString(columnInfo.Name) + ", ";

			return returnValue;
		}
		protected static string AppendAddslashes(List<ColumnInfo> columnInfos, string value, string place) {
			string returnValue = "";

			foreach (ColumnInfo columnInfo in columnInfos) {
				returnValue += AppendAddslashes(columnInfo, value, place);
			}

			return returnValue == "" ? "" : returnValue.Substring(0, returnValue.Length - 2);
		}
		protected static string AppendAddslashes(List<ColumnInfo> columnInfos, string place) {
			return AppendAddslashes(columnInfos, "", place);
		}
		protected static string AppendAddslashes(TableInfo table, string place) {
			return AppendAddslashes(table.Columns, "item.", place);
		}
		protected static string AppendAddslashes(ColumnInfo columnInfo, string place) {
			string returnValue = AppendParameter(columnInfo, "", place);
			return returnValue == "" ? "" : returnValue.Substring(0, returnValue.Length - 2);
		}
	}
}
