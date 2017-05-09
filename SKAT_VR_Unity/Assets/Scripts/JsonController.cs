using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class JsonController : MonoBehaviour
{
	JObject o;
	System.Random rand = new System.Random();

	void Awake(){
		TextAsset ta = (TextAsset)Resources.Load(("documents"), typeof(TextAsset));
		Debug.Log (ta == null);
		//string jsonfile = System.IO.File.ReadAllLines (@"../../../Assets/documents.json");
		o = JObject.Parse(ta.text);
		//read json file
		//Debug.Log(o.GetValue("list")[0]);

		//GetTask ();
	}

	public bool Evaluate(int docId, StampVariation vari){
		//TODO
		return true;
	}

	public string GetTask(out int id){
		JArray jar = o ["list"] as JArray;
		int size = jar.Count;
		JToken task = jar [rand.Next (size)];

		string text = task ["text"].ToString();
		id = System.Convert.ToInt32(task ["id"].ToString());

		return text;

		//Debug.Log (text);
		//Debug.Log (id);
		//TODO return it
	}
}

