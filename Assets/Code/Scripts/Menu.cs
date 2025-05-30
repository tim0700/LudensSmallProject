using UnityEngine;
using TMPro; // For TextMeshPro support


public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;


    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
    }

    //public void SetSeleted
    //{

    //}

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    /// <summary>
    /// 판매 모드를 토글하는 버튼 이벤트 (샵 메뉴에서 호출)
    /// </summary>
    public void OnSellButtonClicked()
    {
        BuildManager.main.ToggleSellMode();
        
        // 판매 모드 상태에 따른 UI 피드백 (옵션)
        if (BuildManager.main.IsSellMode())
        {
            Debug.Log("판매 모드 활성화! 타워를 클릭하여 판매하세요.");
        }
        else
        {
            Debug.Log("판매 모드 비활성화.");
        }
    }

}
