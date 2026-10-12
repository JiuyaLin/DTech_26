using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class branchGrowth : MonoBehaviour
{
    public Vector3 beginBranchPos;
    public GameObject endBranchPt;
    public int maxBranchLayers; 
    LineRenderer lineObj;

    [Header("Growth")]
    public Vector2 growthDirection;
    public float growthRate;
    public float growthRateDecay;
    float growthRateThreshold = 0.1f;
    public bool isGrowing = true;

    [Header("Branching")]
    Vector3 lastBranchPos;
    int branchLayer;
    int branchDirection;
    public float distanceToLastBranch;
    public float branchDistanceDecay;
    public float lengthDecay;
    public float branchAngle;
    public float branchAngleDecay;
    public int branchCount;
    public float branchCountDecay;


    [Header("Prefabs")]
    public GameObject endBranchPtPrefab;

    [Header("Layer Color")]
    public List<Color> layerColors;

    // Start is called before the first frame update
    void Start()
    {
        endBranchPt = this.gameObject;
        lastBranchPos = beginBranchPos;
        lineObj = this.gameObject.transform.GetChild(0).GetComponent<LineRenderer>();
        lineObj.SetPosition(0, beginBranchPos);
        lineObj.startColor = layerColors[branchLayer % layerColors.Count];
        lineObj.endColor = layerColors[(branchLayer+1) % layerColors.Count];
        branchDirection = Random.Range(0, 2) * 2 - 1; 
    }

    // Update is called once per frame
    void Update()
    {
        //grow in the direction
        if(isGrowing)
        {
            endBranchPt.transform.position += (Vector3)growthDirection * growthRate * Time.deltaTime;

            lineObj.SetPosition(1, endBranchPt.transform.position);
        }
        growthRate *= 1 - growthRateDecay * Mathf.Pow(Time.deltaTime, 1.2f);
        if(growthRate < growthRateThreshold) isGrowing = false;



        //new branch
        if(Vector2.Distance(lastBranchPos, endBranchPt.transform.position) > distanceToLastBranch && branchLayer < maxBranchLayers)
        {
            branchCount++;
            newBranch(branchDirection);
            branchDirection *= -1;
            distanceToLastBranch *= branchDistanceDecay;
        }

    }


    void newBranch(int direction)
    {
        GameObject newBranch = Instantiate(endBranchPtPrefab, endBranchPt.transform.position, Quaternion.identity);
        branchGrowth newBranchScript = newBranch.GetComponent<branchGrowth>();
        newBranchScript.beginBranchPos = endBranchPt.transform.position;
        float thisBranchAngle = branchAngle * Mathf.Pow(branchAngleDecay, branchLayer) * Mathf.Pow(branchCountDecay, branchCount);
        Vector2 newDirection = Quaternion.Euler(0, 0, direction * thisBranchAngle) * growthDirection;
        newBranchScript.growthDirection = newDirection;
        newBranchScript.growthRate = growthRate * Mathf.Pow(growthRateDecay, branchLayer);
        newBranchScript.branchLayer = branchLayer + 1;
        newBranchScript.branchAngle *= branchAngleDecay;

        lastBranchPos = endBranchPt.transform.position;
    }



}
