﻿using jatekGyar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jatekGyar
{
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
            createTimer.Enabled = true;
            conveyorTimer.Enabled = true;
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            mainPanel.Controls.Add(ball);
            Left = -Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int legjobbra=0;
            foreach (var item in _balls)
            {
                item.MoveBall();
                if (item.Left>legjobbra)
                {

                }
            }
            
        }
    }
}
