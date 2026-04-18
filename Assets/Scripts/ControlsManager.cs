using UnityEngine;

/// <summary>
/// Global controls manager that tracks whether player input should be disabled.
/// Used by all input-handling scripts to check if interactions are active.
/// </summary>
public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;
    
    private static bool controlsDisabled = false;
    
    // Individual reasons why controls might be disabled (for debugging)
    private static bool dialogueActive = false;
    private static bool pauseMenuActive = false;
    private static bool inventoryOpen = false;
    private static bool interactionActive = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Returns true if any interaction is happening (dialogue, pause menu, inventory, etc)
    /// All input-handling scripts should check this before processing input.
    /// </summary>
    public static bool AreControlsDisabled()
    {
        return controlsDisabled || dialogueActive || pauseMenuActive || inventoryOpen || interactionActive;
    }

    public static void SetDialogueActive(bool value)
    {
        dialogueActive = value;
        UpdateControlsState();
    }

    public static void SetPauseMenuActive(bool value)
    {
        pauseMenuActive = value;
        UpdateControlsState();
    }

    public static void SetInventoryOpen(bool value)
    {
        inventoryOpen = value;
        UpdateControlsState();
    }

    public static void SetInteractionActive(bool value)
    {
        interactionActive = value;
        UpdateControlsState();
    }

    public static void SetControlsDisabled(bool value)
    {
        controlsDisabled = value;
        UpdateControlsState();
    }

    private static void UpdateControlsState()
    {
        // Can be expanded for logging or other global effects
        Time.timeScale = (dialogueActive || pauseMenuActive || inventoryOpen || interactionActive) ? 0f : 1f;
    }

    // Debug info
    public static string GetDisabledReason()
    {
        string reasons = "";
        if (dialogueActive) reasons += "Dialogue ";
        if (pauseMenuActive) reasons += "PauseMenu ";
        if (inventoryOpen) reasons += "Inventory ";
        if (interactionActive) reasons += "Interaction ";
        if (controlsDisabled) reasons += "Manual ";
        return reasons.Length > 0 ? reasons : "None";
    }
}
