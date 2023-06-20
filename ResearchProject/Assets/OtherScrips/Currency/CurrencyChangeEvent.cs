using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyChangeEvent : GameEvent
{
    public int amount;
    public CurrencyType currencyType;

    public CurrencyChangeEvent(int amount, CurrencyType currencyType)
    {
        this.amount = amount;
        this.currencyType = currencyType;
    }
}
