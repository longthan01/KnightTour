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
        public bool[,] PathTrace { get; set; }
        public HorseHeuristic(int chessBoardSize, bool[,] pathTrace)
        {
            this.ChessBoardSize = chessBoardSize;
            PathTrace = pathTrace;
        }
        public Point GetBestPace(Point current)
        {
            Point result = new Point(-1, -1);
            int min = 9;
            int moveCount = 0;

            for (int i = 0; i < ChessBoardSize; i++)
            {
                Point nextPos = new Point();
                nextPos.X = PossibleStepX[i] + current.X;
                nextPos.Y = PossibleStepY[i] + current.Y;
                if(Utils.IsInBoard(nextPos, ChessBoardSize) && !Utils.IsPassedThrough(nextPos, PathTrace))
                {
                    int posNextMove = GetHeuristic(nextPos);
                    if (posNextMove < min)
                    {
                        min = posNextMove;
                        result = nextPos;
                        moveCount++;
                    }
                }
            }
            return result;
        }
        public int GetHeuristic(Point p)
        {
            int count = 0;
            for (int i = 0; i < this.ChessBoardSize; i++)
            {
                Point nextPos = new Point();
                nextPos.X = PossibleStepX[i] + p.X;
                nextPos.Y = PossibleStepY[i] + p.Y;
                if (Utils.IsInBoard(nextPos, ChessBoardSize) && !Utils.IsPassedThrough(nextPos, PathTrace))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
