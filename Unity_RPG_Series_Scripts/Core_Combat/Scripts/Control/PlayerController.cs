using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat()) { return; }; // so that we don't move while attack action
            if (InteractWithMovement()) { return; }
            print("Nothing to do.");
        }

        // Combat functionality implementation
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent(out CombatTarget target))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(target);
                    }
                    return true;
                }
            }

            return false;
        }

        // Movement functionality implementation
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool HasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (HasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}