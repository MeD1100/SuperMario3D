/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level = 1;
    public int health = 20;
    public int gold = 24;

    public void Save(){
        SaveLoadManager.SavePlayer(this);
    }

    public void Load(){
        int[] loadedStats = SaveLoadManager.LoadPlayer();

        level = loadedStats[0];
        health = loadedStats[1];
        gold = loadedStats[2];
        
        GetComponent<PlayerDisplay>().UpdateDisplay();
    }




    //UI
    public void Adjust(int stat, int value){
        stat += value;
        if (stat < 1){
            stat = 1;
        }
        GetComponent<PlayerDisplay>().UpdateDisplay();
    }
    public void AdjustLevel(int value){
        Adjust(level, value);
    }

    public void AdjustHealth(int value){
        Adjust(health, value);
    }

    public void AdjustGold(int value){
        Adjust(gold, value);
    }
}
*/