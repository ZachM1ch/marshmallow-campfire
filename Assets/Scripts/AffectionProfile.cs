using System;

public class AffectionProfile
{
    private int affection;
    private int likedItemsRetrieved;
    private int likedItemsIgnored;
    private int likedItemsRemoved;
    private int fuelRetrieved;
    private int breaksTaken;
    private int stepsWithBadItemsNear;


    public int Affection => affection;
    public int LikedItemsRetrieved => likedItemsRetrieved;
    public int LikedItemsIgnored => likedItemsIgnored;
    public int LikedItemsRemoved => likedItemsRemoved;
    public int FuelRetrieved => fuelRetrieved;
    public int BreaksTaken => breaksTaken;
    public int StepsWithBadItemsNear => stepsWithBadItemsNear;


    public void RetrievedItem(System.Object item) //Item item)
    {
        int amount = CalculateAffectionChange(item);
        RaiseAffection(amount);
        likedItemsRetrieved++;
    }

    private int CalculateAffectionChange(System.Object item) //Item item)
    {
        // Placeholder logic
        return 1; // item.IsSpecial ? 10 : 2;
    }

    private void RaiseAffection(int amount)
    {
        affection += amount;
    }
}

