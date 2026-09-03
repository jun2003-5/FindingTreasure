using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class CouponManager : MonoBehaviour
{

    /*쿠폰의미
    돈: M숫자N숫자Y숫자
    다이아: K다이아갯수I다이아갯수M다이아갯수
    */
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject couponTab;
    [SerializeField] private TMP_InputField inputfield;
    private TouchScreenKeyboard keyboard;
    public Player player;

    public CouponData[] coupons;
    string itemNumber;

    private void Start()
    {
        for(int i = 0; i < coupons.Length; i++) {
            if(PlayerPrefs.GetInt(coupons[i].couponID) == 2) {
                coupons[i].isUsed = true;
            }
        }
    }
    public void couponTyped()
    {
        couponTab.SetActive(true);
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
    public void closeTab()
    {
        couponTab.SetActive(false);

    }
    public void OnValueChangedEvent(string str)
    {
        text.text = $"쿠폰번호: {str}";
    }

    public void OnEndEditEvent()
    {
        string str = "";
        itemNumber = "";
        str = inputfield.text;
        for (int i = 0; i < coupons.Length; i++) {
            if(str == coupons[i].couponID) {

                if (!coupons[i].isUsed) {
                    string[] digits = Regex.Split(str, @"\D+");
                    foreach (string value in digits) {
                        int number;
                        if (int.TryParse(value, out number)) {
                            itemNumber += value;
                        }
                    }

                    if (str.IndexOf("M") == 0) {
                        text.text = $"골드: {string.Format("{0:#,###0}", int.Parse(itemNumber))}";
                        player.money += int.Parse(itemNumber);
                        saveCouponUsed(coupons[i].couponID, 2);
                    } else if (str.IndexOf("K") == 0) {
                        text.text = $"다이아: {string.Format("{0:#,###0}", int.Parse(itemNumber))}";
                        player.diamond += int.Parse(itemNumber);
                        saveCouponUsed(coupons[i].couponID, 2);
                    }

                    coupons[i].isUsed = true;
                    return;

                } else {
                    text.text = $"유효하지않는 쿠폰";
                }
            } else {
                text.text = $"유효하지않는 쿠폰";
            }
        }
    }
    
    public void OnSelectEvent(string str)
    {
        text.text = $"쿠폰을 입력하세요 {str}";
    }

    public void saveCouponUsed(string s, int i)
    {
        PlayerPrefs.SetInt(s, i);
    }
}
