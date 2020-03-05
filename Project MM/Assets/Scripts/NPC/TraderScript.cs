using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TraderScript : MonoBehaviour
{
    public GameObject soldGood;
    public string wantedGood1;
    public string wantedGood2;
    public string wantedGood3;
    public string openingText;
    public string endingText;
    public int wantedCoinAmount;
    public Text text;
    public bool transactionMade;
    public int activationCharges;
    public bool wantsCoin;
    public bool wantsGoods;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(ItemInventory.II.coins);
        if (activationCharges <= 0 && transactionMade == false && wantsGoods == true)
        {
            transactionMade = true;
            Instantiate(soldGood, new Vector2(transform.position.x, transform.position.y - 2), transform.rotation);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transactionMade == true)
            {
                text.text = endingText;
            }
            else
            {
                text.text = openingText;
            }
            //chacks for a specific item
            if (Input.GetKey(KeyCode.Space))
            {

                foreach (string s in ItemInventory.II.items)
                {
                    if (s == wantedGood1 && transactionMade == false)
                    {
                        activationCharges--;
                        ItemInventory.II.items.Remove(wantedGood1);
                        ItemInventory.II.UpdateInv();
                    }

                    if (s == wantedGood2 && transactionMade == false)
                    {
                        activationCharges--;
                        ItemInventory.II.items.Remove(wantedGood2);
                        ItemInventory.II.UpdateInv();
                    }

                    if (s == wantedGood3 && transactionMade == false)
                    {
                        activationCharges--;
                        ItemInventory.II.items.Remove(wantedGood3);
                        ItemInventory.II.UpdateInv();
                    }
                }
                    
                
            }
            //checks for a specific amount of coins
            if (ItemInventory.II.coins >= wantedCoinAmount && Input.GetKeyUp(KeyCode.Space) && transactionMade == false && wantsCoin == true)
            {
                transactionMade = true;
                Instantiate(soldGood, new Vector2(transform.position.x, transform.position.y - 2), transform.rotation);
                ItemInventory.II.coins -= wantedCoinAmount;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.text = null;
        }
    }
}
