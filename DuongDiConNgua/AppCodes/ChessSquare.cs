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
            ChessSquareText.Size = new Size(this.SquareSize, this.SquareSize / 2);
            ChessSquareText.TextAlign = ContentAlignment.MiddleCenter;
            int lblX = 0;
            int lblY = (SquareSize) / 4;
            ChessSquareText.Location = new Point(lblX, lblY);
            int lblFontSize = (SquareSize * 15) / 100; // 30%
            ChessSquareText.Font = new Font("Arial", lblFontSize, FontStyle.Bold);
            ChessSquareText.BackColor = Color.Transparent;
            ChessSquareText.ForeColor = Color.Red;
            Controls.Add(ChessSquareText);
            ChessSquareText.Text = "";
            ChessSquareText.MouseClick += (obj, ev) => { ChessSquare_MouseClick(obj, ev); };
            //
            this.MouseClick += ChessSquare_MouseClick;
        }

        public void ChangeImage(Image img)
        {
            this.Image = img;
        }
        private void SetBlockOrUnBlockSquare()
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
            Utils.PathTrace[this.ChessPoint.X, this.ChessPoint.Y] = IsBlocked;
            if (Utils.StartCell == this)
            {
                Utils.StartCell = null;
            }
        }
        private void ChessSquare_MouseClick(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
            {
                SetBlockOrUnBlockSquare();
                return;
            }
            IsBlocked = false;
            ChessSquare startCell = null;
            Image img = null;
            bool pathTrace = false;
            if (Utils.StartCell != null)
            {
                if (Utils.StartCell != this)
                {
                    return;
                }
                else
                {
                    img = this.Img;
                }
            }
            else
            {
                startCell = this;
                img = Resources.HorseStart;
                pathTrace = true;
            }
            Utils.StartCell = startCell;
            this.ChangeImage(img);
            Utils.PathTrace[this.ChessPoint.X, this.ChessPoint.Y] = pathTrace;
        }
    }
}
