using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class Endscene : MonoBehaviour
{
    // Start is called before the first frame update
    private ScoreData sd;
    public Timer timer;
    private readonly string key = "GameDevIITK";

    public Animator transition;
    void Awake()
    {
        string savePath = Path.Combine(Application.streamingAssetsPath, "scores" + MainMenuController.level);
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            json = EncryptDecrypt(json);
            sd = JsonUtility.FromJson<ScoreData>(json);
        }
        else sd = new ScoreData();
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        bool IsRegistered = false;
        string username = PlayerPrefs.GetString("username");
        for (int i = 0; i < sd.scores.Count; i++)
        {
            if (sd.scores[i].name == username)
            {
                if (timer.timer != 0 && timer.timer < sd.scores[i].score)
                {
                    sd.scores[i].score = timer.timer;

                }
                    IsRegistered = true;
            }
        }
        if(!IsRegistered)
        {
        AddScore(new Score(username, timer.timer));
        }
        SaveScore();
        if (MainMenuController.level == 1)
        {
            StartCoroutine(LoadLevel(7));
        }
        else if (MainMenuController.level == 2) { StartCoroutine(LoadLevel(8)); }
        else if (MainMenuController.level == 3) { StartCoroutine(LoadLevel(9)); }
        else
        {
            StartCoroutine(LoadLevel(12));
        }
    }

    public void AddScore(Score score)
    {
        if (score.score != 0)
            sd.scores.Add(score);
    }

    public void SaveScore()
    {
        string savePath = Path.Combine(Application.streamingAssetsPath, "scores" + MainMenuController.level);
        var json = JsonUtility.ToJson(sd);
        json = EncryptDecrypt(json);
        File.WriteAllText(savePath, json);
    }

    private string EncryptDecrypt(string data)
    {
        string ModifiedData = "";
        for(int i=0;i<data.Length; i++)
        {
            ModifiedData += (char)(data[i]^key[i%key.Length]) ;
        }
        return ModifiedData;
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log("done dan");
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(levelIndex);
    }
}
