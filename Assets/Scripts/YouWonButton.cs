using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWonButton : MonoBehaviour
{
  private void Start()
  {
    Invoke("ToMenu", 5);
  }

  void ToMenu()
  {
    SceneManager.LoadScene("Menu");
  }
}
