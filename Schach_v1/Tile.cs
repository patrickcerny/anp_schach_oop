using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Schach_v1
{
    public class Tile : Panel
    {
        //ID des Tiles (0-63)
        public int ID;

        //X und Y Coordinaten des Tiles
        public Dictionary<string, int> Coordinates = new Dictionary<string, int>();

        //die Daraufliegende Figur
        public Figure CurrenFigure = null;

        public Tile(Size tileSize, Color Color, int id, int[] Coords)
        {
            //Setzung der Hintergrundfarbe
            BackColor = Color;

            //Setzung der ID
            ID = id;

            //Breite und höhe der Tiles anhand der Übergebenden Parameter
            Width = tileSize.Width;
            Height = tileSize.Height;

            //Coordinaten des Tile's
            Coordinates.Add("X", Coords[0]);
            Coordinates.Add("Y", Coords[1]);
            
        }


    }
}
