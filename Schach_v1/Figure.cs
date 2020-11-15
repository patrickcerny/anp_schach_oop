using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Schach_v1
{
    public delegate void EventTypeClickedFigure(object sender, EventArgs e);
    public abstract class Figure : Panel
    {
        //event, wenn die Figur sich bewegt
        public event EventTypeClickedFigure FigureClicked;

        //das Tile auf dem sich die Figure befindet
        public Tile CurrentTile;

        //ob die Figure noch im Spiel ist
        public bool IsOnField = true;

        //Typ der Figur, enum FigureType
        public FigureTypes FigureType;

        //Farbe der Figur / Team der Figur
        private Color _figureColor;

        //alle Tiles auf dem Board (braucht nur der König für den Check)
        public static List<Tile> _boardTiles;

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
        public Figure(Size panelSize, Tile startingTile, List<Tile> Tiles = null)
        {
            //Setzung des CurrentFigure des zugewiesenen Tiles
            startingTile.CurrenFigure = this;

            //boardTiles eigenschaft
            _boardTiles = Tiles;

            //farbe wird Festegelegt
            if (startingTile.Coordinates["Y"] >= 0 && startingTile.Coordinates["Y"] <= 1)
            {
                _figureColor = Color.Black;
            }
            else
            {
                _figureColor = Color.White;
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
        public Color ChangeBackColor(Tile tile)
        {
            return tile.BackColor;
        }

        /// <summary>
        /// Überschriebene Click funktion
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            //Invoked das Clicked event mit dem object sender
            FigureClicked?.Invoke(this, e);
        }



        /// <summary>
        /// Gibt eine Array an Tiles zurück, auf die die gewählte Figur springen kann, wird pro Klasse overridden (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual - bis i des gfunden Hob mein gott)
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        public abstract List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles);
      
    }
}
