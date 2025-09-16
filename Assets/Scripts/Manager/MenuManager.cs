using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Menu Buttons")]
    [SerializeField] private Button btnNewGame;
    [SerializeField] private Button btnLevelSelect;
    [SerializeField] private Button btnInstructions;
    [SerializeField] private Button btnQuit;

    [Header("Level Selection")]
    [SerializeField] private GameObject levelSelection;
    [SerializeField] private Button[] btnLevels;
    [SerializeField] private Button btnCloseLevelSelect;
    private int currentLvl = 0;
    private int maxLvl = 10;
    private int unlockedLevels = 0;

    [Header("Instructions")]
    [SerializeField] private GameObject instructions;
    [SerializeField] private Button btnCloseInstructions;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        levelSelection.SetActive(false);
        instructions.SetActive(false);

        btnNewGame.onClick.AddListener(OnStartNewGameClicked);
        btnQuit.onClick.AddListener(OnQuitGameClicked);
        btnLevelSelect.onClick.AddListener(OnLevelSelectClicked);
        btnCloseLevelSelect.onClick.AddListener(OnCloseLevelSelectClicked);
        btnInstructions.onClick.AddListener(OnInstructionsClicked);
        btnCloseInstructions.onClick.AddListener(OnCloseInstructionsClicked);
    }

    private void OnStartNewGameClicked()
    {
        GameManager.Instance.NewGame();
    }

    private void OnLevelSelectClicked()
    {
        levelSelection.SetActive(true);

        unlockedLevels = PlayerPrefs.GetInt("UnlockedLvl", 0);
        for (int i = currentLvl; i < maxLvl; i++)
        {
            if (i < unlockedLevels)
            {
                btnLevels[i].interactable = true;
            }
            else
            {
                btnLevels[i].interactable = false;
            }
        }
    }

    private void OnCloseLevelSelectClicked()
    {
        levelSelection.SetActive(false);
    }

    private void OnInstructionsClicked()
    {
        instructions.SetActive(true);
    }

    private void OnCloseInstructionsClicked()
    {
        instructions.SetActive(false);
    }

    private void OnQuitGameClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}