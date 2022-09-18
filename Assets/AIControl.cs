using DiasGames.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour, ICapsule, IMover
{
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;
    private CharacterController _controller;
    private Vector3 _velocity;
    private float _terminalVelocity = 53.0f;
    [SerializeField] private bool UseGravity = true;
    [SerializeField] private float Gravity = -15.0f;
    private float _initialCapsuleHeight = 2f;
    private float _initialCapsuleRadius = 0.28f;

    private void Update()
    {
        GroundedCheck();
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, _controller.radius, GroundLayers, QueryTriggerInteraction.Ignore);

        if (!Grounded && !_controller.isGrounded) return;

        Depenetrate();
    }
    private void Depenetrate()
    {
        if (!_controller.enabled) return;

        // first check if there is a possible ground in all grounds
        RaycastHit[] hits = Physics.SphereCastAll(transform.position + Vector3.up, _controller.radius, Vector3.down,
            1 - GroundedOffset, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        foreach (RaycastHit h in hits)
        {
            if (h.distance != 0 && Vector3.Dot(h.normal, Vector3.up) > 0.7f)
                return;
        }

        // if not depenetrate char
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + Vector3.up, _controller.radius, Vector3.down,
            out hit, 1 - GroundedOffset, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (Vector3.Dot(hit.normal, Vector3.up) < 0.5f)
            {
                Grounded = false;
                Vector3 direction = hit.normal;
                direction.y = -1;
                _controller.Move(direction.normalized * _controller.skinWidth * 3);
            }
        }
    }
    public Collider GetGroundCollider()
    {
        if (!Grounded) return null;

        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Collider[] grounds = Physics.OverlapSphere(spherePosition, _controller.radius, GroundLayers, QueryTriggerInteraction.Ignore);

        if (grounds.Length > 0)
            return grounds[0];

        return null;
    }
    private void GravityControl()
    {
        if (_controller.isGrounded)
        {
            Debug.Log("con.gro");
            // stop our velocity dropping infinitely when grounded
            if (_velocity.y < 2.0f)
            {
                _velocity.y = -5f;
            }
        }
        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (UseGravity && _velocity.y < _terminalVelocity)
        {
            Debug.Log("usegra");
            Debug.Log(UseGravity);
            _velocity.y += Gravity * Time.deltaTime;
        }

    }

    public void SetCapsuleSize(float newHeight, float newRadius)
    {
        if (newRadius > newHeight * 0.5f)
            newRadius = newHeight * 0.5f;

        _controller.radius = newRadius;
        _controller.height = newHeight;
        _controller.center = new Vector3(0, newHeight * 0.5f, 0);
    }

    public void ResetCapsuleSize()
    {
        SetCapsuleSize(_initialCapsuleHeight, _initialCapsuleRadius);
    }

    public float GetCapsuleHeight()
    {
        return _controller.height;
    }

    public float GetCapsuleRadius()
    {
        return _controller.radius;
    }

    public void EnableCollision()
    {
        _controller.enabled = true;
    }

    public void DisableCollision()
    {
        _controller.enabled = false;
    }

    public void Move(Vector2 moveInput, float targetSpeed, bool rotateCharacter = true)
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector2 moveInput, float targetSpeed, Quaternion cameraRotation, bool rotateCharacter = true)
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 velocity)
    {
        throw new System.NotImplementedException();
    }

    public void StopMovement()
    {
        throw new System.NotImplementedException();
    }

    public void SetVelocity(Vector3 velocity)
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetVelocity()
    {
        throw new System.NotImplementedException();
    }

    public float GetGravity()
    {
        throw new System.NotImplementedException();
    }

    public void EnableGravity()
    {
        throw new System.NotImplementedException();
    }

    public void DisableGravity()
    {
        throw new System.NotImplementedException();
    }

    public void SetPosition(Vector3 newPosition)
    {
        bool currentEnable = _controller.enabled;

        _controller.enabled = false;
        transform.position = newPosition;
        _controller.enabled = currentEnable;
    }

    public Quaternion GetRotationFromDirection(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public bool IsGrounded()
    {
        return Grounded;
    }

    public void ApplyRootMotion(Vector3 multiplier, bool applyRotation = false)
    {
        throw new System.NotImplementedException();
    }

    public void StopRootMotion()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetRelativeInput(Vector2 input)
    {
        throw new System.NotImplementedException();
    }
}
