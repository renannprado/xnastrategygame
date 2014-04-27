using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DummyProject.Helpers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FPSComponent : DrawableGameComponent
    {
        /// <summary>
        /// Ordem em que este componente será desenhado.
        /// </summary>
        private const int drawOrder = 99;
        private const int updateOrder = 99;

        private SpriteBatch spriteBatch;
        private SpriteFont VideoFont;
        private float _ElapsedTime, _TotalFrames, _Fps;
        private string _FontName = "SpriteFont";

        public FPSComponent(Game game): base(game)
        {

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            //setando o draworder do componente
            this.DrawOrder = drawOrder;
            this.UpdateOrder = updateOrder;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            _ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _TotalFrames++;

            if (_ElapsedTime >= 1.0f)
            {
                _Fps = _TotalFrames;
                _TotalFrames = 0;
                _ElapsedTime = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            // Load the required font
            VideoFont = Game.Content.Load<SpriteFont>(_FontName);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(VideoFont,
                "FPS=" + _Fps.ToString(),
                new Vector2(0, Game.GraphicsDevice.Viewport.Height - VideoFont.LineSpacing),
                Color.Red,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
