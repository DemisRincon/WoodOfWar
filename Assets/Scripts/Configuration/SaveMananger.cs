using UnityEngine;

public class SaveMananger : MonoBehaviour {
    
    public static SaveMananger Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        
        
        Instance = this;
        Load();
    
    }
    //save states of the script
    public void Save()
    {
        PlayerPrefs.SetString("save",HelperSave.Serialize<SaveState>(state));

    }
    //load previous data
    public void Load()
    {
        // do we have data??
        if (PlayerPrefs.HasKey("save"))
        {
            state = HelperSave.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
            Debug.Log("tsave file found");
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("no save file found");
        }
    }
    //check if the weapon is owned
    public bool isWeaponOwned(int index)
    {
        //check if bit is set
        
        return (state.swordOwned & ( 1<<index )) != 0;
    }

    public int actualScene()
    {
        return (state.CompletedLevel);
    }

    public bool scenePassed()
    {
        Debug.Log(state.CompletedLevel);
        state.CompletedLevel += 1;
        Debug.Log(state.CompletedLevel);
        return true;
    }


    public bool saveItems( int cansObtained, int bottlesObtained, int bagsObtained)
    {
        
        state.cans += cansObtained;
        state.bottles += bottlesObtained;
        state.bags += bagsObtained;
        Save();
        return true;
    }
    //attempo buying weapon
    public bool BuyWeapon(int index,int canCost,int bottleCost,int bagCost)
    {
        if (state.cans>=canCost && state.bottles>=bottleCost && state.bags>=bagCost)
        {
            //enougmoney remove the gold
            state.cans -= canCost;
            state.bottles -= bottleCost;
            state.bags -= bagCost;

            UnlockWeapon(index);
            //save progress
            Save();
            return true;
        }
        else
        {
            //not enough money
            return false;
        }
    }
    public void UnlockWeapon(int index)
    {
        //toggle on the bit at index
        state.swordOwned |= 1 << index;
    }
    public string cansOwned()
    {
        return (state.cans.ToString());
    }
    public string bottleOwned()
    {
        return (state.bottles.ToString());
    }
    public string bagsOwned()
    {
        return (state.bags.ToString());
    }
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
        Debug.Log("gamereset");
    }

    public void OnOffStaticMode()
    {
        Debug.Log("el modo estatico esta" + state.StaticMode);
        state.StaticMode = !state.StaticMode;
        Debug.Log("el modo estatico esta" + state.StaticMode);
        Save();
    }
    public bool modeConsult()
    {
        return state.StaticMode;
    }
}
