using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Net;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        dbClss gObjDBClss = new dbClss();
        DataSet gObjDS = new DataSet();
        public Form2()
        {
            InitializeComponent();
        }

        private void sbmt_frgt_pwd_Click(object sender, EventArgs e)
        {
             if (phn_fgt.Text.Equals(""))
            {
                MessageBox.Show("Please enter your registered phone number");
            }
            else
            {
                 string lsQuery = "Select username,pwd from T_Login where phone_number='" + phn_fgt.Text + "'";
                gObjDS = gObjDBClss.GetData(lsQuery);
                if (gObjDS.Tables[0].Rows.Count > 0)
                {
                    string text = "UserName=" + gObjDS.Tables[0].Rows[0][0].ToString() +"Password=" + gObjDS.Tables[0].Rows[0][1].ToString() + "";
                    string URL = "https://smsapi.engineeringtgr.com/send/?Mobile=*********&Password=*******&Message=" + text + "&To=" + phn_fgt.Text + "&Key=***********";
                    try
                    {
                        
                        Process.Start("Firefox.exe",URL);
                    }

                    catch (Exception xe)
                    {
                        Console.Out.WriteLine("------ping--------");
                        Console.Out.WriteLine(xe.Message);
                    }
                   
                }
                else
                {
                    MessageBox.Show("Enter valid phone number");
                }

            }

        }
    }

    }

