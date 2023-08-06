using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
  PlatformEffector2D effector;

  private void Awake()
  {
    effector = GetComponent<PlatformEffector2D>();
  }

  public void OpenOneWayDoor()
  {
    effector.rotationalOffset = (180 + effector.rotationalOffset) % 360;
  }

  //private void Update()
  //{
  //  if (Input.GetKeyDown(KeyCode.Space))
  //  {
  //    OpenOneWayDoor();
  //  }
  //}
}
