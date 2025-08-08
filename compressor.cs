namespace compressor
{
    public class impeller
    {
        public impeller(uint nBladenumber, float fPrRatio, float fInitPr, float fInitTemp, float fFlowrate)
        {
            m_nBladenumber = nBladenumber;
            m_fPressureRatio = fPrRatio;
            m_fInitPressure = fInitPr;
            m_fInitTemperature = fInitTemp;
            m_fMassflow = fFlowrate;

        }
        public void velocitytraingle(inlet impeller.m_oInducer, outlet impeller.m_oExducer)
        {
            
        }
        uint m_nBladenumber;
        float m_fPressureRatio;
        float m_fInitPressure;
        float m_fInitTemperature;
        float m_fMassflow;
        inlet m_oInducer = new();
        outlet m_oExducer = new();
    }

    public class inlet
    {
        float m_fhubdia;
        float m_ftipdia;
        float m_fhubtipratio;
        float m_fBladeangle;
        float m_fInletaxialvel;

    }

    public class outlet
    {
        float m_fdia;
        float m_fBladebacksweepangle;
        float m_fBladeheight;
        float m_fBladewidth;
        float m_fSlipfactor;
    }

    public class performanceparams
    {

    }

    public shape oCreateGeometry
        {
        public oCreategeometry(impeller m_oCentrifugal)
        {

        }
    }

}