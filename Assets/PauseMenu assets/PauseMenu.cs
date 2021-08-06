using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{    
    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;    
    public static bool GameIsPaused;

     void Update()
     {

         if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
         {           
             if (GameIsPaused)
             {
                 Resume();    
                              
             }
             else
             {
                 Pause();
                 Cursor.lockState = CursorLockMode.None;
                 Cursor.visible = true;
             }
         }
     }


    // Start is called before the first frame update

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    public void Home(int sceneID){
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
