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

    private Leve2LogicScript logic2;
    private BunnyController2 bunny;

    // Start is called before the first frame update
    void Start()
    {
        js = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Joystick>();

        if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1")
        {
            logic1 = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        }
        else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2")
        {
            logic2 = GameObject.FindGameObjectWithTag("Logic").GetComponent<Leve2LogicScript>();
            bunny = GameObject.FindGameObjectWithTag("Bunny").GetComponent<BunnyController2>();
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
                js.ActivateSpeedBoost(100);
                break;

            case "Health Potion":
                if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1") logic1.increaseLives(1);
                else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2") logic2.increaseLives(1);
                break;

            case "Fire Resistance Potion":
                if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1") player1.SetFire();
                else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2") bunny.SetFire();
                break;

            case "Invisible Potion":
                if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1") player1.SetProjectile();
                else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2") bunny.SetProjectile();
                break;
        }
    }
}