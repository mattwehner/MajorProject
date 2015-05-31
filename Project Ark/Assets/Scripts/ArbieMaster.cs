﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ArbieMaster : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public static ArbieMaster arbieMaster;

        private WorldStorage _worldStorage;
        private ArbieController _arbieController;
        private GameObject _arbie;
        private NavMeshAgent _arbieAgent;
        private NavMeshPath _path;

        private bool _hasBeenThrown;
        void Awake()
        {
            arbieMaster = this;

        }
        void Start () {
            Debug.Log("CharacterMaster Is Alive");
            _worldStorage = WorldStorage.worldStorage;

            _arbie = PublicReferenceList.Character;
            _arbieController = ArbieController.arbieController;
            _arbieAgent = ArbieController.ArbieNavMeshAgent;

            _arbieAgent.enabled = false;
        }

        void Update()
        {
            if (_hasBeenThrown && (_arbie.GetComponent<Rigidbody>().velocity.magnitude <= Settings.Game.CharacterRecoverVelocity))
            {
                StartCoroutine(WaitFor(5));
                _hasBeenThrown = false;
                _arbieAgent.enabled = !_worldStorage.CompletedWayPoint;
                if (!_worldStorage.CompletedWayPoint)
                {
                    _arbieController.CreatePath(_path);
                }
                
            }
            ConditionsOnPosition();
        }

        private void ConditionsOnPosition()
        {
            if (WorldStorage.worldStorage.CompletedWayPoint) return;
            if (Vector3.Distance(_arbie.transform.position, _arbieAgent.destination) <=
                Settings.Game.DistanceRemainingToWayPoint)
            {
                StateInstructioner.stateInstructioner.ArbieExclamation("way point reached");
            }
        }

        internal void OnCharacterCollision(Collision collision)
        {
            if (collision.collider.tag == "Level") return;
            _hasBeenThrown = true;
            _arbieAgent.enabled = false;
        }

        internal void MoveCharacterToWayPoint(Vector3 destination)
        {
            _arbieAgent.enabled = true;
            _path = new NavMeshPath();
            _arbieAgent.CalculatePath(destination, _path);
            Debug.Log("path: " + _path.ToString());
            if (_path.status == NavMeshPathStatus.PathInvalid || _path.status == NavMeshPathStatus.PathPartial)
            {
                Debug.Log("Oops, Cannot Go There");
            }
            else
            {
                _arbieController.CreatePath(_path);
            }
        }

        internal void WayPointRequestCompleted()
        {
            _arbieController.ClearPath();
            _worldStorage.CompletedWayPoint = true;
        }

        IEnumerator WaitFor(float waitPeriod)
        {
            yield return new WaitForSeconds(waitPeriod);
        }
    }
}