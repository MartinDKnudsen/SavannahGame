﻿using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SavannahGame
{
    public partial class Form1 : Form
    {
        public int NumberOfStartRabbits { get; set; }
        public int NumberOfStartLions { get; set; }
        public bool animalsOnFields = true; 

        public Controller Ct = new Controller();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private int i = 0;
        private Timer time;

        public Form1()
        {
           
            InitializeComponent();
            
           
                time = new Timer();
                time.Tick += time_Tick;
                time.Interval = 10;
            
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

         dataGridView1.DataSource =  Ct.DT();
         // animalsOnFields = true;
        }
        private void time_Tick(object e, EventArgs ea)
        {
           
            var timetoTick = 1000000000;
          
                if (i < timetoTick)
                {
                    NumberOfLions.Text = Ct.CountLions().ToString();
                    TotalNumberOfRabbitsTextBox.Text = Ct.CountRabbits().ToString();
                    TotalCubsBornTextBox.Text = Ct.CountCubsBorn().ToString();
                    LionsTotalWeigthTextBox.Text = Ct.TotalWeigthOfLions().ToString();
                    RabbitTotalWeigthTextBox.Text = Ct.TotalWeigthOfRabbits().ToString();
                    textBoxForNumberOfKilledRabbits.Text = Ct.KilledRabbits().ToString();
                    grasseatenTextBox.Text = Ct.GrassEat().ToString();
                    textBoxNumberOfRabbitCubs.Text = Ct.rabbitCubs().ToString();
                    textBoxNumberOfLionCubs.Text = Ct.lionCubs().ToString();
                    textBoxNumberOfGreenFields.Text = Ct.countGreenField().ToString();
                    textBoxLionsThatKilledEachOther.Text = Ct.lionsKilled().ToString();
                    textBoxHunterKillCount.Text = Ct.hunterKills().ToString();
                    textBoxTotalAnimals.Text = Ct.TotalAnimals().ToString();
                    i++;
                }
                CheckHunterWin();
        }

        private void CheckHunterWin()
        {
            int OnlyHunterLeft = Convert.ToInt32(textBoxTotalAnimals.Text);
            if (OnlyHunterLeft == 1)
            {
                time.Stop();
                MessageBox.Show(Ct.HunterWon());
                
            }

        }

        private void TopPanelForMovement_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MiniMaiseButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {

            try
            {
                if ((NumberOfStartLions < 0 || NumberOfStartLions > 400) || (NumberOfStartRabbits < 0 || NumberOfStartRabbits > 400))
                {
                    MessageBox.Show("Cant run with input numbers, please try again");
                }
                else
                {
                    time.Start();
                    Task.Factory.StartNew(() => Ct.StartSavannahGame(NumberOfStartRabbits, NumberOfStartLions));
                   
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void textBoxNumberOfRabbits_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumberOfStartRabbits = Convert.ToInt16(textBoxNumberOfRabbits.Text);
                if (NumberOfStartRabbits > 400 || NumberOfStartRabbits < 1)
                {
                    MessageBox.Show("Number have to be larger than 0, but lower than 400 total");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Invalid input");
            }
        }
        private void textBoxNumberOfLions_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumberOfStartLions = Convert.ToInt16(textBoxNumberOfLions.Text);
                if (NumberOfStartLions > 400 || NumberOfStartLions < 1)
                {
                    MessageBox.Show("Number have to be larger than 0, but lower than 400 total");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input");
            }

        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        public DataTable SetupCurrentAsDataTable()
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataRow dRow = dt.NewRow();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            return dt;
        }
        private void PrintDTButton_Click(object sender, EventArgs e)
        {
            Ct.PrintDataTable("ResultsFromSavannaGame", SetupCurrentAsDataTable());
            MessageBox.Show("Done");
        }

        private void buttonSaveToDB_Click(object sender, EventArgs e)
        {
            int CubsBorn = Convert.ToInt32(TotalCubsBornTextBox.Text);
            int HunterKills = Convert.ToInt32(textBoxHunterKillCount.Text);
            int RabbitsDead = Convert.ToInt32(textBoxForNumberOfKilledRabbits.Text);
            Ct.saveData(CubsBorn, RabbitsDead, HunterKills);
            dataGridView1.DataSource = Ct.DT();
        }

     
    }
}
