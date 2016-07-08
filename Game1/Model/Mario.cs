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
        public int Life { get; set; }

        public Mario()
        {
            this.Life = 1;
            this.Position = new Point(0, 385);
            this.Person = new Rectangle(0, 385, 48, 92);
            this.Direction = Side.Left;
        }

        public void Move()
        {
            if (Direction == Side.Left)
            {
                Position.X -= 3;
                Person.X = Position.X;
                Person.Y = Position.Y;
                Direction = Side.Left;
            }
            
            if (Direction == Side.Right)
            {
                Position.X += 3;
                Person.X = Position.X;
                Person.Y = Position.Y;
                Direction = Side.Right;
            }
        }

        public void Jump()
        {
            IsJump = true;

            if (IsJump)
            {
                if (Position.Y > 340)
                    Position.Y -= 3;

                if (Position.Y == 340)
                    IsJump = false;
            }
            else
            {
                if (Position.Y <= 384)
                    Position.Y += 3;
            }
        }

    }
}
