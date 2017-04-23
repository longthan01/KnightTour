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
            DrawChessBoard();
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
                DrawChessBoard();
            };
            this.btnRun.Click += (obj, ev) =>
            {
                if (Utils.StartCell == null)
                {
                    MessageBox.Show("Chọn 1 điểm bắt đầu !");
                    return;
                }
                _chessBoard.Knight();
            };
        }
        private void DrawChessBoard()
        {
            _chessBoardSize = 8;
            Utils.Initialize(_chessBoardSize);
            this.Controls.Remove(_chessBoard);
            _chessBoard = new ChessBoard(_chessBoardSize);
            int marginRight = 50;
            _chessBoard.Location = new Point(this.Width - _chessBoard.DefaultChessBoardSize - marginRight, 0);
            this.Controls.Add(_chessBoard);
        }
        private void SmoothMove(Point from, Point to, Image img)
        {
            int a = -(to.Y - from.Y);
            int b = to.X - from.X;
            int gcd = (a == 0 || b == 0) ? 1 : Utils.GCD(a, b);
            a /= gcd;
            b /= gcd;
            HorseRunningAnimation.Visible = true;
            HorseRunningAnimation.BringToFront();
            HorseRunningAnimation.Image = img;
            HorseRunningAnimation.Location = from;

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
                    int toGcd = Utils.GCD(to.X, to.Y);
                    int tox = 1;
                    int toy = (int)Math.Round((float)to.X / to.Y);
                    float t1 = (tox * a) / (float)b;
                    float t2 = toy - ((x * a) / (float)b);
                    float t3 = t1 + t2;
                    y = (int)Math.Round(t3);
                }
                catch (Exception ex)
                {
                }

                p.X = p.X + x;
                p.Y = p.Y + y;
                if (IsDestination(to, p.X, p.Y))
                {
                    break;
                }
                HorseRunningAnimation.Location = p;
                System.Diagnostics.Debug.WriteLine($"Horse at : x: {p.X} y: {p.Y}");
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }
            HorseRunningAnimation.Visible = false;
        }
        private bool IsDestination(Point dest, int x, int y)
        {
            return dest.X - 4 <= x && dest.X + 4 >= x &&
                   dest.Y - 4 <= y && dest.Y + 4 >= y;
        }
        //private void SmoothMove(Point from, Point to, Image img)
        //{
        //    Graphics g = this.CreateGraphics();
        //    Pen pen = new Pen(Brushes.LightGreen);
        //    pen.Width = 2;
        //    g.DrawLine(pen, from, to);
        //}
    }
}
