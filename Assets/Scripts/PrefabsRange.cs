using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsRange : MonoBehaviour
{
    [SerializeField] List<GameObject> ChildGameObject;
    void Start()
    {
        // Transform[] PrefabObject = transform.GetComponentsInChildren<Transform>();
        ChildGameObject.Add(transform.GetChild(0).gameObject);
        ChildGameObject.Add(transform.GetChild(1).gameObject);
        foreach (GameObject Prefab in ChildGameObject)
        {

            Prefab.SetActive(false);
            Debug.Log("Object Disable");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            foreach (GameObject Object in ChildGameObject)
            {
                Object.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            foreach (GameObject Object in ChildGameObject)
            {
                Object.SetActive(false);
            }
        }
    }
}
