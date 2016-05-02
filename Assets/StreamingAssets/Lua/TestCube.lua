
local TestCube = LDeclare("TestCube", LClass("TestCube"))

-- Awake method.
function TestCube:Awake()
    print("TestCube:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        print("Init error in TestCube!")
        return
    end

    local c=coroutine.create(function()
        local www = WWW(LResUpdate.LOCAL_RES_URL .. "/prefabbundles")
        Yield(www)
        local b = www.assetBundle;
        self.bundle = b;
        UnityEngine.Object.Instantiate(b:LoadAsset("Capsule",GameObject), 
            self.gameObject.transform.position, 
            self.gameObject.transform.rotation)

        www:Dispose()

        Yield(WaitForSeconds(2))

        UnityEngine.Object.Instantiate(self.bundle:LoadAsset("Capsule",GameObject), 
        Vector3(391.639,185.317,0), 
        self.gameObject.transform.rotation)
    end)
    coroutine.resume(c)
end

-- Start method.
function TestCube:Start()
    print("TestCube:Start")
end

-- Update method.
function TestCube:Update()
    -- print("TestCube:Update")
end

-- On destroy method.
function TestCube:OnDestroy()
    print("TestCube:OnDestroy")
end


-- Return this class.
return TestCube