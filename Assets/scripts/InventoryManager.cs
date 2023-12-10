using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    private Dictionary<string, string> itemImageMapping = new Dictionary<string, string>
    {
        {"Fire Resistance Potion", "Fire resistance"},
        {"Energy Gel", "Energy gel"},
    };

    public List<Button> itemButtons;

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
            Debug.Log($"Item: {item.DisplayName}, Quantidade: {item.RemainingUses}");

            if (itemImageMapping.ContainsKey(item.DisplayName))
            {
                string imagePath = itemImageMapping[item.DisplayName];
                Debug.Log($"Imagem: {imagePath}");
                SetButtonImage(itemIndex, imagePath);
                itemIndex++;
            }
        }
    }

    private void SetButtonImage(int itemIndex, string imagePath)
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

    public void AddItem(string itemId, int quantity)
    {
        if (inventory.ContainsKey(itemId))
            inventory[itemId] += quantity;
        else
            inventory[itemId] = quantity;
    }

    public void RemoveItem(string itemId, int quantity)
    {
        if (inventory.ContainsKey(itemId))
        {
            inventory[itemId] -= quantity;
            if (inventory[itemId] <= 0)
                inventory.Remove(itemId);
        }
    }

    public void ConsumeItem(string itemId)
    {
        if (inventory.ContainsKey(itemId) && inventory[itemId] > 0)
        {
            // Implemente a lógica para consumir o item aqui

            // Por exemplo, se for uma cura, adicione lógica de cura ao jogador.
            // Se for uma moeda, adicione lógica para aumentar a pontuação, etc.

            // Após o consumo, remova o item do inventário
            RemoveItem(itemId, 1);
        }
        else
        {
            Debug.LogWarning("Item não encontrado no inventário ou quantidade insuficiente.");
        }
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
