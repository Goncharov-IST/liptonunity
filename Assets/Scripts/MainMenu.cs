using UnityEngine;

public class MainMenu : MonoBehaviour
{
  public void StartGame()
  {
    GameManager.StartGame();
  }

  public void ExitGame()
  {
    GameManager.ExitGame();
  }
}
