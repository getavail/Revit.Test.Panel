using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autodesk.Revit.UI;

namespace Revit.Test.Panel
{
    /// <summary>
    /// Interaction logic for RevitTestView.xaml
    /// </summary>
    public partial class RevitTestView : Page
    {
        #region Private Members

        private readonly RevitTestViewModel _viewModel;

        #endregion Private Members

        #region Constructors

        public RevitTestView()
        {
            InitializeComponent();

            _viewModel = new RevitTestViewModel();

            DataContext = _viewModel;
        }

        #endregion Constructors

        #region Events

        Point _dragStartPoint;

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = _dragStartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                if (sender is ListView listView && listView.SelectedItem is ItemViewModel item)
                {
                    try
                    {
                        FamilyDropHandler handler = new FamilyDropHandler();

                        UIApplication.DoDragDrop(item, handler);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectDirectory();
        }

        #endregion Events
    }

    public partial class RevitTestView : IDockablePaneProvider
    {
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;

            var paneState = new DockablePaneState
            {
                DockPosition = DockPosition.Tabbed,
                TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser
            };

            data.InitialState = paneState;
            data.FrameworkElement = this;
            data.VisibleByDefault = true;
        }
    }
}
