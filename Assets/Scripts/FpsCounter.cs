using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// https://forum.unity.com/threads/fps-counter.505495/#post-9841830
// https://forum.unity.com/threads/fps-counter.505495/#post-9884526
public class FpsCounter : MonoBehaviour
{
    public float SmoothSpeed = 1f;
    float fps, smoothFps;
    Text text;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update(){
        fps = 1f / Time.smoothDeltaTime;
        if(Time.timeSinceLevelLoad < 0.1f) smoothFps = fps;
        smoothFps += (fps - smoothFps) * Mathf.Clamp(Time.deltaTime * SmoothSpeed, 0, 1);
        text.text = ((int)smoothFps).ToString()+" fps";
    }
}
