using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{

    private Dictionary<string, string> itemNameToInstanceIdMapping = new Dictionary<string, string>();

    private Dictionary<string, string> itemImageMapping = new Dictionary<string, string>
    {
        {"Fire Resistance Potion", "Fire resistance"},
        {"Energy Gel", "Energy gel"},
        {"Health Potion", "Health Potion"},
        {"Invisible Potion", "Invisibility potion"}
    };

    public List<Button> itemButtons;

    public GameObject itemInfoPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Button useItemButton;

    public void GetPlayerInventory()
    {
        var request = new GetUserInventoryRequest();

        PlayFabClientAPI.GetUserInventory(request, OnGetUserInventorySuccess, OnGetUserInventoryFailure);
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        int itemIndex = 0;

        foreach (var item in result.Inventory)
        {

            if (itemImageMapping.ContainsKey(item.DisplayName))
            {
                string imagePath = itemImageMapping[item.DisplayName];
                GetCatalogItemDetails(item.ItemId, imagePath, item.DisplayName, itemIndex, item.ItemInstanceId);

                itemIndex++;
            }
        }
    }

    private void GetCatalogItemDetails(string itemId, string imagePath, string displayName, int itemIndex, string itemInstanceId)
    {
        var request = new GetCatalogItemsRequest();
        request.CatalogVersion = "Beta";  // Substitua pelo nome da sua versão de catálogo

        PlayFabClientAPI.GetCatalogItems(request, result =>
        {
            var itemDetails = result.Catalog.FirstOrDefault(catalogItem => catalogItem.ItemId == itemId);
            if (itemDetails != null)
            {
                string itemDescription = itemDetails.Description;

                SetButtonImage(itemIndex, imagePath, displayName, itemDescription, itemInstanceId);

            }
            else
            {
                Debug.LogWarning($"Detalhes do item não encontrados para o ItemId: {itemId}");
            }
        }, OnGetCatalogItemsFailure);
    }

    private void OnGetCatalogItemsFailure(PlayFabError error)
    {
        Debug.LogError("Falha ao obter detalhes do catálogo: " + error.ErrorMessage);
    }

    private void SetButtonImage(int itemIndex, string imagePath, string itemName, string itemDescription, string itemId)
    {
        // Certifique-se de que o índice está dentro dos limites da lista de botões
        if (itemIndex >= 0 && itemIndex < itemButtons.Count)
        {
            // Carregue a imagem no componente de imagem do botão
            Image imageComponent = itemButtons[itemIndex].GetComponent<Image>();

            if (imageComponent != null)
            {
                Sprite sprite = Resources.Load<Sprite>(imagePath);
                if (sprite != null)
                {
                    imageComponent.sprite = sprite;

                    GameObject buttonObject = new GameObject("ItemButton");
                    ItemButtonHandler buttonHandler = buttonObject.AddComponent<ItemButtonHandler>();
                    buttonHandler.itemInfoPanel = itemInfoPanel;
                    buttonHandler.itemNameText = itemNameText;
                    buttonHandler.itemDescriptionText = itemDescriptionText;
                    buttonHandler.useItemButton = useItemButton;
                    buttonHandler.SetItemInfo(itemName, itemDescription, itemId);

                    Button button = itemButtons[itemIndex].GetComponent<Button>();
                    if (button != null)
                    {
                        button.onClick.AddListener(buttonHandler.OnButtonClick);
                    }
                }
                else
                {
                    Debug.LogWarning($"Sprite não encontrado para o caminho: {imagePath}");
                }
            }
        }
    }


    private void OnGetUserInventoryFailure(PlayFabError error)
    {
        Debug.LogError("Falha ao obter inventário do jogador: " + error.ErrorMessage);
    }

    public void ConsumeItem(string itemId)
    {

            var request = new ConsumeItemRequest
            {
                ItemInstanceId = itemId,
                ConsumeCount = 1 
            };
            PlayFabClientAPI.ConsumeItem(request, result =>
            {
                Debug.Log("Item consumido com sucesso: " + itemId);

            }, OnConsumeItemFailure);

    }

    private void OnConsumeItemFailure(PlayFabError error)
    {
        Debug.LogError("Falha ao consumir item: " + error.ErrorMessage);
    }


    // Start is called before the first frame update
    void Start()
    {
        GetPlayerInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
