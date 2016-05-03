
import "UnityEngine"
import "UnityEngine.Object"
import "UnityEngine.SceneManagement"
require "LGlobal"
require "LVersion"
require "LClass"
require "TestCube"

local function main()
	-- -- 清理模块
	-- for modelName,flag in pairs(package.loaded) do
	-- 	if flag == true then
	-- 		package.loaded[modelName] = nil
	-- 	end
	-- end

	print("main start")

	local btn = GameObject.Find('btn_go');
	
	local resUpdate = btn:AddComponent("LResUpdate")
	resUpdate.onCompleteHandler = function()
		print("加载完成")

		local c=coroutine.create(function()
		    local www = WWW(LResUpdate.LOCAL_RES_URL .. "/scenebundles")
		    Yield(www)
		    local b = www.assetBundle;--不要注释这句!!!加载assetBundle
            SceneManager.LoadScene("luaScene");

            www:Dispose()
		end)
		coroutine.resume(c)
	end
	btn:GetComponent('Button').onClick:AddListener(function()
		resUpdate:checkUpdate()
	end)
end
-- Declare global function.
LDeclare("main", main)

return main
