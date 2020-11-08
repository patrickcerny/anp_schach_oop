using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class Tower : Figure
    {
        public Tower(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {

            FigureType = FigureTypes.tower;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.tower_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.tower_white;
            }


        }
    }
}
