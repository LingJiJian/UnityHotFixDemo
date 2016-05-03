using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using SLua;
using System;


public class cjson_test : MonoBehaviour
{
	LuaSvr l;


	void Start()
	{
		l = new LuaSvr();
		l.init(null,()=>{l.start("cjsonTest");});
	}

	void Update()
	{

	}
}
