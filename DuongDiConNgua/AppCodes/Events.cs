using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongDiConNgua.AppCodes
{
    public class ImageMovingEventArg : EventArgs
    {
        public Point From { get; set; }
        public Point To { get; set; }
        public Image Image { get; set; }
    }
    public delegate void ImageMovingEventHandler(object sender, ImageMovingEventArg e);
}
