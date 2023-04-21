using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class JsonReader : MonoBehaviour
{
    public static JsonReader Instance;

    private string json;

    [System.Serializable]
    public class Block
    {
        public int id;
        public string subject;
        public string grade;
        public int mastery;
        public string domainid;
        public string domain;
        public string cluster;
        public string standardid;
        public string standarddescription;
    }

    [System.Serializable]
    public class BlockList
    {
        public Block[] blocks;
    }

    public List<Block> blocks;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(GetRequest("https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack"));
        
    }

    void Update()
    {

    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            json = uwr.downloadHandler.text;
        }

        JSONNode data = JSON.Parse(json);


        foreach (JSONNode block in data)
        {
            Block newBlock = new Block();
            newBlock.id = block["id"].AsInt;
            newBlock.subject = block["subject"].Value;
            newBlock.grade = block["grade"].Value;
            newBlock.mastery = block["mastery"].AsInt;
            newBlock.domainid = block["domainid"].Value;
            newBlock.domain = block["domain"].Value;
            newBlock.cluster = block["cluster"].Value;
            newBlock.standardid = block["standardid"].Value;
            newBlock.standarddescription = block["standarddescription"].Value;
            blocks.Add(newBlock);
        }

        StackManager.Instance.Initialize();
    }
}
