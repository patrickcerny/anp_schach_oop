 using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Schach_v1
{
    public delegate void EventTypeClickedFigure(Figure clickedFigure);

    public abstract class Figure : Panel
    {
        //event, wenn die Figur sich bewegt
        public event EventTypeClickedFigure FigureClicked;

        // MACO: Dieses Feld wird nie verwendet. -> aufräumen! (1)
        Color CurrentPlayer = Color.White;

        // MACO: (1)
        //Farbe die Felder bekommen, wenn auf sie gezogen werden kann
         readonly Color _possibleMoveColor;

        // MACO: (2)
        //das Tile auf dem sich die Figure befindet
        public Tile CurrentTile;


        public List<Tile> BoardTiles;

        // MACO: (2)
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
            
            //Setzung der readonly _possibleMoveColor
             _possibleMoveColor = Color.ForestGreen;

            //Speicherung aller Tiles der Form 
            BoardTiles = Tiles;

            //Setzung des CurrentFigure des zugewiesenen Tiles
            startingTile.CurrentFigure = this;

            // MACO: Hier wäre nett, wenn das auch für größere / kleinere Spielfelder
            // gehen würde und wenn man die Figuren individuell einfärben könnte. 
            // Überlegung - Farbe als Übergabeparameter im Konstruktor machen, damit
            // sie von außen bestimmt werden kann. Wenn ihr nur Schwarz und Weiß als
            // Farben zulassen wollt, könnt ihr das ja trotzdem z.B. mit Hilfe einer
            // Enumeration beschränken.
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
        /// Gibt eine Array an Tiles zurück, auf die die gewählte Figur springen kann, wird pro Klasse overridden (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual - bis i des gfunden Hob mein gott) MACO: aber nice, dass as gfunda heasch :-D
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        public abstract List<Tile> GetPossibleMoves();

    }
}
