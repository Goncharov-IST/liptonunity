using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using Wowmaking.RNU; // if installed react-native-unity package

public class GameManager : MonoBehaviour //, IRNCommandsReceiver
{
  public GameObject pauseLink;
  public GameObject resumeButton;
  public static GameObject pause;
  public static GameObject resume;
  public static bool isPaused = false;

  public Text mileage;
  public Text score;
  public Text FPS;
  public static int countBottle;
  private float countMileage;

  // FPS count
  private int frameCount = 0;
  private float dt = 0.0f;
  private float fps = 0.0f;
  private float updateRate = 4.0f; // 4 updates per sec.

  #region React Command Receiver

  /* private void Awake()
  {
    RNBridge.SetCommandsReceiver(this);
  }

  public void HandleCommand(RNCommand command)
  {
    switch (command.name)
    {
      case "resolve_command":
        command.Resolve(new
        {
          a = "test_data",
          b = 123,
        });
        break;

      case "reject_command":
        command.Reject(new
        {
          error = "test_data error",
        });
        break;
    }
  } */

  #endregion

  void Start()
  {
    pause = pauseLink;
    resume = resumeButton;

    countBottle = 0;
    countMileage = 0f;
  }

  void Update()
  {
    #region FPS Count

    frameCount++;
    dt += Time.deltaTime;
    if (dt > 1.0f / updateRate)
    {
      fps = frameCount / dt;
      frameCount = 0;
      dt -= 1.0f / updateRate;
      FPS.text = fps.ToString("0");
    }

    #endregion

    if (PlayerController.isGameStart)
    {
      score.text = countBottle.ToString();
      countMileage += 2 * Time.deltaTime;
      mileage.text = countMileage.ToString("0");
    }
  }

  public void ReplyLevel()
  {
    isPaused = false;
    Time.timeScale = 1;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public static void Loss(bool paus, bool res, int time) // Проигрыш
  {
    isPaused = true;
    Time.timeScale = time;
    pause.SetActive(paus);
    resume.SetActive(res);
  }

  public void Pause()
  {
    isPaused = true;
    Time.timeScale = 0;
    pauseLink.SetActive(true);
  }

  public void Resume()
  {
    isPaused = false;
    Time.timeScale = 1;
    pauseLink.SetActive(false);
  }

  public static void StartGame()
  {
    SceneManager.LoadScene(1);
  }

  public static void ExitGame()
  {
    //SendEvent("game_unity", new { type = "quit", })
    Application.Quit();
  }

  public void ExitGameLevel()
  {
    //SendEvent("game_unity", new { type = "quit", })
    Application.Quit();
  }
}