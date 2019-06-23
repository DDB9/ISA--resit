using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherGhost : Enemy {

    public GameObject GhostPrefab;

    private bool hatching;

    // Start is called before the first frame update
    void Start() {
        PlayerController.OnEnemyHit += ChangeColorOnHit;

        enemySpeed = 6f;
        health = 21;
    }

    // Update is called once per frame
    void Update() {
        CheckState();
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceFromPlayer <= engageDistance && distanceFromPlayer > meleeRange) {
            currentState = EnemyState.ENGAGE;
        }
        else if (distanceFromPlayer > engageDistance) {
            currentState = EnemyState.HATCH;
        }
    }

    // the ghost deals damage over time to the player when in range.
    public override IEnumerator DealDamageOverTime() {
        player.playerLives -= 2;
        player.imagePlayerLives[player.imagePlayerLives.Count - 1].enabled = false;
        player.imagePlayerLives.RemoveAt(player.imagePlayerLives.Count - 1);

        yield return new WaitForSeconds(2);

        attacked = false;
        currentState = EnemyState.ENGAGE;
    }

    // checks the state of the ghost's AI.
    public override void CheckState() {
        base.CheckState();

        switch (currentState) {
            case EnemyState.HATCH:
                if (hatching == false) {
                    StartCoroutine("HatchGhostling");
                }
                break;
        }
    }

    // the mother ghost hatches a new ghost every 8 seconds.
    IEnumerator HatchGhostling() {
        hatching = true;

        yield return new WaitForSeconds(8f);
        GameObject ghost = Instantiate(GhostPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
        manager.enemies.Add(ghost.GetComponent<Enemy>());

        hatching = false;
    }

    public void ChangeColorOnHit(Enemy enemy) {    // Why isn't IEnumerator possible? Invoke instead of simply calling?
        StartCoroutine("ChangeColor", enemy);
    }

    // changes the Enemy Color on hit.
    public IEnumerator ChangeColor(Enemy enemy) {
        if (this != null) {
            enemy.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            enemy.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // activates if object becomes inactive or is destroyed.
    private void OnDisable() {
        PlayerController.OnEnemyHit -= ChangeColorOnHit;    // unsubscribes from the OnEnemyHit event.
    }
}
