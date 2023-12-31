<?php

namespace App\Http\Controllers;

use App\Models\Comment;
use App\Models\Post;
use App\Http\Requests\StorePostRequest;
use App\Http\Requests\UpdatePostRequest;
use App\Models\PostLike;
use App\Models\PostTemplate;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Storage;
use Illuminate\Support\Facades\Validator;
use Illuminate\Validation\Rule;
use Illuminate\Validation\Rules\File;

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
                'user_id' => 'required|exists:users,id',
                'per_page' => 'required',
                'filter' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        if ($request->filter != "0") {
            $posts = Post::orderBy('created_at', 'DESC')->where('post_template_id', $request->filter)->where('post_status_id', '1')->simplePaginate($request->per_page);

        }

        if ($request->filter == "0") {
            $posts = Post::orderBy('created_at', 'DESC')->where('post_status_id', '1')->simplePaginate($request->per_page);
        }


        foreach ($posts as $post) {
            $post->postTemplate;
            $post->user;
            $post->comment_count = Comment::where("post_id", $post->id)->where('comment_status_id', "1")->count();
            $post->post_likes_up = PostLike::where("post_id", $post->id)->where("like_status", 1)->count();
            $post->post_likes_down = PostLike::where("post_id", $post->id)->where("like_status", -1)->count();
            $post->like_status = PostLike::where("post_id", $post->id)->where("user_id", $request->user_id)->first();

            // if (Storage::disk('public')->exists('posts/' . $post->id . ".png")) {
            //     $post->image_path = Storage::url('posts/' . $post->id . ".png");
            // } else {
            //     $post->image_path = "";
            // }
        }

        return response()->json(['data' => $posts], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getPrivatePosts(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'per_page' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $posts = Post::where('user_id', $request->user_id)->where('post_status_id', '2')->orderBy('created_at', 'DESC')->simplePaginate($request->per_page);

        foreach ($posts as $post) {
            $post->postTemplate;
            $post->user;
            $post->comment_count = Comment::where("post_id", $post->id)->where('comment_status_id', "1")->count();
            $post->post_likes_up = PostLike::where("post_id", $post->id)->where("like_status", 1)->count();
            $post->post_likes_down = PostLike::where("post_id", $post->id)->where("like_status", -1)->count();
            $post->like_status = PostLike::where("post_id", $post->id)->where("user_id", $request->user_id)->first();
        }

        return response()->json(['data' => $posts], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getOldPosts(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'per_page' => 'required',
                'date' => 'required',
                'filter' => 'required'

            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
                'date.required' => 'phải có ngày bắt đầu lấy',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        if ($request->filter != "0") {
            $posts = Post::orderBy('created_at', 'DESC')->where('post_template_id', $request->filter)->where('post_status_id', '1')->where('created_at', '<', $request->date)->simplePaginate($request->per_page);

        }

        if ($request->filter == "0") {
            $posts = Post::orderBy('created_at', 'DESC')->where('post_status_id', '1')->where('created_at', '<', $request->date)->simplePaginate($request->per_page);
        }



        foreach ($posts as $post) {
            $post->postTemplate;
            $post->user;
            $post->comment_count = Comment::where("post_id", $post->id)->where('comment_status_id', "1")->count();
            $post->post_likes_up = PostLike::where("post_id", $post->id)->where("like_status", 1)->count();
            $post->post_likes_down = PostLike::where("post_id", $post->id)->where("like_status", -1)->count();
            $post->like_status = PostLike::where("post_id", $post->id)->where("user_id", $request->user_id)->first();

            // if (Storage::disk('public')->exists('posts/' . $post->id . ".png")) {
            //     $post->image_path = Storage::url('posts/' . $post->id . ".png");
            // } else {
            //     $post->image_path = "";
            // }
        }

        return response()->json(['data' => $posts], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function uploadAPost(StorePostRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'content' => 'required|min:1|max:256',
                'user_id' => 'required|exists:users,id',
                'post_template_id' => 'required|exists:post_templates,id',
                'post_status_id' => 'required',
                'title' => 'sometimes|max:128',
                'image' => [
                    'sometimes',
                    File::image()
                        ->min(1)
                        ->max(64 * 1024)
                ],
            ],
            [
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
                'title.max' => 'Tiêu đề tối đa 128 ký tự',
                'user_id.required' => 'User Id không được rỗng',
                'post_template_id.required' => 'Id mẫu bài viết không được rỗng',
                'post_status_id.required' => 'trạng thái bài viết không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()->first()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $template = PostTemplate::find($request->post_template_id);
        
        if ($template->is_require_title) {
            if (empty($request->title)) {
                return response()->json(['message' => "Cần phải có tiêu đề của bài viết"], 200, [], JSON_UNESCAPED_UNICODE);
            }
        }

        if ($template->is_require_image) {
            if (!isset($request->image)) {
                return response()->json(['message' => "Cần phải có hình ảnh của bài viết"], 200, [], JSON_UNESCAPED_UNICODE);
            }
        }


        $post = Post::create(
            [
                'user_id' => $request->user_id,
                'post_template_id' => $request->post_template_id,
                'content' => $request->content,
                'post_status_id' => $request->post_status_id,
            ]
        );

        if (isset($request->title)) {
            $post->title = $request->title;
            $post->save();
        }

        if (isset($request->image)) {
            Storage::disk('public')->putFileAs("posts", $request->image, $post->id . '.png');
        }

        return response()->json(['data' => $post], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function updatePost(UpdatePostRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'id' => 'required|exists:posts,id',
                'content' => 'required|min:1|max:256',
                'post_template_id' => 'required',
                'title' => 'sometimes|max:128',
                'post_status_id' => '',
                'image' => [
                    'sometimes',
                    File::image()
                        ->min(1)
                        ->max(64 * 1024)
                ]
            ],
            [
                'id.required' => 'Id bài viết không được rỗng',
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
                'title.max' => 'Tiêu đề tối đa 128 ký tự',
                'post_template_id.required' => 'Id của mẫu bài viết không được rỗng',
                'post_status_id' => '',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $template = PostTemplate::find($request->post_template_id);
        
        if ($template->is_require_title) {
            if (empty($request->title)) {
                return response()->json(['message' => "Cần phải có tiêu đề của bài viết"], 200, [], JSON_UNESCAPED_UNICODE);
            }
        }

        if ($template->is_require_image) {
            if (!isset($request->image)) {
                return response()->json(['message' => "Cần phải có hình ảnh của bài viết"], 200, [], JSON_UNESCAPED_UNICODE);
            }
        }
        
        $post = Post::find($request->id);
        $post->content = $request->content;
        $post->post_template_id = $request->post_template_id;
        $post->post_status_id = $request->post_status_id;
        $post->save();
        $post->postTemplate;

        if (isset($request->title)) {
            $post->title = $request->title;
            $post->save();
        }
        else {
            $post->title = "";
            $post->save();
        }

        $file = $request->image;
        if (isset($request->image)) {
            Storage::disk('public')->putFileAs("posts", $file, $post->id . '.png');
        } else {
            Storage::disk('public')->delete("posts/" . $post->id . '.png');
        }

        return response()->json(['data' => $post], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getUserPosts(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'other_user_id' => 'required|exists:users,id',
                'per_page' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'other_user_id.required' => 'Other User Id không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $posts = Post::where('user_id', $request->other_user_id)->where('post_status_id', '1')->orderBy('created_at', 'DESC')->simplePaginate($request->per_page);

        foreach ($posts as $post) {
            $post->postTemplate;
            $post->user;
            $post->comment_count = Comment::where("post_id", $post->id)->where('comment_status_id', "1")->count();
            $post->post_likes_up = PostLike::where("post_id", $post->id)->where("like_status", 1)->count();
            $post->post_likes_down = PostLike::where("post_id", $post->id)->where("like_status", -1)->count();
            $post->like_status = PostLike::where("post_id", $post->id)->where("user_id", $request->user_id)->first();

            // if (Storage::disk('public')->exists('posts/' . $post->id . ".png")) {
            //     $post->image_path = Storage::url('posts/' . $post->id . ".png");
            // } else {
            //     $post->image_path = "";
            // }
        }

        return response()->json(['data' => $posts], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getUserOldPosts(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'per_page' => 'required',
                'date' => 'required',
                'other_user_id' => 'required|exists:users,id',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
                'date.required' => 'phải có ngày bắt đầu lấy',
                'other_user_id.required' => 'Other User Id không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $posts = Post::where('user_id', $request->other_user_id)
            ->where('post_status_id', '1')
            ->where('created_at', '<', $request->date)
            ->orderBy('created_at', 'DESC')
            ->simplePaginate($request->per_page);

        foreach ($posts as $post) {
            $post->postTemplate;
            $post->user;
            $post->comment_count = Comment::where("post_id", $post->id)->where('comment_status_id', "1")->count();
            $post->post_likes_up = PostLike::where("post_id", $post->id)->where("like_status", 1)->count();
            $post->post_likes_down = PostLike::where("post_id", $post->id)->where("like_status", -1)->count();
            $post->like_status = PostLike::where("post_id", $post->id)->where("user_id", $request->user_id)->first();

            // if (Storage::disk('public')->exists('posts/' . $post->id . ".png")) {
            //     $post->image_path = Storage::url('posts/' . $post->id . ".png");
            // } else {
            //     $post->image_path = "";
            // }
        }

        return response()->json(['data' => $posts], 200, [], JSON_UNESCAPED_UNICODE);
    }
}