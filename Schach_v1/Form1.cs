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

        //farbe die das Feld annimmt wenn es besprungen werden kann
        public Color PossibleMoveColor = Color.ForestGreen;

        //der Spielende Spieler
        Color CurrentPlayer = Color.White;

        //größe des Boards
        const int _BOARDSIZE = 800;
        
        //größe der einzelnen Teile
        Size _tileSize;

        //letzte Figur die angeklickt worden ist
        Figure _lastFigureClicked = null;

        //liste aller tiles
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
                        color = _tileColors[0];
                    }
                    else if (id % 2 == 1 && i % 2 == 1){
                        color = _tileColors[0];
                    }
                    else
                    {
                        color = _tileColors[01];
                    }

                   //Erstellung des Tiles und Position anhand der ID
                    Tile ChessTile = new Tile(_tileSize, color, new int[] { j, i });
                    ChessTile.TileClicked += ChessTile_TileClicked;
                    ChessTile.Left = ChessTile.Width * j;
                    ChessTile.Top = ChessTile.Height * i;

                    //manuelle generation der Queen / King white

                    if (i == 0)
                    {
                        if (j == 3)
                        {
                            Controls.Add(new Queen(ClientSize, ChessTile));
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
                            Controls.Add(new Queen(ClientSize, ChessTile));
                        }
                        if (j == 4)
                        {
                            Controls.Add(new King(ClientSize, ChessTile, Tiles));
                        }
                    }

                    //generation other figures
                    if (i == 0 || i == 7)
                    {
                        if (j == 0 || j == 7)
                        {
                            Controls.Add(new Tower(ClientSize, ChessTile));
                        }

                        if (j == 1 || j == 6)
                        {
                            Controls.Add(new Horse(ClientSize, ChessTile));
                            
                        }

                        if (j == 2 || j == 5)
                        {
                            Controls.Add(new Bishop(ClientSize, ChessTile));
                        }
                    }

                    //generation pawns
                    if (i == 1 || i == 6)
                    {
                        Controls.Add(new Pawn(ClientSize, ChessTile));
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
                    figure.FigureClicked += Figure_FigureClicked;

                }
            }

        }

        private void ChessTile_TileClicked(object sender, EventArgs e)
        {
            
            Tile clickedTile = sender as Tile;

            //checkt ob überhaupt schonmal ne figur geclickt wurde
            if (_lastFigureClicked != null)
            {
                //wenn ja ob sie die gleiche farbe hat wie der spieler der dran ist
                if (CurrentPlayer == _lastFigureClicked.FigureColor)
                {
                    //checkt ob die Hintergrundfarbe die Farbe von einem Möglichen zug hat
                    if (clickedTile.BackColor == PossibleMoveColor)
                    {
                        //wenn die figur leer ist ==> einfach die figur moven
                        if (clickedTile.CurrenFigure == null)
                        {
                            MoveFigure(clickedTile);
                        }
                        //wenn eine figur drauf ist und sie die andere Farbe hat wie der Spieler dran ist ==> schlag
                        else if (clickedTile.CurrenFigure.FigureColor != _lastFigureClicked.FigureColor)
                        {
                            //entfernt die Figur von den Controls
                            Controls.Remove(clickedTile.CurrenFigure);
                            //die Figur des Tiles wird auf 0 gesetzt
                            clickedTile.CurrenFigure = null;
                            //der zug wird vollbracht
                            MoveFigure(clickedTile);
                        }
                        //spieler wird getausch
                        ChangePlayer();
                    }
                    
                }
            } 
        }

        private void Figure_FigureClicked(object sender, EventArgs e)
        {
            //das board einfach neu painten
           

            Figure clickedFigure = sender as Figure;


            //wenn die figur nicht die farbe des zurzeit angreifenden spielers hat (möglichkeit auf absicht auf das schlagen der Figur)
            if (clickedFigure.FigureColor != CurrentPlayer)
            {
                if (clickedFigure.CurrentTile.BackColor == PossibleMoveColor)
                {
                    Controls.Remove(clickedFigure);
                    clickedFigure.CurrentTile.CurrenFigure = null;
                    MoveFigure(clickedFigure.CurrentTile);
                    ChangePlayer();
                }
            }
            
            else
            {
                //ansonsten ist es halt die zuletzt gedrückte Figur
                _lastFigureClicked = clickedFigure;

                RePaintBoard();
                //Tiles, welche gefärbt werden müssen
                List<Tile> _tilesToColor = clickedFigure.GetPossibleMoves(clickedFigure, Tiles);

                //färbt jedes Tile 
                foreach (Tile item in _tilesToColor)
                {
                    //jedes mögliche Feld wird in die mögliche zug farbe gefärbt
                    item.BackColor = PossibleMoveColor;

                    //checkt ob auf dem Feld überhaupt eine Figure ist
                    if (item.CurrenFigure != null)
                    {
                        //färbt jedes Figure dessen Hintergrundfarbe geändert wurde
                        item.CurrenFigure.BackColor = item.CurrenFigure.ChangeBackColor(item);
                    }

                }
            }
        }

        /// <summary>
        /// färbt das board neu ein
        /// </summary>
        public void RePaintBoard()
        {
           //geht jedes einzelne Tile durch
            foreach (Tile tile in Tiles)
            {
                //Setzt die Farben auf den Standard zurück
                if (tile.Coordinates["X"] % 2 == 0 && tile.Coordinates["Y"] % 2 == 0)
                {
                    
                    tile.BackColor = _tileColors[0];
                }
                else if (tile.Coordinates["X"] % 2 == 1 && tile.Coordinates["Y"] % 2 == 1)
                {
                    
                    tile.BackColor = _tileColors[0];
                }
                else
                {
                    
                    tile.BackColor = _tileColors[1];
                }

                //checkt ob das Tile eine Currenfigurehat
                if (tile.CurrenFigure != null)
                {
                    //Färbt 
                    tile.CurrenFigure.BackColor = tile.BackColor;
                }
            }
        }

        /// <summary>
        /// Bewegt die figur auf das Tile
        /// </summary>
        /// <param name="clickedTile">Tile, auf welches die Figur bewegt werden soll</param>
        void MoveFigure(Tile clickedTile)
        {          

            //setzt auf das geclickte Tile die alte figure
            clickedTile.CurrenFigure = _lastFigureClicked;

            //cleart das alte Tile wo die Figure war
            _lastFigureClicked.CurrentTile.CurrenFigure = null;


            clickedTile.CurrenFigure.CurrentTile = clickedTile;

            //position anhand der Position des Tiles
            clickedTile.CurrenFigure.Left = clickedTile.Left + _tileSize.Width / 2 - clickedTile.CurrenFigure.Width / 2;
            clickedTile.CurrenFigure.Top = clickedTile.Top + _tileSize.Height / 2 - clickedTile.CurrenFigure.Height / 2;

            //z-index mol wieder
            clickedTile.CurrenFigure.BringToFront();

            //neu färben
            RePaintBoard();
        }

        /// <summary>
        /// Wechselt den zurzeit spielenden spieler
        /// </summary>
        void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }
    }
}
