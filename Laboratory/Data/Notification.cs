using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Laboratory.Data
{
    class Notification
    {
        Notifier notifier;
        public Notification()
        {
            this.notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }



        public async void show(string s,char x)
        {
            switch (x) {
                case 'i': notifier.ShowInformation(s); break;
                case 's': notifier.ShowSuccess(s); break;
                case 'w': notifier.ShowWarning(s); break;
                case 'e': notifier.ShowError(s); break;
                default:
                    
                    break;
            }
            
            await PutTaskDelay();
        }

        async Task PutTaskDelay()
        {
            await Task.Delay(5000);

        }
    }
}
