using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongDiConNgua.AppCodes
{
    public class Utils
    {
        public static ChessSquare StartCell { get; set; }
        public static int ChessBoardSize { get; set; }
        public static bool[,] PathTrace { get; set; }
        public static bool IsPassedThrough(Point point)
        {
            return PathTrace[point.X, point.Y];
        }
        public static void Initialize(int chessBoardSize)
        {
            StartCell = null;
            ChessBoardSize = chessBoardSize;
            PathTrace = new bool[ChessBoardSize, ChessBoardSize];
            for (int i = 0; i < ChessBoardSize; i++)
            {
                for (int j = 0; j < ChessBoardSize; j++)
                {
                    PathTrace[i, j] = false;
                }
            }
        }
        public void ResetPathTrace()
        {
            if (PathTrace != null)
            {
                for (int i = 0; i < PathTrace.Length; i++)
                {
                    for (int j = 0; j < PathTrace.Length; j++)
                    {
                        PathTrace[i, j] = false;
                    }
                }
            }
        }
        public static bool IsValidPoint(Point p)
        {
            bool res = p.X != -1 && p.Y != -1;
            res = res && Utils.PathTrace[p.X, p.Y] == false;
            return res;
        }
        public static int GCD(int a, int b)
        {
            if (b == 0) { return a; }
            else
            {
                return GCD(b, a % b);
            }
        }
        #region backtracking utils
        public static Dictionary<Point, List<Point>> IgnoredPoints { get; set; } = new Dictionary<Point, List<Point>>();
        #endregion
        private static int[] PossibleStepX = new int[8] { -1, 1, 2, 2, 1, -1, -2, -2 };
        private static int[] PossibleStepY = new int[8] { -2, -2, -1, 1, 2, 2, 1, -1 };
        public static List<Point> GetAdjencies(Point p, int chessBoardSize)
        {
            List<Point> list = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                Point nextPos = new Point();
                nextPos.X = PossibleStepX[i] + p.X;
                nextPos.Y = PossibleStepY[i] + p.Y;
                if (IsInBoard(nextPos, chessBoardSize))
                {
                    list.Add(nextPos);
                }
            }
            return list;
        }
        private static bool IsInBoard(Point point, int chessBoardSize)
        {
            return point.X >= 0 && point.X < chessBoardSize && point.Y >= 0 && point.Y < chessBoardSize;
        }
        static Utils()
        {

        }
    }
}
