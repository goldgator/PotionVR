using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{

    public List<Potion.PotionInfo> potionEffects = new List<Potion.PotionInfo>();

    private static PotionController instance;
    public static PotionController Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    //Finds if there is a mix, if not, returns null
    public Potion.PotionInfo FindMix(string name1, string name2)
    {
        foreach(Potion.PotionInfo potion in potionEffects)
        {
            if (potion.componentNames.Count == 0) continue;
            if ((name1 == potion.componentNames[0] && name2 == potion.componentNames[1]) || 
                (name2 == potion.componentNames[0] && name1 == potion.componentNames[0]))
            {
                return potion;
            }
        }

        return null;
    }

}
