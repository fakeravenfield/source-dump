using System;
using System.Collections.Generic;
using Lua.Wrapper;
using MoonSharp.Interpreter;

namespace Lua.Proxy
{
	// Token: 0x020009B1 RID: 2481
	[Proxy(typeof(ArcadeCar))]
	public class ArcadeCarProxy : IProxy
	{
		// Token: 0x06004126 RID: 16678 RVA: 0x0002C605 File Offset: 0x0002A805
		[MoonSharpHidden]
		public ArcadeCarProxy(ArcadeCar value)
		{
			this._value = value;
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x0002C614 File Offset: 0x0002A814
		public ArcadeCarProxy()
		{
			this._value = new ArcadeCar();
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x0002C627 File Offset: 0x0002A827
		// (set) Token: 0x06004129 RID: 16681 RVA: 0x0002C634 File Offset: 0x0002A834
		public float acceleration
		{
			get
			{
				return this._value.acceleration;
			}
			set
			{
				this._value.acceleration = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x0002C642 File Offset: 0x0002A842
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x0002C64F File Offset: 0x0002A84F
		public float accelerationTipAmount
		{
			get
			{
				return this._value.accelerationTipAmount;
			}
			set
			{
				this._value.accelerationTipAmount = value;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x0002C65D File Offset: 0x0002A85D
		// (set) Token: 0x0600412D RID: 16685 RVA: 0x0002C66A File Offset: 0x0002A86A
		public float airAngularDrag
		{
			get
			{
				return this._value.airAngularDrag;
			}
			set
			{
				this._value.airAngularDrag = value;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x0002C678 File Offset: 0x0002A878
		// (set) Token: 0x0600412F RID: 16687 RVA: 0x0002C685 File Offset: 0x0002A885
		public float airDrag
		{
			get
			{
				return this._value.airDrag;
			}
			set
			{
				this._value.airDrag = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x0002C693 File Offset: 0x0002A893
		// (set) Token: 0x06004131 RID: 16689 RVA: 0x0002C6A0 File Offset: 0x0002A8A0
		public float baseTurnTorque
		{
			get
			{
				return this._value.baseTurnTorque;
			}
			set
			{
				this._value.baseTurnTorque = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x0002C6AE File Offset: 0x0002A8AE
		// (set) Token: 0x06004133 RID: 16691 RVA: 0x0002C6BB File Offset: 0x0002A8BB
		public float brakeAccelerationTriggerSpeed
		{
			get
			{
				return this._value.brakeAccelerationTriggerSpeed;
			}
			set
			{
				this._value.brakeAccelerationTriggerSpeed = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x0002C6C9 File Offset: 0x0002A8C9
		// (set) Token: 0x06004135 RID: 16693 RVA: 0x0002C6D6 File Offset: 0x0002A8D6
		public float brakeDrag
		{
			get
			{
				return this._value.brakeDrag;
			}
			set
			{
				this._value.brakeDrag = value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		// (set) Token: 0x06004137 RID: 16695 RVA: 0x0002C6F1 File Offset: 0x0002A8F1
		public float brakeDriftMinSpeed
		{
			get
			{
				return this._value.brakeDriftMinSpeed;
			}
			set
			{
				this._value.brakeDriftMinSpeed = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x0002C6FF File Offset: 0x0002A8FF
		// (set) Token: 0x06004139 RID: 16697 RVA: 0x0012FD7C File Offset: 0x0012DF7C
		public SoundBankProxy brakeSounds
		{
			get
			{
				return SoundBankProxy.New(this._value.brakeSounds);
			}
			set
			{
				SoundBank brakeSounds = null;
				if (value != null)
				{
					brakeSounds = value._value;
				}
				this._value.brakeSounds = brakeSounds;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x0002C711 File Offset: 0x0002A911
		// (set) Token: 0x0600413B RID: 16699 RVA: 0x0002C71E File Offset: 0x0002A91E
		public float downforcePerSpeed
		{
			get
			{
				return this._value.downforcePerSpeed;
			}
			set
			{
				this._value.downforcePerSpeed = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600413C RID: 16700 RVA: 0x0002C72C File Offset: 0x0002A92C
		// (set) Token: 0x0600413D RID: 16701 RVA: 0x0002C739 File Offset: 0x0002A939
		public bool driftByAcceleration
		{
			get
			{
				return this._value.driftByAcceleration;
			}
			set
			{
				this._value.driftByAcceleration = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x0002C747 File Offset: 0x0002A947
		// (set) Token: 0x0600413F RID: 16703 RVA: 0x0002C754 File Offset: 0x0002A954
		public bool driftByBrake
		{
			get
			{
				return this._value.driftByBrake;
			}
			set
			{
				this._value.driftByBrake = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x0002C762 File Offset: 0x0002A962
		// (set) Token: 0x06004141 RID: 16705 RVA: 0x0002C76F File Offset: 0x0002A96F
		public float driftDuration
		{
			get
			{
				return this._value.driftDuration;
			}
			set
			{
				this._value.driftDuration = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x0002C77D File Offset: 0x0002A97D
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x0002C78A File Offset: 0x0002A98A
		public float driftingSlip
		{
			get
			{
				return this._value.driftingSlip;
			}
			set
			{
				this._value.driftingSlip = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x0002C798 File Offset: 0x0002A998
		// (set) Token: 0x06004145 RID: 16709 RVA: 0x0002C7A5 File Offset: 0x0002A9A5
		public float extraStability
		{
			get
			{
				return this._value.extraStability;
			}
			set
			{
				this._value.extraStability = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x0002C7B3 File Offset: 0x0002A9B3
		// (set) Token: 0x06004147 RID: 16711 RVA: 0x0002C7C0 File Offset: 0x0002A9C0
		public float frictionTipAmount
		{
			get
			{
				return this._value.frictionTipAmount;
			}
			set
			{
				this._value.frictionTipAmount = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x0002C7CE File Offset: 0x0002A9CE
		// (set) Token: 0x06004149 RID: 16713 RVA: 0x0002C7DB File Offset: 0x0002A9DB
		public float groundAngularDrag
		{
			get
			{
				return this._value.groundAngularDrag;
			}
			set
			{
				this._value.groundAngularDrag = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x0002C7E9 File Offset: 0x0002A9E9
		// (set) Token: 0x0600414B RID: 16715 RVA: 0x0002C7F6 File Offset: 0x0002A9F6
		public float groundDrag
		{
			get
			{
				return this._value.groundDrag;
			}
			set
			{
				this._value.groundDrag = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x0002C804 File Offset: 0x0002AA04
		// (set) Token: 0x0600414D RID: 16717 RVA: 0x0002C811 File Offset: 0x0002AA11
		public float groundSteeringDrag
		{
			get
			{
				return this._value.groundSteeringDrag;
			}
			set
			{
				this._value.groundSteeringDrag = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x0002C81F File Offset: 0x0002AA1F
		// (set) Token: 0x0600414F RID: 16719 RVA: 0x0002C82C File Offset: 0x0002AA2C
		public bool inReverseGear
		{
			get
			{
				return this._value.inReverseGear;
			}
			set
			{
				this._value.inReverseGear = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x0002C83A File Offset: 0x0002AA3A
		// (set) Token: 0x06004151 RID: 16721 RVA: 0x0002C847 File Offset: 0x0002AA47
		public bool isAmphibious
		{
			get
			{
				return this._value.isAmphibious;
			}
			set
			{
				this._value.isAmphibious = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x0002C855 File Offset: 0x0002AA55
		// (set) Token: 0x06004153 RID: 16723 RVA: 0x0002C862 File Offset: 0x0002AA62
		public float reverseAcceleration
		{
			get
			{
				return this._value.reverseAcceleration;
			}
			set
			{
				this._value.reverseAcceleration = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x0002C870 File Offset: 0x0002AA70
		// (set) Token: 0x06004155 RID: 16725 RVA: 0x0002C87D File Offset: 0x0002AA7D
		public float slideDrag
		{
			get
			{
				return this._value.slideDrag;
			}
			set
			{
				this._value.slideDrag = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06004156 RID: 16726 RVA: 0x0002C88B File Offset: 0x0002AA8B
		// (set) Token: 0x06004157 RID: 16727 RVA: 0x0002C898 File Offset: 0x0002AA98
		public float speedTurnTorque
		{
			get
			{
				return this._value.speedTurnTorque;
			}
			set
			{
				this._value.speedTurnTorque = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x0002C8A6 File Offset: 0x0002AAA6
		// (set) Token: 0x06004159 RID: 16729 RVA: 0x0012FDA4 File Offset: 0x0012DFA4
		public SoundBankProxy suspensionShiftSounds
		{
			get
			{
				return SoundBankProxy.New(this._value.suspensionShiftSounds);
			}
			set
			{
				SoundBank suspensionShiftSounds = null;
				if (value != null)
				{
					suspensionShiftSounds = value._value;
				}
				this._value.suspensionShiftSounds = suspensionShiftSounds;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x0002C8C5 File Offset: 0x0002AAC5
		public bool tankTurning
		{
			get
			{
				return this._value.tankTurning;
			}
			set
			{
				this._value.tankTurning = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x0002C8D3 File Offset: 0x0002AAD3
		// (set) Token: 0x0600415D RID: 16733 RVA: 0x0002C8E0 File Offset: 0x0002AAE0
		public float topSpeed
		{
			get
			{
				return this._value.topSpeed;
			}
			set
			{
				this._value.topSpeed = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x0002C8EE File Offset: 0x0002AAEE
		public GameObjectProxy gameObject
		{
			get
			{
				return GameObjectProxy.New(WMonoBehaviour.GetGameObject(this._value));
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x0002C900 File Offset: 0x0002AB00
		public TransformProxy transform
		{
			get
			{
				return TransformProxy.New(WMonoBehaviour.GetTransform(this._value));
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x0002C912 File Offset: 0x0002AB12
		public float avoidanceRadius
		{
			get
			{
				return WVehicle.GetAvoidanceRadius(this._value);
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x0002C91F File Offset: 0x0002AB1F
		public bool canSeePlayer
		{
			get
			{
				return WVehicle.GetCanSeePlayer(this._value);
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x0002C92C File Offset: 0x0002AB2C
		public ActorProxy driver
		{
			get
			{
				return ActorProxy.New(WVehicle.GetDriver(this._value));
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x0002C93E File Offset: 0x0002AB3E
		public EngineProxy engine
		{
			get
			{
				return EngineProxy.New(WVehicle.GetEngine(this._value));
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x0002C950 File Offset: 0x0002AB50
		public bool hasCountermeasures
		{
			get
			{
				return WVehicle.GetHasCountermeasures(this._value);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x0002C95D File Offset: 0x0002AB5D
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x0002C96A File Offset: 0x0002AB6A
		public float health
		{
			get
			{
				return WVehicle.GetHealth(this._value);
			}
			set
			{
				WVehicle.SetHealth(this._value, value);
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x0002C978 File Offset: 0x0002AB78
		public bool isAirplane
		{
			get
			{
				return WVehicle.GetIsAirplane(this._value);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x0002C985 File Offset: 0x0002AB85
		public bool isBeingLocked
		{
			get
			{
				return WVehicle.GetIsBeingLocked(this._value);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x0002C992 File Offset: 0x0002AB92
		public bool isBoat
		{
			get
			{
				return WVehicle.GetIsBoat(this._value);
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x0002C99F File Offset: 0x0002AB9F
		public bool isBurning
		{
			get
			{
				return WVehicle.GetIsBurning(this._value);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		public bool isCar
		{
			get
			{
				return WVehicle.GetIsCar(this._value);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x0002C9B9 File Offset: 0x0002ABB9
		public bool isHelicopter
		{
			get
			{
				return WVehicle.GetIsHelicopter(this._value);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x0002C9C6 File Offset: 0x0002ABC6
		public bool isTrackedByMissile
		{
			get
			{
				return WVehicle.GetIsTrackedByMissile(this._value);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600416E RID: 16750 RVA: 0x0002C9D3 File Offset: 0x0002ABD3
		public bool isTurret
		{
			get
			{
				return WVehicle.GetIsTurret(this._value);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x0002C9ED File Offset: 0x0002ABED
		public float maxHealth
		{
			get
			{
				return WVehicle.GetMaxHealth(this._value);
			}
			set
			{
				WVehicle.SetMaxHealth(this._value, value);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x0002C9FB File Offset: 0x0002ABFB
		public TextureProxy minimapBlip
		{
			get
			{
				return TextureProxy.New(WVehicle.GetMinimapBlip(this._value));
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06004172 RID: 16754 RVA: 0x0002CA0D File Offset: 0x0002AC0D
		public string name
		{
			get
			{
				return WVehicle.GetName(this._value);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x0002CA1A File Offset: 0x0002AC1A
		public float playerDistance
		{
			get
			{
				return WVehicle.GetPlayerDistance(this._value);
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x0002CA27 File Offset: 0x0002AC27
		public bool playerIsInside
		{
			get
			{
				return WVehicle.GetPlayerIsInside(this._value);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x0002CA34 File Offset: 0x0002AC34
		public RigidbodyProxy rigidbody
		{
			get
			{
				return RigidbodyProxy.New(WVehicle.GetRigidbody(this._value));
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x0002CA46 File Offset: 0x0002AC46
		public IEnumerable<Seat> seats
		{
			get
			{
				return WVehicle.GetSeats(this._value);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x0002CA53 File Offset: 0x0002AC53
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x0002CA60 File Offset: 0x0002AC60
		public float spotChanceMultiplier
		{
			get
			{
				return WVehicle.GetSpotChanceMultiplier(this._value);
			}
			set
			{
				WVehicle.SetSpotChanceMultiplier(this._value, value);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x0002CA6E File Offset: 0x0002AC6E
		public int team
		{
			get
			{
				return WVehicle.GetTeam(this._value);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x0002CA7B File Offset: 0x0002AC7B
		public bool hasDriver
		{
			get
			{
				return WVehicle.HasDriver(this._value);
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x0002CA88 File Offset: 0x0002AC88
		public bool isDead
		{
			get
			{
				return WVehicle.IsDead(this._value);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x0002CA95 File Offset: 0x0002AC95
		public bool isEmpty
		{
			get
			{
				return WVehicle.IsEmpty(this._value);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x0002CAA2 File Offset: 0x0002ACA2
		public bool isFull
		{
			get
			{
				return WVehicle.IsFull(this._value);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x0002CAAF File Offset: 0x0002ACAF
		public bool isInWater
		{
			get
			{
				return WVehicle.IsInWater(this._value);
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x0002CABC File Offset: 0x0002ACBC
		[MoonSharpHidden]
		public object GetValue()
		{
			return this._value;
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x0012FDCC File Offset: 0x0012DFCC
		[MoonSharpHidden]
		public static ArcadeCarProxy New(ArcadeCar value)
		{
			if (value == null)
			{
				return null;
			}
			ArcadeCarProxy arcadeCarProxy = (ArcadeCarProxy)ObjectCache.Get(typeof(ArcadeCarProxy), value);
			if (arcadeCarProxy == null)
			{
				arcadeCarProxy = new ArcadeCarProxy(value);
				ObjectCache.Add(typeof(ArcadeCarProxy), value, arcadeCarProxy);
			}
			return arcadeCarProxy;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
		[MoonSharpUserDataMetamethod("__call")]
		public static ArcadeCarProxy Call(DynValue _)
		{
			return new ArcadeCarProxy();
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x0002CACB File Offset: 0x0002ACCB
		public bool IsChangingGears()
		{
			return this._value.IsChangingGears();
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x0012FE18 File Offset: 0x0012E018
		public void Damage(ActorProxy source, float amount)
		{
			Actor source2 = null;
			if (source != null)
			{
				source2 = source._value;
			}
			WVehicle.Damage(this._value, source2, amount);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		public SeatProxy GetEmptySeat(bool allowDriverSeat)
		{
			return SeatProxy.New(WVehicle.GetEmptySeat(this._value, allowDriverSeat));
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x0002CAEB File Offset: 0x0002ACEB
		public TargetSeekingMissile[] GetTrackingMissiles()
		{
			return WVehicle.GetTrackingMissiles(this._value);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
		public bool Repair(float amount)
		{
			return WVehicle.Repair(this._value, amount);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x0002CB06 File Offset: 0x0002AD06
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400314A RID: 12618
		[MoonSharpHidden]
		public ArcadeCar _value;
	}
}
