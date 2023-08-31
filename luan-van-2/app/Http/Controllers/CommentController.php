<?php

namespace App\Http\Controllers;

use App\Models\Comment;
use App\Http\Requests\StoreCommentRequest;
use App\Http\Requests\UpdateCommentRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class CommentController extends Controller
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
    public function store(StoreCommentRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(Comment $comment)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Comment $comment)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateCommentRequest $request, Comment $comment)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Comment $comment)
    {
        //
    }

    public function getComments(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required',
                'post_id' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'post_id.required' => 'Id Bài viết không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comments = Comment::where('post_id', $request->post_id)->orderBy('created_at', 'DESC')->get();

        foreach ($comments as $comment) {
            $comment->user;
        }

        return response()->json(['data' => $comments], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function storeApi(StoreCommentRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required',
                'post_id' => 'required',
                'content' => 'required|min:3',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'post_id.required' => 'Id Bài viết không được rỗng',
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung không được dưới 3 ký tự',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comment = Comment::create([
            'post_id' => $request->post_id,
            'user_id' => $request->user_id,
            'content' => $request->content,
        ]);

        $comment->user;

        return response()->json(['data' => $comment], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
