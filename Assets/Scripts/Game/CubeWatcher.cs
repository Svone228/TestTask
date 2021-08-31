using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWatcher : MonoBehaviour
{
    BoxCollider collid;
    private void Awake()
    {
        collid = GetComponent<BoxCollider>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        var character = Character.GetCharacter();
        character.AddCube(gameObject);
        collid.isTrigger = false;


    }
    

}
