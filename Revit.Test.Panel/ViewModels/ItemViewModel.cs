namespace Revit.Test.Panel
{
    public class ItemViewModel : ViewModelBase
    {
        #region Private Members

        private string _title;

        private string _path;

        #endregion Private Members

        #region Properties

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                NotifyPropertyChanged(nameof(Path));
            }
        }

        #endregion Properties
    }
}
