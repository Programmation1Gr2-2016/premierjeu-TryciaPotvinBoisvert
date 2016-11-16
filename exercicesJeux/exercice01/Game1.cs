using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace exercice01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject Ennemi;
        GameObject[] tableauEnnemi = new GameObject[10];
        GameObject projectiles;
        public Texture2D fond;
        SoundEffect son;
        SoundEffectInstance mort;

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

            this.graphics.ToggleFullScreen();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Song song = Content.Load<Song>("Son\\Musique");
            MediaPlayer.Play(song);


            spriteBatch = new SpriteBatch(GraphicsDevice);

            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;

            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 10;
            heros.sprite = Content.Load<Texture2D>("Objets/KirbySingle.png");
            heros.position = heros.sprite.Bounds;

            Ennemi = new GameObject();
            Ennemi.estVivant = true;
            Ennemi.vitesse = 10;
            Ennemi.sprite = Content.Load<Texture2D>("Objets/KirbyEnnemi.png");
            //Ennemi.position = new Rectangle(fenetre.Right-Ennemi.sprite.Bounds.Width,10,Ennemi.sprite.Bounds.Width,Ennemi.sprite.Bounds.Height);
            Ennemi.position = Ennemi.sprite.Bounds;
            Ennemi.position.X = fenetre.Right - Ennemi.sprite.Width;

            projectiles = new GameObject();
            projectiles.estVivant = true;
            projectiles.vitesse = 20;
            projectiles.sprite = Content.Load<Texture2D>("Objets/ArmeKirby.png");
            projectiles.position = projectiles.sprite.Bounds;
            projectiles.position.X = Ennemi.position.X;
            projectiles.position.Y = Ennemi.position.Y;



            for (int i = 0; i < tableauEnnemi.GetLength(0) ; i++)
            {
                for (int j = 0; j < tableauEnnemi.GetLength(1); j++)
                {
                    
            tableauEnnemi[i] = new GameObject();
            tableauEnnemi[i].estVivant = true;
            tableauEnnemi[i].vitesse = 10;
            tableauEnnemi[i].sprite = Content.Load<Texture2D>("Objets/KirbyEnnemi.png");
            tableauEnnemi[i].position = Ennemi.sprite.Bounds;
            tableauEnnemi[i].position.X = fenetre.Right - Ennemi.sprite.Width;

                }
            }

            this.fond = this.Content.Load<Texture2D>("Objets/kirbyFond.png");

            son = Content.Load<SoundEffect>("Son\\KirbyDeath");
            mort = son.CreateInstance();

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heros.position.X -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heros.position.Y -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heros.position.Y += heros.vitesse;
            }

            UpdateHeros();
            UpdateEnnemi();
            UpdateProjectiles();
            UpdateCollision();
            
            base.Update(gameTime);
        }

        protected void UpdateHeros()
        {
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            else if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }

            else if (heros.position.X > fenetre.Right - heros.sprite.Width)
            {
                heros.position.X = fenetre.Right - heros.sprite.Width;
            }
            else if (heros.position.Y > fenetre.Bottom - heros.sprite.Height)
            {

                heros.position.Y = fenetre.Bottom - heros.sprite.Height;
            }

        }

        protected void UpdateEnnemi()
        {
            int nombreEnnemi = 0;


            Ennemi.position.Y += Ennemi.vitesse;

            if (Ennemi.position.Y < fenetre.Top)
            {
                Ennemi.position.Y = fenetre.Top;
                Ennemi.vitesse = -(Ennemi.vitesse);
            }


            else if (Ennemi.position.Y > fenetre.Bottom - Ennemi.sprite.Height)
            {

                Ennemi.position.Y = fenetre.Bottom - Ennemi.sprite.Height;
                Ennemi.vitesse = -(Ennemi.vitesse);
            }
        }

        protected void UpdateProjectiles()
        {
            projectiles.position.X = projectiles.position.X - projectiles.vitesse;
            if (projectiles.position.X < fenetre.Left)
            {
                projectiles.position.X = Ennemi.position.X;
                projectiles.position.Y = Ennemi.position.Y;
            }


        }

        protected void UpdateCollision()
        {
            //Quand le heros ou l'ennemie se touche. ils meurent.

            if (heros.position.Intersects(Ennemi.position))
            {
                Ennemi.estVivant = false;
                //Sort l'ennemi de le fenêtre = ne peut pas continue a tuer le heros.
                Ennemi.position.X = Ennemi.position.X - 2000;
                Ennemi.position.Y = Ennemi.position.Y - 2000;
            }

            if (heros.position.Intersects(projectiles.position))
            {
                heros.position = heros.sprite.Bounds;

                //joue le son quand le joueur se fait tuer        
                mort.Play();

                projectiles.position.X = Ennemi.position.X;
                projectiles.position.Y = Ennemi.position.Y;
                
            }
            if (projectiles.position.Intersects(heros.position))
            {
                projectiles.estVivant = false;
            }
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Mettre en ordre d'affichage
            spriteBatch.Begin();

            this.spriteBatch.Draw(fond, fenetre, Color.White);
            
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            
            if (Ennemi.estVivant)
            {
                spriteBatch.Draw(Ennemi.sprite, Ennemi.position, Color.White);
                spriteBatch.Draw(projectiles.sprite, projectiles.position, Color.White);

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
