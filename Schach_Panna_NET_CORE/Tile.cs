using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Schach_v1
{
    public delegate void EventTypeTileClicked(Tile clickedTile);

    public class Tile : Panel
    {
        public event EventTypeTileClicked TileClicked;

        //X und Y Coordinaten des Tiles
        public int X, Y;

        //die Daraufliegende Figur
        public Figure CurrentFigure = null;

        private readonly Color _initialColor;
        public Tile(Size tileSize, Color Color, Point Coords)
        {
            //Setzung der Hintergrundfarbe
            BackColor = Color;
            _initialColor = Color;
            //Breite und höhe der Tiles anhand der Übergebenden Parameter
            Width = tileSize.Width;
            Height = tileSize.Height;

            //Coordinaten des Tile's
            X = Coords.X;
            Y = Coords.Y;
        }

        protected override void OnClick(EventArgs e)
        {
            TileClicked?.Invoke(this);
        }


        public void RepaintSelf()
        {
            this.BackColor = _initialColor;

            if(this.CurrentFigure != null)
            {
                this.CurrentFigure.BackColor = _initialColor;
            }
        }

        public void PaintSelfPossible(Color PossibleMoveColor)
        {
            this.BackColor = PossibleMoveColor;

            if (this.CurrentFigure != null)
            {
                this.CurrentFigure.BackColor = PossibleMoveColor;
            }
        }
    }
}
