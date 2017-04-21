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
        private int ChessBoardSize { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.ChessBoardSize = 8;
        }
        private void CreateLayout()
        {
            chessBoardPanel = new FlowLayoutPanel();
            chessBoardPanel.Size = new Size(700,700);
            chessBoardPanel.AutoSize = false;
            chessBoardPanel.Location = new Point(this.Width - chessBoardPanel.Size.Width - MarginRight, 0);
            this.Controls.Add(chessBoardPanel);
            Image first = Properties.Resources.Red;
            Image second = Properties.Resources.White;
            int s = this.chessBoardPanel.Width;
            int size = (s - (3 * ChessBoardSize)) / ChessBoardSize;
            for (int i = 0; i < ChessBoardSize; i++)
            {
                for (int j = 0; j < ChessBoardSize; j++)
                {
                    this.chessBoardPanel.Controls.Add(new ChessSquare(size, first));
                    SwapImg(ref first, ref second);
                }
                SwapImg(ref first, ref second);
                Application.DoEvents();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CreateLayout();
        }
        void SwapImg(ref Image a, ref Image b)
        {
            Image t = a;
            a = b;
            b = t;
        }
        private FlowLayoutPanel chessBoardPanel;
        private int MarginRight = 10;
    }
}
