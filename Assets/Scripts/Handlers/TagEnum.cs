using System;

public enum TagEnum {Hackble, Takeble, Player, Ground, Damageble, Room}
public enum GameStateEnum {Game, Pause}

public enum HasGunValueNameEnum { AttackRobotHasGun, AgilityRobotHasGun, TankRobotHasGun}
public enum GunTriggerNameEnum { AttackRobotGunTrigger, AgilityRobotGunTrigger, TankRobotGunTrigger}

public class TagManager
{

    public static string GetTagNameByEnum<T>(T value)
    {
        return Enum.GetName(typeof(T), value);
    }
}


