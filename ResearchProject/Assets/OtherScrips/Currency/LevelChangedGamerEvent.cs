using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangedGamerEvent : GameEvent
{
    public int newLvl;

    public LevelChangedGamerEvent(int currLvl)
    {
        newLvl = currLvl;
    }
}
