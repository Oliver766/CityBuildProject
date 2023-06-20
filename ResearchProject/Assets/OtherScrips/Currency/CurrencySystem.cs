using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencySystem : MonoBehaviour
{
    private static Dictionary<CurrencyType, int> CurrencyAmounts = new Dictionary<CurrencyType, int>();
    [SerializeField] private List<GameObject> texts;

    private Dictionary<CurrencyType, TextMeshProUGUI> CurrencyTexts = new Dictionary<CurrencyType, TextMeshProUGUI>();


    private void Awake()
    {
        for(int i = 0; i < texts.Count; i++)
        {
            CurrencyAmounts.Add((CurrencyType)i, 0);
            CurrencyTexts.Add((CurrencyType)i, texts[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>());

        }
    }

    private void Start()
    {
        EventManager.Instance.AddListener<CurrencyChangeEvent>(onCurrencyChange);
        EventManager.Instance.AddListener<NotEnoughCurrencyEvent>(OnNotEnough);
    }


    private void  onCurrencyChange(CurrencyChangeEvent info)
    {
        // save the currency

        CurrencyAmounts[info.currencyType] += info.amount;
        CurrencyTexts[info.currencyType].text = CurrencyAmounts[info.currencyType].ToString();

    }


    private void OnNotEnough(NotEnoughCurrencyEvent info)
    {
        Debug.Log(message: $"You don't have enough");
    }

}
public enum CurrencyType
{
    Coins,
    Crystals,
}


