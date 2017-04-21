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
        private int _chessBoardSize;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            _chessBoardSize = 8;
            Utils.CreatePathTrace(_chessBoardSize);
            this.btnRun.Click += (obj, ev) => 
            {
                _chessBoard.Knight();
            };
            this.btnReset.Click += (obj, ev) =>
            {
                Utils.CreatePathTrace(_chessBoardSize);
                for (int i = 0; i < _chessBoardSize; i++)
                {
                    for (int j = 0; j < _chessBoardSize; j++)
                    {
                        _chessBoard.ChessSquares[i, j].ChessSquareText.ResetText();
                    }
                }
            };
        }
       
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
             _chessBoard = new ChessBoard(_chessBoardSize);
            int marginRight = 50;
            _chessBoard.Location = new Point(this.Width - _chessBoard.DefaultChessBoardSize - marginRight, 0);
            this.Controls.Add(_chessBoard);
        }
    }
}
