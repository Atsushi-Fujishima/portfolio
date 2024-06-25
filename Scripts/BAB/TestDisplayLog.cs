using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDisplayLog : MonoBehaviour
{
    [SerializeField] public GameObject testObject = null;
    [SerializeField] public Vector3 pos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayLog()
    {
        GameObject g = Instantiate(testObject, pos, Quaternion.identity);
        g.SetActive(true);
        Debug.Log("kanntannkayo www  gakusyuu azasu.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
