using DuongDiConNgua.AppCodes;
using DuongDiConNgua.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuongDiConNgua
{
    public partial class Form1 : Form
    {
        private ChessBoard _chessBoard;
        private int _chessBoardSize;
        private PictureBox HorseRunningAnimation = null;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            #region draw chessboard
            _chessBoardSize = 8;
            Utils.CreatePathTrace(_chessBoardSize);
            _chessBoard = new ChessBoard(_chessBoardSize);
            int marginRight = 50;
            _chessBoard.Location = new Point(this.Width - _chessBoard.DefaultChessBoardSize - marginRight, 0);
            this.Controls.Add(_chessBoard);
            #endregion

            HorseRunningAnimation = new PictureBox();
            HorseRunningAnimation.Size = new Size(_chessBoard.ChessSquareSize, _chessBoard.ChessSquareSize);
            HorseRunningAnimation.BackColor = Color.Transparent;
            HorseRunningAnimation.SizeMode = PictureBoxSizeMode.StretchImage;
            HorseRunningAnimation.BringToFront();
            this.Controls.Add(HorseRunningAnimation);
            _chessBoard.HorseMove += (obj, ev) =>
            {
                SmoothMove(ev.From, ev.To, ev.Image);
            };
            this.btnReset.Click += (obj, ev) =>
            {
                Utils.CreatePathTrace(_chessBoardSize);
                for (int i = 0; i < _chessBoardSize; i++)
                {
                    for (int j = 0; j < _chessBoardSize; j++)
                    {
                        _chessBoard.ChessSquares[i, j].ChessSquareText.ResetText();
                    }
                }
            };
            this.btnRun.Click += (obj, ev) =>
            {
                _chessBoard.Knight();
            };
        }
        private void SmoothMove(Point from, Point to, Image img)
        {
            int a = -(to.Y - from.Y);
            int b = to.X - from.X;
            HorseRunningAnimation.Visible = true;
            HorseRunningAnimation.BringToFront();
            HorseRunningAnimation.Image = img;
            HorseRunningAnimation.Location = from;
            // a (x - x0) + b (y - y0) = 0
            // => ax - x0a + by - y0b = 0 => ax + by = x0a + y0b => x = ((x0a + y0b) - by) / a
            // => x = (x0a + y0b) / a - b/a y
            // x0 = to.X
            // y0 = to.Y

            Point p = new Point(from.X, from.Y);
            int x = 0;
            int y = 0;
            int f = from.X < to.X ? from.X : to.X;
            int t = from.X > to.X ? from.X : to.X;
            for (int i = f; i < t; i++)
            {
                if (from.X < to.X)
                {
                    x++;
                }
                else
                {
                    x--;
                }
                try
                {
                    float t1 = ((to.X * a) + (to.Y * b)) / (float)a;
                    float t2 = t1 - x;
                    y = (int)Math.Round((t2 / ((float)b / a)));
                }
                catch (Exception ex)
                {
                }

                p.X = p.X + x;
                p.Y = p.Y + y;
                HorseRunningAnimation.Location = p;
                System.Diagnostics.Debug.WriteLine($"Horse at : x: {p.X} y: {p.Y}");
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }
            HorseRunningAnimation.Visible = false;
        }
    }
}
