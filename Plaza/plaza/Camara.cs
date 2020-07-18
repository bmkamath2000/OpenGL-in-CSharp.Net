using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using System.Drawing;
using ShadowEngine;


namespace Plaza
{
    public class Camara
    {     
        #region Private attributes

        static float eyex, eyey, eyez;
        static float forwardSpeed = 0.2f;
 
        static float angleY, angleX;
        static float rotationSpeed = 1/5f;

        #endregion
    
        public void PlaceCamera(int width, int height)
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(500, 500, 500, 500, 10, 100);
            Glu.gluPerspective(50, width/(float)height, 0.1f, 300);
            eyex = 3f;
            eyey = -4f;
            eyez = 3f;
        }

        public static void CenterMouse()
        {
            MainForm.Instance.CenterMouse();  
            
        }
        


        public void Update(int pressedButton,int movbck)
        {
            #region calculate differences in current and last positions of camera

            Point position = MainForm.Instance.MousePos;
            Point center = MainForm.Instance.ScreenCenter();

            int difX = center.X - position.X;
            int difY = center.Y - position.Y;

            if (position.Y < 384)
            {
                angleX -= rotationSpeed * difY;
            }
            else
                if (position.Y > 384)
                {
                    angleX += rotationSpeed * -difY;
                }
            if (position.X < 512)
            {
                angleY += rotationSpeed * -difX;
            }
            else
                if (position.X > 512)
                {
                    angleY -= rotationSpeed * difX;
                }

            #endregion

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glRotatef(angleX, 1, 0, 0);
            Gl.glRotatef(angleY, 0, 1, 0);

            Gl.glTranslatef(eyex, eyey, eyez);

            CenterMouse();

            if (pressedButton == 1) 
            {
                float dispX = -(float)Math.Sin(Helper.DegreeToRad((double)angleY)) * forwardSpeed;
                float dispY =(float)Math.Cos(Helper.DegreeToRad((double)angleY)) * forwardSpeed;
                if (dispX > 0)
                {
                    dispX = dispX + 0.1f;
                }
                else
                    dispX = dispX - 0.1f;
                if (dispY > 0)
                {
                    dispY = dispY + 0.1f;
                }
                else
                    dispY = dispY - 0.1f;


                float newEyeX = eyex + dispX;
                float newEyeZ = eyez + dispY;

                if (!Collision.CheckCollision(new Point3D(-newEyeX, -newEyeZ, 0)))
                {
                    eyex = newEyeX;
                    eyez = newEyeZ;
                }
                else
                    if (!Collision.CheckCollision(new Point3D(-newEyeX, -eyez, 0)))
                    {
                        eyex = newEyeX; 
                    }
                    else
                        if (!Collision.CheckCollision(new Point3D(-eyex, -newEyeZ, 0)))
                        {
                            eyez = newEyeZ;
                        }

            }
            else if(movbck==1)
            {
                    float dispX = (float)Math.Sin(Helper.DegreeToRad((double)angleY)) * forwardSpeed;
                    float dispY = -(float)Math.Cos(Helper.DegreeToRad((double)angleY)) * forwardSpeed;
                    if (dispX > 0)
                    {
                        dispX = dispX + 0.1f;
                    }
                    else
                        dispX = dispX - 0.1f;
                    if (dispY > 0)
                    {
                        dispY = dispY + 0.1f;
                    }
                    else
                        dispY = dispY - 0.1f;


                    float newEyeX = eyex + dispX;
                    float newEyeZ = eyez + dispY;

                    if (!Collision.CheckCollision(new Point3D(-newEyeX, -newEyeZ, 0)))
                    {
                        eyex = newEyeX;
                        eyez = newEyeZ;
                    }
                    else
                    if (!Collision.CheckCollision(new Point3D(-newEyeX, -eyez, 0)))
                    {
                        eyex = newEyeX;
                    }
                    else
                    if (!Collision.CheckCollision(new Point3D(-eyex, -newEyeZ, 0)))
                    {
                        eyez = newEyeZ;
                    }
                
            }
        }
    }
}
