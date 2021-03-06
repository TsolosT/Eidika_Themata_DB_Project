﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Project_2018
{
    public partial class Form3 : Form
    {

        SqlConnection conn;
        SqlDataAdapter dataAdapter1, dataAdapter2;
        DataSet dataSet2;
        BindingSource  bindingSource2;

       

        public Form3()
        {
   
            InitializeComponent();
            conn = new SqlConnection("Data Source = wubeast\\mssqlserver01; Initial Catalog = APOTHIKI_4179; Integrated Security = True");
            conn.Open();

            dataAdapter1 = new SqlDataAdapter("Select * from APOTHIKI", conn);
            DataTable dt1 = new DataTable();
            dataAdapter1.Fill(dt1);
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "EIDOS";
   
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
            {
                MessageBox.Show("Connection Error!"); Application.Exit();
            }
 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDataSet();
        }


        public void fillDataSet()
        {
            dataAdapter2 = new SqlDataAdapter("SELECT KOD_PAR,K_PEL,HMER_PARAGELIAS,TROPOS_PLHROMHS,TOPOS_PARADOSHS,TIMH_POLHSHS,FPA,POSOTHTA,SUM(TIMH_POLHSHS*POSOTHTA*((FPA/100)*TIMH_POLHSHS)) AS TELIKI_TIMH FROM PARAGELIA INNER JOIN(PROIONTA_PARAGELIAS INNER JOIN APOTHIKI ON PROIONTA_PARAGELIAS.K_E = APOTHIKI.KE) ON PARAGELIA.KOD_PAR = PROIONTA_PARAGELIAS.K_PAR WHERE APOTHIKI.EIDOS = '" + comboBox1.Text.ToString() +"' GROUP BY KOD_PAR, HMER_PARAGELIAS, K_PEL, POSOTHTA, TROPOS_PLHROMHS, TOPOS_PARADOSHS, TIMH_POLHSHS, FPA, POSOTHTA", conn);
            dataSet2 = new DataSet();
            dataAdapter2.Fill(dataSet2);
            bindingSource2 = new BindingSource();
            DataTable dt = new DataTable();
            bindingSource2.DataSource = dataSet2.Tables[0].DefaultView;
            dataGridView1.DataSource = bindingSource2;
            int sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value); 
            }
            label4.Text = sum.ToString();
            
        }

    

    }
}
