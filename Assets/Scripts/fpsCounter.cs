using UnityEngine;

public class fpsCounter : MonoBehaviour
{
  private int frameCount = 0;
  private float dt = 0.0f;
  private float fps = 0.0f;
  private float updateRate = 4.0f;  // 4 updates per sec.
  public static string countFPS;

  void Update()
  {
    frameCount++;
    dt += Time.deltaTime;
    if (dt > 1.0f / updateRate)
    {
      fps = frameCount / dt;
      frameCount = 0;
      dt -= 1.0f / updateRate;
      string FPS = fps.ToString("0");
      Debug.Log(FPS);
    }
  }
}
