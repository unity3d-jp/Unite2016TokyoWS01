#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("LNeQc2akEzazWnUpaHP0Alhu/vhbUFazv/h/lLvj9WfONj6km/cU8/sxQ+3WCgF/lJFAjmNNrCPJj/WBiSLCzaEnTM47EKXfi61ZZDv+PvLGCTKlqqqOwJlQFTYIHstLBBbMlga3sEbZVnlklEeIgRx3LK85HVEUmGvvhAL1F/m9vxzWgHmcHu6hBT6ICwUKOogLAAiICwsKiQoO//vNn8QqswvJUy+MmB4HU8IgZKpIn93VsiTwKFuN/unlHm2NMZYJAR9dK0cTbKJaysGJMwxvALjKNHVsnnnPo0cXMx4I/WI7bwMuNg4UAOZmAHEnOogLKDoHDAMgjEKM/QcLCwsPCgkbEO78ARJCt2SJ4LqDRgtfTJQieIyXvq+W1DzjQQgJCwoL");
        private static int[] order = new int[] { 8,4,3,13,11,12,13,11,10,12,11,12,12,13,14 };
        private static int key = 10;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
