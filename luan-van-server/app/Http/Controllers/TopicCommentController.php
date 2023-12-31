<?php

namespace App\Http\Controllers;

use App\Models\Notification;
use App\Models\TopicComment;
use App\Http\Requests\StoreTopicCommentRequest;
use App\Http\Requests\UpdateTopicCommentRequest;
use App\Models\TopicCommentLike;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class TopicCommentController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreTopicCommentRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(TopicComment $topicComment)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(TopicComment $topicComment)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateTopicCommentRequest $request, TopicComment $topicComment)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(TopicComment $topicComment)
    {
        //
    }

    public function getTopicComments(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_topic_id' => 'required|exists:classrooms,id',
                'per_page' => 'required'
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'classroom_topic_id.required' => 'classroom_id.required',
                'per_page.required' => 'Phải có số phần tử trên trang',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comments = TopicComment::where("classroom_topic_id", $request->classroom_topic_id)
            ->where("topic_comment_status_id", "1")
            ->orderBy('created_at', 'DESC')
            ->simplePaginate($request->per_page);

        foreach ($comments as $comment) {
            $comment->user;
            $comment->like_up = TopicCommentLike::where("topic_comment_id", $comment->id)->where("like_status", 1)->count();
            $comment->like_down = TopicCommentLike::where("topic_comment_id", $comment->id)->where("like_status", -1)->count();
            $comment->like_status = TopicCommentLike::where("topic_comment_id", $comment->id)->where("user_id", $request->user_id)->first();
        }

        return response()->json(['data' => $comments], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getOldTopicComments(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_topic_id' => 'required|exists:classrooms,id',
                'per_page' => 'required',
                'date' => 'required'
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comments = TopicComment::where("classroom_topic_id", $request->classroom_topic_id)
            ->where("topic_comment_status_id", "1")
            ->where('created_at', '<', $request->date)
            ->orderBy('created_at', 'DESC')
            ->simplePaginate($request->per_page);

        foreach ($comments as $comment) {
            $comment->user;
            $comment->like_up = TopicCommentLike::where("topic_comment_id", $comment->id)->where("like_status", 1)->count();
            $comment->like_down = TopicCommentLike::where("topic_comment_id", $comment->id)->where("like_status", -1)->count();
            $comment->like_status = TopicCommentLike::where("topic_comment_id", $comment->id)->where("user_id", $request->user_id)->first();
        }

        return response()->json(['data' => $comments], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function uploadATopicComment(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_topic_id' => 'required|exists:classrooms,id',
                'content' => 'required|min:1|max:256',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'classroom_topic_id.required' => 'classroom_id.required',
                'classroom_topic_id.exists' => 'Bài đăng không tồn tại',
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comment = TopicComment::create(
            [
                'user_id' => $request->user_id,
                'classroom_topic_id' => $request->classroom_topic_id,
                'content' => $request->content,
            ]
        );
        $comment->user;

        if ($comment->topic->user->id != $request->user_id) {
            Notification::updateOrCreate(
                [
                    'user_id' => $comment->topic->user->id,
                    'sender_id' => $request->user_id,
                    'notification_type_id' => 7,
                ],
                [
                    'model_id' => $request->classroom_topic_id,
                ]
            );
        }
        
        return response()->json(['data' => $comment], 200, [], JSON_UNESCAPED_UNICODE);
    }
    public function updateATopicComment(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'id' => 'required|exists:topic_comments,id',
                'content' => 'required|min:1|max:256',
                'topic_comment_status_id' => 'required',
            ],
            [
                'id.required' => 'id.required',
                'topic_comment_status_id.required' => 'topic_comment_status_id.required',
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comment = TopicComment::updateOrCreate(
            [
                'id' => $request->id,
            ],
            [
                'content' => $request->content,
                'topic_comment_status_id' => $request->topic_comment_status_id,
            ]
        );

        return response()->json(['data' => $comment], 200, [], JSON_UNESCAPED_UNICODE);
    }
}