using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TaskParameter : ScriptableObject
{
    public float Duration; //le temps que prends la t�che � �tre r�alis�
    public float EnergyRemoved; //le taux de fatigue que la t�che ajoute au matelot
}
