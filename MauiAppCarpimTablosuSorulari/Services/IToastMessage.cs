namespace MauiAppCarpimTablosuSorulari.Services
{
    public interface IToastMessage
    {
        void ShortToast(string message);
        void LongToast(string message);
    }
}
