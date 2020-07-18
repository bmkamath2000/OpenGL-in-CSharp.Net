using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaza
{
    /*public struct SVertex
    {
       public double x, y, z;
    };*/
    public unsafe struct SVertex
    { //structure for a point
        public double x;
        public double y;
        public double z;

        public unsafe SVertex(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public IntPtr getPtr()
        {
            unsafe
            {
                fixed (double* ptr = &(this.x))
                {
                    return (IntPtr)ptr;
                }
            }
        }
        public unsafe SVertex* getSVPtr()
        {
            unsafe
            {
                fixed (double * ptr = &(this.x))
                {
                    SVertex* svptr = (SVertex*)ptr;
                    return svptr;
                }
            }
        }
    }
    class CDrop
    {
        private
            float time;
        private SVertex ConstantSpeed;
        private float AccFactor;
        protected float WaterHeight = 0.45f;
        public
            void SetConstantSpeed(SVertex NewSpeed)
        {
            ConstantSpeed = NewSpeed;
        }
        public void SetAccFactor(float NewAccFactor)
        {
            AccFactor = NewAccFactor;
        }
        public void SetTime(float NewTime)
        {
            time = NewTime;
        }
        public unsafe void GetNewPosition(SVertex * PositionVertex)  //increments time, gets the new position
{
    SVertex Position;
	time += 0.15f;
	Position.x = ConstantSpeed.x * time;
	Position.y = ConstantSpeed.y * time - AccFactor * time *time;
	Position.z = ConstantSpeed.z * time;
            PositionVertex->x = Position.x;
	PositionVertex->y = Position.y + WaterHeight;
	PositionVertex->z = Position.z;
	if (Position.y < 0.0)
	{
		time = time - (int)(time);
		if (time > 0.0) time -= 1.0f;
	}

}
    }
}
   
