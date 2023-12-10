using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonHandler : MonoBehaviour
{
    public GameObject itemInfoPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;

    private string itemName;
    private string itemDescription;

    public void SetItemInfo(string name, string description)
    {
        itemName = name;
        itemDescription = description;
    }

    public void OnButtonClick()
    {
        // Ativar o painel de informações do item
        itemInfoPanel.SetActive(true);

        // Atualizar o nome e descrição do item no painel
        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
    }
}