using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection sqlConnection;
        bool checkData = false,
             checkOrg = false,
             checkCity = false,
             checkCountry = false,
             checkMan = false;
        string stringDate = "",
               stringOrg = "",
               stringCity = "",
               stringCountry = "",
               stringMan = "";

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sqlConnection.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkOrg = !checkOrg;
            if (checkBox2.Checked) stringOrg = "[Organization], ";
            else stringOrg = "";
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkCountry = !checkCountry;
            if (checkBox4.Checked) stringCountry = "[Country], ";
            else stringCountry = "";
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkMan = !checkMan;
            if (checkBox5.Checked) stringMan = "[Mananger], ";
            else stringMan = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dGV1.DataSource = null;
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rrafa\Desktop\WindowsFormsApp\WindowsFormsApp\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            if (!(checkMan || checkOrg || checkCity || checkCountry || checkData))
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM [base_Table]", sqlConnection);
                    DataTable dtb = new DataTable();
                    sqlDataAdapter.Fill(dtb);
                    dGV1.DataSource = dtb;
                }
            }
            else
            {
                string select = stringDate + stringOrg + stringCity + stringCountry + stringMan;
                int x = select.Length - 2;
                string group = select.Remove(x);
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT "+select+"SUM([Count]) AS [Count], SUM([Summ]) AS [Summ] FROM [base_Table] GROUP BY "+group, sqlConnection);
                    DataTable dtb = new DataTable();
                    sqlDataAdapter.Fill(dtb);
                    dGV1.DataSource = dtb;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkData = !checkData;
            if (checkBox1.Checked) stringDate = "[Date], ";
            else stringDate = "";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkCity = !checkCity;
            if (checkBox3.Checked) stringCity = "[City], ";
            else stringCity = "";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rrafa\Desktop\WindowsFormsApp\WindowsFormsApp\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM [base_Table]", sqlConnection);
                DataTable dtb = new DataTable();
                sqlDataAdapter.Fill(dtb);
                dGV1.DataSource = dtb;
            }
        }
    }
}
