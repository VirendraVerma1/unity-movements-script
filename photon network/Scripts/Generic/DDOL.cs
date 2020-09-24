using UnityEngine;

public class DDOL : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (this);//this will not destroy gameobject while changing scenes
	}
}
