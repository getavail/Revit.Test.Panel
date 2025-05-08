using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

namespace Revit.Test.Panel
{
    [Transaction(TransactionMode.Manual)]
    public class ToggleCommand : IExternalCommand
    {
        public const string DOCKABLE_PANEL_ID = "941FC8DB-BE96-4A68-8D2A-71EBD37C7828";
        public static readonly Guid DOCKABLE_PANE_GUID = new Guid(DOCKABLE_PANEL_ID);
        public static readonly DockablePaneId DOCKABLE_PANE_ID = new DockablePaneId(DOCKABLE_PANE_GUID);
        public const string ADDIN_TITLE = "Revit Test";
        public const string COMMAND_CLASS_NAME = "Revit.Test.Panel.ToggleCommand";

        public Result Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            return ToggleDockablePane(commandData);
        }

        private Result ToggleDockablePane(ExternalCommandData commandData)
        {
            Result ret;

            try
            {
                DockablePane dp = commandData.Application.GetDockablePane(DOCKABLE_PANE_ID);

                if (dp.IsShown())
                {
                    dp.Hide();
                }
                else
                {
                    dp.Show();
                }

                ret = Result.Succeeded;
            }
            catch (Exception ex)
            {
                ret = Result.Failed;
            }

            return ret;
        }

        public static void RegisterDockablePane(UIControlledApplication application, IDockablePaneProvider paneProvider)
        {
            try
            {
                application.RegisterDockablePane(DOCKABLE_PANE_ID, ADDIN_TITLE, paneProvider);
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }
    }
}
