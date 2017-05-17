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
	JArray jar;
	System.Random rand = new System.Random();

	void Awake(){
		TextAsset ta = (TextAsset)Resources.Load(("documents"), typeof(TextAsset));
		//Debug.Log (ta == null);
		//string jsonfile = System.IO.File.ReadAllLines (@"../../../Assets/documents.json");
		JObject o = JObject.Parse(ta.text);
		jar = o ["list"] as JArray;
		//read json file
		//Debug.Log(o.GetValue("list")[0]);

		//GetTask ();
	}

	public bool Evaluate(int docId, StampVariation vari)
	{
		//Assumes there is exactly one entry with the given id
		JToken job = jar.First(e => e["id"].Value<int>() == docId);

		if (vari == StampVariation.Approved) {
			return job ["answer"].Value<bool>();
		} else {
			return !job ["answer"].Value<bool>();
		}
	}

	public string GetTask(out int id){
		int size = jar.Count;
		JToken task = jar [rand.Next (size)];

		string text = task ["text"].ToString();
		id = System.Convert.ToInt32(task ["id"].ToString());

		return text;
	}

	public string GetTaskOrder(int id){
		JToken task = jar [id];
		return task ["text"].ToString();
	}
}

