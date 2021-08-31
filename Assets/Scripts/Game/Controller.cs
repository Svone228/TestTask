using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float startPos = 0;
    public float maxOffset = 0;
    float input = 0;
    bool gameStarted = false;
    float horizontalSpeed;
    
    Character character;
    // Start is called before the first frame update
    void Start()
    {
        character = Character.GetCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPos = Input.mousePosition.x;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            input = Input.mousePosition.x - startPos;
            if (maxOffset > Mathf.Abs(input))
            {
                startPos = Input.mousePosition.x;
            }
            else
            {
                maxOffset = Mathf.Abs(input);
                maxOffset = 0;
            }
        }
        

        if (Input.GetAxis("Horizontal") != 0)
        {
            input = Input.GetAxis("Horizontal");
        }

        if(input != 0 && !gameStarted)
        {
            gameStarted = true;
            character.StartGame();
        }

        if(input == 0 && gameStarted) 
        {
            character.Align();
        }
        else 
        {
            character.StopALign();
        }
        if(input != 0) 
        {
            horizontalSpeed = Mathf.Abs(input)/60;
            if (input > 0)
                input = 1;
            else
                input = -1;
            
        }
        if (gameStarted) 
        {
            character.Move(input, horizontalSpeed);
            input = 0;
        }
            
    }
}
