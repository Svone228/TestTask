using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollider : MonoBehaviour
{
    public Barrier barrier;
    private void OnTriggerEnter(Collider other)
    {
        var character = Character.GetCharacter();
        character.RemoveCubes(other.gameObject);
        barrier.CollideDetected();
        
        
    }
}
