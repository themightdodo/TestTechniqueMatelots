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
        canvas = GameManager.GM_Instance.canvas; //on r�cup�re le canvas avec le singleton
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
            TiredVFX.KillFX(); //Si le matelot n'est pas fatigu�, ne pas afficher l'ic�ne de fatigue
        }
        switch (matelot._MatelotStates)
        {

            //Le matelot est entrain de r�aliser une t�che
            case Matelot.States.DOINGTASK:
                State_DoingTask();
                break;
            //le matelot est fatigu�
            case Matelot.States.TIRED:
                State_Tired();
                break;
        }
    }
    void State_DoingTask()
    {
        ProgressBarVFX.InstanciateVFX(canvas);
        Slider Progress = ProgressBarVFX.CurrentVFX.GetComponent<Slider>(); //Progression et initialisation de la barre 
        Progress.transform.position = transform.position + Offset; //Position calqu� au matelot
        Progress.maxValue = matelot.CurrentTaskDuration.StartValue;
        Progress.value = matelot.CurrentTaskDuration.CurrentValue;
      

    }
    void State_Tired()
    {
        TiredVFX.InstanciateVFX(canvas); //si le matelot est fatigu� afficher l'ic�ne
        TiredVFX.CurrentVFX.transform.position = transform.position + Offset;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Offset,0.5f);
    }
}
