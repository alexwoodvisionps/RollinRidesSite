using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace RollingRides.WebApp.Components.Datalayer.Common
{
	public class GenericRepository<M> where M : class
	{
		public GenericRepository ()
		{
		}
		
		protected SqlConnection GetNewConnection()
		{			
			return new SqlConnection(ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString);
		}
		public M GetById ( int id, string tableName, object prototype)
		{
			var dt = ExecuteQuery ("SELECT * FROM ["+tableName+"] WHERE Id = " + id);
			return dt.Rows.Count == 0 ? null : ((DataRow)dt.Rows [0]).DataRowToModel<M> (prototype);
		}
		protected DataTable ExecuteQuery (string sql, IEnumerable<SqlParameter> paramList = null, CommandType type = CommandType.Text, SqlConnection con = null)
		{
            if (con == null)
                con = GetNewConnection();
			var dt = new DataTable ();
			var adapter = new SqlDataAdapter (sql, con);
			if (paramList != null) {
				foreach (var p in paramList) {
						adapter.SelectCommand.Parameters.Add (p);
					}
				
			}
			adapter.SelectCommand.CommandType = type;
			adapter.Fill(dt);
			return dt;
		}
		protected int ExecuteNonQuery (string sql, IEnumerable<SqlParameter> parameters = null, CommandType cmdType = CommandType.Text, SqlConnection con = null)
		{
		    var flag = false;
            if(con == null)
            {
                flag = true;
                con = GetNewConnection();
            }
		    var cmd = new SqlCommand (sql, con);
			if (parameters != null) {
				foreach (var parameter in parameters) {
					cmd.Parameters.Add (parameter);
				}
			}
			cmd.CommandType = cmdType;
			var res = 0;
			try {
				cmd.Connection.Open ();
			
				res = cmd.ExecuteNonQuery ();
			}
			finally{
                if (!flag)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
			}
			return res;
		}
	}
}

