using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigReferences : MonoBehaviour
{
    public static VRRigReferences instance;

    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private void Awake()
    {
        instance = this;
    }
}
