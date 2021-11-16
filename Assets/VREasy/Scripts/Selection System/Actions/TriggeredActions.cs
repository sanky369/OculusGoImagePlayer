using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VREasy
{

    public class TriggeredActions : MonoBehaviour
    {

        // Public Trigger
        public VRGrabTrigger grabTrigger;

        // Public TriggerList
        public ActionList actionList
        {
            get
            {
                if (_actionList == null)
                {
                    _actionList = GetComponent<ActionList>();
                }
                if (_actionList == null)
                {
                    _actionList = gameObject.AddComponent<ActionList>();
                }
                return _actionList;
            }

        }
        private ActionList _actionList;

        private void Start()
        {
            grabTrigger = GetComponent<VRGrabTrigger>();
        }


        // Update is called once per frame
        void Update()
        {
            // Trigger Action List
            if (grabTrigger.Triggered())
            {
                actionList.Trigger();
            }


        }

    }

}
