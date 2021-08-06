using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public int _newGameLevel;        
    private List<GameObject> models;
    public TMP_Text Mario,Luigi;
    public float rotateSpeed;

    private int selectionIndex = 0;
    private void Start(){
        models = new List<GameObject>();
        foreach(Transform t in transform){
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[selectionIndex].SetActive(true);
        Mario.gameObject.SetActive(true);
    }

    private void Update(){
        if(Input.GetMouseButton(0))
            transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed);
        if(Input.GetKeyDown(KeyCode.LeftArrow))    
            SelectLeft();
        if(Input.GetKeyDown(KeyCode.RightArrow))
            {SelectRight();}
        if (Input.GetKeyDown(KeyCode.Return)){
            Level1DialogYes();
        }
    }
    public void SelectLeft(){
        if (selectionIndex == 0) return;
        else
        {
           models[selectionIndex].SetActive(false); 
            models[--selectionIndex].SetActive(true);
            Mario.gameObject.SetActive(true);

        }
    }
    public void SelectRight(){
        if (selectionIndex == (models.Count - 1)) return ;
        else
        {  
           models[selectionIndex].SetActive(false); 
           models[++selectionIndex].SetActive(true); 
           Luigi.gameObject.SetActive(true);

        }
    }

    public void Level1DialogYes(){
        SceneManager.LoadScene(_newGameLevel);
    }
}
