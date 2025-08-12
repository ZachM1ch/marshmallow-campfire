using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CharacterAffection : MonoBehaviour
{
    private AffectionProfile affectionProfile;

    public int CurrentAffection => affectionProfile.Affection;

    void Awake()
    {
        affectionProfile = new AffectionProfile();
    }

    public void RetrievedItem(ObtainableItem item)
    {
        affectionProfile.RetrievedItem(item);
    }    
}
