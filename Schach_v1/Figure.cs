 using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Schach_v1
{
    // MACO: Ist es wirklich sinnvoll, dass die Handler-Methode des Click-Events die
    // Liste von möglichen Moves mitgeliefert bekommt? Das nimmt schon einiges vorweg
    // in Bezug auf wie das Event verwendet wird und das soll aber eigentlich der, der
    // es verwendet (= die Form) in der Handler-Methode entscheiden damit die Freiheit
    // da ist, das Event wofür auch immer zu verwenden. Warum werden also nicht ein-
    // fach in der Handler-Methode dann die möglichen Moves geholt (die Methode dafür 
    // ist eh public)? (**)
    public delegate void EventTypeClickedFigure(object sender, EventArgs e, List<Tile> PossibleMoves);
    
    public abstract class Figure : Panel
    {
        //event, wenn die Figur sich bewegt
        public event EventTypeClickedFigure FigureClicked;

        // MACO: Dieses Feld wird nie verwendet. -> aufräumen! (1)
        Color CurrentPlayer = Color.White;

        // MACO: (1)
        //Farbe die Felder bekommen, wenn auf sie gezogen werden kann
        Color _possibleMoveColor;

        // MACO: (2)
        //das Tile auf dem sich die Figure befindet
        public Tile CurrentTile;

        // MACO: Warum verfügt die Klasse Figure über diese Liste? Vom Sinn her müsste
        // doch das Spiel selbst (also die Form) alle Tiles verwalten und nicht die
        // einzelne Spielfigur. (3)
        // MACO: (**) Dann bräuchtet ihr auch dieses Feld nicht und lauft nicht Gefahr
        // hier ein nicht mehr aktuelles Spielfeld gespeichert zu haben.
        //Liste aller Tiles in der Form
        List<Tile> BoardTiles;

        // MACO: (2)
        //ob die Figure noch im Spiel ist
        public bool IsOnField = true;

        // MACO: (2)
        //Typ der Figur, enum FigureType
        public FigureTypes FigureType;

        //Farbe der Figur / Team der Figur
        private Color _figureColor;
        public Color FigureColor
        {
            get { return _figureColor; }
        }

        // MACO: Warum ist diese Enumeration innerhalb der Klasse definiert? Wenn ihr
        // sie mal in einer anderen Klasse braucht, macht das Umstände.
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

        // MACO: Wofür wird das Feld FigureType und die dazugehörige Enum
        // benötigt? Das Feld wird nur gesetzt, aber nie verwendet. Auch die
        // Enum wird nie verwendet.

        //Construtor
        // MACO: Warum ist dieser Konstruktor public? In Anbetracht dessen, dass die
        // Klasse abstract ist, ergibt das keinen Sinn.
        // MACO: Der Parameter panelSize wird nie verwendet. -> aufräumen!
        public Figure(Size panelSize, Tile startingTile, List<Tile> Tiles)
        {
            // MACO: const geht nicht wegen dem Datentyp Color (komplex). Man kann aber
            // mit static und readonly was konstantenähnliches hinkriegen ;-).
            //Setzung der _possibleMoveColor (kann keine const sein weil weis Gott warum)
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
            // MACO: (*) Und warum schreibt ihr hier nicht einfach hin "startingTile.
            // BackColor"?
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
            // MACO: Den Sinn dieser Methode verstehe ich nicht. Wo soll die unechte
            // Transparenz herkommen und was ist eine unechte Transparenz überhaupt? (*)
            return tile.BackColor;

            // MACO: Angenommen diese Methode hätte einen Sinn. Dann gehört sie vom
            // Sinn her aber doch eher zum Tile, da sie ja nur dessen Hintergrundfarbe
            // zurückliefert. -> in die Tile-Klasse!
        }

        /// <summary>
        /// Überschriebene Click funktion
        /// </summary>
        /// <param name="e"></param> 
        protected override void OnClick(EventArgs e)
        {
            //Invoked das Clicked event
            FigureClicked?.Invoke(this, e, GetPossibleMoves(this, BoardTiles));
        }

        /// <summary>
        /// Gibt eine Array an Tiles zurück, auf die die gewählte Figur springen kann, wird pro Klasse overridden (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual - bis i des gfunden Hob mein gott) MACO: aber nice, dass as gfunda heasch :-D
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        // MACO: Warum muss hier die Figure mitgegeben werden, für die die möglichen
        // Moves berechnet werden? Die Methode muss ja auf eine Figure aufgerufen 
        // werden und damit ist ja dann klar um welche Figure es geht?
        // MACO: Warum muss man hier die BoardTiles mitgeben? Die sind ja eh in einem
        // Feld gespeichert, das alle Unterklassen erben. Falls diese Feld im Zuge der
        // Einarbeitung des Feedbacks (3) weiter oben wegfällt, ist dieser Parameter ok.
        public abstract List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles);
    }
}
