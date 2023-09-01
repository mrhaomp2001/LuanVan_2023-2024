using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirector : MonoBehaviour
{
    [System.Serializable]
    public struct UIScreenRoute
    {
        public string name;
        public List<RectTransform> rectTransformsUi;
    }

    [SerializeField] private List<UIScreenRoute> screenRoutes = new List<UIScreenRoute>();
    [SerializeField] private Stack<string> stackRoute = new Stack<string>();

    public void Push(string routeName)
    {
        if (stackRoute.Count > 0)
        {
            foreach (var route in screenRoutes)
            {
                if (route.name.Equals(stackRoute.Peek()))
                {
                    foreach (var rectTransform in route.rectTransformsUi)
                    {
                        rectTransform.gameObject.SetActive(false);
                    }

                    break;
                }
            }
        }
        foreach (var route in screenRoutes)
        {
            if (route.name.Equals(routeName))
            {
                foreach (var rectTransform in route.rectTransformsUi)
                {
                    rectTransform.gameObject.SetActive(true);
                }

                stackRoute.Push(routeName);

                break;
            }
        }
    }

    public void Pop()
    {
        if (stackRoute.Count > 0)
        {
            foreach (var route in screenRoutes)
            {
                if (route.name.Equals(stackRoute.Peek()))
                {
                    foreach (var rectTransform in route.rectTransformsUi)
                    {
                        rectTransform.gameObject.SetActive(false);
                    }

                    stackRoute.Pop();
                    break;
                }
            }
        }

        if (stackRoute.Count > 0)
        {
            foreach (var route in screenRoutes)
            {
                if (route.name.Equals(stackRoute.Peek()))
                {
                    foreach (var rectTransform in route.rectTransformsUi)
                    {
                        rectTransform.gameObject.SetActive(true);
                    }

                    break;
                }
            }
        }
    }
}