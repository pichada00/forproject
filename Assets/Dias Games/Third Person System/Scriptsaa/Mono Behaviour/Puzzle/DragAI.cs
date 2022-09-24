using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DiasGames.Puzzle
{
    public class DragAI : MonoBehaviour
    {
        private Rigidbody _rigidbody = null;
        //private AI_Buddy aiBuddy = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            //aiBuddy = GetComponent<AI_Buddy>();
        }

        public virtual bool Move(Vector3 velocity)
        {
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;

            return true;
        }

        public void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = Vector3.zero;
        }
        public void EnablePhysicsofAI()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
        }

        public virtual void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
        }
        public virtual void DisablePhysicsAI()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = true;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
