using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships.Model
{
    class Ship
    {
        public Cell[] buttons;
        public int life;
        public int Maxlife;
        public Direction direction;
        public bool Destroyed = false;
        public Ship(Cell[] buttons, int life, Direction direction, bool visible) 
        {
            this.buttons = buttons;
            this.life = life;
            this.Maxlife = life;
            this.direction = direction;
            foreach (var but in buttons)
            {
                if (visible)
                {
                    switch (life)
                    {

                        case 1:
                            but.BackColor = Color.Red;
                            break;
                        case 2:
                            but.BackColor = Color.Blue;
                            break;
                        case 3:
                            but.BackColor = Color.Green;
                            break;
                        case 4:
                            but.BackColor = Color.Purple;
                            break;
                    }
             
                but.Enabled = false;
                }
                but.Empty = false;
            }
        }
    }
}
