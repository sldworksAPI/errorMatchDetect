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
            Component2 swComponent;
            AssemblyDoc swAssembly;
            object[] Components = null;
            object[] swMateEntityList = null;
            MateEntity2 swMateEntity;
            Mate2 swMate;
            MateInPlace swMateInPlace;
            int numMateEntities = 0;
            int i = 0;
            dynamic temp;
            Component2 temp2;

            Feature swFeat = default(Feature);
            Feature swSubFeat;
            string Filename = null;
            int errors = 0;
            int warnings = 0;

            Filename = "F:\\solidWorksAPI\\TEST_20180105\\Assembly_20180105.sldasm";

            // Open document
            swApp.OpenDoc6(Filename, (int)swDocumentTypes_e.swDocASSEMBLY, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            swModel = (ModelDoc2)swApp.ActiveDoc;
            swAssembly = (AssemblyDoc)swModel;

            /*           // Iterate through parts document and list concentric mate. Would Get repeating ones.
                        Components = (Object[])swAssembly.GetComponents(false);
                        foreach (Object SingleComponent in Components)
                        {
                            swComponent = (Component2)SingleComponent;
                            Console.WriteLine("Name of component: " + swComponent.Name2);
                            Mates = (Object[])swComponent.GetMates();
                            if (Mates != null)
                            {
                                foreach (Object SingleMate in Mates)
                                {
                                    if (SingleMate is SolidWorks.Interop.sldworks.Mate2)
                                    {
                                        swMate = (Mate2)SingleMate;
                                        if (swMate.Type == 1)
                                            Console.WriteLine("Found one concentric mate.");

                                    }


                                    if (SingleMate is SolidWorks.Interop.sldworks.MateInPlace)

                                    {

                                        swMateInPlace = (MateInPlace)SingleMate;

                                        numMateEntities = swMateInPlace.GetMateEntityCount();

                                        for (i = 0; i <= numMateEntities - 1; i++)

                                        {

                                            Console.WriteLine(" Mate component name: " + swMateInPlace.get_MateComponentName(i));

                                            Console.WriteLine(" Type of mate inplace: " + swMateInPlace.get_MateEntityType(i));

                                        }
                                    }
                                }

                            }
                        }

            */
            // Get first feature in swModel
            swFeat = (Feature)swModel.FirstFeature();
            // Iterate over features in this part document in 
            // FeatureManager design tree

            while ((swFeat != null))
            {
                // Write the name of the feature and its 
                // creator to the Immediate window
                Console.WriteLine("  Feature " + swFeat.Name + " created by " + swFeat.GetTypeName());

                if (swFeat.GetTypeName() == "MateGroup")
                {
                    // Get first mate, which is a subfeature
                    swSubFeat = (Feature)swFeat.GetFirstSubFeature();

                    while (swSubFeat != null)
                    {
                        swMate = swSubFeat.GetSpecificFeature2();
                        // Go further analysis if swMate is of concentric type
                        if (swMate != null & swMate.Type == (int)swMateType_e.swMateCONCENTRIC)
                        {
                            Console.WriteLine(swSubFeat.Name);
                            Console.WriteLine("Num of Entity envolved: " + swMate.GetMateEntityCount());
                            //Console.WriteLine(swMate.MateEntity(2).GetEntityParamsSize());
                            for (i = 0; i <= 1; i++)
                            {
                                swMateEntity = swMate.MateEntity(i);
                                // Can't watch entity params in the watch window but works if setting a new variable.
                                temp = swMateEntity.EntityParams;
                                // verify which entity belongs to a threaded hole feature and the other should be counterbore or through hole
                                 = swMateEntity.ReferenceComponent;
                                temp3 = temp2.FirstFeature;
                                while 
                                Console.WriteLine(temp2.Name);
                                //swMateEntity[i] = (IMateEntity2)swMate.MateEntity(i);
                                swComponent = swMate.MateEntity(i).ReferenceComponent;
                                Console.WriteLine(swComponent.Name);
                                //swMate.MateEntity(i).ReferenceType;
                                //Console.WriteLine("radius1: " + swMate.MateEntity(i).IGetEntityParams(6));
                                //Console.WriteLine("radius2: " + swMate.MateEntity(i).IGetEntityParams(7));
                            }
                        }
                        // Get the next mate in MateGroup
                        swSubFeat = swSubFeat.GetNextSubFeature();
                    }
                }
                // Get next feature in this part document
                swFeat = (Feature)swFeat.GetNextFeature();
            }

            swApp.CloseDoc(Filename);

            swApp.ExitApp();
            swApp = null;
            Console.WriteLine("Processing done");
            Console.ReadKey();
        }

        /// <summary>
        /// The SldWorks swApp variable is pre-assigned for you.
        /// </summary>
    }
}