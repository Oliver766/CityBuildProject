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

    //[SerializeField] GameObject pointChange;
    //[SerializeField] Transform pointParent;
    //[SerializeField] RectTransform endPoint;
    //[SerializeField] Color colorGreen;
    //[SerializeField] Color colorRed;

    public TextMeshProUGUI CurrencyTXT;

    public bool MoneyUp = true;

    public RoadManager manager;

    public GameObject GameoverScreen;

    public GameObject managers;


    public TextMeshProUGUI populationtxt;

    public GameManager gameManager;
    public TextMeshProUGUI DayText;

    public DaysCounter counter;

    public GameObject HUD;

    public GameObject NumberPrefab;
    public Transform Parent;
    public Vector3 newPosition;
    public Quaternion newRotation;
    public GameObject NumberminusPrefab;
    public Transform Parenttwo;
    public Vector3 newPositiontwo;
    public Quaternion newRotationtwo;




    public void Start()
    {
        amount = 10000;
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


   

    public void Gameover()
    {
        Time.timeScale = 0;
        GameoverScreen.SetActive(true);
        Debug.Log("game over");
        managers.SetActive(false);
        HUD.SetActive(false);
        populationtxt.text = gameManager.Population.ToString();
        DayText.text = counter.dayCount.ToString();
    }

    public IEnumerator AddCoins()
    {
        while (MoneyUp)
        {
         
           Instantiate(NumberPrefab, newPosition, newRotation, Parent);
            amount += 1000;
            yield return new WaitForSeconds(1);
            Debug.Log("Add noww");
        }

    }  

    public IEnumerator TakeAaway()
    {
        while (MoneyUp)
        {
            Instantiate(NumberminusPrefab, newPositiontwo, newRotationtwo, Parenttwo);
            amount -= 500;
            yield return new WaitForSeconds(1);
       
          
        }
    }

    public void BuyItem(float Currentamount)
    {
        amount = amount - Currentamount;
    }

    //public IEnumerator PulseBig()
    //{
    //    for(float i = 1f; i <= 1.2f; i += 0.05f)
    //    {
    //        coinstxt.rectTransform.localScale = new Vector3(i, i, i);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    coinstxt.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

    //    amount -= 1000;
    //}

  




    // todo
    // add currency base that can be used in other scripts done
    // add when currency is zero then you can't build anything done
    // add system where you can gradually own money over time to do
    // can loose money when  shops on grid todo
    // play can loose when bankedrupt done
}
