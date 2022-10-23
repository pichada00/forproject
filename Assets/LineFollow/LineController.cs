using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [Header("Line Animation params")]
    [SerializeField] private LineRenderer castLine = null; // Actual line renderer
    [Space(10)]
    [SerializeField] private Gradient activeColorWay = null;
    [SerializeField] private float activeWidth = 0.1f;
    [Space(10)]
    [SerializeField] private float curveActiveFollowSpeed = 20;
    [SerializeField] private float curveInActiveFollowSpeed = 100;
    [SerializeField] private float curveHitPointOffset = 0f;
    [SerializeField] private Transform[] curvePoints = null;
    [SerializeField] private int numberOfPointsOnCurve = 25;
    private BezierCurve _curveGenerator = null;
    private Vector3 _curvePointPosition;
    private bool _curveLocked = false;

    private bool _check = false;

    private Target _curNode = null;
    private Target curSeenNode = null;

    public bool openLine = false;

    /// Initialized Bezier Curve, and castline params
    private void Start()
    {
        _curveGenerator = new BezierCurve(numberOfPointsOnCurve);
        castLine.positionCount = numberOfPointsOnCurve;
        _curveLocked = false;

        ActivateCheck(true);
    }

    private void Awake()
    {
        curSeenNode = GameObject.Find("AI").GetComponentInParent<Target>();
    }

    void Update()
    {
        if (_check)
        {
            CheckNode();

            Vector3[] newPositions = _curveGenerator.GetQuadraticCurvePoints(curvePoints[0].position, curvePoints[1].position, curvePoints[2].position);
            castLine.SetPositions(newPositions);

            if (!_curveLocked)
            {
                curvePoints[2].position = Vector3.Lerp(curvePoints[2].position, curvePoints[1].position, curveInActiveFollowSpeed * Time.deltaTime);
            }
        }
        if(openLine == true)
        {
            _curvePointPosition = curSeenNode.transform.position * curveHitPointOffset;
        }
        
        if (_curNode != null)
        {
            ActivateLine();
        }
    }
    
    /// Raycast out looking for a TPNode GameObject
    private void CheckNode()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            openLine = true;
            if (curSeenNode != null)
            {
                Debug.Log("activate");
                if (_curNode == null && !curSeenNode.isHovered)
                {
                    _curNode = curSeenNode;
                    _curNode.OnHover();
                }

                //_curvePointPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z) + (hit.normal * curveHitPointOffset);
            }
            else
            {
                Debug.Log("Reactivate 1 ");
                _curveLocked = false;

                if (_curNode != null)
                {
                    _curNode.OnHoverLost();
                    _curNode = null;
                }

                DeActivateLine();
            }
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Reactivate 2 ");
            _curveLocked = false;

            if (_curNode != null)
            {
                _curNode.OnHoverLost();
                _curNode = null;
            }
                
            DeActivateLine();
        }
    }
    
    /// Helper method to enable the curve
    public void ActivateCheck(bool t)
    {
        _check = t;

        if (_check)
        {
            curvePoints[2].position = curvePoints[1].position;

            Vector3[] newPositions = _curveGenerator.GetQuadraticCurvePoints(curvePoints[0].position, curvePoints[1].position, curvePoints[2].position);
            castLine.SetPositions(newPositions);

            castLine.enabled = true;
        }
    }
    
    /// Throw line at the TPNode
    private void ActivateLine()
    {
        if (castLine.colorGradient != activeColorWay)
            castLine.colorGradient = activeColorWay;

        castLine.startWidth = activeWidth;
        castLine.endWidth = activeWidth;

        curvePoints[1].position = Vector3.Lerp(curvePoints[1].position, _curvePointPosition, curveActiveFollowSpeed * Time.deltaTime);

        if (Vector3.Distance(curvePoints[1].position, _curvePointPosition) < 0.15f)
            _curveLocked = true;

        if (_curveLocked)
            curvePoints[2].position = Vector3.Lerp(curvePoints[2].position, _curNode.lockPoint.position, curveActiveFollowSpeed * Time.deltaTime);
    }
    
    /// Pull line back
    private void DeActivateLine()
    {
        castLine.enabled = false;

        curvePoints[1].localPosition = curvePoints[0].localPosition + new Vector3(0, 0, 0);

        Vector3[] newPositions = _curveGenerator.GetQuadraticCurvePoints(curvePoints[0].position, curvePoints[1].position, curvePoints[2].position);
        castLine.SetPositions(newPositions);

        castLine.enabled = true;
    }
}
