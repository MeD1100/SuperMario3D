using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted",0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    private void UpdateButtonIcon(){
        if (muted == false){
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        if (muted == true) {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    public void onButtonPress(){
        if (muted ==  false){
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }

    public void onButtonPress_Home(){
        if (muted == false){
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = true;
        }
        Save();
    }

    private void Load(){
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save(){
        PlayerPrefs.SetInt("muted",muted?1:0);
    }
}
