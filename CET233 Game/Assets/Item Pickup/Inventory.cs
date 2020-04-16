using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Item  GameObject variables
    [SerializeField]
    private GameObject item1;
    [SerializeField]
    private GameObject item2;
    [SerializeField]
    private GameObject item3;
    [SerializeField]
    private GameObject item4;

    //Text Variables
    [SerializeField]
    private Text txtPress;
    [SerializeField]
    private Text items;

    //Run time GameObject variables
    private GameObject droppedItem;
    public GameObject itemNear;
    public GameObject heldItem;

    //others
    private string itemName;
    private bool inrange;
    private bool hasItem;


    private void Start()
    {
        hasItem = false;
    }


    void Update()
    {
        DropItem();

        PickupItem();

    }

    // if player has an item and presses E he instantiates an instance of it and the helditem variable
    // becomes null
    public void DropItem()
    {
        if (hasItem && Input.GetKeyDown(KeyCode.E))
        {
            if (itemName == "item1")
            {
                heldItem = item1;
            }
            else if (itemName == "item2")
            {
                heldItem = item2;
            }
            else if (itemName == "item3")
            {
                heldItem = item3;
            }
            else if (itemName == "item4")
            {
                heldItem = item4;
            }

            items.text = "Item Equipped: NO ITEM";

            droppedItem = Instantiate(heldItem, transform.position + (transform.forward * 2), transform.rotation);
            droppedItem.name = itemName;


            hasItem = false;
            Debug.Log("item dropped");

        }
    }

    // if player is near an item and he doesnt already have one, item gets destroyed and said item is stored in
    // a held item variable
   
    public void PickupItem()
    {

        if (inrange && Input.GetKeyDown(KeyCode.E) && !hasItem)
        {
            hasItem = true;
            itemName = itemNear.name;
            items.text = "Item Equipped: " + itemName;

            Destroy(itemNear);

            txtPress.enabled = false;
            Debug.Log("item picked up");
        }

    }



    // When player near an item txt to press E gets enabled, and the item is sotred in a variable
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("item"))
        {
            itemNear = other.gameObject;
            txtPress.enabled = true;
            Debug.Log("Player in range");
            inrange = true;
        }
    }

    //when player is no longer near an object itemnear variable becomes null and the txt if disabled
    private void OnTriggerExit(Collider other)
    {
        itemNear = null;

        if (other.CompareTag("item"))
        {
            txtPress.enabled = false;
            Debug.Log("Player out of range");
            inrange = false;
        }
    }
}
