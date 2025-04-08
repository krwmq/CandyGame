using System;
namespace CandyGame.ViewModels;

public class GameWindowViewModel : CandyChanging
{
    private int[,] _candyMatrix;
    public int ekstra;
    public int[,] CandyMatrix
    {
        get { return _candyMatrix; }
        private set { _candyMatrix = value; }
    }
    public GameWindowViewModel()
    {
        GenerateCandyMatrix();
    }
    private void GenerateCandyMatrix()
    {
        CandyMatrix = new int[9, 9];
        Random random = new Random();
        int sans;
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                int value;
                bool isValid;
                
                do
                {
                    value = random.Next(4); // 0-3 arasında rastgele bir sayı
                    isValid = true;

                    // Yan yana 3 aynı sayı kontrolü
                    if (column >= 2 &&
                        CandyMatrix[row, column - 1] == value &&
                        CandyMatrix[row, column - 2] == value)
                    {
                        isValid = false;
                    }

                    // Alt alta 3 aynı sayı kontrolü
                    if (row >= 2 &&
                        CandyMatrix[row - 1, column] == value &&
                        CandyMatrix[row - 2, column] == value)
                    {
                        isValid = false;
                    }
                } while (!isValid);
                CandyMatrix[row, column] = value;
                sans = random.Next(51);
                if (sans == 1)
                {
                    CandyMatrix[row, column] = 4;
                }
                sans = random.Next(51);
                if (sans == 1)
                {
                    CandyMatrix[row, column] = 5;
                }
                sans = random.Next(51);
                if (sans == 1)
                {
                    CandyMatrix[row, column] = 6;
                }
                sans = random.Next(70);
                if (sans == 1)
                {
                    CandyMatrix[row, column] = 7;
                }
                sans = random.Next(82);
                if (sans == 1)
                {
                    CandyMatrix[row, column] = 8;
                }
                
            }
        }
    }
    // İki hücreyi takas yapma fonksiyonu
    public void SwapCandies(int row1, int col1, int row2, int col2)
    {
        oyunmasasitut(_candyMatrix);
        candychange(row1, col1, row2, col2);
        catchscore();
        BoslukGoster();
        _candyMatrix = tablegonder();
       // Console.WriteLine(toplam);
    }

    //boşalan gücreleri doldurma metodu
    public void FillEmptyCandies()
    {
        oyunmasasitut(_candyMatrix);
        Doldur();
        _candyMatrix = tablegonder();
    }

    // Güncellenmiş matrisi döndürme fonksiyonu
    public int[,] GetUpdatedMatrix()
    {
        //yazdır();
        return _candyMatrix;
    }
    
    //dinamik olarak güncellenmesi için kontrol
    public bool kontrol()
    {
        // Geçerli tabloyu tut
        oyunmasasitut(_candyMatrix);

        // Mevcut durumu kontrol et
        catchscore();

        // Eğer tekrar eden konumlar varsa patlama işlemi yapılmalı
        if (tekrarEdenKonumlar.Count > 0)
        {
            for (int i = 0; i < tekrarEdenKonumlar.Count; i++)
            {
                ekstra += 1;
            }
            BoslukGoster(); // Boşluk gösterme işlemini yap
            return true;    // Dinamik kontrol için true döndür
        }

        return false; // Yeni patlama yok
    }
    
}