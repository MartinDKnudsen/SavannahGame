using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SavannahGame
{
    public partial class Form1 : Form
    {
        public int NumberOfStartRabbits { get; set; }
        public int NumberOfStartLions { get; set; }

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
            time.Interval = 100;
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void time_Tick(object e, EventArgs ea)
        {
            if (i < 100000)
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
                i++;

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
                Task.Factory.StartNew(() => Ct.StartSavannahGame(NumberOfStartRabbits, NumberOfStartLions));
                time.Start();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error");

            }

        }

        private void textBoxNumberOfRabbits_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumberOfStartRabbits = Convert.ToInt16(textBoxNumberOfRabbits.Text);
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

    }
}
