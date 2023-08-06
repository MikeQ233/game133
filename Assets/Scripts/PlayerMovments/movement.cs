using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
  [SerializeField] public LayerMask PlatformLayerMask;
  public float speed;
  public float jumpForce;
  public bool isGrounded;
  public int jumpLimit = 2;
  private BoxCollider2D boxCollider;
  private PassDownOneWayPlatform passDownOneWayPlatform;
  private int jumpTime;
  private float moveHorizontal;
  private Rigidbody2D rb;
  private Animator animator;
  // used when player holding down w key while grounded.
  private float lastJumpedTime;
  private bool facingRight = true;
  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();
    passDownOneWayPlatform = GetComponent<PassDownOneWayPlatform>();
    animator = GetComponent<Animator>();
    jumpTime = 0;
  }

  // Update is called once per frame
  void Update()
  {
    moveHorizontal = Input.GetAxisRaw("Horizontal");
    if (Input.GetKeyDown(KeyCode.W))
    {
      DoJump();
    }
    else if (isGrounded == true && Input.GetKey(KeyCode.W) && Time.time - lastJumpedTime > 0.5f)
    {
      // while player is on the ground, holding w will jump again!
      // throttled to 0.5s
      DoJump();
    }
  }
  void FixedUpdate()
  {
    if (moveHorizontal > 0 && !facingRight)
    {
      flip();
    }
    if (moveHorizontal < 0 && facingRight)
    {
      flip();
    }
    if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
    {
      rb.AddForce(new Vector2(moveHorizontal * speed, 0), ForceMode2D.Impulse);
      animator.SetBool("isRunning", true);
    }
    else
    {
      animator.SetBool("isRunning", false);
    }
    checkIsGrounded();
  }

  private void checkIsGrounded()
  {
    float heightTest = 0.2f;
    var center = boxCollider.bounds.center - new Vector3(0, boxCollider.bounds.extents.y - 0.1f, 0);
    var size = new Vector3(boxCollider.bounds.size.x, 0.1f, 0.0f);
    RaycastHit2D raycastHit = Physics2D.BoxCast(center, size, 0f, Vector2.down, heightTest, PlatformLayerMask);
    Color rayColor = raycastHit.collider != null ? Color.red : Color.green;
    // to visualize box cast, need to set gizmos visible to see
    Debug.DrawRay(center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (size.y / 2.0f + heightTest), rayColor);
    Debug.DrawRay(center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (size.y / 2.0f + heightTest), rayColor);
    if (raycastHit.collider != null)
    {
      var supp = raycastHit.collider.gameObject.GetComponent<MoveDownSupport>();
      if (supp && supp.IsPassThroughEnabled())
      {
        isGrounded = false;
      } 
      else
      {
        // the box cast hits when player _jumps_ too, so we need to check if player is "landing" on the platform.
        if (Mathf.Abs(rb.velocity.y) < 0.1)
        {
          isGrounded = true;
          // here when player lands, set y vel to zero, otherwise when the player jumps again,
          // the y-direction force will not be applied correctly
          var vel = rb.velocity;
          vel.y = 0;
          rb.velocity = vel;
          jumpTime = 0;
          passDownOneWayPlatform.OnHitGround(raycastHit.collider);
        }
      }

    }
    else
    {
      isGrounded = false;
    }
  }

  private void flip()
  {
    var current = transform.localScale;
    current.x *= -1;
    transform.localScale = current;
    facingRight = !facingRight;
  }

  private void DoJump()
  {
    if (jumpTime < jumpLimit)
    {
      rb.velocity += Vector2.up * jumpForce;
      jumpTime += 1;
      isGrounded = false;
      lastJumpedTime = Time.time;
      Debug.Log(jumpTime);
    }
  }
}
