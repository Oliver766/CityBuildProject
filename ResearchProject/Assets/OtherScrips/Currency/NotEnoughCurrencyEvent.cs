using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughCurrencyEvent : GameEvent
{
    public int amount;
    public CurrencyType currencyType;

    public NotEnoughCurrencyEvent(int amount, CurrencyType currencyType)
    {
        this.amount = amount;
        this.currencyType = currencyType;
    }
}
