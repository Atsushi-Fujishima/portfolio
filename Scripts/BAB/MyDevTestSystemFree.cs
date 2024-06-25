using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDevTestSystemFree : MonoBehaviour
{
    [SerializeField] GameObject origin;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestCol());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TestCol()
    {
        while (true) 
        {
            yield return new WaitForSeconds(3f);

            GameObject.Instantiate(origin, transform.position, Quaternion.identity);
            yield return null;
        }
        
    }
}
