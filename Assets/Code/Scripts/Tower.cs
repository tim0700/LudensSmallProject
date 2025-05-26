using System;
using UnityEngine;


[Serializable]
public class Tower
{
    public string name; // 타워 이름
    public int cost; // 타워 구매 비용
    public GameObject prefab; // 타워 프리팹

    public Tower (string _name, int _cost, GameObject _prefab)
    {
        this.name = _name;
        this.cost = _cost;
        this.prefab = _prefab;
    }




}
