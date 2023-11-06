<?php

namespace App\Http\Controllers;

use App\Models\Notification;
use App\Models\PostLike;
use App\Http\Requests\StorePostLikeRequest;
use App\Http\Requests\UpdatePostLikeRequest;
use Illuminate\Support\Facades\Validator;

class PostLikeController extends Controller
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
    public function store(StorePostLikeRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(PostLike $postLike)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(PostLike $postLike)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdatePostLikeRequest $request, PostLike $postLike)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(PostLike $postLike)
    {
        //
    }

    public function createOrUpdateLikeStatus(UpdatePostLikeRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'post_id' => 'required|exists:posts,id',
                'like_status' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'post_id.required' => 'Post Id không được rỗng',
                'like_status.required' => 'Trạng thái không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $post_like = PostLike::updateOrCreate(
            [
                'user_id' => $request->user_id,
                'post_id' => $request->post_id,
            ],
            [
                'like_status' => $request->like_status,
            ]
        );

        if ($post_like->post->user->id != $request->user_id) {
            Notification::updateOrCreate(
                [
                    'user_id' => $post_like->post->user->id,
                    'sender_id' => $request->user_id,
                    'notification_type_id' => ($post_like->like_status == 1) ? 1 : 5,
                ],
                [
                    'model_id' => $request->post_id,
                ]
            );
        }

        return response()->json(['data' => $post_like], 200, [], JSON_UNESCAPED_UNICODE);
    }
}