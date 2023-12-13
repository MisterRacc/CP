using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{

    public GameObject Inventory;
    public GameObject InventoryManager;
    public List<Button> itemButtons;

    public void OpenInventory(){
        Inventory.SetActive(true);
        Time.timeScale = 0f;
        InventoryManager.GetComponent<InventoryManager>().GetPlayerInventory();
    }

    public void CloseInventory(){
        Inventory.SetActive(false);
        resetImages();
        Time.timeScale = 1f;
    }

    public void resetImages(){
        Sprite sprite = Resources.Load<Sprite>("BoxItemInventory");
        foreach (Button button in itemButtons)
        {
            button.GetComponent<Image>().sprite = sprite;
        }
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
