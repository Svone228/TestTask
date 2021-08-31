using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIHandler : MonoBehaviour
{
    public GameObject image1, image2,pointer;
    Sequence startScreenSequence,coinSequence;
    public Canvas canvas;
    public RectTransform startScreen, winScreen, loseScreen, inGameScreen;
    static UIHandler currentHandler;
    public Text coinsCount;
    public float canvasHeight = 1920, canvasWidth = 1080;
    float animationTime = 0.3f;
    private void Awake()
    {
        currentHandler = this;
        StartAnimation();
    }

    public void StartScreenShow() 
    {
        CloseAll();
        startScreen.transform.DOLocalMoveX(0, animationTime);
        StartScreenContinueAnimation();


    }

    void StartScreenPauseAnimation()
    {
        startScreenSequence.Pause();

    }
    void StartScreenContinueAnimation()
    {
        startScreenSequence.Play();
    }
    void StartAnimation()
    {
        pointer.transform.localPosition = image1.transform.localPosition - new Vector3(0, 20, 0);
        startScreenSequence = DOTween.Sequence();

        Tween anim1 = pointer.transform.DOLocalMoveX(image2.transform.localPosition.x, 1);
        Tween anim2 = pointer.transform.DOLocalMoveX(image1.transform.localPosition.x, 1);
        startScreenSequence.Append(anim1);
        startScreenSequence.Append(anim2);
        startScreenSequence.OnComplete(() => { startScreenSequence.Restart(); });

    }
    


    public void WinScreenShow()
    {
        CloseAll();
        winScreen.transform.DOLocalMoveX(0, animationTime);
    }
    public void LoseScreenShow()
    {
        CloseAll();
        loseScreen.transform.DOLocalMoveY(0, animationTime);
    }
    

    public void InGameScreenShow() 
    {
        CloseAll();
        inGameScreen.transform.DOLocalMoveX(0, animationTime);
    }
    void CloseAll()
    {
        inGameScreen.DOAnchorPosX(canvasWidth * 2, animationTime);
        winScreen.DOAnchorPosX(-canvasWidth * 2, animationTime);
        loseScreen.DOAnchorPosY(canvasHeight * 2, animationTime);
        startScreen.DOAnchorPosY(-canvasHeight * 2, animationTime);
        StartScreenPauseAnimation();
    }

    public void AddCoin(int count) 
    {
        coinSequence = DOTween.Sequence();
        var anim1 = coinsCount.rectTransform.DOAnchorPosY(40,0.25f);
        anim1.OnComplete(() => { coinsCount.text = count.ToString(); });
        var anim2 = coinsCount.rectTransform.DOAnchorPosY(0, 0.25f);
        coinSequence.Append(anim1);
        coinSequence.Append(anim2);
        coinSequence.Play();
    }
    public static UIHandler GetUIHandler() 
    {
        return currentHandler;
    }
    void OnDestroy()
    {
        startScreenSequence.Kill();
        coinSequence.Kill();
    }
    
}
