using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird {

    //重写
    public override void ShowSkill()
    {
        base.ShowSkill();
        rg.velocity *= 2;//加速
    }
}
