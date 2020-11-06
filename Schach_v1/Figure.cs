using System.Drawing;
using System.Windows.Forms;

namespace Schach_v1
{
    public delegate void EventTypeMoveFigure(Panel panel);
    internal class Figure : Panel
    {
        public event EventTypeMoveFigure Moves;
        public int ID;
        public string Name;

        
        public Figure(Size panelSize, Color color)
        {
            
        }
    }
}
