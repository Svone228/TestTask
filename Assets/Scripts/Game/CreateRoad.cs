using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject routeTilePrefab;
    public GameObject start;
    public GameObject road;
    public GameObject startTile;
    public GameObject bonusCube;
    public GameObject barrier;
    public GameObject finish;
    public GameObject coin;
    int extraCubes = 0;
    [System.Serializable]
    public class Route
    {
        public MoveDirection moveDirection;
        public int moveLength;
    }
    public List<Route> routes;

    List<GameObject> tileList;



    float prefab_zscale;
    Vector3 direction;
    void Awake()
    {
        tileList = new List<GameObject>();
        tileList.Add(start);
        prefab_zscale = routeTilePrefab.transform.localScale.z;
        direction = new Vector3(0, 0, 1) * prefab_zscale;
        CreateRoutes();

    }
    void CreateRoutes()
    {
        for (int i = 0; i < routes.Count; i++)
        {
            CreateTiles(routes[i]);

        }
        CreateFinish();
    }

    void CreateTiles(Route route)
    {
        var turn = CreateTurn(route);
        GameObject temp, lastTile;


        for (int i = 1; i < route.moveLength; i++)
        {
            temp = Instantiate(routeTilePrefab, turn.transform);
            lastTile = tileList[tileList.Count - 1];
            temp.transform.position = CalculatePosition(lastTile);
            tileList.Add(temp);
            var created = CreateBonusCube(temp);
            if(!created)
                created = CreateBarier(temp);
            if (!created)
                created = CreateCoin(temp);
        }

    }
    void CreateFinish() 
    {
        var finish = Instantiate(this.finish);
        var lastTile = tileList[tileList.Count - 1];
        finish.transform.position = CalculatePosition(lastTile);
        finish.transform.LookAt(lastTile.transform);
    }
    bool CreateCoin(GameObject tile) 
    {
        bool boolean = (Random.Range(0, 11) > 8);//1/3 chanse
        if (boolean)
        {
            var temp = Instantiate(coin);
            //temp.transform.localScale = new Vector3(1, 1, 1);
            temp.transform.SetParent(tile.transform);
            temp.transform.position = new Vector3(Random.Range(-2, 2), 3.05f, 0) + tile.transform.position;
            temp.GetComponent<CoinAnimation>().StartAnimation();
            return true;
        }
        return false;
    }
    GameObject CreateTurn(Route route) 
    {
        var turn = new GameObject("Turn");
        turn.transform.SetParent(road.transform);
        var temp = Instantiate(startTile, turn.transform);
        temp.GetComponent<TurnWatcher>().turn = turn;
        var lastTile = tileList[tileList.Count - 1];
        temp.transform.position = CalculatePosition(lastTile);
        direction = RotateDirection(direction, (int)route.moveDirection);
        turn.transform.position = temp.transform.position;
        temp.transform.localPosition = Vector3.zero;

        turn.transform.LookAt(CalculatePosition(temp));
        temp.transform.LookAt(CalculatePosition(temp));
        tileList.Add(temp);
        
        return turn;
    }

    bool CreateBonusCube(GameObject tile) 
    {
        bool boolean = (Random.Range(0,11) > 8);//1/5 chanse
        if (boolean) 
        {
            var temp = Instantiate(bonusCube);
            //temp.transform.localScale = new Vector3(1, 1, 1);
            temp.transform.SetParent(tile.transform);
            temp.transform.position = new Vector3(Random.Range(-2, 2), 3.05f, 0) + tile.transform.position;
            extraCubes++;
            return true;
        }
        return false;
    }
    bool CreateBarier(GameObject tile) 
    {
        bool boolean = (Random.Range(0, 15) > 12);//1/7 chanse
        if (boolean)
        {
            float min_height = (extraCubes - 2) / 1.5f;
            if (min_height < 0)
                min_height = 0;
            Mathf.RoundToInt(min_height);
            extraCubes -= (int)min_height;
            float max_height = (min_height + 2) * 1.3f;
            Mathf.RoundToInt(max_height);
            var temp = Instantiate(barrier);
            temp.transform.rotation = tile.transform.rotation;
            temp.transform.position = tile.transform.position + new Vector3(0, 3, 0);
            temp.GetComponent<Barrier>().CreateBarrier(new BarrierHeight((int)min_height, (int)max_height),tile.transform);
            temp.transform.SetParent(tile.transform);
            return true;
        }
        return false;
    }
    Vector3 RotateDirection(Vector3 direction, int angle)
    {
        Vector2 point = new Vector2(direction.x, direction.z);
        float radAngle = Mathf.Deg2Rad * angle;
        float newX = point.x * Mathf.Round(Mathf.Cos(radAngle)) - point.y * Mathf.Round(Mathf.Sin(radAngle));
        float newY = point.x * Mathf.Round(Mathf.Sin(radAngle)) + point.y * Mathf.Round(Mathf.Cos(radAngle));

        var result = new Vector2(newX, newY);
        return new Vector3(result.x, 0, result.y);
    }



    Vector3 CalculatePosition(GameObject LastTile)
    {
        return LastTile.transform.position + direction;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public enum MoveDirection
{
    Forward = 0,
    Left = 90,
    Right = -90
}
