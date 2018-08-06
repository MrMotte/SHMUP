using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform targetTransform;
    public float YMax;
    public float YMin;
    Vector3 tempVec3 = new Vector3();
    public float CamSpeed = 0.125f;

    void LateUpdate()
    {
        if (targetTransform)
        {
            tempVec3.x = this.transform.position.x;
            tempVec3.y = Mathf.Clamp(targetTransform.position.y, YMin, YMax);
            tempVec3.z = this.transform.position.z;

            this.transform.position = Vector3.Lerp(this.transform.position, tempVec3, (CamSpeed * Time.deltaTime));
        }
    }

}
