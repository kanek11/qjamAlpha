using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonController : MonoBehaviour
{

    private GameObject _mainMenuScreen;
    private GameObject _optionsScreen;
    private GameObject _selectDifficultyScreen;


    private TitleSceneManager _titleSceneManager;



    //====public UI
    public Sprite[] NormalEnemySprites;
    public Image NormalEnemyImage;


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

    public void OnNormalClicked()
    {
        
        //generalize random number of 0 to Sprite.Length
        int randomIndex = Random.Range(0, NormalEnemySprites.Length);

        GlobalState.EnemyIndex = randomIndex;
        Debug.Log("Normal Clicked, _currentEnemy is set to number :" + randomIndex); 

        //set the image to the random sprite 
        //set back alpha to 1
        NormalEnemyImage.color = new Color(1, 1, 1, 1);
        NormalEnemyImage.sprite = NormalEnemySprites[randomIndex];

        StartCoroutine(StartGameAfterDelay(3f));


    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        _titleSceneManager.StartGame();

    }





}
