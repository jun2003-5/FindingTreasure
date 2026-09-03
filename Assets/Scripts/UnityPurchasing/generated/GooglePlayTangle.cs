// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("HM8VYULHV05rO+CF3kylLYAx+sRMwzVWCvQcW9Au+FJE2s4ce3H+C/p9DgaguxcDgFlPW2EbuurmmMog+kjL6PrHzMPgTIJMPcfLy8vPyskkm04+eWcYL5RrbZUmuQBq3GVvV+gQ69trpxSeWd4M59paozjRZZZ4SMvFyvpIy8DISMvLyk8Ka9LOOhKnO9xg3HnB6QqO17yF8Iv/8p4RMBHJyfqKuDjtr7UJhg+pYSiS5oZKPJd8qw7gowdkmrGS6XOzKSeL+/FUOh/+e/ZM+rvNvcQswM+X6+nxhxCT3w0/VxHAE4ihfbCl8FjXOneIkXq6bXfRIAY1DQpz27iU7HTh8KoIheBkeZixqo7RuHmxROlKDgCF8CHXFLcMrkm158jJy8rL");
        private static int[] order = new int[] { 13,10,9,13,6,5,10,7,9,10,13,12,13,13,14 };
        private static int key = 202;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
