﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        GameObject projectiles;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;
            
            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("KirbySingle.png");
            heros.position = heros.sprite.Bounds;

            Ennemi = new GameObject();
            Ennemi.estVivant = true;
            Ennemi.vitesse = 5;
            Ennemi.sprite = Content.Load<Texture2D>("KirbyEnnemi.png");
            Ennemi.position = new Rectangle(fenetre.Right-Ennemi.sprite.Bounds.Width,10,Ennemi.sprite.Bounds.Width,Ennemi.sprite.Bounds.Height);

            projectiles = new GameObject();
            projectiles.estVivant = true;
            projectiles.vitesse = 5;
            projectiles.sprite = Content.Load<Texture2D>("ArmeKirby.png");
            projectiles.position = Ennemi.position;

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

            else if (heros.position.X > fenetre.Right-heros.sprite.Width)
            {
                heros.position.X = fenetre.Right - heros.sprite.Width;
            }
            else if (heros.position.Y > fenetre.Bottom-heros.sprite.Height)
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
            }

            
            else if (Ennemi.position.Y > fenetre.Bottom - Ennemi.sprite.Height)
            {

                Ennemi.position.Y = fenetre.Bottom - Ennemi.sprite.Height;
                Ennemi.position.Y = Ennemi.vitesse;
            }
        }
        protected void UpdateProjectiles()
        {
            projectiles.position = Ennemi.position;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            spriteBatch.Draw(Ennemi.sprite, Ennemi.position, Color.White);
            spriteBatch.Draw(projectiles.sprite, projectiles.position, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
