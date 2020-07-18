using System;
using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using Tao.OpenGl;

namespace Plaza
{
    class SkyBox
    {
        public void Draw()
        {
            Gl.glPushMatrix();
     
            Gl.glRotatef(90, 0, 1, 0);
          
            int width = 240;
            int height = 200;
            int length = 240;

           
            int x = 10;
            int y = -3;
            int z = 7;

      
            x = x - width / 2;
            y = y - height / 2;
            z = z - length / 2;

            //Front Face
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName("front.bmp"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(-1, 1, 1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width, y, z);
            Gl.glNormal3d(-1, -1, 1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z);
            Gl.glNormal3d(1, -1, 1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x, y + height, z);
            Gl.glNormal3d(1, 1, 1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x, y, z);
            Gl.glEnd();

            //Back face
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName("back.bmp"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(1, 1, -1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x, y, z + length);
            Gl.glNormal3d(1, -1, -1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x, y + height, z + length);
            Gl.glNormal3d(-1, -1, -1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z + length);
            Gl.glNormal3d(-1, 1, -1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y, z + length);
            Gl.glEnd();

            //Top face
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName("top.bmp"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(-1, -1, 1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z);
            Gl.glNormal3d(-1, -1, -1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y + height, z + length);
            Gl.glNormal3d(1, -1, -1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x, y + height, z + length);
            Gl.glNormal3d(1, -1, 1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x, y + height, z);
            Gl.glEnd();

            //Left face
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName("left.bmp"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(1, -1, 1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x, y + height, z);
            Gl.glNormal3d(1, -1, -1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x, y + height, z + length);
            Gl.glNormal3d(1, 1, -1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x, y, z + length);
            Gl.glNormal3d(1, 1, 1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x, y, z);
            Gl.glEnd();

            //Right Face
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName("right.bmp"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(-1, 1, 1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y, z);
            Gl.glNormal3d(-1, 1, -1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width, y, z + length);
            Gl.glNormal3d(-1, -1, -1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z + length);
            Gl.glNormal3d(-1, -1, 1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y + height, z);
            Gl.glEnd();

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName("grasss.jpg"));
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(1, 1, 1);
            Gl.glTexCoord2f(8.0f, 0.0f); Gl.glVertex3d(x, 1, z);
            Gl.glNormal3d(1, 1, -1);
            Gl.glTexCoord2f(8.0f, 8.0f); Gl.glVertex3d(x, 1, z + length);
            Gl.glNormal3d(-1, 1, -1);
            Gl.glTexCoord2f(0.0f, 8.0f); Gl.glVertex3d(x + width, 1, z + length);
            Gl.glNormal3d(-1, 1, 1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, 1, z);
            Gl.glEnd();


            Gl.glPopMatrix(); 
        }
    }
}
