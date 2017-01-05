using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionsFunctions : MonoBehaviour {

    private string home = "MainMenu";

    public void PlayThisAgain() {
        //Need to discard any progress in this scene --TODO--
        StateManager.Instance.SetAsLastState(SceneState.ActiveTimeRange - 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoBackHome()
    {
        SceneManager.LoadScene(home);

    }

}
