using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class UIMultiListViewItem : MonoBehaviour
    {
        [SerializeField] private PostController postController;
        [SerializeField] private ReportController reportController;
        [SerializeField] private TextMeshProUGUI textContent;
        [SerializeField] private Image imageBackground;
        [SerializeField] private UIMultiModel multiModel;

        public UIMultiModel MultiModel { get => multiModel; set => multiModel = value; }

        public void UpdateView()
        {
            imageBackground.color = new Color(1f, 1f, 1f, 1f);
            textContent.fontStyle = FontStyles.Normal;

            if (multiModel.Type.Equals("filter_template"))
            {
                textContent.text = multiModel.PassedVariable["name"];
                textContent.alignment = TextAlignmentOptions.MidlineLeft;
            }

            if (multiModel.Type.Equals("report_type"))
            {
                textContent.text = multiModel.PassedVariable["name"];
                textContent.alignment = TextAlignmentOptions.MidlineLeft;
            }

            if (multiModel.Type.Equals("center_text"))
            {
                textContent.text = multiModel.PassedVariable["content"];
                textContent.alignment = TextAlignmentOptions.Center;
            }

            if(multiModel.Type.Equals("border_less_center_text"))
            {
                textContent.text = multiModel.PassedVariable["content"];
                textContent.alignment = TextAlignmentOptions.Center;
                imageBackground.color = new Color(0f, 0f, 0f, 0f);
            }

            if (multiModel.Type.Equals("left_text"))
            {
                textContent.text = multiModel.PassedVariable["content"];
                textContent.alignment = TextAlignmentOptions.MidlineLeft;
                textContent.fontStyle = FontStyles.Normal;
            }

        }

        public void OnButtonClicked()
        {
            if (multiModel.Type.Equals("filter_template"))
            {
                postController.ChangeFilter(multiModel.PassedVariable["id"]);
            }

            if (multiModel.Type.Equals("report_type"))
            {
                reportController.SetReportType(multiModel);
            }
        }
    }
}
