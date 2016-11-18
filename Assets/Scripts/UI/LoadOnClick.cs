using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingPanel;
	
	public void LoadScene (string scene) {
		GameObject child = (GameObject)GameObject.Instantiate(loadingPanel);
		RectTransform parent = (RectTransform)(GetComponentInParent<Canvas>().gameObject.transform);
		child.transform.SetParent(parent, false);
        SceneManager.LoadScene(scene);
	}
}
