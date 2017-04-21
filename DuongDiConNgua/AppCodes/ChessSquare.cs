using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuongDiConNgua.AppCodes
{
    public class ChessSquare : PictureBox
    {
        public int SquareSize { get; set; }
        public Image Img { get; set; }
        public ChessSquare(int size, Image img)
        {
            this.SquareSize = size;
            this.Img = img;
            this.Width = size;
            this.Height = size;
            this.Image = img;
            this.BackColor = Color.Red;
            this.Margin = new Padding(1,1,1,1);
            this.MouseEnter += ChessSquare_MouseEnter;
            this.MouseLeave += ChessSquare_MouseLeave;
        }

        private void ChessSquare_MouseLeave(object sender, EventArgs e)
        {
            this.Image = Img;
        }

        private void ChessSquare_MouseEnter(object sender, EventArgs e)
        {
            this.Image = Properties.Resources.RedHorse;
        }

        public void SetImage(Image img)
        {
            this.Image = img;
        }
    }
}
