using System.Collections;
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

    [Header("Level Components")]
    [SerializeField]
    private ScrollingTexture[] _scrollingTextures;
    [SerializeField]
    private LevelGenerator _generator;

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
        _player.pDied += OnPlayerDeath;
        _player.pScored += UpdateScore;

        _totalScore = 0;
        _countDownText.gameObject.SetActive(false);
        _gameOverScreen.SetActive(false);
    }

    public void OnLevelWasLoaded(int level) {
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
        OnTimerFinished();
        yield return new WaitForSeconds(0.5f);
        _countDownText.gameObject.SetActive(false);
        yield return 0f;
    }

    /// <summary>
    /// Start the object spawner
    /// </summary>
    public void OnTimerFinished() {
        //_player.ToggleRunning(true);
    }

    /// <summary>
    /// When player dead, bring up UI
    /// </summary>
    public void OnPlayerDeath() {
        _finalScoreText.text = _totalScore.ToString("N0");
        _generator.StopGenerator();
        foreach (ScrollingTexture s in _scrollingTextures) {
            s.enabled = false;
        }
        StartCoroutine(ShowGameEnd());
    }

    private IEnumerator ShowGameEnd() {
        yield return new WaitForSeconds(1.5f);
        _gameOverScreen.SetActive(true);
        yield return 0f;
    }

    public void LoadLevel(int levelID = 99) {
        if (levelID == 99) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
        else { SceneManager.LoadScene(levelID); }
        //some kind of fade in and out
    }

}
