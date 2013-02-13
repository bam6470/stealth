using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rogue
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Map testMap;
        Guard testGuard;
        SpriteFont arial;


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
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
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
            player = new Player(Content.Load<Texture2D>(@"images/playerSprite"), new Vector2(100, 100), new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), Content.Load<Texture2D>(@"images/staminaBack"), Content.Load<Texture2D>(@"images/staminaFront"), Content.Load<Texture2D>(@"images/HealthBack"), Content.Load<Texture2D>(@"images/HealthFront"));
            testMap = new Map(Content.Load<Texture2D>("MapTest"));
            testMap.LoadMap(new StreamReader("MapTestText.txt"), Content.Load<Texture2D>("MapTest"));
            // TODO: use this.Content to load your game content here
            testGuard = new Guard(new Vector2(0, 0), Content.Load<Texture2D>(@"images/guard"), 0, false, null);
            arial = Content.Load<SpriteFont>(@"Fonts/Arial");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            player.Update(testMap.GetTiles(new Vector2(400, 300)));
            testGuard.Update(player, gameTime);
            // TODO: Add your update logic here

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
            // Draw backdround
            testMap.DrawMap(spriteBatch,player.offset,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight);
            testGuard.Draw(spriteBatch, player);
            player.Draw(spriteBatch);
            spriteBatch.DrawString(arial, Convert.ToString("Text"), Vector2.Zero, Color.Red);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
