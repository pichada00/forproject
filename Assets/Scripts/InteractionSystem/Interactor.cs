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
        [SerializeField] private Collider box;
        [SerializeField] private LayerMask _interactableMask;
        [SerializeField] private LayerMask _somethingbigMask;
        [SerializeField] private LayerMask _weaponMask;
        [SerializeField] private LayerMask _AIMask;
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
        if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask) >= 1)
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _interactableMask);
        } else if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                                _somethingbigMask) >= 1)
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _somethingbigMask);
        } else if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               _AIMask) >= 1)
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius + 1.0f, _colliders,
                               _AIMask);
        }
        else if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                             _weaponMask) >= 1)
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius + 1.0f, _colliders,
                               _weaponMask);
        }

            if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            switch (interactable.interactsomething)
            {
                case interactsomething.handright:
                    TypeInteract(1);
                    TypeInteract(2);
                    break;
                case interactsomething.handleft:
                    TypeInteract(2);
                    TypeInteract(1);
                    break;
                case interactsomething.handboth:
                    InteractBoth();
                    break;
                case interactsomething.mouseright:
                    TypeInteract(2);
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

        private void TypeInteract(int i)
        {
        switch (i)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask) >= 1)
                    {
                        InteractR(_interactableMask);
                    }
                    if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,_AIMask) >= 1)
                    {
                        InteractR(_AIMask);
                    }

                    if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _weaponMask) >= 1)
                    {
                        InteractR(_weaponMask);
                    }
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask) >= 1)
                    {
                        InteractL(_interactableMask);
                    }
                    if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _weaponMask) >= 1)
                    {
                        InteractL(_weaponMask);
                    }
                }
                break;
        }
            
            //ThirdPersonController.Instance._input.InteractR = false;
        }
    private void InteractR(LayerMask mask)
    {
        if (handRight == false)
        {
            Debug.Log("interact");
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                       mask);
            if (_numFound > 0)
            {
                var interactable = _colliders[0].GetComponent<IInteractable>();
                if(mask == _weaponMask)
                {
                    box = _colliders[0].gameObject.transform.GetChild(0).GetComponent<BoxCollider>();
                }
                if (interactable != null)
                {                    
                    handRight = true;
                    interactable.InteractR(this);
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
        private void InteractL(LayerMask mask)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (handLeft == false)
                {
                    Debug.Log("interact");
                    _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                               mask);
                    if (_numFound > 0)
                    {
                        var interactable = _colliders[0].GetComponent<IInteractable>();

                    if (mask == _weaponMask)
                    {
                        box = _colliders[0].gameObject.transform.GetChild(0).GetComponent<BoxCollider>();
                    }
                    if (interactable != null)
                        {
                            
                            handLeft = true; 
                        interactable.InteractL(this);
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
    public void AttackStartAtInteract()
    {
        Debug.Log("start");
        box.enabled = true;
    }   
    public void AttackEndAtInteract()
    {
        Debug.Log("end");
        box.enabled = false;
    }    
}


