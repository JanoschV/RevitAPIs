/*
 * Created by SharpDevelop.
 * User: julianvenczel
 * Date: 1/09/2017
 * Time: 9:00 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace viewFilterChanger
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("004FF341-7740-488C-A540-4FFF317A891D")]
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
		public void filterChanger()
		{
			Document CurDoc = this.ActiveUIDocument.Document;
			
			ElementCategoryFilter GMFilt = new ElementCategoryFilter(BuiltInCategory.OST_GenericModel);
			string GmMat = ""; 
			
			foreach (Element G in new FilteredElementCollector(CurDoc)
			         .OfClass(typeof(FamilyInstance))
			         .WherePasses(GMFilt))
			{
				FamilyInstance gfi = G as FamilyInstance;
				FamilySymbol gs = gfi.Symbol;
				GmMat	+= gs.StructuralMaterialType;			
			}
			TaskDialog.Show("elements", GmMat);
		}
	}
}