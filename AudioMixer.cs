using System;
using Lua;

// Token: 0x0200012D RID: 301
public enum AudioMixer
{
	// Token: 0x04000943 RID: 2371
	[Doc("Only affected by SFX volume, ignores time pitch.")]
	Master,
	// Token: 0x04000944 RID: 2372
	[Doc("Affected by SFX volume and time pitch.")]
	Ingame,
	// Token: 0x04000945 RID: 2373
	[Doc("Ingame mix child, Ducks World.")]
	Important,
	// Token: 0x04000946 RID: 2374
	[Doc("Ingame mix child, Ducks World and PlayerVehicle. Slightly boosted Bass.")]
	FirstPerson,
	// Token: 0x04000947 RID: 2375
	[Doc("Ingame mix child, Ducked by first person. Full volume when in enclosed seat.")]
	PlayerVehicle,
	// Token: 0x04000948 RID: 2376
	[Doc("Ingame mix child, Ducked by first person. Full volume when in enclosed seat. Zero volume when in third person.")]
	PlayerVehicleInterior,
	// Token: 0x04000949 RID: 2377
	[Doc("Ingame mix child, Ducked by first person. Reduced volume when in enclosed seat.")]
	World,
	// Token: 0x0400094A RID: 2378
	[Doc("Music mixer with separate volume, Ignores time pitch.")]
	Music,
	// Token: 0x0400094B RID: 2379
	[Doc("Music sting mixer with separate volume, Ignores time pitch.")]
	MusicSting
}
