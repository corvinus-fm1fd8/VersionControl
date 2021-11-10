﻿using jatekGyar.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jatekGyar.Entities
{
    public class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image kep = Image.FromFile("Images/car.png");
            g.DrawImage(kep, new Rectangle(0, 0, Width, Height));
        }
    }
}
