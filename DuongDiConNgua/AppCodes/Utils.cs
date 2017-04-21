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
        public static bool IsInBoard(Point point, int chessBoardSize)
        {
            return point.X >= 0 && point.X < chessBoardSize && point.Y >= 0 && point.Y < chessBoardSize;
        }
        public static bool IsPassedThrough(Point point, bool[,] pathTrace)
        {
            return pathTrace[point.X, point.Y];
        }
    }
}
