using jatekGyar.Abstractions;
using jatekGyar.Entities;
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
        List<Toy> _toys = new List<Toy>();
        Toy _nextToy;

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
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
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
            Left = -Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int legjobbra=0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (item.Left>legjobbra)
                {
                    legjobbra = item.Left;
                }
            }
            if (legjobbra>1000)
            {
                var elso = _toys[0];
                _toys.Remove(elso);
                mainPanel.Controls.Remove(elso);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory=new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            {
                BallColor = button3.BackColor
            };

        }
        private void DisplayNext()
        {
            if (_nextToy!=null)
            {
                mainPanel.Controls.Remove(_nextToy);
            }
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BackColor = Color.BlanchedAlmond;
            var button = (Button)sender;
            ColorDialog cd = new ColorDialog();
            cd.Color = BackColor;
            if (cd.ShowDialog()!=DialogResult.OK)
            {
                return;
            }
            else
            {
                button3.BackColor = cd.Color;
            }
        }
    }
}
