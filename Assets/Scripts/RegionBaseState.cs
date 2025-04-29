using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RegionBaseState
{
    public abstract void Enter(RegionAI regionAI);
    public abstract void Tick(RegionAI regionAI);
    public abstract void Exit(RegionAI regionAI);


}
