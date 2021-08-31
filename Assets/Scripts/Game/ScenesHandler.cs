using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class ScenesHandler : MonoBehaviour
{
    public Button nextLevel;
    public Button restartLevel;
    //int currentLevel = 0;

    private void Awake()
    {
        nextLevel.onClick.AddListener(() => NextLevel());
        restartLevel.onClick.AddListener(() => RestartLevel());

    }
    public void NextLevel() 
    {
        RestartLevel();
    }
    public void RestartLevel()
    {
        DOTween.KillAll();
        var level = SceneManager.LoadSceneAsync(0);
    }

}
