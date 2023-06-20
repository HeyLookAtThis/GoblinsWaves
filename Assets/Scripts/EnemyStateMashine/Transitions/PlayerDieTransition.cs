using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieTransition : Transition
{    
    private void Update()
    {
        if(Target.gameObject.activeSelf == false)
            NeedTransit = true;
    }
}
