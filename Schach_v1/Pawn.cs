using System.Collections.Generic;
using System.Drawing;

namespace Schach_v1
{
    public class Pawn : Figure
    {
<<<<<<< HEAD
        public Pawn( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)
=======
        public Pawn(Size panelSize, Tile startingTile, List<Tile> Tiles) : base(panelSize, startingTile, Tiles)
>>>>>>> 2a02e19f3548dfaf48b0c6802bcbf83765365052
        {
         
            if (FigureColor == Color.Black)
            {
                BackgroundImage = Properties.Resources.pawn_black;
            }
            else 
            {
                BackgroundImage = Properties.Resources.pawn_white;
            }
        }

<<<<<<< HEAD
        public override List<Tile> GetPossibleMoves()
=======
        public override List<Tile> GetPossibleMoves(Figure figure, List<Tile> Tiles)
>>>>>>> 2a02e19f3548dfaf48b0c6802bcbf83765365052
        {
            List<Tile> PossibleMoves = new List<Tile>();
            // MACO: Diese Liste wird nie verwendet. -> aufräumen!
            List<Tile> TilesToRemove = new List<Tile>();

            //ALLE möglichen felder, auch wenn sie belegt sind
            foreach (Tile tile in this.BoardTiles)
            {
                //nur wenn pawn schwarz ist kann er nach unten
                if (this.FigureColor == Color.Black)
                {
                    //ob die Tile genau drüber isch
                    if (tile.X == this.CurrentTile.X)
                    {
                        //geraudeaus fahren 8ein einzelnes feld
                        if (tile.Y == this.CurrentTile.Y + 1 && tile.CurrentFigure == null)
                        {
                            PossibleMoves.Add(tile);
                        }

<<<<<<< HEAD
                        // MACO: Ob der Bauer schon mal gefahren ist oder nicht, könnte
=======
                        // MACO: Ob der Baure schon mal gefahren ist oder nicht, könnte
>>>>>>> 2a02e19f3548dfaf48b0c6802bcbf83765365052
                        // man sich leicht mit einem Feld merken. Dann sind auch andere
                        // Startpositionen als die offizielle möglich und funktionieren.
                        //geradeaus fahren wenn das 1. feld nicht belegt ist und man an der startposition ist
                        if (tile.Y == this.CurrentTile.Y + 2 && tile.CurrentFigure == null && this.CurrentTile.Y == 1)
                        {
<<<<<<< HEAD
                            
=======
                            // MACO: Warum geht ihr so kompliziert alle Tiles durch
                            // und checkt für jedes, ob es das gewünschte ist? Ihr
                            // könntet doch einfach alle betreffenden Tiles mit Find
                            // aus der Liste rausholen, checken und gegebenenfalls
                            // adden?
>>>>>>> 2a02e19f3548dfaf48b0c6802bcbf83765365052
                            //findet das feld unter dem 2. möglichen zugfeld und fragt ab ob dieses null ist == true => kann besprungen werden
                            Tile tileUnderPawn = this.BoardTiles.Find(x => x.X == tile.X && x.Y - 1 == this.CurrentTile.Y);
                            if (tileUnderPawn.CurrentFigure == null)
                            {
                                PossibleMoves.Add(tile);
                            }
                        }
                    }
                    //checkt ob es ein Tile über der figure ist
                    if (tile.Y == this.CurrentTile.Y + 1)
                    {
                        //checkt ob das feld links oder rechts vom pawn ist
                        if (tile.X == this.CurrentTile.X - 1 || tile.X == this.CurrentTile.X + 1)
                        {
                            //checkt ob das feld nicht leer ist
                            if (tile.CurrentFigure != null)
                            {
                                //checkt ob die figur gegnerisch ist
                                if (tile.CurrentFigure.FigureColor != this.FigureColor)
                                {
                                    PossibleMoves.Add(tile);
                                }
                            }
                        }
                    }
                }
                //nur wenn pawn weiss ist kann er nach oben
                else
                {
                    //ob die Tile genau drüber isch
                    if (tile.X == this.CurrentTile.X)
                    {
                        //geraudeaus fahren 8ein einzelnes feld
                        if (tile.Y == this.CurrentTile.Y - 1 && tile.CurrentFigure == null)
                        {
                            PossibleMoves.Add(tile);
                        }

                        //geradeaus fahren wenn das 1. feld nicht belegt ist und man an der startposition ist
                        if (tile.Y == this.CurrentTile.Y - 2 && tile.CurrentFigure == null && this.CurrentTile.Y == 6)
                        {
                            //findet das feld unter dem 2. möglichen zugfeld und fragt ab ob dieses null ist == true => kann besprungen werden
                            Tile tileUnderPawn = this.BoardTiles.Find(x => x.X == tile.X && x.Y + 1 == this.CurrentTile.Y);
                            
                            if (tileUnderPawn.CurrentFigure == null)
                            {
                                PossibleMoves.Add(tile);
                            }
                        }
                    }
                    //checkt ob es ein Tile über der figure ist
                    if (tile.Y == this.CurrentTile.Y - 1)
                    {
                        //checkt ob das feld links oder rechts vom pawn ist
                        if (tile.X == this.CurrentTile.X - 1 || tile.X == this.CurrentTile.X + 1)
                        {
                            //checkt ob das feld nicht leer ist
                            if (tile.CurrentFigure != null)
                            {
                                //checkt ob die figur gegnerisch ist
                                if (tile.CurrentFigure.FigureColor != this.FigureColor)
                                {
                                    PossibleMoves.Add(tile);
                                }
                            }
                        }
                    }
                }
            }

            return PossibleMoves;
        }
    }
}
