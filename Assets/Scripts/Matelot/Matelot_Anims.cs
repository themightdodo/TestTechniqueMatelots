using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Matelot))]
public class Matelot_Anims : MonoBehaviour
{
    Matelot matelot;
    GameObject canvas;


    [Header("Visuels")]
    [SerializeField] GameObject ProgressBar;
    [SerializeField] GameObject Tired;
    VFX TiredVFX;
    VFX ProgressBarVFX;

    [Header("Position des visuels")]
    [SerializeField] Vector3 Offset;


    
    // Start is called before the first frame update
    void Start()
    {
        TiredVFX = new VFX(Tired);
        ProgressBarVFX = new VFX(ProgressBar);
        matelot = GetComponent<Matelot>();
        canvas = GameManager.GM_Instance.canvas; //on récupère le canvas avec le singleton
    }

    // Update is called once per frame
    void Update()
    {
        if(matelot._MatelotStates != Matelot.States.DOINGTASK)
        {
            ProgressBarVFX.KillFX(); //Si le matelot ne fait rien, ne pas afficher la barre de progression
        }
        if (matelot._MatelotStates != Matelot.States.TIRED)
        {
            TiredVFX.KillFX(); //Si le matelot n'est pas fatigué, ne pas afficher l'icône de fatigue
        }
        switch (matelot._MatelotStates)
        {

            //Le matelot est entrain de réaliser une tâche
            case Matelot.States.DOINGTASK:
                State_DoingTask();
                break;
            //le matelot est fatigué
            case Matelot.States.TIRED:
                State_Tired();
                break;
        }
    }
    void State_DoingTask()
    {
        ProgressBarVFX.InstanciateVFX(canvas);
        Slider Progress = ProgressBarVFX.CurrentVFX.GetComponent<Slider>(); //Progression et initialisation de la barre 
        Progress.transform.position = transform.position + Offset; //Position calqué au matelot
        Progress.maxValue = matelot.CurrentTaskDuration.StartValue;
        Progress.value = matelot.CurrentTaskDuration.CurrentValue;
      

    }
    void State_Tired()
    {
        TiredVFX.InstanciateVFX(canvas); //si le matelot est fatigué afficher l'icône
        TiredVFX.CurrentVFX.transform.position = transform.position + Offset;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Offset,0.5f);
    }
}
