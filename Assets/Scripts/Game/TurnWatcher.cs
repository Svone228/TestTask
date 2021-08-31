using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TurnWatcher : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject turn;
    private void OnTriggerEnter(Collider other)
    {
        var character = Character.GetCharacter();
        character.transform.SetParent(turn.transform);
        character.Turn(transform.rotation.eulerAngles);

    }
}
