using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Engine
{
    public static class Camera
    {
        private static Vector2 cameraPos;       
        public static int screenWidth, screenHeight, screenXcap, screenYcap, scrollingSpeed;
        public static int screenRightOffset = 0;
        public static int screenLeftOffset = 0;
                      
        public static Vector2 CameraPos                                                 //property to set and get the value of cameraPos
        {                                                                               //to prevent the camera to get out of the playing field
            get { return cameraPos;}                                                    //the set part will only accept values that are between 0 and the screencap value
            set
            {
                cameraPos.Y = MathHelper.Clamp(value.Y, 0f, screenYcap - screenHeight);
                cameraPos.X = MathHelper.Clamp(value.X, 0f + screenLeftOffset, screenXcap - screenWidth + screenRightOffset);
            }              
        }
     }
}
