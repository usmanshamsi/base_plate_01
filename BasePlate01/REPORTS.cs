using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasePlate01
{
    public static class Reports
    {
        public static StringBuilder Inputs;
        public static StringBuilder Calculations;
        public static StringBuilder Outputs;
        public static StringBuilder Warnings;


        public static void ClearAll()
        {
            Inputs = new StringBuilder();
            Calculations = new StringBuilder();
            Outputs = new StringBuilder();
            Warnings = new StringBuilder();
        }


        public static void appendInputs(String parameter, double value, String units)
        {
            Inputs.Append(String.Format("    - {0,-50} = {1,15} {2,-10} \n",
                            parameter,
                            Math.Round(value, 4).ToString(),
                            units));
        }
        public static void appendInputs(String parameter, int value, String units)
        {
            Inputs.Append(String.Format("    - {0,-50} = {1,15} {2,-10} \n",
                            parameter,
                            value.ToString(),
                            units));
        }
        public static void appendInputs(String parameter, double value)
        {
            Inputs.Append(String.Format("    - {0,-50} = {1,15} \n",
                            parameter,
                            Math.Round(value, 4).ToString()));
        }
        public static void appendInputs(String parameter, int value)
        {
            Inputs.Append(String.Format("    - {0,-50} = {1,15} \n",
                            parameter,
                            value.ToString()));
        }


        public static void appendCalculations(String parameter, double value, String units)
        {
            Calculations.Append(String.Format("    - {0,-50} = {1,15} {2,-10} \n",
                            parameter,
                            Math.Round(value, 4).ToString(),
                            units));
        }
        public static void appendCalculations(String parameter, int value, String units)
        {
            Calculations.Append(String.Format("    - {0,-50} = {1,15} {2,-10} \n",
                            parameter,
                            value.ToString(),
                            units));
        }
        public static void appendCalculations(String parameter, double value)
        {
            Calculations.Append(String.Format("    - {0,-50} = {1,15} \n",
                            parameter,
                            Math.Round(value, 4).ToString()));
        }
        public static void appendCalculations(String parameter, int value)
        {
            Calculations.Append(String.Format("    - {0,-50} = {1,15} \n",
                            parameter,
                            value.ToString()));
        }


        public static void appendOutputs(String parameter, double value, String units)
        {
            Outputs.Append(String.Format("    - {0,-50} = {1,15} {2,-10} \n",
                            parameter,
                            Math.Round(value, 4).ToString(),
                            units));
        }
        public static void appendOutputs(String parameter, int value, String units)
        {
            Outputs.Append(String.Format("    - {0,-50} = {1,15} {2,-10} \n",
                            parameter,
                            value.ToString(),
                            units));
        }
        public static void appendOutputs(String parameter, double value)
        {
            Outputs.Append(String.Format("    - {0,-50} = {1,15} \n",
                            parameter,
                            Math.Round(value, 4).ToString()));
        }
        public static void appendOutputs(String parameter, int value)
        {
            Outputs.Append(String.Format("    - {0,-50} = {1,15} \n",
                            parameter,
                            value.ToString()));
        }


    }
}
