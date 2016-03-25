using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace CompleteProject
{
	public class CloudBuildVersion : MonoBehaviour {

	    [Serializable]
	    public class BuildManifest
	    {
	        public int buildNumber;
	        public string cloudBuildTargetName;
	        public string unityVersion;
	        public string buildStartTime;
			public string scmCommitId;
			public string projectId;

		}

		public Text uiText;
		// Use this for initialization
		void Start () {
			if (this.uiText == null) {
	            return;
	        }
			#if UNITY_5_3 || UNITY_5_3_OR_NEWER
	        var manifest = (TextAsset) Resources.Load("UnityCloudBuildManifest.json");
	        if (manifest == null)
	        {
				this.uiText.text = "not CloubBuild";
	            return;
	        }
	        BuildManifest buildManifest = JsonUtility.FromJson<BuildManifest>(manifest.text);
			this.uiText.text = 
				buildManifest.cloudBuildTargetName + "\n" + 
				"BuildNumber #" + buildManifest.buildNumber + "\n" +
				buildManifest.buildStartTime + "\n" +
				"SCM " + buildManifest.scmCommitId + "\n" +
				"PID " + buildManifest.projectId + "\n" +
				"(unity ver" + buildManifest.unityVersion +")";
			#endif
		}
	}
}