using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject tilePrefab;
    List<GameObject> barriers = new List<GameObject>();
    /*BarrierHeight barrierHeight = new BarrierHeight(0, 2, 3, 2, 1);
    private void Awake()
    {
        CreateBarrier(barrierHeight);
    }*/
    public void CreateBarrier(BarrierHeight barrierHeight, Transform tile) 
    {
        int startPosition = -2;
        int[] heights = barrierHeight.GetHeigths();
        for (int i = 0; i < heights.Length; i++)
        {
            CreateColumn(heights[i],startPosition+i).transform.SetParent(tile);
            
        }
    }
    GameObject CreateColumn(int heigth, int offset) 
    {
        var column = new GameObject();
        column.transform.SetParent(transform);
        column.transform.localPosition = new Vector3(0 + offset, 0, 0);
        for (int i = 0; i < heigth; i++)
        {

            var temp = Instantiate(tilePrefab, column.transform);
            barriers.Add(temp);
            temp.transform.localPosition = new Vector3(0, i, 0);
            temp.GetComponent<BarrierCollider>().barrier = this;
        }
        return column;
    }
    public void CollideDetected()
    {
        for (int i = 0; i < barriers.Count; i++)
        {
            barriers[i].GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}

public class BarrierHeight 
{
    int[] heights;
    public BarrierHeight(int first,int second,int third,int fourth,int fifth) 
    {
        heights = new int[5];
        heights[0] = first;
        heights[1] = second;
        heights[2] = third;
        heights[3] = fourth;
        heights[4] = fifth;
    }
    public BarrierHeight(int min, int max) 
    {
        int min_pos = Random.Range(0, 5);
        heights = new int[5];
        for (int i = 0; i < heights.Length; i++)
        {
            heights[i] = Random.Range(min, max + 1);//inclusive
        }
        heights[min_pos] = min;
    }
    public int[] GetHeigths()
    {
        return heights;
    }
}