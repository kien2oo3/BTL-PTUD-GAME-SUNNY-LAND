using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    public GameObject panelPause;
    public Button btnHome, btnRestart, btnContinue, btnPause;

    public AudioSource aus;
    public AudioClip clickMenu;

    void Start()
    {
        // Gán sự kiện onClick cho button
        btnHome.onClick.AddListener(home);
        btnRestart.onClick.AddListener(restartGame);
        btnPause.onClick.AddListener(pauseGame);
        btnContinue.onClick.AddListener(continueGame);
    }
    //Tạm dừng game
    void pauseGame()
    {
        aus.PlayOneShot(clickMenu);
        enablePanelResume(true);
        Time.timeScale = 0.0f;
    }
    //Mở panel tạm dừng game
    void enablePanelResume(bool isEnable)
    {
        // Khi sự kiện onClick() được kích hoạt, enable panel
        if (panelPause != null)
        {
            panelPause.SetActive(isEnable);
        }
    }
    //Chơi lại game
    void restartGame()
    {
        aus.PlayOneShot(clickMenu);
        StartCoroutine(LoadSceneDelayed(SceneManager.GetActiveScene().name));
        Time.timeScale = 1.0f;
    }
    //Tiếp tục game
    void continueGame()
    {
        aus.PlayOneShot(clickMenu);
        enablePanelResume(false);
        Time.timeScale = 1.0f;
    }
    //Quay về màn hình chính
    void home()
    {
        aus.PlayOneShot(clickMenu);
        StartCoroutine(LoadSceneDelayed("Start"));
        Time.timeScale = 1.0f;
    }

    IEnumerator LoadSceneDelayed(string name)
    {
        yield return new WaitForSeconds(0.2f); // Đợi 0.2 giây trước khi chuyển scene
        SceneManager.LoadScene(name);
    }
}
