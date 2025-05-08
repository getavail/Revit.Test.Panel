using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Revit.Test.Panel
{
    public class RevitTestViewModel : ViewModelBase
    {
        #region Private Members

        private readonly ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
                
        private string _selectedDirectory;

        #endregion Private Members

        #region Properties

        public ObservableCollection<ItemViewModel> Items => _items;

        public string SelectedDirectory
        {
            get => _selectedDirectory;
            set
            {
                _selectedDirectory = value;
                NotifyPropertyChanged(nameof(SelectedDirectory));
            }
        }

        #endregion Properties

        #region Constructors

        public RevitTestViewModel()
        { }

        #endregion Constructors

        #region Public Logic

        public void SelectDirectory()
        {
            try
            {
                var directory = PickDirectory();

                if (string.IsNullOrEmpty(directory))
                    return;

                SelectedDirectory = directory;

                _items.Clear();

                var files = GetFiles(directory, ".rfa");

                foreach (var file in files)
                {
                    var item = new ItemViewModel()
                    {
                        Title = Path.GetFileName(file),
                        Path = file
                    };

                    _items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion Public Logic

        #region Private Logic

        private static string PickDirectory()
        {
            var dialog = new FolderPicker()
            {
                InputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Select Families Folder",
                Multiselect = false,
            };

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                return dialog.ResultPath;
            }

            return default;
        }

        private static List<string> GetFiles(string rootDirectory, string extension)
        {
            if (string.IsNullOrWhiteSpace(rootDirectory) || !Directory.Exists(rootDirectory))
                throw new ArgumentException("Invalid or non-existent directory.", nameof(rootDirectory));

            var files = new List<string>();

            try
            {
                files.AddRange(Directory.EnumerateFiles(rootDirectory, $"*{extension}", SearchOption.AllDirectories));
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied to some directories: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return files;
        }


        #endregion Private Logic
    }
}
