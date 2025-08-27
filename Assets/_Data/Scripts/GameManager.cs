using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameEvent
{
    public GameObject canvasSetting;
    public GameObject canvasGameOver;

    private void Awake()
    {
        if (canvasSetting != null)
            canvasSetting.SetActive(false);
        if (canvasGameOver != null)
            canvasGameOver.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        canvasSetting.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        canvasSetting.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Map_1");
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
        DOTween.Clear(true);
    }

    public void GameOver()
    {
        StartCoroutine(PlayerDead());
    }

    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(2f);
        canvasGameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
