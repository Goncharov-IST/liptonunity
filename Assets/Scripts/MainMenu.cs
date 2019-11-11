using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  private void Start()
  {
    //QualitySettings.vSyncCount = 0;
    //Application.targetFrameRate = 60;
  }
  public void StartGame()
  {

    SceneManager.LoadScene(1);
  }
  public void ExitGame()
  {
    Application.Quit();
  }
}
