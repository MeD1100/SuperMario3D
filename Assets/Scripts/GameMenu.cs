using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public PlayerController player;

    
    public void SaveButton(){
        SaveByPlayerPrefs();
        Debug.Log("saved");
    }

    public void LoadButton(){
        LoadByPlayerPrefs();
        Debug.Log("loaded");
    }
    
    private void SaveByPlayerPrefs(){
        PlayerPrefs.SetInt("Gold", GameManager.instance.gold);
        PlayerPrefs.SetInt("Level", GameManager.instance.whatlevel); 
        PlayerPrefs.SetFloat("PlayerPosX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY",player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ",player.transform.position.z);
    }
    private void LoadByPlayerPrefs(){
        if (PlayerPrefs.HasKey("Gold"))
            GameManager.instance.gold = PlayerPrefs.GetInt("Gold");
        if (PlayerPrefs.HasKey("Level"))
            GameManager.instance.whatlevel = PlayerPrefs.GetInt("Level");
        if (PlayerPrefs.HasKey("PlayerPosX"))
            player.playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
        if (PlayerPrefs.HasKey("PlayerPosY"))
            player.playerPosY = PlayerPrefs.GetFloat("PlayerPosY");
        if (PlayerPrefs.HasKey("PlayerPosZ"))
            player.playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ");
        player.transform.position = new Vector3(player.playerPosX,player.playerPosY,player.playerPosZ);
    }
    


}





//METHOD2

/*

   
public static List<Save> savedSaves = new List<Save>();

public PlayerController player;
    public void SaveButton(){
        SaveBySerialization();

    }

    public void LoadButton(){
        LoadBySerialization();

    }
    public Save createSaveGameObject(){
        Save save = new Save();
        save.goldNum = GameManager.instance.gold;
        save.level = GameManager.instance.whatlevel;

        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y;
        save.playerPositionZ = player.transform.position.z;
        
        return save;
    }
    public void SaveBySerialization(){
        Debug.Log("Saved.");
        Save save = createSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/Data.text"))
        {
            File.Delete(Application.persistentDataPath + "/Data.text");
            //Debug.Log("Delete the old");
        }
        FileStream fileStream = File.Create(Application.persistentDataPath + "/Data.text");

        bf.Serialize(fileStream,save);
        fileStream.Close();
    }
    
    // ------------
// I'm going to try PlayerPrefs in place of BinaryFormatters in order to do it right with loading game and levels.
    // ------------

    public void LoadBySerialization(){
        if (File.Exists(Application.persistentDataPath + "/Data.text")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open  (Application.persistentDataPath + "/Data.text", FileMode.Open);
        
            Save save = bf.Deserialize(fileStream) as Save;

            fileStream.Close();
            GameManager.instance.gold = save.goldNum;
            GameManager.instance.whatlevel =  save.level;


            player.transform.position = new Vector3(save.playerPositionX, save.playerPositionY,save.playerPositionZ);
            Debug.Log("Saved.");

        } else
        {
            Debug.LogError("File does not exist");
        }
    }
*/