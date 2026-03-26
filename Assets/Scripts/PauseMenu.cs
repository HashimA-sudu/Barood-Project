using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static PauseMenu Instance;
    public bool isPaused = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Force the menu off at the very start of the game
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused) Resume();
            else Pause();
        }   
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState= CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void ReturnToHideout()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
