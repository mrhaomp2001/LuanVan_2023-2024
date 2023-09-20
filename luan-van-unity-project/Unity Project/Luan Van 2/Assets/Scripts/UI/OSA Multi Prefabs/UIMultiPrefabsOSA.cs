/*
 * * * * This bare-bones script was auto-generated * * * *
 * The code commented with "/ * * /" demonstrates how data is retrieved and passed to the adapter, plus other common commands. You can remove/replace it once you've got the idea
 * Complete it according to your specific use-case
 * Consult the Example scripts if you get stuck, as they provide solutions to most common scenarios
 * 
 * Main terms to understand:
 *		Model = class that contains the data associated with an item (title, content, icon etc.)
 *		Views Holder = class that contains references to your views (Text, Image, MonoBehavior, etc.)
 * 
 * Default expected UI hiererchy:
 *	  ...
 *		-Canvas
 *		  ...
 *			-MyScrollViewAdapter
 *				-Viewport
 *					-Content
 *				-Scrollbar (Optional)
 *				-ItemPrefab (Optional)
 * 
 * Note: If using Visual Studio and opening generated scripts for the first time, sometimes Intellisense (autocompletion)
 * won't work. This is a well-known bug and the solution is here: https://developercommunity.visualstudio.com/content/problem/130597/unity-intellisense-not-working-after-creating-new-1.html (or google "unity intellisense not working new script")
 * 
 * 
 * Please read the manual under "/Docs", as it contains everything you need to know in order to get started, including FAQ
 */

using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using frame8.Logic.Misc.Other.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace LuanVan.OSA
{
    // There are 2 important callbacks you need to implement, apart from Start(): CreateViewsHolder() and UpdateViewsHolder()
    // See explanations below
    public class UIMultiPrefabsOSA : OSA<MyParams, BaseVH>
    {
        // Helper that stores data and notifies the adapter when items count changes
        // Can be iterated and can also have its elements accessed by the [] operator
        public SimpleDataHelper<BaseModel> Data { get; private set; }


        #region OSA implementation
        protected override void Start()
        {
            Data = new SimpleDataHelper<BaseModel>(this);

            // Calling this initializes internal data and prepares the adapter to handle item count changes
            base.Start();
        }

        // This is called initially, as many times as needed to fill the viewport, 
        // and anytime the viewport's size grows, thus allowing more items to be displayed
        // Here you create the "ViewsHolder" instance whose views will be re-used
        // *For the method's full content check the base implementation
        protected override BaseVH CreateViewsHolder(int itemIndex)
        {
            var modelType = Data[itemIndex].CachedType; // _ModelTypes[itemIndex];

            if (modelType == typeof(QuestionItemModel))
            {
                var vh = new QuestionItemViewsHolder();
                vh.Init(_Params.questionPrefab, _Params.Content, itemIndex);
                return vh;
            }

            if (modelType == typeof(ClassroomItemModel))
            {
                var vh = new ClassroomItemViewsHolder();
                vh.Init(_Params.classroomPrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(AnswerItemModel))
            {
                var vh = new AnswerItemViewsHolder();
                vh.Init(_Params.answerPrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(PostItemModel))
            {
                var vh = new PostItemViewsHolder();
                vh.Init(_Params.postPrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(PostTemplateItemModel))
            {
                var vh = new PostTemplateItemViewsHolder();
                vh.Init(_Params.postTemplatePrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(CommentItemModel))
            {
                var vh = new CommentItemViewsHolder();
                vh.Init(_Params.commentPrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(LatestOnlineUserItemModel))
            {
                var vh = new LatestOnlineUserItemViewsHolder();
                vh.Init(_Params.latestOnlineUserPrefab, _Params.Content, itemIndex);
                return vh;
            }            
            if (modelType == typeof(ProfileItemModel))
            {
                var vh = new ProfileItemViewsHolder();
                vh.Init(_Params.profilePrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(ClassroomInfoItemModel))
            {
                var vh = new ClassroomInfoItemViewsHolder();
                vh.Init(_Params.classroomInfoPrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(TopicItemModel))
            {
                var vh = new TopicItemViewsHolder();
                vh.Init(_Params.topicPrefab, _Params.Content, itemIndex);
                return vh;
            }
            if (modelType == typeof(TopicCommentItemModel))
            {
                var vh = new TopicCommentItemViewsHolder();
                vh.Init(_Params.topicCommentPrefab, _Params.Content, itemIndex);
                return vh;
            }

            throw new InvalidOperationException("Unrecognized model type: " + modelType.Name);
        }


        // This is called anytime a previously invisible item become visible, or after it's created, 
        // or when anything that requires a refresh happens
        // Here you bind the data from the model to the item's views
        // *For the method's full content check the base implementation
        protected override void UpdateViewsHolder(BaseVH newOrRecycled)
        {
            BaseModel model = Data[newOrRecycled.ItemIndex];

            newOrRecycled.UpdateViews(model, newOrRecycled);

            for (int i = 0; i < Data.Count; i++)
            {
                Data[i].id = i;
            }

            ScheduleComputeVisibilityTwinPass();
        }
        protected override void OnItemHeightChangedPreTwinPass(BaseVH vh)
        {
            base.OnItemHeightChangedPreTwinPass(vh);
            var m = Data[vh.ItemIndex];
            m.hasPendingSizeChange = false;
        }

        protected override bool IsRecyclable(BaseVH potentiallyRecyclable, int indexOfItemThatWillBecomeVisible, double sizeOfItemThatWillBecomeVisible)
        {
            BaseModel model = Data[indexOfItemThatWillBecomeVisible];

            for (int i = 0; i < Data.Count; i++)
            {
                Data[i].id = i;
            }

            return potentiallyRecyclable.CanPresentModelType(model.CachedType);
        }

        // This is the best place to clear an item's views in order to prepare it from being recycled, but this is not always needed, 
        // especially if the views' values are being overwritten anyway. Instead, this can be used to, for example, cancel an image 
        // download request, if it's still in progress when the item goes out of the viewport.
        // <newItemIndex> will be non-negative if this item will be recycled as opposed to just being disabled
        // *For the method's full content check the base implementation
        /*
		protected override void OnBeforeRecycleOrDisableViewsHolder(MyListItemViewsHolder inRecycleBinOrVisible, int newItemIndex)
		{
			base.OnBeforeRecycleOrDisableViewsHolder(inRecycleBinOrVisible, newItemIndex);
		}
		*/


        protected override void OnItemsRefreshed(int prevCount, int newCount)
        {
            base.OnItemsRefreshed(prevCount, newCount);

            for (int i = 0; i < Data.Count; i++)
            {
                Data[i].id = i;
            }
        }


        // You only need to care about this if changing the item count by other means than ResetItems, 
        // case in which the existing items will not be re-created, but only their indices will change.
        // Even if you do this, you may still not need it if your item's views don't depend on the physical position 
        // in the content, but they depend exclusively to the data inside the model (this is the most common scenario).
        // In this particular case, we want the item's index to be displayed and also to not be stored inside the model,
        // so we update its title when its index changes. At this point, the Data list is already updated and 
        // shiftedViewsHolder.ItemIndex was correctly shifted so you can use it to retrieve the associated model
        // Also check the base implementation for complementary info

        protected override void OnItemIndexChangedDueInsertOrRemove(BaseVH shiftedViewsHolder, int oldIndex, bool wasInsert, int removeOrInsertIndex)
        {
            base.OnItemIndexChangedDueInsertOrRemove(shiftedViewsHolder, oldIndex, wasInsert, removeOrInsertIndex);

            for (int i = 0; i < Data.Count; i++)
            {
                Data[i].id = i;
            }

            shiftedViewsHolder.MarkForRebuild();
            ScheduleComputeVisibilityTwinPass();
        }
        #endregion

        // These are common data manipulation methods
        // The list containing the models is managed by you. The adapter only manages the items' sizes and the count
        // The adapter needs to be notified of any change that occurs in the data list. Methods for each
        // case are provided: Refresh, ResetItems, InsertItems, RemoveItems
        #region data manipulation
        public void AddItemsAt(int index, IList<BaseModel> items)
        {
            // Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.InsertRange(index, items);
            //InsertItems(index, items.Length);

            Data.InsertItems(index, items);
        }

        public void RemoveItemsFrom(int index, int count)
        {
            // Commented: the below 2 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.RemoveRange(index, count);
            //RemoveItems(index, count);

            Data.RemoveItems(index, count);
        }

        public void SetItems(IList<BaseModel> items)
        {
            // Commented: the below 3 lines exemplify how you can use a plain list to manage the data, instead of a DataHelper, in case you need full control
            //YourList.Clear();
            //YourList.AddRange(items);
            //ResetItems(YourList.Count);

            Data.ResetItems(items);
        }
        #endregion
    }

    // This class keeps references to an item's views.
    // Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
    [Serializable] // serializable, so it can be shown in inspector
    public class MyParams : BaseParams
    {
        public RectTransform classroomPrefab;
        public RectTransform questionPrefab;
        public RectTransform answerPrefab;
        public RectTransform postPrefab;
        public RectTransform postTemplatePrefab;
        public RectTransform commentPrefab;
        public RectTransform latestOnlineUserPrefab;
        public RectTransform profilePrefab;
        public RectTransform classroomInfoPrefab;
        public RectTransform topicPrefab;
        public RectTransform topicCommentPrefab;
        public override void InitIfNeeded(IOSA iAdapter)
        {
            base.InitIfNeeded(iAdapter);

            if (classroomPrefab != null)
            {
                AssertValidWidthHeight(classroomPrefab);
            }
            if (questionPrefab != null)
            {
                AssertValidWidthHeight(questionPrefab);
            }            
            if (answerPrefab != null)
            {
                AssertValidWidthHeight(answerPrefab);
            }
            if (postPrefab != null)
            {
                AssertValidWidthHeight(postPrefab);
            }
            if (postTemplatePrefab != null)
            {
                AssertValidWidthHeight(postTemplatePrefab);
            }            
            if (commentPrefab != null)
            {
                AssertValidWidthHeight(commentPrefab);
            }
            if (latestOnlineUserPrefab != null)
            {
                AssertValidWidthHeight(latestOnlineUserPrefab);
            }
            if (profilePrefab != null)
            {
                AssertValidWidthHeight(profilePrefab);
            }
            if (classroomInfoPrefab != null)
            {
                AssertValidWidthHeight(classroomInfoPrefab);
            }
            if (topicPrefab != null)
            {
                AssertValidWidthHeight(topicPrefab);
            }
            if (topicCommentPrefab != null)
            {
                AssertValidWidthHeight(topicCommentPrefab);
            }

        }
    }
}
