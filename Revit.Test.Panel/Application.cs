using Autodesk.Revit.UI;

namespace Revit.Test.Panel
{
    public partial class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            var panel = application.CreateRibbonPanel(ToggleCommand.ADDIN_TITLE);
            var pushButtonData = new PushButtonData(ToggleCommand.ADDIN_TITLE,
                                                      ToggleCommand.ADDIN_TITLE,
                                                      typeof(Application).Assembly.Location,
                                                      ToggleCommand.COMMAND_CLASS_NAME);

            PushButton _pushButton = (PushButton)panel.AddItem(pushButtonData);
            _pushButton.Enabled = true;

            ToggleCommand.RegisterDockablePane(application, new RevitTestView());

            return Result.Succeeded;
        }
    }
}
