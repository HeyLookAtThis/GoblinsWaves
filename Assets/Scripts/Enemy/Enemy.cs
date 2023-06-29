using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _damage;
    [SerializeField] private int _reward;
    [SerializeField] private Transform _spellTarget;
    [SerializeField] private AudioClip _dieAudio;
    [SerializeField] private AudioClip _attackAudio;

    private CapsuleCollider _capsule;
    private AudioSource _audioSource;

    public bool Attacked { get; private set; }

    public Player Target { get; private set; }

    private UnityAction _died;
    private UnityAction _attacking;

    private void Awake()
    {
        _audioSource= GetComponent<AudioSource>();
        _capsule = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        SetAttacked(false);
        _capsule.enabled = true;
    }    

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    public event UnityAction OnDied
    {
        add => _died += value;
        remove => _died -= value;
    }

    public event UnityAction OnAttacking
    {
        add => _attacking += value;
        remove => _attacking -= value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Attacked == false && Target.gameObject.activeSelf)
            Target.TryAttack(_spellTarget);
    }

    public void SetAttacked(bool wasAttacked)
    {
        Attacked = wasAttacked;
    }

    public void InitializeTarget(Player target)
    {
        Target = target;
    }

    public void TryAttack()
    {
        if (Target.Health > 0)
        {
            _attacking?.Invoke();
            Target.TakeDamage(_damage);
            _audioSource.PlayOneShot(_attackAudio);
        }
    }

    public void Die()
    {
        _died?.Invoke();
        _capsule.enabled = false;
        Target.AddReward(_reward);
        StartCoroutine(Deactivator());
        _audioSource.PlayOneShot(_dieAudio);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void LookAtTarget()
    {
        Vector3 targetDirection = Target.transform.position - transform.position;
        transform.forward = targetDirection;
    }

    private IEnumerator Deactivator()
    {
        float second = 0.5f;
        var waitTime = new WaitForSeconds(second);
        bool isSkeppedTime = false;

        while (isSkeppedTime == false)
        {
            isSkeppedTime = true;
            yield return waitTime;
        }

        if (isSkeppedTime)
        {
            gameObject.SetActive(false);
            yield break;
        }
    }
}
