<?php

namespace App\Http\Controllers;

use App\Models\TopicCommentLike;
use App\Http\Requests\StoreTopicCommentLikeRequest;
use App\Http\Requests\UpdateTopicCommentLikeRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class TopicCommentLikeController extends Controller
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
    public function store(StoreTopicCommentLikeRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(TopicCommentLike $topicCommentLike)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(TopicCommentLike $topicCommentLike)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateTopicCommentLikeRequest $request, TopicCommentLike $topicCommentLike)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(TopicCommentLike $topicCommentLike)
    {
        //
    }

    public function updateLikeStatus(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'topic_comment_id' => 'required|exists:topic_comments,id',
                'status' => 'required',
            ],
            [
                'user_id.required' => 'user_id.required',
                'topic_comment_id.required' => 'topic_comment_id.required',
                'status.required' => 'status.required',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $status = TopicCommentLike::updateOrCreate(
            [
                'user_id' => $request->user_id,
                'topic_comment_id' => $request->topic_comment_id,
            ],
            [
                'like_status' => $request->status,
            ]
        );

        return response()->json(['data' => $status], 200, [], JSON_UNESCAPED_UNICODE);
    }
}