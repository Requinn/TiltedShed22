﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private Coroutine routine = null;

    public void UpdateScoreText(int newScore) {
        if(routine != null) StopCoroutine(routine);
        //_text.text = newScore.ToString("N0");
        routine = StartCoroutine("LerpScore", newScore);
    }

    private IEnumerator LerpScore(int target) {
        int old = int.Parse(_text.text.Replace(",",""));
        float t = old, interp = 0;
        while (interp <= 1.0f) {
            t = Mathf.Lerp(old, target, interp);
            interp += Time.deltaTime * 1.45f;
            _text.text = ((int)t).ToString("N0");
            yield return 0f;
        }
        _text.text = target.ToString("N0");
        yield return 0f;
    }
}
