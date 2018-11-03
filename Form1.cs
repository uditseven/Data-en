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
using Dynamsoft.UVC;
using Dynamsoft.Core;
using Dynamsoft.Common;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Drawing.Imaging;
using WebcamDemo;
using BusinessRefinery.Barcode;
using ZXing.Common;
using ZXing;
using ZXing.QrCode;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SqlConnection lsConnstr = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=EX;Data Source=HP-UP\\SQLEXPRESS");
        SqlCommand cmd;
        String gsUserName;       
        dbClss gObjDBClss = new dbClss();
        DataSet gObjDS = new DataSet();
        int lnPrgInc = 0;
        int counter = 5000;
        QrCodeEncodingOptions options = new QrCodeEncodingOptions();
        public Form1(string lsUserName)
        {
            InitializeComponent();
            gsUserName = lsUserName;
        }  
        private void nameT_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(nameT.Text, "[^a-z||^A-Z]"))
            {
                nameT.Text = "";
            }             
            progressBar1.Value = nameT.TextLength;
            if (progressBar1.Value > 0)
                lnPrgInc = 10;
            progressBar1.Value = lnPrgInc;
            int per = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.Refresh();
            progressBar1.CreateGraphics().DrawString(per.ToString() + "%", new Font("Arial", (float)10, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));           
        }       
        private void nameT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void salT_TextChanged(object sender, EventArgs e)
        {
                if (progressBar1.Value > 0)
                    lnPrgInc = 40;
                progressBar1.Value = lnPrgInc;
                int per = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                progressBar1.Refresh();
                progressBar1.CreateGraphics().DrawString(per.ToString() + "%", new Font("Arial", (float)10, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));                  
        }
        private void dateoB_ValueChanged(object sender, EventArgs e)
        {   
            if (progressBar1.Value > 0)
                lnPrgInc = 20;
            progressBar1.Value = lnPrgInc;
            int per = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.Refresh();
            progressBar1.CreateGraphics().DrawString(per.ToString() + "%", new Font("Arial", (float)10, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));           
        }
        private void occupT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (progressBar1.Value > 0)
                lnPrgInc = 30;
            progressBar1.Value = lnPrgInc;
            int per = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.Refresh();
            progressBar1.CreateGraphics().DrawString(per.ToString() + "%", new Font("Arial", (float)10, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));           
        }
        private void marital1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (progressBar1.Value > 0)
                lnPrgInc = 50;
            progressBar1.Value = lnPrgInc;
            int per = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.Refresh();
            progressBar1.CreateGraphics().DrawString(per.ToString() + "%", new Font("Arial", (float)10, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));           
        }
        private void healthy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (progressBar1.Value > 0)
                lnPrgInc = 60;
            progressBar1.Value = lnPrgInc;           
            int per = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.Refresh();
            progressBar1.CreateGraphics().DrawString(per.ToString() + "%", new Font("Arial", (float)10, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));          
        }
        private void children_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (progressBar1.Value > 0)
                lnPrgInc = 70;
            progressBar1.Value = lnPrgInc;
              
            int per = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.Refresh();
            progressBar1.CreateGraphics().DrawString(per.ToString() + "%", new Font("Arial", (float)10, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));
            
        }
               
        private void register_Click(object sender, EventArgs e)
        {
            CreateTable();
            SqlConnection lsConnstr = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=EX;Data Source=HP-UP\\SQLEXPRESS");            
            SqlCommand cmd;
            lsConnstr.Open();
            string filepath = Application.StartupPath + "\\capturedimg.bmp";
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            br.Close();
            fs.Close();
            string s = "Insert into T_" + gsUserName + " values(@nameT,@marital1,@dateoB,@healthy,@occupT,@children,@salT,@profile_photo,@id_txtbx)";
            cmd = new SqlCommand(s, lsConnstr);
            cmd.Parameters.AddWithValue("@nameT", nameT.Text);
            cmd.Parameters.AddWithValue("@marital1", marital1.Text);
            cmd.Parameters.AddWithValue("@dateoB", dateoB.Text);
            cmd.Parameters.AddWithValue("@healthy", healthy.Text);
            cmd.Parameters.AddWithValue("@occupT", occupT.Text);
            cmd.Parameters.AddWithValue("@children", children.Text); 
            cmd.Parameters.AddWithValue("@salT", salT.Text);
            cmd.Parameters.AddWithValue("@profile_photo",bytes);
            cmd.Parameters.AddWithValue("@id_txtbx", id_txtbx.Text);

            progressBar1.PerformStep();
            cmd.CommandType = CommandType.Text;

            int i = cmd.ExecuteNonQuery();
            lsConnstr.Close();

            {
                MessageBox.Show(" data entered successfully ");
            }
        }
        public void CreateTable()
        {
            try
            {
                SqlConnection lsConnstr = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=EX;Data Source=HP-UP\\SQLEXPRESS");
                SqlCommand cmd;
                lsConnstr.Open();
                string s = "";
                s = "Create table T_" + gsUserName + "(name nchar(20) ,marital_status nchar(20) ,date_of_birth nchar(20) ,health_status nchar(20) ,occupation nchar(20) ,no_of_children nchar(20) ,salary nchar(20),ID_no nchar(10)";
                cmd = new SqlCommand(s, lsConnstr);
                cmd.ExecuteNonQuery();
            }
            catch (Exception )
            { 
                
            }
            
        }
        private void clear_Click(object sender, EventArgs e)
        {
            nameT.Text = "\0";
            salT.Text = "\0";
            marital1.Text = "\0";
            occupT.Text = "\0";
            healthy.Text = "\0";
            children.Text = "\0";
            id_txtbx.Text = "\0";

        }
        private void tablegridinside_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView lObjDtGrdView = (DataGridView)sender;
            int index = e.RowIndex;
            name2.Text = lObjDtGrdView.Rows[index].Cells[0].Value.ToString();
            dateTimePicker2.Text = lObjDtGrdView.Rows[index].Cells[2].Value.ToString();
            occupS.Text = "\0";
            occupS.SelectedText = lObjDtGrdView.Rows[index].Cells[4].Value.ToString();
            MaritS.Text = "\0";
            MaritS.SelectedText = lObjDtGrdView.Rows[index].Cells[1].Value.ToString();
            comboBox4.Text = lObjDtGrdView.Rows[index].Cells[3].Value.ToString();
            textBox3.Text = lObjDtGrdView.Rows[index].Cells[5].Value.ToString();
            SalS.Text = lObjDtGrdView.Rows[index].Cells[6].Value.ToString();
            id_srch.Text = lObjDtGrdView.Rows[index].Cells[8].Value.ToString();
            if (!lObjDtGrdView.Rows[index].Cells[7].Value.ToString().Equals(""))
            {
                var photo = (Byte[])(lObjDtGrdView.Rows[index].Cells[7].Value);
                var fs = new MemoryStream(photo);
                prof_photoMang.Image = Image.FromStream(fs);
                prof_photoMang.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            options = new QrCodeEncodingOptions()
            {              
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 107,
                Height = 107,
                Margin=0,
            };
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options; 

            try
            {
                string lsQuery = "Select * from T_" + gsUserName + "";
                gObjDS = gObjDBClss.GetData(lsQuery);
                if (gObjDS.Tables[0].Rows.Count > 0)
                {
                    tablegridinside.DataSource = gObjDS.Tables[0];
                }
                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1.RunWorkerAsync();
                timer1.Enabled = true;
                timer1.Start();             
                timer1.Tick += new EventHandler(timer1_Tick);
                
            }
            catch (Exception )
            { 
            }
      
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            
            counter--;
           countdown.Text = counter.ToString();
           if (Formwebcam.imgflag == 1)
               profile_photo.ImageLocation = Application.StartupPath + "\\capturedimg.bmp";
           profile_photo.SizeMode = PictureBoxSizeMode.StretchImage;
            if (counter == 0)
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1;
            if (or.SelectedItem.Equals("OR"))
            {
                if (comboBox2.SelectedIndex==-1)
                {
                    query1 = "Select * from T_" + gsUserName + " where marital_status='" + comboBox3.SelectedItem.ToString() + "'";
                }
                else
                {
                    query1 = "Select * from T_" + gsUserName + " where occupation='" + comboBox2.SelectedItem.ToString() + "'";
                }
            }
            else
            {
                query1 = "Select * from T_" + gsUserName + " where occupation='" + comboBox2.SelectedItem.ToString() + "' and marital_status='" + comboBox3.SelectedItem.ToString() + "'";
            }
        
            gObjDS = gObjDBClss.GetData(query1);
            if (gObjDS.Tables[0].Rows.Count > 0)
            {
                tablegridinside.DataSource = null;
                tablegridinside.DataSource = gObjDS.Tables[0];
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string lsQuery = "Delete from T_" + gsUserName + " where name='" + name2.Text + "'";
            gObjDBClss.InsertUpdateData(lsQuery);
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            string lsQuery = "Select * from T_" + gsUserName + "";
            gObjDS = gObjDBClss.GetData(lsQuery);
            if (gObjDS.Tables[0].Rows.Count > 0)
            {
                tablegridinside.DataSource = null;
                tablegridinside.DataSource = gObjDS.Tables[0];
            }
        }
        private void copyAlltoClipboard()
        {
            tablegridinside.RowHeadersVisible = false;
            tablegridinside.SelectAll();
            DataObject dataObj = tablegridinside.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void export_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

        }

        private void print_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.tablegridinside.Width, this.tablegridinside.Height);
            tablegridinside.DrawToBitmap(bm, new Rectangle(0, 0, this.tablegridinside.Width, this.tablegridinside.Height));
            e.Graphics.DrawImage(bm, 0, 0);        
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (nameT.Text != " ")
            {
                string filepath = Application.StartupPath + "\\capturedimg.bmp";
                FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);                
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                br.Close();
                fs.Close();
                cmd = new SqlCommand("update T_" + gsUserName + " set marital_status=@marital1,date_of_birth=@dateoB,health_status=@healthy,occupation=@occupT, no_of_children=@children,salary=@salT,photo=@prof_photoMang,ID_no=@id_txtbx  where name=@nameT", lsConnstr);
                lsConnstr.Open();
                cmd.Parameters.AddWithValue("@nameT", name2.Text);
                cmd.Parameters.AddWithValue("@marital1", MaritS.Text);
                cmd.Parameters.AddWithValue("@dateoB", dateTimePicker2.Text);
                cmd.Parameters.AddWithValue("@healthy", comboBox4.Text);
                cmd.Parameters.AddWithValue("@occupT", occupS.Text);
                cmd.Parameters.AddWithValue("@children", textBox3.Text);
                cmd.Parameters.AddWithValue("@salT", SalS.Text);
                cmd.Parameters.AddWithValue("@prof_photoMang",bytes);
                cmd.Parameters.AddWithValue("@id_txtbx", id_srch.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("record updated successfully");
                lsConnstr.Close();

            }
            else
            {
                MessageBox.Show("please select record");
            }
        }
        public DataTable readexcel(string filename, string fileext)
        {
            string lsConnstr = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileext.CompareTo(".xls") == 0)
            {
                lsConnstr = "Provider=Microsoft.Jet.OLEDB.8.0;Data source=" + filename + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";
            }
            else
            {
                lsConnstr = "Provider=Microsoft.Ace.OLEDB.12.0;Data source=" + filename + ";Extended Properties='Excel 12.0;HRD=No';";
            }
            using (OleDbConnection con = new OleDbConnection(lsConnstr))
            {
                try
                {
                    OleDbDataAdapter oleadpt = new OleDbDataAdapter("select * from [sheet1$]", con);
                    oleadpt.Fill(dtexcel);
                }
                catch { }
            }
            return dtexcel;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "XML Files(*.xml;*.xls;*.xlsx;*.xlsm;*.xlsb)|*.xml;*.xls;*.xlsx;*.xlsm;*.xlsb";
                openFileDialog1.FilterIndex = 3;
                openFileDialog1.Multiselect = false;
                openFileDialog1.Title = "open text file";
                openFileDialog1.InitialDirectory = @"Desktop";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string pathname = openFileDialog1.FileName;
                    textBox1.Text = pathname;
                    String lsSheetSelected = "sheet1";
                    String lsExcelQuery = "select * from [" + lsSheetSelected + "$]";
                    OleDbConnection lObjOledbConn;
                    DataSet lObjOledbDataSet;
                    OleDbDataAdapter lObjOledbAdptr;
                    String lObjConnStr = "";
                    if (textBox1.Text.Split('.')[1] == "xls")
                        lObjConnStr =@"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Persist Security Info=False";
                    else
                        lObjConnStr =@"Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0 Xml;HDR=YES';Persist Security Info=False";
                    
                    lObjConnStr = lObjConnStr + "Data Source=" + textBox1.Text.ToString();
                    
                    lObjOledbConn = new OleDbConnection(lObjConnStr);
                    if (lObjOledbConn.State != ConnectionState.Open)
                        lObjOledbConn.Open();
                    lObjOledbAdptr = new OleDbDataAdapter(lsExcelQuery, lObjOledbConn);
                    
                    
                    lObjOledbConn.ConnectionString = "Data Source=.\\SQLExpress;" + "User Instance=true;" + "User Id=username;" + "Password=password;" + "AttachDbFileName=C:\\Program Files (x86)\\Microsoft SQL Server\\MSSQL.1\\MSSQL\\Data\\EX.mdf;";
                  
                    lObjOledbAdptr.TableMappings.Add("Table","TestTable");
                    lObjOledbDataSet = new DataSet();
                    lObjOledbAdptr.Fill(lObjOledbDataSet);
                    tablegridinside.DataSource = lObjOledbDataSet.Tables[0];
                    MessageBox.Show("File Imported successfully..!!");
                   
                    
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        private void go_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("children");
            chart1.Titles.Clear();

            SqlConnection lsConnstr = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=EX;Data Source=HP-UP\\SQLEXPRESS");
            DataSet ds = new DataSet();
            lsConnstr.Open();
            if (gphslct1.SelectedItem.Equals("Name") && gphslct2.SelectedItem.Equals("No. of Children"))
            {
                SqlDataAdapter adapt = new SqlDataAdapter("Select name,no_of_children from T_" + gsUserName + "", lsConnstr);
                adapt.Fill(ds);

                chart1.DataSource = ds;
                chart1.Series["children"].XValueMember = "name";
                chart1.Series["children"].YValueMembers = "no_of_children";
                chart1.Titles.Add("CHILDREN CHART");
                chart1.ChartAreas[0].AxisX.Title = "NAME";
                chart1.ChartAreas[0].AxisY.Title = "CHILDREN";
                chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart1.ChartAreas[0].AxisX.Minimum = 1;
                chart1.ChartAreas[0].AxisX.Maximum = ds.Tables[0].Rows.Count;
                chart1.ChartAreas[0].AxisX.Interval = 1;

                chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
                chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
                chart1.ChartAreas[0].AxisY.Minimum = double.NaN;
                chart1.ChartAreas[0].RecalculateAxesScale();
            }
            else
                if (gphslct1.SelectedItem.Equals("Name") && gphslct2.SelectedItem.Equals("date of birth"))
                {
                    SqlDataAdapter adapt = new SqlDataAdapter("Select name,Floor(DateDiff(d, date_of_birth, GetDate()) / 365.25) as date_of_birth from T_" + gsUserName + "", lsConnstr);
                    adapt.Fill(ds);
                    chart1.DataSource = ds;
                    chart1.Series["children"].XValueMember = "name";
                    chart1.Series["children"].YValueMembers = "date_of_birth";
                    chart1.Titles.Add("AGE CHART");
                    chart1.ChartAreas[0].AxisX.Title = "NAME";
                    chart1.ChartAreas[0].AxisY.Title = "DATE OF BIRTH";
                    chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                    chart1.ChartAreas[0].AxisX.Minimum = 1;
                    chart1.ChartAreas[0].AxisX.Maximum = ds.Tables[0].Rows.Count;
                    chart1.ChartAreas[0].AxisX.Interval = 1;

                    chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
                    chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                    chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
                    chart1.ChartAreas[0].AxisY.Minimum = double.NaN;
                    chart1.ChartAreas[0].RecalculateAxesScale();
                }
                else
                    if (gphslct1.SelectedItem.Equals("Name") && gphslct2.SelectedItem.Equals("Salary"))
                    {
                        SqlDataAdapter adapt = new SqlDataAdapter("Select name,salary from T_" + gsUserName + "", lsConnstr);
                        adapt.Fill(ds);
                        chart1.DataSource = ds;
                        chart1.Series["children"].XValueMember = "name";
                        chart1.Series["children"].YValueMembers = "salary";
                        chart1.Titles.Add("SALARY CHART");
                        chart1.ChartAreas[0].AxisX.Title = "NAME";
                        chart1.ChartAreas[0].AxisY.Title = "SALARY";
                        chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                        chart1.ChartAreas[0].AxisX.Minimum = 1;
                        chart1.ChartAreas[0].AxisX.Maximum = ds.Tables[0].Rows.Count;
                        chart1.ChartAreas[0].AxisX.Interval = 1;

                        chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
                        chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                        chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
                        chart1.ChartAreas[0].AxisY.Minimum = double.NaN;
                        chart1.ChartAreas[0].RecalculateAxesScale();
                    }
                    else
                        if (gphslct1.SelectedItem.Equals("Occupation") && gphslct2.SelectedItem.Equals("No. of Children"))
                        {
                            SqlDataAdapter adapt = new SqlDataAdapter("Select occupation,no_of_children from T_" + gsUserName + "", lsConnstr);
                            adapt.Fill(ds);

                            chart1.DataSource = ds;
                            chart1.Series["children"].XValueMember = "occupation";
                            chart1.Series["children"].YValueMembers = "no_of_children";
                            chart1.Titles.Add("OCCUPATION-CHILDREN CHART");
                            chart1.ChartAreas[0].AxisX.Title = "OCCUPATION";
                            chart1.ChartAreas[0].AxisY.Title = "CHILDREN";
                            chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                            chart1.ChartAreas[0].AxisX.Minimum = 1;
                            chart1.ChartAreas[0].AxisX.Maximum = ds.Tables[0].Rows.Count;
                            chart1.ChartAreas[0].AxisX.Interval = 1;

                            chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
                            chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                            chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
                            chart1.ChartAreas[0].AxisY.Minimum = double.NaN;
                            chart1.ChartAreas[0].RecalculateAxesScale();
                        }
                        else
                            if (gphslct1.SelectedItem.Equals("Occupation") && gphslct2.SelectedItem.Equals("date of birth"))
                            {
                                SqlDataAdapter adapt = new SqlDataAdapter("Select occupation,Floor(DateDiff(d, date_of_birth, GetDate()) / 365.25) as date_of_birth  from T_" + gsUserName + "", lsConnstr);
                                adapt.Fill(ds);
                                chart1.DataSource = ds;
                                chart1.Series["children"].XValueMember = "occupation";
                                chart1.Series["children"].YValueMembers = "date_of_birth";
                                chart1.Titles.Add("OCCUPATION-AGE CHART");
                                chart1.ChartAreas[0].AxisX.Title = "OCCUPATION";
                                chart1.ChartAreas[0].AxisY.Title = "DATE  OF BIRTH";
                                chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                                chart1.ChartAreas[0].AxisX.Minimum = 1;
                                chart1.ChartAreas[0].AxisX.Maximum = ds.Tables[0].Rows.Count;
                                chart1.ChartAreas[0].AxisX.Interval = 1;

                                chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
                                chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                                chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
                                chart1.ChartAreas[0].AxisY.Minimum = double.NaN;
                                chart1.ChartAreas[0].RecalculateAxesScale();
                            }
                            else
                                if (gphslct1.SelectedItem.Equals("Occupation") && gphslct2.SelectedItem.Equals("Salary"))
                                {
                                    SqlDataAdapter adapt = new SqlDataAdapter("Select occupation,salary from T_" + gsUserName + "", lsConnstr);
                                    adapt.Fill(ds);
                                    chart1.DataSource = ds;
                                    chart1.Series["children"].XValueMember = "occupation";
                                    chart1.Series["children"].YValueMembers = "salary";
                                    chart1.Titles.Add("OCCUPATION-SALARY CHART");
                                    chart1.ChartAreas[0].AxisX.Title = "OCCUPATION";
                                    chart1.ChartAreas[0].AxisY.Title = "SALARY";
                                    chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                                    chart1.ChartAreas[0].AxisX.Minimum = 1;
                                    chart1.ChartAreas[0].AxisX.Maximum = ds.Tables[0].Rows.Count;
                                    chart1.ChartAreas[0].AxisX.Interval = 1;

                                    chart1.ChartAreas[0].AxisX.Maximum = double.NaN;
                                    chart1.ChartAreas[0].AxisX.Minimum = double.NaN;
                                    chart1.ChartAreas[0].AxisY.Maximum = double.NaN;
                                    chart1.ChartAreas[0].AxisY.Minimum = double.NaN;
                                    chart1.ChartAreas[0].RecalculateAxesScale();
                                }           
            lsConnstr.Close();
        }

        private void capturepic_Click(object sender, EventArgs e)
        {
            Formwebcam frmwbcam = new Formwebcam();
            frmwbcam.ShowDialog();             
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            if (Formwebcam.imgflag == 1)
                profile_photo.ImageLocation = Application.StartupPath + "\\capturedimg.bmp";
        }
        
        private void profile_photo_Click(object sender, EventArgs e)
        {
            Formwebcam fwbcm = new Formwebcam();
            fwbcm.Show();
            ImageCore m_ImageCore = new ImageCore();

        }

        private void updt_phto_Click(object sender, EventArgs e)
        {
            prof_photoMang.Image = null;
            Formwebcam frmwbcam = new Formwebcam();
            frmwbcam.ShowDialog();
            counter--;
            countdown.Text = counter.ToString();
            if (Formwebcam.imgflag == 1)
                prof_photoMang.ImageLocation = Application.StartupPath + "\\capturedimg.bmp";
            prof_photoMang.SizeMode = PictureBoxSizeMode.StretchImage;
            if (counter == 0)
                this.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty (id_srch.Text) || String.IsNullOrEmpty(id_srch.Text))
            {
                qr_code.Image = null;
                MessageBox.Show("Text not found", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var qr = new ZXing.BarcodeWriter();
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;
                var result = new Bitmap(qr.Write(id_srch.Text.Trim()));
                qr_code.Image = result;
             
            }

            prof_photoMang_cpy.Image = prof_photoMang.Image;
            prof_photoMang_cpy.SizeMode = PictureBoxSizeMode.StretchImage;
            qr_code_cpy.Image = qr_code.Image;
            id_srch_cpy.Text = id_srch.Text;
            name2_cpy.Text = name2.Text;
            dateTimePicker2_cpy.Text = dateTimePicker2.Text;
            occupS_cpy.Text = occupS.Text;
            MaritS_cpy.Text = MaritS.Text;
        }

        private void pnt_ID_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            PrintDocument Pd = new PrintDocument();
            Pd.PrintPage += printDocument2_PrintPage;
            ppd.Document = Pd;
            ppd.ShowDialog();
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(identity_box.ClientRectangle.Width, identity_box.ClientRectangle.Height);
            identity_box.DrawToBitmap(bmp, identity_box.ClientRectangle);
            e.Graphics.DrawImage(bmp, 0, 0);

        } 
          }
}










