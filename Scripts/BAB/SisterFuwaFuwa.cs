using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterFuwaFuwa : MonoBehaviour
{
    [Header("FuwaFuwa")]
    public float fuwaSpeed = 1.0f;
    public float upperLimitMax = 3f; // 移動の上限値
    public float upperLimitMin = 1.0f;
    public float damping = 5f; // 移動の減衰率
    [SerializeField] private bool isFuwaFuwa = true;
    private float initializeValue = 0f; // 初期化値
    private float permissibleDistance = 0.5f; // 初期化実行時の許容範囲

    // Start is called before the first frame update
    private void Start()
    {
        initializeValue = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isFuwaFuwa)
        {
            FuwaFuwa();
        }
        else
        {
            var offset = Mathf.Abs(initializeValue - transform.position.y);
            if (offset < permissibleDistance)
                return;

            InitializeFuwaFuwa();
        }
    }

    private void FuwaFuwa()
    {
        if (transform.position.y < upperLimitMax)
        {

        }
        else
        {

        }
            
    }

    private void Down()
    {
        transform.Translate(Vector3.down * fuwaSpeed * Time.deltaTime);
    }

    private void Up()
    {
        transform.Translate(Vector3.up * fuwaSpeed * Time.deltaTime);
    }

    private void InitializeFuwaFuwa()
    {
        var targetPos = new Vector3(
            transform.position.x,
            initializeValue,
            transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * damping);
    }
}
