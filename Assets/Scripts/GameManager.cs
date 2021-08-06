using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int whatlevel;
    public TMP_Text levelText;
    public int gold;
    public TMP_Text goldText;
    

    private void Awake(){
        if (instance==null) {
            instance=this;
        }else
        {
            if (instance !=this){
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update(){
        if (PlayerPrefs.GetInt("Gold") == 0 && PlayerPrefs.GetInt("Level") == 1){
            instance.gold = 0;
            instance.whatlevel = 1;
            goldText.text = gold.ToString();
            levelText.text = whatlevel.ToString();
        }
        else
        {
            
            goldText.text = gold.ToString();
            levelText.text = whatlevel.ToString();
        }
        
    }
    public void AddGold(int goldToAdd)
    {
        gold+= goldToAdd;
        
    }
}
