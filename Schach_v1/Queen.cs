using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class Queen : Figure
    {
        public Queen(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {
            
            FigureType = FigureTypes.queen;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.queen_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.queen_white;
            }


        }

    }
}
