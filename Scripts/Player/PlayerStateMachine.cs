using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Parameter
{
    [Header("Component")]
    public PlayerData playerData;
    public Animator animator;
    public CharacterController characterController;
    public Transform playerTr;
    public Transform mainCameraTr;
    public ForceReceiver forceReceiver;
    public Targeter targeter;

    public Transform pushedTr;

    public Attack[] attacks;

    public List<Detection> detections = new List<Detection>();
    public TrailRenderer trailRenderer;

    public ParticleSystem hitSystem;

    public AudioSource audioSource;

    public PlayerSensor playerSensor;

    [HideInInspector]
    public bool is_targeting;

    [HideInInspector]
    public float force;

    [HideInInspector] public PlayerSensor.NextPlayerMovement currentPlayerMovement;

}
public class PlayerStateMachine : StateMachine
{
    [SerializeField] private PlayerBaseState[] playerBaseStates;

    private InputReader inputReader;


    [SerializeField]
    public Parameter parameter;


    public bool isAllowChangeing;

    private Material material;

    private bool isDetection;



    private void Awake()
    {
        states = new Dictionary<Type, State>(playerBaseStates.Length);
        inputReader = GetComponent<InputReader>();

        foreach (var state in playerBaseStates)
        {
            state.Initialize(this, parameter, inputReader);
            states.Add(state.GetType(), state);

        }
        parameter.animator = GetComponent<Animator>();
        parameter.characterController = GetComponent<CharacterController>();
        parameter.playerTr = GetComponent<Transform>();
        parameter.mainCameraTr = Camera.main.transform;
        parameter.forceReceiver = GetComponent<ForceReceiver>();

        parameter.playerData.attackCombatIndex = parameter.attacks.Length;
        parameter.playerData.currrntCombatIndex = 0;

        parameter.is_targeting = false;
        isDetection = false;
        parameter.playerData.currentHp = parameter.playerData.Hp;
        parameter.audioSource = GetComponent<AudioSource>();
        parameter.playerSensor = GetComponent<PlayerSensor>();

    }
    private void Start()
    {
        SwitchState(states[typeof(PlayerFreeLookState)]);
    }
    protected override void Update()
    {
        base.Update();

        //if (isDetection)
        // HandleDetection();
    }

    public void Hurt(float damage, Vector3 direction, float force,float distanceAttack)
    {
        if (force != 0)
        {
            Vector3 position = transform.position + direction * distanceAttack;
            parameter.pushedTr.position = new Vector3(position.x, transform.position.y, position.z);
        }
        parameter.force = force;
        parameter.playerData.currentHp -= damage;
        MeunController.Instance.SycPlayerBlood(parameter.playerData.Hp,parameter.playerData.currentHp);
        
        ChangeState(states[typeof(PlayerHurtState)]);
    }

    /// <summary>
    /// 攻击检测
    /// </summary>
    /*
    void HandleDetection()
    {

        foreach (var item in parameter.detections)
        {
            foreach (var hit in item.GetDectection())
            {
                Vector3 direction = hit.transform.position - transform.position;
                direction = direction.normalized;
                hit.GetComponent<Target>().GetDamage(1, direction, 3);
            }
        }
    }
    */

    IEnumerator IEDetection()
    {
        while (true)
        {
            foreach (var item in parameter.detections)
            {
                foreach (var hit in item.GetDectection())
                {
                    Attack attack = parameter.attacks[parameter.playerData.currrntCombatIndex];
                    Vector3 direction = hit.transform.position - transform.position;
                    direction = Vector3.Normalize(direction);
                    hit.GetComponent<Target>().GetDamage(attack.damage, direction, attack.Force, attack.distanceAttacked);
                    
                    //parameter.hitSystem.Play();

                    Debug.Log("hit target");
                }
            }
            yield return null;
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
            foreach (var item in parameter.detections)
            {
                item.ClearWasHit();
            }
            StopCoroutine(nameof(IEDetection));
            isDetection = false;
        }

    }

    public void Climbing()
    {
        
        parameter.currentPlayerMovement = parameter.playerSensor.ClimbDetect(parameter.playerTr, SetDirection(), 1);
        Debug.Log(parameter.currentPlayerMovement);
        if (parameter.currentPlayerMovement !=
            PlayerSensor.NextPlayerMovement.jump)
        {
            ChangeState(states[typeof(PlayerClimbState)]);
        }
    }
    public Vector3 SetDirection()
    {
        Vector3 forward = parameter.mainCameraTr.forward;
        Vector3 right = parameter.mainCameraTr.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * inputReader.moveValue.y + right * inputReader.moveValue.x;
    }

    public void EventWalk()
    {
       
    }

    public void StopEventWalk()
    {
       
    }

    public void FootL()
    {

    }

    public void FootR()
    {

    }

  
}
