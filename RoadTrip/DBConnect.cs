using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Odbc;

namespace RoadTrip
{
    class DBConnect
    {
        //database connection string
        private static string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
            Application.StartupPath + @"..\..\..\data\rtDB.mdb";

        //temp connecting to database by fill function
        public static DataSet ExecuteDataSet(string StrSql)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter adp = new OleDbDataAdapter(StrSql, strConnection);
            try
            {
                adp.Fill(ds);
            }
            catch
            {
                MessageBox.Show("error connecting to database.");
            }

            return ds;
        }

        public static void UpdateTableInDataBase(string StrSql, DataSet news) //database updating
        {
            OleDbDataAdapter adp = new OleDbDataAdapter(StrSql, strConnection);
            OleDbCommandBuilder autoCmdBuild = new OleDbCommandBuilder(adp);

            adp.UpdateCommand = autoCmdBuild.GetUpdateCommand();
            adp.DeleteCommand = autoCmdBuild.GetDeleteCommand();
            adp.InsertCommand = autoCmdBuild.GetInsertCommand();
            adp.Update(news);

            return;
        }
    }
}
