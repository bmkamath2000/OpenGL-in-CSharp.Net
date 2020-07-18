using System.Text;
using ShadowEngine;
using ShadowEngine.ContentLoading;
using Tao.OpenGl; 

namespace Plaza
{
    class Object
    {
        ModelContainer N;

        public void create()
        {

            N = ContentManager.GetModelByName("Sofa 3 seat.3DS");
            N.CreateDisplayList();
        }
        public void Draw()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(0f, 0f, 75f);
            Gl.glScalef(0.5f, 0.5f, 0.5f);
            N.DrawWithTextures();
            Gl.glPopMatrix();
        }
    }
}
