using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] Slider volume;
    bool start;
    private void Start()
    {
        start = true;
        volume.value = PlayerPrefs.GetFloat("Volume", 1); ;
        volume.maxValue = 1;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetFloat("Volume", volume.value);
        Debug.Log(volume.value);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void UpdateValue()
    {
        if(start)
        {
            start = false;
            Invoke("UpdateValue", 0.1f);
            return;
        }
        AudioController.instance.Setting(volume.value);
    }
}
