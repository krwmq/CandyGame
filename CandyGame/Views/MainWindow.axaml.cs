using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
using Avalonia.Threading;

namespace CandyGame.Views;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;
    private DateTime _startTime;
    public MainWindow()
    {
        InitializeComponent();
        
    }
    private void OpenWindow1_Click(object source, RoutedEventArgs args)
    {
        var bestscores = new BestScores(-1, "null");
        bestscores.Show();
    }
    private void OyunuAc_Click(object source, RoutedEventArgs args)
    {
        var name = isimMenu.Text;
        var gameWindow = new GameWindow(name);
        gameWindow.Show();
        this.Close();
    }
    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        // Eğer TextBox'ın içeriği "isminizi giriniz" ise, o zaman temizle
        if (isimMenu.Text == "isminizi giriniz")
        {
            isimMenu.Clear();
        }
    }
    
}