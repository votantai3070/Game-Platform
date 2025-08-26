using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameEvent
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuGame");
    }

    public void PauseGame()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Map_1");
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
