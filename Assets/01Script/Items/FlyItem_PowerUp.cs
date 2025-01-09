using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem_PowerUp : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreMGR.PowerUp();
    }

}
