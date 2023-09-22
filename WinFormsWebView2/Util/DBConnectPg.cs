using System;
using System.Data;
using System.IO;
using Npgsql;

namespace DBConstGenerator.Util
{

    class DBConnectPg
    {
        public static string CONNECT_CONF_PATH = System.IO.Directory.GetCurrentDirectory() + "\\temp\\" + "connect.xml";

        public static NpgsqlConnection? gConnect;
        public static NpgsqlTransaction? gTran;

        public static string? errMsg;

        /// <summary>
        /// DB接続
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="dbname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ConnectionOpen(string server, string port, string dbname, string username, string password)
        {
            errMsg = "";
            try
            {
                string connectInfo = "Server=" + server
                    + ";Port=" + port
                    + ";User Id=" + username
                    + ";Password=" + password
                    + ";Database=" + dbname + ";"
                    ;
                gConnect = new NpgsqlConnection(connectInfo);
                gConnect.Open();

                if (gTran != null)
                {
                    gTran.Dispose();
                }
                gTran = null;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errMsg = errMsg + ":" + ex.InnerException.Message;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// DB切断
        /// </summary>
        /// <returns></returns>
        public static bool ConnectionClose()
        {
            errMsg = "";
            try
            {
                if (gTran != null)
                {
                    gTran.Dispose();
                }
                gTran = null;

                if (gConnect != null)
                {
                    gConnect.Close();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        /// <returns></returns>
        public static bool trasactionStart()
        {
            errMsg = "";
            try
            {
                if (gConnect == null)
                {
                    errMsg = "DBに接続されていません。";
                    return false;
                }

                if (gTran == null)
                {
                    gTran= gConnect.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// トランザクションロールバック
        /// </summary>
        /// <returns></returns>
        public static bool TrasactionRollback()
        {
            errMsg = "";
            try
            {
                if (gTran != null)
                {
                    gTran.Rollback();
                    gTran.Dispose();
                    gTran = null;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// トランザクションコミット
        /// </summary>
        /// <returns></returns>
        public static bool TrasactionRCommit()
        {
            errMsg = "";
            try
            {
                if (gTran != null)
                {
                    gTran.Commit();
                    gTran.Dispose();
                    gTran = null;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool Execute(string sql)
        {
            errMsg = "";
            try
            {
                if (gConnect == null)
                {
                    errMsg = "DBに接続されていません。";
                    return false;
                }

                NpgsqlCommand command = new NpgsqlCommand(sql, gConnect);

                if (gTran != null)
                {
                    command.Transaction = gTran;
                }

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool Execute(string sql, List<KeyValuePair<string, string>> param)
        {
            errMsg = "";
            try
            {
                if (gConnect == null)
                {
                    errMsg = "DBに接続されていません。";
                    return false;
                }

                NpgsqlCommand command = new NpgsqlCommand(sql, gConnect);
                foreach (KeyValuePair<string, string> pair in param)
                {
                    command.Parameters.Add(pair);
                }

                if (gTran != null)
                {
                    command.Transaction = gTran;
                }

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// SELECT文実行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ExecuteSelect(string sql, ref DataTable data)
        {
            errMsg = "";
            try
            {
                if (gConnect == null)
                {
                    errMsg = "DBに接続されていません。";
                    return false;
                }

                DataSet set = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, gConnect);
                adapter.Fill(set);
                data = set.Tables[0];
                set.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(sql);
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// SELECT文実行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ExecuteSelect(string sql, KeyValuePair<string, string>[] param, ref DataTable data)
        {
            errMsg = "";
            try
            {
                if (gConnect == null)
                {
                    errMsg = "DBに接続されていません。";
                    return false;
                }

                NpgsqlCommand command = new NpgsqlCommand(sql, gConnect);
                foreach (KeyValuePair<string, string> pair in param)
                {
                    command.Parameters.Add(pair);
                }


                DataSet set = new DataSet();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                adapter.Fill(set);
                data = set.Tables[0];
                set.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(sql);
                errMsg = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// ファイルから読み込み
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetSqlFromFile(string filePath)
        {
            string result = "";
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                result = result + sr.ReadToEnd().Replace("\r", " ").Replace("\n", " ");
            }
            return result;
        }

        public static string GetSqlFromFile(string filePath, string[] param)
        {
            string result = "";
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                string sqlLine = sr.ReadToEnd();
                result = result + sqlLine.Replace("\r", " ").Replace("\n", " ");
            }

            for (int i = 0; i < param.Length; i++)
            {
                result = result.Replace("[" + i.ToString() + "]", param[i]);
            }

            return result;
        }

    }
}
