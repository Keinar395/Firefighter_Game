using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    public Slider postureSlider;

    public void SetMaxPosture(int posture)
    {
        postureSlider.maxValue = posture;
        //postureSlider.value = posture;
    }

    public void SetPosture(int posture)
    {
        postureSlider.value = posture;
    }

    public void AddPosture(int posture)
    {
        postureSlider.value += posture;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity; // Can barýný sabit tut
    }

}
