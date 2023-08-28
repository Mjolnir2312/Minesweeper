using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniJSON
{
	// Token: 0x0200016C RID: 364
	public class JSONAnimationCurveConverter : JSONCustomConverter
	{
		public override bool IsCanBeDeserialized(string type)
		{
			return type.Equals("AnimationCurve");
		}

		public override bool IsCanBeDeserialized(Type type)
		{
			return type.Equals(typeof(AnimationCurve));
		}

		public override object Serialize(object obj)
		{
			AnimationCurve animationCurve = (AnimationCurve)obj;
			return new Dictionary<string, object>
			{
				{
					"__type",
					"AnimationCurve"
				},
				{
					"keys",
					animationCurve.keys
				}
			};
		}

		public override object Deserialize(object obj)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
			object[] array = dictionary["keys"] as object[];
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				Keyframe key = (Keyframe)Json.JsonConverters[typeof(Keyframe)].Deserialize(array[i]);
				animationCurve.AddKey(key);
			}
			return animationCurve;
		}
	}
}
