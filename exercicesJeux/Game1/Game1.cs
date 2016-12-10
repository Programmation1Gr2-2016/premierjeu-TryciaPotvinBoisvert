using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;

        // Fond de tuiles
        GameObjectTile fond;
        GameObjectAnime cat;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        SpriteFont font;

        //temps
        bool calculTemps = true;
        int totalTemps;
        int finTempsSec;
        double moyenne = 0;


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
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.Window.Position = new Point(0, 0);
            this.graphics.ApplyChanges();

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

            // TODO: use this.Content to load your game content here
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;


            font = Content.Load<SpriteFont>("Font/Font");

            fond = new GameObjectTile();
            fond.texture = Content.Load<Texture2D>("Fond\\TileSheet.png");

            //musique
            Song song = Content.Load<Song>("Music\\Motivated");
            MediaPlayer.Play(song);

            cat = new GameObjectAnime();
            cat.direction = Vector2.Zero;
            cat.estVivant = true;
            cat.vitesse.X = 2;
            cat.vitesse.Y = 2;
            cat.objetState = GameObjectAnime.etats.attenteDroite;
            cat.position = new Rectangle(65, 65, 63, 63);   //Position initiale du chat
            cat.sprite = Content.Load<Texture2D>("Perso\\SpriteCat1.png");


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keys = Keyboard.GetState();
            //cat.position.X += (int)(cat.vitesse.X * cat.direction.X);
            //cat.position.Y += (int)(cat.vitesse.Y * cat.direction.Y);


            if (keys.IsKeyDown(Keys.Right))
            {
                cat.direction.X = 2;
                cat.objetState = GameObjectAnime.etats.runDroite;
            }
            if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
            {
                cat.direction.X = 0;
                cat.objetState = GameObjectAnime.etats.attenteDroite;
            }

            if (keys.IsKeyDown(Keys.Left))
            {
                cat.direction.X = -2;
                cat.objetState = GameObjectAnime.etats.runGauche;
            }
            if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
            {
                cat.direction.X = 0;
                cat.objetState = GameObjectAnime.etats.attenteGauche;
            }

            if (keys.IsKeyDown(Keys.Up))
            {
                cat.direction.Y = -2;
                cat.objetState = GameObjectAnime.etats.runHaut;
            }
            if (keys.IsKeyUp(Keys.Up) && previousKeys.IsKeyDown(Keys.Up))
            {
                cat.direction.Y = 0;
                cat.objetState = GameObjectAnime.etats.attenteHaut;
            }
            if (keys.IsKeyDown(Keys.Down))
            {
                cat.direction.Y = 2;
                cat.objetState = GameObjectAnime.etats.runBas;
            }
            if (keys.IsKeyUp(Keys.Down) && previousKeys.IsKeyDown(Keys.Down))
            {
                cat.direction.Y = 0;
                cat.objetState = GameObjectAnime.etats.attenteBas;
            }

            if (keys.IsKeyDown(Keys.Space))
            {
                cat.position = new Rectangle(65, 65, 63, 63);
            }

            //On appelle la méthode Update du chat qui permet de gérer les états
            cat.Update(gameTime);
            previousKeys = keys;
            cat.position.X += (int)(cat.vitesse.X * cat.direction.X);
            cat.position.Y += (int)(cat.vitesse.Y * cat.direction.Y);
            base.Update(gameTime);
            UpdateCat(gameTime);
            Collision();


        }

        protected void UpdateCat(GameTime gameTime)
        {
            if (cat.position.X < fenetre.Left)
            {
                cat.position.X = fenetre.Left;
            }
            else if (cat.position.Y < fenetre.Top)
            {
                //cat.position.Y = fenetre.Top;
                cat.estVivant = false;
                calculTemps = false;
                

                totalTemps = (int)gameTime.TotalGameTime.TotalSeconds;
                moyenne = totalTemps / 3;

            }

            else if (cat.position.X + 65 > fenetre.Right)
            {
                //cat.position.X = fenetre.Right - 65;

                finTempsSec = (int)gameTime.TotalGameTime.TotalSeconds;

                //Changer la map
                fond.map = fond.map2;
                cat.position = new Rectangle(65, 65, 63, 63);

            }
            else if (cat.position.Y + 65 > fenetre.Bottom)
            {
               
                cat.position.Y = fenetre.Bottom - 65;
                fond.map = fond.map3;
                cat.position = new Rectangle(65, 65, 63, 63);
             
            }

        }

        protected void Collision()
        {
            for (int i = 0; i < GameObjectTile.LIGNE; i++)
            {
                fond.rectSource.Y = (i * GameObjectTile.HAUTEUR_TUILE);

                for (int j = 0; j < GameObjectTile.COLONNE; j++)
                {
                    fond.rectSource.X = (j * GameObjectTile.LARGEUR_TUILE);

                    if (fond.map[i, j] == 1)
                    {
                        Rectangle poil = new Rectangle(cat.position.X + 2, cat.position.Y + 2, cat.position.Width - 4, cat.position.Height - 4);
                        if (poil.Intersects(fond.rectSource))
                        {
                            cat.position.X -= (int)(cat.vitesse.X * cat.direction.X);
                            cat.position.Y -= (int)(cat.vitesse.Y * cat.direction.Y);

                        }
                    }
                    if (fond.map[i, j] == 3)
                    {
                        if (cat.position.Intersects(fond.rectSource))
                        {
                            cat.vitesse.X = 1;
                            cat.vitesse.Y = 1;
                        }
                    }
                    if (fond.map[i, j] == 2)
                    {
                        if (cat.position.Intersects(fond.rectSource))
                        {
                            cat.vitesse.X = 2;
                            cat.vitesse.Y = 2;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            Rectangle lumiere = new Rectangle(cat.position.X - 50, cat.position.Y - 50, cat.spriteAfficher.Width + 150, cat.spriteAfficher.Height + 100);

            fond.Draw(spriteBatch, lumiere);

            //DrawRectangle(new Rectangle(0, 0, (int)font.MeasureString("Time: " + gameTime.TotalGameTime.Seconds + " Sec ").X, (int)font.MeasureString("Time: " + gameTime.TotalGameTime.Seconds + " Sec ").Y), Color.White*.1F);
            
            spriteBatch.Draw(cat.sprite, cat.position, cat.spriteAfficher, Color.White);
            
            if (calculTemps)
            {
                spriteBatch.DrawString(font, "Time: " + gameTime.TotalGameTime.Seconds + " Sec ", new Vector2(0, 0), Color.White);

            }
            else
            {
                spriteBatch.DrawString(font, "Time: " + totalTemps + " Sec ", new Vector2(0, 0), Color.White);
            }
            
            //spriteBatch.DrawString(font, "Time: " + gameTime.TotalGameTime.Minutes + " minutes, " + gameTime.TotalGameTime.Seconds + " Sec ", new Vector2(0, 0), Color.White);

            if (cat.estVivant != true)
            {
                spriteBatch.DrawString(font, "Felicitation vous avez fini le jeu. :) ", new Vector2(500, 500), Color.White);
                spriteBatch.DrawString(font, "Temps moyen: " + moyenne + " Sec ", new Vector2(500, 550), Color.White);
                
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawRectangle(Rectangle coords, Color color)
        {
            var rect = new Texture2D(graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { color });
            spriteBatch.Draw(rect, coords, color);
        }
    }
}
