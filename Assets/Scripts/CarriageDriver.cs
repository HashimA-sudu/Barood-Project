using UnityEngine;
using UnityEngine.SceneManagement;
public class CarriageDriver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    public void TravelToDesertedVillage()
    {
        SceneManager.LoadScene("DesertedVillage");
        Time.timeScale = 1f;
    }

    // Update is called once per frame
   public void CancelTravel()
    {
        NPCUIManager.Instance.CloseMenu();
    }
}


