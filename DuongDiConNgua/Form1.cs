using DuongDiConNgua.AppCodes;
using DuongDiConNgua.AppCodes.Algorithm;
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
        private int _drawDelayTime;
        private PictureBox HorseRunningAnimation = null;
        private Label lblGO;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            #region draw chessboard
            // DrawChessBoard();
            #endregion
            #region smooth moving animation
            //HorseRunningAnimation = new PictureBox();
            //HorseRunningAnimation.Size = new Size(_chessBoard.ChessSquareSize, _chessBoard.ChessSquareSize);
            //HorseRunningAnimation.BackColor = Color.Transparent;
            //HorseRunningAnimation.SizeMode = PictureBoxSizeMode.StretchImage;
            //HorseRunningAnimation.BringToFront();
            //this.Controls.Add(HorseRunningAnimation);
            #endregion
            this.comboBox1.SelectedIndex = 4; // 500
            this.cbxAlgorithm.SelectedIndex = 0;
            this.txtChessSquare.Text = 8.ToString();
            this._chessBoardSize = 8;
            this._drawDelayTime = 500;
            DrawChessBoard();
            this.btnReset.Click += (obj, ev) =>
            {
                _chessBoard.AlgTimer.Stop();
                if (_chessBoardSize != 0)
                {
                    DrawChessBoard();
                }
                Utils.Initialize(_chessBoardSize);
                Utils.IgnoredPoints.Clear();
            };
            this.comboBox1.SelectedIndexChanged += (obj, ev) =>
            {
                int s = 0;
                int.TryParse(comboBox1.SelectedItem.ToString(), out s);
                this._drawDelayTime = s;
                if (_chessBoard != null)
                {
                    _chessBoard.SetInterval(this._drawDelayTime);
                }
            };
            this.txtChessSquare.TextChanged += (obj, ev) =>
            {
                int s = 0;
                int.TryParse(txtChessSquare.Text, out s);
                this._chessBoardSize = s;
                if (s != 0 && s <= 20)
                {
                    DrawChessBoard();
                }
                else
                {
                    MessageBox.Show("Range: 0 - 20");
                }
            };
            this.btnRun.Click += (obj, ev) =>
            {
                if (Utils.StartCell == null)
                {
                    MessageBox.Show("Chọn 1 điểm bắt đầu !");
                    return;
                }
                switch (this.cbxAlgorithm.SelectedItem.ToString())
                {
                    case "NNA":
                        _chessBoard.Algorithm = new HorseHeuristic(_chessBoardSize);
                        _chessBoard.Knight();
                        break;
                    case "Backtracking":
                        _chessBoard.Algorithm = new HorseBacktracking(_chessBoardSize);
                        _chessBoard.KnightBacktracking();
                        break;
                }
            };
            this.button1.Click += (obj, ev) =>
            {
                string help = System.IO.File.ReadAllText("help.txt");
                MessageBox.Show(help, "Intruction");
            };
            this.btnPause.Click += (obj, ev) =>
            {
                if (_chessBoard != null && _chessBoard.AlgTimer != null)
                {
                    _chessBoard.AlgTimer.Enabled = !_chessBoard.AlgTimer.Enabled;
                }
            };
        }
        private void DrawGameOver(string text)
        {
            if (lblGO == null)
            {
                lblGO = new Label();
            }
            lblGO.Font = new Font("Showcard Gothic", 24, FontStyle.Bold);
            lblGO.Location = new Point(0, this.Height / 3);
            lblGO.ForeColor = Color.Green;
            lblGO.Size = new Size(300, this.Height / 4);
            lblGO.Text = text;
            this.Controls.Add(lblGO);
            lblGO.BringToFront();
            Application.DoEvents();
        }
        private void DrawChessBoard()
        {
            Utils.Initialize(_chessBoardSize);
            this.Controls.Remove(_chessBoard);
            _chessBoard = new ChessBoard(_chessBoardSize);
            _chessBoard.SetInterval(_drawDelayTime);
            DrawGameOver("");
            _chessBoard.DrawingCompleted += (obj, ev) =>
            {
                DrawGameOver("Gêm Ô VƠ");
            };
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
    }
}
