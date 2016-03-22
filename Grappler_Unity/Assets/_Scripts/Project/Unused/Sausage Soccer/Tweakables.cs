/*using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tweakables : ScriptableObjectSingleton<Tweakables>
{
	ActorPhysicsVars _actorVars = null;
	public static ActorPhysicsVars GenericActorPhysics
	{ 
		get
		{
			if (instance._actorVars == null)
				instance._actorVars = Resources.Load<ActorPhysicsVars>("Tweakables/GenericActorPhysicsSettings");

			return instance._actorVars; 
		}
	}

	EntityPhysicsVars _entityVars = null;
	public static EntityPhysicsVars GenericEntityPhysics
	{
		get
		{
			if (instance._entityVars == null)
				instance._entityVars = Resources.Load<EntityPhysicsVars>("Tweakables/GenericEntityPhysicsSettings");

			return instance._entityVars;
		}
	}

	PlayerPhysicsVars _physicsVars = null;
	public static PlayerPhysicsVars SharedPlayerPhysics
	{
		get
		{
			if (instance._physicsVars == null)
				instance._physicsVars = Resources.Load<PlayerPhysicsVars>("Tweakables/SharedPlayerPhysicsSettings");

			return instance._physicsVars;
		}
	}

	ActorPhysicsVars _poundOverrideVars = null;
	public static ActorPhysicsVars PlayerPoundOverrideVars
	{
		get
		{
			if (instance._poundOverrideVars == null)
				instance._poundOverrideVars = Resources.Load<ActorPhysicsVars>("Tweakables/PlayerPoundOverrideSettings");

			return instance._poundOverrideVars;
		}
	}

	PlayerAIVars _playerAIVars = null;
	public static PlayerAIVars SharedPlayerAI
	{
		get
		{
			if (instance._playerAIVars == null)
				instance._playerAIVars = Resources.Load<PlayerAIVars>("Tweakables/SharedPlayerAISettings");

			return instance._playerAIVars;
		}
	}

	PlayerSausageVars _sausageVars = null;
	public static PlayerSausageVars SharedSausage
	{
		get
		{
			if (instance._sausageVars == null)
				instance._sausageVars = Resources.Load<PlayerSausageVars>("Tweakables/SharedPlayerSausageSettings");
			return instance._sausageVars;
		}
	}

	GameModeVars _modeVars = null;
	public static GameModeVars ModeVars
	{
		get
		{
			if (instance._modeVars == null)
				instance._modeVars = Resources.Load<GameModeVars>("Tweakables/GameModeSettings");
			return instance._modeVars;
		}
	}
}
*/