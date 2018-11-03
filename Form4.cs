using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Drawing.Printing;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Data.Odbc;
using Microsoft.VisualStudio.Data;
using System.Windows.Forms.DataVisualization.Charting; 

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void sbmt_sgnup_Click(object sender, EventArgs e)
        {
            SqlConnection lsConnstr = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=EX;Data Source=HP-UP\\SQLEXPRESS");
            SqlCommand cmd;
            lsConnstr.Open();
            string s = "insert into T_Login values(@usr_T_box_sgnup,@pwd_T_box_sgnup,@phn_T_box_sgnup)";
            cmd = new SqlCommand(s, lsConnstr);
            cmd.Parameters.AddWithValue("@usr_T_box_sgnup", usr_T_box_sgnup.Text);
            cmd.Parameters.AddWithValue("@pwd_T_box_sgnup", pwd_T_box_sgnup.Text);
            cmd.Parameters.AddWithValue("@phn_T_box_sgnup", phn_T_box_sgnup.Text);
            cmd.CommandType = CommandType.Text;

            int i = cmd.ExecuteNonQuery();
            lsConnstr.Close();

            {
                MessageBox.Show("Congratulations !! On your NEW Signup ");
            }
        }
    }
}
