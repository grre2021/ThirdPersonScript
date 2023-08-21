using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class EnemyStateMachine : StateMachine
{
    [SerializeField] private EnemyBaseState[] enemyBaseStates;

    [Header("Component")]
    public Transform Tr;

    public Rigidbody enemyRigidbody;

    public Animator animator;
    public Transform[] partolPoints;
    public NavMeshAgent navMeshAgent;
    public Transform player;

    public RootMotion rootMotion;

    public Detection detection;

    public Transform test_transform;

    private AudioSource audioSource;

    [Header("Data Value")] 
    [SerializeField]private float Hp;
    [SerializeField] private float fieldOfViewDistance;
    [SerializeField] private float fieldOfViewAngle;
    [SerializeField] private AudioClip walkClip;

    [Header("Debug")] public bool is_standby;

    [HideInInspector] public bool isSuperArmor = false;


    [HideInInspector]
    public Dictionary<int, State> attacktypes = new Dictionary<int, State>();

    [HideInInspector]
    public float standByTime;

    [HideInInspector]
    public bool is_targeting;

    [HideInInspector]
    public float attackRange;

    [HideInInspector]
    public UnityEvent<float, Vector3, float> hurtingEvent;

    [HideInInspector]
    public Vector3 hurtDirection;

    [HideInInspector]
    public float force;
    private bool isDetection = false;

    [HideInInspector]
    public Attack currentAttack;

    private float currentHp;




    private void Awake()
    {
        states = new Dictionary<System.Type, State>(enemyBaseStates.Length);
        foreach (var enemyBaseState in enemyBaseStates)
        {
            EnemyBaseState var = new EnemyBaseState();
            var = enemyBaseState;
            states.Add(enemyBaseState.GetType(), var);
            var.Initialize(this);
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        Tr = transform;
        animator = GetComponent<Animator>();
        attacktypes.Add(0, states[typeof(EnemyAttack_light)]);
        attacktypes.Add(1, states[typeof(EnemyAttack_heavy)]);

        rootMotion = GetComponent<RootMotion>();
        detection = GetComponent<Detection>();
        enemyRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        is_targeting = false;
        currentHp = Hp;
    }
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        SwitchState(states[typeof(EnemyPartolState)]);

    }


    public bool isFieldOfView(Transform target)
    {
        float distance = Vector3.Distance(Tr.position, target.position);
        float angle = Vector3.Angle(Tr.forward, target.position - Tr.position);
        if (distance < fieldOfViewDistance && angle < fieldOfViewAngle)
        {
            return true;
        }


        return false;



    }
    private void OnDrawGizmos()
    {
        if(Tr==null||player==null) return;
        Gizmos.DrawLine(Tr.position, player.position);
    }
      /*
        public void HandleDetection()
        {
            foreach (var hit in detection.GetDectection())
            {
                hit.GetComponent<Target>().GetDamage(currentAttack.damage, transform.position, currentAttack.Force,currentAttack.distanceAttacked);
            }

        }
        */


    public void Hurt(float damage, Vector3 direction, float force, float distanceAttacked)
    {
        if (force != 0)
        {
            Vector3 position = transform.position + direction * distanceAttacked;
            test_transform.position = new Vector3(position.x, transform.position.y, position.z);
        }
        this.force = force;
        currentHp -= damage;
        MeunController.Instance.SycEnemyBlood(Hp,currentHp);
        if (currentHp <= 0)
        {
            ChangeState(states[typeof(EnemyDieState)]);
        }
        if (!isSuperArmor)
        {
            ChangeState(states[typeof(EnemyHurtingState)]);
            StopAllCoroutines();
            isDetection = false;
        }


    }

    public void AttackDectionEvent()
    {
        //Debug.Log("AttackDectionEvent");
        if (!isDetection)
        {
            isDetection = true;
            StartCoroutine(nameof(IEDetection));
        }
        else
        {

            detection.ClearWasHit();

            StopCoroutine(nameof(IEDetection));
            isSuperArmor = false;
            isDetection = false;
          
        }

    }

    IEnumerator IEDetection()
    {
        isSuperArmor = true;
        while (true)
        {
            foreach (var hit in detection.GetDectection())
            {
                //Attack attack = parameter.attacks[parameter.playerData.currrntCombatIndex];
                Vector3 direction = hit.transform.position - transform.position;
                direction = Vector3.Normalize(direction);
                Debug.Log(currentAttack.animName);
                Debug.Log("currentattack force" +currentAttack.Force);
                Debug.Log("currentattack distance" +currentAttack.distanceAttacked);
                hit.GetComponent<PlayerHitBoxAgent>().GetDamage(currentAttack.damage, direction, currentAttack.Force, currentAttack.distanceAttacked);

                //Debug.Log("hit target");
            }
            yield return null;
        }


    }
    IEnumerator IESphereDetection(float radius)
    {
        isSuperArmor = true;
        while (true)
        {
            foreach (var hit in detection.SphereDetection(transform, radius))
            {
                Vector3 direction = hit.transform.position - transform.position;
                direction = Vector3.Normalize(direction);
                Debug.Log(currentAttack.animName);
                Debug.Log("currentattack force" +currentAttack.Force);
                Debug.Log("currentattack distance" +currentAttack.distanceAttacked);
                hit.GetComponent<PlayerHitBoxAgent>().GetDamage(currentAttack.damage, direction, currentAttack.Force, currentAttack.distanceAttacked);
                //Debug.Log("hit target");
            }
            yield return null;
        }


    }

    public void SphereDetectionEvent(float radius)
    {
        if (!isDetection)
        {
            isDetection = true;
            StartCoroutine(nameof(IESphereDetection), radius);
        }
        else
        {

            detection.ClearWasHit();

            StopCoroutine(nameof(IESphereDetection));
            isSuperArmor = false;
            isDetection = false;
        }

    }

    
    public void EventWalk()
    {
        
    }
    public void StopEventWalk()
    {
        
    }



}
