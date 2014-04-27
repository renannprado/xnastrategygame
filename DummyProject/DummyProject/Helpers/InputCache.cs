using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DummyProject.Helpers
{
    public class InputCache : GameComponent
    {
        private const int updateOrder = 0;
        
        private MouseState _previousMouseState;
        private MouseState _currentMouseState;
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        public InputCache(Game game) : base(game)
        { 
        
        }

        public override void Initialize()
        {
            this.UpdateOrder = updateOrder;

            base.Initialize();
        }

        public MouseState CurrentMouseState
        {
            get { return _currentMouseState; }
            private set
            {
                _previousMouseState = _currentMouseState;
                _currentMouseState = value;
            }
        }

        public MouseState PreviousMouseState
        {
            get { return _previousMouseState; }
            //private set { _previousMouseState = value; }
        }

        public KeyboardState CurrentKeyboardState
        {
            get { return _currentKeyboardState; }
            private set 
            {
                _previousKeyboardState = _currentKeyboardState;
                _currentKeyboardState = value; 
            }
        }

        public KeyboardState PreviousKeyboardState
        {
            get { return _previousKeyboardState; }
        }

        public override void Update(GameTime gameTime)
        {
            this.CurrentKeyboardState = Keyboard.GetState();
            this.CurrentMouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        public bool hasLeftButtonClicked
        {
            get
            {
                return
                    ((CurrentMouseState.LeftButton == ButtonState.Pressed
                    && PreviousMouseState.LeftButton == ButtonState.Released) ? true : false);
            }
        }

        public bool wasButtonPressedAndReleased(Keys k)
        {
            return
            ((CurrentKeyboardState.IsKeyDown(k) && PreviousKeyboardState.IsKeyUp(k) ) ? true : false);
        }
    }
}
