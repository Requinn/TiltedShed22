using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField]
    private Vector2 _scrollDirection = new Vector2(0,1);
    private Vector2 _appliedDir = new Vector2(0,0); 
    [SerializeField]
    private float _scrollSpeed = 1.0f;
    private float offset = 0;
    private Material _mat;

    public void Start() {
        _mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * _scrollSpeed * 0.1f;
        _appliedDir.x = offset * _scrollDirection.x;
        _appliedDir.y = offset * _scrollDirection.y;
        _mat.SetTextureOffset("_MainTex", _appliedDir);
        if (offset >= 10) offset = 0;
    }
}
