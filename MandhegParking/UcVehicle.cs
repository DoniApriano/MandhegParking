using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MandhegParking
{
    public partial class UcVehicle : UserControl
    {

        SqlConnection conn = new SqlConnection("data source = DESKTOP-BK8I0G1; initial catalog = MandhegParkingSystem; integrated security = true;");

        public UcVehicle()
        {
            InitializeComponent();
        }

        private void empty()
        {
            textBox4.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox2.Text = "";


            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void unEnable()
        {
            textBox4.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

            comboBox2.Enabled = false;

        }

        private void enable()
        {
            textBox4.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

            comboBox2.Enabled = true;
        }

        private void dataGV()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Vehicle.id, Member.name as 'Member_Name', VehicleType.name as 'Vehicle_Type', Vehicle.license_plate as 'License_Plate', Vehicle.notes, Vehicle.created_at  from Vehicle INNER JOIN VehicleType ON Vehicle.vehicle_type_id = VehicleType.id INNER JOIN Member ON Vehicle.member_id = Member.id", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void UcVehicle_Load(object sender, EventArgs e)
        {
            dataGV();

            conn.Open();
            SqlCommand sqlCommand = new SqlCommand("select name from VehicleType", conn);
            SqlDataReader r = sqlCommand.ExecuteReader();
            while (r.Read())
            {
                comboBox2.Items.Add(r[0]);
            }
            conn.Close();

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Vehicle_Type");
            comboBox1.Items.Add("License Plate");
            comboBox1.Items.Add("Member Name");

            unEnable();
            label6.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("pilih salah satu");
            }
            else
            {
                string text = null;
                if (comboBox1.Text == "Vehicle_Type")
                {
                    text = "VehicleType.name";
                }
                else if (comboBox1.Text == "License Plate")
                {
                    text = "Vehicle.license_plate";
                }
                else if (comboBox1.Text == "Member Name")
                {
                    text = "Member.name";
                }

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Vehicle.id, Member.name as 'Member_Name', VehicleType.name as 'Vehicle_Type', Vehicle.license_plate as 'License_Plate', Vehicle.notes, Vehicle.created_at  from Vehicle INNER JOIN VehicleType ON Vehicle.vehicle_type_id = VehicleType.id INNER JOIN Member ON Vehicle.member_id = Member.id WHERE " + text + " like '%" + textBox1.Text + "%' ", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                var dtg = dataGridView1.Rows[e.RowIndex];

                label6.Text = dtg.Cells["id"].Value.ToString();
                textBox2.Text = dtg.Cells["License_Plate"].Value.ToString();
                textBox3.Text = dtg.Cells["Member_Name"].Value.ToString();
                comboBox2.Text = dtg.Cells["Vehicle_Type"].Value.ToString();
                textBox4.Text = dtg.Cells["notes"].Value.ToString();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;

            enable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;

            enable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = true;

            enable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (
                textBox4.Text == "" &&
                textBox2.Text == "" &&
                textBox3.Text == "" &&
                comboBox1.Text == ""
                )
            {
                MessageBox.Show("Isi semua field");
            }
            else
            {
                if (button1.Enabled == false)
                {
                    conn.Open();
                    SqlCommand sqlCommand1 = new SqlCommand("select * from Member where name = '" + textBox3.Text + "'", conn);
                    SqlDataReader reader = sqlCommand1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        conn.Close();
                        conn.Open();
                        DateTime dateTime = DateTime.Now;
                        string formattedDateTime2 = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        SqlCommand cmd = new SqlCommand("INSERT INTO Vehicle(vehicle_type_id,member_id,license_plate,notes,created_at) VALUES ((SELECT id FROM VehicleType WHERE name = '" + comboBox2.Text + "'),(SELECT id FROM Member WHERE name = '" + textBox3.Text + "'),'" + textBox2.Text + "','" + textBox4.Text + "','" + formattedDateTime2 + "')", conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Hai");
                        conn.Close();
                    }
                    else
                    {
                        conn.Close();
                        MessageBox.Show("Nama Tidak ditemuka");
                    }
                    dataGV();
                }
                else if (button2.Enabled == false)
                {
                    conn.Open();
                    SqlCommand sqlCommand1 = new SqlCommand("select * from Member where name = '" + textBox3.Text + "'", conn);
                    SqlDataReader reader = sqlCommand1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        conn.Close();
                        conn.Open();
                        DateTime dateTime = DateTime.Now;
                        string formattedDateTime2 = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        SqlCommand cmd = new SqlCommand("UPDATE Vehicle set vehicle_type_id = (SELECT id FROM VehicleType WHERE name = '" + comboBox2.Text + "'), member_id = (SELECT id FROM Member WHERE name = '" + textBox3.Text + "'), license_plate = '" + textBox2.Text + "', notes = '" + textBox4.Text + "', last_updated_at = '" + formattedDateTime2 + "' where id = '" + label6.Text + "'", conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Hai");
                        conn.Close();
                        dataGV();
                    }
                    else
                    {
                        conn.Close();
                        MessageBox.Show("Nama Tidak ditemuka");
                    }
                }
                else if (button3.Enabled == false)
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("DELETE FROM Vehicle where id = '" + label6.Text + "'", conn);
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Hapus");
                    conn.Close();
                    dataGV();
                }
                else
                {
                    MessageBox.Show("Tolong beri aksi");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            empty();
            unEnable();
        }
    }
}
