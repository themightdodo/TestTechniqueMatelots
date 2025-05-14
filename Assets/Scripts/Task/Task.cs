using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] public TaskParameter taskParameter; //Les paramètres statiques
    public float ActivationRadius; //la distance à laquelle le matelot doit être pour pouvoir réaliser la tâche

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ActivationRadius);
    }

}
