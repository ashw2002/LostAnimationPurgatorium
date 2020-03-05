using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory PI { get; private set; }
    //put item from checkitems into this list
    public List<string> items = new List<string>();
    public bool canCollect;
    public Sprite itemSprite;
    public GameObject currentCollectable;
    public Image itemImg;

    public void Update()
    {
        CollectCheck();
    }

    public void Start()
    {
        PI = this;
    }
    //When the player collides with an object it checks if its a collectable
    //Then it runs check item
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Collectable"))
        {
            canCollect = true;
            currentCollectable = col.gameObject;
            Debug.Log("eeee");
        }
    }

    public void UpdateTransparancy() {
        var color = itemImg.color;
        color.a = 1f;
        itemImg.color = color;
    }

    public void CollectCheck(){
        if(canCollect == true){
            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("e2");
/*              items.Add(currentCollectable.gameObject.name);
                itemSprite = currentCollectable.gameObject.GetComponent<SpriteRenderer>().sprite;
                */
                OpenInventory.OI.CreateInventoryObject();
                canCollect = false;
            }
        }
    }

    //Writes most recently collected Item's Name to Item Text
    //Changes Item Image to Item's Sprite
    public void UpdateItemUI()
    {
        //int itemsNum = items.Count;
        //GameObject.Find("UITest").GetComponent<Text>().text = items[itemsNum - 1];

        items.Add(currentCollectable.gameObject.name);
        itemSprite = currentCollectable.gameObject.GetComponent<SpriteRenderer>().sprite;


        itemImg = GameObject.Find("ImgTest").GetComponent<Image>();
        UpdateTransparancy();
    }

}
