using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Matelot : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [SerializeField] MatelotParameter matelotParameter; 
    public float CurrentEnergy; //l'énergie restante au matelot
    public Timer CurrentTaskDuration { get; private set; } //la durée de la tâche actuelle 

    List<Task> tasks; //La liste des tâches du matelot

    public enum States //Les états possible d'un matelot
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
            //Le matelot est entrain de réaliser une tâche
            case States.DOINGTASK:
                State_DoingTask();
                break;
            //le matelot est fatigué
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
        else if (Vector3.Distance(transform.position,navMeshAgent.destination) < matelotParameter.WalkSpeed/2 ) //Condition pour faire bouger les matelots disponible aléatoirement
        {
           navMeshAgent.SetDestination(GetRandomPosition());
        }
    }

    /// <summary>
    /// Les conditions pour passer à l'état d'exécution de tâche
    /// </summary>
    void DoingTask_Transition()
    {
        Task CurrentTask = tasks[0]; //La tâche à réaliser est la première dans la liste
        Vector3 TaskDestination = CurrentTask.gameObject.transform.position; //On récupère la position de la tâche
        float DistanceFromDestination = Vector3.Distance(transform.position, TaskDestination);//La distance du joueur par rapport à la tâche
        if (DistanceFromDestination > CurrentTask.ActivationRadius) //Si la distance par rapport à la tâche est plus grande que la distance d'activation
        {
            navMeshAgent.SetDestination(TaskDestination); //on déplace le matelot vers la tâche
        }
        else //Sinon on initialise la durée de la tâche actuelle et on change d'état
        {
            CurrentTaskDuration = new Timer(CurrentTask.taskParameter.Duration);
            _MatelotStates = States.DOINGTASK;
        }
    }
    void State_DoingTask()
    {
        CurrentTaskDuration.Remove(Time.deltaTime * matelotParameter.Efficiency); //on fait avancer la progression de la tâche par rapport à l'efficacité du matelot
        if (CurrentTaskDuration.Done()) //Si la tâche est réalisée, on met à jour le taux de fatigue du matelot et on change d'état, si l'énergie du matelot est à 0 on passe à l'état fatigué 
        {
            CurrentEnergy -= tasks[0].taskParameter.EnergyRemoved;

            
            tasks.RemoveAt(0); //on enlève la tâche qui vient d'être réalisée
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
        CurrentEnergy += matelotParameter.EnergyRecoveryRate * Time.deltaTime; //le matelot récupère de l'énergie par rapport à sa vitesse de repos 
        if(CurrentEnergy >= matelotParameter.MaxEnergy) //Si il a récupéré toute son énergie le matelot est à nouveau disponible
        {
            CurrentEnergy = matelotParameter.MaxEnergy;
            _MatelotStates = States.AVAIABLE;
        }
    }

    /// <summary>
    /// Fontion pour ajouter une tâche au matelot
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
