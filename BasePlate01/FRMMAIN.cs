using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WELDS;

namespace BasePlate01
{
    public partial class frmMain : Form
    {

        #region Form Level Variables

            double resultantShear;
            double resultantShearAngle;

            double lengthOfWeldAroundFlange;
            double lengthOfWeldAroundWeb;

            double tensionInWeldDueToMomentAboutX_PUL;
            double tensionInWeldDueToMomentAboutY_PUL;
            double tensionInWeldDueToAxialTension_PUL;
            double maxTensionInFlangeWeld_PUL;

            double tensionCapacityOfWeld_flange_PUL;
            double capacityRatioOfWeld_flange;

            double shearInWeld_web_PUL;
            double shearCapacityOfWeld_web_PUL;
            double capacityRatioOfWeld_web;
            int totalNumberOfBolts;

            double maximumAxialForceInBolt;
            double maximumShearForceInBolt;
            double shearStressInBolt;
            double capacityRatioOfBoltInShear;
            double capacityRatioOfBoltInTension;
            double modifiedNominalTensileStressOfBolt;

            double basePlateSx;
            double basePlateSy;

            double areaOfBasePlate;
            double areaOfConcretePadestal;
            double maximumBearingStress;

            double allowableBearingStress;
            double capacityRatioInBearing;

            double projectionOfBasePlateAlongX;
            double projectionOfBasePlateAlongY;
            double maximumProjectionOfBasePlate;

            double shearStressInBasePlate;
            double flexuralStressInBasePlate;
            double capacityRatioOfBasePlateInFlexure;
            double capacityRatioOfBasePlateInShear;

            bool stopInput = false;

        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtTitle.Focus();
        }

        private void action_Check(object sender, EventArgs e)
        {
            stopInput = false;
            takeInputs();
            if (stopInput) return;

            performCalculations();

            Reports.ClearAll();

            writeInputs();
            writeCalculations();
            writeOutputs();
            writeWarnings();

            Form resultsForm = new frmResults();
            resultsForm.ShowDialog();


        }

        private void writeWarnings()
        {
            if (capacityRatioOfWeld_flange > 1.0)
                Reports.Warnings.Append("    - Size of Flange Weld is not satisfactory. Revise.\n      Also consider providing full penetration weld in flange\n");
            if (capacityRatioOfWeld_web > 1.0)
                Reports.Warnings.Append(" - Size of Web weld is not satisfactory. Revise.\n");
            if (capacityRatioOfBoltInShear > 1.0)
                Reports.Warnings.Append("    - Bolts are not satisfactory in shear.\n");
            if (capacityRatioOfBoltInTension > 1.0)
                Reports.Warnings.Append("    - Bolts are not satisfactory in tension.\n");
            if (capacityRatioInBearing > 1.0)
                Reports.Warnings.Append("    - Concrete of Padestal fails in bearing.\n      Increase concrete strength or bearing area.\n");
            if (capacityRatioOfBasePlateInShear > 1.0)
                Reports.Warnings.Append("    - Thickness of base plate is not satisfactory in shear.\n");
            if (capacityRatioOfBasePlateInFlexure > 1.0)
                Reports.Warnings.Append("    - Thickness of base plate is not satisfactory in flexure.\n");

            //MessageBox.Show(Reports.Inputs.ToString(),"INPUTS");
            //Console.Clear();
            //Console.Write(Reports.Warnings.ToString());
            //Console.ReadLine();
        }

        private void writeOutputs()
        {
            Reports.appendOutputs("D/C Ratio of Flange Weld", capacityRatioOfWeld_flange);
            Reports.appendOutputs("D/C Ratio of Web Weld", capacityRatioOfWeld_web);
            Reports.appendOutputs("D/C Ratio of bolt in shear", capacityRatioOfBoltInShear);
            Reports.appendOutputs("D/C Ratio of bolt in tension", capacityRatioOfBoltInTension);
            Reports.appendOutputs("D/C Ratio in Bearing", capacityRatioInBearing);
            Reports.appendOutputs("D/C Ratio of Base Plate in Shear", capacityRatioOfBasePlateInShear);
            Reports.appendOutputs("D/C Ratio of Base Plate in Flexure", capacityRatioOfBasePlateInFlexure);

            //MessageBox.Show(Reports.Inputs.ToString(),"INPUTS");
            //Console.Clear();
            //Console.Write(Reports.Outputs.ToString());
            //Console.ReadLine();
        }

        private void writeCalculations()
        {
            /* ECHO CALCULATIONS */
            Reports.Calculations.Append("RESULTANT SHEAR AND SHEAR ANGLE\n");
            Reports.appendCalculations("Resultant Shear", resultantShear, "kip");
            Reports.appendCalculations("Resultant Shear Angle w.r.t to web weld axis", resultantShearAngle, "degree");
            Reports.Calculations.Append("\n");

            /* weld */
            Reports.Calculations.Append("CHECK FOR WELDS SIZES\n");
            Reports.appendCalculations("PHI Factor for Weld", Constants.WELD_PHI);
            Reports.appendCalculations("Length of weld around flange", lengthOfWeldAroundFlange, "inch");
            Reports.appendCalculations("Length of weld around web", lengthOfWeldAroundWeb, "inch");
            Reports.appendCalculations("Tension in weld due to moment about X)", tensionInWeldDueToMomentAboutX_PUL, "kip per inch");
            Reports.appendCalculations("Tension in weld due to moment about Y", tensionInWeldDueToMomentAboutY_PUL, "kip per inch");
            Reports.appendCalculations("Tension in weld due to axial force", tensionInWeldDueToAxialTension_PUL, "kip per inch");
            Reports.appendCalculations("Maximum tension in flange weld", maxTensionInFlangeWeld_PUL, "kip per inch");
            Reports.appendCalculations("Shear Force in weld of web", shearInWeld_web_PUL, "kip per inch");
            Reports.appendCalculations("Tension Capacity of Flange Weld", tensionCapacityOfWeld_flange_PUL ,"kip per inch");
            Reports.appendCalculations("D/C Ratio of Flange Weld", capacityRatioOfWeld_flange);
            Reports.appendCalculations("Tension Capacity of Web Weld", shearCapacityOfWeld_web_PUL ,"kip per inch");
            Reports.appendCalculations("D/C Ratio of Web Weld", capacityRatioOfWeld_web);
            Reports.Calculations.Append("\n");

            /* bolts */
            Reports.Calculations.Append("CHECK FOR BOLTS CAPACITY\n");
            Reports.appendCalculations("PHI Factor for Weld", Constants.BOLT_PHI);
            Reports.appendCalculations("Total number of bolts", totalNumberOfBolts);
            Reports.appendCalculations("Maximum Axial Force in bolt (Tension Positive)", maximumAxialForceInBolt, "kip");
            Reports.appendCalculations("Maximum Shear Force in bolt", maximumShearForceInBolt,"kip");
            Reports.appendCalculations("D/C Ratio of bolt in shear", capacityRatioOfBoltInShear);
            Reports.appendCalculations("Modified Nominal Tensile Strength of bolt", modifiedNominalTensileStressOfBolt,"ksi");
            Reports.appendCalculations("D/C Ratio of bolt in tension", capacityRatioOfBoltInTension);
            Reports.Calculations.Append("\n");

            /* base plate and padestal */
            Reports.Calculations.Append("CHECK FOR BASE PLATE AND PADESTAL\n");
            Reports.appendCalculations("PHI Factor for Weld", Constants.CONCRETE_BEARING_PHI);
            Reports.appendCalculations("Elastic modulus of base plate about X", basePlateSx, "cu.inch");
            Reports.appendCalculations("Elastic modulus of base plate about Y", basePlateSy, "cu.inch");
            Reports.appendCalculations("Area of base plate", areaOfBasePlate, "sq.inch");
            Reports.appendCalculations("Area of concrete padestal", areaOfConcretePadestal,"sq.inch");
            Reports.appendCalculations("Maximum Bearing Stress of base plate", maximumBearingStress,"ksi");
            Reports.appendCalculations("Allowable Bearing Stress", allowableBearingStress, "ksi");
            Reports.appendCalculations("D/C Ratio in Bearing", capacityRatioInBearing);
            Reports.appendCalculations("Maximum projection of base plate", maximumProjectionOfBasePlate, "inch");
            Reports.appendCalculations("Shear Stress in base plate", shearStressInBasePlate, "ksi");
            Reports.appendCalculations("Flexural Stress in base plate", flexuralStressInBasePlate, "ksi");
            Reports.appendCalculations("D/C Ratio of Base Plate in Shear", capacityRatioOfBasePlateInShear);
            Reports.appendCalculations("D/C Ratio of Base Plate in Flexure", capacityRatioOfBasePlateInFlexure);
            Reports.Calculations.Append("\n");

            //MessageBox.Show(Reports.Inputs.ToString(),"INPUTS");
            //Console.Clear();
            //Console.Write(Reports.Calculations.ToString());
            //Console.ReadLine();
        }

        private void writeInputs()
        {
            /* column details*/
            Reports.Inputs.Append("COLUMN DETAILS\n");
            Reports.appendInputs("Depth of Column", Inputs.depthOfColumn, "inch");
            Reports.appendInputs("Width of Column", Inputs.widthOfColumn, "inch");
            Reports.appendInputs("Thickness of Flange of Column", Inputs.columnFlangeThickness, "inch");
            Reports.appendInputs("Thickness of Web of Column", Inputs.columnWebThickness, "inch");
            Reports.Inputs.Append("\n");

            /* applied forces*/
            Reports.Inputs.Append("APPLIED FORCES\n");
            Reports.appendInputs("Axial Force", Inputs.axialForce, "kip");
            Reports.appendInputs("Moment About X Axis", Inputs.momentAboutX / 12.0, "kip-ft");
            Reports.appendInputs("Moment About Y Axis", Inputs.momentAboutY / 12.0, "kip-ft");
            Reports.appendInputs("Shear Along X Axis", Inputs.shearAlongX, "kip");
            Reports.appendInputs("Shear Along Y Axis", Inputs.shearAlongY, "kip");
            Reports.Inputs.Append("\n");

            /* weld data*/
            Reports.Inputs.Append("WELD DATA\n");
            Reports.appendInputs("Electrode Number", Inputs.electrodeNumber);
            Reports.appendInputs("Thickness of Flange Weld", Inputs.weldThickness_flange, "inch");
            Reports.appendInputs("Thickness of Web Weld", Inputs.weldThickness_web, "inch");
            Reports.Inputs.Append("\n");

            /* bolt data*/
            Reports.Inputs.Append("BOLT DATA\n");
            Reports.appendInputs("Bolt Offset from Column", Inputs.boltOffset);
            Reports.appendInputs("Number of Bolts Along X Axis", Inputs.numberOfBoltsAlongX);
            Reports.appendInputs("Number of Bolts Along Y Asix", Inputs.numberOfBoltsAlongY);
            Reports.appendInputs("Area of Bolt", Inputs.areaOfBolt, "sq.inch");
            Reports.appendInputs("Nominal Tensile Stress of Bolt", Inputs.nominalTensileStressOfBolt, "ksi");
            Reports.appendInputs("Nominal Shear Stress of Bolt", Inputs.nominalShearStressOfBolt, "ksi");
            Reports.Inputs.Append("\n");

            /* base plate and padestal data*/
            Reports.Inputs.Append("BASE PLATE DATA\n");
            Reports.appendInputs("Depth of Base Plate", Inputs.depthOfBasePlate, "inch");
            Reports.appendInputs("Width of Base Plate", Inputs.widthOfBasePlate, "inch");
            Reports.appendInputs("Thickness of Base Plate)", Inputs.thicknessOfBasePlate, "inch");
            Reports.appendInputs("Yield Strength of Base Plate", Inputs.FyOfBasePlateMaterial, "ksi");
            Reports.appendInputs("Ultimate Strength of Base Plate", Inputs.FuOfBasePlateMaterial, "ksi");
            Reports.Inputs.Append("\n");

            Reports.Inputs.Append("CONCRETE PADESTAL DATA\n");
            Reports.appendInputs("Depth of Concrete Padestal", Inputs.depthOfConcretePadestal, "inch");
            Reports.appendInputs("Width of Concrete Padestal", Inputs.widthOfConcretePadestal, "inch");
            Reports.appendInputs("f'c of Concrete Padestal", Inputs.fc_concrete, "ksi");
            Reports.Inputs.Append("\n");

            //MessageBox.Show(Reports.Inputs.ToString(),"INPUTS");
            //Console.Write(Reports.Inputs.ToString());
            //Console.ReadLine();
        }

        private void performCalculations()
        {
            
            /* CALCULATIONS */

            //  Resultant of shear X and shear Y
            resultantShear = Math.Sqrt(Math.Pow(Inputs.shearAlongX, 2.0) + Math.Pow(Inputs.shearAlongY, 2.0));

            /* angle of resultant shear with respect to axis of web weld i.e. Y-Axis */
            resultantShearAngle = Math.Atan(Inputs.shearAlongX / Inputs.shearAlongY) * Constants.RADIANS_TO_DEGREES;

            /* check weld capacity*/
            /* Calculate Length Of Weld*/
            lengthOfWeldAroundFlange = 2.0 * Inputs.widthOfColumn - Inputs.columnFlangeThickness;
            lengthOfWeldAroundWeb = 2.0 * (Inputs.depthOfColumn - 2.0 * Inputs.columnFlangeThickness);

            /* Calculate Tension In Flange Weld */
            /* it is assumed that the weld aroundflanges will take care of three forces
                1. Moment about X axis
                2. Moment about Y axis
                3. Axial Tension Force	*/
            //  PUL = Per Unit Length
            tensionInWeldDueToMomentAboutX_PUL = (Inputs.momentAboutX / (Inputs.depthOfColumn - Inputs.columnFlangeThickness)) / lengthOfWeldAroundFlange;
            tensionInWeldDueToMomentAboutY_PUL = (Inputs.momentAboutY / (Inputs.widthOfColumn * 0.5)) / lengthOfWeldAroundFlange;
            tensionInWeldDueToAxialTension_PUL = Math.Max(0.0, Inputs.axialForce) / (2.0 * lengthOfWeldAroundFlange);
            maxTensionInFlangeWeld_PUL =
                        tensionInWeldDueToMomentAboutX_PUL + tensionInWeldDueToMomentAboutY_PUL + tensionInWeldDueToAxialTension_PUL;

            /*  Flange Weld capacity */
            tensionCapacityOfWeld_flange_PUL = WeldCapacity.filletWeldCapacity(1.0, Inputs.weldThickness_flange, 90.0, Inputs.electrodeNumber);
            capacityRatioOfWeld_flange = maxTensionInFlangeWeld_PUL / tensionCapacityOfWeld_flange_PUL;

            /* Calculate Shear in web weld */
            shearInWeld_web_PUL = resultantShear / lengthOfWeldAroundWeb;

            /*  Web Weld capacity */
            shearCapacityOfWeld_web_PUL = WeldCapacity.filletWeldCapacity(1.0, Inputs.weldThickness_web, resultantShearAngle, Inputs.electrodeNumber);
            capacityRatioOfWeld_web = shearInWeld_web_PUL / shearCapacityOfWeld_web_PUL;

            /* bolt capacity*/
            totalNumberOfBolts = 2 * (Inputs.numberOfBoltsAlongX + Inputs.numberOfBoltsAlongY) - 4;

            /* compute maximum axial force in bolt */
            maximumAxialForceInBolt = Inputs.axialForce / (totalNumberOfBolts);     //kip
            maximumAxialForceInBolt += Inputs.momentAboutX / ((Inputs.depthOfColumn + 2.0 * Inputs.boltOffset) * Inputs.numberOfBoltsAlongX); //kip
            maximumAxialForceInBolt += Inputs.momentAboutY / ((Inputs.widthOfColumn + 2.0 * Inputs.boltOffset) * Inputs.numberOfBoltsAlongY); //kip

            /* compute maximum shear in bolt */
            maximumShearForceInBolt = resultantShear / totalNumberOfBolts;

            /* compute D/C Ratio of bolt in shear */
            shearStressInBolt = maximumShearForceInBolt / Inputs.areaOfBolt;
            capacityRatioOfBoltInShear = shearStressInBolt / (Constants.BOLT_PHI * Inputs.nominalShearStressOfBolt);

            /* compute D/C Ratio of bolt in tension */
            if (capacityRatioOfBoltInShear < 1.0)
            {
                modifiedNominalTensileStressOfBolt = Math.Min(
                    1.3 * Inputs.nominalTensileStressOfBolt - (Inputs.nominalTensileStressOfBolt * shearStressInBolt) / (Constants.BOLT_PHI * Inputs.nominalShearStressOfBolt),
                    Inputs.nominalTensileStressOfBolt);
            }
            else
            {
                modifiedNominalTensileStressOfBolt = 0;
            }

            if (maximumAxialForceInBolt > 0)
            {
                capacityRatioOfBoltInTension = maximumAxialForceInBolt / (Constants.BOLT_PHI * modifiedNominalTensileStressOfBolt * Inputs.areaOfBolt);
            }
            else
            {
                capacityRatioOfBoltInTension = 0;
            }


            /* base plate bearing stress and D/C Ratio */

            /*  calculate base plate sx and sy */
            basePlateSx = Inputs.widthOfBasePlate * Math.Pow(Inputs.depthOfBasePlate, 2.0) / 6.0;
            basePlateSy = Inputs.depthOfBasePlate * Math.Pow(Inputs.widthOfBasePlate, 2.0) / 6.0;

            /* calculate base plate area and concrete padestal areas */
            areaOfBasePlate = Inputs.depthOfBasePlate * Inputs.widthOfBasePlate;
            areaOfConcretePadestal = Inputs.depthOfConcretePadestal * Inputs.widthOfConcretePadestal;

            /* calculate maximum bearing stress */
            maximumBearingStress = -(Math.Min(0.0, Inputs.axialForce) / areaOfBasePlate);
            maximumBearingStress += Inputs.momentAboutX / basePlateSx;
            maximumBearingStress += Inputs.momentAboutY / basePlateSy;

            /* calculate allowable bearing pressure and bearing D/C Ratio */
            allowableBearingStress = Constants.CONCRETE_BEARING_PHI * 0.85 * Inputs.fc_concrete * Math.Sqrt(areaOfConcretePadestal / areaOfBasePlate);
            capacityRatioInBearing = maximumBearingStress / allowableBearingStress;

            /* check base plate thickness*/

            /* calculate base plate projection w.r.t column */
            projectionOfBasePlateAlongX = (Inputs.widthOfBasePlate - Inputs.widthOfColumn * 0.95) / 2.0;
            projectionOfBasePlateAlongY = (Inputs.depthOfBasePlate - Inputs.depthOfColumn * 0.95) / 2.0;
            maximumProjectionOfBasePlate = Math.Max(projectionOfBasePlateAlongX, projectionOfBasePlateAlongY);

            /* calculate shear and flexural stresses */
            shearStressInBasePlate = (maximumProjectionOfBasePlate * maximumBearingStress) / (1.0 * Inputs.thicknessOfBasePlate);
            flexuralStressInBasePlate = ((maximumProjectionOfBasePlate * maximumBearingStress) * (0.5 * maximumProjectionOfBasePlate))
                                            / (Math.Pow(Inputs.thicknessOfBasePlate, 2.0) / 4.0);
            /* calculate D/C Ratios */
            capacityRatioOfBasePlateInFlexure = flexuralStressInBasePlate / (Constants.BASE_PLATE_PHI * Inputs.FyOfBasePlateMaterial);
            capacityRatioOfBasePlateInShear = shearStressInBasePlate / (Constants.BASE_PLATE_PHI * 0.6 * Inputs.FuOfBasePlateMaterial);

        }

        private void takeInputs()
        {
            Inputs.title = txtTitle.Text;

            /* applied forces*/
            inputdouble(ref Inputs.axialForce, ref txtAxialForce); if (stopInput) return;
            inputdouble(ref Inputs.momentAboutX, ref txtMomentX); if (stopInput) return;
            inputdouble(ref Inputs.momentAboutY, ref txtMomentY); if (stopInput) return;
            inputdouble(ref Inputs.shearAlongX, ref txtShearX); if (stopInput) return;
            inputdouble(ref Inputs.shearAlongY, ref txtShearY); if (stopInput) return;
            Inputs.momentAboutX *= 12.0; 							/* convert to kip-inch for calculations */
            Inputs.momentAboutY *= 12.0;
            
            /* column details*/
            inputdouble(ref Inputs.depthOfColumn, ref txtColumnDepth); if (stopInput) return;
            inputdouble(ref Inputs.widthOfColumn, ref txtColumnWidth); if (stopInput) return;
            inputdouble(ref Inputs.columnWebThickness, ref txtColumnWebThickness); if (stopInput) return;
            inputdouble(ref Inputs.columnFlangeThickness, ref txtColumnFlangeThickness); if (stopInput) return;

            //  concrete padestal
            inputdouble(ref Inputs.depthOfConcretePadestal, ref txtPadestalDepth); if (stopInput) return;
            inputdouble(ref Inputs.widthOfConcretePadestal, ref txtPadestalWidth); if (stopInput) return;
            inputdouble(ref Inputs.fc_concrete, ref txtPadestalfc); if (stopInput) return;

            /* weld data*/
            inputint(ref Inputs.electrodeNumber, ref txtElectrodeNumber); if (stopInput) return;
            inputdouble(ref Inputs.weldThickness_flange, ref txtFlangeWeldThickness); if (stopInput) return;
            inputdouble(ref Inputs.weldThickness_web, ref txtWebWeldThickness); if (stopInput) return;
            
            ///* base plate and padestal data*/
            inputdouble(ref Inputs.depthOfBasePlate, ref txtBasePlateDepth); if (stopInput) return;
            inputdouble(ref Inputs.widthOfBasePlate, ref txtBasePlateWidth); if (stopInput) return;
            inputdouble(ref Inputs.thicknessOfBasePlate, ref txtBasePlateThickness); if (stopInput) return;
            inputdouble(ref Inputs.FyOfBasePlateMaterial, ref txtBasePlateFy); if (stopInput) return;
            inputdouble(ref Inputs.FuOfBasePlateMaterial, ref txtBasePlateFu); if (stopInput) return;
            
            ///* bolt data*/
            inputdouble(ref Inputs.boltOffset, ref txtBoltOffset); if (stopInput) return;
            inputint(ref Inputs.numberOfBoltsAlongX, ref txtNumberOfBoltsX); if (stopInput) return;
            inputint(ref Inputs.numberOfBoltsAlongY, ref txtNumberOfBoltsY); if (stopInput) return;
            inputdouble(ref Inputs.areaOfBolt, ref txtAreaOfBolt); if (stopInput) return;
            inputdouble(ref Inputs.nominalTensileStressOfBolt, ref txtBoltFu); if (stopInput) return;
            inputdouble(ref Inputs.nominalShearStressOfBolt, ref txtBoltFv); if (stopInput) return;

            
        }

        private void inputdouble(ref double _variable, ref TextBox _textbox)
        {
            try
            {
                _variable = Convert.ToDouble(_textbox.Text);
            }
            catch (FormatException)
            {
                //  MessageBox.Show("Invalid input format in TextBox " + _textbox.Name.ToString(), "Invalid Input Format");
                MessageBox.Show("Invalid input provided for field \"" + _textbox.Tag.ToString() +"\"", "Invalid Input Format");
                stopInput = true;
                _textbox.Focus();
                _textbox.SelectAll();
            }
        }
        private void inputint(ref int _variable, ref TextBox _textbox)
        {
            try
            {
                _variable = Convert.ToInt32(_textbox.Text);
            }
            catch (FormatException)
            {
                //MessageBox.Show("Invalid input format in TextBox " + _textbox.Name.ToString(), "Invalid Input Format");
                MessageBox.Show("Invalid input provided for field \"" + _textbox.Tag.ToString() + "\"", "Invalid Input Format");
                stopInput = true;
                _textbox.Focus();
                _textbox.SelectAll();
            }
        }

        private void bttnLoad_Click(object sender, EventArgs e)
        {
            String fileName;

            openFileDialog1.Filter = "BasePlate01 File|*.baseplate01";
            openFileDialog1.DefaultExt = ".baseplate01";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = openFileDialog1.FileName;
                    System.IO.StreamReader file = new System.IO.StreamReader(fileName);
                    //  title
                    txtTitle.Text = file.ReadLine();
                    //  column data
                    txtColumnDepth.Text = file.ReadLine();
                    txtColumnWidth.Text = file.ReadLine();
                    txtColumnFlangeThickness.Text = file.ReadLine();
                    txtColumnWebThickness.Text = file.ReadLine();
                    // forces
                    txtAxialForce.Text = file.ReadLine();
                    txtMomentX.Text = file.ReadLine();
                    txtMomentY.Text = file.ReadLine();
                    txtShearX.Text = file.ReadLine();
                    txtShearY.Text = file.ReadLine();
                    //weld
                    txtElectrodeNumber.Text = file.ReadLine();
                    txtFlangeWeldThickness.Text = file.ReadLine();
                    txtWebWeldThickness.Text = file.ReadLine();
                    // bolts
                    txtBoltOffset.Text = file.ReadLine();
                    txtNumberOfBoltsX.Text = file.ReadLine();
                    txtNumberOfBoltsY.Text = file.ReadLine();
                    txtAreaOfBolt.Text = file.ReadLine();
                    txtBoltFu.Text = file.ReadLine();
                    txtBoltFv.Text = file.ReadLine();
                    //  base plate
                    txtBasePlateDepth.Text = file.ReadLine();
                    txtBasePlateWidth.Text = file.ReadLine();
                    txtBasePlateThickness.Text = file.ReadLine();
                    txtBasePlateFy.Text = file.ReadLine();
                    txtBasePlateFu.Text = file.ReadLine();
                    //  Padestal
                    txtPadestalDepth.Text = file.ReadLine();
                    txtPadestalWidth.Text = file.ReadLine();
                    txtPadestalfc.Text = file.ReadLine();

                    file.Close();
                }
                catch (System.Exception fe)
                {
                    
                    MessageBox.Show(fe.Message, "Error");
                }
            }
        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            String fileName;
            fileName = txtTitle.Text + ".baseplate01";
            saveFileDialog1.DefaultExt = ".baseplate01";
            saveFileDialog1.Filter = "BasePlate01 File|*.baseplate01";
            saveFileDialog1.FileName = fileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = saveFileDialog1.FileName;
                    System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
                    //  title
                    file.WriteLine(txtTitle.Text);
                    //  column data
                    file.WriteLine(txtColumnDepth.Text);
                    file.WriteLine(txtColumnWidth.Text);
                    file.WriteLine(txtColumnFlangeThickness.Text);
                    file.WriteLine(txtColumnWebThickness.Text);
                    // forces
                    file.WriteLine(txtAxialForce.Text);
                    file.WriteLine(txtMomentX.Text);
                    file.WriteLine(txtMomentY.Text);
                    file.WriteLine(txtShearX.Text);
                    file.WriteLine(txtShearY.Text);
                    //weld
                    file.WriteLine(txtElectrodeNumber.Text);
                    file.WriteLine(txtFlangeWeldThickness.Text);
                    file.WriteLine(txtWebWeldThickness.Text);
                    // bolts
                    file.WriteLine(txtBoltOffset.Text);
                    file.WriteLine(txtNumberOfBoltsX.Text);
                    file.WriteLine(txtNumberOfBoltsY.Text);
                    file.WriteLine(txtAreaOfBolt.Text);
                    file.WriteLine(txtBoltFu.Text);
                    file.WriteLine(txtBoltFv.Text);
                    //  base plate
                    file.WriteLine(txtBasePlateDepth.Text);
                    file.WriteLine(txtBasePlateWidth.Text);
                    file.WriteLine(txtBasePlateThickness.Text);
                    file.WriteLine(txtBasePlateFy.Text);
                    file.WriteLine(txtBasePlateFu.Text);
                    //  Padestal
                    file.WriteLine(txtPadestalDepth.Text);
                    file.WriteLine(txtPadestalWidth.Text);
                    file.WriteLine(txtPadestalfc.Text);

                    file.Close();
                }
                catch (System.Exception fe)
                {

                    MessageBox.Show(fe.Message, "Error");
                }
            }
        }

        
        private void showPicture01(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._01;
        }

        private void showPicture02(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._02;

        }

        private void showPicture03(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._03;

        }

        private void showPicture04(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._04;

        }

        private void showPicture05(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._05;

        }

        private void showPicture06(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._06;

        }

        private void showPicture07(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._07;

        }

        private void showPicture08(object sender, EventArgs e)
        {
            hintPicture.Image = Properties.Resources._08;

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            this.Text = "BasePlate01[" + txtTitle.Text +"]";
        }

        private void bttnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showAboutWindow(object sender, EventArgs e)
        {
            Form af = new frmAbout();
            af.ShowDialog();
        }
        
        
    }
}
