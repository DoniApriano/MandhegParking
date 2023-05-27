using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MandhegParking
{
    public partial class FormUtama : Form
    {
        public FormUtama(string value)
        {
            InitializeComponent();
            this.value = value;

            UcHome ucHome = new UcHome();
            addUserControl(ucHome);
        }

        public string value { get; set; }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel2.Controls.Clear();
            panel2.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void FormUtama_Load(object sender, EventArgs e)
        { 
            timer1.Start();
            label1.Text = "Welcome \n" + value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UcHome ucHome = new UcHome();
            addUserControl(ucHome);

            button1.BackColor = SystemColors.Control;
            button2.BackColor = Color.Red;
            button3.BackColor = Color.Red;
            button4.BackColor = Color.Red;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UcMember ucMember = new UcMember();
            addUserControl(ucMember);

            button1.BackColor = Color.Red;
            button2.BackColor = SystemColors.Control;
            button3.BackColor = Color.Red;
            button4.BackColor = Color.Red;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UcVehicle ucVehicle = new UcVehicle();  
            addUserControl(ucVehicle);

            button1.BackColor = Color.Red;
            button2.BackColor = Color.Red;
            button3.BackColor = SystemColors.Control;
            button4.BackColor = Color.Red;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UcPayment ucPayment = new UcPayment(value);
            addUserControl(ucPayment);

            button1.BackColor = Color.Red;
            button2.BackColor = Color.Red;
            button3.BackColor = Color.Red;
            button4.BackColor = SystemColors.Control;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
