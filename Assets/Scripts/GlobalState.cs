using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalState
{
    public static int EnemyIndex = 0;

    public static Dictionary<Attributes, int> PotionAttributes = new Dictionary<Attributes, int>();

    public static void Reset()
    {
        EnemyIndex = 0;
        PotionAttributes.Clear();
    }




}

