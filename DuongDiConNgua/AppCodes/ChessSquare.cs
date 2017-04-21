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
            get; set;
        }
        public Label ChessSquareText
        {
            get;
            set;
        }
        public const int ChessSquareMargin = 1;
        public bool IsBlocked { get; set; }
        public ChessSquare(int size, Image img)
        {
            this.SquareSize = size;
            this.Img = img;
            this.Image = this.Img;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Width = size;
            this.Height = size;
            this.Margin = new Padding(ChessSquareMargin, ChessSquareMargin, ChessSquareMargin, ChessSquareMargin);
            // Label
            ChessSquareText = new Label();
            ChessSquareText.Size = new Size(this.SquareSize / 2, this.SquareSize / 2);
            ChessSquareText.Location = new Point(0, (this.SquareSize * 3) / 4);
            ChessSquareText.Font = new Font("Arial", 14, FontStyle.Bold);
            ChessSquareText.BackColor = Color.Transparent;
            ChessSquareText.ForeColor = Color.Black;
            Controls.Add(ChessSquareText);
            ChessSquareText.Text = "";
            this.MouseClick += ChessSquare_MouseClick;
        }

        public void ChangeImage(Image img)
        {
            this.Image = img;
        }
        private void ChessSquare_MouseClick(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
            {
                if (!IsBlocked)
                {
                    this.Image = Resources.Blocked;
                    IsBlocked = true;
                }
                else
                {
                    this.Image = Img;
                    IsBlocked = false;
                }
            }
            Utils.PathTrace[this.ChessPoint.X, this.ChessPoint.Y] = IsBlocked;
        }
    }
}
