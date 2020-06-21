using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteReport : MonoBehaviour
{
    public delegate void OnBiteEvent();
    public OnBiteEvent BiteEvent;

    public void OnTriggerEnter2D(Collider2D c) {
        if (c.CompareTag("Obstacle")) {
            Debug.Log("Bit " + c.name);
            if (BiteEvent != null) BiteEvent();
            c.gameObject.SetActive(false);
        }
    }
}
