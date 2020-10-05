using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDialog : MonoBehaviour
{

    private float sinceLastDialog = 0f;
    private float sinceLastCheck = 0f;
    private float checkInterval = 0.5f;
    private float minDialogInterval = 5f;

    private float minProximity = 1f;


    private List<PlayerCommentTag> playerComments = new List<PlayerCommentTag>();

    void Start() {
        playerComments = Configs.main.UI.PlayerComments;
        sinceLastDialog = minDialogInterval;
        minProximity = Configs.main.UI.PlayerCommentMaxDistance;
    }

    void Update()
    {
        sinceLastCheck += Time.deltaTime;
        sinceLastDialog += Time.deltaTime;
        PlayerCommentTag nearestCommentable = null;
        float nearestDistance = -1f;
        if (sinceLastDialog >= minDialogInterval) {
            if (sinceLastCheck >= checkInterval) {
                sinceLastCheck = 0f;
                foreach(PlayerCommentTag tagComment in playerComments) {
                    if (!tagComment.ConditionChecksOut()) {
                        continue;
                    }
                    float nearest = GetNearestDistance(tagComment.Tag);
                    if (nearest < nearestDistance || nearestDistance < 0) {
                        nearestDistance = nearest;
                        nearestCommentable = tagComment;
                    }
                }
                if (nearestCommentable != null && nearestDistance <= minProximity) {
                    Comment(nearestCommentable.Comment);
                    sinceLastDialog = 0f;
                }
            }
        }
    }

    private void Comment(string comment) {
        UIManager.main.ShowBillboardText(comment, Tools.GetPlayerPosition() + new Vector3(0f, 2f, 0f), null, default(Color), true);
    }

    private float GetNearestDistance(string tag) {
        float nearestDistance = -1;
        foreach(GameObject foundObject in GameObject.FindGameObjectsWithTag(tag)) {
            float distance = Vector2.Distance(foundObject.transform.position, Tools.GetPlayerPosition());
            if (distance < nearestDistance || nearestDistance < 0) {
                nearestDistance = distance;
            }
        }
        return nearestDistance;
    }
}

[System.Serializable]
public class PlayerCommentTag
{
    [SerializeField]
    private string tag;
    public string Tag { get { return tag; } }

    [SerializeField]
    private string comment;
    public string Comment { get { return comment; } }

    [SerializeField]
    private PlayerCommentCondition condition;

    private List<ItemType> weapons = new List<ItemType>(){
        ItemType.CLUB,
        ItemType.SWORD,
        ItemType.PICKAXE
    };


    public bool ConditionChecksOut () {
        if (condition == PlayerCommentCondition.DontHaveWeapon) {
            return !Configs.main.PlayerInventory.PlayerItems.Any(item => weapons.Contains(item.Type));
        } else if (condition == PlayerCommentCondition.DontHavePickaxe) {
            return !Configs.main.PlayerInventory.PlayerItems.Any(item => item.Type == ItemType.PICKAXE);
        }
        return true;
    }
}

public enum PlayerCommentCondition {
    None,
    DontHaveWeapon,
    DontHavePickaxe
}
