using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

    public GameObject Inventory;
    public GameObject InventoryManager;

    public void OpenInventory(){
        Inventory.SetActive(true);
        Time.timeScale = 0f;
        InventoryManager.GetComponent<InventoryManager>().GetPlayerInventory();
    }

    public void CloseInventory(){
        Inventory.SetActive(false);
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
