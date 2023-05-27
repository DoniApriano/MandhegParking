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

namespace MandhegParking
{
    public partial class UcPayment : UserControl
    {

        SqlConnection conn = new SqlConnection("data source = DESKTOP-BK8I0G1; initial catalog = MandhegParkingSystem; integrated security = true;");

        public UcPayment(string value)
        {
            InitializeComponent();
            this.value = value;
        }

        private string value { get; set; }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void UcPayment_Load(object sender, EventArgs e)
        {
            label15.Visible = false;
            label15.Text = value;

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into(membership_id,vehicle_type_id,value,created_at) values((select id from Membership where name = '"+comboBox2.Text+"'),(select id from VehicleType where name = '"+comboBox1.Text+"')) ",conn);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Membership INNER JOIN Member ON Member.membership_id = Membership.id WHERE Member.name = '" + textBox6.Text + "' ", conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                comboBox2.Text = rdr["name"].ToString();
                conn.Close();
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.Text == "VIP")
            {
                label16.Text = "4000";
                Convert.ToInt32(label16.Text);
            }
        }
    }
}
