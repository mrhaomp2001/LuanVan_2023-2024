<?php

namespace App\Http\Controllers;

use App\Models\CommentLike;
use App\Http\Requests\StoreCommentLikeRequest;
use App\Http\Requests\UpdateCommentLikeRequest;
use App\Models\Notification;
use Illuminate\Support\Facades\Validator;

class CommentLikeController extends Controller
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
    public function store(StoreCommentLikeRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(CommentLike $commentLike)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(CommentLike $commentLike)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateCommentLikeRequest $request, CommentLike $commentLike)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(CommentLike $commentLike)
    {
        //
    }

    public function createOrUpdateLikeStatus(StoreCommentLikeRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'comment_id' => 'required|exists:comments,id',
                'like_status' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'comment_id.required' => 'Comment Id không được rỗng',
                'like_status.required' => 'Trạng thái không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comment_like = CommentLike::updateOrCreate(
            [
                'user_id' => $request->user_id,
                'comment_id' => $request->comment_id,
            ],
            [
                'like_status' => $request->like_status,
            ]
        );

        if ($comment_like->comment->user->id != $request->user_id) {
            Notification::updateOrCreate(
                [
                    'user_id' => $comment_like->comment->user->id,
                    'sender_id' => $request->user_id,
                    'notification_type_id' => ($comment_like->like_status == 1) ? 3 : 4,
                ],
                [
                    'model_id' => $request->comment_id,
                ]
            );
        }

        return response()->json(['data' => $comment_like], 200, [], JSON_UNESCAPED_UNICODE);
    }
}