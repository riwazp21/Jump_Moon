using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void OnPlayButton()
   {
    SceneManager.LoadScene("_Scene_0");
   }
   public void OnBackButton()
   {
    SceneManager.LoadScene("MainMenu");
   }
   public void AboutUsScene()
   {
    SceneManager.LoadScene("About");
   }
   public void RulesScene()
   {
    SceneManager.LoadScene("Rules");
   }
   public void QuitButton()
   {
    Application.Quit(); 
   }
}
