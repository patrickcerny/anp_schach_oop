using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schach_v1
{
    public partial class Form1 : Form
    {
        Size _tileSize;
        Size _figureSize;
        public Form1()
        {
            InitializeComponent();
            InitWindow();
            
            
        }


        void InitWindow()
        {

            int id = 0;
            ClientSize = new Size(1000, 1000);
            _tileSize = new Size(ClientSize.Width / 8, ClientSize.Height / 8);

            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color color;
                    if (id % 2 == 0 && i % 2 == 0)
                    {
                        color = Color.Black;
                    }
                    else if (id % 2 == 1 && i % 2 == 1){
                        color = Color.Black;
                    }
                    else
                    {
                        color = Color.White;
                    }

                   
                    Tile ChessTile = new Tile(_tileSize, color, id);
                    ChessTile.Left = ChessTile.Width * j;
                    ChessTile.Top = ChessTile.Height * i;
                    ChessTile.ID = id;
                    Controls.Add(ChessTile);



                    id++;

                    // TEST
                    
                }
                




                
            }

            
            
        }
    }
}
