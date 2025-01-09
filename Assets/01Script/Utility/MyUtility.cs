using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtility : MonoBehaviour
{
    public static Transform FindChildRescursive(Transform parent, string targetName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
                return child;

            Transform findTrnas = FindChildRescursive(child, targetName); // Àç±ÍÇÔ¼ö

            if(findTrnas != null)
                return findTrnas;
        }
        return null;
    }
}
