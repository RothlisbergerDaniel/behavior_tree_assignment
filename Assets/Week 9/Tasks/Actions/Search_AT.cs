using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace NodeCanvas.Tasks.Actions {

	public class Search_AT : ActionTask {

		private float searchRadius; //current radius to search at
		private float searchSpeed; //speed at which to search radius increases
		private DrawSearchSphere dss;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
            searchSpeed = blackboard.GetVariableValue<float>("searchSpeed");
			dss = agent.GetComponent<DrawSearchSphere>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			searchRadius = 0;
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			searchRadius += Time.deltaTime * searchSpeed;
			dss.doSphereGizmo(searchRadius, true); //use this workaround because gizmos are weird with NodeCanvas
            Collider[] hits = Physics.OverlapSphere(agent.transform.position, searchRadius); //get collisions within the current search radius of the robot
            if (hits.Length > 1) // make sure the length is greater than 1 to make sure we don't count the robot itself
            {
                foreach (Collider col in hits) //iterate through the list since we don't know the order
                {
                    if (col.tag == "garbage") //if it's tagged as garbage, then garbage it must be!
					{
						blackboard.SetVariableValue("target", col.gameObject); //set target to the first piece of garbage the robot "sees"
						EndAction(true);									   //"target" variable has to be a graph variable because for some reason
																			   //NodeCanvas doesn't recognize a "robot" blackboard variable of the same name
					}
                }

            }
        }

		//Called when the task is disabled.
		protected override void OnStop() {
            dss.doSphereGizmo(0, false); //reset search gizmo
        }

		//Called when the task is paused.
		//protected override void OnPause() {
		//	
		//}

	}
}