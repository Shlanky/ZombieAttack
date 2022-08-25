using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPushback : MonoBehaviour
{
    [SerializeField] float PushStrength = 1;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        CharacterController othercontroller = other.gameObject.GetComponent<CharacterController>();
        if (othercontroller != null)
        {
            Vector3 pushVec = other.gameObject.transform.position - gameObject.transform.position + -other.gameObject.transform.forward;
            pushVec.Normalize();

            gameManager.instance.playerScript.pushback += pushVec * PushStrength;
            //othercontroller.Move(pushVec * PushStrength);


        }
    }

}
