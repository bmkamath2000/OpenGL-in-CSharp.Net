using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using ShadowEngine.OpenGL;
 
namespace Plaza
{

    unsafe class Fountain
    {
        const float PI=3.14152653597689786f;
     const float RandomFactor=2.0f;
     const int ESCAPE=27;
     const int TEXTID = 3;
protected int RAND_MAX = 65568;

protected int i;
protected int flag=0,f=2;
protected int vflag=0;
protected float xt=0.0f,yt=0.0f,zt=0.0f;
protected float xangle=0.0f,yangle=0.0f,zangle=0.0f;
protected float[] X;    // size was 3
protected int ListNum;            //The number of the diplay list

protected float OuterRadius = 2.4f;	//reservoir
protected float InnerRadius = 2.0f;
protected int NumOfVerticesStone = 6;	// reservoir shape
protected float StoneHeight = 0.5f;
protected float WaterHeight = 0.45f;

CDrop[] FountainDrops;
SVertex[] FountainVertices;
protected static int Steps = 4;              //a fountain has several steps, each with its own height
protected static int RaysPerStep =8;
protected static int DropsPerRay = 80;
protected int DropsComplete = 2560;//Step * RaysPerStep * DropsPerRay;
protected float AngleOfDeepestStep = 80;
protected float AccFactor = 0.011f;

        Random r=new Random();
/*
void *fonts[]=
{
GLUT_BITMAP_9_BY_15,
GLUT_BITMAP_TIMES_ROMAN_10,
GLUT_BITMAP_TIMES_ROMAN_24
};

void output(int x, int y, char *string,void *font)
{
  int len, i;

  glRasterPos2f(x, y);
  len = (int) strlen(string);
  for (i = 0; i < len; i++) {
    glutBitmapCharacter(font, string[i]);
  }
}
        */



///////////////////////////////fountain///////////////////////////////////
        

// Creating reservoir boundary
public void CreateList()
{
	SVertex[] Vertices = new SVertex[NumOfVerticesStone*3];  //allocate mem for the required vertices
	ListNum = Gl.glGenLists(1);
  for (int i = 0; i<NumOfVerticesStone; i++)
	{
		Vertices[i].x = (float)Math.Cos(2.0 * PI / NumOfVerticesStone * i) * OuterRadius;
		Vertices[i].y = StoneHeight;  //Top
		Vertices[i].z = (float)Math.Sin(2.0 * PI / NumOfVerticesStone * i) * OuterRadius;
	}
  for (i = 0; i<NumOfVerticesStone; i++)
	{
		Vertices[i + NumOfVerticesStone*1].x = (float)Math.Cos(2.0 * PI / NumOfVerticesStone * i) * InnerRadius;
		Vertices[i + NumOfVerticesStone*1].y = StoneHeight;  //Top
		Vertices[i + NumOfVerticesStone*1].z = (float)Math.Sin(2.0 * PI / NumOfVerticesStone * i) * InnerRadius;
	}
  for (i = 0; i<NumOfVerticesStone; i++)
	{
		Vertices[i + NumOfVerticesStone*2].x = (float)Math.Cos(2.0 * PI / NumOfVerticesStone * i) * OuterRadius;
		Vertices[i + NumOfVerticesStone*2].y = 0.0f;  //Bottom
		Vertices[i + NumOfVerticesStone*2].z = (float)Math.Sin(2.0 * PI / NumOfVerticesStone * i) * OuterRadius;
	}


	Gl.glNewList(ListNum, Gl.GL_COMPILE);

		Gl.glBegin(Gl.GL_QUADS);
		//ground quad:
		Gl.glColor3ub(0,105,0);
		Gl.glVertex3f(-OuterRadius*10.0f,0.0f,OuterRadius*10.0f);
		Gl.glVertex3f(-OuterRadius*10.0f,0.0f,-OuterRadius*10.0f);
		Gl.glVertex3f(OuterRadius*10.0f,0.0f,-OuterRadius*10.0f);
		Gl.glVertex3f(OuterRadius*10.0f,0.0f,OuterRadius*10.0f);
		//stone:
		for (int j = 1; j < 3; j++)
		{
			if (j == 1) Gl.glColor3f(1.3f,0.5f,1.2f);
			if (j == 2) Gl.glColor3f(0.4f,0.2f,0.1f);
			for (i = 0; i<NumOfVerticesStone-1; i++)
			{
				Gl.glVertex3d(Vertices[i+NumOfVerticesStone*j].x,Vertices[i+NumOfVerticesStone*j].y,Vertices[i+NumOfVerticesStone*j].z);
				Gl.glVertex3d(Vertices[i].x,Vertices[i].y,Vertices[i].z);
				Gl.glVertex3d(Vertices[i+1].x,Vertices[i+1].y,Vertices[i+1].z);
				Gl.glVertex3d(Vertices[i+NumOfVerticesStone*j+1].x,Vertices[i+NumOfVerticesStone*j+1].y,Vertices[i+NumOfVerticesStone*j+1].z);
			}
			Gl.glVertex3d(Vertices[i+NumOfVerticesStone*j].x,Vertices[i+NumOfVerticesStone*j].y,Vertices[i+NumOfVerticesStone*j].z);
			Gl.glVertex3d(Vertices[i].x,Vertices[i].y,Vertices[i].z);
	        Gl.glVertex3d(Vertices[0].x,Vertices[0].y,Vertices[0].z);
			Gl.glVertex3d(Vertices[NumOfVerticesStone*j].x,Vertices[NumOfVerticesStone*j].y,Vertices[NumOfVerticesStone*j].z);
		}

		Gl.glEnd();

	    //The "water":
		Gl.glTranslatef(0.0f,WaterHeight - StoneHeight, 0.0f);
	    Gl.glBegin(Gl.GL_POLYGON);
        for (i = 0; i<NumOfVerticesStone; i++)
		{


			Gl.glVertex3d(Vertices[i+NumOfVerticesStone].x,Vertices[i+NumOfVerticesStone].y,Vertices[i+NumOfVerticesStone].z);
			int m1,n1,p1;
	         m1=r.Next()%255;
             n1=r.Next()%255;
             p1=r.Next()%255;

	         Gl.glColor3i(m1,n1,p1);

		//	glColor3f(1.0,1.0,1.0);
		}

		Gl.glEnd();
		Gl.glEndList();
}

float GetRandomFloat(float range)
{
	return (float)r.Next() / (float)RAND_MAX * range * RandomFactor;
}

public void InitFountain()
{
	//This function needn't be and isn't speed optimized
    double range = (double) float.MaxValue - (double) float.MinValue;
double sample = r.NextDouble();
double scaled = (sample * range) + float.MinValue;
float f = (float) scaled;
	FountainDrops=new CDrop[DropsComplete];
	FountainVertices = new SVertex [ (int)DropsComplete ];
	SVertex NewSpeed=new SVertex();
	float DropAccFactor; //different from AccFactor because of the random change
	float TimeNeeded;
	float StepAngle; //Angle, which the ray gets out of the fountain with
	float RayAngle;	//Angle you see when you look down on the fountain
	int i,j,k;
	for (k = 0; k <Steps; k++)
	{
		for (j = 0; j < RaysPerStep; j++)
		{
			for (i = 0; i < DropsPerRay; i++)
			{
                CDrop temp = new CDrop();
                FountainDrops[i + j * DropsPerRay + k * DropsPerRay * RaysPerStep] = temp ;
				DropAccFactor = AccFactor + GetRandomFloat(0.0005f);
                StepAngle = (float)(AngleOfDeepestStep + (90.0 - AngleOfDeepestStep) * (float)(k) / (Steps - 1) + GetRandomFloat(0.2f + 0.8f * (Steps - k - 1) / (Steps - 1)));
				//This is the speed caused by the step:
				NewSpeed.x = Math.Cos ( StepAngle * PI / 180.0f) * (0.2f+0.04f*k);
				NewSpeed.y = Math.Sin ( StepAngle * PI / 180.0f) * (0.2f+0.04f*k);
				//This is the speed caused by the ray:

				RayAngle = (float)j / (float)RaysPerStep * 360.0f;
				//for the next computations "NewSpeed.x" is the radius. Care! Dont swap the two
				//lines, because the second one changes NewSpeed.x!
				NewSpeed.z = NewSpeed.x * Math.Sin ((double) (RayAngle * PI /180.0f));
				NewSpeed.x = NewSpeed.x * Math.Cos ( RayAngle * PI /180.0);

				//Calculate how many steps are required, that a drop comes out and falls down again
				TimeNeeded = (float)NewSpeed.y/ DropAccFactor;
				FountainDrops[i+j*DropsPerRay+k*DropsPerRay*RaysPerStep].SetConstantSpeed ( NewSpeed );
				FountainDrops[i+j*DropsPerRay+k*DropsPerRay*RaysPerStep].SetAccFactor (DropAccFactor);
				FountainDrops[i+j*DropsPerRay+k*DropsPerRay*RaysPerStep].SetTime(TimeNeeded * i / DropsPerRay);
			}
		}
	}


	//Tell OGL that we'll use the vertex array function
/*	Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
	//Pass the data position
	Gl.glVertexPointer(	3,			//x,y,z-components
						Gl.GL_FLOAT,	//data type of SVertex
					    0,			//the vertices are tightly packed
	FountainVertices[0].getPtr());
    Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);*/	// End Drawing Vertices
}

public void randcolor()
{
	int a,b,c;
	a=((int)(r.NextDouble()*200))%101;
	b=((int)(r.NextDouble()*200))%101;
	c=((int)(r.NextDouble()*200))%101;
	X[0]=(float)a/100.0f;
	X[1]=(float)b/100.0f;
	X[2]=(float)c/100.0f;
	//glColor3f(A,B,C);
}

public unsafe void DrawFountain()
{
if(flag==0)
	Gl.glColor3f(1.0f,1.0f,1.0f);
else if(flag==1)
    Gl.glColor3fv(X);
	//randcolor();
else if(flag==2)
    Gl.glColor3f(0.0f,1.0f,0.0f);
else
    Gl.glColor3f(0.0f,1.0f,1.0f);

	for (int i = 0; i < DropsComplete; i++)
	{
		FountainDrops[i].GetNewPosition(FountainVertices[i].getSVPtr());
	}
  /*  Gl.glPushMatrix();
    Gl.glTranslated(0, -5, 0);
    Gl.glScaled(20, 20, 20);
	Gl.glDrawArrays(   Gl.GL_POINTS,	0,DropsComplete);
    Gl.glPopMatrix();*/
    Gl.glPushMatrix();
   // Gl.glTranslated(0,-5,0);
    Gl.glScaled(20, 20, 20);
    Gl.glBegin(Gl.GL_POINTS);
    for(int i=0;i<DropsComplete;i++)
    {
        Gl.glVertex3d(FountainVertices[i].x,FountainVertices[i].y,FountainVertices[i].z);
       // Console.WriteLine(" "+FountainVertices[i].x+","+ FountainVertices[i].y+","+ FountainVertices[i].z+"\n");
    }
    Gl.glEnd();
    Gl.glPopMatrix();
//Glut.glutPostRedisplay();
}


    }
}
