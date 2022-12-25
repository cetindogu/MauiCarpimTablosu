using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace MauiAppCarpimTablosuSorulari;

public partial class MainPage : ContentPage
{
    int toplamYanlisYanitSayisi = 0;

    int dogruYanit = 0;
    string siradakiSoru = "";
    List<string> liste = new();

    public MainPage()
    {
        Generate();

        InitializeComponent();
        GetRandomSoru();
    }
    private void Generate()
    {
        for (int j = 1; j <= 10; j++)
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{i} X {j} = {(i * j)}");
                liste.Add($"{i} X {j}");
            }
            Console.WriteLine();
        }
    }

    readonly Random random = new();
    private void GetRandomSoru()
    {
        if (liste.Count > 0)
        {
            Random r = new();
            var indexRandom = r.Next(0, liste.Count);
            siradakiSoru = liste[indexRandom];
            var splitted = liste[indexRandom].Split("X");
            var sayi1 = splitted[0].Trim();
            var sayi2 = splitted[1].Trim();
            var rastgeleSayi = random.Next(0, 2);
            dogruYanit = (int.Parse(sayi1) * int.Parse(sayi2));
            var yanlisYanit = -1;
            while (yanlisYanit == -1)
            {
                var rastgele = random.Next(1, 101);
                if (rastgele != dogruYanit)
                    yanlisYanit = rastgele;
            }
            if (rastgeleSayi == 1)//doğru yanıt 1. seçenek olsun.
            {
                BtnYanit1.Text = dogruYanit.ToString();
                BtnYanit2.Text = yanlisYanit.ToString();
            }
            else////doğru yanıt 2. seçenek olsun.
            {
                BtnYanit1.Text = yanlisYanit.ToString();
                BtnYanit2.Text = dogruYanit.ToString();
            }
            LblSoru.Text = liste.ElementAt(indexRandom);
            SemanticScreenReader.Announce(BtnYanit1.Text);
            SemanticScreenReader.Announce(BtnYanit2.Text);
            SemanticScreenReader.Announce(LblSoru.Text);
        }
        else
        {
            ToastMesajVer("TESTİ BAŞARIYLA BİTİRDİNİZ");
            PopopGoster("TESTİ BAŞARIYLA BİTİRDİNİZ");
        }
    }

    private void RemoveCorrentAnswer()
    {
        liste.Remove(siradakiSoru);
        siradakiSoru = null;
        dogruYanit = -1;
        LblKalanSoruSayisi.Text = $"Kalan Soru Sayısı : {liste.Count}";
    }
    private void OnClicked(object sender, EventArgs e)
    {
        var secilenDugmeMetni = ((Button)sender).Text;
        if (dogruYanit.ToString() == secilenDugmeMetni)
        {
            ToastMesajVer("DOĞRU");
            RemoveCorrentAnswer();
            GetRandomSoru();
        }
        else
        {
            toplamYanlisYanitSayisi++;
            LblToplamYanlisYanitSayisi.Text = $"Toplam Yanlış Yanıt Sayısı : {toplamYanlisYanitSayisi}";
        }
    }
    private void PopopGoster(string message)
    {
        var popup = new Popup
        {
            Content = new VerticalStackLayout
            {
                Children =
        {
            new Label
            {
                Text = message
            }
        }
            }
        };
        this.ShowPopup(popup);
    }
    private void ToastMesajVer(string text)
    {
        CancellationTokenSource cancellationTokenSource = new();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 32;
        var toast = Toast.Make(text, duration, fontSize);
        toast.Show(cancellationTokenSource.Token);
    }

    private void BtnTestiSonlandir_Clicked(object sender, EventArgs e)
    {
        PopopGoster("TESTİ BAŞARIYLA BİTİRDİNİZ");
    }
}

