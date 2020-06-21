using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject basicObstaclePrefab;
    [SerializeField] private Path[] paths;
    
    public void StartGenerator()
    {
        
    }

    public void StopGenerator()
    {
        
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TestPath(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TestPath(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TestPath(2);
        }
    }
    
    public void TestPath(int pathIndex)
    {
        GameObject go = GameObject.Instantiate(basicObstaclePrefab);
        PathFollower pathFollower = go.GetComponent<PathFollower>();
        pathFollower.StartPath(paths[pathIndex]);
    }
    
}
