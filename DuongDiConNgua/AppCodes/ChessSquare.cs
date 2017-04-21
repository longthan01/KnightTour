using DuongDiConNgua.Properties;
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
        public Point ChessPoint { get; set; }
        public int SquareSize { get; set; }
        public Image Img
        {
            get
            {
                return this.Image;
            }
            set
            {
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Image = value;
            }
        }
        public Label ChessSquareText
        {
            get;
            set;
        }
        public const int ChessSquareMargin = 1;

        public ChessSquare(int size, Image img)
        {
            this.SquareSize = size;
            this.Img = img;
            this.Width = size;
            this.Height = size;
            this.Margin = new Padding(ChessSquareMargin, ChessSquareMargin, ChessSquareMargin, ChessSquareMargin);
            this.MouseEnter += ChessSquare_MouseEnter;
            this.MouseLeave += ChessSquare_MouseLeave;
            // Label
            ChessSquareText = new Label();
            ChessSquareText.Size = new Size(this.SquareSize / 2, this.SquareSize / 2);
            ChessSquareText.Location = new Point(0, (this.SquareSize * 3) / 4);
            ChessSquareText.Font = new Font("Arial", 14, FontStyle.Bold);
            ChessSquareText.BackColor = Color.Transparent;
            ChessSquareText.ForeColor = Color.Black;
            Controls.Add(ChessSquareText);
            ChessSquareText.Text = "";
        }
        private void ChessSquare_MouseLeave(object sender, EventArgs e)
        {
            this.Image = Img;
        }
        private void ChessSquare_MouseEnter(object sender, EventArgs e)
        {
            this.Img = Resources.RedHorse;
        }
    }
}
