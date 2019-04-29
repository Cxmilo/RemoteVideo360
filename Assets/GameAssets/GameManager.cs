using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public VideoPlayer videoPlayer;
    public VideoPlayer IntroVideoPlayer;

    public VideoClip videoHold;
    public VideoClip videoCountdown;
    public VideoClip thanksFor;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        IntroVideoPlayer.isLooping = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        DisableVR();
        Invoke("StartConnection", 1f);
    }

    public void PlayVideo ()
    {
        StartCoroutine(StartVideo());
    }

    private void StartConnection ()
    {
#if UNITY_EDITOR
        //NetManager.Instance.StartServer();
#else
// NetManager.Instance.StartClient();
#endif
    }

    IEnumerator LoadDevice(string newDevice, bool enable)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = enable;
    }

    public void EnableVR()
    {
        StartCoroutine(LoadDevice("Cardboard", true));
    }
    public void DisableVR()
    {
        StartCoroutine(LoadDevice("", false));
    }

    IEnumerator StartVideo ()
    {
        IntroVideoPlayer.Stop();
        IntroVideoPlayer.isLooping = false;
        IntroVideoPlayer.clip = videoCountdown;
        yield return new WaitForEndOfFrame();
        IntroVideoPlayer.Play();
        yield return new WaitForSeconds((float)videoCountdown.length);
        IntroVideoPlayer.Stop();
        IntroVideoPlayer.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        videoPlayer.Play();
        yield return new WaitForSeconds((float)videoPlayer.clip.length + 1.0f);
        videoPlayer.gameObject.SetActive(false);
        IntroVideoPlayer.isLooping = true;
        IntroVideoPlayer.clip = thanksFor;
        IntroVideoPlayer.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        IntroVideoPlayer.Play();

    }
}


