using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveDownSupport : MonoBehaviour
{
  private TilemapCollider2D tilemapCollider;
  private bool isPassThroughEnabled;
  private void Awake()
  {
    tilemapCollider = GetComponent<TilemapCollider2D>();
    isPassThroughEnabled = false;
  }

  public void EnablePassThrough(Collider2D targetCollider)
  {
    isPassThroughEnabled = true;
    Physics2D.IgnoreCollision(targetCollider, tilemapCollider);
  }

  public void DisablePassThrough(Collider2D targetCollider)
  {
    isPassThroughEnabled = false;
    Physics2D.IgnoreCollision(targetCollider, tilemapCollider, false);
  }

  public bool IsPassThroughEnabled()
  {
    return isPassThroughEnabled;
  }
}
