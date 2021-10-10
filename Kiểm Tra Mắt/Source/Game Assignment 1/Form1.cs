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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region "Vùng biến"

        //Tạo biến randum
        private Random rnd = new Random();
        //Tạo biến ô kích chuột
        private struct TKichO
        {
            public int i;
           public int j;
            
        };
        TKichO KichO = new TKichO();
        //Tạo biến ô màu cá biệt
        private struct Ocabiets
        {
            public int i;
            public int j;

        };
        Ocabiets Ocabiet = new Ocabiets();

        //biến lưu địa chỉ PaintEventArgs
        PaintEventArgs trang;

        //Biến lưu màn chơi
        int Man = 1;
        //Số lần chơi trong 1 màn
        int Manlan = 1;
        //Điểm
        int Diem = 0;
        //Lỗi
        int Loi = 0;
        //Thời gian kết thúc
        int time = 15;
        //màu time
        Color mau=Color.Green;

        #endregion

        #region "Vẽ khi vào game"
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
            trang = e;
           //Thêm vào mức
            pgbMuc.Value = Man;
            //Khởi tạo số cột cơ bản
            OChonMau(trang, Man+1);
           
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            VeHinhTron(mau);

        }

        #endregion

        #region "Tạo ô"

        private void OChonMau(PaintEventArgs e, int SoCot)
        {

                Graphics g = e.Graphics;
                //Cài đặt độ cao và độ rộng các ô con
                int rongO = flpGame.Size.Width / SoCot;
                int caoO = flpGame.Size.Height / SoCot;

                //màu xanh cho ô time
                mau = Color.Green;

                //Randum màu
                 int Rran = rnd.Next(246);
                int Gran = rnd.Next(243);
                int Bran = rnd.Next(246);
                Color randomColor = Color.FromArgb(Rran, Gran, Bran);

                //Màu tất cả
                SolidBrush bruALL = new SolidBrush(randomColor);

                //Màu cá biệt
                SolidBrush bruONE = new SolidBrush(Color.FromArgb(Rran + 10, Gran + 10, Bran + 10));

                // Randum vị trí màu nhạt hơn các ô còn lại
                Ocabiet.i = rnd.Next(SoCot);
                Ocabiet.j = rnd.Next(SoCot);
                for (int i = 0; i < SoCot; i++)
                    for (int j = 0; j < SoCot; j++)
                    {


                        if (i == Ocabiet.i && j == Ocabiet.j) //Vị trí vẽ ô Cá Biệt
                        {
                            g.FillRectangle(bruONE, new Rectangle(i * rongO, j * caoO, rongO - 5, caoO - 5)); //Cách nhau 5 
                        }
                        else    // Vẽ các ô đồng màu                
                        {
                            g.FillRectangle(bruALL, new Rectangle(i * rongO, j * caoO, rongO - 5, caoO - 5));
                        }
                    }

            Ocabiet.i++;
            Ocabiet.j++;
            

        }

        #endregion

        #region "Xử lý chuột"

        ///Hàm thay đổi vị trí ô địa chọn
        private void ODuocChon(int X, int Y, int SoCot)
        {
 
           //Kích thước ô con
            int rongO = (int)flpGame.Size.Width / SoCot;
            int caoO = (int)flpGame.Size.Height / SoCot;
            //Vị trí kích bằng phần nguyên tọa độ cửa con trỏ/kích thước +1 (vì ô tính từ 1:1)
            KichO.i = (int)X / rongO + 1;
            KichO.j = (int)Y / caoO + 1;


        }
        //Sự kiện khi click vào ô ngẫu nhiên
        private void flpGame_MouseClick(object sender, MouseEventArgs e)
        {
           

            //Cập nhật lại vị trí ô được chọn vào Kich.i và Kich.j 
            ODuocChon(e.X, e.Y, Man + 1);
            
            //Xử lý nếu Click đúng ô //
            if (KichO.i==Ocabiet.i && KichO.j == Ocabiet.j)
            {
                //Nếu mới chơi màn 1 cho đồng hồ chạy
                if (Man == 1) {  timerEnd.Start(); }
                else
                {
                    timerEnd.Stop();
                    time = 15; lbTime.Text = "15"; //Cập nhật lại time
                    timerEnd.Start();
                }

                Diem++; // Tăng điểm
                txtDiem.Text = Diem.ToString(); // Cập nhật điểm
                //Nếu là lần cuối cùng của lần chơi trong màn thì thêm màn 
                //và số lần trong màn được đưa về 1
                if (Manlan == Man)
                {
                    Man++;
                    Manlan = 1;
                    if (Man > 7) Man = 7;//không cho vượt qua 8 cột

                }
                else //Tăng sô lần làm trong màn đó
                {
                    Manlan++;
                }
                //Làm mới hàm Paint của layout
                flpGame.Refresh();
            }
            else // Sai ô
            {
                //Nếu mới chơi không tính lỗi
                if (Man != 1) 
                {
                    Loi++; // Tăng lỗi
                    txtLoi.Text = Loi.ToString(); // Cập nhật Lỗi
                    if (time - 3 <= 0) //Không cho trừ time đến số âm
                    {

                        
                        time = 0;
                        lbTime.Text = time.ToString();

                    }
                    else
                    {
                        
                        time -= 3;//Bớt đi 3 giây thời gian chơi
                        lbTime.Text = time.ToString();
                    }
                    if (time <= 5) mau = Color.Red; //Cập nhật màu lập tức


                }
               
            }
            
           
           
          
        }

        #endregion

        #region "Xử lý kết thúc game"

        //Hàm thực hiện khi game kết thức
        private void ShowFormEndGame()
        {
            //Khởi tạo form endgame và truyền thông số qua
            EndGame endgame = new EndGame();
            endgame.Diem = this.Diem;
            endgame.Loi = this.Loi;
            endgame.Man = this.Man;
            endgame.Manlan = this.Manlan;
            

            endgame.ShowDialog();
            
            //Nếu không thoát ứng dụng thì reset lại thông số trò chơi và refresh lại ứng dụng
                this.Diem=0;
                this.Loi=0;
                this.Man=1;
                this.Manlan=1;
                this.time = 15;
                lbTime.Text = "15";
                txtDiem.Text = "0";
                txtLoi.Text = "0";
                mau = Color.Green;
            this.Refresh();
          


        }

        #endregion

        #region "Tác vụ"
        private void timerEnd_Tick(object sender, EventArgs e)
        {
            time--;//giảm time
            
            lbTime.Text = time.ToString();

            if(time<= 5) //Time bé hơn 5 đổi màu ô time
            {
                mau = Color.Red;
            }
            if(time<=0)
            {
                lbTime.Text = "0"; //không để time âm
                //Dừng đồng hồ
                timerEnd.Stop();
                //Gọi form endgame
                ShowFormEndGame();

              
            }
        }
        //Hàm vẽ hình tròn
        private void VeHinhTron(Color mau)
        {
            Graphics g = panel4.CreateGraphics();
            //vẽ hình tròn
            g.Clear(panel4.BackColor);
            g.DrawEllipse(new Pen(mau, 10), 15, 15, panel4.Size.Width - 30, panel4.Size.Height - 30);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn muốn thoát game?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true; //Không thoát là đúng
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
            
        }



        #endregion

      
    }
}
