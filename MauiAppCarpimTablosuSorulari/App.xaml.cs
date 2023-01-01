namespace MauiAppCarpimTablosuSorulari;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        DependencyService.Register<IToastMessage, ToastMessage>();
        MainPage = new AppShell();
	}
}
