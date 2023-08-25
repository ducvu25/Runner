using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public bool isPlaying;
    float score;
    public Data data;

    [SerializeField] float time_spawn = 2f;

    public static GameController Instance;
    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent endGame = new UnityEvent();

    float volume;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        string s = LoadData.Load("save");
        if(s != null)
        {
            data = JsonUtility.FromJson<Data>(s);
        }
        else
        {
            data = new Data();
        }
        isPlaying = false;
        volume = PlayerPrefs.GetFloat("Volume", 1);
        Invoke("SetAudio", 0.3f);
    }
    void SetAudio()
    {
        if (AudioController.instance == null)
        {
            GetComponent<AudioController>().Setting(volume);
        }
        else
            AudioController.instance.Setting(volume);
    }
    // Update is called once per frame
    void Update()
    {
       if(isPlaying)
            score += Time.deltaTime;
    }
    public void NewGame()
    {
        onPlay.Invoke();
        score = 0;
        isPlaying = true;
    }
    public void EndGame()
    {
        AudioController.instance.PlaySound((int)AudioSetting.x);
        if (score > data.score)
        {
            data.score = score;
            LoadData.Save("save", JsonUtility.ToJson(data));
        }
        endGame.Invoke();
        isPlaying = false;
    }
    public int Score()
    {
        return Mathf.RoundToInt(score);
    }
    public float TimeSpawn()
    {
        return time_spawn;
    }
}
