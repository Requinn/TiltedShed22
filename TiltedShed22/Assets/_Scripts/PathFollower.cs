using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    public Transform startPoint;
    public Transform midGroundPoint; // 
    public Transform endPoint;

    public float duration;
    public AnimationCurve posCurve;
    public AnimationCurve scaleCurve;
}

public class PathFollower : MonoBehaviour
{
    [SerializeField] private GameObject root;
    
    private Path path;
    private Coroutine followPathCoroutine;
    
    public void StartPath(Path argPath)
    {
        path = argPath;
        followPathCoroutine = StartCoroutine(CoFollowPath());
    }

    private IEnumerator CoFollowPath()
    {
        float timer = 0f;
        while (timer < path.duration)
        {
            if (Time.timeScale > 0f)
            {
                timer += Time.deltaTime;

                float posLerp = path.posCurve.Evaluate(timer/path.duration);

                Vector3 worldPos = Vector3.Lerp(path.startPoint.position, path.endPoint.position, posLerp);
                
                transform.position = worldPos;

                yield return null;
            }
        }

        followPathCoroutine = null;
        
        Destroy(gameObject);
    }
    
}
