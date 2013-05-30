using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    public class InputHelper
	{
		public MouseState MouseState { get { return currentMouseState; } }
		public KeyboardState KeyboardState { get { return currentKeyboardState; } }
        MouseState currentMouseState, previousMouseState, originalMouseState;
        KeyboardState currentKeyboardState, previousKeyboardState;

        double timeSinceLastKeyPress;
        double keyPressInterval;

        bool mouseLocked = false;

        Vector2 MouseDifference;

        public InputHelper(bool lockMouse = false)
        {
            keyPressInterval = 200;
            timeSinceLastKeyPress = 0;
            mouseLocked = lockMouse;

            if (mouseLocked)
            {
                Mouse.SetPosition(100, 100);
                originalMouseState = Mouse.GetState();
            }
        }

        public void Update(GameTime gameTime)
        {
            // check if keys are pressed and update the timeSinceLastKeyPress variable
            Keys[] prevKeysDown = previousKeyboardState.GetPressedKeys();
            Keys[] currKeysDown = currentKeyboardState.GetPressedKeys();
            if (currKeysDown.Length != 0 && (prevKeysDown.Length == 0 || timeSinceLastKeyPress > keyPressInterval))
                timeSinceLastKeyPress = 0;
            else
                timeSinceLastKeyPress += gameTime.ElapsedGameTime.TotalMilliseconds;

            // update the mouse and keyboard states
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;

            if (mouseLocked)
            {
                MouseDifference = Vector2.Zero;
                if (currentMouseState != originalMouseState)
                {
                    MouseDifference = new Vector2(currentMouseState.X - originalMouseState.X, currentMouseState.Y - originalMouseState.Y);
                    Mouse.SetPosition(100, 100);
                    currentMouseState = originalMouseState;
                }
            }

            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        public Keys[] PressedKeys
        {
            get 
            {
                if(currentKeyboardState != previousKeyboardState)
                    return currentKeyboardState.GetPressedKeys(); 
                return new Keys[0];
            }
        }

        public Vector2 MousePosition
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        public bool MouseLeftButtonPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        public bool KeyPressed(Keys k, bool detecthold = true)
        {
            return currentKeyboardState.IsKeyDown(k) && (previousKeyboardState.IsKeyUp(k) || (timeSinceLastKeyPress > keyPressInterval && detecthold));
        }

        public bool IsKeyDown(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }

        public bool IsKeyUp(Keys k)
        {
            return currentKeyboardState.IsKeyUp(k);
        }

        public bool WasKeyUp(Keys k)
        {
            return previousKeyboardState.IsKeyUp(k);
        }

        public bool MouseLocked
        {
            get { return this.mouseLocked; }
        }

        public Vector2 GetMouseDifference()
        {
            if (!mouseLocked)
                return Vector2.Zero;

            return MouseDifference;
        }
    }
}