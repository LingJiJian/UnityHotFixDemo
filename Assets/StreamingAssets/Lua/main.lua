
import "UnityEngine"
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

	if LGameConfig.GetInstance().isDebug then

		SceneManager.LoadScene("luaScene");
	else
		local resUpdate = btn:AddComponent("LResUpdate")
		resUpdate.onCompleteHandler = function()
			print("热更完成")

			local c=coroutine.create(function()
			    local www = WWW(LResUpdate.LOCAL_RES_URL .. "/".. "newScene.unity3d")
			    Yield(www)
			    local b = www.assetBundle;--不要注释这句!!!不然加载不了场景（坑到爆炸
	            SceneManager.LoadScene("myScene");
			end)
			coroutine.resume(c)
		end
		btn:GetComponent('Button').onClick:AddListener(function()
			resUpdate:checkUpdate()
		end)
	end

	

end
-- Declare global function.
LDeclare("main", main)

return main
