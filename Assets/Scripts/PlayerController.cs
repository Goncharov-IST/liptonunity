using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float jumpForce = 9.5f;
  private int lane;
  public ParticleSystem Score;
  public ParticleSystem Jump;
  private Rigidbody rb;
  private Animator ch_animator;
  bool isGrounded = false;
  public Swipe swipeControls;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    ch_animator = GetComponent<Animator>();
    lane = 0;
  }

  void Update()
  {
    #region Inputs
    if (isGrounded && !GameManager.isPaused)
    {
      ch_animator.SetBool("squat", false);

      if (Input.GetKeyDown(KeyCode.LeftArrow) || swipeControls.SwipeLeft)
      {
        if (lane > -1)
        {
          lane--;
        }
      }
      if (Input.GetKeyDown(KeyCode.RightArrow) || swipeControls.SwipeRight)
      {
        if (lane < 1)
        {
          lane++;
        }
      }
      if (Input.GetKeyDown(KeyCode.UpArrow) || swipeControls.SwipeUp)
      {
        isGrounded = false;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      }
      if (Input.GetKeyDown(KeyCode.DownArrow) || swipeControls.SwipeDown)
      {
        ch_animator.SetBool("squat", true);
      }
    }
    if (swipeControls.Tap)
    {
      Debug.Log("Tap!");
    }
    #endregion

    // Movement
    Vector3 newPosition = transform.position;
    newPosition.z = lane;
    transform.position = newPosition;
    transform.Rotate(Vector3.up, .0f);
  }

  #region Collision and Triggers
  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("Ground"))
    {
      isGrounded = true;
      Jump.Emit(20);
    }
    if (other.gameObject.CompareTag("Barrier"))
    {
      GameManager.Loss(true, false, 0);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Bottle"))
    {
      Score.Emit(1);
      Destroy(other.gameObject);
      GameManager.countBottle++;
    }
  }
  #endregion
}

