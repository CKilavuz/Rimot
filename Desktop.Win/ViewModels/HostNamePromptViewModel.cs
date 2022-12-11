using Rimot.Desktop.Core.ViewModels;

namespace Rimot.Desktop.Win.ViewModels
{
    public class HostNamePromptViewModel : BrandedViewModelBase
    {
        private string _host = "https://";

        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                FirePropertyChanged();
            }
        }
    }
}
