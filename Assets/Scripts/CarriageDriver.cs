using UnityEngine;
using UnityEngine.SceneManagement;

public class CarriageDriver : MonoBehaviour
{
    public void TravelToDesertedVillage()
    {
        // 1. Check if the manager is currently talking to the driver
        // We compare the string directly because currentNPC is a string
        if (NPCUIManager.Instance.currentNPC == "Bo Nasser (Carriage Driver)")
        {
            Time.timeScale = 1f; // Always reset time before loading
            SceneManager.LoadScene("DesertedVillage");
        }
       
    }

    public void CancelTravel()
    {
        NPCUIManager.Instance.CloseMenu();
    }
}