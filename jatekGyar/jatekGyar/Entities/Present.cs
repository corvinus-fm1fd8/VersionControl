using jatekGyar.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace jatekGyar.Entities
{
    public class Present : Toy
    {
        public SolidBrush RibbonColor { get; private set; }
        public SolidBrush BoxColor { get; private set; }
       
        protected override void DrawImage(Graphics g)
        {  
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            g.FillRectangle(RibbonColor, 25, 0, Width/5, Height);
            g.FillRectangle(RibbonColor, 0, 25, Width, Height/5);
        }
    }
}
