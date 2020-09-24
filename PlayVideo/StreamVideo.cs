using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
	public RawImage rawImage;
	public VideoPlayer videoPlayer;
	public AudioSource audioSource;
	
	void Start()
	{
		StartCoroutine(PlayVideo());
	}
	
	IEnumerator PalyVideo()
	{
		videoPlayer.Prepare();
		waitForSceconds waitForSceconds=new waitForSceconds(1);
		while(!videoPlayer.isPrepared)
		{
			yield return waitForSceconds;
			break;
		}
		rawImage.texture=videoPlayer.texture;
		videoPlayer.Play();
		audioSource.Play();
	}
}