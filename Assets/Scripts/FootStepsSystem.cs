using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsSystem : MonoBehaviour
{
    [SerializeField] AudioClip Concrete;
    [SerializeField] AudioSource audioSource;
    
    RaycastHit hit;
    [SerializeField] float RaycastRange;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform RaycastStarting;
    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FootStep();
    }

    public void FootStep()
    {
        if(Physics.Raycast(RaycastStarting.position, RaycastStarting.up * -1, out hit, RaycastRange, layerMask))
        {
            Debug.Log("Raycast Occur");
            if(hit.collider.tag.Equals("Concrete"))
            {
                PlayFootStepSound(Concrete);
            }
        }
    }
    
    public void PlayFootStepSound(AudioClip clip)
    {
        audioSource.pitch = Random.Range(0.08f, 1f);
        audioSource.PlayOneShot(clip);
    }
}
