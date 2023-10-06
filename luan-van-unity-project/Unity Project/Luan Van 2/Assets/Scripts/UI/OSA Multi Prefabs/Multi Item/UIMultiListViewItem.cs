using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LuanVan.OSA
{

    public class UIMultiListViewItem : MonoBehaviour
    {
        [SerializeField] private PostController postController;
        [SerializeField] private ReportController reportController;
        [SerializeField] private TextMeshProUGUI textContent;
        [SerializeField] private UIMultiModel multiModel;

        public UIMultiModel MultiModel { get => multiModel; set => multiModel = value; }

        public void UpdateView()
        {
            if (multiModel.Type.Equals("filter_template"))
            {
                textContent.text = multiModel.PassedVariable["name"];
            }

            if (multiModel.Type.Equals("report_type"))
            {
                textContent.text = multiModel.PassedVariable["name"];
            }

            if (multiModel.Type.Equals("center_text"))
            {
                textContent.text = multiModel.PassedVariable["content"];
                textContent.alignment = TextAlignmentOptions.Center;
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
