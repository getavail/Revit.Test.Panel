using System;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revit.Test.Panel
{
    internal class FamilyDropHandler : IControllableDropHandler
    {
        public void Execute(UIDocument uiDocument, object data)
        {
            try
            {
                var activeDoc = uiDocument.Document;
                var item = data as ItemViewModel;

                var familyPath = item.Path;
                Family loadedFamily;

                // Load family into project
                using (Transaction tx = new Transaction(activeDoc, "Load Family"))
                {
                    tx.Start();

                    if (!activeDoc.LoadFamily(familyPath, out loadedFamily))
                    {
                        tx.RollBack();
                        return;
                    }
                    else
                    {
                        tx.Commit();
                    }
                }

                // Get the first symbol in the loaded family
                FamilySymbol symbol = new FilteredElementCollector(activeDoc)
                    .OfClass(typeof(FamilySymbol))
                    .Cast<FamilySymbol>()
                    .FirstOrDefault(s => s.Family.Id == loadedFamily.Id);

                if (symbol == null)
                {
                    return;
                }

                if (!symbol.IsActive)
                {
                    using (Transaction tx = new Transaction(activeDoc, "Activate Symbol"))
                    {
                        tx.Start();
                        symbol.Activate();
                        tx.Commit();
                    }
                }

                //Prompt for Family Instance Placement
                try
                {
                    uiDocument.PromptForFamilyInstancePlacement(symbol);
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                { }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public bool CanExecute(UIDocument uiDocument, object data, ElementId dropViewId)
        {
            return true;
        }
    }
}
