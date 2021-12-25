using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasePlate01
{
    public partial class frmResults : Form
    {
        public frmResults()
        {
            InitializeComponent();
        }

        private void frmResults_Load(object sender, EventArgs e)
        {
            this.Text = "Results - " + Inputs.title;
            txtOutputs.Text = Reports.Outputs.ToString().Replace("\n", "\r\n");
            txtWarnings.Text = Reports.Warnings.ToString().Replace("\n", "\r\n");
            txtCalculations.Text = Reports.Calculations.ToString().Replace("\n", "\r\n");
        }

        private void bttnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fileName = Inputs.title + " - output.txt";

            //  Show save dialog
            saveFileDialog1.DefaultExt = ".txt";
            saveFileDialog1.Filter = "Text File|*.txt";
            saveFileDialog1.FileName = fileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                
                StringBuilder of = new StringBuilder();
                of.Append("Title: ");
                of.Append(Inputs.title.ToUpper());
                of.Append("\n\n");

                of.Append("*** I N P U T S ***\n");
                of.Append("===================\n\n");
                of.Append(Reports.Inputs.ToString());
                of.Append("\n");

                of.Append("*** C A L C U LA T I O N S ***\n");
                of.Append("==============================\n\n");
                of.Append(Reports.Calculations.ToString());
                of.Append("\n");

                of.Append("*** O U T P U T S ***\n");
                of.Append("=====================\n\n");
                of.Append(Reports.Outputs.ToString());
                of.Append("\n");

                of.Append("**** W A R N IN G S ***\n");
                of.Append("=======================\n\n");
                of.Append(Reports.Warnings.ToString());
                of.Append("\n");

                System.IO.File.WriteAllText(fileName, of.ToString().Replace("\n", "\r\n"));
            }
            
        }
    }
}
