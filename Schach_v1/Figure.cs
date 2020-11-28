 using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Schach_v1
{
    public delegate void EventTypeClickedFigure(object sender, EventArgs e, List<Tile> PossibleMoves);
    public abstract class Figure : Panel
    {

        //event, wenn die Figur sich bewegt
        public event EventTypeClickedFigure FigureClicked;

        

        Color CurrentPlayer = Color.White;

        //Farbe die Felder bekommen, wenn auf sie gezogen werden kann
        Color _possibleMoveColor;

        //das Tile auf dem sich die Figure befindet
        public Tile CurrentTile;

        

        //Liste aller Tiles in der Form
        List<Tile> BoardTiles;
        
        //ob die Figure noch im Spiel ist
        public bool IsOnField = true;

        //Typ der Figur, enum FigureType
        public FigureTypes FigureType;


        //Farbe der Figur / Team der Figur
        private Color _figureColor;

        public Color FigureColor
        {
            get { return _figureColor; }
        }


        //enum der Typen der Figur
        public enum FigureTypes
        {
            pawn,
            bishop,
            tower,
            horse,
            queen,
            king
        }

        //Construtor
        public Figure(Size panelSize, Tile startingTile, List<Tile> Tiles)
        {
            //Setzung der _possibleMoveColor (kann keine const sein weil weis Gott warum)
            _possibleMoveColor = Color.ForestGreen;

            //Speicherung aller Tiles der Form 
            BoardTiles = Tiles;

            //Setzung des CurrentFigure des zugewiesenen Tiles
            startingTile.CurrentFigure = this;

            //farbe wird Festegelegt
            if (startingTile.Coordinates["Y"] >= 6)
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
            BackColor = ChangeBackColor(startingTile);

            //anpassung des Hntergundbildes
            BackgroundImageLayout = ImageLayout.Stretch;

            //position anhand der Position des Tiles
            Left = startingTile.Left + startingTile.Width / 2 - Width / 2;
            Top = startingTile.Top + startingTile.Height / 2 - Height / 2;

            
            //z-index: 999999999; á la CSS
            BringToFront();
            
        }

        /// <summary>
        /// Ändert Hnertgrundfarbe des Panels und erstellte somit eine unechte Tranzparenz
        /// </summary>
        /// <param name="tile">Tile und dessen Farbe die man zurückgibt.</param>
        /// <returns></returns>
        Color ChangeBackColor(Tile tile)
        {
            return tile.BackColor;
        }

        /// <summary>
        /// Überschriebene Click funktion
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            //Invoked das Clicked event
            FigureClicked?.Invoke(this, e,  GetPossibleMoves(this, BoardTiles));

        }
        /// <summary>
        /// Gibt eine Array an Tiles zurück, auf die die gewählte Figur springen kann, wird pro Klasse overridden (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual - bis i des gfunden Hob mein gott)
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        public abstract List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles);
      
    }
}
