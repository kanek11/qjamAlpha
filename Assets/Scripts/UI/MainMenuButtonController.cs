using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonController : MonoBehaviour
{

    private GameObject _mainMenuScreen;
    private GameObject _optionsScreen;
    private GameObject _selectDifficultyScreen;


    private TitleSceneManager _titleSceneManager;
  

    private void Awake()
    {
        _mainMenuScreen = GameObject.Find("MainMenuScreen");
        _optionsScreen = GameObject.Find("OptionsScreen");
        _selectDifficultyScreen = GameObject.Find("SelectDifficultyScreen");

        _titleSceneManager = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();
    }


    public void OnStartGameClicked()
    {
        Debug.Log("Start Game Clicked");
        //set the main menu alpha to 0, not interactable. by the canvas group
        this.GetComponentInParent<CanvasGroup>().alpha = 0;
        this.GetComponentInParent<CanvasGroup>().interactable = false;
        this.GetComponentInParent<CanvasGroup>().blocksRaycasts = false;


        //set the select difficulty screen to 1, interactable
        _selectDifficultyScreen.GetComponent<CanvasGroup>().alpha = 1;
        _selectDifficultyScreen.GetComponent<CanvasGroup>().interactable = true;
        _selectDifficultyScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
         
         
    }

    public void OnQuitGameClicked()
    {
        Application.Quit();
    }


    public void OnOptionsClicked()
    { 
    }

    public void OnBackToTitleClicked()
    {

        //set the select difficulty screen to 0, interactable
        _selectDifficultyScreen.GetComponent<CanvasGroup>().alpha = 0;
        _selectDifficultyScreen.GetComponent<CanvasGroup>().interactable = false;
        _selectDifficultyScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;


        //set the main menu alpha to 1, not interactable. by the canvas group
        this.GetComponentInParent<CanvasGroup>().alpha = 1;
        this.GetComponentInParent<CanvasGroup>().interactable = true;
        this.GetComponentInParent<CanvasGroup>().blocksRaycasts = true;



    }

    public void OnStage1Clicked()
    {

        _titleSceneManager.StartGame(); 
    }


}
