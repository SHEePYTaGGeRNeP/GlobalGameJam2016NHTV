using System.Collections.Generic;

using UnityEngine;
using System.Linq;

using Assets.Scripts.Helpers;
using Assets.Scripts.Units;

public class Boss : Unit
{
    [SerializeField]
    private AnimationClip _attackAnimation;
    private Animator _animator;
    private Transform[] _childTransforms;
    [SerializeField]
    private Vector3 _target;

    [SerializeField]
    private GameObject _targetGameObject;

    [SerializeField]
    private Transform _rightArm;
    [SerializeField]
    private Transform _rightShoulder;
    [SerializeField]
    private Transform _armGroundTarget;

    private float _armOffset;

    public float SearchEnemyAoE = 50f;
    public float Damping = 10f;

    public float MinWaitAfterAttack = 1.5f;
    public float ArmDownMinTime = 2f;
    public float ArmDownMaxTime = 6f;

    [SerializeField]
    private GameObject _dangerAreaProjectorPrefab;

    private bool _allowedToAttack = true;
    private bool _allowedToTurn = true;

    private List<SpawnedDangerArea> _spawnedDangerAreas = new List<SpawnedDangerArea>();
    private Transform parent;
    public QTE qte;

    private class SpawnedDangerArea
    {
        public Object GameObject { get; set; }
        public float SpawnedTime { get; set; }
        public float TimeToLive { get; set; }

        public SpawnedDangerArea(Object gameObject, float spawnedTime, float timeToLive)
        {
            this.GameObject = gameObject;
            this.SpawnedTime = spawnedTime;
            this.TimeToLive = timeToLive;
        }
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        parent = transform.root;
        this._animator = this.GetComponent<Animator>();
        this._childTransforms = parent.GetComponentsInChildren<Transform>();
        this._armOffset = this._rightArm.position.x;
        //this._rightArm.GetComponent<Collider>().enabled = false;
    }

    private void Smash()
    {
        Object go = Instantiate(this._dangerAreaProjectorPrefab, this._armGroundTarget.position, Quaternion.identity);
        GameObject gameO = (GameObject)go;
        gameO.transform.SetParent(parent);
        gameO.transform.eulerAngles = new Vector3(90, parent.eulerAngles.y + 90, 0);
        this._spawnedDangerAreas.Add(new SpawnedDangerArea(go, Time.timeSinceLevelLoad, this._attackAnimation.length));
        //this._rightArm.GetComponent<Collider>().enabled = true;
        this._allowedToTurn = false;
        this._allowedToAttack = false;
        this._animator.SetTrigger("Attacking");
        this.Invoke("AttackAnimationCompleted", this._attackAnimation.length + 0.01f);
    }

    private void AttackAnimationCompleted()
    {
        // TODO: DO QUICK TIME EVENT SHIT HERE maybe?

        this._rightArm.GetComponent<DamageGiver>().attacking = false;
        float moveToIdleTime = Random.Range(this.ArmDownMinTime, this.ArmDownMaxTime);
        LogHelper.Log(typeof(Boss), "Attacking: Going idle in " + moveToIdleTime);
        this.Invoke("MoveToIdle", moveToIdleTime);
    }

    private void MoveToIdle()
    {
        LogHelper.Log(typeof(Boss), "Moving to idle");
        this._animator.SetTrigger("MoveToIdle");
        this.Invoke("AllowAttack", this.MinWaitAfterAttack);
        this._allowedToTurn = true;
    }

    private void AllowAttack()
    {
        this._allowedToAttack = true;
        this._rightArm.GetComponent<DamageGiver>().attacking = true;
        LogHelper.Log(typeof(Boss), "Allowed to attack.");
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (this._allowedToAttack)
                this.Smash();
        }
    }
    // Update is called once per frame
    private new void Update()
    {           
        if (!_allowedToAttack)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Pull back"))
            {
                qte.Reset();
                print("Activate QTE");
            }
        }

        this.CleanSpawnedDangerAreas();
        if (this.feedbackRef != null)
            base.Update();
        if (!this._allowedToTurn)
            return;
        Collider[] colliders = Physics.OverlapSphere(parent.position, this.SearchEnemyAoE);
        this._target = Vector3.zero;
        foreach (Collider c in colliders)
            if (!this._childTransforms.Contains(c.transform) && c.transform.tag == "Player")
                if (this._target == Vector3.zero ||
                    Vector3.Distance(parent.position, c.transform.position) < Vector3.Distance(parent.position, this._target))
                {
                    this._target = c.transform.position;
                }

        if (this._target == Vector3.zero)
            return;
        this._targetGameObject.transform.position = this._target;
        Vector3 lookPos = this._target - this._rightShoulder.position;
        lookPos.y = 0;

        Quaternion rotation = Quaternion.LookRotation(-lookPos);
        parent.rotation = Quaternion.Slerp(parent.rotation, rotation, Time.deltaTime * this.Damping);
    }
    private void CleanSpawnedDangerAreas()
    {
        List<SpawnedDangerArea> removeDangerAreas = new List<SpawnedDangerArea>();
        foreach (SpawnedDangerArea sda in this._spawnedDangerAreas)
        {
            if (Time.timeSinceLevelLoad > sda.SpawnedTime + sda.TimeToLive)
            {
                Object.Destroy(sda.GameObject);
                removeDangerAreas.Add(sda);
            }
        }
        foreach (SpawnedDangerArea sda in removeDangerAreas)
            this._spawnedDangerAreas.Remove(sda);
    }

}
