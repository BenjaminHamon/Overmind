using Newtonsoft.Json;
using Overmind.Core.Extensions;
using Overmind.Games.Engine;
using Overmind.Unity;
using System.Collections.Generic;
using UnityEngine;

namespace Overmind.Games.Unity
{
    public class MainMenuView : MonoBehaviourBase
    {
        private Loader loader { get { return UnityApplication.Instance.Loader; } }

        public TextAsset ConfigurationFile;
        public Transform ModListView;
        public GameObject ModItemPrefab;

        public override void Start()
        {
            ApplicationConfiguration configuration = JsonConvert.DeserializeObject<ApplicationConfiguration>(ConfigurationFile.text);

            LoadModList();
        }

        public void LoadModList()
        {
            IEnumerable<string> modCollection = loader.GetModList();
            Debug.Log("LoadModList " + modCollection.ToCollectionString());

            foreach (string mod in modCollection)
            {
                GameObject modItem = Instantiate(ModItemPrefab);
                modItem.GetComponent<ModView>().Mod = mod;
                modItem.transform.SetParent(ModListView);
            }
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}