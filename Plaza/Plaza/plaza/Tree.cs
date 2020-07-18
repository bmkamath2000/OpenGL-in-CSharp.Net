using System;
using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using Tao.OpenGl;

namespace Plaza
{
    class Tree
    {
        
        bool flag = true;
        public void makecylinder(float height,float Base)
        {           
            Glu.GLUquadric obj = Glu.gluNewQuadric();
            Gl.glColor3f(0.64f, 0.16f, 0.16f);
            Gl.glPushMatrix();
            Gl.glRotatef(-90f, 1.0f, 0.0f, 0.0f);
            Glu.gluCylinder(obj, Base, Base - (0.2 * Base), height, 20, 20);
            Gl.glPopMatrix();     
        }
        public void maketree(float height,float Base)
        {
            
            Gl.glPushMatrix();

         

            float angle;
            makecylinder(height, Base);
            Gl.glTranslatef(0.0f, height,0.0f);
            height -=height*0.2f; 
            Base -=Base*0.3f;
            for(int a= 0; a<3; a++)
            {
                Random r=new Random() ;

                angle = r.Next()%50+20;
                if(angle >48)
                angle = -(r.Next()%50+20);
                if (height > 1)
                {
                    Gl.glPushMatrix();
                    if (flag)
                        Gl.glRotatef(angle, 1f, 0.0f, 1f);
                    else
                        Gl.glRotatef(angle, 0f, 1.0f, 1f);
                    flag = !flag;
                    maketree(height, Base); //recursive call
                    Gl.glPopMatrix();
                    
                }
                else
                {
                    Gl.glColor3f(0.0f, 1.0f / a, 0.0f);
                    Glut.glutSolidSphere(0.2, 10, 10);// for fruits.
                  
                }
            }
            //Glut.glutSwapBuffers();
            Gl.glPopMatrix();
        }
    }
}
