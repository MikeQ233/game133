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
  // used when player holding down w key while grounded.
  private float lastJumpedTime;
  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();
    passDownOneWayPlatform = GetComponent<PassDownOneWayPlatform>();
    jumpTime = 0;
  }

  // Update is called once per frame
  void Update()
  {
    moveHorizontal = Input.GetAxisRaw("Horizontal");
    if (Input.GetKeyDown(KeyCode.W) && jumpTime < jumpLimit)
    {
      DoJump();
    }
    else if (isGrounded == true && Input.GetKey(KeyCode.W) && Time.time - lastJumpedTime > 0.5f)
    {
      DoJump();
    }
  }
  void FixedUpdate()
  {
    if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
    {
      rb.AddForce(new Vector2(moveHorizontal * speed, 0), ForceMode2D.Impulse);
    }
    checkIsGrounded();
  }

  private void checkIsGrounded()
  {
    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center + Vector3.down * 1.0f, boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, 0.25f, PlatformLayerMask);
    if (raycastHit.collider != null)
    {
      if (raycastHit.collider.gameObject.GetComponent<MoveDownSupport>()?.IsPassThroughEnabled() ?? false)
      {
        isGrounded = false;
      } 
      else
      {
        isGrounded = true;
        jumpTime = 0;
        passDownOneWayPlatform.OnHitGround(raycastHit.collider);
      }

    }
    else
    {
      isGrounded = false;
    }
  }

  //void OnTriggerEnter2D(Collider2D collision)
  //{
  //  if (collision.gameObject.tag == "platform")
  //  {
  //    var moveDownSupp = collision.gameObject.GetComponent<MoveDownSupport>();
  //    if (!moveDownSupp || !moveDownSupp.IsPassThroughEnabled())
  //    {
  //      isGrounded = false;
  //    } 
  //    else
  //    {
  //      isGrounded = true;
  //      jumpTime = 0;
  //    }
  //  }
  //}

  //void OnTriggerExit2D(Collider2D collision)
  //{
  //  if (collision.gameObject.tag == "platform")
  //  {
  //    isGrounded = false;
  //  }
  //}

  private void DoJump()
  {
    if (jumpTime < jumpLimit)
    {
      rb.velocity += Vector2.up * jumpForce;
      jumpTime += 1;
      isGrounded = false;
      lastJumpedTime = Time.time;
    }
  }
}
