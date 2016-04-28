
import "UnityEngine"
-- import "UnityEngine.SceneManagement"
require "LGlobal"
require "LVersion"
require "LClass"
require "TestCube"

local function main()
	-- 清理模块
	for modelName,flag in pairs(package.loaded) do
		if flag == true then
			package.loaded[modelName] = nil
		end
	end
end
-- Declare global function.
LDeclare("main", main)

return main
