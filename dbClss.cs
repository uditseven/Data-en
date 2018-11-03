using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    class dbClss
    {
        SqlConnection gObjConn;
        SqlCommand gObjCmd;
        DataSet gObjDS;
        String gsSQLQuery = "";
        SqlDataAdapter gObjAdpt;

        public string GetConnStr()
        {
            string lsConnstr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=EX;Data Source=HP-UP\SQLEXPRESS";
            return lsConnstr;
        }
        public DataSet GetData(String isSqlQuery)
        {
            gObjConn = new SqlConnection(GetConnStr());
            gObjConn.Open();
            gsSQLQuery = isSqlQuery;
            gObjCmd = new SqlCommand(gsSQLQuery, gObjConn);
            gObjAdpt = new SqlDataAdapter(gObjCmd);
            gObjDS = new DataSet();
            gObjAdpt.Fill(gObjDS);
            return gObjDS;

        }
        public void InsertUpdateData(String isSqlQuery)
        {
            gObjConn = new SqlConnection(GetConnStr());
            gObjConn.Open();
            gsSQLQuery = isSqlQuery;
            gObjCmd = new SqlCommand(gsSQLQuery, gObjConn);
            gObjCmd.ExecuteNonQuery();
        }
    }
}
