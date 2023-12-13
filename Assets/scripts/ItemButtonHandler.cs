using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonHandler : MonoBehaviour
{
    public GameObject itemInfoPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Button useItemButton;

    private string itemName;
    private string itemDescription;
    private string itemId;

    public void SetItemInfo(string name, string description, string ItemId)
    {
        itemName = name;
        itemDescription = description;
        itemId = ItemId;
    }

    public void OnButtonClick()
    {
        // Ativar o painel de informações do item
        itemInfoPanel.SetActive(true);

        // Atualizar o nome e descrição do item no painel
        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        useItemButton.onClick.AddListener(() => UseItem(itemId));
    }

    public void OnCloseButtonClick(){
        itemInfoPanel.SetActive(false);
    }

    public void UseItem(string itemId){
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryManager.ConsumeItem(itemId);

        OnCloseButtonClick();

        InventoryHandler InventoryHandler = FindObjectOfType<InventoryHandler>();
        InventoryHandler.CloseInventory();
    }
}