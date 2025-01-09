using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ShootingGame : ScriptableObject
{
	public List<MonsterTable_Entity> MonsterTable; // Replace 'EntityType' to an actual type that is serializable.
}
