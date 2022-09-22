using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



    public class Interactor : MonoBehaviour
    {
        public static Interactor Instance;

        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private float _interactionPointRadius = 0.5f;
        [SerializeField] private LayerMask _interactableMask;
        [SerializeField] private LayerMask _somethingbigMask;
        public int _numFound;
        private readonly Collider[] _colliders = new Collider[3];

        public Transform PickUpPointR;
        public Transform PickUpPointL;
        public Transform hips;
        public bool handRight = false;
        public bool handLeft = false;

        private void Start()
        {
            
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
        }
    private void LateUpdate()
    {

        /*InteractL();
        InteractR();
        InteractBoth();*/
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _interactableMask);
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _somethingbigMask);
        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            switch (interactable.interactsomething)
            {
                case interactsomething.handright:
                    InteractR();
                    InteractL();
                    break;
                case interactsomething.handleft:
                    InteractL();
                    InteractR();
                    break;
                case interactsomething.handboth:
                    InteractBoth();
                    break;
            }
        }
    }

        private void InteractBoth()
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            {
                if (handRight == false && handLeft == false)
                {
                    Debug.Log("interactboth");
                    _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _somethingbigMask);
                    if (_numFound > 0)
                    {
                        var interactable = _colliders[0].GetComponent<IInteractable>();
                        if (interactable != null)
                        {
                            interactable.Interact(this);
                            handLeft = true;
                            handRight = true;
                            return;
                        }

                    }
                }
                else if (handRight == true && handLeft ==true)
                {
                    Debug.Log("full hand");
                    return;
                }



            }
            //ThirdPersonController.Instance._input.InteractR = false;
        }

        private void InteractR()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (handRight == false)
                {
                    Debug.Log("interact");
                    _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _interactableMask);
                    if (_numFound > 0)
                    {
                        var interactable = _colliders[0].GetComponent<IInteractable>();
                        if (interactable != null)
                        {
                            interactable.InteractR(this);
                            handRight = true;
                            return;
                        }

                    }
                }
                else if (handRight == true)
                {
                    Debug.Log("full hand");
                    return;
                }



            }
            //ThirdPersonController.Instance._input.InteractR = false;
        }
        private void InteractL()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (handLeft == false)
                {
                    Debug.Log("interact");
                    _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _interactableMask);
                    if (_numFound > 0)
                    {
                        var interactable = _colliders[0].GetComponent<IInteractable>();
                        if (interactable != null)
                        {
                            interactable.InteractL(this);
                            handLeft = true;
                            return;
                        }

                    }
                }
                else if (handLeft == true)
                {
                    Debug.Log("full hand");
                    return;
                }
            }
            //ThirdPersonController.Instance._input.InteractL = false;
        }
    }


