using Microsoft.Xna.Framework;

namespace Game1.Model
{
    public class Mario
    {
        public Rectangle Person;
        public Point Position;
        public Side Direction { get; set; }
        public int MyProperty { get; set; }
        public bool IsJump{ get; set; }
        public bool HighJump { get; set; }
        public int Life { get; set; }
        public const int MARIO_INITIAL_X_POSITION = 330;
        public const int MARIO_JUMP_SIZE = 250;

        public Mario()
        {
            this.Life = 1;
            this.Position = new Point(0, MARIO_INITIAL_X_POSITION);
            this.Person = new Rectangle(0, MARIO_INITIAL_X_POSITION, 48, 92);
            this.Direction = Side.Left;
        }

        public void Move()
        {
            if (Direction == Side.Left)
            {
                Person.X -= 3;
                Direction = Side.Left;
            }
            
            if (Direction == Side.Right)
            {
                Person.X += 3;
                Direction = Side.Right;
            }
        }

        public void Jump()
        {
            if (Person.Y > MARIO_JUMP_SIZE && !HighJump)
                Person.Y -= 3;

            if(Person.Y <= MARIO_JUMP_SIZE)
            {
                HighJump = true;
            }

            if (HighJump)
            {
                Person.Y += 3;
                if (Person.Y == MARIO_INITIAL_X_POSITION)
                {
                    IsJump = false;
                    HighJump = false;
                }
            }
        }

    }
}
