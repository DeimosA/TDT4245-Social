using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BusinessFeatureTitle
{
    Images,
    Videos,
    Gif_Keyboards,
    Stories,
    Hacking,
    Basic_Encryption,
    Routine_Backups,
    End_To_End_Encryption,
    NSA_Backdoor,
    Text,
    Friend_System,
    Commenting,
    Favorites,
    User_blocking,
    Archiving
}

[System.Serializable]
public class BusinessFeature
{
    public BusinessFeatureTitle title;
    public int cost;
    public bool purchased;
    public List<StatChange> statChangesPerTurn;


    public bool CanBePurchased(PlayerStatIntDictionary playerStats)
    {
        return (playerStats[PlayerStat.capital] >= cost);
    }

    public void SetPurchased(bool purchased)
    {
        this.purchased = purchased;
    }


}
