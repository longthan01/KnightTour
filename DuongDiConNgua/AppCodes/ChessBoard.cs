using DuongDiConNgua.AppCodes.Algorithm;
using DuongDiConNgua.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuongDiConNgua.AppCodes
{
    public class ChessBoard : FlowLayoutPanel
    {
        public int ChessBoardSize { get; set; }
        public IPathFindingAlgorithm Algorithm { get; set; }
        public ChessSquare[,] ChessSquares { get; set; }
        public int DefaultChessBoardSize = 700;
        public int DrawInterval = 500;

        private int MarginRight = 10;
        private Timer DrawTimer;
        private Timer AlgTimer = new Timer();
        private bool AlgRunning;
        private Queue<ChessSquare> Path = new Queue<ChessSquare>();
        private ChessSquare PreviousSquare = null;
        private int CurrentStep = 1;

        public int ChessSquareSize { get; set; }
        #region event declaration
        public event ImageMovingEventHandler HorseMove;
        #endregion
        public ChessBoard(int chessBoardSize)
        {
            this.Algorithm = new HorseHeuristic(chessBoardSize);
            ChessSquares = new ChessSquare[chessBoardSize, chessBoardSize];
            this.ChessBoardSize = chessBoardSize;
            this.Size = new Size(DefaultChessBoardSize, DefaultChessBoardSize);
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Margin = new Padding(0, 0, 0, 0);
            this.AutoSize = false;
            this.Location = new Point(this.Width - this.Size.Width - MarginRight, 0);
            #region draw chess board
            Image first = Resources.Red;
            Image second = Resources.White;
            int s = Width;
            ChessSquareSize = (s - ((ChessBoardSize) * ChessSquare.ChessSquareMargin * 2)) / ChessBoardSize;

            for (int i = 0; i < ChessBoardSize; i++)
            {
                for (int j = 0; j < ChessBoardSize; j++)
                {
                    var chessSquare = new ChessSquare(ChessSquareSize, first)
                    {
                        ChessPoint = new Point(i, j)
                    };
                    ChessSquares[i, j] = chessSquare;
                    Controls.Add(chessSquare);
                    Application.DoEvents();
                    SwapImg(ref first, ref second);
                }
                if (chessBoardSize % 2 == 0)
                {
                    SwapImg(ref first, ref second);
                }
            }
            #endregion
            DrawTimer = new Timer();
            DrawTimer.Interval = DrawInterval;
            DrawTimer.Tick += DrawTimer_Tick;
        }

        #region events
        private List<Point> t = new List<Point>();
        private bool contains(Point p)
        {
            var p1 = t.Any(x => x.X == p.X && x.Y == p.Y);
            return p1;
        }
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (this.Path.Any())
            {
                ChessSquare p = this.Path.Dequeue();
                if (contains(p.ChessPoint))
                {
                    Debug.WriteLine($"{p.ChessPoint.X},{p.ChessPoint.Y} repeated");
                }
                t.Add(p.ChessPoint);

                bool isValid = Utils.IsPassedThrough(p.ChessPoint);
                Image img = null;
                if (PreviousSquare != null)
                {
                    if (p.ChessPoint.Y > PreviousSquare.ChessPoint.Y)
                    {
                        img = Resources.HorseRunningRight;
                    }
                    else
                    {
                        img = Resources.HorseRunningLeft;
                    }
                    PreviousSquare.ChangeImage(PreviousSquare.Img);
                    //if (HorseMove != null)
                    //{
                    //    HorseMove(this, new ImageMovingEventArg()
                    //    {
                    //        From = PreviousSquare.PointToScreen(PreviousSquare.Location),
                    //        To = p.PointToScreen(p.Location),
                    //        Image = img
                    //    });
                    //}
                }
                else
                {
                    if (Utils.StartCell.ChessPoint.Y > Utils.StartCell.ChessPoint.Y)
                    {
                        img = Resources.HorseRunningRight;
                    }
                    else
                    {
                        img = Resources.HorseRunningLeft;
                    }
                }
                p.ChangeImage(img);
                System.Threading.Thread.Sleep(10);
                p.ChessSquareText.Text = (CurrentStep++).ToString();
                PreviousSquare = p;
            }
            else
            {
                Application.DoEvents();
                DrawTimer.Enabled = false;
                if (!AlgRunning)
                {
                    MessageBox.Show("Kết cmn thúc!", "Done");
                }
                DrawTimer.Stop();
            }
        }
        #endregion

        private void SwapImg(ref Image a, ref Image b)
        {
            Image t = a;
            a = b;
            b = t;
        }
        public void Knight()
        {
            DrawTimer.Start();
            AlgRunning = true;
            AlgTimer.Interval = this.DrawInterval;
            Point p = new Point(Utils.StartCell.ChessPoint.X, Utils.StartCell.ChessPoint.Y);
            AlgTimer.Tick += (obj, ev) =>
                {
                    // find next pace
                    p = Algorithm.GetBestPace(p);
                    if (Utils.IsValidPoint(p))
                    {
                        Utils.PathTrace[p.X, p.Y] = true;
                        var square = this.ChessSquares[p.X, p.Y];
                        this.Path.Enqueue(square);
                    }
                    else
                    {
                        AlgRunning = false;
                        AlgTimer.Enabled = false;
                        AlgTimer.Stop();
                    }
                };
            AlgTimer.Start();
        }
        public void SetInterval(int interval)
        {
            Application.DoEvents();
            this.AlgTimer.Interval = interval;
            this.DrawTimer.Interval = interval;
        }
    }
}
