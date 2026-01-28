using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] Playercontroller playercontroller;
    [SerializeField] GameObject GameStartMenu;
    [SerializeField] GameObject GameRestartMenu;
    [SerializeField] TextMeshProUGUI endScore;


    private void Start()
    {
        GameStartMenu.SetActive(true);
        GameRestartMenu.SetActive(false);
    }

    private void Update()
    {
        if(playercontroller.isDead)
        {
            GameRestartMenu.SetActive(true);
            endScore.text = "Score: " + playercontroller.score;

        }
    }

    public void StartGame()
    {
        playercontroller.isStart = true;
        playercontroller.anim.SetBool("Run", true);
        GameStartMenu.SetActive(false);
        playercontroller.enabled = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
