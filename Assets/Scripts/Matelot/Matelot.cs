using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Matelot : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] MatelotParameter matelotParameter; 
    public float CurrentEnergy; //l'�nergie restante au matelot
    public Timer CurrentTaskDuration { get; private set; } //la dur�e de la t�che actuelle 

    List<Task> tasks; //La liste des t�ches du matelot

    public enum States //Les �tats possible d'un matelot
    {
        AVAIABLE,
        DOINGTASK,
        TIRED
    }
    public States _MatelotStates { get; set; }

    private void Start()
    {
        CurrentEnergy = matelotParameter.MaxEnergy;
        tasks = new List<Task>();
        navMeshAgent = GetComponent<NavMeshAgent>(); //Initialisation du NavMeshAgent
        navMeshAgent.speed = matelotParameter.WalkSpeed;
        navMeshAgent.destination = GetRandomPosition();
    }

    void Update()
    {
        switch (_MatelotStates)
        {
            //Le matelot est disponible
            case States.AVAIABLE:
                State_Avaiable();
                break;
            //Le matelot est entrain de r�aliser une t�che
            case States.DOINGTASK:
                State_DoingTask();
                break;
            //le matelot est fatigu�
            case States.TIRED:
                State_Tired();
                break;
        }
    }

    void State_Avaiable()
    {
     
        if(tasks.Count != 0)
        {
            DoingTask_Transition();
        }
        else if (Vector3.Distance(transform.position,navMeshAgent.destination) < matelotParameter.WalkSpeed/2 ) //Condition pour faire bouger les matelots disponible al�atoirement
        {
           navMeshAgent.SetDestination(GetRandomPosition());
        }
    }

    /// <summary>
    /// Les conditions pour passer � l'�tat d'ex�cution de t�che
    /// </summary>
    void DoingTask_Transition()
    {
        Task CurrentTask = tasks[0]; //La t�che � r�aliser est la premi�re dans la liste
        Vector3 TaskDestination = CurrentTask.gameObject.transform.position; //On r�cup�re la position de la t�che
        float DistanceFromDestination = Vector3.Distance(transform.position, TaskDestination);//La distance du joueur par rapport � la t�che
        if (DistanceFromDestination > CurrentTask.ActivationRadius) //Si la distance par rapport � la t�che est plus grande que la distance d'activation
        {
            navMeshAgent.SetDestination(TaskDestination); //on d�place le matelot vers la t�che
        }
        else //Sinon on initialise la dur�e de la t�che actuelle et on change d'�tat
        {
            CurrentTaskDuration = new Timer(CurrentTask.taskParameter.Duration);
            _MatelotStates = States.DOINGTASK;
        }
    }
    void State_DoingTask()
    {
        CurrentTaskDuration.Remove(Time.deltaTime * matelotParameter.Efficiency); //on fait avancer la progression de la t�che par rapport � l'efficacit� du matelot
        if (CurrentTaskDuration.Done()) //Si la t�che est r�alis�e, on met � jour le taux de fatigue du matelot et on change d'�tat, si l'�nergie du matelot est � 0 on passe � l'�tat fatigu� 
        {
            CurrentEnergy -= tasks[0].taskParameter.EnergyRemoved;

            
            tasks.RemoveAt(0); //on enl�ve la t�che qui vient d'�tre r�alis�e
            if(CurrentEnergy < 0)
            {
                CurrentEnergy = 0;
                _MatelotStates = States.TIRED;
            }
            else
            {
                _MatelotStates = States.AVAIABLE;
            }
        }
     
    }

    void State_Tired()
    {
        CurrentEnergy += matelotParameter.EnergyRecoveryRate * Time.deltaTime; //le matelot r�cup�re de l'�nergie par rapport � sa vitesse de repos 
        if(CurrentEnergy >= matelotParameter.MaxEnergy) //Si il a r�cup�r� toute son �nergie le matelot est � nouveau disponible
        {
            CurrentEnergy = matelotParameter.MaxEnergy;
            _MatelotStates = States.AVAIABLE;
        }
    }

    /// <summary>
    /// Fontion pour ajouter une t�che au matelot
    /// </summary>
    public void AddTask(Task task)
    {
        if(task == null)
        {
            return;
        }
        if(!tasks.Contains(task))
        {
            tasks.Add(task);

        }
        
    }
    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * matelotParameter.WalkSpeed;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, matelotParameter.WalkSpeed, 1);
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }
}
