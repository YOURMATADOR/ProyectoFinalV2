using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoArma : MonoBehaviour {

	// Use this for initializationLight luzArma;
	   public int danio;                // The damage inflicted by each bullet.
        public float cadencia;       // The time between each shot.
        public float rango;                      // The distance the gun can fire.
		   AudioSource sonidoArma;
		public Light luzArma;
		public static movimientoArma instance;
		public string tipo;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	
	void Awake (){
	
		MakeSingleton();
		sonidoArma = GetComponent <AudioSource> ();
	}
	void MakeSingleton(){
		if (instance == null){
			instance = this;
		}
	}
public void dispara (){
	sonidoArma.Play();
}
	
	void Start () {
		Instantiate(luzArma);
		luzArma.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
				MakeSingleton();

		//  transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
	}
}
