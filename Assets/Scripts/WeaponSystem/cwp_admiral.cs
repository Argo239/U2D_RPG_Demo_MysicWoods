using Assets.Scripts.Interface;
using System.Collections;
using UnityEngine;

public class cwp_admiral : MonoBehaviour, IWeaponLogic {
    public static cwp_admiral Instance {  get; private set; }

    [SerializeField] private SectorHitboxRangeConfig SectorHitboxConfig;

    private PolygonCollider2D polyCol;
    private PlayerController playerController;
    private CalculateHitboxRange calHitboxRange;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        calHitboxRange = new CalculateHitboxRange();
    }

    private void Start() {
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    public void Attack() {
        GenerateSectorHitbox();
        StartCoroutine(DisableColliderAfterDelay(polyCol, 0.8f));

    }

    public void StopAttack() {
    
    }


    void GenerateSectorHitbox() {
        PolygonCollider2D exitsPolyCol = transform.parent.GetComponent<PolygonCollider2D>();

        if (polyCol != null) {
            Destroy(exitsPolyCol);
        }

        polyCol = transform.parent.gameObject.AddComponent<PolygonCollider2D>();
        polyCol.points = calHitboxRange.GenerateSector(SectorHitboxConfig, playerController.GetFacingDir());
        polyCol.isTrigger = true;
        polyCol.enabled = true;
        polyCol.offset = SectorHitboxConfig.offset;
    }

    IEnumerator DisableColliderAfterDelay(Collider2D collider, float delay) {
        yield return new WaitForSeconds(delay);
        if (collider != null) {
            collider.enabled = false;
            Destroy(collider);
        }
    }
}