using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Character : MonoBehaviour
{
    static Character mainCharacter;
    public GameObject cubes;
    Rigidbody rb;
    public float speed;
    Tween turnAnim, alignAnim;
    List<GameObject> cubesList;
    public GameObject model;
    bool stop = false;
    int coinsCount = 0;


    //float canvasHeight, canvasWidth;
    private void Awake()
    {
        cubesList = new List<GameObject>();
        mainCharacter = this;
        rb = GetComponent<Rigidbody>();
        SetDefaultAnim();
        model.GetComponent<Rigidbody>().freezeRotation = true;

    }
    /*private void Start()
    {
        canvasHeight = UIHandler.GetUIHandler().canvasHeight;
        canvasWidth = UIHandler.GetUIHandler().canvasWidth;
    }*/
    public void StartGame()
    {
        model.GetComponent<Animator>().SetFloat("MoveSpeed", speed);
        UIHandler.GetUIHandler().InGameScreenShow();


    }

    // Update is called once per frame
   
    public void Move(float horizontal,float horizontalSpeed)
    {
        if (!stop)
        {
            float a = horizontal * horizontalSpeed * Time.deltaTime;
            transform.Translate(a, 0, speed * Time.deltaTime);
            if (transform.localPosition.x < -2)
            {
                transform.DOLocalMoveX(-2, 0);
            }
            if (transform.localPosition.x > 2)
            {
                transform.DOLocalMoveX(2, 0);
            }
        }
    }
    public void Align()
    {
        float x = Mathf.Round(transform.localPosition.x * 10f) / 10f;
        alignAnim = transform.DOLocalMoveX(x, 0.1f);

    }
    public void StopALign()
    {
        alignAnim?.Kill();
        alignAnim = null;
    }
    public void Turn(Vector3 direction)
    {
        turnAnim = transform.DORotate(direction, 0.3f);
        turnAnim.OnUpdate(() => { turnAnim = null; });
    }
    public void AddCube(GameObject cube)
    {

        if (!cubesList.Contains(cube))
        {
            UpPosition(gameObject);
            cubesList.Add(cube);
            cube.transform.SetParent(cubes.transform);

            for (int i = 0; i < cubesList.Count; i++)
            {
                cubesList[i].transform.position = transform.position - new Vector3(0, i + 1, 0);
            }
        }
    }
    public void RemoveCubes(GameObject cube)
    {
        if (cube == gameObject)
        {
            FailLevel();
        }
        else
        {
            cubesList.Remove(cube);
            Destroy(cube);
            SetFallAnim();
        }


    }
    public void AddCoin()
    {
        coinsCount++;
        UIHandler.GetUIHandler().AddCoin(coinsCount);
    }
    void UpPosition(GameObject cube, int additional = 1)
    {
        cube.transform.position += new Vector3(0, additional, 0);
    }
    void SetFallAnim()
    {
        if (!stop)
        {
            float pause = 0.3f;
            float startCubeY = 3.05f;
            turnAnim.Kill();
            turnAnim = transform.DOMoveY((startCubeY + cubesList.Count), 0.4f);
            turnAnim.SetDelay(pause);
            turnAnim.OnComplete(() => { turnAnim = null; });

        }
    }
    void SetDefaultAnim()
    {
        model.GetComponent<Animator>().SetFloat("MoveSpeed", 0);
        model.GetComponent<Animator>().SetBool("Grounded", true);
    }
    void SetWaveAnim()
    {
        SetDefaultAnim();
        model.GetComponent<Animator>().SetBool("Wave", true);
        model.transform.LookAt(Camera.main.transform);
    }
    public void WinLevel()
    {
        stop = true;
        UIHandler.GetUIHandler().WinScreenShow();
        SetWaveAnim();
    }
    public void FailLevel()
    {
        stop = true;
        UIHandler.GetUIHandler().LoseScreenShow();
        SetDefaultAnim();
        model.GetComponent<Rigidbody>().freezeRotation = false;
    }



    public static Character GetCharacter()
    {
        return mainCharacter;
    }

    

}
