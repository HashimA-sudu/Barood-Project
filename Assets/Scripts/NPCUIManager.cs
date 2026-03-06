using UnityEngine;
using TMPro;

public class NPCUIManager : MonoBehaviour
{
    public static NPCUIManager Instance;

    public GameObject menuPanel; // Drag "ChatMenu" here
    public TextMeshProUGUI titleField; // Drag "Title" here
    public TextMeshProUGUI descriptionField; // Drag "description" here

    void Awake()
    {
        if (Instance == null) Instance = this;
        
        if(menuPanel != null) menuPanel.SetActive(false); 
    }

    public void DisplayDialogue(string npcName, string npcDialogue)
    {
        if (titleField != null) titleField.text = npcName;
        if (descriptionField != null) descriptionField.text = npcDialogue;
        
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
        }
    }

    public void CloseMenu()
    {
        if (menuPanel != null) menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }
}