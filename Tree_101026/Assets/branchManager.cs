using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class branchManager : MonoBehaviour
{
    [Header("Branch Global Setting")]
    public int maxBranchLayer;
    public int maxBranchBranching;
    public int curBranchLayer;

    [Header("Branch Specifics")]
    public float iniLength;
    public float iniBranchAngle;
    public float iniRadius;

    public float lengthDecay;
    public float angleDecay;
    public float radiusDecay;

    [Header("Misc")]
    public float branchLiftAngle;
    public GameObject linePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
