using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BgmAudioManager : MonoBehaviour
{
  [SerializeField] AudioMixer mixer;

  string[] clipVolumes = { "Clip1", "Clip2", "Clip3", "Clip4", "Clip5" };
  int index = 0;
  
  public void MixNextClip()
  {
    if (index < clipVolumes.Length)
    {
      mixer.SetFloat(clipVolumes[index++], 0);
    }
  }

  //private void Update()
  //{
  //  if (Input.GetKeyDown(KeyCode.Space))
  //  {
  //    MixNextClip();
  //  }
  //}
}
