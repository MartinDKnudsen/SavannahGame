using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
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
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error");
               
            }

        }

        private void textBoxNumberOfRabbits_TextChanged(object sender, EventArgs e)
        {
            NumberOfStartRabbits = Convert.ToInt16(textBoxNumberOfRabbits.Text);
        }

        private void textBoxNumberOfLions_TextChanged(object sender, EventArgs e)
        {
            NumberOfStartLions = Convert.ToInt16(textBoxNumberOfLions.Text);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            NumberOfLions.Text = "Frank";

        }

    }
}
