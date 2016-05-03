using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using SLua;
using System;


public class SqliteTest : MonoBehaviour
{
	LuaSvr l;


	void Start()
	{
		l = new LuaSvr();
		l.init(null,()=>{l.start("sqliteTest");});
	}

	void Update()
	{

	}
}
