using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class time : MonoBehaviour
{
    public int time1;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        time1 = 60;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        InvokeRepeating("decreaseTime", 1.0f, 1.0f);
    }

    void decreaseTime()
    {
        time1--;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Time left: " + time1;
    }
}
