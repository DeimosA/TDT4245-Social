using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Add all custom serializable dictionaries to this file
//To add a new serializable dictionary type, declare it in this file,
//and declare a new CustomPropertyDrawer in SerializableDictionary/Editor/SerializableDictionaryPropertyDrawer.cs

[Serializable]
public class CardChoiceDictionary : SerializableDictionary<ActivityCard, ActivityChoice> { }

[Serializable]
public class StringBoolDictionary : SerializableDictionary<string, bool> { }

[Serializable]
public class PlayerStatIntDictionary : SerializableDictionary<PlayerStat, int> { }



