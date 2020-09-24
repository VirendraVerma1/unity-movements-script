using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

	public static PlayerNetwork Instance;
	public string PlayerName{ get; private set;}
	// Use this for initialization
	void Awake () {
		Instance = this;
		PlayerName = "Viru#" + Random.Range (10000, 9999);//getting some random name
	}
}
