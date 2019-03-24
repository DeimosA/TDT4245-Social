using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Declare any new CustomPropertyDrawer types here

[CustomPropertyDrawer(typeof(CardChoiceDictionary))]
[CustomPropertyDrawer(typeof(StringBoolDictionary))]
[CustomPropertyDrawer(typeof(PlayerStatIntDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

//[CustomPropertyDrawer(typeof(ColorArrayStorage))]
//public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
