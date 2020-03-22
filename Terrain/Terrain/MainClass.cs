using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowEngine;
using Tao.OpenGl;

namespace Terrain
{
    class MainClass
    {
        int index;
        Camera camera = new Camera();
        Terrain1 terr;
        public void CreateObject()
        {
            terr = new Terrain1("grasss.jpg");
            Sprite.Create();
            Collision.GhostMode = false;
            terr.generatenoise();
        }

        public Camera Camera
        {
            get { return camera; }
        }

        public void DrawScene()
        {
            Gl.glCallList(index);
            terr.draw();
            Collision.DrawColissions();
            Sprite.Begin();
            Sprite.DrawText(20, 20, "Press mouse left button to move forward right to move backward", Glut.GLUT_BITMAP_HELVETICA_18);
            Sprite.DrawText(20, 50, "Press escape to exit", Glut.GLUT_BITMAP_HELVETICA_18);
            Sprite.End();
        }
    }
}
