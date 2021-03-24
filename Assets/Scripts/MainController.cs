using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour {


	private bool Button1Push = false;
	private bool Button3Push = false;

	private List<GameObject> Buildings;

	private GameObject Button1;
	private GameObject Button2;
	private GameObject Button3;

	private int TriggerTestTimes = 0;


	void Start(){


		Button1 = GameObject.Find ("Button1");
		Button2 = GameObject.Find ("Button2");
		Button3 = GameObject.Find ("Button3");
		Button3.GetComponent<Button> ().interactable = false;

		Buildings = new List<GameObject> ();

	}

	void Update () {


		if (Input.mousePosition.x > Screen.width - 15 && this.gameObject.transform.position.x < 300) {
			//print ("Right");
			this.gameObject.transform.Translate(0.5F,0,0);
		}
		if (Input.mousePosition.x < 15 && this.gameObject.transform.position.x > 200) {
			//print ("Left");
			this.gameObject.transform.Translate(-0.5F,0,0);
		}
		if (Input.mousePosition.y > Screen.height - 10 && this.gameObject.transform.position.z < 300) {
			//print ("Up");
			this.gameObject.transform.Translate(0,0,0.5F);
		}
		if (Input.mousePosition.y < 10 && this.gameObject.transform.position.z > 200) {
			//print ("Down");
			this.gameObject.transform.Translate(0,0,-0.5F);
		}



		if (Button1Push == true) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {					
				Buildings[Buildings.Count-1].transform.position = new Vector3 (hit.point.x, hit.point.y + 0.15F, hit.point.z);
				if (Input.GetMouseButtonDown (0)) {
					if(Buildings.Count > 1){
						for (int z = 0; z < Buildings.Count-1; z++) {
							if (TriggerBuildings (Buildings [Buildings.Count - 1], Buildings [z]) == false) {
								TriggerTestTimes++;
								if (TriggerTestTimes == Buildings.Count - 1) {
									Button1Push = false;
									Button1.GetComponent<Button> ().interactable = true;
									Button2.GetComponent<Button> ().interactable = true;
									TriggerTestTimes = 0;
									for (int i = 0; i < Buildings.Count; i++) {
										Buildings [i].layer = 0;
									}
								}
							}
						}
						TriggerTestTimes = 0;
					}
					if (Buildings.Count <= 1) {
						Button1Push = false;
						Button1.GetComponent<Button> ().interactable = true;
						Button2.GetComponent<Button> ().interactable = true;
						Button3.GetComponent<Button> ().interactable = true;
						for (int i = 0; i < Buildings.Count; i++) {
							Buildings [i].layer = 0;
						}
					}
				}
			}		
		}
		if (Button3Push == true && Input.GetMouseButton(0)) {	
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {

				if (hit.collider.tag == "Untagged") {
					
				}
				if (hit.collider.gameObject.tag == "Right") {
					Buildings [Buildings.Count - 1].transform.position = new Vector3 (hit.collider.transform.position.x+0.08f, 
																					  hit.collider.transform.position.y, 
																					  hit.collider.transform.position.z);
				}
				if (hit.collider.tag == "Left") {
					Buildings [Buildings.Count - 1].transform.position = new Vector3 (hit.collider.transform.position.x-0.08f, 
																					  hit.collider.transform.position.y, 
																					  hit.collider.transform.position.z);
				}
				if (hit.collider.tag == "Up") {
					Buildings [Buildings.Count - 1].transform.position = new Vector3 (hit.collider.transform.position.x, 
																					  hit.collider.transform.position.y, 
																					  hit.collider.transform.position.z+0.08f);
					Buildings [Buildings.Count - 1].transform.rotation = Quaternion.LookRotation (hit.collider.transform.up);
				}
				if (hit.collider.tag == "Down") {
					Buildings [Buildings.Count - 1].transform.position = new Vector3 (hit.collider.transform.position.x, 
																					  hit.collider.transform.position.y, 
																				   	  hit.collider.transform.position.z-0.08f);
					Buildings [Buildings.Count - 1].transform.rotation = Quaternion.LookRotation (hit.collider.transform.up*-1);
				}								
				//Buildings [Buildings.Count - 1].transform.rotation = Quaternion.LookRotation (hit.collider.transform.TransformDirection(hit.collider.transform.forward), hit.collider.transform.TransformDirection(hit.collider.transform.up));
				
			}
		}
	}

	public void button1(){

		Buildings.Add (Instantiate ((GameObject)Resources.Load("Objs/Corters",typeof(GameObject))) as GameObject);
		Button1Push = true;
		Button3Push = false;
		Button1.GetComponent<Button> ().interactable = false;
		Button2.GetComponent<Button> ().interactable = false;
		Button3.GetComponent<Button> ().interactable = false;
		for (int i = 0; i < Buildings.Count; i++) {
			Buildings [i].layer = 2;
		}

	}

	public void button2(){
		
		Buildings.Add (Instantiate ((GameObject)Resources.Load("Objs/WaterTreatment",typeof(GameObject))) as GameObject);
		Button1Push = true;
		Button3Push = false;
		Button1.GetComponent<Button> ().interactable = false;
		Button2.GetComponent<Button> ().interactable = false;
		Button3.GetComponent<Button> ().interactable = false;
		for (int i = 0; i < Buildings.Count; i++) {
			Buildings [i].layer = 2;
		}
	
	}

	public void button3(){
	
		if (Buildings.Count > 0) {
			Buildings.Add (Instantiate ((GameObject)Resources.Load("Objs/Tunnel",typeof(GameObject))) as GameObject);
			Buildings [Buildings.Count - 1].transform.position = new Vector3 (999f, 999f, 999f);
			Button3Push = true;
			Button1.GetComponent<Button> ().interactable = false;
			Button2.GetComponent<Button> ().interactable = false;
			Button3.GetComponent<Button> ().interactable = false;
			for (int i = 0; i < Buildings.Count; i++) {
				Buildings [i].layer = 2;
			}
		}	
	}

	public bool TriggerBuildings(GameObject Obj1, GameObject Obj2){

		//print ("CheckTrigger");

		if (Obj1.transform.position.x >= Obj2.transform.position.x) {
			if (Obj1.transform.position.x <= Obj2.transform.position.x + Obj2.transform.localScale.x) {
				if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 >= Obj2.transform.position.y) {
					if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 <= Obj2.transform.position.y + Obj2.transform.localScale.y) {
						if (Obj1.transform.position.z >= Obj2.transform.position.z) {
							if (Obj1.transform.position.z <= Obj2.transform.position.z + Obj2.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}
		if (Obj1.transform.position.x + Obj1.transform.localScale.x >= Obj2.transform.position.x) {
			if (Obj1.transform.position.x + Obj1.transform.localScale.x <= Obj2.transform.position.x + Obj2.transform.localScale.x) {
				if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 >= Obj2.transform.position.y) {
					if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 <= Obj2.transform.position.y + Obj2.transform.localScale.y) {
						if (Obj1.transform.position.z >= Obj2.transform.position.z) {
							if (Obj1.transform.position.z <= Obj2.transform.position.z + Obj2.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}
		if (Obj1.transform.position.x >= Obj2.transform.position.x) {
			if (Obj1.transform.position.x <= Obj2.transform.position.x + Obj2.transform.localScale.x) {
				if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 >= Obj2.transform.position.y) {
					if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 <= Obj2.transform.position.y + Obj2.transform.localScale.y) {
						if (Obj1.transform.position.z + Obj1.transform.localScale.z >= Obj2.transform.position.z) {
							if (Obj1.transform.position.z + Obj1.transform.localScale.z <= Obj2.transform.position.z + Obj2.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}
		if (Obj1.transform.position.x + Obj1.transform.localScale.x >= Obj2.transform.position.x) {
			if (Obj1.transform.position.x + Obj1.transform.localScale.x <= Obj2.transform.position.x + Obj2.transform.localScale.x) {
				if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 >= Obj2.transform.position.y) {
					if (Obj1.transform.position.y + Obj1.transform.localScale.y/2 <= Obj2.transform.position.y + Obj2.transform.localScale.y) {
						if (Obj1.transform.position.z + Obj1.transform.localScale.z >= Obj2.transform.position.z) {
							if (Obj1.transform.position.z + Obj1.transform.localScale.z <= Obj2.transform.position.z + Obj2.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}
		if (Obj2.transform.position.x >= Obj1.transform.position.x) {
			if (Obj2.transform.position.x <= Obj1.transform.position.x + Obj1.transform.localScale.x) {
				if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 >= Obj1.transform.position.y) {
					if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 <= Obj1.transform.position.y + Obj1.transform.localScale.y) {
						if (Obj2.transform.position.z >= Obj1.transform.position.z) {
							if (Obj2.transform.position.z <= Obj1.transform.position.z + Obj1.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}
		if (Obj2.transform.position.x + Obj2.transform.localScale.x >= Obj1.transform.position.x) {
			if (Obj2.transform.position.x + Obj2.transform.localScale.x <= Obj1.transform.position.x + Obj1.transform.localScale.x) {
				if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 >= Obj1.transform.position.y) {
					if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 <= Obj1.transform.position.y + Obj1.transform.localScale.y) {
						if (Obj2.transform.position.z >= Obj1.transform.position.z) {
							if (Obj2.transform.position.z <= Obj1.transform.position.z + Obj1.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}
		if (Obj2.transform.position.x >= Obj1.transform.position.x) {
			if (Obj2.transform.position.x <= Obj1.transform.position.x + Obj1.transform.localScale.x) {
				if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 >= Obj1.transform.position.y) {
					if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 <= Obj1.transform.position.y + Obj1.transform.localScale.y) {
						if (Obj2.transform.position.z + Obj2.transform.localScale.z >= Obj1.transform.position.z) {
							if (Obj2.transform.position.z + Obj2.transform.localScale.z <= Obj1.transform.position.z + Obj1.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}
		if (Obj2.transform.position.x + Obj2.transform.localScale.x >= Obj1.transform.position.x) {
			if (Obj2.transform.position.x + Obj2.transform.localScale.x <= Obj1.transform.position.x + Obj1.transform.localScale.x) {
				if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 >= Obj1.transform.position.y) {
					if (Obj2.transform.position.y + Obj2.transform.localScale.y/2 <= Obj1.transform.position.y + Obj1.transform.localScale.y) {
						if (Obj2.transform.position.z + Obj2.transform.localScale.z >= Obj1.transform.position.z) {
							if (Obj2.transform.position.z + Obj2.transform.localScale.z <= Obj1.transform.position.z + Obj1.transform.localScale.z) {
								return true;
							}
						}
					}
				}
			}
		}



		return false;
	}
}
