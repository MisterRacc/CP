using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class InventoryHandler : MonoBehaviour
{

    public GameObject Inventory;
    public GameObject ItemInfoPanel;
    public GameObject InventoryManager;
    public List<Button> itemButtons;
    public Text messageText;

    public void OpenInventory(){
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            Inventory.SetActive(true);
            Time.timeScale = 0f;
            InventoryManager.GetComponent<InventoryManager>().GetPlayerInventory();
        } else {
            messageText.text = "You must be logged in to use the Inventory!";
            StartCoroutine(HideMessageTextAfterDelay(3f));
        }
    }

    private IEnumerator HideMessageTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = string.Empty; // Clear the messageText
    }

    public void CloseInventory(){
        ItemInfoPanel.SetActive(false);
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
