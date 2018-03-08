//-------------------------------------------------
// This example shows how to get the names of the creators
// of features in a part document.
//
// Preconditions: 
// 1. Specified file exists.
// 2. Open the Immediate window.
// 3. Run the macro.
//
// Postconditions: Names of the creators of the features are 
// printed to the Immediate window.
//-------------------------------------------------
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;

namespace CloseDocCSharp.csproj
{

    class Program
    {


        static void Main(string[] args)
        {
            SldWorks.SldWorks swApp;
            swApp = new SldWorks.SldWorks();

            ModelDoc2 swModel = default(ModelDoc2);
            Feature swFeat = default(Feature);
            string Filename = null;
            int errors = 0;
            int warnings = 0;

            Filename = "F:\\solidWorksAPI\\TEST_20180105\\part1.sldprt";

            // Open document
            swApp.OpenDoc6(Filename, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            swModel = (ModelDoc2)swApp.ActiveDoc;

            // Get first feature in this part document
            swFeat = (Feature)swModel.FirstFeature();
            // Iterate over features in this part document in 
            // FeatureManager design tree

            while ((swFeat != null))
            {
                // Write the name of the feature and its 
                // creator to the Immediate window
                Debug.Print("  Feature " + swFeat.Name + " created by " + swFeat.GetTypeName());

                // Get next feature in this part document
                swFeat = (Feature)swFeat.GetNextFeature();
            }

            swApp.CloseDoc(Filename);

            swApp.ExitApp();
            swApp = null;

        }

        /// <summary>
        /// The SldWorks swApp variable is pre-assigned for you.
        /// </summary>
    }
}