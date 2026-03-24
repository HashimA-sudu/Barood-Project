using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class NPCUIManager : MonoBehaviour {
    public static NPCUIManager Instance;
    public GameObject menuPanel;
    public TextMeshProUGUI titleField;
    public TextMeshProUGUI descriptionField;

    [Header("Choice Buttons")]
    public GameObject choiceButtonsParent; 
    public TextMeshProUGUI textA, textB;

    private DialogueNode[] currentNodes;
    private int currentIndex;
    private bool isConversationActive = false;
    private float lastClosedTime; // Tracks when the menu closed
    void Awake() { 
        Instance = this; 
        menuPanel.SetActive(false); 
    }

    public bool IsMenuBusy() {
        // Returns true if the menu is open OR if it closed less than 0.1 seconds ago
        return isConversationActive || (Time.time - lastClosedTime < 0.1f);
    }

    void Update() {
        // If the menu is open, NOT showing a choice, and player Left Clicks
        if (isConversationActive && !currentNodes[currentIndex].isChoice && Mouse.current.leftButton.wasPressedThisFrame) {
            AdvanceDialogue();
        }
    }

    public void StartDialogue(string name, DialogueNode[] nodes) {
        titleField.text = name;
        currentNodes = nodes;
        currentIndex = 0;
        isConversationActive = true;
        
        menuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        ShowNode();
    }

    void ShowNode() {
        DialogueNode node = currentNodes[currentIndex];
        descriptionField.text = node.npcText;
        
        // Show buttons only if it's a choice node
        choiceButtonsParent.SetActive(node.isChoice);
        
        if(node.isChoice) {
            textA.text = node.choiceA_Text;
            textB.text = node.choiceB_Text;
        }
    }

    // Called by Left Click (for Talk nodes)
    void AdvanceDialogue() {
        int next = currentNodes[currentIndex].nextIndexA; // Use Index A as the "Next" path
        
        if (next == -1) {
            CloseMenu();
        } else {
            currentIndex = next;
            ShowNode();
        }
    }

    // Called by the Choice Buttons
    public void PickChoice(bool isA) {
        currentIndex = isA ? currentNodes[currentIndex].nextIndexA : currentNodes[currentIndex].nextIndexB;
        
        if (currentIndex == -1) CloseMenu();
        else ShowNode();
    }

    public void CloseMenu() {
        isConversationActive = false;
        lastClosedTime = Time.time; // Mark the exact second it closed
        menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}