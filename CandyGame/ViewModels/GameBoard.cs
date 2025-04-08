using CandyGame.Views;
namespace CandyGame.ViewModels;

public class GameBoard : GameWindow.IImageTake
{
    public string locate { get; set; }
    public string ImageLocation(int location)
    {
        int candyIndex = location;
                        if (candyIndex == 0)
                        {
                            BlueCandy blue = new BlueCandy();
                            locate = blue.GetImagePath();
                        }
                        else if (candyIndex == 1)
                        {
                            GreenCandy green = new GreenCandy();
                            locate = green.GetImagePath();
                        }
                        else if (candyIndex == 2)
                        {
                            YellowCandy yellow = new YellowCandy();
                            locate = yellow.GetImagePath();
                        }
                        else if (candyIndex == 3)
                        {
                            RedCandy red = new RedCandy();
                            locate = red.GetImagePath();
                        }
                        else if (candyIndex == 4)
                        {
                            RoketDikey roketdikey = new RoketDikey();
                            locate = roketdikey.GetImagePath();
                        }
                        else if (candyIndex == 5)
                        {
                            RoketYatay roketyatay = new RoketYatay();
                            locate = roketyatay.GetImagePath();
                        }
                        else if (candyIndex == 6)
                        {
                            Kopter kopter = new Kopter();
                            locate = kopter.GetImagePath();
                        }
                        else if (candyIndex == 7)
                        {
                            Bomba bomb = new Bomba();
                            locate = bomb.GetImagePath();
                        }
                        else
                        {
                            Gokkusagi gokkusagi = new Gokkusagi();
                            locate = gokkusagi.GetImagePath();
                        }

        return locate;
    }
}