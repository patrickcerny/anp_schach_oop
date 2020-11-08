using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach_v1
{
    class King : Figure
    {
        public King(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
        {
            
            FigureType = FigureTypes.king;

            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.king_black;
            }
            else
            {
                BackgroundImage = Properties.Resources.king_white;
            }

        }


        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
        {

            List<Tile> PossibleMoves = new List<Tile>(); ;
            List<Tile> TilesToRemove = new List<Tile>();


            //ALLE möglichen felder, auch wenn sie belegt sind
            foreach (Tile tile in Tiles)
            {

                if (tile.ID == figure.Position -8 )
                {
                    PossibleMoves.Add(tile);

                    
                }
                if (tile.ID == figure.Position + 8)
                {
                    PossibleMoves.Add(tile);
                }
                if (tile.ID == figure.Position - 1)
                {
                    PossibleMoves.Add(tile);
                }
                if (tile.ID == figure.Position  +1)
                {
                    PossibleMoves.Add(tile);
                }
            }


            //welche felder entfernt werden müssen da sie nicht zugänglich sind
            foreach (Tile item in PossibleMoves)
            {
                
                if (item.CurrenFigure != null)
                {
                    
                    if (item.CurrenFigure.FigureColor == figure.FigureColor)
                    {
                        
                        TilesToRemove.Add(item);
                    }
                }
            }

            //entfernung der nicht möglichen moves
            foreach (Tile tileToRemove in TilesToRemove)
            {
                PossibleMoves.Remove(tileToRemove);
            }

            return PossibleMoves;
        }


    }
}
