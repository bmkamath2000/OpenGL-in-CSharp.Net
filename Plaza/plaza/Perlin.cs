using System;
using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using Tao.OpenGl;


namespace Plaza
{
    class Perlin
    {
        int i, j;
        int cols = 100, rows = 100;

        float[,] noisearr=new float[100,100];

        public void assign()
        {
            for (i = 0; i < rows-1; i++)
            {
                for (j = 0; j < cols; j++)
               {
                    Random a=new Random();
                    noisearr[i, j] = a.Next(-10,10);
                }
            }
        }

        public void draw()
        {   
            for(i=0;i<100;i++)
            {
                Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
                for(j=0;j<100;j++)
                {
                    Random r = new Random();
                    Gl.glVertex3f(i, j, noisearr[i,j]);
                    Gl.glVertex3f(i, j + 1,noisearr[i,j+1]);
                }
                Gl.glEnd();
            }
        }
    }
}
