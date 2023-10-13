using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;

    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {

        moveSpeed = 10 * 350;
        //DontDestroyOnLoad(this.gameObject); // ���� ������Ʈ �ı�����
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x + 2, target.transform.position.y + 3f, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}