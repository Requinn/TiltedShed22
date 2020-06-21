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
       // float scaleStart = 0f;
       // float scaleMid = 1f;

        float timer = 0f;
        while (timer < path.duration)
        {
            if (Time.timeScale > 0f)
            {
                timer += Time.deltaTime;

                float posLerp = path.posCurve.Evaluate(timer/path.duration);

                Vector3 worldPos = Vector3.Lerp(path.startPoint.position, path.endPoint.position, posLerp);
                
//                Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
//                screenPos.z = 10f;
//                transform.position = Camera.main.ScreenToWorldPoint(screenPos);
                
                transform.position = worldPos;
                
                
                //float scaleLerp = path.scaleCurve.Evaluate(timer/path.duration);
                
                //transform.position = Vector3.ProjectOnPlane(worldPos, Vector3.forward);
                //root.transform.position = Vector3.ProjectOnPlane(worldPos, Vector3.forward);

                //float zDelta = path.endPoint.position.z - path.startPoint.position.z;
                
                //float startDepth = Camera.main.
//                scaleLerp = (worldPos.z - path.startPoint.position.z) / zDelta;
//                root.transform.localScale = Vector3.LerpUnclamped(Vector3.one * 0.25f, Vector3.one * 1.25f, scaleLerp);
                
                
                //root.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, scaleLerp);

                yield return null;
            }
        }

        followPathCoroutine = null;
        
        Destroy(gameObject);
    }
    
}
