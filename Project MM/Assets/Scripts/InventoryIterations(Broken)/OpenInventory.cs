using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenInventory : MonoBehaviour
{
    public static OpenInventory OI { get; private set; }
    public Transform Canvas;
    public Transform LastInvObject;
    public Object inventoryContainer;
    public bool inventoryOpen = true;
    public float inventoryObjectCount = 1;
    public float lastInvObjectSize;
    public Sprite lastInvObjectImageSprite;
    public Image lastInvObjectImage;
    public List<GameObject> inventoryObjects = new List<GameObject>();

    //Start is called when the scene is loaded
    private void Start()
    {
        OI = this;
        LastInvObject = GameObject.Find("InventoryContainer").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab)){
            //openInventory
            //CreateInventoryObject();
            
        }
        if (inventoryOpen){
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                //Select ++
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)){
                //Select --
            }
        }
    }

    public void CreateInventoryObject(){
        GameObject Obj;
        lastInvObjectSize = LastInvObject.GetChild(1).GetComponent<RectTransform>().rect.height;

        Obj = Instantiate(inventoryContainer, new Vector3(0, -(lastInvObjectSize * inventoryObjectCount * .75f), 0), Quaternion.identity)as GameObject;
        Obj.transform.SetParent(Canvas, false);

        LastInvObject = Obj.transform;
        inventoryObjectCount++;
        PlayerInventory.PI.UpdateItemUI();
        lastInvObjectImage = LastInvObject.GetChild(1).GetChild(0).GetComponent<Image>();
        lastInvObjectImageSprite = PlayerInventory.PI.itemSprite;

        lastInvObjectImage.sprite = lastInvObjectImageSprite;
    }
}
