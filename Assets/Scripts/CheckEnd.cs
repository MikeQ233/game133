using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnd : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<Toast>().showToast("You playing the Organ for them!! Happy wedding!!", 5);
      Invoke("endGame", 8.0f);
    }
  }

  void endGame()
  {
    SceneManager.LoadScene("Ending");
  } 
}
