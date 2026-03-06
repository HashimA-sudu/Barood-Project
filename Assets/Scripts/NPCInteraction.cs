using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public string characterName; // Type "Nasser" or "Merchant" in Inspector
    [TextArea(3, 10)]
    public string dialogueText; // Type the message here

    public void Interact()
    {
        // Tell the Manager to show MY name and MY text
        NPCUIManager.Instance.DisplayDialogue(characterName, dialogueText);
    }

    public string GetInteractLabel()
    {
        return "Press E to talk to " + characterName; // GDD interaction rule [cite: 280]
    }
}