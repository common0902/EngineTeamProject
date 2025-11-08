using _00.Work.AJ._01._Scripts.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/EnemyStateChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "EnemyStateChannel", message: "Set [CurrentState]", category: "Events", id: "8dc0906f038740bcf87092df79f66833")]
public sealed partial class EnemyStateChannel : EventChannel<EnemyStates> { }

