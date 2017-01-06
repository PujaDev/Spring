using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionsFunctions : MonoBehaviour {

    private string home = "MainMenu";

    public void PlayThisAgain() {
        StateManager.Instance.ResetCurrentScene();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene(home);

    }

}
