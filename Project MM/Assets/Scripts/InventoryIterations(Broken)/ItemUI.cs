using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public static ItemUI IU { get; private set; }
    public Transform LastInvObject;
    public Transform Canvas;
    public Object InventoryContainer;
    public GameObject[] InvObject;
    public List<GameObject> InvContainers = new List<GameObject>();
    public GameObject currentCollectable;
    public bool canCollect;
    public int containerCounter = 0;
    public float lastInvObjectSize;
    public float inventoryObjectCount;


    // Start is called before the first frame update
    void Start()
    {
        IU = this;
        GetAllCollectableObjects();
        LastInvObject = GameObject.Find("InventoryContainer").GetComponent<Transform>();
        CreateAllContainers();


    }

    // Update is called once per frame
    void Update()
    {
        //Calls Collect Check to Add A Collectable.
        CollectCheck();
    }

    public void UpdateUI() {

    }


    //Gets all Collectable GameObjects On Start
    public GameObject[] GetAllCollectableObjects() {
        return InvObject = GameObject.FindGameObjectsWithTag("Collectable");
    }
    //Creates Containers, Puts them in order
    public void CreateAllContainers() {
        int c = InvObject.Length;
        lastInvObjectSize = LastInvObject.GetChild(1).GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < c; i++) {
            GameObject Obj;
            Obj = Instantiate(InventoryContainer, new Vector3(LastInvObject.transform.position.x, -(lastInvObjectSize * inventoryObjectCount), 0), Quaternion.identity) as GameObject;
            Obj.transform.SetParent(Canvas, false);

            inventoryObjectCount++;
            InvContainers.Add(Obj);
        }
        Destroy(LastInvObject.gameObject);
        InvContainers.Reverse();
    }
    //Changes Container's Image
    public void SetImg(){
        GameObject container;
        Image img;
        Sprite sprite;

        container = InvContainers[InvContainers.Count -1];
        img = container.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        sprite = currentCollectable.GetComponent<SpriteRenderer>().sprite;
        img.sprite = sprite;

        RemoveContainerFromPossibleContainers(container);
    }
    //Removes Container From List
    public void RemoveContainerFromPossibleContainers(GameObject container){
        InvContainers.Remove(container);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Collectable"))
        {
            canCollect = true;
            currentCollectable = col.gameObject;
            //Debug.Log(col.gameObject.name);
        }
    }

    public void CollectCheck()
    {
        if (canCollect == true)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SetImg();
                canCollect = false;
            }
        }
    }
}
