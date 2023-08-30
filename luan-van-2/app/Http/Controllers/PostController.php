<?php

namespace App\Http\Controllers;

use App\Models\Post;
use App\Http\Requests\StorePostRequest;
use App\Http\Requests\UpdatePostRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class PostController extends Controller
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
    public function store(StorePostRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(Post $post)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Post $post)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdatePostRequest $request, Post $post)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Post $post)
    {
        //
    }

    
    public function getPosts(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user-id' => 'required',
            ],
            [
                'user-id.required' => 'User Id không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $posts = Post::all();
        foreach ($posts as $post) {
            $post->post_template;
            $post->user;
            $post->post_likes_up;
            $post->post_likes_down;
            
        }
        return response()->json(['data' => $posts], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function uploadAPost(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'content' => 'required',
                'user_id' => 'required',
                'post_template_id' => 'required',
            ],
            [
                'content.required' => 'Content không được rỗng',
                'user_id.required' => 'User Id không được rỗng',
                'post_template_id.required' => 'User Id không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $post = Post::create(
            [
                'user_id' => $request->user_id,
                'post_template_id' => $request->post_template_id,
                'content' => $request->content,
            ]
        );

        return response()->json(['data' => $post], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
