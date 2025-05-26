using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    // Main Menu 버튼 클릭시 - 메인 메뉴로
    public void MainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadMainMenu();
        }
    }

    // Exit 버튼 클릭시 - 게임 종료
    public void ExitGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
    }
}
