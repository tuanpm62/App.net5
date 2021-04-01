// Unity, with filter sample.

using ObservableCollections;
using UnityEngine;
using UnityEngine.UI;

public class SampleScript : MonoBehaviour
{
    public Button prefab;
    public GameObject root;
    ObservableRingBuffer<int> collection;
    ISynchronizedView<int, GameObject> view;

    void Start()
    {
        collection = new ObservableRingBuffer<int>();
        view = collection.CreateView(x =>
        {
            var item = GameObject.Instantiate(prefab);
            item.GetComponentInChildren<Text>().text = x.ToString();
            return item.gameObject;
        });
        view.AttachFilter(new GameObjectFilter(root));

        collection.AddFirst(2);
    }

    void OnDestroy()
    {
        view.Dispose();
    }

    public class GameObjectFilter : ISynchronizedViewFilter<int, GameObject>
    {
        readonly GameObject root;

        public GameObjectFilter(GameObject root)
        {
            this.root = root;
        }

        public void OnCollectionChanged(ChangedKind changedKind, int value, GameObject view)
        {
            if (changedKind == ChangedKind.Add)
            {
                view.transform.SetParent(root.transform);
            }
            else if (changedKind == ChangedKind.Remove)
            {
                GameObject.Destroy(view);
            }
        }

        public bool IsMatch(int value, GameObject view)
        {
            return value % 2 == 0;
        }

        public void WhenTrue(int value, GameObject view)
        {
            view.SetActive(true);
        }

        public void WhenFalse(int value, GameObject view)
        {
            view.SetActive(false);
        }
    }
}
