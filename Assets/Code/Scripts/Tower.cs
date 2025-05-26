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




}
