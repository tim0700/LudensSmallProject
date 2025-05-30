using System;
using UnityEngine;


[Serializable]
public class Tower
{
    public string name; // Ÿ�� �̸�
    public int cost; // Ÿ�� ���� ���
    public GameObject prefab; // Ÿ�� ������

    public Tower (string _name, int _cost, GameObject _prefab)
    {
        this.name = _name;
        this.cost = _cost;
        this.prefab = _prefab;
    }

    /// <summary>
    /// 타워 판매시 환불받을 금액을 계산합니다 (원가의 80%)
    /// </summary>
    /// <returns>환불 금액</returns>
    public int GetSellPrice()
    {
        return Mathf.RoundToInt(cost * 0.8f);
    }
}
