
local TestCube = LDeclare("TestCube", LClass("TestCube"))

-- Awake method.
function TestCube:Awake()
    print("TestCube:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        print("Init error in TestCube!")
        return
    end

    self.m_strName = self.gameObject.name;

    print("My name is: " .. self.m_strName)
end

-- Start method.
function TestCube:Start()
    print("TestCube:Start")
end

-- Update method.
function TestCube:Update()
    print("TestCube:Update")
end

-- On destroy method.
function TestCube:OnDestroy()
    print("TestCube:OnDestroy")
end


-- Return this class.
return TestCube