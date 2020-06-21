using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public GameObject[] peoplePrefabs;

    public float generationTimeStep = 0.25f;
    
    [SerializeField] private Path[] paths;

    public float peopleSkewChance = 50f;
    public float obstacleSkewChance = 50f;
    public float obstacleDoubleChance = 75f;

    private Coroutine _generatorCoroutine;
    
    public void StartGenerator()
    {
        _generatorCoroutine = StartCoroutine(CoGenerator());
    }

    public void StopGenerator()
    {
        if (_generatorCoroutine != null)
        {
            StopCoroutine(_generatorCoroutine);
            _generatorCoroutine = null;
        }
    }

    private IEnumerator CoGenerator()
    {

        int peopleStringGap = 0;
        int peopleStringLength = Random.Range((int) 3, 7);
        int currPeopleStringLength = 0;
        int lastPeoplePathIndex = 0;
        
        int obstacleStringGap = 0;
        int obstacleStringLength = Random.Range((int) 2, 5);
        int currObstacleStringLength = 0;
        int lastObstaclePathIndex = 0;
        
        float lastGenTime = 0f;
        float time = 0f;
        while (this.enabled)
        {
            time += Time.deltaTime;

            int obstaclePath = -1;
            int personPath = -1;
            if (time - lastGenTime > generationTimeStep)
            {
                lastGenTime = time;
                
                // Generate something.
                if (peopleStringGap <= 0)
                {
                    if (currPeopleStringLength >= peopleStringLength)
                    {
                        peopleStringLength = Random.Range((int) 3, 7);
                        peopleStringGap = Random.Range((int) 5, 10);
                        lastPeoplePathIndex = Random.Range((int)0, 3);

                        currPeopleStringLength = 0;
                    }
                    else
                    {
                        // Gen person
                        
                        GameObject peep = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)]);
                        PathFollower peepPF = peep.GetComponent<PathFollower>();
                        
                        int rand = Random.Range((int)0, 100);
                        if (rand < peopleSkewChance) // CHANCE TO VARIATE
                        {
                            personPath = (lastPeoplePathIndex + Random.Range((int) 0, 2)) % 3;

                        }
                        else
                        {
                            personPath = lastPeoplePathIndex;
                        }

                        lastPeoplePathIndex = personPath;
                        
                        peepPF.StartPath(paths[personPath]);
                        currPeopleStringLength++;
                    }
                }
                else
                {
                    peopleStringGap--;
                }
                
                // OBSTACLES
                // Generate something.
                if (obstacleStringGap <= 0)
                {
                    if (currObstacleStringLength >= obstacleStringLength)
                    {
                        obstacleStringLength = Random.Range((int) 3, 7);
                        obstacleStringGap = Random.Range((int) 3, 6);
                        lastObstaclePathIndex = Random.Range((int)0, 3);
                        if (lastObstaclePathIndex == personPath)
                        {
                            lastObstaclePathIndex = (lastObstaclePathIndex + 1) % 3;
                        }

                        currPeopleStringLength = 0;
                    }
                    else
                    {
                        // Gen person
                        
                        GameObject obst = GameObject.Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
                        PathFollower obstPF = obst.GetComponent<PathFollower>();
                        
                        int rand = Random.Range((int)0, 100);
                        if (rand < obstacleSkewChance) // CHANCE TO VARIATE
                        {
                            obstaclePath = (lastObstaclePathIndex + Random.Range((int) 0, 2)) % 3;

                        }
                        else
                        {
                            obstaclePath = lastObstaclePathIndex;
                        }

                        lastObstaclePathIndex = obstaclePath;
                        
                        if (lastObstaclePathIndex == personPath)
                        {
                            lastObstaclePathIndex = (lastObstaclePathIndex + 1) % 3;
                        }
                        
                        obstPF.StartPath(paths[lastObstaclePathIndex]);
                        currObstacleStringLength++;
                        
                        // DOUBLE UP
                        int rand2 = Random.Range((int)0, 100);
                        if (rand2 < obstacleDoubleChance) // CHANCE TO DOUBLE
                        {
                            int dubPath = (lastObstaclePathIndex + 1) % 3;
                            if (dubPath == personPath || dubPath == obstaclePath)
                            {
                                dubPath = (dubPath + 1) % 3;
                                if (dubPath == personPath || dubPath == obstaclePath)
                                {
                                    dubPath = (dubPath + 1) % 3;
                                }
                            }
                            
                            GameObject obst2 = GameObject.Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
                            PathFollower obstPF2 = obst2.GetComponent<PathFollower>();
                            obstPF2.StartPath(paths[dubPath]);
                        }
                    }
                }
                else
                {
                    obstacleStringGap--;
                }
                
            }
            
            
            

            yield return null;
        }
        
        _generatorCoroutine = null;
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
        GameObject go = GameObject.Instantiate(obstaclePrefabs[0]);
        PathFollower pathFollower = go.GetComponent<PathFollower>();
        pathFollower.StartPath(paths[pathIndex]);
    }
    
}
