using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WELDS
{
    public static class WeldCapacity
    {
        /* Units: kips, inch */

        public static double filletWeldCapacity(double weldLength, double weldThickness, double forceAngle, int electrodeNumber)
        {
            const double PHI = 0.75;

            double w = weldThickness;
            double L = weldLength;

            double Fw;
            double theta = Math.PI / 180 * forceAngle;
            double sine = Math.Sin(theta);
            double sin15 = Math.Pow(sine, 1.5);
            Fw = 0.60 * electrodeNumber * (1.0 + 0.5 * sin15);

            double Rn = 0.707 * w * L * Fw;
            double phiRn = PHI * Rn;

            return phiRn;
        }
    }
}
