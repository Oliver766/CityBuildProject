using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CurrencySystemv2 : MonoBehaviour
{
    
    public float amount;
    public Button road;
    public Button house;
    public Button special;
    public Button bighouse;

    [SerializeField] GameObject pointChange;
    [SerializeField] Transform pointParent;
    [SerializeField] RectTransform endPoint;
    [SerializeField] Color colorGreen;
    [SerializeField] Color colorRed;

    public TextMeshProUGUI CurrencyTXT;

    public bool MoneyUp = true;

    public RoadManager manager;

    public GameObject GameoverScreen;

    public GameObject managers;

    public TextMeshProUGUI coinstxt;
    public TextMeshProUGUI populationtxt;

    public GameManager gameManager;

    public void Start()
    {
        amount = 1230;
    }





    public void Update()
    {
        if(amount == 0) 
        {

            road.enabled = false;
            house.enabled = false;
            special.enabled = false;
            bighouse.enabled = false;
            manager.enabled = false;

        }
        else if( amount <= 0)
        {
            Gameover();
        }
        else if( amount >= 10 &&  amount < 200)
        {
            road.enabled = true;
            house.enabled = false;
            special.enabled = false;
            bighouse.enabled = false;
            manager.enabled = true;
        }
        else if (amount >= 200 && amount < 500)
        {
            road.enabled = true;
            house.enabled = true;
            special.enabled = false;
            bighouse.enabled = false;
            manager.enabled = false;
        }
        else if (amount >= 500 && amount < 1000)
        {
            road.enabled = true;
            house.enabled = true;
            special.enabled = true;
            bighouse.enabled = false;
            manager.enabled = true;
        }
        else if (amount >= 1000)
        {
            road.enabled = true;
            house.enabled = true;
            special.enabled = true;
            bighouse.enabled = true;
            manager.enabled = true;
        }

        CurrencyTXT.text = amount.ToString();

      
    }


    public void ShowPointChange(int change)
    {
        var inst = Instantiate(pointChange, Vector3.zero,Quaternion.identity);
        inst.transform.SetParent(pointParent, true);

        RectTransform rect = inst.GetComponent<RectTransform>();

        Text text = inst.GetComponent<Text>();

        text.text = (change > 0 ? "+": "" )+ change.ToString();
        text.color = change > 0 ? colorGreen : colorRed;

        LeanTween.moveY(rect, endPoint.anchoredPosition.y, 1.5f).setOnComplete(() => {

            Destroy(inst);
        
        });
        LeanTween.alphaText(rect, 0.25f, 1.5f);



    }

    public void Gameover()
    {
        Time.timeScale = 0;
        GameoverScreen.SetActive(true);
        Debug.Log("game over");
        managers.SetActive(false);
        populationtxt.text = gameManager.Population.ToString();
        CurrencyTXT.text = amount.ToString();
    }

    public IEnumerator AddCoins()
    {
        while (MoneyUp)
        {
            amount += 40;
            yield return new WaitForSeconds(1);
            Debug.Log("Add noww");
        }
    }  

    public IEnumerator TakeAaway()
    {
        while (MoneyUp)
        {
            amount -= 200;

            yield return new WaitForSeconds(1);
        }
    }

    public void BuyItem(float Currentamount)
    {
        amount = amount - Currentamount;
    }




    // todo
    // add currency base that can be used in other scripts done
    // add when currency is zero then you can't build anything done
    // add system where you can gradually own money over time to do
    // can loose money when  shops on grid todo
    // play can loose when bankedrupt done
}
