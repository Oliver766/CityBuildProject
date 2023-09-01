using cityBuilder.AI;
using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
// script reference by Sunny Valley Studio - https://www.youtube.com/watch?v=8ayFCDbfIIM&list=PLcRSafycjWFd6YOvRE3GQqURFpIxZpErI
// script eddited by Oliver Lancashire
// sid 1901981

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;
    public UIController uiController;
    public StructureManager structureManager;
    public ObjectDetector objectDetector;
    public PathVisualizer pathVisualizer;
    public static GameManager Instance;
    public LevelSystemv2 systemv2;
    public DaysCounter counter;
    public AchievementManager achievementManager;
    public CurrencySystemv2 currency;
    public CameraMovement CameraMovement;
    [Header("Int")]
    public int Days;
    public int Population;
    public int POpCountMax;
    public int PopCountMin;
    public static int number = 0;
    public int index;
    [Header("UI")]
    public TextMeshProUGUI PopTxT;
    public TextMeshProUGUI coinstxt;
    public TextMeshProUGUI populationtxt;
    public TextMeshProUGUI DayText;
    [Header("GameObjects")]
    public GameObject cam1;
    public GameObject cam2;
    public GameObject HUD;
    public GameObject Managers;
    public GameObject mainmenu;
    public GameObject managers;
    public GameObject pauseMenu;
    public GameObject WinScreen;
    public GameObject popUp1;
    public GameObject[] popUps;
    [Header("Bool")]
    public bool pause = true;
    public bool builder;
    public bool OFF;
    public bool people;
    [Header("Animator")]
    public Animator transition;



    private void Awake()
    {
        // checks if number is one then set objects active in game and inactive for main menu
        if(number == 1)
        {
            mainmenu.SetActive(false);
            managers.SetActive(true);
            HUD.SetActive(true);
            Time.timeScale = 1;
            cam2.SetActive(true);
            cam1.SetActive(false);
        }
        //  checks if number is one then set objects active in main menu and inactive for game
        else if (number == 0)
        {
            mainmenu.SetActive(true);
            managers.SetActive(false);
            HUD.SetActive(false);
            cam2.SetActive(false);
            cam1.SetActive(true);
            Time.timeScale = 1;
        }
    }


    void Start()
    {
        // set functions  for each input
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnSpecialPlacement += SpecialPlacementHandler;
        uiController.OnBigStructurePlacement += BigStructurePlacement;
        inputManager.OnEscape += HandleEscape;
        people = true;
    }

    
    /// <summary>
    /// function to reset UI and player inputs
    /// </summary>
    private void HandleEscape()
    {
        ClearInputActions();
        uiController.ResetButtonColor();
        pathVisualizer.ResetPath();
        inputManager.OnMouseClick += TrySelectingAgent;
    }

  
    /// <summary>
    /// tries to click on AI object
    /// </summary>
    /// <param name="ray"></param>
    private void TrySelectingAgent(Ray ray)
    {
        GameObject hitObject = objectDetector.RaycastAll(ray);
        if(hitObject != null)
        {
            var agentScript = hitObject.GetComponent<AiAgent>();
            //agentScript?.ShowPath();
        }
    }

    /// <summary>
    /// set input action to place objects
    /// </summary>
    private void BigStructurePlacement()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structureManager.PlaceBigStructure, pos);
        };
        inputManager.OnEscape += HandleEscape;
    }
    /// <summary>
    /// randomise UI pop up
    /// </summary>
    public void OpenPopups()
    {
        index = UnityEngine.Random.Range(0, 4);
        popUps[index].SetActive(true);
    }
    /// <summary>
    /// close POPup
    /// </summary>
    public void ClosePopups()
    {
        index = UnityEngine.Random.Range(0, 4);
        popUps[index].SetActive(false);
    }
    /// <summary>
    /// set input action to place objects
    /// </summary>
    private void SpecialPlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structureManager.PlaceSpecial, pos);
        };
        inputManager.OnEscape += HandleEscape;
    }

    /// <summary>
    /// load games
    /// </summary>
    public void LoadScene()
    {
        number = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// loads main menu
    /// </summary>
    public void LoadMainScene()
    {
        number = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// function to diable camera
    /// </summary>
    public void turnoffcam()
    {
        cameraMovement.enabled = false;
    }
    /// <summary>
    ///  function to enable camera
    /// </summary>
    public void turnoncam()
    {
        cameraMovement.enabled = true;
    }
    /// <summary>
    ///  set input action to place objects
    /// </summary>
    private void HousePlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structureManager.PlaceHouse, pos);
        };
        inputManager.OnEscape += HandleEscape;
    }
    /// <summary>
    ///  set input action to place objects
    /// </summary>
    private void RoadPlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(roadManager.PlaceRoad, pos);
        };
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
        inputManager.OnMouseHold += (pos) =>
        {
            ProcessInputAndCall(roadManager.PlaceRoad, pos);
        };
        inputManager.OnEscape += HandleEscape;
    }
    /// <summary>
    ///  function to disable all imput events
    /// </summary>
    private void ClearInputActions()
    {
        inputManager.ClearEvents();
    }
    /// <summary>
    /// function to detect if you place object
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="ray"></param>
    private void ProcessInputAndCall(Action<Vector3Int> callback, Ray ray)
    {
        Vector3Int? result = objectDetector.RaycastGround(ray);
        if (result.HasValue)
            callback.Invoke(result.Value);
    }



    private void Update()
    {
        // moving camera using keys
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
        PopTxT.text = Population.ToString(); // update population text

        // pausing and unpausing game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(pause == true)
            {
                Paused();
          
            }
         
        }

        // game win loop
        if( systemv2.level == 5  || counter.dayCount == 5)
        {
            // win game
            WinGame();
        }
        // run achievement
        while(OFF == true)
        {
            achievementManager.UnlockAchievement(AchievementID.AHelpingHand);
            OFF = false;
            systemv2.currentXP += 20;
            

        }

        // run achievement
        while (builder == true)
        {
            achievementManager.UnlockAchievement(AchievementID.Builder);


            systemv2.currentXP += 20;
            builder = false;
           
        }


        // run achievement
        if (Population == 4)
        {
            achievementManager.achievementToShow = AchievementID.ThereisLife;
            while(people == true)
            {
                achievementManager.UnlockAchievement(AchievementID.ThereisLife);

                systemv2.currentXP += 20;

                people = false;
                achievementManager.achievementToShow = AchievementID.AHelpingHand;

            }
        }
    
    }

    /// <summary>
    /// load survey
    /// </summary>
   public void URL()
   {
        Application.OpenURL("https://angliaruskin.onlinesurveys.ac.uk/buildacity-ui-survey-copy");
   }

    /// <summary>
    /// run achievement
    /// </summary>
    public void Build()
    {
        builder = true;
        achievementManager.achievementToShow = AchievementID.Builder;

    }

    /// <summary>
    ///    // run achievement
    /// </summary>
    public void PressedInfo()
    {
        OFF = true;
        achievementManager.achievementToShow = AchievementID.AHelpingHand;

    }
    /// <summary>
    /// win game function
    /// </summary>
    public void WinGame()
    {
        Time.timeScale = 0;
        managers.SetActive(false);
        WinScreen.SetActive(true);
        HUD.SetActive(false); 
        Debug.Log("Won!!");
        populationtxt.text = Population.ToString();
        coinstxt.text = currency.amount.ToString();
        DayText.text = counter.dayCount.ToString();
        HUD.SetActive(false);

    }
    /// <summary>
    /// run coroutine
    /// </summary>
    public void Play()
    {
        StartCoroutine(CamTransition()); 
    }
    /// <summary>
    /// allows for smooth camera transition
    /// </summary>
    /// <returns></returns>
    public IEnumerator CamTransition()
    {
        mainmenu.SetActive(false);
        transition.Play("cam1");
        yield return new WaitForSeconds(1f);
        cam1.SetActive(false);
        cam2.SetActive(true);
        popUp1.SetActive(true);
        HUD.SetActive(true);
    
    }
    /// <summary>
    /// set manager object active
    /// </summary>
    public void StartManager()
    {
        managers.SetActive(true);
    }
    /// <summary>
    /// pausing game function
    /// </summary>
    public void Paused()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        managers.SetActive(false);
        HUD.SetActive(false);
    }
    /// <summary>
    /// unpausing game function
    /// </summary>
    public void UnPaused()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        managers.SetActive(true);
    }
    /// <summary>
    /// quit function
    /// </summary>
    public void OnApplicationQuit()
    {
        Application.Quit(); 
    }
}
