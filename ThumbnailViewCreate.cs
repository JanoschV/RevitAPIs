/*
 * Created by SharpDevelop.
 * User: julianvenczel
 * Date: 12/09/2017
 * Time: 11:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace viewCreate
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("17039D86-BDF6-41AC-A160-55E381F00A22")]
	public partial class ThisDocument
	{
		private void Module_Startup(object sender, EventArgs e)
		{

		}

		private void Module_Shutdown(object sender, EventArgs e)
		{

		}

		#region Revit Macros generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(Module_Startup);
			this.Shutdown += new System.EventHandler(Module_Shutdown);
		}
		#endregion
		public void viewCreate()
		{
			
			Document doc = this.Application.ActiveUIDocument.Document;
		
	

		

			// get a ViewFamilyType for a 3D View. created new instance of ViewFamilYType calle 'viewFamilyType'

        ViewFamilyType viewFamilyType = (from v in new FilteredElementCollector(doc).
			// Creates new filteredElementCollector to select all ViewFamilyTypes 
        	OfClass(typeof(ViewFamilyType)).
		
         	Cast<ViewFamilyType>()

            where v.ViewFamily == ViewFamily.ThreeDimensional

            select v).First();
			
			
			
			Categories categories = doc.Settings.Categories;
			
			Category dim = categories.get_Item(BuiltInCategory.OST_Dimensions);
								
			Category line = categories.get_Item(BuiltInCategory.OST_Lines);
			
			

		using (Transaction t = new Transaction(doc,"Create view"))

		{    

		    t.Start();

		    View3D view = View3D.CreateIsometric(doc,viewFamilyType.Id);
		    
		    view.HideCategoryTemporary(dim.Id);
		    
		    view.HideCategoryTemporary(line.Id);
		    
		    view.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE)
		    	.Set(4);
		    
		    view.get_Parameter(BuiltInParameter.VIEW_NAME).Set("Thumbnail");
		    
		    view.SaveOrientationAndLock();
		    

		    t.Commit();

		}
		}
	}
}