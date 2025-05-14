using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TaskParameter : ScriptableObject
{
    public float Duration; //le temps que prends la tâche à être réalisé
    public float EnergyRemoved; //le taux de fatigue que la tâche ajoute au matelot
}
