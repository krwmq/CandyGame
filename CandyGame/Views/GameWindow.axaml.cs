using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CandyGame.ViewModels;
using Avalonia.Interactivity;

namespace CandyGame.Views;

public partial class GameWindow : Window
{
    private GameWindowViewModel _viewModel;
    private bool _isGameStarted = true;
    private string playername;
    public GameWindow(string name)
    {
        InitializeComponent();
        playername = name;
        isim.Content = playername;
        _viewModel = new GameWindowViewModel();
        LoadCandyGrid();
        StartTimer();
    }
    public int puan;
    private System.Timers.Timer _timer;
    private int _timeLeft = 120;

    public interface IImageTake
    {
        string ImageLocation(int location);
        string locate { get; set; }
    }
    //baslangıcta 3 tane yan yana veya alt alta gelmemesini sağlayarak seker ve joker olusturan fonksiyon
    private void LoadCandyGrid()
    {
        var matrix = _viewModel.CandyMatrix;
        string imagePath;

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                int candyIndex = matrix[row, column];
                IImageTake img = new GameBoard();
                imagePath = img.ImageLocation(candyIndex);
                var image = new Image
                {
                    Source = new Bitmap(imagePath),
                    Width = 50,
                    Height = 50
                };

                
                var rowCopy = row; // Yerel değişken
                var columnCopy = column;
                image.PointerPressed += (sender, e) => OnCandyClicked(rowCopy, columnCopy);
                
                Grid.SetRow(image, row);
                Grid.SetColumn(image, column);
                MainGrid.Children.Add(image);
            }
        }
    }

    public static int ilkrow;
    public static int ilkcolumn;
    public static bool cevir = false;
    //sekere tıklanıldığında konumunu alan ve kurallara uyması durumunda şekerlerin veya jokerlerini yerini değiştiren fonksiyon
    private async void OnCandyClicked(int row, int column)
    {
        if (cevir)
        {
            AnimateCandyClick(row, column);
            await Task.Delay(100);
            _viewModel.SwapCandies(ilkrow, ilkcolumn, row, column);
            cevir = false;
            var updatedMatrix = _viewModel.GetUpdatedMatrix();
            RefreshCandyGrid(updatedMatrix);

            // UI güncellemesini hemen uygula
            await Dispatcher.UIThread.InvokeAsync(() => { }, Avalonia.Threading.DispatcherPriority.Render);
        
            // 0.5 saniye bekle
            await Task.Delay(500);
            FillEmpty();
            while (_viewModel.kontrol())
            {
                FillEmpty();
                await Dispatcher.UIThread.InvokeAsync(() => { }, Avalonia.Threading.DispatcherPriority.Render);
                await Task.Delay(500);
            }
        }
        else
        {
            ilkrow = row;
            ilkcolumn = column;
            cevir = true;
            AnimateCandyClick(row, column);
        }

        puan += _viewModel.ekstra;
        _viewModel.ekstra = 0;
        ScoreLabel.Content = puan.ToString();
    }

    //sekere tıklanıldığında anlık büyültülüp küçültülmesini gösteren fonksiyon
    private async void AnimateCandyClick(int row, int column)
    {
        // Grid'deki Image'i bul
        var image = (Image)MainGrid.Children.FirstOrDefault(child => Grid.GetRow(child) == row && Grid.GetColumn(child) == column);

        if (image != null)
        {
            // Görseli geçici olarak büyüt
            var originalWidth = image.Width;
            var originalHeight = image.Height;

            // Büyütme
            image.Width = originalWidth * 1.2;
            image.Height = originalHeight * 1.2;

            // Kısa bir süre bekle
            await Task.Delay(100);

            // Eski boyutuna döndür
            image.Width = originalWidth;
            image.Height = originalHeight;
        }
    }

    //olusan durumlarda patlayan sekerleri dolduran metod fonksiyon
    private void FillEmpty()
    {
        _viewModel.FillEmptyCandies();
        var updatedMatrix = _viewModel.GetUpdatedMatrix();
        RefreshCandyGrid(updatedMatrix);
    }
    
    //patlama ve yer degistirme durumunda sayfayı yenileyen fonksiyon
    private void RefreshCandyGrid(int[,] updatedMatrix)
    {
        MainGrid.Children.Clear(); // Önceki görselleri temizle

        for (int row = 0; row < updatedMatrix.GetLength(0); row++)
        {
            for (int column = 0; column < updatedMatrix.GetLength(1); column++)
            {
                int candyIndex = updatedMatrix[row, column];
                string imagePath;
                if (candyIndex == 0)
                {
                    BlueCandy blue = new BlueCandy();
                    imagePath = blue.GetImagePath();
                }
                else if (candyIndex == 1)
                {
                    GreenCandy green = new GreenCandy();
                    imagePath = green.GetImagePath();
                }
                else if (candyIndex == 2)
                {
                    YellowCandy yellow = new YellowCandy();
                    imagePath = yellow.GetImagePath();
                }
                else if (candyIndex == 3)
                {
                    RedCandy red = new RedCandy();
                    imagePath = red.GetImagePath();
                }
                else if (candyIndex == 4)
                {
                    RoketDikey roketdikey = new RoketDikey();
                    imagePath = roketdikey.GetImagePath();
                }
                else if (candyIndex == 5)
                {
                    RoketYatay roketyatay = new RoketYatay();
                    imagePath = roketyatay.GetImagePath();
                }
                else if (candyIndex == 6)
                {
                    Kopter kopter = new Kopter();
                    imagePath = kopter.GetImagePath();
                }
                else if (candyIndex == 7)
                {
                    Bomba bomb = new Bomba();
                    imagePath = bomb.GetImagePath();
                }
                else if (candyIndex == 8)
                {
                    Gokkusagi gokkusagi = new Gokkusagi();
                    imagePath = gokkusagi.GetImagePath();
                }
                else
                {
                    Empty empty = new Empty();
                    imagePath = empty.GetImagePath();
                    puan += 1;
                }

                // Yeni bir Image kontrolü oluştur
                var image = new Image
                {
                    Source = new Bitmap(imagePath),
                    Width = 50,
                    Height = 50
                };
                
                var rowCopy = row;
                var columnCopy = column;
                image.PointerPressed += (sender, e) => OnCandyClicked(rowCopy, columnCopy);

                
                Grid.SetRow(image, row);
                Grid.SetColumn(image, column);
                MainGrid.Children.Add(image);
            }
        }
    }
    
    //zamanlayıcıyı baslatan ve zamanlayıcı fonksiyonları
    private void StartTimer()
    {
        _timer = new System.Timers.Timer(1000); // 1000 ms = 1 saniye
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }
    
    private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (_timeLeft > 0)
            {
                _timeLeft--;
                TimeLabel.Content = _timeLeft.ToString();
            }
            else
            {
                _timer.Stop();
                var mainWindow = new MainWindow();
                mainWindow.Show();
                var bestscores = new BestScores(puan, playername);
                Close();
            }
        });
    }
    
    //stop butonuna tıklanıldığında oyunu ve timeri durduran veya devam ettiren metod
    private void ButtonStop(object source, RoutedEventArgs args)
    {
        if (_isGameStarted)
        {
            _isGameStarted = false;
            _timer.Stop();
            DisableGameInteraction();
        }
        else
        {
            _isGameStarted = true;
            _timer.Start();
            EnableGameInteraction();
        }
        Console.WriteLine(_isGameStarted);
    }
    
    //stop butonuna tıklanması durumuna gore oyunu durdurup devam ettiren fonksiyonlar
    private void EnableGameInteraction()
    {
        foreach (var child in MainGrid.Children)
        {
            if (child is Image image)
            {
                image.IsEnabled = true;
            }
        }
    }

    private void DisableGameInteraction()
    {
        foreach (var child in MainGrid.Children)
        {
            if (child is Image image)
            {
                image.IsEnabled = false; 
            }
        }
    }
    
}