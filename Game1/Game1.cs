using Game1.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle life, goomba;
        Point life_position, goomba_position;
        string hud, goomba_direction;
        SoundEffect jump_sound, kick_sound, mario_theme;
        SpriteFont Font;
        Texture2D background;
        private Mario Mario;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Mario = new Mario();

            life = new Rectangle(0, 0, 48, 48);
            life_position = new Point(300, 315);

            goomba = new Rectangle(0, 0, 40, 42);
            goomba_position = new Point(500, 435);
            goomba_direction = "right";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Font = Content.Load<SpriteFont>("Font");
            background = this.Content.Load<Texture2D>(@"background");

            jump_sound = this.Content.Load<SoundEffect>("jump");
            kick_sound = this.Content.Load<SoundEffect>("kick");
            mario_theme = this.Content.Load<SoundEffect>("mario theme");
            mario_theme.Play();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            hud = $"X {Mario.Person.X} - Y {Mario.Person.Y}";

            life.Y = life_position.Y;
            life.X = life_position.X;

            goomba.Y = goomba_position.Y;
            goomba.X = goomba_position.X;

            Window.Title = "X:" + Mario.Person.X + " Y:" + Mario.Person.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Mario.Direction = Side.Right;
                Mario.Move();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Mario.Direction = Side.Left;
                Mario.Move();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (Mario.Person.Y == Mario.MARIO_INITIAL_X_POSITION)
                {
                    Mario.IsJump = true;
                    jump_sound.Play();
                }                
            }

            if (Mario.IsJump)
            {
                Mario.Jump();
            }

            if (Mario.Person.Intersects(life))
            {
                Mario.Life++;
                life_position.Y = 999;
            }

            //Goomba walk
            if (goomba_direction.Equals("right"))
            {
                if (goomba_position.X >= 300)
                    goomba_position.X -= 4;
                if (goomba_position.X == 300)
                    goomba_direction = "left";
            }
            if (goomba_direction.Equals("left"))
            {
                if (goomba_position.X < 500)
                    goomba_position.X += 4;
                if (goomba_position.X == 500)
                    goomba_direction = "right";
            }

            //Mario x Goomba
            if (Mario.Person.Intersects(goomba))
            {
                goomba_position.Y = 999;
                kick_sound.Play();
            }

            if(Mario.Life == 2 && Mario.Person.X == 750)


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            this.spriteBatch.Draw(background, new Rectangle(0, 0, 800, 500), Color.White);

            if (Mario.Direction == Side.Right)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("mario"), Mario.Person, Color.White);
            }
            else if (Mario.Direction == Side.Left)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("mario2"), Mario.Person, Color.White);
            }

            spriteBatch.Draw(Content.Load<Texture2D>("life"), life, Color.White);

            spriteBatch.Draw(Content.Load<Texture2D>("goomba"), goomba, Color.White);

            spriteBatch.DrawString(Font, hud , Vector2.Zero, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
