using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace ExerciceJeuxVersion2
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
        GameObject projectiles;
        GameObject armeKirby;
        public Texture2D fond;
        SoundEffect son;
        SoundEffectInstance mort;
        SpriteFont font;
        int viekirby = 5;


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

            //this.graphics.ToggleFullScreen();
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
            //Song winSong = Content.Load<Song>("Son\\KirbyDanse");
            //Song gameOver = Content.Load<Song>("Son\\GameOverKirby");
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

            armeKirby = new GameObject();
            armeKirby.estVivant = false;
            armeKirby.vitesse = 20;
            armeKirby.sprite = Content.Load<Texture2D>("Objets/ProjectilesKirby.png");
            armeKirby.position = armeKirby.sprite.Bounds;
            //armeKirby.position.X = heros.position.X;
            //armeKirby.position.Y = heros.position.Y;



            this.fond = this.Content.Load<Texture2D>("Objets/kirbyFond.png");

            son = Content.Load<SoundEffect>("Son\\KirbyDeath");
            mort = son.CreateInstance();

            font = Content.Load<SpriteFont>("Font/Font");

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
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //armeKirby.position.X = heros.position.X;
                // armeKirby.position.Y = heros.position.Y;
                armeKirby.estVivant = true;
                //armeKirby.position.X += armeKirby.vitesse;

                
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

            armeKirby.position.X =  armeKirby.position.X + armeKirby.vitesse;
            if (armeKirby.position.X > fenetre.Left - armeKirby.position.Width)
            {
                armeKirby.position.X = heros.position.X;
                armeKirby.position.Y = heros.position.Y;
                armeKirby.estVivant = false;
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
                viekirby--;

                if (viekirby == 0)
                {
                    heros.estVivant = false;
                    heros.position.X = heros.position.X - 3000;
                    heros.position.Y = heros.position.Y - 3000;

                }

                projectiles.position.X = Ennemi.position.X;
                projectiles.position.Y = Ennemi.position.Y;

            }
            if (projectiles.position.Intersects(heros.position))
            {
                projectiles.estVivant = false;
            }

            if (armeKirby.position.Intersects(Ennemi.position))
            {
                projectiles.estVivant = false;
                armeKirby.position.X = heros.position.X;
                armeKirby.position.Y = heros.position.Y;
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


            if (heros.estVivant)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);

                if (armeKirby.estVivant)
                {
                    spriteBatch.Draw(armeKirby.sprite, heros.position, Color.White);
                }
                

            }

            if (heros.estVivant == false)
            {
                MediaPlayer.Stop();
                Song gameOver = Content.Load<Song>("Son\\GameOverKirby1");
                MediaPlayer.Play(gameOver);

                spriteBatch.DrawString(font,
                    "****************\n"+
                    "*    D.E.A.D   *\n"+
                    "****************\n", 
                    new Vector2(600, 100), Color.Red);

                Ennemi.position.X = Ennemi.position.X + 2000;
                Ennemi.position.Y = Ennemi.position.Y + 2000;
                projectiles.position.X = projectiles.position.X + 2000;
                projectiles.position.Y = projectiles.position.Y + 2000;
                

            }

            if (Ennemi.estVivant)
            {
                spriteBatch.Draw(Ennemi.sprite, Ennemi.position, Color.White);
                spriteBatch.Draw(projectiles.sprite, projectiles.position, Color.White);

            }

            if (Ennemi.estVivant == false)
            {
                MediaPlayer.Stop();
                Song winSong = Content.Load<Song>("Son\\KirbyDanse");
                MediaPlayer.Play(winSong);
                spriteBatch.DrawString(font, "YOU WIN!!!", new Vector2(600, 100), Color.Black);

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}

//Demander comment faire pour que mon projectile part du Heros. CA MARCHE PAS ;C
//Demander comment faire pour changer de chanson quand le joueur perd ou meurt.

