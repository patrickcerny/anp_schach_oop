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
        //Colors des SChachbretts
        Color[] _tileColors =  new Color[] {Color.FromArgb(163, 163, 163), Color.FromArgb(102, 59, 7) };


        //größe des Boards
        const int _BOARDSIZE = 800;
        
        //größe der einzelnen Teile
        Size _tileSize;

        List<Tile> Tiles = new List<Tile> { };

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
            ClientSize = new Size(_BOARDSIZE, _BOARDSIZE);
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
                        color = _tileColors[1];
                    }
                    else if (id % 2 == 1 && i % 2 == 1){
                        color = _tileColors[1];
                    }
                    else
                    {
                        color = _tileColors[0];
                    }

                   //Erstellung des Tiles und Position anhand der ID
                    Tile ChessTile = new Tile(_tileSize, color, id, new int[] { j, i });
                    ChessTile.Left = ChessTile.Width * j;
                    ChessTile.Top = ChessTile.Height * i;

                    //manuelle generation der Queen / King white

                    if (i == 0)
                    {
                        if (j == 3)
                        {
                            Controls.Add(new Queen(ClientSize, ChessTile, Tiles));
                        }
                        if (j == 4)
                        {
                            Controls.Add(new King(ClientSize, ChessTile, Tiles));
                        }
                    }

                    //manuelle generation der Queen / King Black
                    if (i == 7)
                    {
                        if (j == 3)
                        {
                            Controls.Add(new Queen(ClientSize, ChessTile, Tiles));
                        }
                        if (j == 4)
                        {
                            Controls.Add(new King(ClientSize, ChessTile, Tiles));
                        }
                    }


                    //testfigur
                    if (i == 2 && j == 5)
                    {
                        Controls.Add(new Pawn(ClientSize, ChessTile, Tiles));
                    }

                    if (i == 5 && j == 2)
                    {
                        Controls.Add(new Bishop(ClientSize, ChessTile, Tiles));
                    }

                    //generation other figures
                    if (i == 0 || i == 7)
                    {
                        if (j == 0 || j == 7)
                        {
                            Controls.Add(new Tower(ClientSize, ChessTile, Tiles));
                        }

                        if (j == 1 || j == 6)
                        {
                            Controls.Add(new Horse(ClientSize, ChessTile, Tiles));
                            
                        }

                        if (j == 2 || j == 5)
                        {
                            Controls.Add(new Bishop(ClientSize, ChessTile, Tiles));
                        }
                    }

                    //generation pawns
                    if (i == 1 || i == 6)
                    {
                        Controls.Add(new Pawn(ClientSize, ChessTile, Tiles));
                    }

                    //hinzufpgen zur liste der Tiles
                    Tiles.Add(ChessTile);

                    //hinzufügen der Figur und des Tile's

                    Controls.Add(ChessTile);

                    //id counter
                    id++;

                    
                    
                }

            }

            //Hinzufügen des moves event für jede Figure auf dem Spielfeld
            foreach (Control item in Controls)
            {
                if (item is Figure figure)
                {
                    figure.Moves += Figure_Moves;
                }
            }

        }

        private void Figure_Moves()
        {
            RePaintBoard();
        }

        //färbt das Board neu
        public void RePaintBoard()
        {
           //geht jedes einzelne Tile durch
            foreach (Tile tile in Tiles)
            {
                //Setzt die Farben auf den Standard zurück
                if (tile.ID % 2 == 0 && tile.Coordinates["Y"] % 2 == 0)
                {
                    
                    tile.BackColor = _tileColors[1];
                }
                else if (tile.ID % 2 == 1 && tile.Coordinates["Y"] % 2 == 1)
                {
                    
                    tile.BackColor = _tileColors[1];
                }
                else
                {
                    
                    tile.BackColor = _tileColors[0];
                }

                //checkt ob das Tile eine Currenfigurehat
                if (tile.CurrenFigure != null)
                {
                    //Färbt 
                    tile.CurrenFigure.BackColor = tile.BackColor;
                }
            }
        }
    }
}
