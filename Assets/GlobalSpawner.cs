using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    public GameObject player;
    public float distanceFromPlayerRadius;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public GameObject objectPrefab;
    [Range(1,10)]
    public int maxNumberOfObjects;

    private List<GameObject> objects;
   
    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        if (ShouldSpawn())
        {
            SpawnAtRandomPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn())
        {
            SpawnAtRandomPosition();
        }
    }

    bool ShouldSpawn()
    {
        return objects.Count <= maxNumberOfObjects;
    }

    void SpawnAtRandomPosition()
    {
        bool spawned = false;

        while (!spawned)
        {
            Vector2 pos = GetRandomPosition(minPosition, maxPosition);
            bool isPositionTooCloseToTargetPosition = IsPositionTooCloseToTargetPosition(player.transform, pos, distanceFromPlayerRadius);
            bool isCloseToAnotherObject = IsCloseToAnotherObject(objects, pos, distanceFromPlayerRadius);
            print(isPositionTooCloseToTargetPosition);
            print(isCloseToAnotherObject);
            if (!isPositionTooCloseToTargetPosition && !isCloseToAnotherObject)
            {
                GameObject obj = Instantiate(objectPrefab,pos,Quaternion.identity) as GameObject;
                objects.Add(obj);
                spawned = true;
            }
        }
    } 

    bool IsCloseToAnotherObject(List<GameObject> objects, Vector2 position, float radius)
    {
        foreach (var obj in objects)
        {
            if (Vector2.Distance(obj.transform.position, position) < radius)
            {
                return true;
            }
        }

        return false;
    }

    bool IsPositionTooCloseToTargetPosition(Transform targetPosition, Vector2 position, float radius)
    {
        return Vector2.Distance(targetPosition.position, position) < radius;
    }


    Vector2 GetRandomPosition(Vector2 minPosition, Vector2 maxPosition)
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float y = Random.Range(minPosition.y, maxPosition.y);

        return new Vector2(x, y);
    }
}
