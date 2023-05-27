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
using System.Security.Cryptography;

namespace MandhegParking
{
    public partial class Form1 : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-BK8I0G1;Initial Catalog=MandhegParkingSystem;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "pellentesque.massa.lobortis@fringillacursus.net";
            textBox2.Text = "admin";
        }

        private static string sha256(string password)
        {
            var chrypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] chrypto = chrypt.ComputeHash(Encoding.ASCII.GetBytes(password));
            foreach (byte theByte in chrypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" &&  textBox2.Text == "")
            {
                MessageBox.Show("Isi semua field");
            }
            else
            {
                string password = sha256(textBox2.Text);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Employee WHERE email = '" + textBox1.Text + "' and password = '" + password + "'", conn);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                SqlCommand cmd = new SqlCommand("select * from Employee where email = '" + textBox1.Text + "'",conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (dataTable.Rows.Count > 0)
                {
                    reader.Read();
                    string name = reader["name"].ToString();
                    FormUtama formUtama = new FormUtama(name);
                    UcPayment ucPayment = new UcPayment(name);
                    this.Hide();
                    formUtama.Show();
                }
                else
                {
                    MessageBox.Show("Hallo");
                }
                
                /*label5.Text = textBox1.Text;
                label4.Text = password;*/
                conn.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
