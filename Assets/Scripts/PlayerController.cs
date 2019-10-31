using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private const float LANE_DISTANCE = 3.0f;
  private const float TURN_SPEED = 0.5f;

  // Movement
  public float jumpForce = 4.0f;
  private float gravity = 10.0f;
  private float verticalVelocity;
  private float speed = 10.0f;
  private int desiredLane = 1; // 0 - Left, 1 = Middle, 2 - Right

  public ParticleSystem Score;
  public ParticleSystem Jump;

  private Animator ch_animator;
  private CharacterController ch_controller;
  public Swipe swipeControls;

  private void Start()
  {
    ch_controller = GetComponent<CharacterController>();
    ch_animator = GetComponent<Animator>();
  }

  private void Update()
  {
    #region Inputs
    if (!GameManager.isPaused)
    {
      ch_animator.SetBool("squat", false);
      ch_animator.SetBool("jump", false);

      if (Input.GetKeyDown(KeyCode.LeftArrow) || swipeControls.SwipeLeft)
      {
        MoveLane(false);
      }
      if (Input.GetKeyDown(KeyCode.RightArrow) || swipeControls.SwipeRight)
      {
        MoveLane(true);
      }
    }
    if (swipeControls.Tap)
    {
      Debug.Log("Tap!");
    }
    #endregion

    #region Movement params
    Vector3 targetPosition = transform.position.x * Vector3.forward;
    if (desiredLane == 0)
      targetPosition += Vector3.left * LANE_DISTANCE;
    else if (desiredLane == 2)
      targetPosition += Vector3.right * LANE_DISTANCE;

    Vector3 moveVector = Vector3.zero;
    moveVector.x = (targetPosition - transform.position).x * 10.0f;
    if (ch_controller.isGrounded)
    {
      verticalVelocity = -0.1f;
      if (Input.GetKeyDown(KeyCode.UpArrow) || swipeControls.SwipeUp)
      {
        verticalVelocity = jumpForce;
        ch_animator.SetBool("jump", true);
      }
      else if (Input.GetKeyDown(KeyCode.DownArrow) || swipeControls.SwipeDown)
      {
        StartSliding();
        Invoke("StopSliding", 1.0f);
      }
    }
    else
    {
      verticalVelocity -= (gravity * Time.deltaTime);
      if (Input.GetKeyDown(KeyCode.UpArrow) || swipeControls.SwipeUp)
      {
        verticalVelocity = -jumpForce;
      }
    }
    moveVector.y = verticalVelocity;
    moveVector.z = speed;
    #endregion

    // Movement
    ch_controller.Move(moveVector * Time.deltaTime);
    Vector3 dir = ch_controller.velocity;
    if (dir != Vector3.zero)
    {
      dir.y = 0;
      transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
    }
  }

  private void MoveLane(bool goingRight)
  {
    desiredLane += (goingRight) ? 1 : -1;
    desiredLane = Mathf.Clamp(desiredLane, 0, 2);
  }
  private void StartSliding()
  {
    ch_animator.SetBool("squat", true);
    ch_controller.height /= 2;
    ch_controller.center = new Vector3(ch_controller.center.x, ch_controller.center.y / 2, ch_controller.center.z);
  }
  private void StopSliding()
  {
    ch_animator.SetBool("squat", false);
    ch_controller.height *= 2;
    ch_controller.center = new Vector3(ch_controller.center.x, ch_controller.center.y * 2, ch_controller.center.z);
  }

  #region Collision and Triggers
  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.gameObject.CompareTag("Ground"))
    {
      //isGrounded = true;
      //Jump.Emit(20);
    }
    if (hit.gameObject.CompareTag("Barrier"))
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

