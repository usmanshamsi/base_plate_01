using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasePlate01
{
    public static class Inputs
    {
        public static String title;

        /* column details*/
			public static double depthOfColumn;
            public static double widthOfColumn;
            public static double columnFlangeThickness;
            public static double columnWebThickness;

		/* applied forces*/
            public static double axialForce, momentAboutX, momentAboutY, shearAlongX, shearAlongY;

		/* weld data*/
            public static int electrodeNumber;
            public static double weldThickness_flange;
            public static double weldThickness_web;

		/* bolt data*/
            public static double boltOffset;
            public static int numberOfBoltsAlongX;
            public static int numberOfBoltsAlongY;
            public static double areaOfBolt;
            public static double nominalTensileStressOfBolt;
            public static double nominalShearStressOfBolt;

        /* base plate and padestal data*/
            public static double depthOfBasePlate, widthOfBasePlate, thicknessOfBasePlate;
            public static double FyOfBasePlateMaterial, FuOfBasePlateMaterial;
            public static double depthOfConcretePadestal, widthOfConcretePadestal, fc_concrete;
    }
}
