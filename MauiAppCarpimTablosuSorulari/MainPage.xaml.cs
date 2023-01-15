using CommunityToolkit.Maui.Views;

namespace MauiAppCarpimTablosuSorulari;

public partial class MainPage : ContentPage
{
    private const int YANIT_SAYISI = 4;

    int toplamYanlisYanitSayisi = 0;

    int dogruYanit = 0;
    string siradakiSoru = "";
    List<string> liste = new();
    private readonly IToastMessage _toastMessage;
    public MainPage()
    {
        InitializeComponent();

        GenerateAnsweringButtons();
        _toastMessage = DependencyService.Get<IToastMessage>();
        Generate();
        GetNextQuestion();
    }

    private void GenerateAnsweringButtons()
    {
        gridAnswering.Clear();
        gridAnswering.ColumnDefinitions.Clear();
        for (int i = 0; i < YANIT_SAYISI; i++)
        {
            gridAnswering.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            Button button = new()
            {
                Text = $"Answer {i + 1}",
                Margin = new Thickness(13, 13)
            };
            button.Clicked += OnClicked;
            gridAnswering.SetRow(button, 0);
            gridAnswering.SetColumn(button, i);
            gridAnswering.Add(button);
        }
    }

    //private void Button_Clicked(object sender, EventArgs e)
    //{
    //    throw new NotImplementedException();
    //}
    int _M = 10;
    int _N = 12;
    private void Generate()
    {

        for (int j = 1; j <= _M; j++)
        {
            for (int i = 1; i <= _N; i++)
            {
                Console.WriteLine($"{i} X {j} = {(i * j)}");
                liste.Add($"{i} X {j}");
            }
            Console.WriteLine();
        }

        LblKalanSoruSayisi.Text = $"Kalan Soru Sayısı : {_M * _N}";
    }

    readonly Random random = new();
    //private void GetRandomSoru()
    //{
    //    if (liste.Count > 0)
    //    {
    //        Random r = new();
    //        var indexRandom = r.Next(0, liste.Count);
    //        siradakiSoru = liste[indexRandom];
    //        var splitted = liste[indexRandom].Split("X");
    //        var sayi1 = splitted[0].Trim();
    //        var sayi2 = splitted[1].Trim();
    //        var rastgeleSayi = random.Next(0, 2);
    //        dogruYanit = (int.Parse(sayi1) * int.Parse(sayi2));
    //        var yanlisYanit = -1;
    //        while (yanlisYanit == -1)
    //        {
    //            var rastgele = random.Next(1, 101);
    //            if (rastgele != dogruYanit)
    //                yanlisYanit = rastgele;
    //        }
    //        if (rastgeleSayi == 1)//doğru yanıt 1. seçenek olsun.
    //        {
    //            BtnYanit1.Text = dogruYanit.ToString();
    //            BtnYanit2.Text = yanlisYanit.ToString();
    //        }
    //        else////doğru yanıt 2. seçenek olsun.
    //        {
    //            BtnYanit1.Text = yanlisYanit.ToString();
    //            BtnYanit2.Text = dogruYanit.ToString();
    //        }
    //        LblSoru.Text = liste.ElementAt(indexRandom);
    //        SemanticScreenReader.Announce(BtnYanit1.Text);
    //        SemanticScreenReader.Announce(BtnYanit2.Text);
    //        SemanticScreenReader.Announce(LblSoru.Text);
    //    }
    //    else
    //    {
    //        ToastMesajVer("TESTİ BAŞARIYLA BİTİRDİNİZ");
    //        PopopGoster("TESTİ BAŞARIYLA BİTİRDİNİZ");
    //    }
    //}
    private void GetNextQuestion()
    {
        if (liste.Count > 0)
        {
            Random r = new();
            var indexRandom = r.Next(0, liste.Count);
            siradakiSoru = liste[indexRandom];
            var splitted = liste[indexRandom].Split("X");
            var sayi1 = splitted[0].Trim();
            var sayi2 = splitted[1].Trim();
            dogruYanit = (int.Parse(sayi1) * int.Parse(sayi2));
            List<int> wrongAnswers = new List<int>();
            for (int i = 0; i < YANIT_SAYISI - 1; i++)
            {
                var yanlisYanit = -1;
                while (yanlisYanit == -1)
                {
                    var rastgele = random.Next(1, _M * _N);
                    if (rastgele == dogruYanit || wrongAnswers.Contains(rastgele))
                        continue;
                    yanlisYanit = rastgele;
                }
                wrongAnswers.Add(yanlisYanit);
            }

            var trueAnswerIndex = random.Next(0, YANIT_SAYISI);
            for (int i = 0; i < gridAnswering.Children.Count; i++)
            {
                var button = gridAnswering.Children[i] as Button;
                //var button = gridAnswering.FindByName<Button>($"btnAnswer{i}");

                if (trueAnswerIndex == i)
                {
                    button.Text = dogruYanit.ToString();
                }
                else
                {
                    var wrongAnswer = wrongAnswers[0];//take fistt wrong answer
                    wrongAnswers.RemoveAt(0); //remove used wrong answer
                    button.Text = wrongAnswer.ToString(); // set wrong answer to button Text
                }
                SemanticScreenReader.Announce(button.Text);
            }
            //if (trueAnswerIndex == 1)//doğru yanıt 1. seçenek olsun.
            //{
            //    BtnYanit1.Text = dogruYanit.ToString();
            //    BtnYanit2.Text = yanlisYanit.ToString();
            //}
            //else////doğru yanıt 2. seçenek olsun.
            //{
            //    BtnYanit1.Text = yanlisYanit.ToString();
            //    BtnYanit2.Text = dogruYanit.ToString();
            //}
            LblSoru.Text = siradakiSoru;
            //SemanticScreenReader.Announce(BtnYanit1.Text);
            //SemanticScreenReader.Announce(BtnYanit2.Text);
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
            GetNextQuestion();
        }
        else
        {
            ToastMesajVer("YANLIŞ");
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
        _toastMessage.ShortToast(text);
    }

    private void BtnTestiSonlandir_Clicked(object sender, EventArgs e)
    {
        PopopGoster("TESTİ BAŞARIYLA BİTİRDİNİZ");
    }
}

