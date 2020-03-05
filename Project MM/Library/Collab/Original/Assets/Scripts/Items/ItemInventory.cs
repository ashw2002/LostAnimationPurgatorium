using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.Linq;

public class ItemInventory : MonoBehaviour
{
    public static ItemInventory II { get; private set; }
    
    public List<GameObject> InvContainers = new List<GameObject>();
    public List<string> items = new List<string>();
    public bool InventoryOpen;
    public bool canCollect;
    public GameObject[] Items;
    public GameObject currentCollectable;
    public GameObject playerObject;
    private GameObject InventoryContainer;
    private Transform InvContTransform;
    [SerializeField] Transform Canvas;
    private float InvContTransformSize;
    public int coins;

    public void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        II = this;
        InvContTransform = GameObject.Find("InventoryContainer").GetComponent<Transform>();
        InventoryContainer = GameObject.Find("InventoryContainer");
        InvContTransformSize = InvContTransform.GetChild(0).GetComponent<RectTransform>().rect.height;
        Debug.Log(InvContTransformSize);
        if(SaveSystem.isLoaded == true)
        {
            Debug.Log("Loaded");
            SaveSystem.LoadPlayer();
        }

        //Ideally One would destroy this Object, but no matter how I delete it, and no matter if I create a variable that stores the same information
        //I need the Object to use it.

        //I could possibly get the position.x of the object and save it in start, but that isn't very important

        //InvContTransform.gameObject.SetActive(false);
        //Destroy(InvContTransform.gameObject);

    }

    public void onGameLoad()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        items = data.inventory;

        coins = data.coins;

        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(canCollect);
        //Checks if the player has pressed k, Opens Inventory
        if (Input.GetKeyDown(KeyCode.K)){
            OpenInventory();
        }
        //Checks if the player can collect an item
        CollectCheck();
        
    }

    public void CreateInventoryContainer(string s){
        /* Sudo Code
         *  GameObject Obj;
            Obj = Instantiate();
            SetParent(Canvas, false);
            InvContainers.Add(Obj);
            SetImg(Obj)
         */
        GameObject Obj;
        //This is the instantiation for the very first Inventory Object
        //This is Necessary because otherwise the math has issues
        if(InvContainers.Count <= 0){
            //Instantiates Object at Proper Position (Since the Object is a square the proper x and y position is at half of the height or width
            //in this case I use height to calculate the position)
            //It's parent is then set the the Canvas
            //It must be done this way, because the other, simpler way, causes many bugs.
            Obj = Instantiate(InventoryContainer, new Vector3(InvContTransformSize / 2, InvContTransformSize / 2, 0), Quaternion.identity) as GameObject;
            Debug.Log("test2");
            Obj.transform.SetParent(Canvas, false);
            Debug.Log("test3");
        }
        else{
            //Instantiation for all other Inventory Objects
            //The X position calculation stays the same, but I need to change the y based on how many Inventory Objects are there
            //The Rect height is multiplied by the Count of the InvContainers List this sets the position quite well, but we also need to compensate for the very first Object
            //This is accomplished with adding the same y position calculation done for the First Object to the Calculation done with The transform size times the count.
            //Sets the parent, same as the first Object
            Obj = Instantiate(InventoryContainer, new Vector3(InvContTransformSize / 2, (InvContTransformSize * (InvContainers.Count) + InvContTransformSize / 2), 0), Quaternion.identity) as GameObject;
            Obj.transform.SetParent(Canvas, false);

        }
        //Adds Object to the List
        //Calls the SetImg Function
        //Destroys the Object that the player collected
        InvContainers.Add(Obj);
        SetImg(Obj, s);
        Destroy(currentCollectable);
        
    }

    public void SetImg(GameObject g, string s) {
        //Creates "Temporary" variables
        Image img;
        Sprite sprite;
        //Gets Image Component of the Inventory Container/Object
        //Uses .getChild because it is summoned through a prefab and should never change
        img = g.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        //Gets Sprite of the Object the Player Collected
        //sprite = c.GetComponent<SpriteRenderer>().sprite;
        sprite = Resources.Load<Sprite>(s);
        //Sets the Image's Sprite to the Collectable's Sprite
        img.sprite = sprite;

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("enter");
        if (col.CompareTag("Collectable"))
        {
            //Must use a boolean to avoid Composite Colliders
            //Otherwise the player collects items twice
            canCollect = true;
            //sets currentCollectable to the collided Object
            currentCollectable = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("exit");
        if (col.CompareTag("Collectable")){
            canCollect = false;
            currentCollectable = null;
        }
    }
    public void CollectCheck()
    {
        //Checks if canCollect is true because of composite colliders and checks
        //If InventoryOpen == false because we don't want the player to collect items
        //While their Inventory is Open
        if (canCollect == true && InventoryOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(currentCollectable.name == "Coin"){
                    coins++;
                    canCollect = false;
                    PlayerInteractionScript.PIS.coinReadout.text = "Num Coins: " + coins;
                    currentCollectable.gameObject.SetActive(false);
                    Debug.Log("Collected Coin");
                }
                else{
                    //Calls CreateInventoryContainer with the currentCollectable, required for SetImg function
                    //composite collider prevention
                    canCollect = false;
                    items.Add(currentCollectable.name);
                    UpdateInv();
                    Debug.Log("test");
                }

            }
        }
    }   //Keeping incase something breaks lol
    public void OpenInventory(){
        //Different Loops because we want to change the position depending on whether the inventory is open or not
        if (!InventoryOpen){
            for (int i = 0; i < InvContainers.Count; i++)
            {
                //Sets the InventoryObject's Position to the - of the Container's X position * 2 because the
                //Anchor point is in the middle
                //Multiplies by Inventory Container Count to decide how far down it should go
                InvContainers[i].transform.Translate(0f, -InvContainers[i].transform.position.x * 2 * InvContainers.Count, 0f);
            }
        }
        else {
            for (int i = 0; i < InvContainers.Count; i++)
            {
                //Sets the InventoryObject's Position to the + of the Container's X position * 2 because the
                //Anchor point is in the middle
                //Multiplies by Inventory Container Count to decide how far down it should go
                InvContainers[i].transform.Translate(0f, InvContainers[i].transform.position.x * 2 * InvContainers.Count, 0f);
            }
        }
        //flips boolean to the opposite to change whether the inventory is open or not.
        InventoryOpen = !InventoryOpen;
    }

    public void UpdateInv(){
        DeleteAllContainers();
        int i = 0;
        foreach (string s in items.ToList()){
            CreateInventoryContainer(s);
            //Debug.Log(items.Count + "Items Count");
            //Debug.Log(InvContainers.Count + "Inventory Count");
            i++;
        }
    }

    public void DeleteAllContainers(){
        int i = 1;
        foreach (GameObject g in InvContainers.ToList()){
            //Debug.Log(i + " = int i");
            InvContainers.Remove(InvContainers[InvContainers.Count -1]);
            Destroy(g);
        }
    }
}
