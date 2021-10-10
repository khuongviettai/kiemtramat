using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Assignment_1
{
    public partial class EndGame : Form
    {
        public EndGame()
        {
            InitializeComponent();
        }
        //Các biến nhận thông tin từ form1
        public int Loi;
        public int Diem;
        public int Man;
        public int Manlan;

        private void EndGame_Load(object sender, EventArgs e)
        {
            //Load lên màn hình thông tin end game
            txtDiem.Text = Diem.ToString();
            txtLoi.Text = Loi.ToString();

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Đóng cửa sổ endgame
            Dispose();

        }
    }
}
