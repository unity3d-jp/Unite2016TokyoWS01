#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("5SB1PV4/WYJSOrkCQXKFMmMqSMTtFRWKNuIeAMiWDefeAfiwXZh/4ue7zuf6ScxxcsnSv2GtOnu4VqzSThAQN3xsZeBcYGu3TjRv+OWBuu9paHTWrF2zXcSM/TlKlmhLCHM8YedV1vXn2tHe/VGfUSDa1tbW0tfUudD4iw6g3frJAIHiPG20OGEMVynMGdgTHd6zSnYGHE0iegptoq0msiBkqzCTYyxHq7Xfw/9+IVZCDc9PVdbY1+dV1t3VVdbW1201LWcD/phkNzGs+PZyvOATKNLj2HXB2n2nbhklGFHClgMvybeMSkmaHdx1gWPT5ca0oc11TMVHvBmxY1TrGaA4FmYKBCJhmLxViV4Cti69V2HVM3rr8hpGH8faiDYPutXU1tfW");
        private static int[] order = new int[] { 12,2,8,11,6,12,7,11,9,9,10,13,12,13,14 };
        private static int key = 215;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
