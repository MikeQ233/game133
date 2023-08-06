using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformCollisionHelper : MonoBehaviour
{
  public string TargetTag = "Player";
  public float Tolerance = 0.5f;
  public float UpWardForce = 5.0f;
  public bool EnableAntiStuck = false;

  private bool isAdjusting = false;
  private GameObject target;

  private void Awake()
  {
    if (EnableAntiStuck == false)
    {
      this.enabled = false;
    }
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    if (this.enabled == false)
    {
      return;
    }
    if (collision.gameObject.tag == TargetTag)
    {
      float distance;
      if (collision.enabled)
      {
        distance = collision.otherCollider.Distance(collision.collider).distance;
        if (Math.Abs(distance) > Tolerance)
        {
          isAdjusting = true;
          target = collision.gameObject;
        }
      } 
    }
  }

  private void FixedUpdate()
  {
    if (isAdjusting)
    {
      target.GetComponent<Rigidbody2D>().AddForce(Vector2.up * UpWardForce);
      isAdjusting = false;
    }
  }
}
