using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    List<List<GameObject>> Colliders;
    public static GameController instance;
    [SerializeField] bool notinGame;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!notinGame)
            ColliderCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetCollider(){
        Colliders = new List<List<GameObject>>{};
        for(int i=0; i<=9; i+=1){
            List<GameObject> col = new List<GameObject>{};
            Colliders.Add(col);
        }
    }

    public void ColliderCheck(){
        ResetCollider();
        //Debug.Log(GameObject.Find("hero").GetComponent<Numbers>().GetNum());
        Debug.Log("BEGIN");

        GameObject blocks = GameObject.Find("Blocks");
        GameObject boxes = GameObject.Find("Boxes");
        for(int i=0; i<blocks.transform.childCount; i+=1){
            foreach(int num in blocks.transform.GetChild(i).GetComponent<Numbers>().NumberCheck()){
                //Debug.Log(num + " " + blocks.transform.GetChild(i).gameObject.name);
                Colliders[num].Add(blocks.transform.GetChild(i).gameObject);
            }

            Ignore(GameObject.Find("hero"), blocks.transform.GetChild(i).gameObject, true);            
        }

        for(int box=0; box<boxes.transform.childCount; box+=1){
            foreach(int num in boxes.transform.GetChild(box).GetComponent<Numbers>().NumberCheck()){
                Colliders[num].Add(boxes.transform.GetChild(box).gameObject);
            }

            Ignore(GameObject.Find("hero"), boxes.transform.GetChild(box).gameObject, true);

            for(int i=0; i<blocks.transform.childCount; i+=1){
                Ignore(boxes.transform.GetChild(box).gameObject, blocks.transform.GetChild(i).gameObject, true);
            }

            for(int _box=0; _box<boxes.transform.childCount; _box+=1){
                Ignore(boxes.transform.GetChild(box).gameObject, boxes.transform.GetChild(_box).gameObject, true);
            }
        }

        for(int box=0; box<boxes.transform.childCount; box+=1){
            ColliderSet(boxes.transform.GetChild(box).gameObject);
        }

        ColliderSet(GameObject.Find("hero"));
    }

    private void Ignore(GameObject obj1, GameObject obj2, bool ig){
        if(obj1 == obj2)
            return ;
        if(obj1.tag == "Hero" && obj2.tag == "Box"){
            if(ig)  //true代表不一样
                obj2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            else
                obj2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        if(obj1.name != "hero")
            Debug.Log(obj1.name + " " + obj2.name + " " + ig);
        Physics2D.IgnoreCollision(obj1.GetComponent<Collider2D>(), obj2.transform.Find("wall1").GetComponent<Collider2D>(), ig);
        Physics2D.IgnoreCollision(obj1.GetComponent<Collider2D>(), obj2.transform.Find("wall2").GetComponent<Collider2D>(), ig);
    }

    private void ColliderSet(GameObject obj){
        string names = "";
        for(int i=1; i<=9; i+=1)
            if(Colliders[i].Count != 0)
                foreach(GameObject j in Colliders[i])
                    names += i + " " + j.name + " ";
        Debug.Log(names);
        foreach(int num in obj.GetComponent<Numbers>().NumberCheck()){ 
        //    Debug.Log(num + " " + Colliders[num].Count);
            if(Colliders[num].Count != 0)
                foreach(GameObject b in Colliders[num]){
        //            Debug.Log(num + " " + obj.name + " " + b.name);
                    Ignore(obj, b, false);
                }
        }
    }

    public static bool Contact(GameObject obj1, GameObject obj2){
        List<int> nums1 = obj1.GetComponent<Numbers>().NumberCheck();
        List<int> nums2 = obj2.GetComponent<Numbers>().NumberCheck();

        foreach(int num1 in nums1){
            foreach(int num2 in nums2){
                //Debug.Log(obj1.name + " " + num1 + " " + num2 + " " + obj2.name);
                if(num1 == num2)
                    return true;
            }
        }

        return false;
    }

    public void nextLevel(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void Reset(){
        GameObject plminuses = GameObject.Find("Plminus");
        GameObject boxes = GameObject.Find("Boxes");
        for(int plminus=0; plminus<plminuses.transform.childCount; plminus+=1){
            plminuses.transform.GetChild(plminus).GetComponent<Plminus>().Reset();
        }
        for(int box=0; box<boxes.transform.childCount; box+=1){
            boxes.transform.GetChild(box).GetComponent<Box>().Reset();
        }
    }
}
