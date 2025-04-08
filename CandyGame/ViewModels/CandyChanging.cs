using System;
using System.Collections.Generic;

namespace CandyGame.ViewModels;

public class CandyChanging
{
    public int toplam;
    public int[,] table;
    private int _score; // 
    public int Score
    {
        get => _score;
        set
        {
            if (value >= 0)
            {
                _score = value;
            }
        }
    }

    public List<(int row, int col)> tekrarEdenKonumlar = new List<(int, int)>();

    //oyunmasasını tutan metod
    public void oyunmasasitut(int[,] matrix)
    {
        table = matrix;
    }

    //güncellenmiş oyun masasını gönderen metod
    public int[,] tablegonder()
    {
        return table;
    }
    
    //jokerlerin durumuna göre ve şekerlere göre swap yapan metod
    public void candychange(int row1, int col1, int row2, int col2)
    {
        if ((row1 + 1 == row2 && col1 == col2) ||
            (row1 - 1 == row2 && col1 == col2) ||
            (col1 - 1 == col2 && row1 == row2) ||
            (col1 + 1 == col2 && row1 == row2))
        {
            int temp = table[row1, col1];
            table[row1, col1] = table[row2, col2];
            table[row2, col2] = temp;
            
            bool firstJoker = table[row1, col1] > 3;
            bool secondJoker = table[row2, col2] > 3;

            if (firstJoker && secondJoker)
            {
                int joker1 = table[row1, col1];
                int joker2 = table[row2, col2];
                if (joker1==4 || joker1==5 || joker1==7)
                {
                    joker(row1, col1, joker1);
                }
                else
                {
                    joker(row1, col1, row2, col2, joker1);
                }

                if (joker2==4 || joker2==5 || joker2==7)
                {
                    joker(row2, col2, joker2); 
                }
                else
                {
                    joker(row2, col2, row1, col1, joker2);
                }
            }
            else if (firstJoker)
            {
                int joker3 = table[row1, col1];
                if (joker3==4 || joker3==5 || joker3==7)
                {
                    joker(row1, col1, joker3); 
                }
                else
                {
                    joker(row1, col1, row2, col2, joker3);
                }
            }
            else if (secondJoker)
            {
                int joker4 = table[row2, col2];
                if (joker4==4 || joker4==5 || joker4==7)
                {
                    joker(row2, col2, joker4); 
                }
                else
                {
                    joker(row2, col2, row1, col1, joker4);
                }
            }
        }
    }

    //tekrar eden konumları tekrarEdenKonumlar Listine gönderen metod
    public void catchscore()
    {
        // Yatay kontrol
        for (int i = 0; i < 9; i++)
        {
            int count = 1;
            for (int j = 0; j < 8; j++)
            {
                if (table[i, j] == table[i, j + 1])
                {
                    count++;
                }
                else
                {
                    if (count >= 3)
                    {
                        for (int k = j - count + 1; k <= j; k++)
                        {
                            tekrarEdenKonumlar.Add((i, k));
                        }
                    }

                    count = 1; // Sıfırla
                }
            }

            if (count >= 3)
            {
                for (int k = 8 - count + 1; k <= 8; k++)
                {
                    tekrarEdenKonumlar.Add((i, k));
                }
            }
        }

        // Dikey kontrol
        for (int j = 0; j < 9; j++)
        {
            int count = 1;
            for (int i = 0; i < 8; i++)
            {
                if (table[i, j] == table[i + 1, j])
                {
                    count++;
                }
                else
                {
                    if (count >= 3)
                    {
                        for (int k = i - count + 1; k <= i; k++)
                        {
                            tekrarEdenKonumlar.Add((k, j));
                        }
                    }

                    count = 1; // Sıfırla
                }
            }

            if (count >= 3)
            {
                for (int k = 8 - count + 1; k <= 8; k++)
                {
                    tekrarEdenKonumlar.Add((k, j));
                }
            }
        }
    }

    //boslukları göstermesi tekrarEdenKonumlar Listinden konumlarına göre içeriğini empty yapan metod
    public void BoslukGoster()
    {
        for (int i = 0; i < tekrarEdenKonumlar.Count; i++)
        {
            table[tekrarEdenKonumlar[i].row, tekrarEdenKonumlar[i].col] = 9;
            toplam += 1;
        }
        tekrarEdenKonumlar.Clear();
    }

    //boslukları şekerlerle dolduran metodlar
    public void Doldur()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (table[i, j] == 9)
                {
                    bool durdur = true;
                    while (durdur)
                    {
                        SekerDoldur(i, j);
                        if (table[i, j] != 9)
                        {
                            durdur = false;
                        }
                    }
                }
            }
        }
        
    }
    public void SekerDoldur(int satir, int sutun)
    {
        int temp;
        for (int i = satir; i > 0; i--)
        {
            temp = table[i, sutun];
            table[i, sutun] = table[i - 1, sutun];
            table[i - 1, sutun] = temp;
        }
        table[0, sutun] = yenideger();
    }

    //sekerlere ve jokerlere göre random değer üreten fonksiyon
    public int yenideger()
    {
        Random random = new Random();
        int sans = random.Next(51);
        if (sans == 1)
        {
            return 4;
        }
        sans = random.Next(51);
        if (sans == 1)
        {
            return 5;
        }
        sans = random.Next(51);
        if (sans == 1)
        {
            return 6;
        }
        sans = random.Next(72);
        if (sans == 1)
        {
            return 7;
        }
        sans = random.Next(82);
        if (sans == 1)
        {
            return 8;
        }
            int value = random.Next(4);
            return value;
    }

    //joker özelliklerine göre seker patlatan metodlar
    public void joker(int row, int col, int row2, int col2,int jkr)
    {
        
        if (jkr == 6)
        {
            table[row, col] = 9;
            table[row2, col2] = 9;
        }
        else
        {
            if (table[row2, col2]<4)
            {
                int tut = table[row2, col2];
                table[row, col] = 9;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (table[i, j]==tut)
                        {
                            table[i, j] = 9;
                        }
                    }
                }
            }
        }
    }

    public void joker(int row, int col, int jkr)
    {
        if (jkr == 4)
        {
            for (int i = 0; i < 9; i++)
            {
                table[i, col] = 9;
            }
        }

        else if (jkr == 5)
        {
            for (int i = 0; i < 9; i++)
            {
                table[row, i] = 9;
            }
        }
        else if(jkr == 7)
        {
            table[row, col] = 9;
            if (row+1!=9)
            {
                table[row + 1, col] = 9;
            }
            if (row-1!=-1)
            {
                table[row - 1, col] = 9;
            }
            if (col+1!=9)
            {
                table[row, col+1] = 9;
            }
            if (col-1!=-1)
            {
                table[row, col - 1] = 9;
            }
            if (row+1!=9 && col+1!=9)
            {
                table[row + 1, col+1] = 9;
            }
            if (row-1!=-1 && col-1!=-1)
            {
                table[row - 1, col-1] = 9;
            }
            if (col+1!=9 && row-1!=-1)
            {
                table[row-1, col+1] = 9;
            }
            if (col-1!=-1 && row+1!=9)
            {
                table[row+1, col - 1] = 9;
            }
        }
    }
}