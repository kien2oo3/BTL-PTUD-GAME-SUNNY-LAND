using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject panelLevel;
    public Button btnLevel, btnStart, btn1,btn2,btn3,btn4,btn5;

    public AudioSource aus;
    public AudioClip clickMenu;

    void Start()
    {
        // Gán sự kiện onClick cho button
        btnLevel.onClick.AddListener(enablePanelLevel);
        btnStart.onClick.AddListener(startGame);
        btn1.onClick.AddListener(()=>changeLevel("level1"));
        btn2.onClick.AddListener(() => changeLevel("level2"));
        btn3.onClick.AddListener(() => changeLevel("level3"));
        btn4.onClick.AddListener(() => changeLevel("level4"));
        btn5.onClick.AddListener(() => changeLevel("level5"));
    }
    //Mở panel lựa chọn level
    void enablePanelLevel()
    {
        aus.PlayOneShot(clickMenu);
        // Khi sự kiện onClick() được kích hoạt, enable panel
        if (panelLevel != null)
        {
            panelLevel.SetActive(true);
        }
    }

    //Bắt đầu game với màn 1
    void startGame()
    {
        aus.PlayOneShot(clickMenu);
        StartCoroutine(LoadLevelDelayed("Level1"));
    }
    //Lựa chọn màn game
    void changeLevel(string level)
    {
        aus.PlayOneShot(clickMenu);
        StartCoroutine(LoadLevelDelayed(level));
    }

    IEnumerator LoadLevelDelayed(string level)
    {
        yield return new WaitForSeconds(0.2f); // Đợi 0.2 giây trước khi chuyển scene
        SceneManager.LoadScene(level);
    }
}
