using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem_Boom : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreMGR.ChangeBombCount(true);
    }
}
