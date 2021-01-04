using System.Collections.Generic;
using System.Drawing;

namespace Schach_v1
{
    public class Pawn : Figure
    {

        public Pawn( Tile startingTile, List<Tile> Tiles) : base( startingTile, Tiles)

        {
         
            if (FigureColor == Color.Black)
            {
                BackgroundImage = Image.FromFile("C:\\Users\\patri\\Desktop\\Schach_Test\\Schach_Test\\img\\king_black.png");
            }
            else 
            {
                BackgroundImage = Image.FromFile("C:\\Users\\patri\\Desktop\\Schach_Test\\Schach_Test\\img\\king_black.png");
            }
        }


        public override List<Tile> GetPossibleMoves()
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


                        // MACO: Ob der Bauer schon mal gefahren ist oder nicht, könnte
                        // man sich leicht mit einem Feld merken. Dann sind auch andere
                        // Startpositionen als die offizielle möglich und funktionieren.
                        //geradeaus fahren wenn das 1. feld nicht belegt ist und man an der startposition ist
                        if (tile.Y == this.CurrentTile.Y + 2 && tile.CurrentFigure == null && this.CurrentTile.Y == 1)
                        {
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
