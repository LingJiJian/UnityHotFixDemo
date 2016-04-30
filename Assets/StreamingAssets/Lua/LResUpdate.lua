
local LResUpdate = LDeclare("LResUpdate", LClass("LResUpdate"))

-- Awake method.
function LResUpdate:Awake()
    print("LResUpdate:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        print("Init error in LResUpdate!")
        return
    end
end

-- Start method.
function LResUpdate:Start()
    print("LResUpdate:Start")

    self.gameObject.getComponent('Button').onClick.addListener(function()

    	self.checkUpdate();
    end)
end

-- Update method
function LResUpdate:Update()
    print("LResUpdate:Update")
end

-- On destroy method.
function LResUpdate:OnDestroy()
    print("LResUpdate:OnDestroy")
end


return LResUpdate