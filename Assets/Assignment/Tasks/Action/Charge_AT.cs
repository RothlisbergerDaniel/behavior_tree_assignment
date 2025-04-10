using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class Charge_AT : ActionTask {

		public GameObject aimPivot; //object pivot to scale charge indicator correctly
		public BBParameter<float> chargeDelay; //time between preparing to charge and actually charging
		public BBParameter<float> chargeDistance; //how far to charge
		public BBParameter<float> chargeSpeed; //how FAST to charge
        public BBParameter<NavMeshAgent> navmesh; //navmesh
		public BBParameter<GameObject> target; //player to aim at
		public BBParameter<float> chargeCooldown; //max cooldown before the enemy can charge again
		public BBParameter<float> chargeCooldownTimer; //timer to keep track of charge time
        private NavMeshAgent nma;		//
        private float cDelay;			//
		private float cDistance;		// LOCAL versions of the BBParameters for cleaner code
		private float cSpeed;			//
		private float chargeDelayTimer; //
		private float chargeTime; //how long to charge for.
		private MiscFunctionality vc;
		private bool buffed;
		private Rigidbody rb;
		private Vector3 aimPoint;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			cDelay = chargeDelay.value;
			cDistance = chargeDistance.value;
			cSpeed = chargeSpeed.value;
            nma = navmesh.value;
			vc = agent.GetComponent<MiscFunctionality>();
			rb = agent.GetComponent<Rigidbody>();
            aimPivot.SetActive(false);
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			chargeDelayTimer = 0;
			if (agent.GetComponent<ChargerBuffHandler>().buffTimer > 0 )
			{
				buffed = true;
                chargeTime = cDistance / (cSpeed * 2);
            } else
			{
				buffed = false;
                chargeTime = cDistance / cSpeed;
            }
			
			nma.SetDestination(agent.transform.position);
			aimPoint = target.value.transform.position;
            agent.transform.LookAt(aimPoint);
			aimPivot.SetActive(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			if (chargeDelayTimer >= cDelay ) //if charge windup is finished
			{
                aimPivot.SetActive(false); //disable aim reticle

				if (buffed)
				{
                    vc.velocity = agent.transform.forward * cSpeed * 2;
                } else
				{
                    vc.velocity = agent.transform.forward * cSpeed;
                }
                
				chargeTime -= Time.deltaTime;
				//rb.Move(rb.position + vc.velocity * Time.deltaTime, Quaternion.identity);
                nma.Move(vc.velocity * Time.deltaTime);
                nma.SetDestination(agent.transform.position);
                if (chargeTime <= 0 )
				{
                    vc.velocity = new Vector3();
					if (buffed)
					{
                        chargeCooldownTimer.SetValue(chargeCooldown.value / 2); //set cooldown to half of max value when buffed
                    }
                    else
                    {
                        chargeCooldownTimer.SetValue(chargeCooldown.value); //set cooldown to max value
                    }
                    //chargeCooldownTimer.SetValue(chargeCooldown.value); //set cooldown to max value
                    EndAction(true); //end this action
				}
			} else //if it's winding up
			{

                chargeDelayTimer += Time.deltaTime; //increase the windup timer
                aimPivot.transform.localScale = new Vector3(1, 1, (chargeDelayTimer / cDelay) * cDistance * 0.33f); //make reticle longer
                nma.SetDestination(agent.transform.position);
                agent.transform.LookAt(aimPoint);
				agent.transform.eulerAngles = new Vector3(0, agent.transform.eulerAngles.y, 0);

            }

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
    }
}