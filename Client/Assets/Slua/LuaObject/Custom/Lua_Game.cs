using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_Game : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetLuaSvr_s(IntPtr l) {
		try {
			var ret=Game.GetLuaSvr();
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_onProgressHandler(IntPtr l) {
		try {
			Game self=(Game)checkSelf(l);
			UnityEngine.Events.UnityAction<System.Int32> v;
			int op=LuaDelegation.checkDelegate(l,2,out v);
			if(op==0) self.onProgressHandler=v;
			else if(op==1) self.onProgressHandler+=v;
			else if(op==2) self.onProgressHandler-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"Game");
		addMember(l,GetLuaSvr_s);
		addMember(l,"onProgressHandler",null,set_onProgressHandler,true);
		createTypeMetatable(l,null, typeof(Game),typeof(UnityEngine.MonoBehaviour));
	}
}
