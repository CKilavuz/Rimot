using Avalonia.Controls;
using Avalonia.Threading;
using Rimot.Desktop.Core.Interfaces;
using Rimot.Desktop.XPlat.Views;

namespace Rimot.Desktop.XPlat.Services
{
    public class SessionIndicatorLinux : ISessionIndicator
    {
        public void Show()
        {
            Dispatcher.UIThread.Post(() =>
            {
                var indicatorWindow = new SessionIndicatorWindow();
                indicatorWindow.Show();
            });
        }
    }
}
