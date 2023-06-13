using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour
{
    private RectTransform rect;
    public RectTransform RotationCenter;
    public float AngularSpeed, RotationRadius;
    float posX, posY, angle = 0;

    public bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            move = true;
        }

        if(move)
        {
            posX = RotationCenter.position.x + Mathf.Cos(angle) * RotationRadius;
            posX = RotationCenter.position.y + Mathf.Sin(angle) * RotationRadius;
            rect.position = new Vector2(posX, posY);
            angle = angle + Time.deltaTime * AngularSpeed;

            if(angle>=360)
            {
                angle = 0;
            }
        }
    }

    private void FixedUpdate()
    {
    }


}