using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var character = Character.GetCharacter();
        character.WinLevel();
        

    }
}
