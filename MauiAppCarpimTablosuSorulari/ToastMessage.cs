using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace MauiAppCarpimTablosuSorulari
{
    public class ToastMessage : IToastMessage
    {
        public void LongToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new();
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 32;
            var toast = Toast.Make(message, duration, fontSize);
            toast.Show(cancellationTokenSource.Token);
        }

        public void ShortToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new();
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 32;
            var toast = Toast.Make(message, duration, fontSize);
            toast.Show(cancellationTokenSource.Token);
        }
    }
}
