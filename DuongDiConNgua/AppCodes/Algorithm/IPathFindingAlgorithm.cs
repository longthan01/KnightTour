using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongDiConNgua.AppCodes.Algorithm
{
    public interface IPathFindingAlgorithm
    {
        Point GetBestPace(Point current);
    }
}
