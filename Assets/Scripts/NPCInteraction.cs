using UnityEngine;
using System;

[Serializable]
public class DialogueNode {
    public string npcText;
    public bool isChoice;
    public string choiceA_Text;
    public int nextIndexA; // The index of the node to go to if A is picked
    public string choiceB_Text;
    public int nextIndexB; // The index of the node to go to if B is picked
}

public class NPCInteraction : MonoBehaviour, IInteractable {
    public string characterName;
    public DialogueNode[] nodes; // You can build the whole tree in the Inspector
   
    public void Interact() {
        if (NPCUIManager.Instance != null) {
            NPCUIManager.Instance.StartDialogue(characterName, nodes);
        }
    }
    public string GetInteractLabel() => "Press E to talk to " + characterName;
}