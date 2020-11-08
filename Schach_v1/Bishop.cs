using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class Bishop : Figure
    {
        public Bishop(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {

            FigureType = FigureTypes.bishop;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.bishop_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.bishop_white;
            }


        }
    }
}
