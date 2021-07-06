using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] GameObject tracerPrefab;
    [SerializeField] GameObject textPrefab;

    MouseController mouseController;
    ParticleUI particleUI;
    public Atom element;
    public GameController gameController;

    public bool playerInteracted = false;

    [SerializeField] string particleName;
    [SerializeField] int protonNumber;
    [SerializeField] int capacity;

    void Start()
    {
        mouseController = new MouseController(gameObject, tracerPrefab);
        element = new Atom(particleName, protonNumber, capacity);
        particleUI = new ParticleUI(gameObject, textPrefab);
        gameObject.name = element.name;

        Vector2 startVector = new Vector2(Random.Range(-1, 1), Random.Range(-1f, 1f)).normalized*200f;
        print(startVector.normalized);
        GetComponent<Rigidbody2D>().AddForce(startVector);

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        mouseController.HandleMouseInput();
        particleUI.UpdateText();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Atom")
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude > collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude &&
                element.BondPossible(collision.gameObject.GetComponent<Particle>().element) &&
                GetComponent<Rigidbody2D>().velocity.magnitude > 1f &&
                (playerInteracted || collision.gameObject.GetComponent<Particle>().playerInteracted))
            {
                element.FormBond(collision.gameObject.GetComponent<Particle>().element);
                collision.gameObject.GetComponent<Particle>().playerInteracted = true;
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.anchor = collision.contacts[0].point;
                joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                joint.enableCollision = false;
            }
        }
    }

    public static List<Particle> getParticlesFromTag()
    {
        GameObject[] Gos = GameObject.FindGameObjectsWithTag("Atom");
        List<Particle> particles = new List<Particle>();
        foreach (GameObject Go in Gos)
        {
            particles.Add(Go.GetComponent<Particle>());
        }
        return particles;
    }

}
