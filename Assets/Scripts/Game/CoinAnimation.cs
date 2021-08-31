using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CoinAnimation : MonoBehaviour
{
    Rigidbody rb;
    public GameObject rotateObj;
    Tween anim1, anim2;
    bool taked = false;
    public void StartAnimation() 
    {
        StartRotateAnimation();
        JumpAnimation();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!taked) 
        {
            taked = true;
            Character.GetCharacter().AddCoin();
            Destroy(gameObject);
            
        }

    }
    void StartRotateAnimation() 
    {
        anim1 = rotateObj.transform.DOLocalRotate(rotateObj.transform.rotation.eulerAngles + new Vector3(0, 180, 0), 0.8f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        
    }
    void JumpAnimation() 
    {
        anim2 = transform.DOJump(transform.position, 0.3f, 1, 1.5f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
    void OnDestroy() 
    {
        anim1.Kill();
        anim2.Kill();
    }
}
