using BepInEx;
using System.Linq;

namespace DeathFixFix;

[BepInPlugin("com.github.dualiron.deathfixfix", nameof(DeathFixFix), "1.0.0")]
public sealed class Plugin : BaseUnityPlugin
{
    public void OnEnable()
    {
		On.RegionState.RainCycleTick += RegionState_RainCycleTick;
	}

	private void RegionState_RainCycleTick(On.RegionState.orig_RainCycleTick orig, RegionState self, int ticks, int foodRepBonus)
	{
		orig(self, ticks, foodRepBonus);

		if (ticks > 0) {
			for (int i = 0; i < self.world.NumberOfRooms; i++) {
				AbstractRoom abstractRoom = self.world.GetAbstractRoom(self.world.firstRoomIndex + i);

                foreach (var creatureInDen in abstractRoom.entitiesInDens.OfType<AbstractCreature>()) {
					creatureInDen.state.CycleTick();
                }
			}
		}
	}
}
