using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variable pour l UI
    [SerializeField] GameObject fpsCount;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject endScreen;
    bool isPause = false;

    [SerializeField] Move player;

    //variable d instance du GameManager
    public static GameManager instance;

    //permet de creer une instance unique de GameManager appelable avec GameManager.instance
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameManager");
            return;
        }
        instance = this;
    }

    //au start on d�sactive les 2 ui
    private void Start()
    {
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
            endScreen.SetActive(false);
        }
    }

    //si on appuie sur la touche Tab on met en pause le jeu
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
            Pause();
    }

    //foncton activant l ui de pause et mettant en pause le jeu
    public void Pause()
    {
        isPause = !isPause;
        player.disableGravityGun = isPause;
        if (isPause)
            Cursor.lockState = CursorLockMode.None;
        else{
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
        }
        fpsCount.SetActive(!isPause);
        pauseScreen.SetActive(isPause);
        Time.timeScale = (isPause ? 0 : 1);
    }

    //foncton activant l ui de fin et mettant en pause le jeu
    public void End()
    {
        Cursor.lockState = CursorLockMode.None;
        fpsCount.SetActive(false);
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    //fonction permettant de faire la transition entre les scenes.
    public void LoadTheGame(int index)
    {
        if(player != null)
            player.disableGravityGun = true;
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

}
