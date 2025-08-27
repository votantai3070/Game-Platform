using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameEvent
{
    public GameObject canvasSetting;

    private void Awake()
    {
        if (canvasSetting != null)
            canvasSetting.SetActive(false);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}
