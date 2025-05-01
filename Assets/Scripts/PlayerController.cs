using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool IsOn;
    private Animator m_animator;
    private AudioSource audioSource;
    [SerializeField] AudioSource FootStepSound;

    [SerializeField] Light TorchLight;
    [SerializeField] AudioClip TorchOnSound;
    private float Vertical, Horizontal;
    [SerializeField] float m_turnSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] KeyCode m_enterCarKey;

    // [SerializeField] KeyCode m_torchButton;
    // [SerializeField] List<Light> m_torchLight = new List<Light>();


    // [Range(0, 1f)] public float DistanceToGround;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            FootStepSound.enabled = true;
            // FootStepSound.pitch = UnityEngine.Random.Range(0.08f, 1f);
        }
        else
        {
            FootStepSound.enabled = false;
        }
        PlayerTorch();
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        m_animator.SetFloat("Vertical", Vertical, 0.1f, Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime * m_turnSpeed * Horizontal);
    }

    private void PlayerTorch()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IsOn = !IsOn;
            audioSource.PlayOneShot(TorchOnSound);
        }
        if (!IsOn)
        {
            TorchLight.enabled = false;
        }
        if (IsOn)
        {
            TorchLight.enabled = true;
        }
    }

    public void OnTriggerStay(Collider Other)
    {
        if (Other.gameObject.tag.Equals("Public"))
        {
            int CarId = Other.gameObject.GetComponent<CarIdentity>().m_carId;
            if (Input.GetKeyDown(m_enterCarKey))
            {
                Events.DoPersonDeActivate();
                Events.DoCarActivate(CarId);
            }
        }
    }

    // private void OnAnimatorIK(int layerIndex)
    // {
    //     if(m_animator)
    //     {
    //         m_animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
    //         m_animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
    //         m_animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
    //         m_animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

    //         RaycastHit hit;
    //         Ray ray = new Ray(m_animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
    //         if(Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
    //         {
    //             if(hit.transform.tag == "Walkable")
    //             {
    //                 Vector3 footPosition = hit.point;
    //                 footPosition.y += DistanceToGround;

    //                 m_animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
    //                 m_animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));

    //             }
    //         }

    //         ray = new Ray(m_animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
    //         if(Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
    //         {
    //             if(hit.transform.tag == "Walkable")
    //             {
    //                 Vector3 footPosition = hit.point;
    //                 footPosition.y += DistanceToGround;

    //                 m_animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
    //                 m_animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));

    //             }
    //         }
    //     }
    // }
}
