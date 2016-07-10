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
        public Side Direction { get; set; }
        public const int GOOMBA_INITIAL_Y_POSITION = 380;
        public int InitialXPosition { get; set; }
        private int RandomMove { get; set; }

        public Goomba(int x)
        {
            this.InitialXPosition = x;
            this.Person = new Rectangle(x, GOOMBA_INITIAL_Y_POSITION, 40, 42);
            this.Direction = Side.Right;
            this.RandomMove = new Random().Next(100, 200);
        }

        public void Walk()
        {
            if (Direction == Side.Left)
            {
                Person.X -= 4;
                if (Person.X <= InitialXPosition - RandomMove)
                    Direction = Side.Right;
            }
            if (Direction == Side.Right)
            {
                Person.X += 4;
                if (Person.X >= InitialXPosition + RandomMove)
                    Direction = Side.Left;
            }
        }

    }
    
}
