﻿using DuongDiConNgua.AppCodes.Algorithm;
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
    public class ChessBoard : FlowLayoutPanel
    {
        private int ChessBoardSize { get; set; }
        private int MarginRight = 10;
        public int DefaultChessBoardSize = 700;
        public IPathFindingAlgorithm Algorithm { get; set; }
        public ChessSquare[,] ChessSquares { get; set; }
        public Timer DrawTimer;
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
            ChessSquareSize = (s - ((ChessBoardSize - 1) * ChessSquare.ChessSquareMargin * 2)) / ChessBoardSize;

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
                    SwapImg(ref first, ref second);
                }
                SwapImg(ref first, ref second);
            }
            #endregion
            DrawTimer = new Timer();
            DrawTimer.Interval = 5000;
            DrawTimer.Enabled = true;
            DrawTimer.Tick += DrawTimer_Tick;
            DrawTimer.Start();
        }
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (this.Path.Any())
            {
                ChessSquare p = this.Path.Dequeue();
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
                }
                p.ChessSquareText.Text = (CurrentStep++).ToString();
                if (PreviousSquare != null)
                {
                    if (HorseMove != null)
                    {
                        HorseMove(this, new ImageMovingEventArg() {
                            From = PreviousSquare.PointToScreen(PreviousSquare.Location),
                            To = p.PointToScreen(p.Location),
                            Image = img });
                    }
                }
                PreviousSquare = p;
            }
        }
        private void SwapImg(ref Image a, ref Image b)
        {
            Image t = a;
            a = b;
            b = t;
        }
        public void Knight()
        {
            Utils.PathTrace[0, 0] = true; // start
            Point p = new Point(0, 0);
            Path.Enqueue(ChessSquares[0, 0]);
            while (true)
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
                    break;
                }
            }
        }
        private bool IsDestination(Point dest, int x, int y)
        {
            return dest.X - 4 <= x && dest.X + 4 >= x &&
                   dest.Y - 4 <= y && dest.Y + 4 >= y;
        }
    }
}
