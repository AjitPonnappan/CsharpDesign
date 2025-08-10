using System.Numerics;
using PicoGK;

namespace test
{
    public class App
    {
        public static void run()
        {
            Lattice lat = new();
            lat.AddBeam(
                new Vector3(0, 0, 0),
                new Vector3(50, 0, 0),
                5, 20,
                true);

            Voxels vox = new(lat);
            Library.oViewer().Add(vox);
        }

        public static void geometry()
        {
            float fhubdia = 5f;
            float fmaxdia = 10f;
            float fheight = 5f;
            Lattice lat = new();
            lat.AddBeam(
                new Vector3(0, 0, 0),
                new Vector3(0, 0, fheight),
                fhubdia, fmaxdia,
                false);

            Lattice latdonut = new();
            Vector3 vecPos = Vector3.Zero;
            for (float fAngle = 0; fAngle < float.Pi * 2; fAngle += 0.01f)
            {
                vecPos.X = fmaxdia * (float.Cos(fAngle));
                vecPos.Y = fmaxdia * (float.Sin(fAngle));
                latdonut.AddSphere(vecPos, fmaxdia-fhubdia);
            }

            Voxels vox = new(lat);
            Voxels voxSubtract = new(latdonut);
            vox.BoolSubtract(voxSubtract);
            Library.oViewer().Add(vox);
        }

        public static void donut()
        {
            Lattice latdonut = new();
            Vector3 vecPos = Vector3.Zero;
            for (float fAngle = 0; fAngle < float.Pi * 2; fAngle += 0.01f)
            {
                vecPos.X = 5 * (float.Cos(fAngle));
                vecPos.Y = 5 * (float.Sin(fAngle));
                latdonut.AddSphere(vecPos, 5);
            }
            
            Voxels vox = new(latdonut);
            Library.oViewer().Add(vox);
        }
    }
}