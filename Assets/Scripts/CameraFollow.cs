using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform target;

  public float smoothSpeed = 0.125f;
  public Vector3 offset;

  void Start()
  {
    //Screen.SetResolution(480, 640, true, 30);
    //Screen.autorotateToPortrait = true;
    //Screen.autorotateToPortraitUpsideDown = true;
    //Screen.orientation = ScreenOrientation.PortraitUpsideDown;
  }

  void LateUpdate()
  {
    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothPosition;

    //transform.LookAt(target);
  }
}
