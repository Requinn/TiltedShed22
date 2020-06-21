using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public GameObject[] peoplePrefabs;

    public float generationTimeStep = 0.25f;
    
    [SerializeField] private Path[] paths;

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
        
        
        
        float lastGenTime = 0f;
        float time = 0f;
        while (this.enabled)
        {
            time += Time.deltaTime;

            int personPath = -1;
            if (time - lastGenTime > generationTimeStep)
            {
                // Generate something.
                if (peopleStringGap <= 0)
                {
                    if (currPeopleStringLength >= peopleStringLength)
                    {
                        peopleStringLength = Random.Range((int) 3, 7);
                        peopleStringGap = Random.Range((int) 5, 10);
                        lastPeoplePathIndex = Random.Range(0, peoplePrefabs.Length);
                    }
                    else
                    {
                        // Gen person
                        //int rand = Random.Range((int)0, 100);
                        //if(rand =)
                        GameObject peep = GameObject.Instantiate(peoplePrefabs[Random.Range(0, peoplePrefabs.Length)]);
                        PathFollower peepPF = peep.GetComponent<PathFollower>();
                        peepPF.StartPath(paths[lastPeoplePathIndex]);
                        personPath = lastPeoplePathIndex;
                        peopleStringLength++;
                    }
                }
                else
                {
                    peopleStringGap--;
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
