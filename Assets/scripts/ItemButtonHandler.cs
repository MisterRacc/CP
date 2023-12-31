using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemButtonHandler : MonoBehaviour
{
    public GameObject itemInfoPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Button useItemButton;

    private string itemName;
    private string itemDescription;
    private string itemId;
    private bool onDrugs;
    
    private Joystick js;

    private LogicScript logic1;
    private PlayerScript player1;

    private Level2LogicScript logic2;
    private BunnyController2 bunny;

    private LogicLevel3 logic3;
    private HookScript player3;

    private LogicLevel4 logic4;
    private TurtleScript player4;

    private LogicLevel5 logic5;
    private PlayerLvl5Script player5;

    private PowerupTimer powerupTimer;
    private PowerupDisplay powerupDisplay;

    // Start is called before the first frame update
    void Start()
    {
        js = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Joystick>();

        if (SceneManager.GetActiveScene().name != "InventoryScreen"){
            powerupTimer = FindObjectOfType<PowerupTimer>();
            powerupDisplay = FindObjectOfType<PowerupDisplay>();

            if (powerupTimer == null){
                Debug.LogError("PowerupTimer not found in the scene.");
            }
        }

        if (SceneManager.GetActiveScene().name != "InventoryScreen"){
            if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1")
            {
                logic1 = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
                player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
            }
            else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2")
            {
                logic2 = GameObject.FindGameObjectWithTag("Logic").GetComponent<Level2LogicScript>();
                bunny = GameObject.FindGameObjectWithTag("Bunny").GetComponent<BunnyController2>();
            }
            else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 3")
            {
                logic3 = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel3>();
                player3 = GameObject.FindGameObjectWithTag("Player").GetComponent<HookScript>();
            }
            else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 4")
            {
                logic4 = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel4>();
                player4 = GameObject.FindGameObjectWithTag("Player").GetComponent<TurtleScript>();
            }
            else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 5")
            {
                logic5 = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel5>();
                player5 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLvl5Script>();
            }
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
        if(SceneManager.GetActiveScene().name != "Inventory Screen"){
            if (((PlayerPrefs.GetString("CurrentLevel", "Default") != "Level 1" && PlayerPrefs.GetString("CurrentLevel", "Default") != "Level 2") && (itemName == "Fire Resistance Potion" || itemName == "Invisible Potion"))
            || (PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 6" && itemName == "Health Potion")){
                    useItemButton.interactable = false;
            }
            else{
                useItemButton.onClick.AddListener(() => UseItem(itemId));
            }
        }
    }

    public void OnCloseButtonClick(){
        useItemButton.interactable = true;
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

    void ApplyEffect(string name)
    {
        if(!GetDrugs())
        {
            switch(name)
            {
                case "Energy Gel":
                    SetDrugs(true);
                    js.ActivateSpeedBoost(150);
                    powerupTimer.StartTimer(10f);
                    powerupDisplay.DisplayPowerup(name);
                    break;

                case "Health Potion":
                    if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1") logic1.increaseLives(1);
                    else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2") logic2.increaseLives(1);
                    else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 3") logic3.increaseLives(1);
                    else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 4") logic4.increaseLives(1);
                    else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 5") logic5.increaseLives(1);
                    break;

                case "Fire Resistance Potion":
                    SetDrugs(true);
                    if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1") player1.SetFire();
                    else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2") bunny.SetFire();
                    powerupTimer.StartTimer(20f);
                    powerupDisplay.DisplayPowerup(name);
                    break;

                case "Invisible Potion":
                    SetDrugs(true);
                    if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 1") player1.SetProjectile();
                    else if(PlayerPrefs.GetString("CurrentLevel", "Default") == "Level 2") bunny.SetProjectile();
                    powerupTimer.StartTimer(10f);
                    powerupDisplay.DisplayPowerup(name);
                    break;
            }
        }
    }

    public void SetDrugs(bool boolean)
    {  
        onDrugs = boolean;
    }

    public bool GetDrugs()
    {
        return onDrugs;
    }
}