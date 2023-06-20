using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPAddedGameEvent : GameEvent
{
    public int amount;

    public XPAddedGameEvent(int amount)
    {
        this.amount = amount;
    }
}
