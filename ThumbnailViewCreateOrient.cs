/*
 * Created by SharpDevelop.
 * User: julianvenczel
 * Date: 11/09/2017
 * Time: 12:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace ViewCreate
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("43688061-4DB0-4A42-A82B-68F6638C25AB")]
	public partial class ThisApplication
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
		public void ViewCreate()
		{

		Document doc = this.ActiveUIDocument.Document;
		
	

		

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
			
			XYZ eye = new XYZ(100,100,100);
			
			XYZ forward = new XYZ(-1,1,-1);
			
			XYZ up = new XYZ (-1,1,2);
			
			ViewOrientation3D vOrient = new ViewOrientation3D(eye,up,forward);
			

		using (Transaction t = new Transaction(doc,"Create view"))

		{    

		    t.Start();

		    View3D view = View3D.CreateIsometric(doc,viewFamilyType.Id);
		    
		   	view.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE)
		    .Set(4);
		   	
		   	
		   	view.SetOrientation(vOrient);
		   	
		    view.HideCategoryTemporary(dim.Id);
		    
		    view.HideCategoryTemporary(line.Id);
		    
		    view.SaveOrientationAndLock();
		    

		    t.Commit();

		}

	}
		public void SetVisStyle()
		{
			Document doc = this.ActiveUIDocument.Document;
			View CurView = this.ActiveUIDocument.ActiveView;
			using (Transaction t = new Transaction(doc, "Set View"))
			{
				t.Start();
				CurView.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(4);
				t.Commit();
			}
				
			
				
			
		}
		public void getOrientation()
		{
			Document doc = this.ActiveUIDocument.Document;
			View currentView = this.ActiveUIDocument.ActiveView;
			
			View3D v3D = (currentView as View3D);
			ViewOrientation3D vOrient = v3D.GetOrientation();
			
			XYZ eye = vOrient.EyePosition;
			XYZ forward = vOrient.ForwardDirection;
			XYZ up = vOrient.UpDirection;
			
			String eyex = (eye.X).ToString();
			String eyey = (eye.Y).ToString();
			String eyez = (eye.Z).ToString();
			
			String eyeXYZ = "eye orientation is \nX: " + eyex + "\nY: " + eyey + "\nZ: " + eyez;
			
			String forwardx = (forward.X).ToString();
			String forwardy = (forward.Y).ToString();
			String forwardz = (forward.Z).ToString();
			
			String forwardXYZ = "forward orientation is\nX: " + forwardx + "\nY: " + forwardy + "\nZ: " + forwardz; 
			
			String upx = (up.X).ToString();
			String upy = (up.Y).ToString();
			String upz = (up.Z).ToString();
			
			String upXYZ = "up orientation is \nX: " + upx + "\nY: " + upy + "\nZ: " + upz;
			
			TaskDialog.Show( "View Orientation", eyeXYZ  + "\n" + forwardXYZ +"\n" + upXYZ );
		}
	}
}