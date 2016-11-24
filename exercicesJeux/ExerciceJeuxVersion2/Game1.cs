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
        //GameObject projectiles;
        GameObject[] tabProjectiles = new GameObject[3];
        Random projectilesRdm = new Random();
        GameObject armeKirby;
        public Texture2D fond;
        SoundEffect son;
        SoundEffectInstance mort;
        SpriteFont font;
        bool sonGagne = false;
        bool sonPerd = false;
        Song winSong;
        Song gameOver;

        int viekirby = 5;
        int vieEnnemi = 3;


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

            this.fond = this.Content.Load<Texture2D>("Objets/kirbyFond.png");
            font = Content.Load<SpriteFont>("Font/Font");

            //Musique
            Song song = Content.Load<Song>("Son\\Musique");
            winSong = Content.Load<Song>("Son\\KirbyDanse");
            gameOver = Content.Load<Song>("Son\\GameOverKirby1");
            MediaPlayer.Play(song);

            //Son
            son = Content.Load<SoundEffect>("Son\\KirbyDeath");
            mort = son.CreateInstance();

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
            Ennemi.position = Ennemi.sprite.Bounds;
            Ennemi.position.X = fenetre.Right - Ennemi.sprite.Width;

            /*projectiles = new GameObject();
            projectiles.estVivant = true;
            projectiles.vitesse = 20;
            projectiles.sprite = Content.Load<Texture2D>("Objets/ArmeKirby.png");
            projectiles.position = projectiles.sprite.Bounds;
            projectiles.position.X = Ennemi.position.X;
            projectiles.position.Y = Ennemi.position.Y;*/

            for (int i = 0; i < tabProjectiles.Length; i++)
            {
                tabProjectiles[i] = new GameObject();
                tabProjectiles[i].estVivant = true;
                tabProjectiles[i].vitesse = 10;
                tabProjectiles[i].sprite = Content.Load<Texture2D>("Objets/ArmeKirby.png");
                tabProjectiles[i].position = tabProjectiles[i].sprite.Bounds;
                tabProjectiles[i].position.X = Ennemi.position.X;
                tabProjectiles[i].position.Y = Ennemi.position.Y;
                tabProjectiles[i].direction = Vector2.Zero;
                tabProjectiles[i].direction.X = projectilesRdm.Next(-4, 5);
                tabProjectiles[i].direction.Y = projectilesRdm.Next(-4, 5);

            }

            armeKirby = new GameObject();
            armeKirby.estVivant = false;
            armeKirby.vitesse = 20;
            armeKirby.sprite = Content.Load<Texture2D>("Objets/ProjectilesKirby.png");
            armeKirby.position = armeKirby.sprite.Bounds;

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
                armeKirby.position.X = heros.position.X;
                armeKirby.position.Y = heros.position.Y;
                armeKirby.estVivant = true;

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
            for (int i = 0; i < tabProjectiles.Length; i++)
            {
                tabProjectiles[i].position.X = tabProjectiles[i].position.X - tabProjectiles[i].vitesse;
                tabProjectiles[i].position.X += (int)tabProjectiles[i].direction.X;
                tabProjectiles[i].position.Y += (int)tabProjectiles[i].direction.Y;

                if (tabProjectiles[i].position.Y < fenetre.Top)
                {
                    tabProjectiles[i].position.Y = fenetre.Top;
                    tabProjectiles[i].direction.X = projectilesRdm.Next(-4, 5);
                    tabProjectiles[i].direction.Y = projectilesRdm.Next(-4, 5);
                }


                if (tabProjectiles[i].position.Y > fenetre.Bottom - tabProjectiles[i].sprite.Height)
                {

                    tabProjectiles[i].position.Y = fenetre.Bottom - tabProjectiles[i].sprite.Height;
                    tabProjectiles[i].direction.X = projectilesRdm.Next(-4, 5);
                    tabProjectiles[i].direction.Y = projectilesRdm.Next(-4, 5);
                }

                if (tabProjectiles[i].position.X > fenetre.Right - tabProjectiles[i].sprite.Width)
                {
                    tabProjectiles[i].position.X = fenetre.Right - tabProjectiles[i].sprite.Width;
                    tabProjectiles[i].direction.X = projectilesRdm.Next(-4, 5);
                    tabProjectiles[i].direction.Y = projectilesRdm.Next(-4, 5);
                }


                if (tabProjectiles[i].position.X < fenetre.Left)
                {
                    tabProjectiles[i].position.X = Ennemi.position.X;
                    tabProjectiles[i].position.Y = Ennemi.position.Y;
                }
            }

            /*projectiles.position.X = projectiles.position.X - projectiles.vitesse;
            if (projectiles.position.X < fenetre.Left)
            {
                projectiles.position.X = Ennemi.position.X;
                projectiles.position.Y = Ennemi.position.Y;
            }*/


            if (armeKirby.estVivant && heros.estVivant)
            {
                armeKirby.position.X = armeKirby.position.X + armeKirby.vitesse;
                if (armeKirby.position.X > fenetre.Right + armeKirby.sprite.Bounds.Width)
                {
                    armeKirby.estVivant = false;
                    armeKirby.position.X = heros.position.X;
                    armeKirby.position.Y = heros.position.Y;

                }

            }


        }

        protected void UpdateCollision()
        {
            //Quand le heros ou l'ennemie se touche. ils meurent.

            if (heros.position.Intersects(Ennemi.position))
            {

                vieEnnemi--;

                if (vieEnnemi == 0)
                {
                    Ennemi.estVivant = false;
                    //Sort l'ennemi de le fenêtre = ne peut pas continue a tuer le heros.
                    Ennemi.position.X = Ennemi.position.X - 2000;
                    Ennemi.position.Y = Ennemi.position.Y - 2000;
                }


            }

            for (int i = 0; i < tabProjectiles.Length; i++)
            {


                if (heros.position.Intersects(tabProjectiles[i].position))
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

                    tabProjectiles[i].position.X = Ennemi.position.X;
                    tabProjectiles[i].position.Y = Ennemi.position.Y;

                }

                /*if (projectiles.position.Intersects(heros.position))
                {
                    projectiles.estVivant = false;
                }*/
            }

            if (armeKirby.position.Intersects(Ennemi.position))
            {
                armeKirby.estVivant = false;
                armeKirby.position.X = heros.position.X;
                armeKirby.position.Y = heros.position.Y;

                vieEnnemi--;

                if (vieEnnemi == 0)
                {
                    Ennemi.estVivant = false;
                }
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

            //Affiche le Heros
            if (heros.estVivant)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);

                if (armeKirby.estVivant)
                {
                    spriteBatch.Draw(armeKirby.sprite, heros.position, Color.White);
                }


            }

            //Afficher Message Perdant.
            if (heros.estVivant == false)
            {
                if (!sonPerd)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(gameOver);
                    sonPerd = true;
                }

                spriteBatch.DrawString(font,
                    "****************\n" +
                    "*    D.E.A.D   *\n" +
                    "****************\n",
                    new Vector2(550, 250), Color.Red);
                heros.position.X = heros.position.X - 2000;
                heros.position.Y = heros.position.Y - 2000;
                Ennemi.position.X = Ennemi.position.X + 2000;
                Ennemi.position.Y = Ennemi.position.Y + 2000;
                //projectiles.position.X = projectiles.position.X + 2000;
                //projectiles.position.Y = projectiles.position.Y + 2000;
                for (int i = 0; i < tabProjectiles.Length; i++)
                {
                    if (tabProjectiles[i].estVivant)
                    {
                        tabProjectiles[i].position.X = tabProjectiles[i].position.X + 2000;
                        tabProjectiles[i].position.Y = tabProjectiles[i].position.Y + 2000;
                    }
                }
            }

            //Affiche l'ennemi et tableau projectiles
            if (Ennemi.estVivant)
            {
                spriteBatch.Draw(Ennemi.sprite, Ennemi.position, Color.White);
                //spriteBatch.Draw(projectiles.sprite, projectiles.position, Color.White);

                for (int i = 0; i < tabProjectiles.Length; i++)
                {
                    if (tabProjectiles[i].estVivant)
                    {
                        spriteBatch.Draw(tabProjectiles[i].sprite, tabProjectiles[i].position, Color.White);
                    }
                }

            }

            //Affiche message gagnant
            if (Ennemi.estVivant == false)
            {


                if (!sonGagne)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(winSong);
                    sonGagne = true;
                }

                spriteBatch.DrawString(font, "YOU WIN!!!", new Vector2(550, 250), Color.Black);
                spriteBatch.DrawString(font, "Time: " + gameTime.TotalGameTime.Seconds.ToString(), new Vector2(0, 0), Color.Black);
            }
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}


