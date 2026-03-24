using UnityEngine;

public class GateController : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public Animator gateAnimator; 

    public string GetInteractLabel()
    {
        return isOpen ? "Press [E] to Close Gate" : "Press [E] to Open Gate";
    }

    public void Interact()
{
    isOpen = !isOpen; // Toggles the state
    
    // This sends the signal to your Animator's 'isOpen' parameter
    if (gateAnimator != null)
    {
        gateAnimator.SetBool("isOpen", isOpen);
        gateAnimator.SetTrigger("Toggle"); // Optional: Trigger to play an animation
    }
}
}