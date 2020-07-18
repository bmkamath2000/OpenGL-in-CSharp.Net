using System;
using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using Tao.OpenGl;

namespace Plaza
{
    class Terrain
    {
        int noiseHeight = 128, noiseWidth = 128;
        double[,] noise= new double [128,128];
        float dx = 1.0f, dy = 1.0f;
        int textureFlag;
        
        double[,] noisearr = new double[128,128];
        public Terrain(string textureFlag)
        {
            this.textureFlag = ContentManager.GetTextureByName(textureFlag);
        }

        public void generatenoise()
        {
            for (int y = 0; y < noiseHeight; y++)
                for (int x = 0; x < noiseWidth; x++)
                {
                    Random r = new Random();
                   
                    noise[y,x] = (double)(r.Next() % 32768) / 32768.0f;
                }
        }//generatenoise close

        double smoothNoise(double x, double y)
        {
            //get fractional part of x and y
            double fractX = x - (int)(x);
            double fractY = y - (int)(y);

            //wrap around
            int x1 = (int)(((x) + noiseWidth) % noiseWidth);
            int y1 = (int)(((y) + noiseHeight) % noiseHeight);

            //neighbor values
            int x2 = (int)((x1 + noiseWidth - 1) % noiseWidth);
            int y2 = (int)((y1 + noiseHeight - 1) % noiseHeight);

            //smooth the noise with bilinear interpolation
            double value = 0.0;
            value += fractX * fractY * noise[y1,x1];
            value += (1 - fractX) * fractY * noise[y1,x2];
            value += fractX * (1 - fractY) * noise[y2,x1];
            value += (1 - fractX) * (1 - fractY) * noise[y2,x2];

            return value;
        }//smoothnoise close

        double turbulence(double x, double y, double size)
        {
            double value = 0.0, initialSize = size;
            double maximum = 0.0;
            while (size >= 1)
            {
                maximum += size;
                value += smoothNoise(x / size, y / size) * size;
                size /= 2.0;
            }

            return (value / maximum);
        }//turbulence close

        
        

        public void draw()
        {
            
            int i, j;
            double xyValue, value;
            Double xPeriod = 5.0f; //defines repetition of marble lines in x direction
            Double yPeriod = 10.0f; //defines repetition of marble lines in y direction

            //turbPower = 0 ==> it becomes a normal sine pattern
            Double turbPower = 50; //makes twists
            Double turbSize = 128; //initial size of the turbulence

            
            for ( i = 0; i < 128; i += (int)dx)
            {
                for ( j = 0; j < 128; j += (int)dy)
                {
                    xyValue = ((i * xPeriod) / noiseWidth) + ((j * yPeriod) / noiseHeight) + (turbPower * turbulence(i, j, turbSize));
                    value =(2*turbulence(i, j, turbSize)*(1.0 + Math.Sin(xyValue * 3.14159)));
                    noisearr[i, j] = value;

                }
            }

            // to do use a formula here to create a texture
            
            Gl.glPushMatrix();
            Gl.glDisable(Gl.GL_LIGHTING);

////            Gl.glTranslatef(screenPos.X, screenPos.Y, screenPos.Z);			// Translate 17 Units Into The Screen
////            Gl.glScalef(0.4f, 0.30f, 0.4f);
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            //Gl.glShadeModel(Gl.GL_SMOOTH);
            //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_GENERATE_MIPMAP, Gl.GL_TRUE);


            //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            //Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureFlag);		// Select Our Texture
            
            for (i = 0; i < 127; i += (int)dx)
            {
                for (j = 0; j < 127; j += (int)dy)
                {
                    float float_x = i/127.0f;
                    float float_y=j/127.0f;
                    float float_xb = (i + 1) / 127.0f;
                    float float_yb = (j + 1) / 127.0f;

                    Gl.glBegin(Gl.GL_QUADS);
                    


                    //Gl.glColor3f(0, 1, 0);
                    Gl.glTexCoord2d(float_x, float_y);
                    Gl.glVertex3d(i / 10.0f, 1+noisearr[(int)i, (int)j], j / 10.0f);
                    Gl.glTexCoord2d(float_xb, float_y);
                    Gl.glVertex3d((i + dx) / 10.0f, 1+noisearr[(int)(i + dx), (int)j], j / 10.0f);
                    Gl.glTexCoord2d(float_xb, float_yb);
                    Gl.glVertex3d((i + dx) / 10.0f, 1+noisearr[(int)(i + dx), (int)(j + dy)], (j + dy) / 10.0f);
                    Gl.glTexCoord2d(float_x, float_yb);
                    Gl.glVertex3d(i / 10.0f, 1+noisearr[(int)i, (int)(j + dy)], (j + dy) / 10.0f);
                    
                    Gl.glEnd();
                }
            }//for close
            
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

        }//draw close

    }//terrain class close

}//namespace close


