﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles starting/ending a "level/round/map/play/etc"
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;

    private int _totalScore = 0;
    public int Score {
        get { return _totalScore; }
    }

    [Header("UI Components")]
    [SerializeField]
    private GameObject _gameOverScreen;
    [SerializeField]
    private TextMeshProUGUI _finalScoreText;
    [SerializeField]
    private TextMeshProUGUI _countDownText;
    [SerializeField]
    private ScoreText _runningScoreText;

    private bool _isCountingDown = false;

    public void UpdateScore(int delta) {
        _totalScore += delta;
        _runningScoreText.UpdateScoreText(_totalScore);
    }

    public void Start() {
        _totalScore = 0;
        _countDownText.gameObject.SetActive(false);
        _gameOverScreen.SetActive(false);
    }

    public void OnLevelWasLoaded(int level) {
        //listen for player death
        //_player
        StartLevel();
    }

    /// <summary>
    /// public call to start the level
    /// </summary>
    /// <param name="countDown"></param>
    public void StartLevel(float countDown  = 3.0f) {
        if (!_isCountingDown) {
            _isCountingDown = true;
            _countDownText.gameObject.SetActive(true);
            StartCoroutine(CountDownCo(countDown));
        }
    }

    /// <summary>
    /// Countdown and set text;
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator CountDownCo(float time) {
        int t = 0;
        time++;
        while(time > 1.0f) {
            t = Mathf.FloorToInt(time);
            switch (t) {
                case 3: _countDownText.color = Color.red; break;
                case 2: _countDownText.color = new Color(1.0f, 0.6337f, 0.0f); break;
                case 1: _countDownText.color = Color.yellow; break;
            }
            _countDownText.text = t.ToString();
            time -= Time.deltaTime;
            yield return 0f;
        }
        _countDownText.color = Color.green;
        _countDownText.text = "GO!";
        StartPlayer();
        yield return new WaitForSeconds(0.5f);
        _countDownText.gameObject.SetActive(false);
        yield return 0f;
    }

    /// <summary>
    /// Send player command either through an event or direct call
    /// </summary>
    public void StartPlayer() {
        Debug.Log("Player Started!");
        //_player.
    }

    /// <summary>
    /// When player dead, bring up UI
    /// </summary>
    public void OnPlayerDeath() {
        _finalScoreText.text = _totalScore.ToString("N0");
        _gameOverScreen.SetActive(true);
    }

    public void LoadLevel(int levelID = 99) {
        if (levelID == 99) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
        else { SceneManager.LoadScene(levelID); }
        //some kind of fade in and out
    }

}
