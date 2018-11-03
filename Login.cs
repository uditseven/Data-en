using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        dbClss gObjDBClss = new dbClss();
        DataSet gObjDS = new DataSet();
        public Login()
        {
            InitializeComponent();
        }
        private void loginB_Click(object sender, EventArgs e)
        {
            if ((usernm.Text.Equals("")) || (pass1.Text.Equals("")))
            {
                MessageBox.Show("Please enter username and password");
            }
            else
            {
                string lsQuery = "Select username from T_Login where Username='" + usernm.Text + "' and pwd='" + pass1.Text + "'";
                gObjDS = gObjDBClss.GetData(lsQuery);
                if (gObjDS.Tables[0].Rows.Count > 0)
                {
                    Form1 lObjFrm1 = new Form1(usernm.Text);
                    lObjFrm1.ShowDialog();
                }
                else
                    MessageBox.Show("Enter valid username and password");
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.ShowDialog();           
        }
        private void frgt_pwd_lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }        
    }
}
