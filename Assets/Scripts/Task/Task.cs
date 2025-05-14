using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] public TaskParameter taskParameter; //Les param�tres statiques
    public float ActivationRadius; //la distance � laquelle le matelot doit �tre pour pouvoir r�aliser la t�che

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ActivationRadius);
    }

}
