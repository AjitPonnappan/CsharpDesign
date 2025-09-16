using PicoGK;
using System.Numerics;
using System.Reflection;

namespace HeatExchanger
{
    public class ExchangerGyroid : IImplicit
    {
        public ExchangerGyroid(float fUnitSize,
                                float fThickness)
        {
            m_fUnitSize = fUnitSize;
            m_fThickness = fThickness;
        }
        public float fSignedDistance(in Vector3 vec)
        {
            // Scale coordinates to match unit cell size (period becomes m_fUnitSize)
            float k = 2.0f * float.Pi / m_fUnitSize;
            float sx = k * vec.X;
            float sy = k * vec.Y;
            float sz = k * vec.Z;

            // Gyroid implicit function: sin(x)*cos(y) + sin(y)*cos(z) + sin(z)*cos(x)
            float f = float.Sin(sx) * float.Cos(sy) +
                        float.Sin(sy) * float.Cos(sz) +
                        float.Sin(sz) * float.Cos(sx);

            // SDF: distance from surface with thickness m_fThickness
            return float.Abs(f) - (m_fThickness / 2.0f);
        }

        float m_fUnitSize;
        float m_fThickness;
    }

    public class ExchangerShell
    {
        public ExchangerShell(float fLength,
                                float fWidth, float fHeight)
        {
            m_fLength = fLength;
            m_fWidth = fWidth;
            m_fHeight = fHeight;
        }

        public BBox3 ShellBox()
        {
            BBox3 m_oBox = new(new(0, 0, 0), new(m_fLength, m_fWidth, m_fHeight));
            return m_oBox;
        }
        public BBox3 ShellBoxCut()
        {
            BBox3 m_oBox = new(new(m_fLength, m_fWidth, m_fHeight), new(100, 100, 100));
            return m_oBox;
        }

        float m_fLength;
        float m_fWidth;
        float m_fHeight;
    }

    public class tube
    {
        public tube(Vector3 posStart, Vector3 posEnd, float fDiameter)
        {
            m_fDiameter = fDiameter;
            m_PosStart = posStart;
            m_PosEnd = posEnd;
        }

        public Lattice createTube()
        {
            Lattice m_pipe = new();

            m_pipe.AddBeam(
                m_PosStart,
                m_PosEnd,
                m_fDiameter, m_fDiameter,
                true);
            return m_pipe;
        }
        float m_fDiameter;
        Vector3 m_PosStart;
        Vector3 m_PosEnd;
    }

    public static class Demo
    {
        public static void Run()
        {
            //Create Shell
            ExchangerShell oSize = new(60, 40, 40);
            Mesh meshbox = Utils.mshCreateCube(oSize.ShellBox());
            Voxels voxInbox = new(meshbox);
            //shell thickness 4 mm
            Voxels voxOutbox = voxInbox.voxOffset(4);
            Voxels voxShell = voxOutbox - voxInbox;

            //Add Inlet and outlet to shell
            tube inlet1 = new(new Vector3(0, 10, 20), new Vector3(-10, 10, 20), 4);
            Voxels voxInletID = new(inlet1.createTube());
            Voxels voxInletOD = voxInletID.voxOffset(2);
            Voxels voxInlet = voxInletOD - voxInletID;

            //Fill Gyroid in shell
            ExchangerGyroid oGyroid = new(8, 1f);
            voxInbox.IntersectImplicit(oGyroid);
            Voxels VoxExchanger = voxShell + voxInbox + voxInlet;

            //Create a cross section
            ExchangerShell oSize2 = new(-10, 25, -10);
            Mesh meshbox2 = Utils.mshCreateCube(oSize2.ShellBoxCut());
            Voxels voxBoolsubtract = new(meshbox2);

            Voxels CrossSection = VoxExchanger - voxBoolsubtract;//Bool subtract to view cross section

            //Visualization
            //Library.oViewer().SetGroupMaterial(1, "fb96FF33", 0.9f, 0.2f);
            Library.oViewer().Add(CrossSection, 1);
        }
    }
}
