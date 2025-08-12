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


    public void RetrievedItem(ObtainableItem item)
    {
        int amount = CalculateAffectionChange(item);
        RaiseAffection(amount);
        likedItemsRetrieved++;
    }

    private int CalculateAffectionChange(ObtainableItem item)
    {
        int affectionAmount = 0;

        if (item.IsSpecial)
        {
            affectionAmount += 50;
        }
        if (item.IsBurnable)
        {
            affectionAmount += 10;
        }
        if (item.IsTrash)
        {
            affectionAmount += 10;
        }
        if (item.IsWet)
        {
            affectionAmount += -15;
        }
        if (item.IsSoaked)
        {
            affectionAmount += -20;
        }

        return affectionAmount;
    }

    private void RaiseAffection(int amount)
    {
        affection += amount;
    }
}

