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
        //größe des Boards
        int _boardSize = 800;
        
        //größe der einzelnen Teile
        Size _tileSize;

        

        public Form1()
        {
            InitializeComponent();
            InitWindow();
            
            
        }

        /// <summary>
        /// Einstellung / Erstellung aller Tiles und Figuren
        /// </summary>
        void InitWindow()
        {
            //Id Counter für die Tiles
            int id = 0;


            //festlegung der Größe des Spielfeldes
            ClientSize = new Size(_boardSize, _boardSize);
            _tileSize = new Size(ClientSize.Width / 8, ClientSize.Height / 8);

            //erstellung aller Tiles mit den ID's 0-63
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //"Berechnung" der Hintergrundfarbe
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

                   //Erstellung des Tiles und Position anhand der ID
                    Tile ChessTile = new Tile(_tileSize, color, id);
                    ChessTile.Left = ChessTile.Width * j;
                    ChessTile.Top = ChessTile.Height * i;
                    ChessTile.ID = id;
                    
                    //hinzufügen der Figur und des Tile's
                    //i woas ned warum aber wenn man die 2 lines switched denn klappts ned lol
                    Controls.Add(new Figure(ClientSize, ChessTile));
                    Controls.Add(ChessTile);

                    //id counter
                    id++;

                    
                    
                }

            }


        }
    }
}
