using DuongDiConNgua.AppCodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuongDiConNgua
{
    public partial class Form1 : Form
    {
        private ChessBoard _chessBoard;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.btnRun.Click += (obj, ev) => 
            {
                _chessBoard.Knight();
            };
        }
       
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
             _chessBoard = new ChessBoard(8);
            int marginRight = 50;
            _chessBoard.Location = new Point(this.Width - _chessBoard.DefaultChessBoardSize - marginRight, 0);
            this.Controls.Add(_chessBoard);
        }
    }
}
