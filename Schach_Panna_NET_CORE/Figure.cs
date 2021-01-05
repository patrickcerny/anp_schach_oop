 using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Schach_v1
{
    public delegate void EventTypeClickedFigure(Figure clickedFigure);

    public abstract class Figure : Panel
    {
        //event, wenn die Figur sich bewegt
        public event EventTypeClickedFigure FigureClicked;
        
        //das Tile auf dem sich die Figure befindet
        public Tile CurrentTile;

        public List<Tile> BoardTiles;
       
        //ob die Figure noch im Spiel ist
        public bool IsOnField = true;

        //Farbe der Figur / Team der Figur
        private Color _figureColor;
        public Color FigureColor
        {
            get { return _figureColor; }
        }



        // MACO: Warum ist dieser Konstruktor public? In Anbetracht dessen, dass die
        // Klasse abstract ist, ergibt das keinen Sinn.
        // MACO: Der Parameter panelSize wird nie verwendet. -> aufräumen!

        // Antwort: base funktioniert ohne "public" nicht!
        public Figure(Tile startingTile, List<Tile> Tiles)
        {
           
            //Speicherung aller Tiles der Form 
            BoardTiles = Tiles;

            //Setzung des CurrentFigure des zugewiesenen Tiles
            startingTile.CurrentFigure = this;

            //farbe wird Festegelegt
            if (startingTile.Y >= 6)
            {
                _figureColor = Color.White;
            }
            else 
            {
                _figureColor = Color.Black;
            }

            //Tile auf dem es sich befindet
            CurrentTile = startingTile;

            //größe der Figure anhand der TIlesize
            Width = startingTile.Width / 2;
            Height = startingTile.Height / 2;

            //dynamische änderung der Hintergrundfarbe
            BackColor = startingTile.BackColor;


            //anpassung des Hntergundbildes
            BackgroundImageLayout = ImageLayout.Stretch;

            //position anhand der Position des Tiles
            Left = startingTile.Left + startingTile.Width / 2 - Width / 2;
            Top = startingTile.Top + startingTile.Height / 2 - Height / 2;
            
            //z-index: 999999999; á la CSS
            BringToFront();
        }



        /// <summary>
        /// Überschriebene Click funktion
        /// </summary>
        /// <param name="e"></param> 
        protected override void OnClick(EventArgs e)
        {
            
            //Invoked das Clicked event

            FigureClicked?.Invoke(this);

        }

        /// <summary>
        /// Gibt eine Liste an Tiles zurück, auf die die gewählte Figur springen kann, wird pro Klasse overridden (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual - bis i des gfunden Hob mein gott) MACO: aber nice, dass as gfunda heasch :-D
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        public abstract List<Tile> GetPossibleMoves();


        public List<Tile> GetDiagonalMoves()
        {
            List<Tile> PossibleMoves = new List<Tile>();

            List<Tile> MovesUpperRight = new List<Tile>();
            List<Tile> MovesLowerRight = new List<Tile>();
            List<Tile> MovesLowerLeft = new List<Tile>();
            List<Tile> MovesUpperLeft = new List<Tile>();

            //sorry für dean code, i hobs ned effizienter herkrigt :( es isch 2:28 und i HEUL weil der Code so hässlich isch, but u gotta do what u gotta do

            //checkt die  anzahl der möglichen züge nach oben
            foreach (Tile tile in this.BoardTiles)
            {
                if (this.CurrentTile.X + this.CurrentTile.Y == tile.X + tile.Y)
                {            
                    //nur die Tiles oben rechts und unten links
                    if (tile.Y < this.CurrentTile.Y && tile.X > this.CurrentTile.X)
                    {             
                        MovesUpperRight.Add(tile);
                    }
                    //unten links
                    else if (tile.Y > this.CurrentTile.Y && tile.X < this.CurrentTile.X)
                    {
                        MovesLowerLeft.Add(tile);
                    }
                }

                
                if (this.CurrentTile.Y - this.CurrentTile.X  == tile.Y - tile.X)
                {
                    if (tile.Y < this.CurrentTile.Y && tile.X < this.CurrentTile.X)
                    {
                        MovesUpperLeft.Add(tile);
                    }
                    else if (tile.Y > this.CurrentTile.Y && tile.X > this.CurrentTile.X)
                    {
                        MovesLowerRight.Add(tile);
                    }
                }
            }



            //OBEN RECHTS SORTIERUNG UND ENTFERNUNG
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesUpperRight.OrderBy(x => x.X).ToList<Tile>()));

            //UNTEN RECHTS SORTIERUNG UND ENTFERNUNG
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesLowerRight.OrderBy(x => x.X).ToList<Tile>()));

            //UNTEN LINKS SORTIERUNG UND ENTFERNUNG
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesLowerLeft.OrderByDescending(x => x.X).ToList<Tile>()));


            //OBEN LENKS SORTIERUNG UND ENTFERNUNG
            
            PossibleMoves.AddRange(this.SortOutMoves(MovesUpperLeft.OrderByDescending(x => x.X).ToList<Tile>()));


           
            return PossibleMoves;
        }

        public List<Tile> GetStraightMoves()
        {

            List<Tile> PossibleMoves = new List<Tile>();
            List<Tile> MovesInDirection = new List<Tile>();

            for (int i = 0; i < 4; i++)
            {
                //Cleared die Liste von dem Vorherigem Durchlauf
                MovesInDirection.Clear();
                switch (i)
                {
                    //nach oben
                    case (0):
                        //geht jedes Tile durch
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //ob gleiche X position und ob tile Y kleiner als Figure Y
                            if (tile.X == this.CurrentTile.X && tile.Y < this.CurrentTile.Y)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        PossibleMoves.AddRange(this.SortOutMoves(MovesInDirection.OrderByDescending(x => x.Y).ToList<Tile>()));



                        break;

                    //nach rechts
                    case (1):
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //Ob auf der gleichen Y aber größere X
                            if (tile.Y == this.CurrentTile.Y && tile.X > this.CurrentTile.X)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        PossibleMoves.AddRange(this.SortOutMoves(MovesInDirection.OrderBy(x => x.X).ToList<Tile>()));

                        //geht jedes Tile in der richtigen Reihenfolge durch

                        break;

                    //nach unten
                    case (2):
                        //geht jedes Tile durch
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //ob gleiche X position und ob tile Y größer als Figure Y
                            if (tile.X == this.CurrentTile.X && tile.Y > this.CurrentTile.Y)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }

                        //Sortiert die Tiles AUSTEIGEND
                        PossibleMoves.AddRange(this.SortOutMoves(MovesInDirection.OrderBy(x => x.Y).ToList<Tile>()));
                        

                        
                        break;

                    //nach links
                    case (3):
                        foreach (Tile tile in this.BoardTiles)
                        {
                            //Ob auf der gleichen Y aber kleinere X
                            if (tile.Y == this.CurrentTile.Y && tile.X < this.CurrentTile.X)
                            {
                                MovesInDirection.Add(tile);
                            }
                        }
                        //Sortiert die Tiles AUSTEIGEND
                        PossibleMoves.AddRange(this.SortOutMoves(MovesInDirection.OrderByDescending(x => x.X).ToList<Tile>()));
                        ;

               
                        break;
                }
            }

            return PossibleMoves;
        }

        public List<Tile> SortOutMoves(List<Tile> PossibleMoves) {

            List<Tile> ListToReturn = new List<Tile>();


           
            
            //geht jedes Tile in der richtigen Reihenfolge durch
            foreach (Tile tile in PossibleMoves)
            {
                //wenn das Feld leer ist wird es hinzugefügt
                if (tile.CurrentFigure == null)
                {
                    ListToReturn.Add(tile);
                }
                //falls nicht wird gecheckt welche FigureColor die Figur hat
                else
                {
                    //wenn sie die gleiche Farbe hat wird abgebrochen
                    if (tile.CurrentFigure.FigureColor == this.FigureColor)
                    {
                        return ListToReturn;

                    }
                    //ansonsten Hinzugefügt und abgebrochen
                    else
                    {
                        ListToReturn.Add(tile);
                        return ListToReturn;
                    }
                }
            }



            return ListToReturn;
        }
    }
}
