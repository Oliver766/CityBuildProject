using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// script by Oliver lancashire
// sid 1901981

public class CurrencySystemv2 : MonoBehaviour
{
    [Header("floats")]
    public float amount;
    [Header("UI")]
    public Button road;
    public Button house;
    public Button special;
    public Button bighouse;
    public TextMeshProUGUI CurrencyTXT;
    public TextMeshProUGUI populationtxt;
    public TextMeshProUGUI DayText;
    [Header("Bool")]
    public bool MoneyUp = true;
    [Header("References")]
    public GameManager gameManager;
    public RoadManager manager;
    public DaysCounter counter;
    [Header("Gameobjects")]
    public GameObject GameoverScreen;
    public GameObject managers;
    public GameObject HUD;
    public GameObject NumberPrefab;
    public GameObject NumberminusPrefab;
    [Header("vectors")]
    public Vector3 newPositiontwo;
    public Quaternion newRotationtwo;
    public Vector3 newPosition;
    public Quaternion newRotation;
    [Header("Transform")]
    public Transform Parenttwo;
    public Transform Parent;

    public void Start()
    {
        amount = 10000; // set amount
    }

    public void Update()
    {
        // enable buttons if certain amount
        if(amount == 0) 
        {

            road.enabled = false;
            house.enabled = false;
            special.enabled = false;
            bighouse.enabled = false;
            manager.enabled = false;

        }
        // loose state
        else if( amount <= 0)
        {
            Gameover();
        }
        // enable buttons if certain amount
        else if ( amount >= 10 &&  amount < 200)
        {
            road.enabled = true;
            house.enabled = false;
            special.enabled = false;
            bighouse.enabled = false;
            manager.enabled = true;
        }
        // enable buttons if certain amount
        else if (amount >= 200 && amount < 500)
        {
            road.enabled = true;
            house.enabled = true;
            special.enabled = false;
            bighouse.enabled = false;
            manager.enabled = false;
        }
        // enable buttons if certain amount
        else if (amount >= 500 && amount < 1000)
        {
            road.enabled = true;
            house.enabled = true;
            special.enabled = true;
            bighouse.enabled = false;
            manager.enabled = true;
        }
        // enable buttons if certain amount
        else if (amount >= 1000)
        {
            road.enabled = true;
            house.enabled = true;
            special.enabled = true;
            bighouse.enabled = true;
            manager.enabled = true;
        }

        CurrencyTXT.text = amount.ToString(); // update currency text

    }
    /// <summary>
    /// gameover function
    /// </summary>
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
    /// <summary>
    /// function that will keep adding money if bool is true
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// function that will keep taking away money if bool is true
    /// </summary>
    /// <returns></returns>
    public IEnumerator TakeAaway()
    {
        while (MoneyUp)
        {
            Instantiate(NumberminusPrefab, newPositiontwo, newRotationtwo, Parenttwo);
            amount -= 500;
            yield return new WaitForSeconds(1);
       
          
        }
    }

    
}
