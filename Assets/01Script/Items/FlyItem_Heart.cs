using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem_Heart : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreMGR.PlayerHpChange(true);
    }

}
