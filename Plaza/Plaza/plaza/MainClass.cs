using System;
using System.Collections.Generic;
using System.Text;
using ShadowEngine;
using Tao.OpenGl;


namespace Plaza
{
    public class MainClass
    {
        int index;
        
        Flag flag;
        Camara camara = new Camara();
        Plaza plaza = new Plaza();
        SkyBox box = new SkyBox();
        Tree T = new Tree();
        Object obj = new Object();
        Terrain terr ;
        Fountain f=new Fountain();

        //Perlin P = new Perlin();

        public void CreateObject()
        {
            flag = new Flag(new Vector3(-4, 10, -4), "sunny.jpg");
            terr = new Terrain("grasss.jpg");
            flag.Create();
            plaza.Create();
            plaza.CreateCollisions();
            obj.create();
            f.InitFountain();
            f.CreateList();

            index = Gl.glGenLists(1);
            Gl.glNewList(index, Gl.GL_COMPILE);
            T.maketree(4.0f, 0.2f);
            Gl.glEndList();
            Sprite.Create();  
            Collision.GhostMode = false;
            terr.generatenoise();            
  
        }

        public Camara Camara
        {
            get { return camara; }
        }

        public void DrawScene()
        {
            plaza.Draw();
            box.Draw();
            flag.Draw();
            obj.Draw();
           // Gl.glPushMatrix();
           // for (int i = 50; i < 55; i++)
           //{       
           //         Gl.glTranslated(i, 0, 30);
           //        Gl.glCallList(index);
           // }
           // //Glut.glutSwapBuffers();
           // Gl.glPopMatrix();

            //P.draw();
            terr.draw();


            Collision.DrawColissions();
            Sprite.Begin();
            Sprite.DrawText(20, 20, "Press mouse left button to move forward right to move backward", Glut.GLUT_BITMAP_HELVETICA_18);
            Sprite.DrawText(20, 50, "Press escape to exit", Glut.GLUT_BITMAP_HELVETICA_18); 
            Sprite.End();
            f.DrawFountain();
        }
    }
}
