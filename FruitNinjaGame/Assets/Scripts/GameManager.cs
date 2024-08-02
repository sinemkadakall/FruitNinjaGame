using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    private Blade blade;
    private Spawner spawner;
    public UnityEngine.UI.Image fadeImage;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        blade = FindObjectOfType<Blade>();
    }


    private void Start()
    {
        newGame();
    }
    void newGame()
    {
        Time.timeScale = 1f;
        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        clearScene();
      
    }

    private void clearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

   public void IncreaseScore()
    {
        score++;
        scoreText.text=score.ToString();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duraiton=0.5f;

        while (elapsed < duraiton)
        {
            float t = Mathf.Clamp01(elapsed / duraiton);
            fadeImage.color = Color.Lerp(Color.clear,Color.white,t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        newGame();

        elapsed = 0f;

        while (elapsed < duraiton)
        {
            float t = Mathf.Clamp01(elapsed / duraiton);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

           
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
