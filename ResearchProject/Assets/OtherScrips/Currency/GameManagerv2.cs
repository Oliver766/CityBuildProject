using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerv2 : MonoBehaviour
{
    public static GameManagerv2 current;

    public GameObject canvas;

    private void Awake()
    {
        current = this;
    }

    public void GetXP(int amount)
    {
        XPAddedGameEvent info = new XPAddedGameEvent(amount);
        EventManager.Instance.QueueEvent(info);
    }

    public void GetCoins(int amount)
    {
        CurrencyChangeEvent info = new CurrencyChangeEvent(amount, CurrencyType.Coins);

        EventManager.Instance.QueueEvent(info);
    }


}
