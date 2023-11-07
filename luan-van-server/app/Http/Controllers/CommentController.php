<?php

namespace App\Http\Controllers;

use App\Models\Comment;
use App\Http\Requests\StoreCommentRequest;
use App\Http\Requests\UpdateCommentRequest;
use App\Models\CommentLike;
use App\Models\Notification;
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
                'user_id' => 'required|exists:users,id',
                'post_id' => 'required|exists:posts,id',
                'per_page' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'post_id.required' => 'Id Bài viết không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comments = Comment::where('post_id', $request->post_id)->where('comment_status_id', "1")->orderBy('created_at', 'DESC')->simplePaginate($request->per_page);

        foreach ($comments as $comment) {
            $comment->user;
            $comment->like_up = CommentLike::where("comment_id", $comment->id)->where("like_status", 1)->count();
            $comment->like_down = CommentLike::where("comment_id", $comment->id)->where("like_status", -1)->count();
            $comment->like_status = CommentLike::where("comment_id", $comment->id)->where("user_id", $request->user_id)->first();
        }

        return response()->json(['data' => $comments], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getOldComments(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required',
                'post_id' => 'required',
                'date' => 'required',
                'per_page' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'post_id.required' => 'Id Bài viết không được rỗng',
                'date.required' => 'phải có ngày bắt đầu lấy',
                'per_page.required' => 'Phải có số phần tử trên trang',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comments = Comment::where('post_id', $request->post_id)->where('comment_status_id', "1")->where('created_at', '<', $request->date)->orderBy('created_at', 'DESC')->simplePaginate($request->per_page);

        foreach ($comments as $comment) {
            $comment->user;
            $comment->like_up = CommentLike::where("comment_id", $comment->id)->where("like_status", 1)->count();
            $comment->like_down = CommentLike::where("comment_id", $comment->id)->where("like_status", -1)->count();
            $comment->like_status = CommentLike::where("comment_id", $comment->id)->where("user_id", $request->user_id)->first();
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
                'content' => 'required|min:1|max:256',
                'comment_status_id' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'post_id.required' => 'Id Bài viết không được rỗng',
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
                'comment_status_id.required' => 'Trạng thái không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comment = Comment::create([
            'post_id' => $request->post_id,
            'user_id' => $request->user_id,
            'content' => $request->content,
            'comment_status_id' => $request->comment_status_id,
        ]);

        $comment->user;

        if ($comment->post->user->id != $request->user_id) {
            Notification::updateOrCreate(
                [
                    'user_id' => $comment->post->user->id,
                    'sender_id' => $request->user_id,
                    'notification_type_id' => 2,
                ],
                [
                    'model_id' => $request->post_id,
                ]
            );
        }

        return response()->json(['data' => $comment], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function updateApi(StoreCommentRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'comment_id' => 'required',
                'content' => 'required|min:1|max:256',
                'comment_status_id' => 'required',
            ],
            [
                'comment_id.required' => 'Id Bài viết không được rỗng',
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
                'comment_status_id.required' => 'Trạng thái không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $comment = Comment::find($request->comment_id);

        $comment->content = $request->content;
        $comment->comment_status_id =  $request->comment_status_id;

        $comment->save();

        return response()->json(['data' => $comment], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
