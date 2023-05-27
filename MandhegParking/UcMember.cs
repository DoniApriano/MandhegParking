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
    public partial class UcMember : UserControl
    {

        SqlConnection conn = new SqlConnection("data source = DESKTOP-BK8I0G1; initial catalog = MandhegParkingSystem; integrated security = true;");

        public UcMember()
        {
            InitializeComponent();
        }

        private void empty()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            comboBox1.Text = "";

            richTextBox1.Text = "";

            radioButton1.Checked = false;
            radioButton2.Checked = false;

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void unEnable()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

            comboBox1.Enabled = false;

            richTextBox1.Enabled = false;

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
        }

        private void enable()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

            comboBox1.Enabled = true;

            richTextBox1.Enabled = true;

            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
        }

        private void dataGV()
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Member",conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void UcMember_Load(object sender, EventArgs e)
        {
            empty();
            dataGV();
            label9.Visible = false;

            if (
                button1.Enabled == true ||
                button2.Enabled == true ||
                button3.Enabled == true 
                )
            {
                unEnable();
            }

            conn.Open();
            SqlCommand sqlCommand = new SqlCommand("select name from Membership",conn);
            SqlDataReader r = sqlCommand.ExecuteReader();
            while (r.Read())
            {
                comboBox1.Items.Add(r[0]);
            }
            conn.Close() ;
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
                textBox1.Text == "" &&
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
                    string gender = null;
                    if (radioButton1.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    conn.Open();
                    string formattedDateTime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    DateTime dateTime = DateTime.Now;
                    string formattedDateTime2 = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    SqlCommand cmd = new SqlCommand("INSERT INTO Member(membership_id,name,email,phone_number,address,date_of_birth,gender,created_at) VALUES ((SELECT id FROM Membership WHERE name = '" + comboBox1.Text + "'),'" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + richTextBox1.Text + "','" + formattedDateTime + "','" + gender + "','" + formattedDateTime2 + "')", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hai");
                    conn.Close();
                    dataGV();
                }
                else if (button2.Enabled == false)
                {
                    string gender = null;
                    if (radioButton1.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    conn.Open();
                    string formattedDateTime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    DateTime dateTime = DateTime.Now;
                    string formattedDateTime2 = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    SqlCommand cmd = new SqlCommand("UPDATE Member set membership_id = (SELECT id FROM Membership WHERE name = '" + comboBox1.Text + "'), name = '" + textBox1.Text + "', email = '" + textBox2.Text + "', phone_number = '" + textBox3.Text + "', address = '" + richTextBox1.Text + "', date_of_birth = '" + formattedDateTime + "', gender = '" + gender + "', last_updated_at = '" + formattedDateTime2 + "' where id = '" + label9.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hai");
                    conn.Close();
                    dataGV();
                }
                else if (button3.Enabled == false)
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("DELETE FROM Member where id = '" + label9.Text + "'", conn);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;

                var hehe = dataGridView1.Rows[e.RowIndex];
                label9.Text = hehe.Cells["id"].FormattedValue.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();

                comboBox1.Text = hehe.Cells["membership_id"].Value.ToString();

                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["email"].FormattedValue.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["phone_number"].FormattedValue.ToString();
                richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["address"].FormattedValue.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells["date_of_birth"].FormattedValue.ToString();

                if (dataGridView1.Rows[e.RowIndex].Cells["gender"].FormattedValue.ToString() == "Male")
                {
                    radioButton1.Checked = true;
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells["gender"].FormattedValue.ToString() == "Female")
                {
                    radioButton2.Checked = true;
                }
            }
        }
    }
}
