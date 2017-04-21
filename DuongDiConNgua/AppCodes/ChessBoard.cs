using DuongDiConNgua.AppCodes.Algorithm;
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
            Image first = Resources.Red;
            Image second = Resources.White;
            int s = Width;
            int size = (s - ((ChessBoardSize - 1) * ChessSquare.ChessSquareMargin * 2)) / ChessBoardSize;
            for (int i = 0; i < ChessBoardSize; i++)
            {
                for (int j = 0; j < ChessBoardSize; j++)
                {
                    var chessSquare = new ChessSquare(size, first)
                    {
                        ChessPoint = new Point(i, j)
                    };
                    ChessSquares[i, j] = chessSquare;
                    Controls.Add(chessSquare);
                    SwapImg(ref first, ref second);
                }
                SwapImg(ref first, ref second);
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
            Utils.CreatePathTrace(this.ChessBoardSize);
            Utils.PathTrace[0, 0] = true; // start
            Point p = new Point(0, 0);
            int step = 1;
            while (true)
            {
                // find next pace
                p = Algorithm.GetBestPace(p);
                if (Utils.IsValidPoint(p))
                {
                    this.ChessSquares[p.X, p.Y].ChessSquareText.Text = (step++).ToString();
                    Utils.PathTrace[p.X, p.Y] = true;
                    Application.DoEvents();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
