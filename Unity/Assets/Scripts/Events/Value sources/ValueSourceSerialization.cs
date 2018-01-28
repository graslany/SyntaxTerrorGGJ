using System;

	public class ValueSourceSerialization
	{
		private static readonly string ERR_UNSUPPORTED_DATA_TYPE = "Type de donnée non supporté";

		public static string Serialize<T> (SimpleValueSource<T> source)
		{
			if (source == null || string.IsNullOrEmpty (source.Identifier))
				return null;

			string variableID = source.Identifier;
			if (typeof(T) == typeof(bool)) {
				SimpleValueSource<bool> castSource = (SimpleValueSource<bool>)(object) source;
				return SerializeInternal(variableID, (castSource.StoredValue ? "b1" : "b0"));
			} else if (typeof(T) == typeof(int)) {
				SimpleValueSource<int> castSource = (SimpleValueSource<int>)(object) source;
				return SerializeInternal(variableID, "i" + castSource.StoredValue.ToString());
			} else if (typeof(T) == typeof(float)) {
				SimpleValueSource<float> castSource = (SimpleValueSource<float>)(object) source;
				return SerializeInternal(variableID, "f" + castSource.StoredValue.ToString());
			} else {
				throw new ArgumentException (ERR_UNSUPPORTED_DATA_TYPE);
			}
		}

		public static IEventSource Deserialize (string source)
		{
			if (source == null)
				return null;

			int sepIndex = source.IndexOf (':');

			if (sepIndex < 1 || source.Length == sepIndex)
				throw new ArgumentException ("Contenu sérialisé invalide");

			string variableName = source.Substring (0, sepIndex);
			char variableType = source[sepIndex +1];
			string variableContents = source.Substring(sepIndex +2);

			switch (variableType) {
			case 'b':
				return new SimpleValueSource<bool> (variableName, bool.Parse(variableContents));
			case 'i':
				return new SimpleValueSource<int> (variableName, int.Parse(variableContents));
			case 'f':
				return new SimpleValueSource<float> (variableName, float.Parse(variableContents));
			default:
				throw new ArgumentException(ERR_UNSUPPORTED_DATA_TYPE);
			}
		}

		private static string SerializeInternal(string name, string contents) {
			return name + ":" + contents;
		}
	}

