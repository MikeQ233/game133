using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PassDownOneWayPlatform : MonoBehaviour
{
  // in seconds
  public float DoubleTapInterval = 0.5f;
  public string OneWayPlatformTag = "OneWayPlatform";

  private float doubleTapD = -99.0f;
  private GameObject oneWayPlatform;

  private void Awake()
  {
    oneWayPlatform = null;
  }
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S))
    {
      if (doubleTapD + DoubleTapInterval > Time.time && GetComponent<movement>().isGrounded)
      {
        if (oneWayPlatform && oneWayPlatform.GetComponent<MoveDownSupport>())
        {
          oneWayPlatform.GetComponent<MoveDownSupport>().EnablePassThrough(GetComponent<BoxCollider2D>());
        }
      }
      doubleTapD = Time.time;
    }
  }

  public void OnHitGround(Collider2D collision)
  {
    if (oneWayPlatform && oneWayPlatform.GetComponent<MoveDownSupport>())
    {
      oneWayPlatform.GetComponent<MoveDownSupport>().DisablePassThrough(GetComponent<BoxCollider2D>());
    }
    oneWayPlatform = collision.gameObject;
  }
}

