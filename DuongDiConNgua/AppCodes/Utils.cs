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
        public static bool IsValidPoint(Point p)
        {
            return p.X != -1 && p.Y != -1;
        }
        public static int GCD(int a, int b)
        {
            if (b == 0) { return a; }
            else
            {
                return GCD(b, a % b);
            }
        }
    }
}
