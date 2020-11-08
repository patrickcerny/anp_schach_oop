using System.Drawing;
using System.Windows.Forms;

namespace Schach_v1
{
    public delegate void EventTypeMoveFigure(Panel panel);
    internal class Figure : Panel
    {

        //event, wenn die Figur sich bewegt
        public event EventTypeMoveFigure Moves;

        //position der Figure / ID des Tiles auf dem es sich befindet
        public int Position;
        
        //ob die Figure noch im Spiel ist
        public bool IsOnField = true;

        //Typ der Figur, enum FigureType
        public FigureTypes FigureType;


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
        public Figure(Size panelSize, Tile startingTile)
        {
           //testing
            FigureType = FigureTypes.pawn;
            
            BackgroundImage = Properties.Resources.queen_white;
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
            if (tile.BackColor == Color.Black)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
    }
}
