using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    public class UIClassroomListViewItem : MonoBehaviour
    {
        [SerializeField] private Sprite spriteDefaultAvatar;
        [SerializeField] private Image imageAvatar;
        [SerializeField] private ClassroomController classroomController;
        [SerializeField] private UIClassroomModel classroomModel;

        public UIClassroomModel ClassroomModel { get => classroomModel; set => classroomModel = value; }

        /// <summary>
        /// call in button get questions and answers
        /// </summary>
        public void GetQuestionsAndAnswers()
        {
            classroomController.GetQuestionsAndAnswers(classroomModel);
        }

        public void CheckAndGetOldClassrooms()
        {
            classroomController.CheckAndGetOldClassrooms(classroomModel);
        }

        public void CheckAndDownloadAvatar()
        {
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "classrooms/avatars/")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "classrooms/avatars/"));
            }

            if (!ClassroomModel.AvatarPath.Equals(""))
            {
                if (!File.Exists(Path.Combine("file://", Application.persistentDataPath, "classrooms/avatars/" + classroomModel.Id + ".png")))
                {
                    StartCoroutine(DownloadAndSetImageCorotine());
                }
                else
                {
                    StartCoroutine(SetImageCoroutine());
                }
            }
            else
            {
                imageAvatar.sprite = spriteDefaultAvatar;
            }
        }

        private IEnumerator DownloadAndSetImageCorotine()
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(GlobalSetting.Endpoint + classroomModel.AvatarPath);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading image: " + request.error);
                yield break;
            }

            Texture2D downloadedTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            byte[] imageBytes = downloadedTexture.EncodeToPNG();

            string savePath = Path.Combine(Application.persistentDataPath, "classrooms/avatars/" + classroomModel.Id + ".png");
            File.WriteAllBytes(savePath, imageBytes);

            Debug.Log("Đã được tải và lưu vào: " + savePath);

            imageAvatar.sprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), Vector2.one * 0.5f);

            Debug.Log("Image downloaded and set in Image component.");
        }

        private IEnumerator SetImageCoroutine()
        {
            Texture2D texture;
            UnityWebRequest request = UnityWebRequestTexture.GetTexture("file://" + Path.Combine(Application.persistentDataPath, "classrooms/avatars/" + classroomModel.Id + ".png"));

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                texture = DownloadHandlerTexture.GetContent(request);

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                imageAvatar.sprite = sprite;

            }
            else
            {
                Debug.LogError("Lỗi tải ảnh: " + request.error);
            }
        }
    }
}
