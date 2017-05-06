using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongDiConNgua.AppCodes.Algorithm
{
    public class HorseBacktracking : IPathFindingAlgorithm
    {
        private int[] PossibleStepX = new int[8] { -1, 1, 2, 2, 1, -1, -2, -2 };
        private int[] PossibleStepY = new int[8] { -2, -2, -1, 1, 2, 2, 1, -1 };
        private int ChessBoardSize { get; set; }

        public HorseBacktracking(int chessBoardSize)
        {
            this.ChessBoardSize = chessBoardSize;
        }
        public Point GetBestPace(Point current)
        {
            Point result = new Point(current.X, current.Y);
            for (int i = 0; i < 8; i++)
            {
                Point nextPos = new Point();
                nextPos.X = PossibleStepX[i] + current.X;
                nextPos.Y = PossibleStepY[i] + current.Y;
                
                if (IsValid(current, nextPos))
                {
                    if (IsInBoard(nextPos) && !Utils.IsPassedThrough(nextPos))
                    {
                        result.X = nextPos.X;
                        result.Y = nextPos.Y;
                        break;
                    }
                }
            }
            return result;  
        }
        public bool IsInBoard(Point point)
        {
            return point.X >= 0 && point.X < ChessBoardSize && point.Y >= 0 && point.Y < ChessBoardSize;
        }
        private bool IsValid(Point current, Point nextPace)
        {
            bool res = true;
            try
            {
                var ignoreObj = Utils.IgnoredPoints.FirstOrDefault(x => x.Key.X == current.X && x.Key.Y == current.Y);
                res = !ignoreObj.Value.Any(x => x.X == nextPace.X && x.Y == nextPace.Y);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
    }
}
