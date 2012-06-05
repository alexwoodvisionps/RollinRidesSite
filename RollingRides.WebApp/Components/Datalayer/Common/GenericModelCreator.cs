using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RollingRides.WebApp.Components.Datalayer.Common
{
	public static class GenericModelCreator
	{
		public static List<M> DataTableToList<M> (this DataTable dt, object prototype)
		{
		    return (from DataRow dr in dt.Rows
		            let obj = System.Activator.CreateInstance(prototype.GetType())
		            select DataRowToModel<M>(dr, obj)).ToList();
		}
		public static M DataRowToModel<M> (this DataRow dr, object model)
		{
			//object obj = System.Reflection.Assembly.GetExecutingAssembly ().CreateInstance (classString);
			foreach (DataColumn col in dr.Table.Columns)
			{
			    var val = dr[col.ColumnName] == DBNull.Value ? null : dr[col.ColumnName];
				AssignProperty (model, col.ColumnName, val);
			}
			return (M) model;
		}
		private static bool HasProperty (object obj, string name)
		{
			var props = obj.GetType ().GetProperties ();
		    var en = props.SingleOrDefault(x => x.Name == name);
			return en != null;
		}
		private static void AssignProperty (object obj, string propertyName, object value)
		{
			if (HasProperty (obj, propertyName))
				obj.GetType ().InvokeMember (propertyName, BindingFlags.Public | BindingFlags.Default | BindingFlags.Instance | BindingFlags.SetProperty, 
			                              Type.DefaultBinder, obj, new object[]{value});
		}
	}
}

