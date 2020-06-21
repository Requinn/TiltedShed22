using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteReport : MonoBehaviour
{
    public delegate void OnBiteEvent();
    public OnBiteEvent BiteEvent;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            if (BiteEvent != null) BiteEvent();
        }
    }
}
