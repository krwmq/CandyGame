using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.IO;
using System.Linq;

namespace CandyGame.Views;

public partial class BestScores : Window
{
    private int bestscore;
    private string playername;

    public BestScores(int score, string name)
    {
        InitializeComponent();
        bestscore = score;
        playername = name;
        Console.WriteLine(playername+" "+bestscore);
        txtokuma();
    }

    public void txtokuma()
    {
        string filePath = "CandyGame/text.txt";

        // Verileri tutacak liste
        List<Tuple<string, int>> data = new List<Tuple<string, int>>();

        // Dosya var mı kontrol et
        if (File.Exists(filePath))
        {
            // Dosyayı satır satır oku
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(' '); // İsmi ve sayıyı ayır
                if (parts.Length == 2 && int.TryParse(parts[1], out int number))
                {
                    // Listeye ekle (isim, sayı)
                    data.Add(new Tuple<string, int>(parts[0], number));
                }
            }
        }
        else
        {
            Console.WriteLine("Dosya bulunamadı!");
        }

        // Yeni skoru ekle
        data.Add(new Tuple<string, int>(playername, bestscore));

        // Listeyi puanlara göre azalan sırayla sıralıyoruz ve ilk 5 elemanı alıyoruz
        data = data.OrderByDescending(x => x.Item2).Take(5).ToList();

        // Yeni sıralanmış listeyi dosyaya yaz
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var item in data)
            {
                writer.WriteLine($"{item.Item1} {item.Item2}");
            }
        }

        // Listeyi yazdır (örneğin bir UI bileşenine yazdırma)
        for (int i = 0; i < data.Count; i++)
        {
            if (i == 0)
            {
                bir.Content = data[i].Item1;
                birpuan.Content = data[i].Item2;
            }
            else if (i == 1)
            {
                iki.Content = data[i].Item1;
                ikipuan.Content = data[i].Item2;
            }
            else if (i == 2)
            {
                uc.Content = data[i].Item1;
                ucpuan.Content = data[i].Item2;
            }
            else if (i == 3)
            {
                dort.Content = data[i].Item1;
                dortpuan.Content = data[i].Item2;
            }
            else
            {
                bes.Content = data[i].Item1;
                bespuan.Content = data[i].Item2;
            }
        }
    }

}