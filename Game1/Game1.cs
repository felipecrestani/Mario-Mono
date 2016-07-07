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
        Rectangle mario, life, goomba;
        Point mario_position, life_position, goomba_position;
        bool jump;
        string mario_direction = "right";
        string goomba_direction = "right";
        public int mario_life = 1;
        string hud;
        SoundEffect jump_sound, kick_sound, mario_theme;
        SpriteFont UVfont;

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
            // TODO: Add your initialization logic here

            mario = new Rectangle(0, 0, 48, 92);
            mario_position = new Point(0, 385);
            mario_direction = "left";

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
            spriteBatch = new SpriteBatch(this.graphics.GraphicsDevice);
            UVfont = Content.Load<SpriteFont>("SpriteFont1");

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

            mario.Y = mario_position.Y;
            mario.X = mario_position.X;

            life.Y = life_position.Y;
            life.X = life_position.X;

            goomba.Y = goomba_position.Y;
            goomba.X = goomba_position.X;


            hud = "Life: " + mario_life;

            Window.Title = "X:" + mario_position.X + " Y:" + mario_position.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                mario_position.X += 3;
                mario_direction = "right";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                mario_position.X -= 3;
                mario_direction = "left";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (!jump)
                    if (mario_position.Y == 385)
                    {
                        jump = true;
                        jump_sound.Play();
                    }
            }


            //Mario Jump
            if (jump)
            {
                if (mario_position.Y > 340)
                    mario_position.Y--;

                if (mario_position.Y == 340)
                    jump = false;
            }
            else
            {
                if (mario_position.Y <= 384)
                    mario_position.Y++;
            }

            if (mario.Intersects(life))
            {
                mario_life++;
                life_position.Y = 999;
            }

            //Goomba walk
            if (goomba_direction.Equals("right"))
            {
                if (goomba_position.X >= 300)
                    goomba_position.X--;
                if (goomba_position.X == 300)
                    goomba_direction = "left";
            }
            if (goomba_direction.Equals("left"))
            {
                if (goomba_position.X < 500)
                    goomba_position.X++;
                if (goomba_position.X == 500)
                    goomba_direction = "right";
            }

            //Mario x Goomba

            if (mario.Intersects(goomba))
            {
                goomba_position.Y = 999;
                kick_sound.Play();
            }

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

            if (mario_direction == "right")
            {
                spriteBatch.Draw(Content.Load<Texture2D>("mario"), mario, Color.White);
            }
            else if (mario_direction == "left")
            {
                spriteBatch.Draw(Content.Load<Texture2D>("mario2"), mario, Color.White);
            }

            spriteBatch.DrawString(UVfont, hud, Vector2.Zero, Color.Black);

            spriteBatch.Draw(Content.Load<Texture2D>("life"), life, Color.White);

            spriteBatch.Draw(Content.Load<Texture2D>("goomba"), goomba, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
