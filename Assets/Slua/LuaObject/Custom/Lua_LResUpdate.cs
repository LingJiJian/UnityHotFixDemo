using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_LResUpdate : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int checkUpdate(IntPtr l) {
		try {
			LResUpdate self=(LResUpdate)checkSelf(l);
			self.checkUpdate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_VERSION_FILE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,LResUpdate.VERSION_FILE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_LOCAL_RES_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,LResUpdate.LOCAL_RES_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_LOCAL_RES_PATH(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,LResUpdate.LOCAL_RES_PATH);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_onCompleteHandler(IntPtr l) {
		try {
			LResUpdate self=(LResUpdate)checkSelf(l);
			UnityEngine.Events.UnityAction v;
			int op=LuaDelegation.checkDelegate(l,2,out v);
			if(op==0) self.onCompleteHandler=v;
			else if(op==1) self.onCompleteHandler+=v;
			else if(op==2) self.onCompleteHandler-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"LResUpdate");
		addMember(l,checkUpdate);
		addMember(l,"VERSION_FILE",get_VERSION_FILE,null,false);
		addMember(l,"LOCAL_RES_URL",get_LOCAL_RES_URL,null,false);
		addMember(l,"LOCAL_RES_PATH",get_LOCAL_RES_PATH,null,false);
		addMember(l,"onCompleteHandler",null,set_onCompleteHandler,true);
		createTypeMetatable(l,null, typeof(LResUpdate),typeof(UnityEngine.MonoBehaviour));
	}
}
