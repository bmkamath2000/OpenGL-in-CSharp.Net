using System;
using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using ShadowEngine.OpenGL;
using Tao.OpenGl;

namespace Plaza
{
    public class Flag
    {
        float[, ,] points = new float[47, 10, 3];	// The Array For The Points On The Grid Of Our "Wave"
        int wiggle_count;		                    // Counter Used To Control How Fast Flag Waves
        float hold;					                // Temporarily Holds A Floating Point Value     
        Vector3 screenPos;
        int textureFlag;
        int listA;
        float width;

        public Flag(Vector3 screenPos, string textureFlag)
        {
            this.screenPos = screenPos;
            this.textureFlag = ContentManager.GetTextureByName(textureFlag);
        }

        public void Create()
        {
            for (int x = 0; x < 47; x++)
            {
                // Loop Through The Y Plane
                for (int y = 0; y < 10; y++)
                {
                    // Apply The Wave To Our Mesh
                    points[x, y, 0] = (float)((x / 5.0f) - 0.1f);
                    points[x, y, 1] = (float)((y / 1.125f) - 0.1f);
                    points[x, y, 2] = (float)(Math.Sin(Helper.DegreeToRad(((x / 5.0f) * 40.0f)) * 2.0f));
                }
            }

            width = Math.Abs(points[0, 0, 0] - points[46, 0, 0]) * 0.4f;  

            listA = Gl.glGenLists(1);
            Gl.glNewList(listA, Gl.GL_COMPILE);

            Glu.GLUquadric quadratic = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);

            
            Glu.gluCylinder(quadratic, 0.1f, 0.1f, screenPos.Y + 3, 32, 32);
            Gl.glEndList(); 

        }

        public void Update()
        {
            if (wiggle_count == 1)					// Used To Slow Down The Wave (Every 2nd Frame Only)
            {
                for (int y = 0; y < 10; y++)			// Loop Through The Y Plane
                {
                    hold = points[0, y, 2];			// Store Current Value One Left Side Of Wave
                    for (int x = 0; x < 45; x++)		// Loop Through The X Plane
                    {
                        // Current Wave Value Equals Value To The Right
                        points[x, y, 2] = points[x + 1, y, 2];
                    }
                    points[44, y, 2] = hold;			// Last Value Becomes The Far Left Stored Value
                    points[45, y, 2] = (points[44, y, 2] + points[46, y, 2]) / 2;
                }

                wiggle_count = 0;				// Set Counter Back To Zero
            }
            wiggle_count++;						// Increase The Counter
        }

        public void Draw()
        {        
            Update(); 
            int x, y;						                // Loop Variables
            float float_x, float_y, float_xb, float_yb;		// Used To Break The Flag Into Tiny Quads   

            Gl.glPushMatrix();
            Gl.glDisable(Gl.GL_LIGHTING);

            Gl.glTranslatef(screenPos.X, screenPos.Y, screenPos.Z);			// Translate 17 Units Into The Screen
            Gl.glScalef(0.4f, 0.30f, 0.4f);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureFlag);		// Select Our Texture

            Gl.glBegin(Gl.GL_QUADS);
            for (x = 0; x < 46; x++)				// Loop Through The X Plane 0-44 (45 Points)
            {
                for (y = 0; y < 9; y++)			// Loop Through The Y Plane 0-44 (45 Points)
                {
                    float_x = (float)x / 46.0f;		// Create A Floating Point X Value
                    float_y = (float)y / 9.0f;		// Create A Floating Point Y Value
                    float_xb = (float)(x + 1) / 46.0f;		// Create A Floating Point Y Value+0.0227f
                    float_yb = (float)(y + 1) / 9.0f;		// Create A Floating Point Y Value+0.0227f


                    Gl.glTexCoord2f(float_x, float_y);	// First Texture Coordinate (Bottom Left)
                    Gl.glVertex3f(points[x, y, 0], points[x, y, 1], points[x, y, 2]);

                    Gl.glTexCoord2f(float_x, float_yb);	// Second Texture Coordinate (Top Left)
                    Gl.glVertex3f(points[x, y + 1, 0], points[x, y + 1, 1], points[x, y + 1, 2]);

                    Gl.glTexCoord2f(float_xb, float_yb);	// Third Texture Coordinate (Top Right)
                    Gl.glVertex3f(points[x + 1, y + 1, 0], points[x + 1, y + 1, 1], points[x + 1, y + 1, 2]);

                    Gl.glTexCoord2f(float_xb, float_y);	// Fourth Texture Coordinate (Bottom Right)
                    Gl.glVertex3f(points[x + 1, y, 0], points[x + 1, y, 1], points[x + 1, y, 2]);
                }
            }
            Gl.glEnd();						// Done Drawing Our Quads

            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glPushMatrix();
            Gl.glColor3ub(102, 0, 0); 
            Gl.glTranslatef(screenPos.X + width, 0, screenPos.Z);
            Gl.glRotatef(-90, 1, 0, 0);
            Gl.glCallList(listA);
            Gl.glColor3f(1, 1, 1);
            Gl.glPopMatrix(); 
        }
    }
}
