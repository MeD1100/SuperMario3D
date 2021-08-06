using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    private const string resolutionWidthPlayerPrefKey = "ResolutionWidth";
     private const string resolutionHeightPlayerPrefKey = "ResolutionHeight";
     private const string resolutionRefreshRatePlayerPrefKey = "RefreshRate";
     private const string fullScreenPlayerPrefKey = "masterFullScreen";

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;


    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;
    

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider BrightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    public int _qualityLevel;
    public bool _isFullScreen;
    public float _brightnessLevel;


    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;
 

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;
    

    [Header("Levels To Load")]
    public string _Loading;
    public string _newGameLevel;        
    private int levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    Resolution selectedResolution;
    
    private void Start(){
        resolutions = Screen.resolutions;
         LoadSettings();
         CreateResolutionDropdown();
 
         fullScreenToggle.onValueChanged.AddListener(SetFullscreen);
         resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }


    private void LoadSettings(){
        selectedResolution = new Resolution();
         selectedResolution.width = PlayerPrefs.GetInt(resolutionWidthPlayerPrefKey, Screen.currentResolution.width);
         selectedResolution.height = PlayerPrefs.GetInt(resolutionHeightPlayerPrefKey, Screen.currentResolution.height);
         selectedResolution.refreshRate = PlayerPrefs.GetInt(resolutionRefreshRatePlayerPrefKey, Screen.currentResolution.refreshRate);
         
         fullScreenToggle.isOn = PlayerPrefs.GetInt(fullScreenPlayerPrefKey, Screen.fullScreen ? 1 : 0) > 0;
 
         Screen.SetResolution(selectedResolution.width,selectedResolution.height,fullScreenToggle.isOn);
    }



    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);

        PlayerPrefs.SetInt(resolutionWidthPlayerPrefKey, selectedResolution.width);
        PlayerPrefs.SetInt(resolutionHeightPlayerPrefKey, selectedResolution.height);
        PlayerPrefs.SetInt(resolutionRefreshRatePlayerPrefKey, selectedResolution.refreshRate);
    
    }

    private void CreateResolutionDropdown()
    {
         resolutionDropdown.ClearOptions();
         List<string> options = new List<string>();
         int currentResolutionIndex = 0;
         for (int i = 0; i < resolutions.Length; i++)
         {
             string option = resolutions[i].width + " x " + resolutions[i].height;
             options.Add(option);
             if (Mathf.Approximately(resolutions[i].width, selectedResolution.width) && Mathf.Approximately(resolutions[i].height, selectedResolution.height))
             {
                 currentResolutionIndex = i;
             }
         }
         resolutionDropdown.AddOptions(options);
         resolutionDropdown.value = currentResolutionIndex;
         resolutionDropdown.RefreshShownValue();
     }

    public void NewGameDialogYes(int _newGameLevel){
        SceneManager.LoadScene(_newGameLevel);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("Level", 1);
    }

    public void ChooseDialogYes(){
        SceneManager.LoadScene(_newGameLevel);
    }

     public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("Level"))
        {
            levelToLoad = PlayerPrefs.GetInt("Level");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton(){
        Application.Quit();
    }

    public void SetVolume(float volume){
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply(){
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void SetControllerSen(float sensitivity){
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply(){
        if(invertYToggle.isOn){
            PlayerPrefs.SetInt("masterInvertY",1);//
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY",0);//
        }

        PlayerPrefs.SetFloat("masterSen",mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness){
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullscreen(bool isFullscreen)
     {
         Screen.fullScreen = isFullscreen;
         PlayerPrefs.SetInt(fullScreenPlayerPrefKey, isFullscreen ? 1 : 0);
     }

    public void SetQuality(int qualityIndex){
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply(){
        PlayerPrefs.SetFloat("masterBrightness",_brightnessLevel);
        
        PlayerPrefs.SetInt("masterQuality",_qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        StartCoroutine(ConfirmationBox());
    }


    public void ResetButton(string MenuType){
        if (MenuType == "Graphics"){
            BrightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");
            
            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);
            
            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }
        if (MenuType == "Audio"){
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if (MenuType == "Gameplay"){
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }   
    }

    public IEnumerator ConfirmationBox(){
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }



}
