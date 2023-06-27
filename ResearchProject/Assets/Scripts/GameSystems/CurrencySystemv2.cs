using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CurrencySystemv2 : MonoBehaviour
{
    
    public float amount;
    public Button road;
    public Button house;
    public Button special;
    public Button bighouse;

    public void Start()
    {
        amount = 40;   
    }

    public void Update()
    {
        if(amount == 0) 
        {

            road.enabled = false;
            house.enabled = false;
            special.enabled = false;
            bighouse.enabled = false;

        }
        else if( amount < 0)
        {
            Gameover();
        }
        else if( amount > 0)
        {
            road.enabled = true;
            house.enabled = true;
            special.enabled = true;
            bighouse.enabled = true;
        }
    }

    public void Gameover()
    {
        Time.timeScale = 0;
        Debug.Log("game over");
    }

    public IEnumerator AddCoins()
    {
        while (true)
        {
            amount += 100;

            yield return new WaitForSeconds(1);
        }
    

    }
    public IEnumerator TakeAwayCoins()
    {
        while (true)
        {
            amount -= 5;

            yield return new WaitForSeconds(1);
        }


    }



    // todo
    // add currency base that can be used in other scripts done
    // add when currency is zero then you can't build anything done
    // add system where you can gradually own money over time to do
    // can loose money when  shops on grid todo
    // play can loose when bankedrupt done
}
