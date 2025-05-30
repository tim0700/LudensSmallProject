using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("Reaferences")]
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;
    private bool isSellMode = false;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
        isSellMode = false; // 타워 선택 시 판매 모드 해제
        Debug.Log("Selected Tower: " + towers[selectedTower].name);
    }

    /// <summary>
    /// 판매 모드 상태를 반환합니다
    /// </summary>
    /// <returns>현재 판매 모드 여부</returns>
    public bool IsSellMode()
    {
        return isSellMode;
    }

    /// <summary>
    /// 판매 모드를 설정합니다
    /// </summary>
    /// <param name="sellMode">판매 모드 활성화 여부</param>
    public void SetSellMode(bool sellMode)
    {
        isSellMode = sellMode;
        Debug.Log("Sell Mode: " + (sellMode ? "ON" : "OFF"));
    }

    /// <summary>
    /// 판매 모드를 토글합니다
    /// </summary>
    public void ToggleSellMode()
    {
        isSellMode = !isSellMode;
        Debug.Log("Sell Mode: " + (isSellMode ? "ON" : "OFF"));
    }

    /// <summary>
    /// 모든 타워 정보 배열을 반환합니다
    /// </summary>
    /// <returns>타워 배열</returns>
    public Tower[] GetAllTowers()
    {
        return towers;
    }

}
