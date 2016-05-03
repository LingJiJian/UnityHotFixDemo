using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_ProtoTest : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetProtoBytes_s(IntPtr l) {
		try {
			var ret=ProtoTest.GetProtoBytes();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetProtoBytes_s(IntPtr l) {
		try {
			SLua.ByteArray a1;
			checkType(l,1,out a1);
			ProtoTest.SetProtoBytes(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetProtoTxt_s(IntPtr l) {
		try {
			var ret=ProtoTest.GetProtoTxt();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"ProtoTest");
		addMember(l,GetProtoBytes_s);
		addMember(l,SetProtoBytes_s);
		addMember(l,GetProtoTxt_s);
		createTypeMetatable(l,null, typeof(ProtoTest),typeof(UnityEngine.MonoBehaviour));
	}
}
