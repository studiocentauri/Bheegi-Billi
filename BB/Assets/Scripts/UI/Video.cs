using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    VideoPlayer vp;
    public GameObject text;

    public Animator transition;
    private void Awake()
    {
        vp = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Submit") != 0 && vp.frame < 1500)
        {
            vp.frame = 1500;
        }
        if (vp.frame > 1500)
        {
            text.SetActive(false);
        }
        
        if(vp.frame > 1620)
        {
            StartCoroutine(LoadLevel(1));
        }

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
}
