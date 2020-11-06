﻿using System.Drawing;
using System.Windows.Forms;

namespace Schach_v1
{
    internal class Tile : Panel
    {

        public int ID;
        public Figure CurrenFigure = null;

        public Tile(Size tileSize, Color Color, int id)
        {
            BackColor = Color;
            ID = id;
            Width = tileSize.Width;
            Height = tileSize.Height;
        }

    }
}