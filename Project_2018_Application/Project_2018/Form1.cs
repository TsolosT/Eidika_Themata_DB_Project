using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


//Data Source=wubeast\mssqlserver01;Initial Catalog=APOTHIKI_4179;Integrated Security=True
namespace Project_2018
{
    public partial class Form1 : Form
    {

        SqlConnection connection;
        SqlDataAdapter dataAdapter1, dataAdapter2, dataAdapter3, dataAdapter4;
        DataSet dataSet1, dataSet2, dataSet3,dataSet4;
        BindingSource bindingSource1,bindingSource2,bindingSource3,bindingSource4;
        SqlCommand command, command1;
        SqlCommandBuilder cmdbl;

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=wubeast\\mssqlserver01;Initial Catalog=APOTHIKI_4179;Integrated Security=True");
            connection.Open();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open) 
            { //tab1
                dataAdapter1 = new SqlDataAdapter(@"select * from APOTHIKI", connection);
                dataSet1 = new DataSet();
                dataAdapter1.Fill(dataSet1, "APOTHIKI_Table");
                bindingSource1 = new BindingSource();
                bindingSource1.DataSource = dataSet1.Tables[0].DefaultView;
                bindingNavigator1.BindingSource = bindingSource1;
                dataGridView1.DataSource = bindingSource1;
                textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", bindingSource1, "FOTO", true));
                textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", bindingSource1, "KE", true));
                refreshImage1();
                //tab2
                dataAdapter2 = new SqlDataAdapter(@"select * from PELATHS", connection);
                dataSet2 = new DataSet();
                dataAdapter2.Fill(dataSet2, "PELATHS_Table");
                bindingSource2 = new BindingSource();
                bindingSource2.DataSource = dataSet2.Tables[0].DefaultView;
                bindingNavigator2.BindingSource = bindingSource2;
                dataGridView2.DataSource = bindingSource2;
                textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text",bindingSource2, "FOTO", true));
                textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text",bindingSource2, "KOD_PELATHS", true));
                refreshImage();
                //tab3
                dataAdapter3 = new SqlDataAdapter(@"select * from PARAGELIA", connection);
                dataSet3 = new DataSet();
                dataAdapter3.Fill(dataSet3, "PARAGELIA_Table");
                bindingSource3 = new BindingSource();
                bindingSource3.DataSource = dataSet3.Tables[0].DefaultView;
                bindingNavigator3.BindingSource = bindingSource3;
                dataGridView3.DataSource = bindingSource3;

                //tab4
                dataAdapter4 = new SqlDataAdapter(@"select * from PROIONTA_PARAGELIAS", connection);
                dataSet4 = new DataSet();
                dataAdapter4.Fill(dataSet4, "PROIONTA_PARAGELIAS_Table");
                bindingSource4 = new BindingSource();
                bindingSource4.DataSource = dataSet4.Tables[0].DefaultView;
                bindingNavigator4.BindingSource = bindingSource4;
                dataGridView4.DataSource = bindingSource4;
                //tab5
                             
                this.reportViewer2.RefreshReport();
                //tab6

                this.reportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("Connection Error!"); Application.Exit();
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            String openPath1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openPath1 = openFileDialog1.InitialDirectory +
                openFileDialog1.FileName;
                textBox4.Text = openPath1;
                pictureBox2.Image = Image.FromFile(openPath1);
                command1 = new SqlCommand("update APOTHIKI set FOTO = '" + openPath1 + "'where KE = " + textBox3.Text + "; ", connection);
                command1.ExecuteNonQuery();
            }
        }

        private void bindingNavigator2_RefreshItems(object sender, EventArgs e)
        {
            refreshImage();

        }
        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {
            refreshImage1();

        }
        private void saveToolStripButton1_Click(object sender, EventArgs e)
        {
            cmdbl = new SqlCommandBuilder(dataAdapter1);
            dataAdapter1.Update(dataSet1, "APOTHIKI_Table");
            MessageBox.Show("Information Updated");
        }

        private void saveToolStripButton2_Click(object sender, EventArgs e)
        {
            cmdbl = new SqlCommandBuilder(dataAdapter2);
            dataAdapter2.Update(dataSet2, "PELATHS_Table");
            MessageBox.Show("Information Updated");
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            cmdbl = new SqlCommandBuilder(dataAdapter4);
            dataAdapter4.Update(dataSet4, "PROIONTA_PARAGELIAS_Table");
            MessageBox.Show("Information Updated");
        }

        private void saveToolStripButton3_Click(object sender, EventArgs e)
        {
            cmdbl = new SqlCommandBuilder(dataAdapter3);
            dataAdapter3.Update(dataSet3, "PARAGELIA_Table");
            MessageBox.Show("Information Updated");
        }


        private void button3_Click(object sender, EventArgs e)
        {
            String openPath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openPath = openFileDialog1.InitialDirectory +
                openFileDialog1.FileName;
                textBox1.Text = openPath;
                pictureBox1.Image = Image.FromFile(openPath);
                command = new SqlCommand("update PELATHS set FOTO = '"+openPath+"'where KOD_PELATHS = "+textBox2.Text+"; ",connection);
                 command.ExecuteNonQuery();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm1 = new Form3();
            frm1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        public void refreshImage()
        {
            String photoPath = textBox1.Text.Trim();
            if (photoPath != null && File.Exists(photoPath))
            {
                pictureBox1.Image = Image.FromFile(photoPath);         
            }
            else
            {
                pictureBox1.Image = Image.FromFile("D:\\wubeast\\Αρχεια(διαφορα)\\ΜΗΧΑΝΙΚΟΣ ΠΛΗΡΟΦΟΡΙΚΗΣ ΜΑΘΗΜΑΤΑ Τ.Ε.Ι\\Sql_Lab\\Project_2018\\img\\Oopsbutton.jpg");
              
            }
        }
        public void refreshImage1()
        {
            String photoPath1 = textBox4.Text.Trim();
            if (photoPath1 != null && File.Exists(photoPath1))
            {
                   pictureBox2.Image = Image.FromFile(photoPath1);
            }
            else
            {
                
                pictureBox2.Image = Image.FromFile("D:\\wubeast\\Αρχεια(διαφορα)\\ΜΗΧΑΝΙΚΟΣ ΠΛΗΡΟΦΟΡΙΚΗΣ ΜΑΘΗΜΑΤΑ Τ.Ε.Ι\\Sql_Lab\\Project_2018\\img\\Oopsbutton.jpg");
            }
        }
    }
}
