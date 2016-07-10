using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Model
{
    public class Goomba
    {
        public Rectangle Person;
        public Point Position;
        public Side Direction { get; set; }
        public const int GOOMBA_INITIAL_Y_POSITION = 380;

        public Goomba()
        {
            this.Position = new Point(500, GOOMBA_INITIAL_Y_POSITION);
            this.Person = new Rectangle(500, GOOMBA_INITIAL_Y_POSITION, 40, 42);
            this.Direction = Side.Right;
        }

        public void Walk()
        {
            if (Direction == Side.Left)
            {
                if (Person.X >= 300)
                    Person.X -= 4;
                if (Person.X == 300)
                    Direction = Side.Right;
            }
            if (Direction == Side.Right)
            {
                if (Person.X < 500)
                    Person.X += 4;
                if (Person.X == 500)
                    Direction = Side.Left;
            }
        }

    }
    
}
