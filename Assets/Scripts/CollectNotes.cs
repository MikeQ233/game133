using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectNotes : MonoBehaviour
{
  public int currentNotes;
  public int totalNotes = 4;
  private BgmAudioManager audioManager;
  private Toast toast;
  private string[] intrustments = { "Drums", "Bass", "Lead", "Organ" };
  private void Awake()
  {
    audioManager = GetComponent<BgmAudioManager>();
    toast = GetComponent<Toast>();
  }

  void Start()
  {
    currentNotes = 0;
    audioManager.MixNextClip();
    toast.showToast("Collect notes to improve the performance! Bride and groom are waiting!!", 4);
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "note")
    {
      Destroy(collision.gameObject);
      currentNotes++;
      audioManager.MixNextClip();
      if (currentNotes == totalNotes)
      {
        toast.showToast(intrustments[currentNotes - 1] + " collected, enter the wedding bottom right and play the organ!!", 6);
      } 
      else
      {
        toast.showToast(intrustments[currentNotes - 1] + " collected", 2);
      }

      if (currentNotes == totalNotes)
      {
        var door = GameObject.FindWithTag("Door");
        if (door)
        {
          door.GetComponent<OpenDoor>().OpenOneWayDoor();
        }
      }
    }
  }
}
