using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongDiConNgua.AppCodes.Algorithm
{
    public class HorseHeuristic : IPathFindingAlgorithm
    {
        private int[] PossibleStepX = new int[8] { -1, 1, 2, 2, 1, -1, -2, -2 };
        private int[] PossibleStepY = new int[8] { -2, -2, -1, 1, 2, 2, 1, -1 };
        private int ChessBoardSize { get; set; }
        public HorseHeuristic(int chessBoardSize)
        {
            this.ChessBoardSize = chessBoardSize;
        }
        public Point GetBestPace(Point current)
        {
            Point result = new Point(-1, -1);
            int min = 9;

            for (int i = 0; i < 8; i++)
            {
                Point nextPos = new Point();
                nextPos.X = PossibleStepX[i] + current.X;
                nextPos.Y = PossibleStepY[i] + current.Y;
                if(IsInBoard(nextPos) && !Utils.IsPassedThrough(nextPos))
                {
                    int posNextMove = GetHeuristic(nextPos);
                    if (posNextMove < min)
                    {
                        min = posNextMove;
                        result = nextPos;
                    }
                }
            }
            return result;
        }
        public int GetHeuristic(Point p)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                Point nextPos = new Point();
                nextPos.X = PossibleStepX[i] + p.X;
                nextPos.Y = PossibleStepY[i] + p.Y;
                if (IsInBoard(nextPos) && !Utils.IsPassedThrough(nextPos))
                {
                    count++;
                }
            }
            return count;
        }
        public bool IsInBoard(Point point)
        {
            return point.X >= 0 && point.X < ChessBoardSize && point.Y >= 0 && point.Y < ChessBoardSize;
        }
    }
}
