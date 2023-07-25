using cityBuilder.AI;
using SVS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;


public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public RoadManager roadManager;
    public InputManager inputManager;

    public UIController uiController;

    public StructureManager structureManager;

    public ObjectDetector objectDetector;

    public PathVisualizer pathVisualizer;

    public int Days;
    public int Population;

    public TextMeshProUGUI PopTxT;

    public static GameManager Instance;

    public int POpCountMax;

    public int PopCountMin;

    [Header("int")]
    public static int number = 0;

    public GameObject pauseMenu;

    public bool pause = true;

    public GameObject managers;

    public Animator transition;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject HUD;
    public GameObject Managers;
    public GameObject mainmenu;

    public GameObject WinScreen;

    public LevelSystemv2 systemv2;
    public DaysCounter counter;

    public AchievementManager achievementManager;

    public GameObject[] popUps;
    public int index;

    public TextMeshProUGUI coinstxt;
    public TextMeshProUGUI populationtxt;

    public CurrencySystemv2 currency;

    public AchievementID AchievementID;

    public bool OFF = true;

    private void Awake()
    {
        if(number == 1)
        {
            mainmenu.SetActive(false);
            managers.SetActive(true);
            HUD.SetActive(true);
            Time.timeScale = 1;
            cam2.SetActive(true);
            cam1.SetActive(false);
        }
        else if(number == 0)
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
        uiController.OnRoadPlacement += RoadPlacementHandler;
        uiController.OnHousePlacement += HousePlacementHandler;
        uiController.OnSpecialPlacement += SpecialPlacementHandler;
        uiController.OnBigStructurePlacement += BigStructurePlacement;
        inputManager.OnEscape += HandleEscape;
    }

    

    private void HandleEscape()
    {
        ClearInputActions();
        uiController.ResetButtonColor();
        pathVisualizer.ResetPath();
        inputManager.OnMouseClick += TrySelectingAgent;
    }

  

    private void TrySelectingAgent(Ray ray)
    {
        GameObject hitObject = objectDetector.RaycastAll(ray);
        if(hitObject != null)
        {
            var agentScript = hitObject.GetComponent<AiAgent>();
            //agentScript?.ShowPath();
        }
    }

    private void BigStructurePlacement()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structureManager.PlaceBigStructure, pos);
        };
        inputManager.OnEscape += HandleEscape;
    }

    public void OpenPopups()
    {
        index = UnityEngine.Random.Range(0, 4);
        popUps[index].SetActive(true);
    }

    public void ClosePopups()
    {
        index = UnityEngine.Random.Range(0, 4);
        popUps[index].SetActive(false);
    }

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


    private void HousePlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) =>
        {
            ProcessInputAndCall(structureManager.PlaceHouse, pos);
        };
        inputManager.OnEscape += HandleEscape;
    }

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

    private void ClearInputActions()
    {
        inputManager.ClearEvents();
    }

    private void ProcessInputAndCall(Action<Vector3Int> callback, Ray ray)
    {
        Vector3Int? result = objectDetector.RaycastGround(ray);
        if (result.HasValue)
            callback.Invoke(result.Value);
    }



    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
        PopTxT.text = Population.ToString();


        if (Input.GetKeyDown(KeyCode.P))
        {
            if(pause == true)
            {
                Paused();
          
            }
         
        }


        if( systemv2.level == 5 & counter.dayCount == 5)
        {
            // win game
            WinGame();
        }

        if(Population == 12)
        {
            achievementManager.UnlockAchievement(AchievementID.Thereislife);
        }

       

    }

   


    public void PressedInfo()
    {
        while (OFF == true)
        {
            achievementManager.UnlockAchievement(AchievementID.NeededHelp);
            achievementManager.ShowNotification();
            OFF = false;
        }
    
       
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        managers.SetActive(false);
        WinScreen.SetActive(true);
        HUD.SetActive(false); 
        Debug.Log("Won!!");
        populationtxt.text = Population.ToString();
        coinstxt.text = currency.ToString();      

    }

    public void Play()
    {
        StartCoroutine(CamTransition()); 
    }

    public IEnumerator CamTransition()
    {
        mainmenu.SetActive(false);
        transition.Play("cam1");
        yield return new WaitForSeconds(1f);
        cam1.SetActive(false);
        cam2.SetActive(true);
        HUD.SetActive(true);
        managers.SetActive(true);
    }

    public void Paused()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        managers.SetActive(false);
    }

    public void UnPaused()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        managers.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Application.Quit(); 
    }
}
