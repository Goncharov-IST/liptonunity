using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
  private float updateRate = 4.0f;  // 4 updates per sec.

  void Start()
  {
    //Screen.SetResolution(Screen.width, Screen.height, false);
    //QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = 30;

    pause = pauseLink;
    resume = resumeButton;

    countBottle = 0;
    countMileage = 0f;
  }

  void Update()
  {
    // FPS count
    frameCount++;
    dt += Time.deltaTime;
    if (dt > 1.0f / updateRate)
    {
      fps = frameCount / dt;
      frameCount = 0;
      dt -= 1.0f / updateRate;
      FPS.text = fps.ToString("0");
    }

    score.text = countBottle.ToString();
    countMileage += 2 * Time.deltaTime;
    mileage.text = countMileage.ToString("0");
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
}
