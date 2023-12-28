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
    private Joystick js;
    private LogicScript logic1;
    private PlayerScript player1;

    // Start is called before the first frame update
    void Start()
    {
        js = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Joystick>();
        if(PlayerPrefs.GetString("CurrentLevel","Default")=="Level 1"){
            logic1 = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemInfo(string name, string description, string ItemId)
    {
        itemName = name;
        itemDescription = description;
        itemId = ItemId;
    }

    public void OnButtonClick()
    {
        // Ativar o painel de informações do item
        useItemButton.onClick.RemoveAllListeners();
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

        ApplyEffect(itemName);

        OnCloseButtonClick();

        InventoryHandler InventoryHandler = FindObjectOfType<InventoryHandler>();
        InventoryHandler.CloseInventory();
    }

    void ApplyEffect(string name){
        switch(name)
        {
            case "Energy Gel":
                js.ActivateSpeedBoost(500);
                break;

            case "Health Potion":
                if(PlayerPrefs.GetString("CurrentLevel","Default")=="Level 1") logic1.increaseLives(1);
                // fazer else ifs pos outros niveis
                break;

            case "Fire Resistance Potion":
                if(PlayerPrefs.GetString("CurrentLevel","Default")=="Level 1") player1.SetFire();
                // fazer else ifs pos outros niveis
                break;

            case "Invisible Potion":
                if(PlayerPrefs.GetString("CurrentLevel","Default")=="Level 1") player1.SetProjectile();
                // fazer else ifs pos outros niveis
                break;
        }
    }
}