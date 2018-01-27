using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleValueSource<T> : IValueSource<T>
{

	[SerializeField]
	private string identifier = "NON RENSEIGNE";
	public string Identifier {
		get {
			return identifier;
		}
	}

	[SerializeField]
	private T _storedValue;
	public T StoredValue {
		get {
			return _storedValue;
		}
		set {
			if (!Equals (_storedValue, value)) {
				_storedValue = value;
				ValueSourcesSender.SendNewValue (this);
			}
		}
	}

	T IValueSource<T>.StoredValue {
		get {
			return StoredValue;
		}
	}

}
