using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] float timeToWin;
    [SerializeField] float decreaseRate = 1f;
    float timer = 0f;

    [SerializeField] PlayerController player;

    [SerializeField] UnityEvent onWin;

    [SerializeField] Slider timerSlider;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void UpdateTimer()
    {
        if (player.OnJerry)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime * decreaseRate;
            timer = Mathf.Clamp(timer, 0f, timeToWin);
        }
        if (HasWon())
        {
            onWin.Invoke();
            SceneManager.LoadScene("WinScene");
        }

        UpdateTimerSlider();
    }

    public bool HasWon()
    {
        return timer >= timeToWin;
    }

    private void UpdateTimerSlider()
    {
        timerSlider.value = timer / timeToWin;
    }
}
