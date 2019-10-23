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

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    ch_animator = GetComponent<Animator>();
    lane = 0;
  }

  void Update()
  {
    /* Control */
    if (isGrounded)
    {
      /* Keyboard Control */
      ch_animator.SetBool("squat", false);

      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
        if (lane > -1)
        {
          lane--;
        }
      }
      if (Input.GetKeyDown(KeyCode.RightArrow))
      {
        if (lane < 1)
        {
          lane++;
        }
      }
      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
        isGrounded = false;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      }
      if (Input.GetKeyDown(KeyCode.DownArrow))
      {
        ch_animator.SetBool("squat", true);
      }

      /* Swipe Control */
      if (Input.touchCount > 0)
      {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
          if (touch.position.x < Screen.width / 2 && transform.position.x > -1f)
          {
            lane--;
          }
          if (touch.position.x < Screen.width / 2 && transform.position.x < 1f)
          {
            lane++;
          }
        }
      }
    }

    Vector3 newPosition = transform.position;
    newPosition.z = lane;
    transform.position = newPosition;
    transform.Rotate(Vector3.up, .0f);
  }

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
}

