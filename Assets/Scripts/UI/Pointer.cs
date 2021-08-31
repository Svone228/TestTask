using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StartScreenAnimation : MonoBehaviour
{
    public GameObject screen;
    public Image image1, image2;
    Sequence sequence;
    void Start()
    {
       
    }

   
    void PauseAnimation() 
    {
        sequence.Pause();
    }
    void ContinueAnimation() 
    {
        sequence.Play();
    }
    void OnDestroy() 
    {
        sequence.Kill();
    }
}
